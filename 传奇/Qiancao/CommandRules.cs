/*
 * 纯白铃注册制规则表 —— 由 .workbuddy/tools/migrate_qiancao.py 从 QQMain.cs 自动提取。
 * 命令词有增删改时重新运行该脚本即可；fun = 规则下标 + 1，框架原样回传给 onMessagev3。
 */

using System;
using System.Collections.Generic;

namespace 传奇.Qiancao
{
    public sealed class CommandRule
    {
        public string Type;      // equals / startswith
        public string Key;
        public ulong Id;        // fun，下标 + 1
        public string Name;
        public int Role;
        public bool CaseSensitive;
    }

    public static class CommandRules
    {
        public static readonly CommandRule[] All = Build();

        private static CommandRule[] Build()
        {
            List<CommandRule> list = new List<CommandRule>();
            Add(list, "equals", "屏蔽本群");
            Add(list, "equals", "清除注册信息");
            Add(list, "equals", "屏幕监视");
            Add(list, "equals", "自动挂机");
            Add(list, "equals", "功能");
            Add(list, "equals", "菜单");
            Add(list, "equals", "咕咕鸡传奇");
            Add(list, "equals", "传奇");
            Add(list, "equals", "全部勾选");
            Add(list, "equals", "取消全部勾选");
            Add(list, "equals", "商铺");
            Add(list, "equals", "查看商铺");
            Add(list, "equals", "查看商铺装饰");
            Add(list, "equals", "查看商铺补给");
            Add(list, "equals", "查看商铺强化");
            Add(list, "equals", "查看商铺好友");
            Add(list, "equals", "查看商铺限量");
            Add(list, "equals", "查看垃圾系列");
            Add(list, "equals", "查看沃玛系列");
            Add(list, "equals", "查看祖玛系列");
            Add(list, "equals", "查看赤月系列");
            Add(list, "equals", "查看永恒首饰");
            Add(list, "equals", "查看永恒剑甲");
            Add(list, "equals", "查看毁灭首饰");
            Add(list, "equals", "查看毁灭剑甲");
            Add(list, "equals", "查看刑天首饰");
            Add(list, "equals", "查看焚天首饰");
            Add(list, "equals", "查看沧澜首饰");
            Add(list, "equals", "查看刑焚沧甲");
            Add(list, "equals", "查看刑焚沧剑");
            Add(list, "equals", "查看特殊物品");
            Add(list, "equals", "一键回收");
            Add(list, "equals", "回收装备");
            Add(list, "equals", "装备回收");
            Add(list, "equals", "合成宝石");
            Add(list, "equals", "升级宝石1");
            Add(list, "equals", "升级宝石2");
            Add(list, "equals", "升级宝石3");
            Add(list, "equals", "升级宝石4");
            Add(list, "equals", "升级宝石5");
            Add(list, "equals", "升级宝石6");
            Add(list, "equals", "升级宝石7");
            Add(list, "equals", "升级宝石8");
            Add(list, "equals", "升级宝石9");
            Add(list, "equals", "升级宝石10");
            Add(list, "equals", "升级宝石11");
            Add(list, "equals", "升级宝石12");
            Add(list, "equals", "升级宝石13");
            Add(list, "equals", "升级宝石14");
            Add(list, "equals", "升级宝石15");
            Add(list, "equals", "合成普通斗笠");
            Add(list, "equals", "合成赤月斗笠");
            Add(list, "equals", "合成永恒斗笠");
            Add(list, "equals", "合成毁灭斗笠");
            Add(list, "equals", "合成沧天斗笠");
            Add(list, "equals", "合成灭世斗笠");
            Add(list, "equals", "合成神器天尊");
            Add(list, "equals", "合成神器法神");
            Add(list, "equals", "合成神器圣战");
            Add(list, "equals", "世外高人");
            Add(list, "equals", "灭世斗笠");
            Add(list, "equals", "灭世宝石");
            Add(list, "equals", "灭世称号");
            Add(list, "equals", "灭世神技");
            Add(list, "equals", "高级宝石");
            Add(list, "equals", "领取大彻大悟");
            Add(list, "equals", "提升神技");
            Add(list, "equals", "升级永恒青铜");
            Add(list, "equals", "升级永恒白银");
            Add(list, "equals", "升级永恒黄金");
            Add(list, "equals", "升级永恒白金");
            Add(list, "equals", "升级永恒钻石");
            Add(list, "equals", "升级永恒星耀");
            Add(list, "equals", "升级永恒王者");
            Add(list, "equals", "升级永恒星辰");
            Add(list, "equals", "升级永恒魔王");
            Add(list, "equals", "升级永恒圣佛");
            Add(list, "equals", "充值使者");
            Add(list, "equals", "五大陆");
            Add(list, "equals", "轮回之地");
            Add(list, "equals", "情缘仙人");
            Add(list, "equals", "仙庭战鼓");
            Add(list, "equals", "天帝陵墓");
            Add(list, "equals", "天帝炼狱");
            Add(list, "equals", "天帝足球场");
            Add(list, "equals", "天帝后花园");
            Add(list, "equals", "天帝奴隶场");
            Add(list, "equals", "四大陆");
            Add(list, "equals", "大帝之城");
            Add(list, "equals", "我要成佛");
            Add(list, "equals", "一转圣地");
            Add(list, "equals", "三转圣地");
            Add(list, "equals", "五转圣地");
            Add(list, "equals", "转生使者");
            Add(list, "equals", "我佛慈悲");
            Add(list, "equals", "三大神器合成");
            Add(list, "equals", "首饰盒神技");
            Add(list, "equals", "三大陆");
            Add(list, "equals", "沙漠荒原");
            Add(list, "equals", "虚空无尽");
            Add(list, "equals", "缥缈之巅");
            Add(list, "equals", "幽暗村庄");
            Add(list, "equals", "二大陆");
            Add(list, "equals", "天宫仙境");
            Add(list, "equals", "boss之家");
            Add(list, "equals", "开启公益捐献");
            Add(list, "equals", "公益捐献");
            Add(list, "equals", "开启永恒之力");
            Add(list, "equals", "永恒之力");
            Add(list, "equals", "领取充值");
            Add(list, "equals", "赞助升级会员");
            Add(list, "equals", "元宝升级会员");
            Add(list, "equals", "赞助领取礼包1");
            Add(list, "equals", "赞助领取礼包2");
            Add(list, "equals", "赞助领取礼包3");
            Add(list, "equals", "白嫖领取礼包1");
            Add(list, "equals", "白嫖领取礼包2");
            Add(list, "equals", "白嫖领取礼包3");
            Add(list, "equals", "赞助礼包");
            Add(list, "equals", "唯一赞助");
            Add(list, "equals", "起步礼包");
            Add(list, "equals", "会员特权");
            Add(list, "equals", "开启发送动图");
            Add(list, "equals", "关闭发送动图");
            Add(list, "equals", "宣传码");
            Add(list, "equals", "路费");
            Add(list, "equals", "补签");
            Add(list, "equals", "签到");
            Add(list, "equals", "基本功能");
            Add(list, "equals", "仓库服务");
            Add(list, "equals", "险恶区域");
            Add(list, "equals", "危险区域");
            Add(list, "equals", "城市传送");
            Add(list, "equals", "地图传送");
            Add(list, "equals", "传送员");
            Add(list, "equals", "向商店移动");
            Add(list, "equals", "PK");
            Add(list, "equals", "pk");
            Add(list, "equals", "卖物品");
            Add(list, "equals", "万事通");
            Add(list, "equals", "下一页");
            Add(list, "equals", "上一页");
            Add(list, "equals", "老兵");
            Add(list, "equals", "回城");
            Add(list, "equals", "卖肉");
            Add(list, "equals", "查看时装");
            Add(list, "equals", "查看技能");
            Add(list, "equals", "查看属性");
            Add(list, "equals", "查看状态");
            Add(list, "equals", "查看装备");
            Add(list, "equals", "查看背包");
            Add(list, "equals", "查看附近怪物");
            Add(list, "equals", "高级口令");
            Add(list, "equals", "高级功能");
            Add(list, "equals", "地图口令");
            Add(list, "equals", "地图功能");
            Add(list, "equals", "商店口令");
            Add(list, "equals", "商店功能");
            Add(list, "equals", "GM功能");
            Add(list, "equals", "改性别");
            Add(list, "equals", "赞我");
            Add(list, "equals", "神秘地图");
            Add(list, "equals", "玛雅大厅");
            Add(list, "equals", "散人打宝");
            Add(list, "equals", "进行一转");
            Add(list, "equals", "进行二转");
            Add(list, "equals", "进行三转");
            Add(list, "equals", "进行四转");
            Add(list, "equals", "进行五转");
            Add(list, "equals", "战士");
            Add(list, "equals", "法师");
            Add(list, "equals", "道士");
            Add(list, "equals", "银杏小村");
            Add(list, "equals", "银杏山谷");
            Add(list, "equals", "边界小城");
            Add(list, "equals", "边界村");
            Add(list, "equals", "毒蛇山谷");
            Add(list, "equals", "比奇城");
            Add(list, "equals", "盟重");
            Add(list, "equals", "盟重土城");
            Add(list, "startswith", "进行");
            Add(list, "startswith", "@制造");
            Add(list, "startswith", "@11");
            Add(list, "startswith", "@金币");
            Add(list, "startswith", "@元宝");
            Add(list, "startswith", "@等级");
            Add(list, "startswith", "@声望");
            Add(list, "startswith", "@金刚石");
            Add(list, "startswith", "赠送#");
            Add(list, "startswith", "购买#");
            Add(list, "startswith", "查看称号");
            Add(list, "startswith", "勾选");
            Add(list, "startswith", "取消勾选");
            Add(list, "startswith", "交易#");
            Add(list, "startswith", "充值#");
            Add(list, "startswith", "设置普通自动吃药");
            Add(list, "startswith", "设置百分比自动吃药");
            Add(list, "startswith", "礼包码");
            Add(list, "startswith", "注册");
            Add(list, "startswith", "带装备");
            Add(list, "startswith", "取下");
            Add(list, "startswith", "使用技能");
            Add(list, "startswith", "打怪");
            Add(list, "startswith", "补偿活跃值");
            Add(list, "startswith", "所有人补偿活跃值");
            Add(list, "startswith", "下图");
            Add(list, "startswith", "前往");
            Add(list, "startswith", "买物品");
            Add(list, "startswith", "查看首杀");
            Add(list, "startswith", "查看首爆");
            Add(list, "startswith", "保管");
            Add(list, "startswith", "找回");
            Add(list, "startswith", "取回");
            Add(list, "startswith", "金币换金条");
            Add(list, "startswith", "金条换金币");
            return list.ToArray();
        }

        private static void Add(List<CommandRule> list, string type, string key)
        {
            list.Add(new CommandRule
            {
                Type = type,
                Key = key,
                Id = (ulong)(list.Count + 1),
                Name = "传奇指令",
                Role = 0,
                CaseSensitive = false,
            });
        }

        /// <summary>按 fun 取注册时的原命令词；fun 无效返回空串。</summary>
        public static string KeyOf(ulong fun)
        {
            if (fun == 0 || fun > (ulong)All.Length) return "";
            return All[(int)fun - 1].Key;
        }
    }
}
