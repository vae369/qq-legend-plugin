/*
 * 纯白铃框架插件导出入口（.NET Framework 4.7.2 / DllExport 版）
 *
 * 对照 SDK：qiancao_cs_sdk/plugin.cs
 * 差异：SDK 用 NativeAOT + [UnmanagedCallersOnly]；本工程是既有 .NET Framework
 *       WinForms 工程，绘图与业务代码必须保留，因此用项目原有的 DllExport 导出。
 *       导出名、调用约定(cdecl)、参数与返回语义与 SDK 完全一致。
 *
 * ⚠ 每个导出函数体都必须 try/catch 包死：异常逃出非托管边界会带走整个框架进程。
 * ⚠ onMessagev3 恒返回 IntPtr.Zero：消息由业务侧 Bot.SendMessageEx 自己发，
 *   不和框架约定返回值的归属 / 生命周期。
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace 传奇.Qiancao
{
    public static class QiancaoPlugin
    {
        /// <summary>框架告知的插件目录（统一 / 分隔，结尾带 /），老框架不调这个导出时为空。</summary>
        public static string PluginPath = "";

        private static readonly object InfoLock = new object();
        private static readonly object DispatchLock = new object();
        private static IntPtr _infoPtr = IntPtr.Zero;
        private static IntPtr _fallbackInfoPtr = IntPtr.Zero;

        /// <summary>同一条消息只处理一次：一条消息命中多条规则时框架可能多次投递入口。</summary>
        private static readonly Dictionary<string, DateTime> HandledMessages =
            new Dictionary<string, DateTime>();

        private static readonly string[] RuleGroups =
            { "equals", "startswith", "endswith", "contains", "regex", "event" };

        /* ==================================================================
         * 插件信息：只分配一次，指针长期有效（框架只读、不释放）
         * ================================================================== */

        [DllExport(ExportName = "get_plugin_info", CallingConvention = CallingConvention.Cdecl)]
        public static IntPtr GetPluginInfo(IntPtr uuid, IntPtr cb)
        {
            try
            {
                Native.Init(uuid, cb);
                lock (InfoLock)
                {
                    if (_infoPtr == IntPtr.Zero) _infoPtr = Native.Utf8(BuildPluginInfo());
                    return _infoPtr;
                }
            }
            catch (Exception e)
            {
                try { Bot.OutLog("[传奇] get_plugin_info 异常: " + e.Message); } catch (Exception) { }
                lock (InfoLock)
                {
                    if (_fallbackInfoPtr == IntPtr.Zero)
                        _fallbackInfoPtr = Native.Utf8("{\"name\":\"咕咕鸡传奇\",\"version\":\"2.0.0\"}");
                    return _fallbackInfoPtr;
                }
            }
        }

        /// <summary>手工拼 JSON：保证 fun 是 8 字节整数而不是字符串 / double。</summary>
        private static string BuildPluginInfo()
        {
            StringBuilder sb = new StringBuilder(4096);
            sb.Append("{\"name\":").Append(Js("咕咕鸡传奇"));
            sb.Append(",\"version\":").Append(Js("2.0.0"));
            sb.Append(",\"author\":").Append(Js("叙利亚"));
            sb.Append(",\"description\":").Append(Js("纯白铃框架版：指令由插件内部分发"));
            sb.Append(",\"icon\":").Append(Js(""));
            sb.Append(",\"id\":").Append(Js(""));
            sb.Append(",\"version2\":2");

            for (int g = 0; g < RuleGroups.Length; g++)
            {
                sb.Append(",\"").Append(RuleGroups[g]).Append("\":[");
                bool first = true;
                for (int i = 0; i < CommandRules.All.Length; i++)
                {
                    CommandRule r = CommandRules.All[i];
                    if (r.Type != RuleGroups[g]) continue;
                    if (!first) sb.Append(',');
                    first = false;
                    sb.Append("{\"key\":").Append(Js(r.Key));
                    sb.Append(",\"fun\":").Append(r.Id.ToString(CultureInfo.InvariantCulture));
                    sb.Append(",\"fun_s\":").Append(Js(r.Id.ToString(CultureInfo.InvariantCulture)));
                    sb.Append(",\"name\":").Append(Js(r.Name));
                    sb.Append(",\"role\":").Append(r.Role.ToString(CultureInfo.InvariantCulture));
                    sb.Append(",\"case_sensitive\":").Append(r.CaseSensitive ? "true" : "false");
                    sb.Append('}');
                }
                sb.Append(']');
            }
            sb.Append('}');
            return sb.ToString();
        }

        private static string Js(string s)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(s ?? "");
        }

        /* ==================================================================
         * 生命周期导出
         * ================================================================== */

        [DllExport(ExportName = "set_plugin_path", CallingConvention = CallingConvention.Cdecl)]
        public static void SetPluginPath(IntPtr p)
        {
            try
            {
                if (p != IntPtr.Zero) PluginPath = Native.FromUtf8(p) ?? "";
            }
            catch (Exception) { }
        }

        [DllExport(ExportName = "on_enable", CallingConvention = CallingConvention.Cdecl)]
        public static void OnEnable()
        {
            try { Bot.OutLog("[传奇] 插件已启用，指令数 " + CommandRules.All.Length); }
            catch (Exception) { }
        }

        [DllExport(ExportName = "on_disable", CallingConvention = CallingConvention.Cdecl)]
        public static void OnDisable()
        {
            try { }
            catch (Exception) { }
        }

        [DllExport(ExportName = "on_unload", CallingConvention = CallingConvention.Cdecl)]
        public static void OnUnload()
        {
            try
            {
                lock (DispatchLock) HandledMessages.Clear();
            }
            catch (Exception) { }
        }

        /// <summary>设置面板：沿用旧 MQset 的窗体，改在独立 STA 线程上打开。</summary>
        [DllExport(ExportName = "on_set", CallingConvention = CallingConvention.Cdecl)]
        public static void OnSet()
        {
            try
            {
                Thread t = new Thread(delegate()
                {
                    try
                    {
                        Application.EnableVisualStyles();
                        global::传奇.FrmMagicTest fm = new global::传奇.FrmMagicTest();
                        fm.ShowDialog();
                    }
                    catch (Exception) { }
                });
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception) { }
        }

        /* ==================================================================
         * 唯一消息入口（新版 3 参）
         * json 完整消息帧 / fun 规则下标+1（0 = 框架未匹配）/ cmd 生效命令词
         * ================================================================== */

        [DllExport(ExportName = "onMessagev3", CallingConvention = CallingConvention.Cdecl)]
        public static IntPtr OnMessageV3(IntPtr jsonPtr, ulong fun, IntPtr cmdPtr)
        {
            try
            {
                if (jsonPtr == IntPtr.Zero) return IntPtr.Zero;
                MessageInfo message = Bot.ParseMessage(Native.FromUtf8(jsonPtr));
                if (message == null || string.IsNullOrEmpty(message.MsgType)) return IntPtr.Zero;

                string cmd = cmdPtr == IntPtr.Zero ? "" : (Native.FromUtf8(cmdPtr) ?? "");
                message.Cmd = cmd;
                message.UserKey = Bot.UserKeyOf(message);

                // 用户在框架界面把「签到」改名成「打卡」时，正文是实输原文，
                // 用 cmd 把前缀还原成注册时的 key，业务侧的 switch 才能继续命中。
                string key = CommandRules.KeyOf(fun);
                if (key.Length > 0 && cmd.Length > 0 && cmd != key &&
                    message.Msg != null &&
                    message.Msg.StartsWith(cmd, StringComparison.OrdinalIgnoreCase))
                {
                    message.Msg = key + message.Msg.Substring(cmd.Length);
                }

                lock (DispatchLock)
                {
                    // 框架线程池并发投递：串行化业务处理，保护 ini 存档 / qqKeyDic 等共享状态。
                    if (AlreadyHandled(message)) return IntPtr.Zero;
                    global::传奇.QQMain.HandleMessage(message);
                }
            }
            catch (Exception e)
            {
                try { Bot.OutLog("[传奇] 处理消息异常: " + e.Message); } catch (Exception) { }
            }
            return IntPtr.Zero;
        }

        private static bool AlreadyHandled(MessageInfo m)
        {
            DateTime now = DateTime.UtcNow;
            if (HandledMessages.Count > 512)
            {
                List<string> expired = new List<string>();
                foreach (KeyValuePair<string, DateTime> kv in HandledMessages)
                {
                    if ((now - kv.Value).TotalMinutes > 5) expired.Add(kv.Key);
                }
                for (int i = 0; i < expired.Count; i++) HandledMessages.Remove(expired[i]);
            }

            string id = m.MsgId;
            if (string.IsNullOrEmpty(id)) return false;   // 没有 msgid 的事件不做去重
            if (HandledMessages.ContainsKey(id)) return true;
            HandledMessages[id] = now;
            return false;
        }
    }
}
