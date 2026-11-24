# -*- coding: utf-8 -*-
"""一次性迁移工具：把 QQMain.cs 的旧框架接口改为纯白铃 SDK 写法，并生成指令规则表。

产物：
  Qiancao/CommandRules.cs  —— 从 QQMain.cs 的 case / StartsWith 自动提取的注册制规则表
  QQMain.cs                —— 入口改 HandleMessage(MessageInfo)，调用改 Bot.SendMessageEx
  .workbuddy/tools/migrate_report.txt —— 替换统计，供核验
"""
import json
import os
import re

ROOT = r"E:\qq\QQ插件\传奇"
QQMAIN = os.path.join(ROOT, "QQMain.cs")
RULES = os.path.join(ROOT, "Qiancao", "CommandRules.cs")
REPORT = os.path.join(ROOT, ".workbuddy", "tools", "migrate_report.txt")

report = []


def read_text(path):
    with open(path, "r", encoding="utf-8-sig", newline="") as f:
        return f.read()


def write_text(path, text):
    with open(path, "w", encoding="utf-8-sig", newline="") as f:
        f.write(text)


def sub(pattern, repl, text, label, flags=0):
    global report
    text, n = re.subn(pattern, repl, text, flags=flags)
    report.append("%s: %d" % (label, n))
    return text


src = read_text(QQMAIN)
NL = "\r\n" if "\r\n" in src else "\n"

# ---------------------------------------------------------------- 1. 规则表
equals, starts = [], []
for line in src.splitlines():
    if line.strip().startswith("//"):
        continue
    for m in re.finditer(r'case\s+"([^"]+)"\s*:', line):
        equals.append(m.group(1))
    for m in re.finditer(r'StartsWith\("([^"]+)"\)', line):
        starts.append(m.group(1))


def uniq(items):
    return list(dict.fromkeys(items))


equals = uniq(equals)
starts = uniq(s for s in starts if s not in equals)
report.append("equals rules: %d" % len(equals))
report.append("startswith rules: %d" % len(starts))

lines = []
lines.append("/*")
lines.append(" * 纯白铃注册制规则表 —— 由 .workbuddy/tools/migrate_qiancao.py 从 QQMain.cs 自动提取。")
lines.append(" * 命令词有增删改时重新运行该脚本即可；fun = 规则下标 + 1，框架原样回传给 onMessagev3。")
lines.append(" */")
lines.append("")
lines.append("using System;")
lines.append("using System.Collections.Generic;")
lines.append("")
lines.append("namespace 传奇.Qiancao")
lines.append("{")
lines.append("    public sealed class CommandRule")
lines.append("    {")
lines.append("        public string Type;      // equals / startswith")
lines.append("        public string Key;")
lines.append("        public ulong Id;        // fun，下标 + 1")
lines.append("        public string Name;")
lines.append("        public int Role;")
lines.append("        public bool CaseSensitive;")
lines.append("    }")
lines.append("")
lines.append("    public static class CommandRules")
lines.append("    {")
lines.append("        public static readonly CommandRule[] All = Build();")
lines.append("")
lines.append("        private static CommandRule[] Build()")
lines.append("        {")
lines.append("            List<CommandRule> list = new List<CommandRule>();")
for k in equals:
    lines.append("            Add(list, \"equals\", %s);" % json.dumps(k, ensure_ascii=False))
for k in starts:
    lines.append("            Add(list, \"startswith\", %s);" % json.dumps(k, ensure_ascii=False))
lines.append("            return list.ToArray();")
lines.append("        }")
lines.append("")
lines.append("        private static void Add(List<CommandRule> list, string type, string key)")
lines.append("        {")
lines.append("            list.Add(new CommandRule")
lines.append("            {")
lines.append("                Type = type,")
lines.append("                Key = key,")
lines.append("                Id = (ulong)(list.Count + 1),")
lines.append("                Name = \"传奇指令\",")
lines.append("                Role = 0,")
lines.append("                CaseSensitive = false,")
lines.append("            });")
lines.append("        }")
lines.append("")
lines.append("        /// <summary>按 fun 取注册时的原命令词；fun 无效返回空串。</summary>")
lines.append("        public static string KeyOf(ulong fun)")
lines.append("        {")
lines.append("            if (fun == 0 || fun > (ulong)All.Length) return \"\";")
lines.append("            return All[(int)fun - 1].Key;")
lines.append("        }")
lines.append("    }")
lines.append("}")
lines.append("")
write_text(RULES, "\n".join(lines))

# ---------------------------------------------------------------- 2. QQMain.cs
# 2.1 using
src = sub(r'(using 传奇\.core;\r?\n)', '\\1using 传奇.Qiancao;' + NL, src, "add using", flags=re.M)

# 2.2 删除旧框架插件信息区（MQinfo / Version）
src = sub(r'\s*#region 插件配置文件、资源文件以及版本相关设置[\s\S]*?#endregion\r?\n',
          '\n', src, "drop MQinfo region")

# 2.3 入口：MQEvent(11 参) -> HandleMessage(MessageInfo)
old_head = re.compile(
    r'\s*\[DllExport\(ExportName = nameof\(MQEvent\)[^\]]*\]\s*'
    r'///此子程序[^\n]*\n\s*'
    r'public static int MQEvent\([^)]*\)\s*\{\s*'
    r'try\s*\{\s*switch \(MsgType\)\s*\{\s*'
    r'case 1:\s*case 5:\s*case 6:\s*case 4:\s*case 2:[^\n]*\n\s*'
    r'string msg = Msg\.Trim\(\);')
new_head = """
        /// <summary>
        /// 纯白铃框架唯一消息入口的业务分发，由 QiancaoPlugin.onMessagev3 调用。
        /// 旧框架 11 参 MQEvent 已由 MessageInfo 取代：QQ 号 -> openid，消息类型 -> Type。
        /// </summary>
        public static void HandleMessage(MessageInfo message)
        {
            int MsgType = (int)message.Type;
            string GroupQQ = message.GroupId ?? "";
            string FromQQ = message.UserKey;
            try
            {
                switch (MsgType)
                {
                    case 0: // 群
                    case 1: // 频道
                    case 2: // 私聊
                    case 3: // 频道私聊
                        string msg = (message.Msg ?? "").Trim();""".replace("\n", NL)
src, n = old_head.subn(new_head, src, count=1)
report.append("replace MQEvent head: %d" % n)

# 2.4 返回值：void 方法
src = sub(r'\breturn 1;', 'return;', src, "return 1 -> return")

# 2.5 消息格式：[Pic,route=] -> [image,path=]；[@xxx] -> <@xxx>
src = sub(r'\[Pic,route=', '[image,path=', src, "image tag")
src = sub(r'\[@([^\]\r\n]+)\]', r'<@\1>', src, "at tag")
src = sub(r'"\[@" \+ FromQQ \+ "\] "', '"<@" + FromQQ + "> "', src, "at tag (concat)")

# 2.6 跨群广播降级：SDK 没有「取所有群列表」API，群列表收敛为当前群，循环只跑一次
src = sub(r'Api\.ApiGetGroupList\(RobotQQ\)\.Replace\("\\r\\n", "#"\)',
          'GroupQQ', src, "group list -> current group")
src = sub(r'g\.Substring\(0, g\.IndexOf\("----"\)\)', 'g', src, "group id trim")
src = sub(r'[ \t]*Thread\.Sleep\(5000\);[ \t]*\r?\n', '', src, "drop broadcast sleep")

# 2.7 发消息：统一改 Bot.SendMessageEx(message, text, false, true)
src = sub(r'Api\.ApiSendMsg\(RobotQQ, MsgType, GroupQQ, FromQQ, (.*?), 3996\)',
          r'Bot.SendMessageEx(message, \1, false, true)', src, "send (main)")
src = sub(r'Api\.ApiSendMsg\(robotQQ, MsgType, groupQQ, FromQQ, (.*?), 3996\)',
          r'Bot.SendMessageEx(message, \1, false, true)', src, "send (menu)")
src = sub(r'Api\.ApiSendMsg\(RobotQQ, 2, g, FromQQ, (.*?), 3996\)',
          r'Bot.SendMessageEx(message, \1, false, true)', src, "send (broadcast)")

# 2.8 取昵称：api 9，收文本（短 id / openid 都认）
src = sub(r'Api\.ApiGetNick\(RobotQQ, FromQQ\)',
          r'Bot.GetUserName(message.AppId, FromQQ)', src, "get nickname (self)")
src = sub(r'Api\.ApiGetNick\(RobotQQ, qq2\)',
          r'Bot.GetUserName(message.AppId, qq2)', src, "get nickname (target)")

# 2.9 菜单辅助函数：签名统一收 MessageInfo
src = sub(r'\b(GMMenu|StoreMenu|MapMenu|AdvancedMenu|BaseMenu|LegendMenu)\(RobotQQ, GroupQQ, MsgType, FromQQ\)',
          r'\1(message)', src, "menu calls")
src = sub(r'private static void (GMMenu|StoreMenu|MapMenu|AdvancedMenu|BaseMenu)\(string robotQQ, string groupQQ, int MsgType, string FromQQ\)',
          r'private static void \1(MessageInfo message)', src, "menu signatures (private)")
src = sub(r'static void LegendMenu\(string robotQQ, string groupQQ, int MsgType, string FromQQ\)',
          r'static void LegendMenu(MessageInfo message)', src, "menu signature (LegendMenu)")

# 2.10 点赞降级：官方 QQ 机器人平台无点赞，SDK 26 个 api 也没有
src = sub(r'\bUpVote\(RobotQQ, FromQQ, GroupQQ, MsgType\)', 'UpVote(message)', src, "upvote call")
old_upvote = re.compile(
    r'        private static void UpVote\(string RobotQQ, string FromQQ, string GroupQQ, int MsgType\)\s*\{[\s\S]*?\n        \}\r?\n')
new_upvote = """        /// <summary>
        /// 点赞指令降级：纯白铃 SDK 的 26 个 api 未提供「点赞」，官方 QQ 机器人平台也没有该能力。
        /// 保留指令入口并明确告知用户，不编造不存在的接口。
        /// </summary>
        private static void UpVote(MessageInfo message)
        {
            Bot.SendMessageEx(message, "点赞功能在当前框架下不可用（平台未提供点赞能力）。", false, true);
        }
""".replace("\n", NL)
src, n = old_upvote.subn(new_upvote, src, count=1)
report.append("upvote body replace: %d" % n)

# 2.11 删除旧框架导出 MQset / MQend（窗体入口迁到 QiancaoPlugin.on_set）
src = sub(r'\s*#region 启动窗体设置配置[\s\S]*?#endregion\s*#region 插件关闭销毁事件[\s\S]*?#endregion\r?\n',
          '\n', src, "drop MQset/MQend regions")

# 2.12 残留检测
for token in ["Api.ApiSendMsg", "Api.ApiGetGroupList", "Api.ApiGetNick", "Api.ApiUpVote",
              "RobotQQ", "robotQQ", "groupQQ", "MQEvent", "MQinfo", "MQset", "MQend", "return 1;"]:
    report.append("remaining %-22s %d" % (token, src.count(token)))

write_text(QQMAIN, src)
report.append("QQMain.cs bytes: %d -> %d" % (len(read_text(QQMAIN).encode("utf-8")), len(src.encode("utf-8"))))
write_text(REPORT, "\n".join(report) + "\n")
