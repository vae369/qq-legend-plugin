using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 传奇.core;

namespace 传奇
{
    /// <summary>
    /// 首杀怪物类
    /// </summary>
    public class FirstKillMon
    {
        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonsterName { get; set; }
        /// <summary>
        /// 死亡时间
        /// </summary>
        public string DeadTime { get; set; }
        /// <summary>
        /// 所在地图
        /// </summary>
        public string MonsterMap { get; set; }
        /// <summary>
        /// 所属人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否击杀
        /// </summary>
        public string IsKill { get; set; }
        public int YB { get; set; }
        public int JB { get; set; }
        public int QQHB { get; set; }
        /// <summary>
        /// 增加活力值
        /// </summary>
        public int AcQty { get; set; }

        public static void AddFristKillMonster()
        {
            string path = LegendFilePath.Mud2Path + "首杀怪物提示.ini";
            IniFileHelper stdInI = new IniFileHelper(path);

            List<FirstKillMon> list = new List<FirstKillMon>();

            list.Add(new FirstKillMon { MonsterName = "沃玛教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 100000, YB = 100, AcQty = 1, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "触龙神", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 100000, YB = 100, AcQty = 1, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "石墓尸王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 100000, YB = 100, AcQty = 1, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "祖玛教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 100000, YB = 100, AcQty = 1, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "黄泉教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 100000, YB = 100, AcQty = 1, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "牛魔王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 100000, YB = 100, AcQty = 1, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "双头血魔", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 200000, YB = 200, AcQty = 2, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "双头金刚", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 200000, YB = 200, AcQty = 2, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "暗之双头血魔", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 300000, YB = 300, AcQty = 3, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "暗之双头金刚", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 300000, YB = 300, AcQty = 3, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "暗之黄泉教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 300000, YB = 300, AcQty = 3, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "暗之骷髅精灵", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 300000, YB = 300, AcQty = 3, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "暗之沃玛教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 300000, YB = 300, AcQty = 3, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "暗之虹魔教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 300000, YB = 300, AcQty = 3, QQHB = 0 });
            list.Add(new FirstKillMon { MonsterName = "赤月恶魔", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 500000, YB = 500, AcQty = 5, QQHB = 1 });
            list.Add(new FirstKillMon { MonsterName = "暗之触龙神", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 600000, YB = 800, AcQty = 10, QQHB = 1 });
            list.Add(new FirstKillMon { MonsterName = "暗之牛魔王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 600000, YB = 800, AcQty = 10, QQHB = 1 });
            list.Add(new FirstKillMon { MonsterName = "圣域教主", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 800000, YB = 1000, AcQty = 10, QQHB = 2 });
            list.Add(new FirstKillMon { MonsterName = "圣域牛魔王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 800000, YB = 1000, AcQty = 10, QQHB = 2 });
            list.Add(new FirstKillMon { MonsterName = "圣域金刚", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 800000, YB = 1000, AcQty = 10, QQHB = 2 });
            list.Add(new FirstKillMon { MonsterName = "圣域血魔", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 800000, YB = 1000, AcQty = 10, QQHB = 2 });
            list.Add(new FirstKillMon { MonsterName = "圣域修罗", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 800000, YB = 1200, AcQty = 12, QQHB = 2 });
            list.Add(new FirstKillMon { MonsterName = "圣域魔魂", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 800000, YB = 1500, AcQty = 15, QQHB = 2 });
            list.Add(new FirstKillMon { MonsterName = "地藏王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1000000, YB = 1500, AcQty = 15, QQHB = 3 });
            list.Add(new FirstKillMon { MonsterName = "玄武", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1200000, YB = 1800, AcQty = 18, QQHB = 3 });
            list.Add(new FirstKillMon { MonsterName = "装备全爆", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1200000, YB = 1800, AcQty = 18, QQHB = 3 });
            list.Add(new FirstKillMon { MonsterName = "龙王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1200000, YB = 1800, AcQty = 18, QQHB = 3 });
            list.Add(new FirstKillMon { MonsterName = "冰宫·大魔王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1300000, YB = 1800, AcQty = 18, QQHB = 3 });
            list.Add(new FirstKillMon { MonsterName = "幽暗·蛤蟆精", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "幽暗·火龙王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "沙漠·屠猎兽", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "荒原·大魔王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "酒老人", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "灭世·天帝", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "白虎圣王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "骷髅教皇", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1500000, YB = 2000, AcQty = 20, QQHB = 4 });
            list.Add(new FirstKillMon { MonsterName = "赤月恶魔[沙漠]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1800000, YB = 2200, AcQty = 22, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "秀儿哇哇爆", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1800000, YB = 2200, AcQty = 22, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "秀儿大魔王", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1800000, YB = 2200, AcQty = 22, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "玄武神", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1800000, YB = 2200, AcQty = 22, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "造化钟神秀", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 1800000, YB = 2200, AcQty = 25, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "蒂花之秀", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 2500, AcQty = 25, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "魔灵天珠", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 2500, AcQty = 25, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "魔灵恶魔", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 2500, AcQty = 25, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "魔灵猫妖", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 3000, AcQty = 30, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "天帝将军", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 3000, AcQty = 30, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "天帝玄壶", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 3000, AcQty = 30, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "卧龙战神", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 3000, AcQty = 30, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔王[神]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2000000, YB = 3000, AcQty = 30, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "暗之牛魔王[神]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2200000, YB = 3200, AcQty = 32, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "魔化天蓬元帅", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2200000, YB = 3200, AcQty = 32, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "天帝[神]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2200000, YB = 3200, AcQty = 32, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[诅咒魔树]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2200000, YB = 3200, AcQty = 32, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[火焰漩涡]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2200000, YB = 3200, AcQty = 32, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "归墟神龙[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2500000, YB = 3500, AcQty = 35, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[口交歹匕亻尔]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2500000, YB = 3500, AcQty = 35, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[劈死你]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2500000, YB = 3500, AcQty = 35, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[别脱我大裤衩]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2500000, YB = 3500, AcQty = 35, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[别摸我头]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2800000, YB = 3800, AcQty = 38, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[小拳拳锤你]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2800000, YB = 3800, AcQty = 38, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[你别脱裤子啊]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2800000, YB = 3200, AcQty = 38, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[别惹你爷]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2800000, YB = 3800, AcQty = 38, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[来啊快活啊]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 2800000, YB = 3800, AcQty = 38, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "天帝[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[轮回]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3000000, YB = 4000, AcQty = 40, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[起源]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 3500000, YB = 4500, AcQty = 45, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[幻境]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4000000, YB = 5000, AcQty = 50, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[命运]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 4500000, YB = 5500, AcQty = 55, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[变态]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5000000, YB = 5800, AcQty = 58, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[毁灭]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 5500000, YB = 6000, AcQty = 60, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "地龙[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "牛魔[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "血魔[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "金刚[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "黄泉[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "骷髅[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "沃玛[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "虹魔[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "重装[无敌]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 5 });
            list.Add(new FirstKillMon { MonsterName = "[变态黑轮]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[不可以色色]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "[妲己没我骚]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 6 });
            list.Add(new FirstKillMon { MonsterName = "小白[能吃是福]", UserName = "", DeadTime = "", IsKill = "未击杀", MonsterMap = "", JB = 6000000, YB = 6500, AcQty = 65, QQHB = 6 });

            for (int i = 0; i < list.Count; i++)
            {
                stdInI.Write("首杀怪物类", "首杀怪物" + (i + 1), Api.JsonSerialize(list[i]));
            }
        }

        public static List<FirstKillMon> GetFirstKillMons()
        {
            string path = LegendFilePath.Mud2Path + "首杀怪物提示.ini";
            IniFileHelper stdInI = new IniFileHelper(path);

            List<FirstKillMon> list = new List<FirstKillMon>();

            FirstKillMon sidt = null;
            var listKey1 = stdInI.GetAllKeys("首杀怪物类");
            foreach (var key in listKey1)
            {
                sidt = Api.JsonDeserialize<FirstKillMon>(stdInI.ReadValue("首杀怪物类", key));
                list.Add(sidt);
            }

            return list;
        }

        /// <summary>
        /// 怪物首杀-获得奖励
        /// </summary>
        public static string FirstKillMons(UserInfo user, List<FirstKillMon> firstKillMonList, string monsterName, string map)
        {
            string supNotifyMsg = "";
            int index = firstKillMonList.FindIndex(f => f.MonsterName == monsterName && f.IsKill == "未击杀");
            if (index >= 0)
            {
                var km = firstKillMonList[index];
                km.DeadTime = DateTime.Now.ToString();
                km.UserName = user.Name;
                km.IsKill = "已击杀";
                km.MonsterMap = map;

                
                string path = LegendFilePath.Mud2Path + "首杀怪物提示.ini";
                IniFileHelper stdInI = new IniFileHelper(path);
                stdInI.Write("首杀怪物类", "首杀怪物" + (index + 1), Api.JsonSerialize(km));

                user.User_General.JB += km.JB;
                user.User_General.YB += km.YB;
                user.User_General.ActivityQty += km.AcQty;
                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userIni = new IniFileHelper(newPath);
                userIni.Write("普通数据", "元宝", user.User_General.YB.ToString());
                userIni.Write("普通数据", "金币", user.User_General.JB.ToString());
                userIni.Write("普通数据", "活跃值", user.User_General.ActivityQty.ToString());

                supNotifyMsg = $"号外：恭喜玩家[{user.Name}]今日在地图[{km.MonsterMap}]第一个击杀怪物[{monsterName}],获得：";
                if (km.JB > 0)
                    supNotifyMsg += km.JB + "金币 ";
                if (km.YB > 0)
                    supNotifyMsg += km.YB + "元宝 ";
                if (km.AcQty > 0)
                    supNotifyMsg += km.AcQty + "活跃值 ";
                if (km.QQHB > 0)
                    supNotifyMsg += km.QQHB + "红包 ";
                supNotifyMsg += "奖励!输入 查看首杀 即可查看怪物首杀奖励,输入 上一页/下一页 查看其它怪物首杀奖励.";
            }
            return supNotifyMsg;
        }
    }

}
