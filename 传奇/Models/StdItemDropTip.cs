using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using 传奇.core;

namespace 传奇
{
    /// <summary>
    /// 物品掉落提示类
    /// </summary>
    public class StdItemDropTip
    {
        /// <summary>
        /// 装备名称
        /// </summary>
        public string StdName { get; set; }
        /// <summary>
        /// 掉落时间
        /// </summary>
        public string DropTime { get; set; }
        /// <summary>
        /// 掉落地图
        /// </summary>
        public string DropMap { get; set; }
        /// <summary>
        /// 所属怪物
        /// </summary>
        public string DropMonster { get; set; }
        /// <summary>
        /// 所属人
        /// </summary>
        public string DropUserName { get; set; }
        public int YB { get; set; }
        public int JB { get; set; }
        public int QQHB { get; set; }
        /// <summary>
        /// 增加活力值
        /// </summary>
        public int AcQty { get; set; }
        /// <summary>
        /// 掉落装备类型
        /// 1沃玛 2祖玛 3赤月
        /// </summary>
        public int DropStdType { get; set; }


        /// <summary>
        /// 是否领取
        /// </summary>
        public string IsCollect { get; set; }

        public static void AddStdItems()
        {
            string path = LegendFilePath.MonQuestPath + "装备首爆提示.ini";
            IniFileHelper stdInI = new IniFileHelper(path);

            List<StdItemDropTip> list = new List<StdItemDropTip>();

            #region =====沃玛装备====            
            list.Add(new StdItemDropTip() { StdName = "幽灵项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "生命项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "天珠项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });

            list.Add(new StdItemDropTip() { StdName = "龙之戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "红宝石戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "铂金戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });

            list.Add(new StdItemDropTip() { StdName = "幽灵手套", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "思贝儿手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "心灵手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000, QQHB = 0, YB = 100, AcQty = 1, DropStdType = 1 });

            list.Add(new StdItemDropTip() { StdName = "银蛇", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 0, YB = 200, AcQty = 2, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "无极棍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 0, YB = 200, AcQty = 2, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "井中月", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 0, YB = 200, AcQty = 2, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "炼狱", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 0, YB = 200, AcQty = 2, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "魔杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 0, YB = 200, AcQty = 2, DropStdType = 1 });
            list.Add(new StdItemDropTip() { StdName = "血饮", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 0, YB = 200, AcQty = 2, DropStdType = 1 });

            #endregion


            #region =====祖玛装备====           
            list.Add(new StdItemDropTip() { StdName = "绿色项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "恶魔铃铛", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "灵魂项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });

            list.Add(new StdItemDropTip() { StdName = "力量戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "紫碧螺", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "泰坦戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });

            list.Add(new StdItemDropTip() { StdName = "骑士手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "阎罗手套", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "龙之手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "三眼手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 100000, QQHB = 1, YB = 100, AcQty = 2, DropStdType = 2 });

            list.Add(new StdItemDropTip() { StdName = "裁决之杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 200000, QQHB = 1, YB = 200, AcQty = 4, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "骨玉权杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 200000, QQHB = 1, YB = 200, AcQty = 4, DropStdType = 2 });
            list.Add(new StdItemDropTip() { StdName = "龙纹剑", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 200000, QQHB = 1, YB = 200, AcQty = 4, DropStdType = 2 });

            #endregion

            #region =====赤月装备====
            list.Add(new StdItemDropTip() { StdName = "圣战头盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "法神头盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "天尊头盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });

            list.Add(new StdItemDropTip() { StdName = "圣战项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "法神项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "天尊项链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });

            list.Add(new StdItemDropTip() { StdName = "圣战戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "法神戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "天尊戒指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });

            list.Add(new StdItemDropTip() { StdName = "圣战手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "法神手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "天尊手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 500000, QQHB = 2, YB = 500, AcQty = 5, DropStdType = 3 });

            list.Add(new StdItemDropTip() { StdName = "屠龙", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1000000, QQHB = 3, YB = 1000, AcQty = 10, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "嗜魂法杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1000000, QQHB = 3, YB = 1000, AcQty = 10, DropStdType = 3 });
            list.Add(new StdItemDropTip() { StdName = "霸者之刃", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1000000, QQHB = 3, YB = 1000, AcQty = 10, DropStdType = 3 });

            #endregion

            #region =====40级衣服====            
            list.Add(new StdItemDropTip() { StdName = "天魔神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1200000, QQHB = 3, YB = 1100, AcQty = 11, DropStdType = 4 });
            list.Add(new StdItemDropTip() { StdName = "圣战宝甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1200000, QQHB = 3, YB = 1100, AcQty = 11, DropStdType = 4 });
            list.Add(new StdItemDropTip() { StdName = "法神披风", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1200000, QQHB = 3, YB = 1100, AcQty = 11, DropStdType = 4 });
            list.Add(new StdItemDropTip() { StdName = "霓裳羽衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1200000, QQHB = 3, YB = 1100, AcQty = 11, DropStdType = 4 });
            list.Add(new StdItemDropTip() { StdName = "天尊道袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1200000, QQHB = 3, YB = 1100, AcQty = 11, DropStdType = 4 });
            list.Add(new StdItemDropTip() { StdName = "天师长袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1200000, QQHB = 3, YB = 1100, AcQty = 11, DropStdType = 4 });
            #endregion

            #region =====45级剑甲=========            
            list.Add(new StdItemDropTip() { StdName = "黄金屠龙", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 4, YB = 1200, AcQty = 12, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "黄金噬魂法杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 4, YB = 1200, AcQty = 12, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "黄金逍遥扇", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 4, YB = 1200, AcQty = 12, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "白虎战甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 3, YB = 1300, AcQty = 13, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "白虎战衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 3, YB = 1300, AcQty = 13, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "青龙道袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 3, YB = 1300, AcQty = 13, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "青龙道衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 3, YB = 1300, AcQty = 13, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "朱雀神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 3, YB = 1300, AcQty = 13, DropStdType = 5 });
            list.Add(new StdItemDropTip() { StdName = "朱雀神衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1500000, QQHB = 3, YB = 1300, AcQty = 13, DropStdType = 5 });
            #endregion

            #region =====45级装备====            
            list.Add(new StdItemDropTip() { StdName = "宙神头盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒圣链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒圣镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒圣戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });

            list.Add(new StdItemDropTip() { StdName = "朱雀头盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒法链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒法镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒法戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });

            list.Add(new StdItemDropTip() { StdName = "道域头盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒道链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒道镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒道戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 4, YB = 1300, AcQty = 13, DropStdType = 6 });

            list.Add(new StdItemDropTip() { StdName = "永恒鲜花", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 5, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒魔剑", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 5, YB = 1300, AcQty = 13, DropStdType = 6 });
            list.Add(new StdItemDropTip() { StdName = "永恒圣刃", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 1800000, QQHB = 5, YB = 1300, AcQty = 13, DropStdType = 6 });

            #endregion

            #region =====48级装备====
            list.Add(new StdItemDropTip() { StdName = "毁灭战盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战章", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });

            list.Add(new StdItemDropTip() { StdName = "毁灭法盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法章", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });

            list.Add(new StdItemDropTip() { StdName = "毁灭道盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道章", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 4, YB = 1500, AcQty = 15, DropStdType = 7 });

            list.Add(new StdItemDropTip() { StdName = "毁灭战刃", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭法杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭羽扇", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭魔甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭战衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭魔衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            list.Add(new StdItemDropTip() { StdName = "毁灭道衣", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2000000, QQHB = 5, YB = 1500, AcQty = 15, DropStdType = 7 });
            #endregion

            #region =====52级装备====
            list.Add(new StdItemDropTip() { StdName = "刑天战盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天战链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天护腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天战戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天腰带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天战靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天勋章", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });

            list.Add(new StdItemDropTip() { StdName = "沧澜魔盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜魔链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜魔戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜腰带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜魔靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜勋章", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });

            list.Add(new StdItemDropTip() { StdName = "焚天道盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天道链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天手镯", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天腰带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天道靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天道戒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天勋章", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2200000, QQHB = 5, YB = 2000, AcQty = 20, DropStdType = 8 });

            list.Add(new StdItemDropTip() { StdName = "刑天之怒", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜之啸", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天之吼", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天战甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "刑天战袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜魔甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "沧澜魔袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天道甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            list.Add(new StdItemDropTip() { StdName = "焚天道袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2500000, QQHB = 6, YB = 2500, AcQty = 25, DropStdType = 8 });
            #endregion

            #region =====56级装备====
            list.Add(new StdItemDropTip() { StdName = "灭世神盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });

            list.Add(new StdItemDropTip() { StdName = "灭世之斩", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3000000, QQHB = 6, YB = 3000, AcQty = 30, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3000000, QQHB = 6, YB = 3000, AcQty = 30, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3000000, QQHB = 6, YB = 3000, AcQty = 30, DropStdType = 9 });
            #endregion


            #region =====56级装备====
            list.Add(new StdItemDropTip() { StdName = "灭世神盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 2800000, QQHB = 6, YB = 2800, AcQty = 28, DropStdType = 9 });

            list.Add(new StdItemDropTip() { StdName = "灭世之斩", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3000000, QQHB = 6, YB = 3000, AcQty = 30, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3000000, QQHB = 6, YB = 3000, AcQty = 30, DropStdType = 9 });
            list.Add(new StdItemDropTip() { StdName = "灭世神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3000000, QQHB = 6, YB = 3000, AcQty = 30, DropStdType = 9 });
            #endregion

            #region =====60级装备====
            list.Add(new StdItemDropTip() { StdName = "轮回神盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 3500000, QQHB = 6, YB = 3500, AcQty = 35, DropStdType = 10 });

            list.Add(new StdItemDropTip() { StdName = "轮回之斩", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 10 });
            list.Add(new StdItemDropTip() { StdName = "轮回神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 10 });
            #endregion

            #region =====70级装备====
            list.Add(new StdItemDropTip() { StdName = "起源神盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4000000, QQHB = 7, YB = 4000, AcQty = 40, DropStdType = 11 });

            list.Add(new StdItemDropTip() { StdName = "起源之斩", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 11 });
            list.Add(new StdItemDropTip() { StdName = "起源神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 11 });
            #endregion

            #region =====80级装备====
            list.Add(new StdItemDropTip() { StdName = "回忆神盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 4500000, QQHB = 8, YB = 4500, AcQty = 45, DropStdType = 12 });

            list.Add(new StdItemDropTip() { StdName = "回忆之斩", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 12 });
            list.Add(new StdItemDropTip() { StdName = "回忆神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 12 });
            #endregion

            #region =====90级装备====
            list.Add(new StdItemDropTip() { StdName = "命运神盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "命运神链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "命运神腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "命运神指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "命运神带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "命运神靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "命运神勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5000000, QQHB = 9, YB = 5000, AcQty = 50, DropStdType = 13 });

            list.Add(new StdItemDropTip() { StdName = "回忆之斩", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5500000, QQHB = 10, YB = 5500, AcQty = 55, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "回忆神甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5500000, QQHB = 10, YB = 5500, AcQty = 55, DropStdType = 13 });
            list.Add(new StdItemDropTip() { StdName = "回忆神袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 5500000, QQHB = 10, YB = 5500, AcQty = 55, DropStdType = 13 });
            #endregion

            #region =====100级装备====
            list.Add(new StdItemDropTip() { StdName = "边界の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6000000, QQHB = 10, YB = 6000, AcQty = 60, DropStdType = 14 });

            list.Add(new StdItemDropTip() { StdName = "边界の刺", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 14 });
            list.Add(new StdItemDropTip() { StdName = "边界の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 14 });
            #endregion

            #region =====150级装备====
            list.Add(new StdItemDropTip() { StdName = "毒蛇の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 6500000, QQHB = 11, YB = 6500, AcQty = 65, DropStdType = 15 });

            list.Add(new StdItemDropTip() { StdName = "毒蛇の刃", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 15 });
            list.Add(new StdItemDropTip() { StdName = "毒蛇の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 15 });
            #endregion

            #region =====200级装备====
            list.Add(new StdItemDropTip() { StdName = "沙漠の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7000000, QQHB = 12, YB = 7000, AcQty = 70, DropStdType = 16 });

            list.Add(new StdItemDropTip() { StdName = "沙漠の斧", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 16 });
            list.Add(new StdItemDropTip() { StdName = "沙漠の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 16 });
            #endregion

            #region =====250级装备====
            list.Add(new StdItemDropTip() { StdName = "古城の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 7500000, QQHB = 13, YB = 7500, AcQty = 75, DropStdType = 17 });

            list.Add(new StdItemDropTip() { StdName = "古城の刀", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 17 });
            list.Add(new StdItemDropTip() { StdName = "古城の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 17 });
            #endregion

            #region =====300级装备====
            list.Add(new StdItemDropTip() { StdName = "孤岛の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8000000, QQHB = 14, YB = 8000, AcQty = 80, DropStdType = 18 });

            list.Add(new StdItemDropTip() { StdName = "孤岛の剑", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8500000, QQHB = 15, YB = 8500, AcQty = 85, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8500000, QQHB = 15, YB = 8500, AcQty = 85, DropStdType = 18 });
            list.Add(new StdItemDropTip() { StdName = "孤岛の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 8500000, QQHB = 15, YB = 8500, AcQty = 85, DropStdType = 18 });
            #endregion

            #region =====350级装备====
            list.Add(new StdItemDropTip() { StdName = "仙门の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 9000000, QQHB = 15, YB = 9000, AcQty = 90, DropStdType = 19 });

            list.Add(new StdItemDropTip() { StdName = "仙门の杖", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 10000000, QQHB = 16, YB = 10000, AcQty = 100, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 10000000, QQHB = 16, YB = 10000, AcQty = 100, DropStdType = 19 });
            list.Add(new StdItemDropTip() { StdName = "仙门の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 10000000, QQHB = 16, YB = 10000, AcQty = 100, DropStdType = 19 });
            #endregion

            #region =====350级装备====
            list.Add(new StdItemDropTip() { StdName = "战神の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 20000000, QQHB = 16, YB = 20000, AcQty = 200, DropStdType = 20 });

            list.Add(new StdItemDropTip() { StdName = "战神の刃", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 17, YB = 30000, AcQty = 300, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 17, YB = 30000, AcQty = 300, DropStdType = 20 });
            list.Add(new StdItemDropTip() { StdName = "战神の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 17, YB = 30000, AcQty = 300, DropStdType = 20 });
            #endregion

            #region =====350级装备====
            list.Add(new StdItemDropTip() { StdName = "复古の盔", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の链", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の腕", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の指", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の带", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の靴", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の勋", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 30000000, QQHB = 18, YB = 30000, AcQty = 300, DropStdType = 21 });

            list.Add(new StdItemDropTip() { StdName = "复古の刃", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000000, QQHB = 20, YB = 50000, AcQty = 500, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の甲", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000000, QQHB = 20, YB = 50000, AcQty = 500, DropStdType = 21 });
            list.Add(new StdItemDropTip() { StdName = "复古の袍", DropMap = "", DropMonster = "", DropUserName = "", DropTime = "", IsCollect = "未领取", JB = 50000000, QQHB = 20, YB = 50000, AcQty = 500, DropStdType = 21 });
            #endregion

            for (int i = 0; i < list.Count; i++)
            {
                stdInI.Write("首爆装备类", "首爆装备" + (i + 1), Api.JsonSerialize(list[i]));
            }
        }

        public static List<StdItemDropTip> GetStdItems()
        {
            string path = LegendFilePath.MonQuestPath + "装备首爆提示.ini";
            IniFileHelper stdInI = new IniFileHelper(path);

            List<StdItemDropTip> list = new List<StdItemDropTip>();
            StdItemDropTip sidt = null;
            var listKey1 = stdInI.GetAllKeys("首爆装备类");
            foreach (var key in listKey1)
            {
                sidt = Api.JsonDeserialize<StdItemDropTip>(stdInI.ReadValue("首爆装备类", key));
                list.Add(sidt);
            }

            return list;
        }

        /// <summary>
        /// 装备首爆-获得奖励
        /// </summary>
        public static string SetStdItems(UserInfo user, List<StdItemDropTip> stdItemsList, string stdName, string map)
        {
            string supNotifyMsg = "";
            int index = stdItemsList.FindIndex(f => f.StdName == stdName && f.IsCollect == "未领取");
            if (index >= 0)
            {
                var std = stdItemsList[index];
                std.DropTime = DateTime.Now.ToString();
                std.DropUserName = user.Name;
                std.IsCollect = "已领取";
                std.DropMap = map;

                string path = LegendFilePath.MonQuestPath + "装备首爆提示.ini";
                IniFileHelper stdInI = new IniFileHelper(path);

                stdInI.Write("首爆装备类", "首爆装备" + (index + 1), Api.JsonSerialize(std));

                user.User_General.JB += std.JB;
                user.User_General.YB += std.YB;
                user.User_General.ActivityQty += std.AcQty;
                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userIni = new IniFileHelper(newPath);
                userIni.Write("普通数据", "元宝", user.User_General.YB.ToString());
                userIni.Write("普通数据", "金币", user.User_General.JB.ToString());
                userIni.Write("普通数据", "活跃值", user.User_General.ActivityQty.ToString());
                 
                var stdDBList = LegendApi.GetStdItemsList();
                path = LegendFilePath.LegendIMGDataPath + "Items\\" + stdDBList.Where(s => s.StdName == std.StdName).First().StdImgPath;

                supNotifyMsg = $"号外：恭喜玩家[{user.Name}]今日在地图{std.DropMap}杀死了{user.KillMon.MonName},第一个打到{std.StdName},获得：";
                if (std.JB > 0)
                    supNotifyMsg += std.JB + "金币 ";
                if (std.YB > 0)
                    supNotifyMsg += std.YB + "元宝 ";
                if (std.AcQty > 0)
                    supNotifyMsg += std.AcQty + "活跃值 ";
                if (std.QQHB > 0)
                    supNotifyMsg += std.QQHB + "红包 ";
                supNotifyMsg += $"奖励!输入 查看首爆 即可查看装备首爆奖励,输入 上一页/下一页 查看其它装备首爆奖励.[Pic,route={path}]";
            }

            return supNotifyMsg;
        }
    }
}
