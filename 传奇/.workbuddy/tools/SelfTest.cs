/*
 * 桥接层端到端自测（不依赖纯白铃框架）
 * 用一个假框架回调注入 Qiancao.Native，验证：消息帧解析、主键净化、规则表、
 * get_plugin_info JSON、api 2/9/1 调用参数、onMessagev3 分发与异常兜底。
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using 传奇.Qiancao;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr FakeCallback(IntPtr uuid, int apiId, int appid,
    IntPtr a1, IntPtr a2, IntPtr a3, IntPtr a4,
    IntPtr a5, IntPtr a6, IntPtr a7, IntPtr a8);

public static class SelfTest
{
    private static readonly List<string> Calls = new List<string>();
    private static readonly List<string> Results = new List<string>();
    private static Delegate _cb;
    private static int _pass;
    private static int _fail;

    private static IntPtr Utf8(string s)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s ?? "");
        IntPtr p = Marshal.AllocCoTaskMem(bytes.Length + 1);
        Marshal.Copy(bytes, 0, p, bytes.Length);
        Marshal.WriteByte(p, bytes.Length, 0);
        return p;
    }

    private static string S(IntPtr p)
    {
        if (p == IntPtr.Zero) return "";
        int len = 0;
        while (len < 1024 * 1024 && Marshal.ReadByte(p, len) != 0) len++;
        if (len == 0) return "";
        byte[] bytes = new byte[len];
        Marshal.Copy(p, bytes, 0, len);
        return Encoding.UTF8.GetString(bytes);
    }

    private static IntPtr FakeCb(IntPtr uuid, int apiId, int appid,
        IntPtr a1, IntPtr a2, IntPtr a3, IntPtr a4,
        IntPtr a5, IntPtr a6, IntPtr a7, IntPtr a8)
    {
        Calls.Add("api=" + apiId + " appid=" + appid +
                  " a1=" + S(a1) + " a2=" + S(a2) + " a3=" + S(a3) +
                  " a4=" + S(a4) + " a5=" + S(a5) + " a6=" + S(a6));
        if (apiId == 9) return Utf8("小明");
        return Utf8("{}");
    }

    private static void Check(string name, bool ok, string detail)
    {
        if (ok) _pass++; else _fail++;
        Results.Add((ok ? "[PASS] " : "[FAIL] ") + name + (detail.Length > 0 ? " -> " + detail : ""));
    }

    private static string LastCall()
    {
        return Calls.Count == 0 ? "" : Calls[Calls.Count - 1];
    }

    public static int Main()
    {
        Type cbType = typeof(Bot).Assembly.GetType("传奇.Qiancao.FrameworkCallback", true);
        _cb = Delegate.CreateDelegate(cbType,
            typeof(SelfTest).GetMethod("FakeCb", BindingFlags.Static | BindingFlags.NonPublic));
        IntPtr cbPtr = Marshal.GetFunctionPointerForDelegate(_cb);
        IntPtr uuidPtr = Utf8("selftest-uuid-0001");

        Type nativeType = typeof(Bot).Assembly.GetType("传奇.Qiancao.Native", true);
        MethodInfo init = nativeType.GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic);
        init.Invoke(null, new object[] { uuidPtr, cbPtr });
        Check("Native.Init 注入假回调", Bot.IsReady, "");

        const string frame =
            "{\"t\":\"GROUP_AT_MESSAGE_CREATE\",\"type\":0,\"appid\":1020,\"user_id\":7," +
            "\"at_you\":true,\"member_role\":3,\"GroupName\":\"测试群\"," +
            "\"d\":{\"group_openid\":\"ABC123\",\"id\":\"MSGID2\",\"content\":\"菜单\"," +
            "\"author\":{\"union_openid\":\"U1\",\"id\":\"U1\",\"username\":\"小明\"}," +
            "\"message_scene\":{\"ext\":[\"path1\"]}}}";

        MessageInfo m = Bot.ParseMessage(frame);
        Check("ParseMessage 解析消息帧",
              m != null && m.MsgType == "GROUP_AT_MESSAGE_CREATE" && m.AppId == 1020 &&
              m.UserId == 7 && m.Type == QcMsgType.Group && m.GroupId == "ABC123" &&
              m.User == "U1" && m.Msg == "菜单" && m.MsgId == "MSGID2" &&
              m.AtYou && m.MemberRole == 3 && m.ReplyTo == "[ref,path=path1]",
              m == null ? "null" : m.MsgType + "/" + m.GroupId + "/" + m.User);

        Check("ParseMessage 拒绝非法 JSON", Bot.ParseMessage("{not json") == null, "");
        Check("ParseMessage 拒绝空串", Bot.ParseMessage("") == null, "");

        m.User = "AB/C:1";
        string userKey = Bot.UserKeyOf(m);
        Check("UserKeyOf 净化 openid", userKey.IndexOf('/') < 0 && userKey.IndexOf(':') < 0 &&
              userKey.EndsWith("_7"), userKey);
        m.User = "U1";

        Check("规则表规模", CommandRules.All.Length == 215, CommandRules.All.Length.ToString());
        Check("KeyOf(1) 取原命令词", CommandRules.KeyOf(1) == "屏蔽本群", CommandRules.KeyOf(1));
        Check("KeyOf(0)/越界返回空", CommandRules.KeyOf(0) == "" && CommandRules.KeyOf(99999) == "", "");

        ulong menuFun = 0;
        for (int i = 0; i < CommandRules.All.Length; i++)
        {
            if (CommandRules.All[i].Key == "菜单") { menuFun = CommandRules.All[i].Id; break; }
        }
        Check("规则表含菜单及其 fun", menuFun > 0, menuFun.ToString());

        IntPtr infoPtr = QiancaoPlugin.GetPluginInfo(uuidPtr, cbPtr);
        string info = S(infoPtr);
        Check("get_plugin_info 返回 JSON", info.StartsWith("{") && info.Contains("\"name\":\"咕咕鸡传奇\""), "");
        Check("fun 是数字不是字符串", info.Contains("\"fun\":1"), "");
        Check("规则分组齐全",
              info.Contains("\"equals\":[") && info.Contains("\"startswith\":[") &&
              info.Contains("\"event\":[]"), "");
        Check("未导出 sdk 版本字段", !info.Contains("\"sdk\""), "");

        Calls.Clear();
        Bot.SendMessageEx(m, "pong", false, true);
        Check("SendMessageEx -> api 2 参数顺序",
              LastCall() == "api=2 appid=1020 a1=0 a2=ABC123 a3=pong a4=MSGID2 a5=false a6=true",
              LastCall());

        string nick = Bot.GetUserName(1020, "U1");
        Check("GetUserName -> api 9", nick == "小明" && LastCall().StartsWith("api=9 appid=1020 a1=U1"),
              LastCall());

        Bot.OutLog("selftest");
        Check("OutLog -> api 1", LastCall().StartsWith("api=1 appid=0 a1=selftest"), LastCall());

        Calls.Clear();
        IntPtr jsonPtr = Utf8(frame);
        IntPtr cmdPtr = Utf8("菜单");
        IntPtr ret = QiancaoPlugin.OnMessageV3(jsonPtr, menuFun, cmdPtr);
        bool anySend = false;
        for (int i = 0; i < Calls.Count; i++) if (Calls[i].StartsWith("api=2 ")) anySend = true;
        Check("onMessagev3 恒返回 NULL", ret == IntPtr.Zero, "");
        Check("onMessagev3 分发到业务并产生 api 2", anySend,
              Calls.Count == 0 ? "无回调" : LastCall());

        QiancaoPlugin.OnMessageV3(IntPtr.Zero, menuFun, cmdPtr);
        QiancaoPlugin.OnMessageV3(Utf8("{not json"), menuFun, Utf8("菜单"));
        QiancaoPlugin.OnMessageV3(Utf8(""), 0, Utf8(""));
        Check("NULL / 非法 JSON / 空串不崩", true, "");

        Results.Add("");
        Results.Add("PASS=" + _pass + " FAIL=" + _fail);
        File.WriteAllText(@"E:\qq\QQ插件\传奇\.workbuddy\tools\selftest_out.txt",
                          string.Join(Environment.NewLine, Results), Encoding.UTF8);
        return _fail == 0 ? 0 : 1;
    }
}
