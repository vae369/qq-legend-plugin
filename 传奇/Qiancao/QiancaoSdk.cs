/*
 * 纯白铃 SDK 桥接层（.NET Framework 4.7.2 版）
 *
 * 来源：E:\qq\纯白铃SDK(8种语言)\C#\qiancao_cs_sdk\sdk.cs
 * 本文件把 SDK 的 api 常量 / MessageInfo / Bot / 消息帧解析移植到当前项目。
 * 与 SDK 原版的差异（均为 .NET Framework 4.7.2 的运行时限制所致，接口语义保持一致）：
 *   1. SDK 用 [UnmanagedCallersOnly] + 函数指针调用框架回调；这里用 [DllExport] 导出
 *      + Marshal.GetDelegateForFunctionPointer 生成委托（见 QiancaoPlugin.cs）。
 *   2. SDK 用 Marshal.PtrToStringUTF8 / StringToCoTaskMemUTF8（.NET Core 才有）；
 *      这里用 Encoding.UTF8 自行编解码，并自行写 '\0' 结尾。
 *   3. SDK 用 System.Text.Json（AOT 零反射）；这里用项目已引用的
 *      System.Web.Extensions / JavaScriptSerializer，不引入新依赖。
 *
 * 框架回调契约（cdecl，8 个字符串参数）：
 *   char* cb(const char* uuid, int api_id, int appid, const char* a1 ... a8)
 * 回调返回的串由框架持有（每线程一份，只读，插件不得释放）；这里已拷成 string。
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace 传奇.Qiancao
{
    /// <summary>框架 api 号，与纯白铃各语言 SDK 完全一致。</summary>
    public static class QcApi
    {
        public const int OutLog = 1;
        public const int SendMessage = 2;
        public const int SendArk = 3;
        public const int DeleteMessage = 4;
        public const int GenerateShareLink = 5;
        public const int RespondInteraction = 6;
        public const int GetBotList = 7;
        public const int GetUserOpenid = 8;
        public const int GetUserName = 9;
        public const int GetMemberState = 16;
        public const int GetMemberList = 17;
        public const int GetGroupInfo = 18;
        public const int GetBotGroupState = 19;
        public const int SetJoinRequest = 20;
        public const int GetJoinRequestList = 21;
        public const int SetGroupMute = 22;
        public const int GetMuteList = 23;
        public const int BatchRemoveMembers = 24;
        public const int GetBlacklist = 25;
        public const int ModifyBlacklist = 26;
    }

    /// <summary>消息来源类型：框架下发的 type，0 群 / 1 频道 / 2 私聊 / 3 频道私聊。</summary>
    public enum QcMsgType
    {
        Unknown = -1,
        Group = 0,
        Guild = 1,
        C2C = 2,
        GuildC2C = 3,
    }

    /// <summary>消息事件。字段与 SDK 的 MessageInfo 一一对应，另加两个本项目扩展字段。</summary>
    public sealed class MessageInfo
    {
        public string MsgType;      // 原始事件名，如 GROUP_AT_MESSAGE_CREATE
        public int AppId;
        public int UserId;          // 框架分配的数字短 id
        public QcMsgType Type;
        public bool FullType;
        public bool AtYou;
        public int MemberRole;      // 1 超管 / 2 机器人管理 / 3 群主 / 4 管理 / 5 代管 / 6 群成员
        public string GroupId;      // 群 openid / 子频道 id / 私聊时等于发送人
        public string GuildId;
        public string User;         // 发送人 openid
        public string Nickname;
        public string Nickname2;
        public string GroupName;
        public string MsgId;
        public string Msg;          // 正文：用户实输原文
        public string ReplyTo;
        public string Raw;

        public string Cmd;          // 扩展：onMessagev3 第三参，生效命令词（用户改过名就是新名）
        public string UserKey;      // 扩展：业务主键，净化后的 openid，可直接用作 ini 文件名
    }

    /// <summary>框架回调的 C ABI 签名（cdecl）。返回值是框架持有的字符串指针。</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr FrameworkCallback(
        IntPtr uuid, int apiId, int appid,
        IntPtr a1, IntPtr a2, IntPtr a3, IntPtr a4,
        IntPtr a5, IntPtr a6, IntPtr a7, IntPtr a8);

    /// <summary>保存框架给的 uuid 与回调指针，并按 C ABI 反向调用任意 api 号。</summary>
    internal static class Native
    {
        private static long _uuid;
        private static long _empty;
        private static FrameworkCallback _cb;
        private static readonly object InitLock = new object();

        /// <summary>框架可能在任意线程调用导出函数，读写一律走 Volatile / Interlocked。</summary>
        internal static void Init(IntPtr uuid, IntPtr cb)
        {
            lock (InitLock)
            {
                long oldUuid = Interlocked.Exchange(ref _uuid, 0);
                if (oldUuid != 0) Marshal.FreeCoTaskMem(new IntPtr(oldUuid));
                long oldEmpty = Interlocked.Exchange(ref _empty, 0);
                if (oldEmpty != 0) Marshal.FreeCoTaskMem(new IntPtr(oldEmpty));

                string uuidText = uuid == IntPtr.Zero ? "" : (FromUtf8(uuid) ?? "");
                Volatile.Write(ref _uuid, Utf8(uuidText).ToInt64());
                Volatile.Write(ref _empty, Utf8("").ToInt64());

                // 必须把委托实例保存在静态字段，否则 GC 回收后回调指针会失效。
                FrameworkCallback fn = cb == IntPtr.Zero
                    ? null
                    : (FrameworkCallback)Marshal.GetDelegateForFunctionPointer(cb, typeof(FrameworkCallback));
                Volatile.Write(ref _cb, fn);
            }
        }

        internal static bool Ready { get { return Volatile.Read(ref _cb) != null; } }

        /// <summary>string → 自己管理的 UTF-8 内存（带 '\0' 结尾），用完必须 FreeCoTaskMem。</summary>
        internal static IntPtr Utf8(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s ?? "");
            IntPtr p = Marshal.AllocCoTaskMem(bytes.Length + 1);
            Marshal.Copy(bytes, 0, p, bytes.Length);
            Marshal.WriteByte(p, bytes.Length, 0);
            return p;
        }

        /// <summary>UTF-8 内存 → string（拷贝，不释放来源指针）。</summary>
        internal static string FromUtf8(IntPtr p)
        {
            if (p == IntPtr.Zero) return "";
            const int MaxLen = 16 * 1024 * 1024;
            int len = 0;
            while (len < MaxLen && Marshal.ReadByte(p, len) != 0) len++;
            if (len == 0) return "";
            byte[] bytes = new byte[len];
            Marshal.Copy(p, bytes, 0, len);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>调用框架任意 api。args 最多 8 个，未给的传空串；返回串已拷成 string。</summary>
        internal static string Call(int apiId, int appid, params string[] args)
        {
            FrameworkCallback fn = Volatile.Read(ref _cb);
            if (fn == null) return "";

            long empty = Volatile.Read(ref _empty);
            IntPtr[] a = new IntPtr[8];
            int made = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i < args.Length && args[i] != null && args[i].Length > 0)
                {
                    a[i] = Utf8(args[i]);
                    made = i + 1;
                }
                else
                {
                    a[i] = new IntPtr(empty);
                }
            }

            IntPtr ret;
            try
            {
                ret = fn(new IntPtr(Volatile.Read(ref _uuid)), apiId, appid,
                         a[0], a[1], a[2], a[3], a[4], a[5], a[6], a[7]);
            }
            finally
            {
                for (int i = 0; i < made; i++) Marshal.FreeCoTaskMem(a[i]);
            }
            return ret == IntPtr.Zero ? "" : FromUtf8(ret);
        }
    }

    /// <summary>业务侧唯一需要用的 API 层，方法名/参数顺序与纯白铃 C# SDK 的 Bot 一致。</summary>
    public static class Bot
    {
        public static bool IsReady { get { return Native.Ready; } }

        private static string Bool(bool b) { return b ? "true" : "false"; }

        /// <summary>逃生舱：直接调任意 api 号。</summary>
        public static string CallRaw(int apiId, int appid, params string[] args)
        {
            return Native.Call(apiId, appid, args);
        }

        /// <summary>输出日志到框架（api 1）。</summary>
        public static void OutLog(string text, int colorRgb = 0)
        {
            Native.Call(QcApi.OutLog, 0, text ?? "", colorRgb == 0 ? "" : colorRgb.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>用消息事件回消息（api 2）。async=true 不堵框架线程。</summary>
        public static string SendMessageEx(MessageInfo msg, string text, bool isWakeup, bool async)
        {
            if (msg == null || string.IsNullOrEmpty(msg.GroupId) || string.IsNullOrEmpty(text))
                return "{\"message\":\"Invalid input\"}";
            return Native.Call(QcApi.SendMessage, msg.AppId, ((int)msg.Type).ToString(CultureInfo.InvariantCulture),
                               msg.GroupId, text, msg.MsgId ?? "", Bool(isWakeup), Bool(async));
        }

        /// <summary>主动发到指定会话（api 2）。</summary>
        public static string SendMessage(int appid, QcMsgType type, string openid, string text,
                                         string msgid, bool isWakeup, bool async)
        {
            if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(text))
                return "{\"message\":\"Invalid input\"}";
            return Native.Call(QcApi.SendMessage, appid, ((int)type).ToString(CultureInfo.InvariantCulture),
                               openid, text, msgid ?? "", Bool(isWakeup), Bool(async));
        }

        /// <summary>取昵称（api 9）。userId 是文本，短 id / openid 都认。</summary>
        public static string GetUserName(int appid, string userId, bool useDefault = true)
        {
            return Native.Call(QcApi.GetUserName, appid, userId ?? "", Bool(useDefault));
        }

        /// <summary>用短 id 换 openid（api 8）。</summary>
        public static string GetUserOpenid(int appid, int userId)
        {
            return Native.Call(QcApi.GetUserOpenid, appid, userId.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 由 openid 生成业务主键。旧插件用 QQ 号当 ini 文件名，新框架只有 openid；
        /// 这里把非文件名安全字符替换成 '_' 并限长，避免路径注入 / 文件名过长。
        /// </summary>
        public static string UserKeyOf(MessageInfo m)
        {
            string key = m == null ? "" : (m.User ?? "");
            string suffix = m == null ? "0" : m.UserId.ToString(CultureInfo.InvariantCulture);
            if (key.Length == 0) key = "u" + suffix;

            StringBuilder sb = new StringBuilder(key.Length);
            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];
                bool safe = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ||
                            (c >= '0' && c <= '9') || c == '_' || c == '-';
                sb.Append(safe ? c : '_');
            }
            string safeKey = sb.ToString();
            if (safeKey.Length > 64) safeKey = safeKey.Substring(0, 64);
            return safeKey + "_" + suffix;
        }

        /// <summary>把框架传入的完整消息帧解析为 MessageInfo；解析失败返回 null。</summary>
        public static MessageInfo ParseMessage(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = int.MaxValue;
                IDictionary<string, object> root = jss.DeserializeObject(json) as IDictionary<string, object>;
                if (root == null) return null;

                MessageInfo m = new MessageInfo();
                m.Raw = json;
                m.MsgType = Jsonx.Str(root, "t");
                m.AppId = Jsonx.Int(root, "appid");
                m.UserId = Jsonx.Int(root, "user_id");
                m.AtYou = Jsonx.Bool(root, "at_you");
                m.MemberRole = Jsonx.Int(root, "member_role");
                m.GroupName = Jsonx.Str(root, "GroupName");
                m.Nickname2 = Jsonx.Str(root, "username");
                m.Type = QcMsgType.Unknown;
                m.GroupId = "";
                m.GuildId = "";
                m.User = "";
                m.Nickname = "";
                m.MsgId = "";
                m.Msg = "";
                m.ReplyTo = "";

                IDictionary<string, object> d = Jsonx.Obj(root, "d");
                if (d != null)
                {
                    m.GroupId = Jsonx.Str(d, "group_openid");
                    m.MsgId = Jsonx.Str(d, "id");
                    m.Msg = Jsonx.Str(d, "content");
                    m.GuildId = Jsonx.Str(d, "guild_id");
                    string channelId = Jsonx.Str(d, "channel_id");

                    IDictionary<string, object> scene = Jsonx.Obj(d, "message_scene");
                    if (scene != null)
                    {
                        object[] ext = Jsonx.Arr(scene, "ext");
                        if (ext != null && ext.Length > 0)
                            m.ReplyTo = "[ref,path=" + Jsonx.AsStr(ext[0]) + "]";
                    }

                    IDictionary<string, object> author = Jsonx.Obj(d, "author");
                    if (author != null)
                    {
                        m.User = Jsonx.Str(author, "union_openid");
                        if (m.User.Length == 0) m.User = Jsonx.Str(author, "id");
                        if (m.User.Length == 0) m.User = Jsonx.Str(author, "user_openid");
                        m.Nickname = Jsonx.Str(author, "username");
                    }
                    if (m.User.Length == 0) m.User = Jsonx.Str(d, "user_openid");

                    switch (m.MsgType)
                    {
                        case "GROUP_AT_MESSAGE_CREATE":
                        case "GROUP_MESSAGE_CREATE":
                            m.Type = QcMsgType.Group;
                            m.FullType = m.MsgType == "GROUP_MESSAGE_CREATE";
                            break;
                        case "C2C_MESSAGE_CREATE":
                            m.Type = QcMsgType.C2C;
                            m.GroupId = m.User;
                            break;
                        case "AT_MESSAGE_CREATE":
                        case "MESSAGE_CREATE":
                            m.Type = QcMsgType.Guild;
                            m.FullType = m.MsgType == "MESSAGE_CREATE";
                            m.GroupId = channelId;
                            break;
                        case "DIRECT_MESSAGE_CREATE":
                            m.Type = QcMsgType.GuildC2C;
                            break;
                    }

                    // 有 type 以框架为准（其它事件 4/5/7/8… 只有框架知道）。
                    object jt;
                    if (root.TryGetValue("type", out jt) && jt != null)
                    {
                        int t;
                        if (int.TryParse(Jsonx.AsStr(jt), NumberStyles.Any, CultureInfo.InvariantCulture, out t))
                            m.Type = (QcMsgType)t;
                    }
                }

                if (string.IsNullOrEmpty(m.GroupId)) m.GroupId = m.User;
                return m;
            }
            catch (Exception)
            {
                return null;   // 非法 JSON 不能让异常逃出导出函数边界
            }
        }
    }

    /// <summary>JavaScriptSerializer 返回值的取值辅助（避免各处重复 null / 类型判断）。</summary>
    internal static class Jsonx
    {
        internal static string AsStr(object v)
        {
            if (v == null) return "";
            string s = v as string;
            if (s != null) return s;
            if (v is bool) return (bool)v ? "true" : "false";
            return Convert.ToString(v, CultureInfo.InvariantCulture) ?? "";
        }

        internal static string Str(IDictionary<string, object> o, string name)
        {
            object v;
            if (o == null || !o.TryGetValue(name, out v)) return "";
            return AsStr(v);
        }

        internal static int Int(IDictionary<string, object> o, string name)
        {
            int n;
            if (int.TryParse(Str(o, name), NumberStyles.Any, CultureInfo.InvariantCulture, out n)) return n;
            return 0;
        }

        internal static bool Bool(IDictionary<string, object> o, string name)
        {
            object v;
            if (o == null || !o.TryGetValue(name, out v) || v == null) return false;
            if (v is bool) return (bool)v;
            string s = AsStr(v);
            return s == "true" || s == "1";
        }

        internal static IDictionary<string, object> Obj(IDictionary<string, object> o, string name)
        {
            object v;
            if (o == null || !o.TryGetValue(name, out v)) return null;
            return v as IDictionary<string, object>;
        }

        internal static object[] Arr(IDictionary<string, object> o, string name)
        {
            object v;
            if (o == null || !o.TryGetValue(name, out v)) return null;
            return v as object[];
        }
    }
}
