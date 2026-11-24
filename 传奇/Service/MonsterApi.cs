using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using 传奇.core;
using static System.Net.Mime.MediaTypeNames;

namespace 传奇
{
    /// <summary>
    /// 怪物处理类
    /// </summary>
    public class MonsterApi
    {
        static int JP_Qty = 0, YSJP_Qty = 0;
        static Random rnd = new Random();
        static string mon17 = LegendFilePath.LegendIMGDataPath + "Mon17\\";
        static string mon3 = LegendFilePath.LegendIMGDataPath + "Mon3\\";
        static string mon4 = LegendFilePath.LegendIMGDataPath + "Mon4\\";
        static string mon11 = LegendFilePath.LegendIMGDataPath + "Mon11\\";
        static string mon1 = LegendFilePath.LegendIMGDataPath + "Mon1\\";
        static string mon2 = LegendFilePath.LegendIMGDataPath + "Mon2\\";
        static string mon9 = LegendFilePath.LegendIMGDataPath + "Mon9\\";
        static string mon16 = LegendFilePath.LegendIMGDataPath + "Mon16\\";
        static string mon5 = LegendFilePath.LegendIMGDataPath + "Mon5\\";
        static string mon6 = LegendFilePath.LegendIMGDataPath + "Mon6\\";
        static string mon10 = LegendFilePath.LegendIMGDataPath + "Mon10\\";
        static string mon8 = LegendFilePath.LegendIMGDataPath + "Mon8\\";
        static string mon13 = LegendFilePath.LegendIMGDataPath + "Mon13\\";
        static string mon15 = LegendFilePath.LegendIMGDataPath + "Mon15\\";
        static string mon12 = LegendFilePath.LegendIMGDataPath + "Mon12\\";
        static string mon14 = LegendFilePath.LegendIMGDataPath + "Mon14\\";
        static string mon7 = LegendFilePath.LegendIMGDataPath + "Mon7\\";
        static string mon33 = LegendFilePath.LegendIMGDataPath + "Mon33\\";
        static string mon34 = LegendFilePath.LegendIMGDataPath + "Mon34\\";
        static string mon29 = LegendFilePath.LegendIMGDataPath + "Mon29\\";
        static string mon35 = LegendFilePath.LegendIMGDataPath + "Mon35\\";
        static string mon21 = LegendFilePath.LegendIMGDataPath + "Mon21\\";
        static string mon22 = LegendFilePath.LegendIMGDataPath + "Mon22\\";
        static string mon23 = LegendFilePath.LegendIMGDataPath + "Mon23\\";
        static string mon24 = LegendFilePath.LegendIMGDataPath + "Mon24\\";
        static string mon25 = LegendFilePath.LegendIMGDataPath + "Mon25\\";
        static string mon26 = LegendFilePath.LegendIMGDataPath + "Mon26\\";
        static string mon27 = LegendFilePath.LegendIMGDataPath + "Mon27\\";
        static string mon37 = LegendFilePath.LegendIMGDataPath + "Mon37\\";
        static string mon32 = LegendFilePath.LegendIMGDataPath + "Mon32\\";
        static string mon36 = LegendFilePath.LegendIMGDataPath + "Mon36\\";
        static string mon38 = LegendFilePath.LegendIMGDataPath + "Mon38\\";
        static string mon39 = LegendFilePath.LegendIMGDataPath + "Mon39\\";

        static string Mon_kulou = LegendFilePath.LegendIMGDataPath + "Mon-kulou\\";

        /// <summary>
        /// 根据怪物对象获取对应站立的图片地址
        /// </summary>
        /// <param name="MonsterDB"></param>
        /// <returns></returns>
        public static List<string> GetMonsterStandIMG(int monID)
        {
            string path = "";
            List<int> forList = new List<int>();
            List<string> list = new List<string>();

            List<MonsterDB> monsterList = LegendApi.GetMonsterList();
            var monster = monsterList.Where(m => m.MonID == monID).First();

            int index = 0;
            int num = 0;
            switch (monster.Appr)
            {
                #region Mon1
                case 0: num = 8; index = 0; path = mon1; break;//大刀卫
                case 1: num = 8; index = 280; path = mon1; break;//森林雪人
                case 2: num = 8; index = 560; path = mon1; break;//白日门刀卫
                #endregion

                #region Mon2
                case 10: forList.Add(0); path = mon2; break;//食人花
                #endregion

                #region Mon3
                case 20: num = 10; index = 0; path = mon3; break;//骷髅
                case 21: num = 10; index = 360; path = mon3; break;//掷斧骷髅 
                case 22: num = 10; index = 720; path = mon3; break;//骷髅战士
                case 23: num = 10; index = 1080; path = mon3; break;//骷髅战将
                case 24: num = 10; index = 1440; path = mon3; break;//洞蛆
                case 25: num = 10; index = 1800; path = mon3; break;//多钩猫
                case 26: num = 10; index = 2160; path = mon3; break;//钉耙猫
                case 27: num = 10; index = 2520; path = mon3; break;//稻草人
                case 28: num = 10; index = 2880; path = mon3; break;//暗黑战士
                case 29: num = 10; index = 2880; path = mon3; break;//粪虫
                #endregion

                #region Mon4
                case 30: num = 10; index = 0; path = mon4; break;//沃玛战士
                case 32: num = 10; index = 720; path = mon4; break;//沃玛勇士
                case 33: num = 10; index = 1080; path = mon4; break;//沃玛战将 
                case 31: num = 10; index = 360; path = mon4; break;//火焰沃玛
                case 34: num = 10; index = 1440; path = mon4; break;//沃玛教主 
                case 36: num = 10; index = 2160; path = mon4; break;//红蛇
                case 37: num = 10; index = 2520; path = mon4; break;//变异骷髅
                case 38: num = 10; index = 2880; path = mon4; break;//虎蛇
                case 39: num = 10; index = 3240; path = mon4; break;//楔蛾
                #endregion

                #region Mon5 
                case 40: num = 10; index = 0; path = mon5; break;// 僵尸5 僵尸50 
                case 41: num = 10; index = 600; path = mon5; break;// 角蝇
                case 42: num = 10; index = 720; path = mon5; break;// 蝙蝠
                case 43: num = 10; index = 1080; path = mon5; break;// 羊 
                case 44: num = 10; index = 1440; path = mon5; break;// 鹰 
                case 45: num = 10; index = 1800; path = mon5; break;// 盔甲虫 
                case 46: num = 10; index = 2160; path = mon5; break;// 大老鼠 
                case 47: num = 10; index = 2520; path = mon5; break;// 祖玛弓箭手
                case 48: num = 10; index = 2880; path = mon5; break;// 威思而小虫
                case 49: num = 10; index = 3240; path = mon5; break;// 沙虫
                #endregion

                #region Mon6
                case 50:
                case 51: index = 0; num = 10; path = mon6; break;// 石墓尸王
                case 52: index = 430; num = 10; path = mon6; break;// 僵尸3 僵尸30 
                case 53: index = 860; num = 10; path = mon6; break; // 僵尸4 僵尸40
                case 54: index = 1290; num = 10; path = mon6; break; // 僵尸4 僵尸40
                #endregion

                #region Mon6 
                case 61: index = 520; num = 10; path = mon7; break;// 祖玛雕像
                case 62: index = 960; num = 10; path = mon7; break;// 祖玛卫士
                case 63: index = 1320; num = 1; path = mon7; break;// 祖玛教主
                case 64: index = 0; num = 10; path = mon7; break; // 僵尸4 僵尸40
                #endregion

                #region Mon8
                case 70: num = 10; index = 0; path = mon8; break;//狼
                case 71: num = 10; index = 360; path = mon8; break;//弓箭手 鹰卫
                case 72: num = 10; index = 720; path = mon8; break;//虎卫
                case 73: num = 10; index = 1080; path = mon8; break;//蜈蚣
                case 74: num = 10; index = 1440; path = mon8; break;//黑暗恶趣
                #endregion

                #region Mon9
                case 80: num = 10; index = 0; path = mon9; break;//山洞蝙蝠
                case 81: num = 10; index = 360; path = mon9; break;// 跳跳蜂 
                case 82: num = 10; index = 720; path = mon9; break;// 蠕虫 
                case 83: num = 10; index = 1080; path = mon9; break;// 蠕虫 
                #endregion

                #region Mon10
                case 90: num = 10; index = 0; path = mon10; break;//多角虫
                case 91: num = 10; index = 360; path = mon10; break;//巨型多角虫
                case 92: num = 10; index = 720; path = mon10; break;//双头血魔
                case 93: num = 10; index = 1080; path = mon10; break;//双头金刚
                #endregion

                #region Mon11
                case 100: num = 10; index = 0; path = mon11; break;//半兽人
                #endregion

                #region Mon12
                case 110: num = 10; index = 0; path = mon12; break;//红野猪
                case 111: num = 10; index = 360; path = mon12; break;//黑野猪
                case 112: num = 10; index = 720; path = mon12; break;//白野猪
                case 113: num = 10; index = 1080; path = mon12; break;//月魔蜘蛛
                case 114: num = 10; index = 1440; path = mon12; break;//钢牙蜘蛛
                case 115: num = 10; index = 1800; path = mon12; break;//血僵尸
                case 116: num = 10; index = 2160; path = mon12; break;//邪恶巨人
                case 117: num = 10; index = 2520; path = mon12; break;//花吻蜘蛛
                case 118: num = 10; index = 2880; path = mon12; break;//黑锷蜘蛛
                case 119: num = 10; index = 3240; path = mon12; break;//天狼蜘蛛
                #endregion

                #region Mon13
                case 120: num = 10; index = 0; path = mon13; break;//钳虫
                case 121: num = 10; index = 360; path = mon13; break;//邪恶钳虫
                case 122: num = 10; index = 720; path = mon13; break;//封魔教主
                #endregion

                #region Mon14
                case 130: num = 10; index = 0; path = mon14; break;//蝎蛇
                case 131: num = 10; index = 360; path = mon14; break;//赤月恶魔
                #endregion

                #region Mon15 
                case 140: num = 10; index = 0; path = mon15; break;//触龙神
                case 141: num = 10; index = 360; path = mon15; break;//千年树妖
                case 142: num = 10; index = 720; path = mon15; break;//骨魔僵尸
                case 143: num = 10; index = 1080; path = mon15; break;//骨魔尸王
                case 144: num = 10; index = 1440; path = mon15; break;//骨魔锤兵
                case 145: num = 10; index = 1800; path = mon15; break;//骨魔XX兵
                case 146: num = 10; index = 2160; path = mon15; break;//骨魔刀兵
                case 147: num = 10; index = 2520; path = mon15; break;//骨魔弓箭手
                case 148: num = 10; index = 2880; path = mon15; break;//黄泉教主
                #endregion

                #region Mon16 
                case 150: num = 10; index = 0; path = mon16; break;//骷髅精灵 
                case 151: num = 10; index = 360; path = mon16; break;//沃玛卫士 
                case 152: num = 10; index = 720; path = mon16; break;//尸王 
                case 153: num = 10; index = 1080; path = mon16; break;//虹魔蝎卫
                #endregion

                #region mon17
                case 160: num = 10; index = 0; path = mon17; break;//鸡
                case 161: num = 10; index = 360; path = mon17; break;//鹿
                case 163: num = 10; index = 1080; path = mon17; break;//毒蜘蛛
                case 162: num = 10; index = 720; path = mon17; break;//蛤蟆
                case 164: num = 10; index = 1440; path = mon17; break;//邪恶毒蛇
                case 165:
                case 207: num = 10; index = 1880; path = mon17; break;//牛魔王
                #endregion

                #region Mon21
                case 201: num = 10; index = 360; path = mon21; break;//牛魔战士
                case 202: num = 10; index = 720; path = mon21; break;//牛魔斗士
                case 203: num = 10; index = 1080; path = mon21; break;//牛魔侍卫
                case 204: num = 10; index = 1440; path = mon21; break;//牛魔将军
                case 205: num = 10; index = 1800; path = mon21; break;//牛魔法师
                case 206: num = 10; index = 2160; path = mon21; break;//牛魔祭司

                #endregion

                #region Mon22
                case 101:
                case 210: num = 10; index = 0; path = mon22; break;//大帝近卫 卧龙刺客
                case 211: num = 10; index = 470; path = mon22; break;//大帝力士
                case 102:
                case 212: num = 10; index = 820; path = mon22; break;//大帝战将 卧龙战将
                case 218: num = 10; index = 2710; path = mon22; break;//装备全爆 魔龙教主
                #endregion

                #region Mon23
                case 222: num = 10; index = 430; path = mon23; break;//虚空
                #endregion

                #region Mon24
                case 230: num = 10; index = 0; path = mon24; break;//无尽巨镰
                case 231: num = 10; index = 340; path = mon24; break;//无尽金杖
                case 232: num = 10; index = 720; path = mon24; break;//无尽绿魔
                case 233: num = 10; index = 1180; path = mon24; break;//无尽金蛟
                case 234: num = 10; index = 1770; path = mon24; break;//无尽油白
                case 235: num = 10; index = 2610; path = mon24; break;//无尽铁蛾
                case 236: num = 10; index = 2950; path = mon24; break;//无尽黑牙
                case 237: num = 10; index = 4810; path = mon24; break;//无尽
                case 238: num = 10; index = 3750; path = mon24; break;//无尽蓝刺
                case 239: num = 10; index = 2950; path = mon24; break;//无尽蜘蛛精

                case 243: num = 10; index = 4460; path = mon24; break;//骷髅教皇
                #endregion

                #region Mon25
                case 244: num = 10; index = 1090; path = mon25; break;//缥缈大狮兽
                #endregion

                #region Mon26
                case 250: num = 10; index = 0; path = mon26; break;//冰宫战将
                case 251: num = 10; index = 510; path = mon26; break;//冰宫侍卫
                case 252: num = 10; index = 1020; path = mon26; break;//冰宫五毒魔
                case 253: num = 10; index = 1370; path = mon26; break;//冰宫灭天魔
                case 254: num = 10; index = 1720; path = mon26; break;//冰宫寒冰魔
                case 255: num = 10; index = 2070; path = mon26; break;//冰宫天将

                case 642:
                case 256: num = 10; index = 2740; path = mon26; break;//冰宫*大魔王
                #endregion

                #region Mon27
                case 262: num = 10; index = 680; path = mon27; break;//冰宫力士 
                case 263: num = 10; index = 1190; path = mon27; break;//冰宫天兵 
                case 266: num = 10; index = 2540; path = mon27; break;//冰宫·小旋风 
                case 267: num = 10; index = 3570; path = mon27; break;//冰宫·小霸王
                #endregion

                #region Mon29
                case 280: num = 10; index = 0; path = mon29; break;//玄武
                case 281: num = 10; index = 600; path = mon29; break;//酒老人
                #endregion

                #region Mon32
                case 310: num = 10; index = 170; path = mon32; break;//沙漠·屠猎兽
                case 329: num = 10; index = 6680; path = mon32; break;//荒原·大魔王
                #endregion

                #region Mon33
                case 320: num = 10; index = 0; path = mon33; break;//魔灵黑狐
                case 321: num = 10; index = 440; path = mon33; break;//魔灵赤狐
                case 322: num = 10; index = 820; path = mon33; break;//魔灵素狐
                case 327: num = 10; index = 2900; path = mon33; break;//魔灵天珠
                #endregion

                #region Mon34
                case 171: num = 10; index = 2540; path = mon34; break;//魔灵猫妖
                #endregion


                #region Mon35
                case 342: num = 10; index = 680; path = mon35; break;//龙王
                #endregion

                #region Mon36
                case 616: num = 10; index = 13800; path = mon36; break;//造化钟神秀
                case 628: num = 10; index = 24880; path = mon36; break;//白幽灵
                #endregion

                #region Mon37
                case 366: num = 10; index = 1840; path = mon37; break;//幽暗·蛤蟆精
                #endregion

                #region Mon38
                case 643: num = 10; index = 6680; path = mon38; break;//来自地狱的恶魔
                #endregion

                #region Mon39
                case 953: num = 10; index = 0; path = mon39; break;//灭世·天帝1
                #endregion

                #region Mon-kulou
                case 950: num = 10; index = 0; path = Mon_kulou; break;//亡灵骷髅
                case 951: num = 10; index = 360; path = Mon_kulou; break;//黄泉骷髅
                case 952: num = 10; index = 720; path = Mon_kulou; break;//地狱骷髅
                #endregion

                default:
                    break;
            }
            if (num > 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    forList.Add(index + (num * i));
                }
            }

            List<string> monPos = null;
            int monX;
            int monY;
            foreach (var fl in forList)
            {
                for (int i = fl; i < fl + 4; i++)
                {
                    monPos = FileHelper.ReadTxtReturnList($"{path}Placements\\{i.ToString().PadLeft(6, '0')}.txt");
                    monX = int.Parse(monPos[0]);
                    monY = int.Parse(monPos[1]);
                    list.Add($"{path + i.ToString().PadLeft(6, '0')}.png,{monX},{monY}");
                }
            }

            return list;
        }

        /// <summary>
        /// 获取怪物的图片
        /// </summary>
        /// <param name="killMon"></param>
        /// <returns></returns>
        public static List<string> GetMonsterImgList(int monID)
        {
            try
            {
                string monPath = "";
                List<string> monPos = new List<string>();
                List<string> list = new List<string>();
                List<MonsterDB> monsterList = LegendApi.GetMonsterList();
                var monster = monsterList.Where(m => m.MonID == monID).First();

                int index = 0, num = 360;
                switch (monster.Appr)
                {
                    #region Mon1
                    case 0: index = 0; num = 190; monPath = mon1; break;//大刀卫
                    case 1: index = 280; num = 280; monPath = mon1; break;//森林雪人
                    case 2: index = 560; num = 190; monPath = mon1; break;//白日门刀卫
                    #endregion

                    #region Mon2
                    case 10: index = 0; num = 210; monPath = mon2; break;//食人花
                    #endregion

                    #region Mon3
                    case 20: index = 0; monPath = mon3; break;//骷髅
                    case 21: index = 360; monPath = mon3; break;//掷斧骷髅
                    case 22: index = 720; monPath = mon3; break;//骷髅战士
                    case 23: index = 1080; monPath = mon3; break;//骷髅战将
                    case 24: index = 1440; monPath = mon3; break;//洞蛆
                    case 25: index = 1800; monPath = mon3; break;//多钩猫
                    case 26: index = 2160; monPath = mon3; break;//钉耙猫
                    case 27: index = 2520; monPath = mon3; break;//稻草人
                    case 28: index = 2880; monPath = mon3; break;//暗黑战士
                    case 29: index = 3240; monPath = mon3; break;//粪虫
                    #endregion

                    #region Mon4 
                    case 30: index = 0; monPath = mon4; break;//沃玛战士 
                    case 32: index = 720; monPath = mon4; break;//沃玛勇士 
                    case 33: index = 1080; monPath = mon4; break;//沃玛战将 
                    case 31: index = 360; monPath = mon4; break;//火焰沃玛 
                    case 34: index = 1440; monPath = mon4; break;//沃玛教主 
                    case 36: index = 2160; monPath = mon4; break;//红蛇 
                    case 37: index = 2520; monPath = mon4; break;//变异骷髅 
                    case 38: index = 2880; monPath = mon4; break;//虎蛇 
                    case 39: index = 3240; monPath = mon4; break;//楔蛾
                    #endregion

                    #region Mon5 
                    case 40: index = 0; monPath = mon5; break;// 僵尸5 僵尸50 
                    case 41: index = 600; num = 40; monPath = mon5; break;// 角蝇
                    case 42: index = 720; monPath = mon5; break;// 蝙蝠
                    case 43: index = 1080; monPath = mon5; break;// 羊 
                    case 44: index = 1440; monPath = mon5; break;// 鹰 
                    case 45: index = 1800; monPath = mon5; break;// 盔甲虫 
                    case 46: index = 2160; monPath = mon5; break;// 大老鼠 
                    case 47: index = 2520; monPath = mon5; break;// 祖玛弓箭手
                    case 48: index = 2880; monPath = mon5; break;// 威思而小虫
                    case 49: index = 3240; monPath = mon5; break;// 沙虫
                    #endregion

                    #region Mon6 
                    case 50:
                    case 51: index = 0; num = 420; monPath = mon6; break;// 僵尸1 僵尸10 
                    case 52: index = 430; num = 420; monPath = mon6; break;// 僵尸2 僵尸20 
                    case 53: index = 860; num = 420; monPath = mon6; break;// 僵尸3 僵尸30 
                    case 54: index = 1290; num = 420; monPath = mon6; break; // 僵尸4 僵尸40
                    #endregion

                    #region Mon8
                    case 70: index = 0; monPath = mon8; break;//狼
                    case 71: index = 360; monPath = mon8; break;//弓箭手 鹰卫
                    case 72: index = 720; monPath = mon8; break;//虎卫
                    case 73: index = 1080; monPath = mon8; break;//蜈蚣
                    case 74: index = 1440; monPath = mon8; break;//黑暗恶趣
                    #endregion

                    #region Mon9
                    case 80: index = 0; monPath = mon9; break;//山洞蝙蝠 
                    case 81: index = 360; monPath = mon9; break;// 跳跳蜂 
                    case 82: index = 720; monPath = mon9; break;// 蠕虫 
                    case 83: index = 1080; num = 420; monPath = mon9; break;// 蝎子
                    #endregion

                    #region Mon10
                    case 90: index = 0; monPath = mon10; break;//多角虫
                    case 91: index = 360; monPath = mon10; break;//巨型多角虫
                    case 92: index = 720; monPath = mon10; break;//双头血魔
                    case 93: index = 1080; monPath = mon10; break;//双头金刚
                    #endregion

                    #region Mon11 
                    case 100: index = 0; monPath = mon11; break;//半兽人 
                    case 101: index = 360; monPath = mon11; break;//半兽战士 
                    case 102: index = 720; monPath = mon11; break;//半兽勇士 
                    case 103: index = 1080; monPath = mon11; break;//虹膜猪卫
                    #endregion

                    #region Mon12
                    case 110: index = 0; monPath = mon12; break;//红野猪
                    case 111: index = 360; monPath = mon12; break;//黑野猪
                    case 112: index = 720; monPath = mon12; break;//白野猪
                    case 113: index = 1080; monPath = mon12; break;//月魔蜘蛛
                    case 114: index = 1440; monPath = mon12; break;//钢牙蜘蛛
                    case 115: index = 1800; monPath = mon12; break;//血僵尸
                    case 116: index = 2160; monPath = mon12; break;//邪恶巨人
                    case 117: index = 2520; monPath = mon12; break;//花吻蜘蛛
                    case 118: index = 2880; monPath = mon12; break;//黑锷蜘蛛
                    case 119: index = 3240; monPath = mon12; break;//天狼蜘蛛
                    #endregion

                    #region Mon13
                    case 120: index = 0; monPath = mon13; break;//钳虫
                    case 121: index = 360; monPath = mon13; break;//邪恶钳虫
                    case 122: index = 720; monPath = mon13; break;//封魔教主
                    #endregion

                    #region Mon14
                    case 130: index = 0; monPath = mon14; break;//蝎蛇
                    case 131: index = 360; num = 56; monPath = mon14; break;//赤月恶魔
                    #endregion

                    #region Mon15 
                    case 140: index = 0; monPath = mon15; break;//触龙神
                    case 141: index = 360; monPath = mon15; break;//千年树妖
                    case 142: index = 720; monPath = mon15; break;//骨魔僵尸
                    case 143: index = 1080; monPath = mon15; break;//骨魔尸王
                    case 144: index = 1440; monPath = mon15; break;//骨魔锤兵
                    case 145: index = 1800; monPath = mon15; break;//骨魔XX兵
                    case 146: index = 2160; monPath = mon15; break;//骨魔刀兵
                    case 147: index = 2520; monPath = mon15; break;//骨魔弓箭手
                    case 148: index = 2880; monPath = mon15; break;//黄泉教主
                    #endregion

                    #region Mon16
                    case 150: index = 0; monPath = mon16; break;//骷髅精灵 
                    case 151: index = 360; monPath = mon16; break;//沃玛卫士 
                    case 152: index = 720; monPath = mon16; break;//尸王 
                    case 153: index = 1080; monPath = mon16; break;//虹魔蝎卫
                    #endregion

                    #region Mon17
                    case 160: index = 0; monPath = mon17; break;//鸡 
                    case 161: index = 360; monPath = mon17; break;//鹿
                    case 162: index = 720; monPath = mon17; break;//蛤蟆
                    case 163: index = 1080; monPath = mon17; break;//毒蜘蛛 
                    case 164: index = 1440; monPath = mon17; break;//邪恶毒蛇 
                    case 165: index = 1800; monPath = mon17; break;//牛魔王 
                    case 166: index = 2160; num = 1; monPath = mon17; break;//宝箱
                    #endregion

                    #region Mon21
                    case 201: num = 340; index = 360; monPath = mon21; break;//牛魔战士
                    case 202: num = 340; index = 720; monPath = mon21; break;//牛魔斗士
                    case 203: num = 340; index = 1080; monPath = mon21; break;//牛魔侍卫
                    case 204: num = 340; index = 1440; monPath = mon21; break;//牛魔将军
                    case 205: num = 340; index = 1800; monPath = mon21; break;//牛魔法师
                    case 206: num = 340; index = 2160; monPath = mon21; break;//牛魔祭司
                    #endregion

                    #region Mon22
                    case 210: index = 0; monPath = mon22; break;//大帝近卫
                    case 211: index = 470; monPath = mon22; break;//大帝力士
                    case 212: index = 820; monPath = mon22; break;//大帝战将
                    case 218: index = 2710; monPath = mon22; break;//装备全爆 魔龙教主
                    #endregion

                    #region Mon23
                    case 222: index = 430; monPath = mon23; break;//虚空
                    #endregion

                    #region Mon24
                    case 230: num = 340; index = 0; monPath = mon24; break;//无尽巨镰
                    case 231: num = 340; index = 340; monPath = mon24; break;//无尽金杖
                    case 232: index = 680; monPath = mon24; break;//无尽绿魔
                    case 233: num = 340; index = 1180; monPath = mon24; break;//无尽金蛟
                    case 234: num = 340; index = 1770; monPath = mon24; break;//无尽油白
                    case 235: num = 340; index = 2610; monPath = mon24; break;//无尽铁蛾
                    case 236: num = 340; index = 2950; monPath = mon24; break;//无尽黑牙
                    case 237: num = 340; index = 4810; monPath = mon24; break;//无尽
                    case 238: num = 340; index = 3750; monPath = mon24; break;//无尽蓝刺
                    case 239: num = 340; index = 2950; monPath = mon24; break;//无尽蜘蛛精

                    case 243: num = 340; index = 4460; monPath = mon24; break;//骷髅教皇
                    #endregion

                    #region Mon25
                    case 244: num = 340; index = 1090; monPath = mon25; break;//缥缈大狮兽
                    #endregion

                    #region Mon26
                    case 250: num = 340; index = 0; monPath = mon26; break;//冰宫战将
                    case 251: num = 340; index = 510; monPath = mon26; break;//冰宫侍卫
                    case 252: num = 340; index = 1020; monPath = mon26; break;//冰宫五毒魔
                    case 253: num = 340; index = 1370; monPath = mon26; break;//冰宫灭天魔
                    case 254: num = 340; index = 1720; monPath = mon26; break;//冰宫寒冰魔
                    case 255: num = 340; index = 2070; monPath = mon26; break;//冰宫天将

                    case 642:
                    case 256: num = 340; index = 2740; monPath = mon26; break;//冰宫*大魔王
                    #endregion

                    #region Mon27
                    case 262: num = 340; index = 680; monPath = mon27; break;//冰宫力士 
                    case 263: num = 340; index = 1190; monPath = mon27; break;//冰宫天兵 
                    case 266: num = 340; index = 2540; monPath = mon27; break;//冰宫·小旋风 
                    case 267: num = 340; index = 3570; monPath = mon27; break;//冰宫·小霸王
                    #endregion

                    #region Mon29
                    case 280: index = 0; monPath = mon29; break;//玄武
                    case 281: index = 600; monPath = mon29; break;//酒老人
                    #endregion

                    #region Mon32
                    case 310: num = 340; index = 170; monPath = mon32; break;//沙漠·屠猎兽
                    case 329: index = 6680; monPath = mon32; break;//荒原·大魔王
                    #endregion

                    #region Mon33
                    case 320: num = 340; index = 0; monPath = mon33; break;//魔灵黑狐
                    case 321: num = 340; index = 440; monPath = mon33; break;//魔灵赤狐
                    case 322: num = 340; index = 820; monPath = mon33; break;//魔灵素狐
                    case 327: num = 340; index = 2900; monPath = mon33; break;//魔灵天珠
                    #endregion

                    #region Mon34
                    case 171: num = 340; index = 2540; monPath = mon34; break;//魔灵猫妖
                    #endregion

                    #region Mon35
                    case 342: num = 340; index = 680; monPath = mon35; break;//龙王
                    #endregion

                    #region Mon36
                    case 616: num = 400; index = 13800; monPath = mon36; break;//造化钟神秀
                    case 628: num = 400; index = 24880; monPath = mon36; break;//白幽灵
                    #endregion

                    #region Mon37
                    case 366: index = 1840; monPath = mon37; break;//幽暗·蛤蟆精
                    #endregion

                    #region Mon38
                    case 643: num = 420; index = 6680; monPath = mon38; break;//来自地狱的恶魔
                    #endregion

                    #region Mon39
                    case 953: num = 340; index = 0; monPath = mon39; break;//灭世·天帝1
                    #endregion

                    #region Mon-kulou
                    case 950: index = 0; monPath = Mon_kulou; break;//亡灵骷髅
                    case 951: index = 360; monPath = Mon_kulou; break;//黄泉骷髅
                    case 952: index = 720; monPath = Mon_kulou; break;//地狱骷髅
                    #endregion

                    default:
                        break;
                }
                string placementsPath = "", path = "";
                for (int i = index; i < index + num; i++)
                {
                    placementsPath = monPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt";
                    monPos = FileHelper.ReadTxtReturnList(placementsPath);
                    path = monPath + i.ToString().PadLeft(6, '0') + ".png";
                    if (File.Exists(path))
                    {
                        list.Add($"{monPath + i.ToString().PadLeft(6, '0')}.png,{string.Join(",", monPos)}");
                    }
                }
                switch (monID)
                {
                    case 15:
                    case 192:
                    case 311:
                        for (int i = 0; i < 80; i++)
                        {
                            list.RemoveAt(240);
                        }
                        break;
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 打怪方法
        /// </summary>
        public static UserInfo GetPlayerAttackMonster(UserInfo user, string mon, int killCount, int reCount, List<MonBoss> monBossList)
        {
            string errorMonName = "";
            try
            {
                int qty = 0;

                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                var userSkillList = userInI.GetAllKeys("个人技能");

                // 取人物所在地图                 
                var mapInfoList = LegendApi.GetMapInfoList();
                var mapInfo = mapInfoList.Where(mp => mp.LMI_MapCode == user.MapCode).FirstOrDefault();

                // 取地图中的怪物                
                var legMapMons = LegendApi.GetLegMapMonInfoList();
                string path = Path.GetDirectoryName(user.UserImagePath);
                var files = Directory.GetFiles(path);
                path = files[rnd.Next(0, files.Length)];

                // 取数据库中的怪物
                List<MonsterDB> listMon = LegendApi.GetMonsterList();

                // 取所有技能
                List<MagicDB> magicList = LegendApi.GetMagicList();

                // 取所有怪物爆率文件
                var listMonQuest = LegendApi.GetMonQuestList();

                // 取所有物品数据
                List<StditemDB> stdItemList = LegendApi.GetStdItemsList();

                // 取回收的垃圾装备
                string stdPath = LegendFilePath.MonQuestPath + "垃圾装备.txt";
                var ljqeulist = FileHelper.ReadTxtReturnList(stdPath);

                user.KillMons = new List<MonsterDB>();
                MonsterDB monster = null;
                while (qty < killCount)
                {
                    if (user.User_General.Lv < 7) // 小于7级随机找新手村一张地图刷怪
                    {
                        switch (user.Map)
                        {
                            case "银杏山谷": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\银杏山谷\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                            case "边界村": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\边界村\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                            case "比奇城": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\比奇城\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                            case "比奇城外": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\城外\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                            default: user.UserImagePath = path; break;
                        }
                    }
                    else
                    {
                        user.UserImagePath = path;
                    }

                    LegMapMonInfo legMapMon = null;
                    MonBoss mBoss = null;
                    string mapPath = "";
                    if (mon == "专属BOSS")
                    {
                        switch (user.User_General.VipLv)
                        {
                            case 1: mapPath = LegendFilePath.Mud2Path + "会员专属BOSS1.txt"; break;
                            case 2: mapPath = LegendFilePath.Mud2Path + "会员专属BOSS2.txt"; break;
                            case 3: mapPath = LegendFilePath.Mud2Path + "会员专属BOSS3.txt"; break;
                            case 4: mapPath = LegendFilePath.Mud2Path + "会员专属BOSS4.txt"; break;
                            case 5: mapPath = LegendFilePath.Mud2Path + "会员专属BOSS5.txt"; break;
                            default: break;
                        }
                        var bossList = FileHelper.ReadTxtReturnList(mapPath);
                        var bossName = bossList[rnd.Next(0, bossList.Count)];
                        monster = listMon.Where(m => m.MonName == bossName).First();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mon))
                        {
                            legMapMons = legMapMons.Where(t => t.LMI_ID == mapInfo.LMI_ID).ToList();
                            if (legMapMons.Count <= 0) throw new Exception("噢,很抱歉,当前地图没有这个怪物,请输入 查看附近怪物 后重新再试.");
                        }
                        else
                        {
                            legMapMons = legMapMons.Where(t => t.LMI_ID == mapInfo.LMI_ID && t.MonName.Contains(mon)).ToList();
                            if (legMapMons.Count <= 0) throw new Exception("噢,很抱歉,当前地图没有这个怪物,请输入 查看附近怪物 后重新再试.");
                        }
                        int n = rnd.Next(0, legMapMons.Count);
                        legMapMon = legMapMons[n];
                        mBoss = monBossList.Where(mb => mb.MonsterName == legMapMon.MonName).FirstOrDefault();
                        monster = listMon.Where(ms => ms.MonID == legMapMon.MonID).First();
                    }

                    errorMonName = monster.MonName;

                    // 生成战斗日志记录,根据记录生成动画
                    int userDC = 0, monAC = 0, monDC = 0, nb = 1;
                    user.UserAttackRecordList = new List<UserAttackRecord>();

                    int stDc = 0, endDc = 0, skey = 0, magicQty = 0, hp = 0;
                    while (monster.HP > 0 && user.User_Attribute.Hp > 0)
                    {
                        UserAttackRecord userAttackRecord = new UserAttackRecord();
                        stDc = user.User_Attribute.Dc;
                        endDc = user.User_Attribute.DcMax + 1;
                        if (user.IsUseSkills == "是")
                        {
                            MagicDB magic = new MagicDB();
                            skey = rnd.Next(0, user.User_Skill.Count);
                            var usK = user.User_Skill[skey];
                            switch (user.Job)
                            {
                                case "战士":
                                    {
                                        if (usK.US_IsEnableSkill == "是")
                                        {
                                            magic = magicList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                            userAttackRecord.MagicDBModel1 = new MagicDB();
                                            userAttackRecord.MagicDBModel1 = magic;
                                            stDc = user.User_Attribute.Dc * magic.Power;
                                            endDc = user.User_Attribute.DcMax * magic.MaxPower;
                                        }
                                    }
                                    break;
                                case "法师":
                                    {
                                        if (usK.US_IsEnableSkill == "是")
                                        {
                                            magic = magicList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                            userAttackRecord.MagicDBModel1 = new MagicDB();
                                            userAttackRecord.MagicDBModel1 = magic;
                                            stDc = user.User_Attribute.Mc * magic.Power;
                                            endDc = user.User_Attribute.McMax * magic.MaxPower;
                                        }
                                    }
                                    break;
                                case "道士":
                                    {
                                        if (user.User_General.Lv >= 18)
                                        {
                                            if (usK.US_IsEnableSkill == "是")
                                            {
                                                magic = magicList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                                userAttackRecord.MagicDBModel1 = new MagicDB();
                                                userAttackRecord.MagicDBModel1 = magic;
                                                stDc = user.User_Attribute.Sc * magic.Power;
                                                endDc = user.User_Attribute.ScMax * magic.MaxPower;
                                            }
                                        }
                                        else
                                        {
                                            if (usK.US_IsEnableSkill == "是")
                                            {
                                                magicQty = rnd.Next(11);
                                                if (magicQty > 0 && magicQty < 5)
                                                {
                                                    magic = magicList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                                    userAttackRecord.MagicDBModel1 = new MagicDB();
                                                    userAttackRecord.MagicDBModel1 = magic;
                                                    switch (magic.MagName)
                                                    {
                                                        case "治愈术":
                                                            stDc = 0;
                                                            endDc = 0;
                                                            hp = rnd.Next(magic.Power, magic.MaxPower);
                                                            user.User_Attribute.Hp += hp;
                                                            if (user.User_Attribute.Hp > user.User_Attribute.MaxHp) user.User_Attribute.Hp = user.User_Attribute.MaxHp;//  如果治愈术加的hp大于最大血量则等于当前等级的最大血量
                                                            break;
                                                        default:
                                                            stDc = user.User_Attribute.Sc * magic.Power;
                                                            endDc = user.User_Attribute.ScMax * magic.MaxPower;
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        userDC = rnd.Next(stDc, endDc);
                        monDC = rnd.Next(0, monster.Dc + 1);
                        if (monster.Hit <= 10)
                        {
                            monAC = rnd.Next(0, 10);
                        }
                        else if (monster.Hit > 10 && monster.Hit <= 30)
                        {
                            monAC = rnd.Next(10, 30);
                        }
                        else if (monster.Hit > 30 && monster.Hit <= 50)
                        {
                            monAC = rnd.Next(10, 50);
                        }
                        else if (monster.Hit > 50 && monster.Hit <= 100)
                        {
                            monAC = rnd.Next(50, 100);
                        }
                        else
                        {
                            monAC = rnd.Next(100, monster.Hit);
                        }

                        if (monster.Hit >= monAC)
                        {
                            userDC -= monster.AC;
                        }

                        magicQty = rnd.Next(11);

                        if (magicQty > 0 && magicQty < 4)
                        {
                            if (monster.Dc2 > monster.Dc)
                            {
                                int dc2 = monster.Dc2 - user.User_Attribute.Agile;
                                if (dc2 < monster.Dc)
                                    dc2 = monster.Dc;
                                monDC = rnd.Next(monster.Dc, dc2) / 2 + rnd.Next(user.User_Attribute.Ac, user.User_Attribute.Ac2);
                            }
                            else
                                monDC = (monster.Dc2 - user.User_Attribute.Agile) / 2 + rnd.Next(user.User_Attribute.Ac, user.User_Attribute.Ac2);
                        }
                        else
                        {
                            if (monster.Dc > monster.Dc2)
                                monDC = rnd.Next(monster.Dc2, monster.Dc);
                            else
                                monDC = rnd.Next(monster.Dc, monster.Dc2);
                        }


                        if (user.User_General.Lv <= 8 || mon == "专属BOSS")// 用户小于8级不掉血
                        {
                            monDC = 0;
                            if (rnd.Next(2) > 0)
                            {
                                monDC = -1;
                            }
                        }
                        if (monDC == 0)
                        {
                            monDC = 0;
                            userAttackRecord.p_msg2 = "丢失";
                        }
                        else if (monDC < 0)
                        {
                            monDC = -1;
                            userAttackRecord.p_msg2 = "闪避";
                        }
                        else
                        {
                            user.User_Attribute.Hp -= monDC;

                            if (user.UserSetAutoMedicationQTYType == "普通吃药")
                            {
                                if (user.User_Attribute.Hp < user.UserSetAutoMedicationQTY)// 如果被怪物攻击后血小于保护血量则进行吃药
                                {
                                    user.User_Attribute.Hp = UserUseMedication(userInI, user.User_Attribute.Hp, user.UserSetAutoMedicationQTY);
                                }
                            }
                            if (user.UserSetAutoMedicationQTYType == "百分比吃药")
                            {
                                long pHP = user.User_Attribute.MaxHp * user.UserSetAutoMedicationQTY / 100;
                                if (user.User_Attribute.Hp < pHP)// 如果被怪物攻击后血小于保护血量则进行吃药
                                {
                                    user.User_Attribute.Hp = UserUseMedication(userInI, user.User_Attribute.Hp, pHP);
                                }
                            }
                        }
                        userAttackRecord.p_hp1 = user.User_Attribute.Hp;
                        userAttackRecord.p_id1 = user.Id;
                        userAttackRecord.p_attack1 = userDC;
                        userAttackRecord.p_id2 = monster.MonID;
                        userAttackRecord.p_attack2 = monDC;

                        if (userDC == 0 && userAttackRecord.MagicDBModel1 != null)
                        {
                            userAttackRecord.p_attack1 = 0;
                            userAttackRecord.p_msg1 = "丢失";

                        }
                        else if (userDC < 0 && userAttackRecord.MagicDBModel1 != null)
                        {
                            userAttackRecord.p_attack1 = -1;
                            userAttackRecord.p_msg1 = "攻击无效";
                        }
                        if (userDC <= 0)
                        {
                            if (nb <= 5)// 只保存5次攻击=0的数据,太多就会很卡
                            {
                                monster.HP -= userDC;
                                userAttackRecord.p_hp2 = monster.HP;
                                user.UserAttackRecordList.Add(userAttackRecord);
                            }
                            nb++;
                        }
                        else
                        {
                            monster.HP -= userDC;
                            userAttackRecord.p_hp2 = monster.HP;
                            user.UserAttackRecordList.Add(userAttackRecord);
                        }
                    }

                    // 打怪增加技能熟练度
                    user.User_Skill.ForEach(usKey =>
                    {
                        magicQty = user.UserAttackRecordList.Where(uur => uur.MagicDBModel1?.MagID == usKey.US_MagID).Count();
                        if (magicQty > 0)
                        {
                            MagicDB magic = magicList.Where(mg => mg.MagID == usKey.US_MagID).FirstOrDefault();
                            switch (usKey.US_Level)
                            {
                                case 0:
                                    usKey.US_NeedLN1 += 1;
                                    if (usKey.US_NeedLN1 >= magic.NeedLN1 && user.User_General.Lv >= magic.NeedL1)
                                    {
                                        usKey.US_Level = 1;
                                        usKey.US_NeedLN1 = magic.NeedLN1;
                                    }
                                    break;
                                case 1:
                                    usKey.US_NeedLN2 += 1;
                                    if (usKey.US_NeedLN2 >= magic.NeedLN2 && user.User_General.Lv >= magic.NeedL2)
                                    {
                                        usKey.US_Level = 2;
                                        usKey.US_NeedLN2 = magic.NeedLN2;
                                    }
                                    break;
                                case 2:
                                    usKey.US_NeedLN3 += 1;
                                    if (usKey.US_NeedLN3 >= magic.NeedLN3 && user.User_General.Lv >= magic.NeedL3)
                                    {
                                        usKey.US_Level = 3;
                                        usKey.US_NeedLN3 = magic.NeedLN3;
                                    }
                                    break;
                            }
                            userSkillList.ForEach(uk =>
                            {
                                var us = Api.JsonDeserialize<UserSkill>(userInI.Read("个人技能", uk));
                                if (usKey.US_IsEnableSkill == "是" && usKey.US_MagID == us.US_MagID)
                                {
                                    userInI.Write("个人技能", uk, Api.JsonSerialize<UserSkill>(usKey));
                                }
                            });
                        }
                    });

                    int skip = Math.Max(user.UserAttackRecordList.Count - reCount, 0);
                    // 战斗记录只取最后10条
                    user.UserAttackRecordList = user.UserAttackRecordList.Skip(skip).Take(reCount).ToList();
                    #region 怪物爆装备
                    if (legMapMon != null) monster.HP = legMapMon.HP;

                    user.KillMon = new MonsterDB();
                    user.KillMon = monster;
                    user.KillMons.Add(monster);

                    if (user.User_Attribute.Hp > 0)
                    {
                        monster.MonItemList = new List<StditemDB>();
                        int.TryParse(userInI.Read("人物信息", "城中杀怪数量"), out int citkillNum);
                        if (user.IsOnCity == "是")
                        {
                            if (citkillNum < 10)
                            {
                                citkillNum++;
                                userInI.Write("人物信息", "城中杀怪数量", citkillNum.ToString());
                                switch (user.Map)
                                {
                                    case "比奇城":
                                        user.MapCode = "1_100";
                                        user.UserImagePath = $"地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                                        break;
                                    case "盟重土城":
                                        user.MapCode = "4";
                                        user.UserImagePath = $"地图\\4\\盟重土城\\4_{rnd.Next(1, 16)}.jpg";
                                        break;
                                }
                            }
                            else
                            {
                                user.IsOnCity = "否";
                                user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";

                                switch (user.Map)
                                {
                                    case "比奇城":
                                        user.Map = "比奇城外";
                                        user.MapCode = "1_0_3";
                                        user.UserImagePath = $"地图\\0\\城外\\1_0_3_{rnd.Next(1, 11)}.jpg";
                                        break;
                                    case "盟重土城":
                                        user.Map = "盟重土城";
                                        user.MapCode = "4";
                                        user.UserImagePath = $"地图\\4\\盟重土城\\4_{rnd.Next(1, 16)}.jpg";
                                        break;
                                }
                            }
                        }

                        int.TryParse(userInI.Read("人物信息", "杀怪数量"), out int killNum);
                        killNum++;
                        userInI.Write("人物信息", "杀怪数量", killNum.ToString());

                        var list = listMonQuest.Where(mq => mq.MQ_MonID == monster.MonID).ToList();
                        List<string> bblist = userInI.GetAllKeys("背包物品");
                        user.PageQty = 96 - bblist.Count;// 背包里有空格物品才进背包,否则提示背包格子不够

                        //计算爆率
                        foreach (var item in list)
                        {
                            for (int i = 0; i < item.MQ_MonQty; i++)
                            {
                                List<MagicAttr> MagicAttrlist = new List<MagicAttr>();
                                StditemDB stdItem = stdItemList.Where(std => std.StdID == item.MQ_StdID).First();
                                stdItem.StdItemStuff = monster.MonName;
                                stdItem.StdItemOwner = user.Name;
                                stdItem.StdItemMap = mapInfo.LMI_MapName;
                                stdItem.StdItemTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                if (item.MQ_IsBB)
                                {
                                    // 必爆装备
                                    monster.MonItemList.Add(stdItem);
                                    if (!UserAutoRecyclStditems(user, userInI, stdItemList, ljqeulist, stdItem))
                                    {
                                        stdItem = GetStditemDB(stdItem, MagicAttrlist, listMon, magicList, stdItemList);
                                        userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(stdItem));
                                    }
                                }
                                else
                                {
                                    //计算概率                                        
                                    if (item.MQ_MonExpRate == 1)// 1/1的爆率
                                    {
                                        monster.MonItemList.Add(stdItem);
                                        if (!UserAutoRecyclStditems(user, userInI, stdItemList, ljqeulist, stdItem))
                                        {
                                            stdItem = GetStditemDB(stdItem, MagicAttrlist, listMon, magicList, stdItemList);
                                            userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(stdItem));
                                        }
                                    }
                                    else
                                    {
                                        int monExpRate = (int)Math.Round(item.MQ_MonExpRate / ((user.User_Attribute.MonExpRate + 100.0) / 100)) + 1;
                                        int roll = rnd.Next(1, monExpRate);
                                        if (roll == 1)
                                        {
                                            monster.MonItemList.Add(stdItem);
                                            if (!UserAutoRecyclStditems(user, userInI, stdItemList, ljqeulist, stdItem))
                                            {
                                                stdItem = GetStditemDB(stdItem, MagicAttrlist, listMon, magicList, stdItemList);
                                                userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(stdItem));
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (mBoss != null)
                        {
                            MonBoss.SetMonBossDead(mBoss);
                        }

                        SetUserUpLevel(user, userInI);

                        if (mon == "专属BOSS")
                        {
                            user.User_VIPAttribute.AutoBoosTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            user.User_VIPAttribute.AutoPBoos += 1;
                            userInI.Write("VIP属性", "专属BOSS击杀时间", user.User_VIPAttribute.AutoBoosTime);
                            userInI.Write("VIP属性", "已击杀专属BOOS数量", user.User_VIPAttribute.AutoPBoos.ToString());
                        }
                    }
                    else
                    {
                        user.User_Attribute.Hp = user.User_Attribute.MaxHp;
                        MapApi.GoBackHome2(user);
                        qty = 10;
                    }

                    if (user.User_Attribute.Hp < 0) user.User_Attribute.Hp = 0;

                    #endregion
                    qty++;
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(errorMonName)) FileHelper.Log($"{user.Name}[{user.UserQQ}] 打 怪物 {errorMonName} 异常：" + ex.ToString());
                throw ex;
            }

            return user;
        }

        /// <summary>
        /// 用户升级
        /// </summary>
        public static void SetUserUpLevel(UserInfo user, IniFileHelper userINI, int ybRate = 0, int expRate = 0)
        {
            try
            {
                StditemDB stditemDB = null;
                if (ybRate > 0)
                {
                    user.User_General.CurtYB = ybRate;
                    user.User_General.YB += ybRate;
                }

                if (expRate > 0)
                {
                    user.User_General.Exp = expRate;
                    user.User_General.SumExp += expRate;
                }
                int exp = 0;
                if (user.KillMon != null)
                {
                    exp = user.KillMon.Exp + (int)(user.KillMon.Exp * (user.User_VIPAttribute.MonExpInc / 100.0));
                    user.User_General.SumExp += exp;
                }
                int lv = user.User_General.Lv;
                if (lv == 0) lv++;

                var list = userINI.GetAllKeys("背包物品");
                string jlz = "聚灵珠,超级聚灵珠,神级聚灵珠";
                foreach (var key in list)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(userINI.ReadValue("背包物品", key));
                    if (stditemDB != null)
                    {
                        if (jlz.Contains(stditemDB.StdName))// 聚灵珠存经验
                        {
                            stditemDB.SubAttr2 += exp;
                            if (stditemDB.SubAttr2 >= stditemDB.Price * stditemDB.Need)
                            {
                                user.User_General.SumExp += stditemDB.SubAttr2;
                                user.User_General.JB -= stditemDB.Price;
                                stditemDB.SubAttr1 -= 1;
                                stditemDB.SubAttr2 = 0;// 经验重置0
                            }
                            // 如果聚灵珠兑完经验没有了，则删除背包聚灵珠，如果还有叠加数量则重写入背包
                            if (stditemDB.SubAttr1 <= 0)
                                userINI.Delete("背包物品", key);
                            else
                                userINI.Write("背包物品", key, Api.JsonSerialize(stditemDB));
                            break;
                        }
                    }
                }

                string legenPath = "";
                switch (user.Job)
                {
                    case "战士": legenPath = LegendFilePath.UserAttrPath + "战士.txt"; break;
                    case "法师": legenPath = LegendFilePath.UserAttrPath + "法师.txt"; break;
                    case "道士": legenPath = LegendFilePath.UserAttrPath + "道士.txt"; break;
                    default: legenPath = LegendFilePath.UserAttrPath + "战士.txt"; break;
                }
                string legenStr = FileHelper.ReadTxt(legenPath);
                List<LengendBase> LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                LengendBase lengendBase = null;
                foreach (var l in LengendList)
                {
                    if (l.Level == lv)
                    {
                        lengendBase = l;
                        user.User_General.BgwMax = lengendBase.Bgw;
                        user.User_General.WearMax = lengendBase.Wear;
                        user.User_General.NextExp = lengendBase.LvExp;
                        user.User_General.Lv = lengendBase.Level;
                        if (user.User_General.SumExp > lengendBase.LvExp)
                        {
                            user.User_General.SumExp = user.User_General.SumExp - lengendBase.LvExp;
                            lv++;
                            user.User_General.Lv = lv;
                            lengendBase = LengendList.Where(ld => ld.Level == lv).FirstOrDefault();
                            user.User_General.NextExp = lengendBase.LvExp;
                            user.User_General.BgwMax = lengendBase.Bgw;
                            user.User_General.WearMax = lengendBase.Wear;

                            legenPath = LegendFilePath.UserSkillPath + user.Job + ".ini";
                            IniFileHelper ini_userSkill = new IniFileHelper(legenPath);
                            string userSkill = ini_userSkill.Read("配置信息", "技能" + lv);
                            if (!string.IsNullOrEmpty(userSkill))// 在当前等级学习当前技能,如果已存在则不需要学习
                            {
                                string sk = userINI.Read("个人技能", "技能" + lv);
                                if (string.IsNullOrEmpty(sk))
                                {
                                    int magId = int.Parse(userSkill.Split('_')[0]);
                                    string magName = userSkill.Split('_')[1];
                                    string magImg = userSkill.Split('_')[2];
                                    UserSkill us = new UserSkill
                                    {
                                        US_MagID = magId,
                                        US_Name = magName,
                                        US_Img = magImg,
                                        US_IsEnableSkill = "否",
                                    };
                                    userINI.Write("个人技能", "技能" + lv, Api.JsonSerialize<UserSkill>(us));
                                }
                            }

                            switch (user.Job)
                            {
                                case "战士":
                                    switch (lv)
                                    {
                                        case 7:
                                            user.IsOnCity = "否";
                                            user.Map = "比奇城外";
                                            user.MapCode = "1_0_3";
                                            user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                                            user.UserImagePath = $"地图\\0\\城外\\{user.MapCode}_{rnd.Next(1, 11)}.jpg";
                                            userINI.Write("人物信息", "是否发送等级消息7级", "是");
                                            break;
                                        case 19: userINI.Write("人物信息", "是否发送等级消息19级", "是"); break;
                                        case 25: userINI.Write("人物信息", "是否发送等级消息25级", "是"); break;
                                        case 28: userINI.Write("人物信息", "是否发送等级消息28级", "是"); break;
                                        case 30: userINI.Write("人物信息", "是否发送等级消息30级", "是"); break;
                                        case 35: userINI.Write("人物信息", "是否发送等级消息35级", "是"); break;
                                        default: break;
                                    }
                                    break;
                                case "法师":
                                    switch (lv)
                                    {
                                        case 7:
                                            user.IsOnCity = "否";
                                            user.Map = "比奇城外";
                                            user.MapCode = "1_0_3";
                                            user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                                            user.UserImagePath = $"地图\\0\\城外\\{user.MapCode}_{rnd.Next(1, 11)}.jpg";
                                            userINI.Write("人物信息", "是否发送等级消息7级", "是");
                                            break;
                                        case 12: userINI.Write("人物信息", "是否发送等级消息12级", "是"); break;
                                        case 13: userINI.Write("人物信息", "是否发送等级消息13级", "是"); break;
                                        case 16: userINI.Write("人物信息", "是否发送等级消息16级", "是"); break;
                                        case 17: userINI.Write("人物信息", "是否发送等级消息17级", "是"); break;
                                        case 19: userINI.Write("人物信息", "是否发送等级消息19级", "是"); break;
                                        case 20: userINI.Write("人物信息", "是否发送等级消息20级", "是"); break;
                                        case 22: userINI.Write("人物信息", "是否发送等级消息22级", "是"); break;
                                        case 24: userINI.Write("人物信息", "是否发送等级消息24级", "是"); break;
                                        case 26: userINI.Write("人物信息", "是否发送等级消息26级", "是"); break;
                                        case 30: userINI.Write("人物信息", "是否发送等级消息30级", "是"); break;
                                        case 31: userINI.Write("人物信息", "是否发送等级消息31级", "是"); break;
                                        case 32: userINI.Write("人物信息", "是否发送等级消息32级", "是"); break;
                                        case 35: userINI.Write("人物信息", "是否发送等级消息35级", "是"); break;
                                        default: break;
                                    }
                                    break;
                                case "道士":
                                    switch (lv)
                                    {
                                        case 7:
                                            user.IsOnCity = "否";
                                            user.Map = "比奇城外";
                                            user.MapCode = "1_0_3";
                                            user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                                            user.UserImagePath = $"地图\\0\\城外\\{user.MapCode}_{rnd.Next(1, 11)}.jpg";
                                            userINI.Write("人物信息", "是否发送等级消息7级", "是");
                                            break;
                                        case 9: userINI.Write("人物信息", "是否发送等级消息9级", "是"); break;
                                        case 14: userINI.Write("人物信息", "是否发送等级消息14级", "是"); break;
                                        case 18: userINI.Write("人物信息", "是否发送等级消息18级", "是"); break;
                                        case 19: userINI.Write("人物信息", "是否发送等级消息19级", "是"); break;
                                        case 20: userINI.Write("人物信息", "是否发送等级消息20级", "是"); break;
                                        case 21: userINI.Write("人物信息", "是否发送等级消息21级", "是"); break;
                                        case 22: userINI.Write("人物信息", "是否发送等级消息22级", "是"); break;
                                        case 25: userINI.Write("人物信息", "是否发送等级消息25级", "是"); break;
                                        case 26: userINI.Write("人物信息", "是否发送等级消息26级", "是"); break;
                                        case 28: userINI.Write("人物信息", "是否发送等级消息28级", "是"); break;
                                        case 33: userINI.Write("人物信息", "是否发送等级消息33级", "是"); break;
                                        case 35: userINI.Write("人物信息", "是否发送等级消息35级", "是"); break;
                                        default: break;
                                    }
                                    break;
                            }
                        }

                        if (string.IsNullOrEmpty(user.IsUseSkills)) user.IsUseSkills = "否";

                        //user.User_Attribute.Hp = lengendBase.Hp + user.User_VIPAttribute.Hp + user.User_YHZLAttribute.Hp;
                        //user.User_Attribute.Mp = lengendBase.Mp + user.User_VIPAttribute.Mp;
                        //user.User_Attribute.MaxHp = lengendBase.Hp + user.User_VIPAttribute.Hp + user.User_YHZLAttribute.Hp;
                        //user.User_Attribute.MaxMp = lengendBase.Mp + user.User_VIPAttribute.Mp;
                        //user.User_Attribute.Ac = lengendBase.Ac + user.User_VIPAttribute.Ac;
                        //user.User_Attribute.Ac2 = lengendBase.Ac2 + user.User_VIPAttribute.Ac2;
                        //user.User_Attribute.Mac = lengendBase.Mac + user.User_VIPAttribute.Mac;
                        //user.User_Attribute.Mac2 = lengendBase.Mac2 + user.User_VIPAttribute.Mac2;
                        //user.User_Attribute.Dc = lengendBase.Dc + user.User_VIPAttribute.Dc + user.User_YHZLAttribute.Dc;
                        //user.User_Attribute.DcMax = lengendBase.DcMax + user.User_VIPAttribute.DcMax + user.User_YHZLAttribute.DcMax;
                        //user.User_Attribute.Mc = lengendBase.Mc + user.User_VIPAttribute.Mc + user.User_YHZLAttribute.Mc;
                        //user.User_Attribute.McMax = lengendBase.McMax + user.User_VIPAttribute.McMax + user.User_YHZLAttribute.McMax;
                        //user.User_Attribute.Sc = lengendBase.Sc + user.User_VIPAttribute.Sc + user.User_YHZLAttribute.Sc;
                        //user.User_Attribute.ScMax = lengendBase.ScMax + user.User_VIPAttribute.ScMax + user.User_YHZLAttribute.ScMax;
                        //user.User_Attribute.WristMax = lengendBase.Wrist;
                        //user.User_General.WearMax = lengendBase.Wear;

                        user.User_Attribute.Hp = lengendBase.Hp;
                        user.User_Attribute.Mp = lengendBase.Mp;
                        user.User_Attribute.MaxHp = lengendBase.Hp;
                        user.User_Attribute.MaxMp = lengendBase.Mp;
                        user.User_Attribute.Ac = lengendBase.Ac;
                        user.User_Attribute.Ac2 = lengendBase.Ac2;
                        user.User_Attribute.Mac = lengendBase.Mac;
                        user.User_Attribute.Mac2 = lengendBase.Mac2;
                        user.User_Attribute.Dc = lengendBase.Dc;
                        user.User_Attribute.DcMax = lengendBase.DcMax;
                        user.User_Attribute.Mc = lengendBase.Mc;
                        user.User_Attribute.McMax = lengendBase.McMax;
                        user.User_Attribute.Sc = lengendBase.Sc;
                        user.User_Attribute.ScMax = lengendBase.ScMax;
                        user.User_Attribute.WristMax = lengendBase.Wrist;
                        break;
                    }
                }

                user.User_General.Bgw = 0;
                foreach (var key in list)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(userINI.ReadValue("背包物品", key));
                    if (stditemDB != null)
                    {
                        user.User_General.Bgw += stditemDB.Weight;
                    }
                }

                user.User_Attribute.Acc = 0;
                user.User_Attribute.Agile = 0;
                user.User_Attribute.Lucky = 0;
                list = userINI.GetAllKeys("装备物品");
                list.ForEach(zb =>
                {
                    string key = userINI.Read("装备物品", zb);
                    if (!string.IsNullOrEmpty(key) && key != "0")
                    {
                        stditemDB = Api.JsonDeserialize<StditemDB>(userINI.ReadValue("装备属性", key));

                        // 生成人物属性数据
                        user.User_Attribute.Hp += stditemDB.StdHP;
                        user.User_Attribute.Mp += stditemDB.StdMP;
                        user.User_Attribute.MaxHp += stditemDB.StdHP;
                        user.User_Attribute.MaxMp += stditemDB.StdMP;
                        user.User_Attribute.Ac += stditemDB.StdAC;
                        user.User_Attribute.Ac2 += stditemDB.StdAC2 + stditemDB.SubAttr1;
                        user.User_Attribute.Mac += stditemDB.StdMac;
                        user.User_Attribute.Mac2 += stditemDB.StdMac2 + stditemDB.SubAttr2;
                        user.User_Attribute.Dc += stditemDB.StdDc;
                        user.User_Attribute.DcMax += stditemDB.StdDc2 + stditemDB.SubAttr3;
                        user.User_Attribute.Mc += stditemDB.StdMc;
                        user.User_Attribute.McMax += stditemDB.StdMc2 + stditemDB.SubAttr4;
                        user.User_Attribute.Sc += stditemDB.StdSc;
                        user.User_Attribute.ScMax += stditemDB.StdSc2 + stditemDB.SubAttr5;
                        user.User_Attribute.Acc += stditemDB.Accuracy + stditemDB.SubAttr6;
                        user.User_Attribute.Agile += stditemDB.Agile + stditemDB.SubAttr7;
                        user.User_Attribute.Lucky += stditemDB.Lucky + stditemDB.SubAttr8;
                    }
                });

                userINI.Write("人物信息", "是否在城里", user.IsOnCity);
                userINI.Write("人物信息", "所在地图", user.Map);
                userINI.Write("人物信息", "所在地图编号", user.MapCode);
                userINI.Write("人物信息", "坐标", user.Pos);
                if (!string.IsNullOrEmpty(user.UserImagePath))
                    userINI.Write("人物信息", "个人图片", user.UserImagePath.Replace(LegendFilePath.LegendIMGDataPath, ""));
                if (string.IsNullOrEmpty(user.IsSendMsgImg))
                {
                    user.IsSendMsgImg = "否";
                    userINI.Write("人物信息", "开启发送动图", user.IsSendMsgImg);
                }
                switch (user.Job)
                {
                    case "战士":
                        if (lv < 7)
                        {
                            userINI.Write("人物信息", "是否发送等级消息7级", "否");
                        }
                        else if (lv >= 7 && lv < 19)
                        {
                            userINI.Write("人物信息", "是否发送等级消息19级", "否");
                        }
                        else if (lv >= 19 && lv < 25)
                        {
                            userINI.Write("人物信息", "是否发送等级消息25级", "否");
                        }
                        else if (lv >= 25 && lv < 28)
                        {
                            userINI.Write("人物信息", "是否发送等级消息28级", "否");
                        }
                        else if (lv >= 28 && lv < 30)
                        {
                            userINI.Write("人物信息", "是否发送等级消息30级", "否");
                        }
                        else if (lv >= 30 && lv < 35)
                        {
                            userINI.Write("人物信息", "是否发送等级消息35级", "否");
                        }
                        else
                        {

                        }
                        break;
                    case "法师":
                        if (lv < 7)
                        {
                            userINI.Write("人物信息", "是否发送等级消息7级", "否");
                        }
                        else if (lv >= 7 && lv < 12)
                        {
                            userINI.Write("人物信息", "是否发送等级消息12级", "否");
                        }
                        else if (lv >= 12 && lv < 13)
                        {
                            userINI.Write("人物信息", "是否发送等级消息13级", "否");
                        }
                        else if (lv >= 13 && lv < 16)
                        {
                            userINI.Write("人物信息", "是否发送等级消息16级", "否");
                        }
                        else if (lv >= 16 && lv < 17)
                        {
                            userINI.Write("人物信息", "是否发送等级消息17级", "否");
                        }
                        else if (lv >= 17 && lv < 19)
                        {
                            userINI.Write("人物信息", "是否发送等级消息19级", "否");
                        }
                        else if (lv >= 19 && lv < 20)
                        {
                            userINI.Write("人物信息", "是否发送等级消息20级", "否");
                        }
                        else if (lv >= 20 && lv < 22)
                        {
                            userINI.Write("人物信息", "是否发送等级消息22级", "否");
                        }
                        else if (lv >= 22 && lv < 24)
                        {
                            userINI.Write("人物信息", "是否发送等级消息24级", "否");
                        }
                        else if (lv >= 24 && lv < 26)
                        {
                            userINI.Write("人物信息", "是否发送等级消息26级", "否");
                        }
                        else if (lv >= 26 && lv < 30)
                        {
                            userINI.Write("人物信息", "是否发送等级消息30级", "否");
                        }
                        else if (lv >= 30 && lv < 31)
                        {
                            userINI.Write("人物信息", "是否发送等级消息31级", "否");
                        }
                        else if (lv >= 31 && lv < 32)
                        {
                            userINI.Write("人物信息", "是否发送等级消息32级", "否");
                        }
                        else if (lv >= 32 && lv < 35)
                        {
                            userINI.Write("人物信息", "是否发送等级消息35级", "否");
                        }
                        else
                        {

                        }

                        break;
                    case "道士":
                        if (lv < 7)
                        {
                            userINI.Write("人物信息", "是否发送等级消息7级", "否");
                        }
                        else if (lv >= 7 && lv < 9)
                        {
                            userINI.Write("人物信息", "是否发送等级消息9级", "否");
                        }
                        else if (lv >= 9 && lv < 14)
                        {
                            userINI.Write("人物信息", "是否发送等级消息14级", "否");
                        }
                        else if (lv >= 14 && lv < 18)
                        {
                            userINI.Write("人物信息", "是否发送等级消息18级", "否");
                        }
                        else if (lv >= 18 && lv < 19)
                        {
                            userINI.Write("人物信息", "是否发送等级消息19级", "否");
                        }
                        else if (lv >= 19 && lv < 20)
                        {
                            userINI.Write("人物信息", "是否发送等级消息20级", "否");
                        }
                        else if (lv >= 20 && lv < 21)
                        {
                            userINI.Write("人物信息", "是否发送等级消息21级", "否");
                        }
                        else if (lv >= 21 && lv < 22)
                        {
                            userINI.Write("人物信息", "是否发送等级消息22级", "否");
                        }
                        else if (lv >= 22 && lv < 25)
                        {
                            userINI.Write("人物信息", "是否发送等级消息25级", "否");
                        }
                        else if (lv >= 25 && lv < 26)
                        {
                            userINI.Write("人物信息", "是否发送等级消息26级", "否");
                        }
                        else if (lv >= 26 && lv < 28)
                        {
                            userINI.Write("人物信息", "是否发送等级消息28级", "否");
                        }
                        else if (lv >= 28 && lv < 33)
                        {
                            userINI.Write("人物信息", "是否发送等级消息33级", "否");
                        }
                        else if (lv >= 33 && lv < 35)
                        {
                            userINI.Write("人物信息", "是否发送等级消息35级", "否");
                        }
                        else
                        {

                        }
                        break;
                }

                userINI.Write("人物信息", "是否使用技能攻击", user.IsUseSkills);

                userINI.Write("普通数据", "签到元宝数", user.User_General.CurtYB.ToString());
                userINI.Write("普通数据", "元宝", user.User_General.YB.ToString());
                userINI.Write("普通数据", "金币", user.User_General.JB.ToString());
                userINI.Write("普通数据", "当前经验", user.User_General.Exp.ToString());
                userINI.Write("普通数据", "下一级经验", user.User_General.NextExp.ToString());
                userINI.Write("普通数据", "总经验", user.User_General.SumExp.ToString());
                userINI.Write("普通数据", "等级", user.User_General.Lv.ToString());
                userINI.Write("普通数据", "背包重量", user.User_General.Bgw.ToString());
                userINI.Write("普通数据", "最大背包重量", user.User_General.BgwMax.ToString());
                userINI.Write("普通数据", "最大穿戴重量", user.User_General.WearMax.ToString());
                if (user.User_General.ActivityQty < 0) user.User_General.ActivityQty = 0;
                userINI.Write("普通数据", "活跃值", user.User_General.ActivityQty.ToString());
                userINI.Write("普通数据", "活跃时间", user.User_General.ActivityTime.ToString());

                userINI.Write("属性点", "Hp", user.User_Attribute.Hp.ToString());
                userINI.Write("属性点", "Mp", user.User_Attribute.Mp.ToString());
                userINI.Write("属性点", "MaxHp", user.User_Attribute.MaxHp.ToString());
                userINI.Write("属性点", "MaxMp", user.User_Attribute.MaxMp.ToString());
                userINI.Write("属性点", "防御下限", user.User_Attribute.Ac.ToString());
                userINI.Write("属性点", "防御上限", user.User_Attribute.Ac2.ToString());
                userINI.Write("属性点", "魔法防御下限", user.User_Attribute.Mac.ToString());
                userINI.Write("属性点", "魔法防御上限", user.User_Attribute.Mac2.ToString());
                userINI.Write("属性点", "物理攻击力", user.User_Attribute.Dc.ToString());
                userINI.Write("属性点", "最大物理攻击力", user.User_Attribute.DcMax.ToString());
                userINI.Write("属性点", "魔法攻击力", user.User_Attribute.Mc.ToString());
                userINI.Write("属性点", "最大魔法攻击力", user.User_Attribute.McMax.ToString());
                userINI.Write("属性点", "道术攻击力", user.User_Attribute.Sc.ToString());
                userINI.Write("属性点", "最大道术攻击力", user.User_Attribute.ScMax.ToString());
                userINI.Write("属性点", "最大腕力", user.User_Attribute.WristMax.ToString());

                userINI.Write("属性点", "准确", user.User_Attribute.Acc.ToString());
                userINI.Write("属性点", "敏捷", user.User_Attribute.Agile.ToString());
                userINI.Write("属性点", "暴击", user.User_Attribute.Heals.ToString());
                userINI.Write("属性点", "幸运", user.User_Attribute.Lucky.ToString());
                userINI.Write("属性点", "魔法躲避", user.User_Attribute.MagicEvas.ToString());
                userINI.Write("属性点", "攻击速度", user.User_Attribute.AttackSpeed.ToString());
                userINI.Write("属性点", "移动速度", user.User_Attribute.MoveSpeed.ToString());
                userINI.Write("属性点", "物伤减免", user.User_Attribute.PhysicalDamageReduc.ToString());
                userINI.Write("属性点", "魔伤减免", user.User_Attribute.MagicDamageReduc.ToString());
                userINI.Write("属性点", "物伤反射", user.User_Attribute.PhysicalDamageReflex.ToString());
                userINI.Write("属性点", "魔伤反射", user.User_Attribute.MagicDamageReflex.ToString());
                userINI.Write("属性点", "暴击几率", user.User_Attribute.HealsPR.ToString());
                userINI.Write("属性点", "攻击伤害", user.User_Attribute.AttackPR.ToString());
                userINI.Write("属性点", "魔法防御", user.User_Attribute.MagicAC.ToString());
                userINI.Write("属性点", "忽视防御", user.User_Attribute.IgnoreAC.ToString());
                userINI.Write("属性点", "伤害反弹", user.User_Attribute.DamageRebound.ToString());
                userINI.Write("属性点", "人物爆率", user.User_Attribute.UserExpRate.ToString());
                userINI.Write("属性点", "体力增加", user.User_Attribute.HPIncrease.ToString());
                userINI.Write("属性点", "魔力增加", user.User_Attribute.MPIncrease.ToString());
                userINI.Write("属性点", "怪物爆率", user.User_Attribute.MonExpRate.ToString());
                userINI.Write("属性点", "防爆出率", user.User_Attribute.EquBurstRate.ToString());
                userINI.Write("属性点", "致命伤害", user.User_Attribute.FatalInjury.ToString());
                userINI.Write("属性点", "致命一击", user.User_Attribute.FatalBlow.ToString());
                userINI.Write("属性点", "卓越一击", user.User_Attribute.ExcellentBlow.ToString());
                userINI.Write("属性点", "会心一击", user.User_Attribute.HeartfeltBlow.ToString());

                if (user.User_VIPAttribute != null)
                {
                    user.User_Attribute.Hp += user.User_VIPAttribute.Hp;
                    user.User_Attribute.Mp += user.User_VIPAttribute.Mp;
                    user.User_Attribute.MaxHp += user.User_VIPAttribute.Hp;
                    user.User_Attribute.MaxMp += user.User_VIPAttribute.Mp;
                    user.User_Attribute.Ac += user.User_VIPAttribute.Ac;
                    user.User_Attribute.Ac2 += user.User_VIPAttribute.Ac2;
                    user.User_Attribute.Mac += user.User_VIPAttribute.Mac;
                    user.User_Attribute.Mac2 += user.User_VIPAttribute.Mac2;
                    user.User_Attribute.Dc += user.User_VIPAttribute.Dc;
                    user.User_Attribute.DcMax += user.User_VIPAttribute.DcMax;
                    user.User_Attribute.Mc += user.User_VIPAttribute.Mc;
                    user.User_Attribute.McMax += user.User_VIPAttribute.McMax;
                    user.User_Attribute.Sc += user.User_VIPAttribute.Sc;
                    user.User_Attribute.ScMax += user.User_VIPAttribute.ScMax;
                }
                if (user.User_YHZLAttribute != null)
                {
                    user.User_Attribute.Hp += user.User_YHZLAttribute.Hp;
                    user.User_Attribute.MaxHp += user.User_YHZLAttribute.Hp;
                    user.User_Attribute.Dc += user.User_YHZLAttribute.Dc;
                    user.User_Attribute.DcMax += user.User_YHZLAttribute.DcMax;
                    user.User_Attribute.Mc += user.User_YHZLAttribute.Mc;
                    user.User_Attribute.McMax += user.User_YHZLAttribute.McMax;
                    user.User_Attribute.Sc += user.User_YHZLAttribute.Sc;
                    user.User_Attribute.ScMax += user.User_YHZLAttribute.ScMax;
                }

                user.User_Attribute.WristMax = lengendBase.Wrist;
                user.User_General.WearMax = lengendBase.Wear;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        /// <summary>
        /// 获取物品
        /// </summary>
        static StditemDB GetStditemDB(StditemDB stdItem, List<MagicAttr> MagicAttrlist, List<MonsterDB> listMon, List<MagicDB> magicList, List<StditemDB> stdItemList)
        {
            #region MyRegion

            StditemConfig Std_Weapon = GetPremiumAttribute("武器类");
            StditemConfig Std_Dress = GetPremiumAttribute("衣服类");
            StditemConfig Std_Necklace = GetPremiumAttribute("项链类");
            StditemConfig Std_NecklaceBracelet = GetPremiumAttribute("项链手镯类");
            StditemConfig Std_Bracelet = GetPremiumAttribute("手镯类");
            StditemConfig Std_Ring = GetPremiumAttribute("戒指类");
            StditemConfig Std_Helmet = GetPremiumAttribute("头盔类");
            StditemConfig Std_Boots = GetPremiumAttribute("靴子类");
            StditemConfig Std_Belt = GetPremiumAttribute("腰带类");

            List<string> list1 = new List<string>();
            list1.Add("普通攻击{0}%几率提升{1}点攻击上限,持续{2}秒");
            list1.Add("普通攻击{0}%几率提升{1}点魔法上限,持续{2}秒");
            list1.Add("普通攻击{0}%几率提升{1}点道术上限,持续{2}秒");
            list1.Add("普通攻击{0}%几率提升{1}点防御上限,持续{2}秒");
            list1.Add("普通攻击{0}%几率提升{1}点魔御上限,持续{2}秒");
            list1.Add("普通攻击{0}%几率体力增加{1}点,持续{2}秒");
            list1.Add("普通攻击{0}%几率魔力增加{1}点,持续{2}秒");
            list1.Add("普通攻击{0}%几率让对方防御减少{1}%,持续{2}秒");
            list1.Add("普通攻击{0}%几率让对方魔御减少{1}%,持续{2}秒");
            list1.Add("普通攻击{0}%几率触发打怪增加{1}金币,持续{2}秒");
            list1.Add("普通攻击{0}%几率触发打怪减少{1}金币,持续{2}秒");
            list1.Add("普通攻击{0}%几率触发打怪增加{1}声望,持续{2}秒");
            list1.Add("普通攻击{0}%几率触发打怪减少{1}声望,持续{2}秒");
            list1.Add("普通攻击{0}%几率触发打怪增加{1}金刚石,持续{2}秒");
            list1.Add("普通攻击{0}%几率触发打怪减少{1}金刚石,持续{2}秒");
            List<string> list2 = new List<string>();
            list2.Add("{0}%几率触发{1}技能召唤一只{2},攻击越高,宝宝攻击越高,持续{3}秒");
            list2.Add("{0}%几率触发{1}技能召唤一只{2},魔法越高,宝宝攻击越高,持续{3}秒");
            list2.Add("{0}%几率触发{1}技能召唤一只{2},道术越高,宝宝攻击越高,持续{3}秒");
            list2.Add("使用{0}技能时攻魔道增加{1}%,使对方失明{2}秒,打掉对方HP{3}");
            list2.Add("使用{0}技能时攻魔道增加{1}%,使对方失明{2}秒,打掉对方防御{3}");
            list2.Add("使用{0}技能时攻魔道增加{1}%,使对方失明{2}秒,打掉对方魔御{3}");
            list2.Add("使用{0}技能时防御魔御增加{1}%,使对方无法释放技能{2}秒,持续{3}秒");
            list2.Add("{0}%几率触发使用{1}技能时可以让对方攻击无效{2}%,有{3}%几率拿对方金币");
            list2.Add("{0}%几率触发使用{1}技能时可以让对方攻击无效{2}%,有{3}%几率拿对方声望");
            list2.Add("{0}%几率触发使用{1}技能时可以让对方攻击无效{2}%,有{3}%几率拿对方金刚石");
            list2.Add("使用{0}技能时造成{1}倍攻击,提升敏捷{2},怪物死亡后可鞭尸{3}次");
            list2.Add("使用{0}技能时造成{1}倍攻击,提升准确{2},怪物死亡后可鞭尸{3}次");
            List<string> list3 = new List<string>();
            list3.Add("佩戴时等级+{0},攻魔道增加{1}%,有{2}%几率召唤一只赤月恶魔,攻击越高,宝宝攻击越高");
            list3.Add("佩戴时等级+{0},攻魔道增加{1}%,有{2}%几率召唤一只赤月恶魔,魔法越高,宝宝攻击越高");
            list3.Add("佩戴时等级+{0},攻魔道增加{1}%,有{2}%几率召唤一只赤月恶魔,道术越高,宝宝攻击越高");
            list3.Add("佩戴时攻击+{0},与{1}搭配触发超级攻击,{2}%刀刀切割怪物(人物无效)");
            list3.Add("佩戴时魔法+{0},与{1}搭配触发超级攻击,{2}%刀刀切割怪物(人物无效)");
            list3.Add("佩戴时道术+{0},与{1}搭配触发超级攻击,{2}%刀刀切割怪物(人物无效)");
            list3.Add("佩戴时有{0}%几率增加人物{1}倍爆率,有{2}%几率二次鞭尸");
            list3.Add("佩戴时有{0}%几率增加人物{1}幸运,幸运9双倍攻击伤害,幸运12三倍攻击伤害,12以上每3点增加{2}倍攻击伤害");
            list3.Add("佩戴时有{0}%几率获得{1}倍普通攻击伤害增加{2}%");
            list3.Add("佩戴时获得{0}点穿戴重量{1}%几率获得触发超级攻击,有{2}%几率触发复活");
            list3.Add("佩戴时获得{0}点穿戴重量{1}%几率召唤一只祖玛教主,有{2}%几率触发复活");
            list3.Add("佩戴时获得{0}点腕力{1}%几率获得触发超级攻击,有{2}%几率触发复活");
            list3.Add("佩戴时获得{0}点腕力{1}%几率召唤一只祖玛教主,有{2}%几率触发复活");
            #endregion

            MonsterDB monsterDB = null;
            MagicDB magic = null;
            StditemDB stditem2 = null;
            int qty = 0;
            bool isSetElmAttr = false, isSetHidAttr = false, isSetMagicAttr = false;

            if (JP_Qty > 0)
            {
                #region 获取基础属性_带极品
                if (stdItem.OverLap > 0) stdItem.SubAttr1 = 1;
                switch (stdItem.StdMode)
                {
                    case 5:
                    case 6:// 武器
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.DC_MaxQty >= qty && Std_Weapon.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Weapon.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.MAC_MaxQty >= qty && Std_Weapon.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Weapon.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.SC_MaxQty >= qty && Std_Weapon.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Weapon.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.DUR_MaxQty >= qty && Std_Weapon.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Weapon.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.HP_MaxQty >= qty && Std_Weapon.HP_MaxQty > 0) stdItem.SubAttr14 += rnd.Next(1, Std_Weapon.HP_Max + 1);// 极品加HP
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.ACC_MaxQty >= qty && Std_Weapon.ACC_MaxQty > 0) stdItem.SubAttr6 += rnd.Next(1, Std_Weapon.ACC_Max + 1);// 极品加准确
                        qty = rnd.Next(JP_Qty);
                        if (Std_Weapon.STR_MaxQty >= qty && Std_Weapon.STR_MaxQty > 0) stdItem.SubAttr13 += rnd.Next(1, Std_Weapon.STR_Max + 1);// 极品加强度
                        isSetElmAttr = true;
                        isSetHidAttr = true;
                        isSetMagicAttr = true;
                        break;
                    case 10:
                    case 11:// 衣服
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.DC_MaxQty >= qty && Std_Dress.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Dress.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.MAC_MaxQty >= qty && Std_Dress.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Dress.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.SC_MaxQty >= qty && Std_Dress.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Dress.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.DUR_MaxQty >= qty && Std_Dress.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Dress.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.AC_MaxQty >= qty && Std_Dress.AC_MaxQty > 0) stdItem.SubAttr1 += rnd.Next(1, Std_Dress.AC_Max + 1);// 极品加防御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.MC_MaxQty >= qty && Std_Dress.MC_MaxQty > 0) stdItem.SubAttr2 += rnd.Next(1, Std_Dress.MC_Max + 1);// 极品加魔御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Dress.HP_MaxQty >= qty && Std_Dress.HP_MaxQty > 0) stdItem.SubAttr14 += rnd.Next(1, Std_Dress.HP_Max + 1);// 极品加HP
                        isSetElmAttr = true;
                        isSetHidAttr = true;
                        isSetMagicAttr = true;
                        break;
                    case 15:// 头盔
                        qty = rnd.Next(JP_Qty);
                        if (Std_Helmet.DC_MaxQty >= qty && Std_Helmet.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Helmet.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Helmet.MAC_MaxQty >= qty && Std_Helmet.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Helmet.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Helmet.SC_MaxQty >= qty && Std_Helmet.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Helmet.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Helmet.DUR_MaxQty >= qty && Std_Helmet.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Helmet.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Helmet.AC_MaxQty >= qty && Std_Helmet.AC_MaxQty > 0) stdItem.SubAttr1 += rnd.Next(1, Std_Helmet.AC_Max + 1);// 极品加防御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Helmet.MC_MaxQty >= qty && Std_Helmet.MC_MaxQty > 0) stdItem.SubAttr2 += rnd.Next(1, Std_Helmet.MC_Max + 1);// 极品加魔御
                        isSetElmAttr = true;
                        break;
                    case 16:
                        break;
                    case 19:// 项链
                        qty = rnd.Next(JP_Qty);
                        if (Std_Necklace.DC_MaxQty >= qty && Std_Necklace.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Necklace.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Necklace.MAC_MaxQty >= qty && Std_Necklace.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Necklace.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Necklace.SC_MaxQty >= qty && Std_Necklace.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Necklace.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Necklace.DUR_MaxQty >= qty && Std_Necklace.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Necklace.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Necklace.LUK_MaxQty >= qty && Std_Necklace.LUK_MaxQty > 0) stdItem.SubAttr8 += rnd.Next(1, Std_Necklace.LUK_Max + 1);// 极品加幸运
                        isSetElmAttr = true;
                        break;
                    case 20:
                    case 21:
                    case 24:// 项链手镯
                        qty = rnd.Next(JP_Qty);
                        if (Std_NecklaceBracelet.DC_MaxQty >= qty && Std_NecklaceBracelet.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_NecklaceBracelet.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_NecklaceBracelet.MAC_MaxQty >= qty && Std_NecklaceBracelet.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_NecklaceBracelet.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_NecklaceBracelet.SC_MaxQty >= qty && Std_NecklaceBracelet.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_NecklaceBracelet.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_NecklaceBracelet.DUR_MaxQty >= qty && Std_NecklaceBracelet.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_NecklaceBracelet.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_NecklaceBracelet.ACC_MaxQty >= qty && Std_NecklaceBracelet.ACC_MaxQty > 0) stdItem.SubAttr6 += rnd.Next(1, Std_NecklaceBracelet.ACC_Max + 1);// 极品加准确
                        qty = rnd.Next(JP_Qty);
                        if (Std_NecklaceBracelet.AG_MaxQty >= qty && Std_NecklaceBracelet.AG_MaxQty > 0) stdItem.SubAttr7 += rnd.Next(1, Std_NecklaceBracelet.AG_Max + 1);// 极品加敏捷
                        isSetElmAttr = true;
                        break;
                    case 22:
                    case 23:// 戒指
                        qty = rnd.Next(JP_Qty);
                        if (Std_Ring.DC_MaxQty >= qty && Std_Ring.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Ring.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Ring.MAC_MaxQty >= qty && Std_Ring.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Ring.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Ring.SC_MaxQty >= qty && Std_Ring.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Ring.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Ring.DUR_MaxQty >= qty && Std_Ring.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Ring.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Ring.AC_MaxQty >= qty && Std_Ring.AC_MaxQty > 0) stdItem.SubAttr1 += rnd.Next(1, Std_Ring.AC_Max + 1);// 极品加防御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Ring.MC_MaxQty >= qty && Std_Ring.MC_MaxQty > 0) stdItem.SubAttr2 += rnd.Next(1, Std_Ring.MC_Max + 1);// 极品加魔御
                        isSetElmAttr = true;
                        break;
                    case 26:// 手镯
                        qty = rnd.Next(JP_Qty);
                        if (Std_Bracelet.DC_MaxQty >= qty && Std_Bracelet.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Bracelet.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Bracelet.MAC_MaxQty >= qty && Std_Bracelet.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Bracelet.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Bracelet.SC_MaxQty >= qty && Std_Bracelet.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Bracelet.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Bracelet.DUR_MaxQty >= qty && Std_Bracelet.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Bracelet.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Bracelet.AC_MaxQty >= qty && Std_Bracelet.AC_MaxQty > 0) stdItem.SubAttr1 += rnd.Next(1, Std_Bracelet.AC_Max + 1);// 极品加防御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Bracelet.MC_MaxQty >= qty && Std_Bracelet.MC_MaxQty > 0) stdItem.SubAttr2 += rnd.Next(1, Std_Bracelet.MC_Max + 1);// 极品加魔御
                        isSetElmAttr = true;
                        break;
                    case 27:
                    case 28:
                    case 29:
                        break;
                    case 52:
                    case 62:// 鞋子
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.DC_MaxQty >= qty && Std_Boots.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Boots.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.MAC_MaxQty >= qty && Std_Boots.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Boots.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.SC_MaxQty >= qty && Std_Boots.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Boots.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.DUR_MaxQty >= qty && Std_Boots.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Boots.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.AC_MaxQty >= qty && Std_Boots.AC_MaxQty > 0) stdItem.SubAttr1 += rnd.Next(1, Std_Boots.AC_Max + 1);// 极品加防御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.MC_MaxQty >= qty && Std_Boots.MC_MaxQty > 0) stdItem.SubAttr2 += rnd.Next(1, Std_Boots.MC_Max + 1);// 极品加魔御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Boots.FZ_MaxQty >= qty && Std_Boots.FZ_MaxQty > 0) stdItem.SubAttr11 += rnd.Next(1, Std_Boots.FZ_Max + 1);// 极品加负重
                        isSetElmAttr = true;
                        break;
                    case 54:
                    case 64:// 腰带
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.DC_MaxQty >= qty && Std_Belt.DC_MaxQty > 0) stdItem.SubAttr3 += rnd.Next(1, Std_Belt.DC_Max + 1);// 极品加攻击
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.MAC_MaxQty >= qty && Std_Belt.MAC_MaxQty > 0) stdItem.SubAttr4 += rnd.Next(1, Std_Belt.MAC_Max + 1);// 极品加魔法
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.SC_MaxQty >= qty && Std_Belt.SC_MaxQty > 0) stdItem.SubAttr5 += rnd.Next(1, Std_Belt.SC_Max + 1);// 极品加道术
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.DUR_MaxQty >= qty && Std_Belt.DUR_MaxQty > 0) stdItem.SubAttr12 += rnd.Next(1, Std_Belt.DUR_Max + 1) * 1000;// 极品加持久
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.AC_MaxQty >= qty && Std_Belt.AC_MaxQty > 0) stdItem.SubAttr1 += rnd.Next(1, Std_Belt.AC_Max + 1);// 极品加防御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.MC_MaxQty >= qty && Std_Belt.MC_MaxQty > 0) stdItem.SubAttr2 += rnd.Next(1, Std_Belt.MC_Max + 1);// 极品加魔御
                        qty = rnd.Next(JP_Qty);
                        if (Std_Belt.FZ_MaxQty >= qty && Std_Belt.FZ_MaxQty > 0) stdItem.SubAttr11 += rnd.Next(1, Std_Belt.FZ_Max + 1);// 极品加负重
                        isSetElmAttr = true;
                        break;
                }
                #endregion

                #region 获取元素属性
                if (isSetElmAttr && YSJP_Qty > 0)
                {
                    isSetElmAttr = false;
                    stdItem.StdElemAttr = new ElemAttr();
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.MagicEvas_Qty >= qty && Std_Weapon.MagicEvas_Qty > 0)
                    {
                        stdItem.StdElemAttr.MagicEvas += rnd.Next(1, Std_Weapon.MagicEvas + 1);// 人物魔法躲避
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.AttackSpeed_Qty >= qty && Std_Weapon.AttackSpeed_Qty > 0)
                    {
                        stdItem.StdElemAttr.AttackSpeed += rnd.Next(1, Std_Weapon.AttackSpeed + 1);// 增加攻击速度
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.PhysicalDamageReduc_Qty >= qty && Std_Weapon.PhysicalDamageReduc_Qty > 0)
                    {
                        stdItem.StdElemAttr.PhysicalDamageReduc += rnd.Next(1, Std_Weapon.PhysicalDamageReduc + 1);// 物理伤害减少
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.MagicDamageReduc_Qty >= qty && Std_Weapon.MagicDamageReduc_Qty > 0)
                    {
                        stdItem.StdElemAttr.MagicDamageReduc += rnd.Next(1, Std_Weapon.MagicDamageReduc + 1);// 魔法伤害减少
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.PhysicalDamageReflex_Qty >= qty && Std_Weapon.PhysicalDamageReflex_Qty > 0)
                    {
                        stdItem.StdElemAttr.PhysicalDamageReflex += rnd.Next(1, Std_Weapon.PhysicalDamageReflex + 1);// 人物物伤反射
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.MagicDamageReflex_Qty >= qty && Std_Weapon.MagicDamageReflex_Qty > 0)
                    {
                        stdItem.StdElemAttr.MagicDamageReflex += rnd.Next(1, Std_Weapon.MagicDamageReflex + 1);// 人物魔伤反射
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.HealsPR_Qty >= qty && Std_Weapon.HealsPR_Qty > 0)
                    {
                        stdItem.StdElemAttr.HealsPR += rnd.Next(1, Std_Weapon.HealsPR + 1);// 暴击几率增加
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.AttackPR_Qty >= qty && Std_Weapon.AttackPR_Qty > 0)
                    {
                        stdItem.StdElemAttr.AttackPR += rnd.Next(1, Std_Weapon.AttackPR + 1);// 增加攻击伤害
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.MagicAC_Qty >= qty && Std_Weapon.MagicAC_Qty > 0)
                    {
                        stdItem.StdElemAttr.MagicAC += rnd.Next(1, Std_Weapon.MagicAC + 1);// 增加魔法防御
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.IgnoreAC_Qty >= qty && Std_Weapon.IgnoreAC_Qty > 0)
                    {
                        stdItem.StdElemAttr.IgnoreAC += rnd.Next(1, Std_Weapon.IgnoreAC + 1);// 忽视目标防御
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.DamageRebound_Qty >= qty && Std_Weapon.DamageRebound_Qty > 0)
                    {
                        stdItem.StdElemAttr.DamageRebound += rnd.Next(1, Std_Weapon.DamageRebound + 1);// 所有伤害反弹
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.UserExpRate_Qty >= qty && Std_Weapon.UserExpRate_Qty > 0)
                    {
                        stdItem.StdElemAttr.UserExpRate += rnd.Next(1, Std_Weapon.UserExpRate + 1);// 增加人物爆率
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.HPIncrease_Qty >= qty && Std_Weapon.HPIncrease_Qty > 0)
                    {
                        stdItem.StdElemAttr.HPIncrease += rnd.Next(1, Std_Weapon.HPIncrease + 1);// 人物体力增加
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.MonExpRate_Qty >= qty && Std_Weapon.MonExpRate_Qty > 0)
                    {
                        stdItem.StdElemAttr.MonExpRate += rnd.Next(1, Std_Weapon.MonExpRate + 1);// 增加怪物爆率
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.FatalInjury_Qty >= qty && Std_Weapon.FatalInjury_Qty > 0)
                    {
                        stdItem.StdElemAttr.FatalInjury += rnd.Next(1, Std_Weapon.FatalInjury + 1);// 增加致命伤害
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.FatalBlow_Qty >= qty && Std_Weapon.FatalBlow_Qty > 0)
                    {
                        stdItem.StdElemAttr.FatalBlow += rnd.Next(1, Std_Weapon.FatalBlow + 1);// 致命一击
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.ExcellentBlow_Qty >= qty && Std_Weapon.ExcellentBlow_Qty > 0)
                    {
                        stdItem.StdElemAttr.ExcellentBlow += rnd.Next(1, Std_Weapon.ExcellentBlow + 1);// 卓越一击
                        isSetElmAttr = true;
                    }
                    qty = rnd.Next(YSJP_Qty);
                    if (Std_Weapon.HeartfeltBlow_Qty >= qty && Std_Weapon.HeartfeltBlow_Qty > 0)
                    {
                        stdItem.StdElemAttr.HeartfeltBlow += rnd.Next(1, Std_Weapon.HeartfeltBlow + 1);// 会心一击
                        isSetElmAttr = true;
                    }
                    if (!isSetElmAttr) stdItem.StdElemAttr = null;
                }
                #endregion

                #region 获取隐藏属性
                if (isSetHidAttr)
                {
                    qty = rnd.Next(3);
                    int HidProbability = rnd.Next(50);//概率
                    int HidDuration = rnd.Next(30);//时间
                    switch (qty)
                    {
                        case 0:
                            HidProbability = rnd.Next(20);
                            stdItem.StdHiddAttr = new HiddAttr
                            {
                                HidTitle = string.Format(list1[rnd.Next(list1.Count)], HidProbability, rnd.Next(20), HidDuration),
                                HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                HidProbability = HidProbability,
                                HidDuration = HidDuration,
                                Type = 0
                            };
                            break;// 普通攻击触发
                        case 1:
                            qty = rnd.Next(list2.Count);
                            switch (qty)
                            {
                                case 0:
                                case 1:
                                case 2:
                                    monsterDB = listMon[rnd.Next(listMon.Count)];
                                    magic = magicList[rnd.Next(magicList.Count)];
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list2[qty], HidProbability, magic.MagName, monsterDB.MonName, HidDuration),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        MagId = magic.MagID,
                                        MagName = magic.MagName,
                                        MonId = monsterDB.MonID,
                                        MonName = monsterDB.MonName,
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        Type = 1
                                    };
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 10:
                                case 11:
                                    int prqty = rnd.Next(35);
                                    int rdqty = rnd.Next(10);
                                    magic = magicList[rnd.Next(magicList.Count)];
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list2[qty], magic.MagName, rnd.Next(35), prqty, rdqty),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        MagId = magic.MagID,
                                        MagName = magic.MagName,
                                        DcPR = prqty,
                                        McPR = prqty,
                                        ScPR = prqty,
                                        ACRD = rdqty,
                                        MacRD = rdqty,
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        Type = 1
                                    };
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                    magic = magicList[rnd.Next(magicList.Count)];
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list2[qty], HidProbability, magic.MagName, rnd.Next(10), rnd.Next(10)),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        MagId = magic.MagID,
                                        MagName = magic.MagName,
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        Type = 1
                                    };
                                    break;
                            }
                            break;// 技能攻击触发
                        case 2:
                            qty = rnd.Next(list3.Count);
                            switch (qty)
                            {
                                case 0:
                                case 1:
                                case 2:
                                    HidDuration = rnd.Next(1, 3);
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list3[qty], HidDuration, rnd.Next(30), HidProbability),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        HidProbability = HidProbability,
                                        Lv = HidDuration,
                                        Type = 2
                                    };
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                    stditem2 = stdItemList[rnd.Next(stdItemList.Count)];
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list3[qty], rnd.Next(30), stditem2.StdName, HidProbability),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        DPStdId = stditem2.StdID,
                                        DPStdName = stditem2.StdName,
                                        Type = 2
                                    };
                                    break;
                                case 9:
                                case 10:
                                    HidDuration = rnd.Next(50);
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list3[qty], HidDuration, rnd.Next(20), HidProbability),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        Wear = HidDuration,
                                        Type = 2
                                    };
                                    break;
                                case 11:
                                case 12:
                                    HidDuration = rnd.Next(50);
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list3[qty], HidDuration, rnd.Next(20), HidProbability),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        Wrist = HidDuration,
                                        Type = 2
                                    };
                                    break;
                                default:
                                    stdItem.StdHiddAttr = new HiddAttr
                                    {
                                        HidTitle = string.Format(list3[qty], rnd.Next(50), rnd.Next(20), HidProbability),
                                        HidImg = rnd.Next(850, 860).ToString().PadLeft(6, '0') + ".png",
                                        HidProbability = HidProbability,
                                        HidDuration = HidDuration,
                                        Type = 2
                                    };
                                    break;
                            }
                            break;// 佩戴触发
                    }
                }
                #endregion

                #region 获取技能属性
                if (isSetMagicAttr)
                {
                    stdItem.StdMagicAttr = new List<MagicAttr>();
                    MagicAttr magicAttr = null;
                    MagicAttrlist.Clear();

                    qty = rnd.Next(3);
                    switch (qty)
                    {
                        case 0:
                            magicAttr = new MagicAttr
                            {
                                MagId = 7,
                                MagName = "攻杀剑术",
                                AttackPR = rnd.Next(1, 15),
                                MagImg = "002007.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            magicAttr = new MagicAttr
                            {
                                MagId = 12,
                                MagName = "刺杀剑术",
                                AttackPR = rnd.Next(1, 20),
                                MagImg = "002008.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            magicAttr = new MagicAttr
                            {
                                MagId = 26,
                                MagName = "烈火剑法",
                                AttackPR = rnd.Next(1, 5),
                                MagImg = "002009.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            break;
                        case 1:
                            magicAttr = new MagicAttr
                            {
                                MagId = 11,
                                MagName = "雷 电 术",
                                AttackPR = rnd.Next(1, 15),
                                MagImg = "002004.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            magicAttr = new MagicAttr
                            {
                                MagId = 24,
                                MagName = "地狱雷光",
                                AttackPR = rnd.Next(1, 20),
                                MagImg = "002005.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            magicAttr = new MagicAttr
                            {
                                MagId = 33,
                                MagName = "冰 咆 哮",
                                AttackPR = rnd.Next(1, 25),
                                MagImg = "002006.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            break;
                        case 2:
                            magicAttr = new MagicAttr
                            {
                                MagId = 11,
                                MagName = "施 毒 术",
                                AttackPR = rnd.Next(1, 15),
                                MagImg = "002001.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            magicAttr = new MagicAttr
                            {
                                MagId = 24,
                                MagName = "灵魂火符",
                                AttackPR = rnd.Next(1, 20),
                                MagImg = "002002.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            magicAttr = new MagicAttr
                            {
                                MagId = 33,
                                MagName = "召唤神兽",
                                AttackPR = rnd.Next(1, 5),
                                MagImg = "002003.png",
                            };
                            MagicAttrlist.Add(magicAttr);
                            break;
                    }
                    stdItem.StdMagicAttr.AddRange(MagicAttrlist);
                }
                #endregion
            }

            return stdItem;
        }

        static StditemConfig GetPremiumAttribute(string section)
        {
            string path = LegendFilePath.PremiumAttributePath + "极品信息.ini";
            IniFileHelper paInI = new IniFileHelper(path);
            StditemConfig sc = new StditemConfig();
            sc.DC_Max = int.Parse(paInI.Read(section, "攻击最高点数"));
            sc.DC_MaxQty = int.Parse(paInI.Read(section, "攻击属性几率"));
            sc.MAC_Max = int.Parse(paInI.Read(section, "魔法最高点数"));
            sc.MAC_MaxQty = int.Parse(paInI.Read(section, "魔法属性几率"));
            sc.SC_Max = int.Parse(paInI.Read(section, "道术最高点数"));
            sc.SC_MaxQty = int.Parse(paInI.Read(section, "道术属性几率"));
            sc.AC_Max = int.Parse(paInI.Read(section, "防御最高点数"));
            sc.AC_MaxQty = int.Parse(paInI.Read(section, "防御属性几率"));
            sc.MC_Max = int.Parse(paInI.Read(section, "魔御最高点数"));
            sc.MC_MaxQty = int.Parse(paInI.Read(section, "魔御属性几率"));
            sc.HP_Max = int.Parse(paInI.Read(section, "加血最高点数"));
            sc.HP_MaxQty = int.Parse(paInI.Read(section, "加血属性几率"));
            sc.ACC_Max = int.Parse(paInI.Read(section, "准确最高点数"));
            sc.ACC_MaxQty = int.Parse(paInI.Read(section, "准确属性几率"));
            sc.STR_Max = int.Parse(paInI.Read(section, "强度最高点数"));
            sc.STR_MaxQty = int.Parse(paInI.Read(section, "强度属性几率"));
            sc.LUK_Max = int.Parse(paInI.Read(section, "幸运最高点数"));
            sc.LUK_MaxQty = int.Parse(paInI.Read(section, "幸运属性几率"));
            sc.DUR_Max = int.Parse(paInI.Read(section, "持久最高点数"));
            sc.DUR_MaxQty = int.Parse(paInI.Read(section, "持久属性几率"));
            sc.AG_Max = int.Parse(paInI.Read(section, "敏捷最高点数"));
            sc.AG_MaxQty = int.Parse(paInI.Read(section, "敏捷属性几率"));
            sc.FZ_Max = int.Parse(paInI.Read(section, "负重最高点数"));
            sc.FZ_MaxQty = int.Parse(paInI.Read(section, "负重属性几率"));

            if (section != "项链手镯类")
            {
                path = LegendFilePath.PremiumAttributePath + "新极品信息.ini";
                paInI = new IniFileHelper(path);
                sc.MagicEvas = int.Parse(paInI.Read(section, "人物魔法躲避"));
                sc.AttackSpeed = int.Parse(paInI.Read(section, "增加攻击速度"));
                sc.MoveSpeed = int.Parse(paInI.Read(section, "增加移动速度"));
                sc.PhysicalDamageReduc = int.Parse(paInI.Read(section, "物理伤害减少"));
                sc.MagicDamageReduc = int.Parse(paInI.Read(section, "魔法伤害减少"));
                sc.PhysicalDamageReflex = int.Parse(paInI.Read(section, "人物物伤反射"));
                sc.MagicDamageReflex = int.Parse(paInI.Read(section, "人物魔伤反射"));
                sc.HealsPR = int.Parse(paInI.Read(section, "暴击几率增加"));
                sc.AttackPR = int.Parse(paInI.Read(section, "增加攻击伤害"));
                sc.MagicAC = int.Parse(paInI.Read(section, "人物魔法防御"));
                sc.IgnoreAC = int.Parse(paInI.Read(section, "忽视目标防御"));
                sc.DamageRebound = int.Parse(paInI.Read(section, "所有伤害反弹"));
                sc.UserExpRate = int.Parse(paInI.Read(section, "增加人物爆率"));
                sc.HPIncrease = int.Parse(paInI.Read(section, "人物体力增加"));
                sc.MonExpRate = int.Parse(paInI.Read(section, "增加怪物爆率"));
                sc.FatalInjury = int.Parse(paInI.Read(section, "增加致命伤害"));
                sc.FatalBlow = int.Parse(paInI.Read(section, "增加致命一击"));
                sc.ExcellentBlow = int.Parse(paInI.Read(section, "增加卓越一击"));
                sc.HeartfeltBlow = int.Parse(paInI.Read(section, "增加会心一击"));
                sc.MagicEvas_Qty = int.Parse(paInI.Read(section, "人物魔法躲避几率"));
                sc.AttackSpeed_Qty = int.Parse(paInI.Read(section, "增加攻击速度几率"));
                sc.MoveSpeed_Qty = int.Parse(paInI.Read(section, "增加移动速度几率"));
                sc.PhysicalDamageReduc_Qty = int.Parse(paInI.Read(section, "物理伤害减少几率"));
                sc.MagicDamageReduc_Qty = int.Parse(paInI.Read(section, "魔法伤害减少几率"));
                sc.PhysicalDamageReflex_Qty = int.Parse(paInI.Read(section, "人物物伤反射几率"));
                sc.MagicDamageReflex_Qty = int.Parse(paInI.Read(section, "人物魔伤反射几率"));
                sc.HealsPR_Qty = int.Parse(paInI.Read(section, "暴击几率增加几率"));
                sc.AttackPR_Qty = int.Parse(paInI.Read(section, "增加攻击伤害几率"));
                sc.MagicAC_Qty = int.Parse(paInI.Read(section, "人物魔法防御几率"));
                sc.IgnoreAC_Qty = int.Parse(paInI.Read(section, "忽视目标防御几率"));
                sc.DamageRebound_Qty = int.Parse(paInI.Read(section, "所有伤害反弹几率"));
                sc.UserExpRate_Qty = int.Parse(paInI.Read(section, "增加人物爆率几率"));
                sc.HPIncrease_Qty = int.Parse(paInI.Read(section, "人物体力增加几率"));
                sc.MonExpRate_Qty = int.Parse(paInI.Read(section, "增加怪物爆率几率"));
                sc.FatalInjury_Qty = int.Parse(paInI.Read(section, "增加致命伤害几率"));
                sc.FatalBlow_Qty = int.Parse(paInI.Read(section, "增加致命一击几率"));
                sc.ExcellentBlow_Qty = int.Parse(paInI.Read(section, "增加卓越一击几率"));
                sc.HeartfeltBlow_Qty = int.Parse(paInI.Read(section, "增加会心一击几率"));
            }

            if (JP_Qty == 0)
            {
                paInI = new IniFileHelper(LegendFilePath.ConfigString);
                JP_Qty = int.Parse(paInI.Read("配置信息", "极品出现几率")) + 1;

                YSJP_Qty = int.Parse(paInI.Read("配置信息", "元素极品出现几率")) + 1;
            }

            return sc;
        }

        static long UserUseMedication(IniFileHelper userINI, long userHP, long hpQty)
        {
            var pbList = userINI.GetAllKeys("背包物品");
            foreach (var key in pbList)
            {
                StditemDB std = Api.JsonDeserialize<StditemDB>(userINI.ReadValue("背包物品", key));

                if (std.StdMode == 0 && std.StdHP > 0)// 如果有药就开始吃
                {
                    while (userHP <= hpQty)//如果血量小于限额血量则一直吃药,到吃上来为止
                    {
                        if (std.SubAttr1 > 0)
                        {
                            std.SubAttr1 -= 1;
                            userHP += std.StdHP;
                        }
                        else
                        {
                            std.SubAttr1 = 0;
                            break;
                        }
                    }
                    if (std.SubAttr1 == 0)
                    {
                        // 如果药没有了则删除背包里的药品
                        userINI.Delete("背包物品", key);
                    }
                    else
                    {
                        // 如果背包里还有药品则减去吃掉的写回背包中
                        userINI.Write("背包物品", key, Api.JsonSerialize<StditemDB>(std));
                        break;
                    }
                }
            }
            return userHP;
        }

        /// <summary>
        /// 自动回收装备
        /// </summary>
        static bool UserAutoRecyclStditems(UserInfo user, IniFileHelper userInI, List<StditemDB> stdItemList, List<string> ljqeulist, StditemDB std)
        {
            try
            {
                bool flag = false;
                int exp = 0, yb = 0, jb = 0;
                string[] arrRecyclStd;
                string strRecyclStd = "";
                if (user.User_EquRecycleAttr.AutoRecycl == "是")// 自动回收
                {
                    // 回收垃圾装备
                    if (user.User_EquRecycleAttr.AutoRecyclType1 == "是")
                    {
                        if (ljqeulist.Contains(std.StdName))
                        {
                            exp = 1;
                            jb = 100;
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.JB += jb;
                            userInI.Write("普通数据", "金币", user.User_General.JB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    // 回收沃玛装备
                    if (user.User_EquRecycleAttr.AutoRecyclType2 == "是")
                    {
                        strRecyclStd = "炼狱,银蛇,井中月,无极棍,魔杖,幽灵项链,生命项链,天珠项链,龙之戒指,红宝石戒指,铂金戒指,幽灵手套,思贝儿手镯,心灵手镯";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 10000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: jb = 500; break;
                                case 2: jb = 1000; break;
                                case 3: jb = 2000; break;
                                case 4: jb = 3000; break;
                                case 5: jb = 4000; break;
                                default: jb = 500; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.JB += jb;
                            userInI.Write("普通数据", "金币", user.User_General.JB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }

                    }
                    // 回收祖玛装备
                    if (user.User_EquRecycleAttr.AutoRecyclType3 == "是")
                    {
                        strRecyclStd = "裁决之杖,龙纹剑,血饮,骨玉权杖,黑铁头盔,绿色项链,恶魔铃铛,灵魂项链,力量戒指,紫碧螺,泰坦戒指,骑士手镯,龙之手镯,三眼手镯,阎罗手套,黑铁手套";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 40000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: jb = 2000; break;
                                case 2: jb = 4000; break;
                                case 3: jb = 6000; break;
                                case 4: jb = 8000; break;
                                case 5: jb = 10000; break;
                                default: jb = 2000; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "金币", user.User_General.JB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    // 回收赤月装备
                    if (user.User_EquRecycleAttr.AutoRecyclType4 == "是")
                    {
                        strRecyclStd = "屠龙,命运之刃,霸者之刃,龙牙,怒斩,逍遥扇,嗜魂法杖,天魔神甲,法神披风,天尊道袍,圣战宝甲,霓裳羽衣,天师长袍,圣战头盔,圣战项链,圣战戒指,圣战手镯,法神头盔,法神项链,法神戒指,法神手镯,天尊头盔,天尊项链,天尊戒指,天尊手镯,赤月腰带,赤月靴子";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 400000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: jb = 5000; break;
                                case 2: jb = 10000; break;
                                case 3: jb = 20000; break;
                                case 4: jb = 30000; break;
                                case 5: jb = 40000; break;
                                default: jb = 5000; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.JB += jb;
                            userInI.Write("普通数据", "金币", user.User_General.JB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    // 回收永恒首饰
                    if (user.User_EquRecycleAttr.AutoRecyclType5 == "是")
                    {
                        strRecyclStd = "宙神头盔,朱雀头盔,道域头盔,永恒圣链,永恒法链,永恒道链,永恒圣戒,永恒法戒,永恒道戒,永恒圣镯,永恒法镯,永恒道镯";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 600000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: yb = 10; break;
                                case 2: yb = 20; break;
                                case 3: yb = 30; break;
                                case 4: yb = 40; break;
                                case 5: yb = 60; break;
                                default: yb = 10; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    // 回收永恒剑甲
                    if (user.User_EquRecycleAttr.AutoRecyclType6 == "是")
                    {
                        strRecyclStd = "永恒魔剑,永恒圣刃,黄金屠龙,黄金逍遥扇,黄金噬魂法杖,白虎战甲,青龙道袍,朱雀神袍,白虎战衣,青龙道衣,朱雀神衣";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 1200000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: yb = 10; break;
                                case 2: yb = 20; break;
                                case 3: yb = 30; break;
                                case 4: yb = 40; break;
                                case 5: yb = 60; break;
                                default: yb = 10; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    // 回收毁灭首饰
                    if (user.User_EquRecycleAttr.AutoRecyclType7 == "是")
                    {
                        strRecyclStd = "毁灭战链,毁灭战戒,毁灭战镯,毁灭战章,毁灭战盔,毁灭战靴,毁灭战带,毁灭法链,毁灭法戒,毁灭法镯,毁灭法章,毁灭法盔,毁灭法靴,毁灭法带,毁灭道链,毁灭道戒,毁灭道镯,毁灭道章,毁灭道盔,毁灭道靴,毁灭道带";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 1600000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: yb = 60; break;
                                case 2: yb = 100; break;
                                case 3: yb = 200; break;
                                case 4: yb = 300; break;
                                case 5: yb = 400; break;
                                default: yb = 60; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    // 回收毁灭剑甲
                    if (user.User_EquRecycleAttr.AutoRecyclType8 == "是")
                    {
                        strRecyclStd = "毁灭战刃,毁灭法杖,毁灭羽扇,毁灭战甲,毁灭魔甲,毁灭道甲,毁灭战衣,毁灭魔衣,毁灭道衣,毁灭神甲,毁灭神袍";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            exp = 3000000;
                            switch (user.User_General.VipLv)
                            {
                                case 1: yb = 500; break;
                                case 2: yb = 600; break;
                                case 3: yb = 800; break;
                                case 4: yb = 1000; break;
                                case 5: yb = 1200; break;
                                default: yb = 500; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    if (user.User_EquRecycleAttr.AutoRecyclType9 == "是")
                    {
                        strRecyclStd = "刑天战盔,刑天战链,刑天护腕,刑天战戒,刑天腰带,刑天战靴,刑天勋章";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            switch (user.User_General.VipLv)
                            {
                                case 1: exp = 1; yb = 400; break;
                                case 2: exp = 2; yb = 500; break;
                                case 3: exp = 3; yb = 600; break;
                                case 4: exp = 4; yb = 700; break;
                                case 5: exp = 5; yb = 800; break;
                                default: exp = 1; yb = 400; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    if (user.User_EquRecycleAttr.AutoRecyclType10 == "是")
                    {
                        strRecyclStd = "焚天道盔,焚天道链,焚天手镯,焚天腰带,焚天道靴,焚天道戒,焚天勋章";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            switch (user.User_General.VipLv)
                            {
                                case 1: exp = 1; yb = 400; break;
                                case 2: exp = 2; yb = 500; break;
                                case 3: exp = 3; yb = 600; break;
                                case 4: exp = 4; yb = 700; break;
                                case 5: exp = 5; yb = 800; break;
                                default: exp = 1; yb = 400; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    if (user.User_EquRecycleAttr.AutoRecyclType11 == "是")
                    {
                        strRecyclStd = "沧澜魔盔,沧澜魔链,沧澜手镯,沧澜魔戒,沧澜腰带,沧澜魔靴,沧澜勋章";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            switch (user.User_General.VipLv)
                            {
                                case 1: exp = 1; yb = 400; break;
                                case 2: exp = 2; yb = 500; break;
                                case 3: exp = 3; yb = 600; break;
                                case 4: exp = 4; yb = 700; break;
                                case 5: exp = 5; yb = 800; break;
                                default: exp = 1; yb = 400; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    if (user.User_EquRecycleAttr.AutoRecyclType12 == "是")
                    {
                        strRecyclStd = "沧刑天战甲,沧澜魔甲,焚天道甲,刑天战袍,沧澜魔袍,焚天道袍";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            switch (user.User_General.VipLv)
                            {
                                case 1: exp = 1; yb = 800; break;
                                case 2: exp = 2; yb = 1000; break;
                                case 3: exp = 3; yb = 2000; break;
                                case 4: exp = 4; yb = 3000; break;
                                case 5: exp = 5; yb = 4000; break;
                                default: exp = 1; yb = 800; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                    if (user.User_EquRecycleAttr.AutoRecyclType13 == "是")
                    {
                        strRecyclStd = "刑天之怒,焚天之吼,沧澜之啸";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            switch (user.User_General.VipLv)
                            {
                                case 1: exp = 1; yb = 3000; break;
                                case 2: exp = 2; yb = 5000; break;
                                case 3: exp = 3; yb = 6000; break;
                                case 4: exp = 4; yb = 7000; break;
                                case 5: exp = 5; yb = 8000; break;
                                default: exp = 1; yb = 3000; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }

                    if (user.User_EquRecycleAttr.AutoRecyclType49 == "是")
                    {
                        strRecyclStd = "麻痹戒指,复活戒指,传送戒指,火焰戒指,护身戒指,超负载戒指,防御戒指,赤月斗笠";
                        arrRecyclStd = strRecyclStd.Split(',');
                        if (arrRecyclStd.Contains(std.StdName))
                        {
                            switch (user.User_General.VipLv)
                            {
                                case 1: exp = 10; yb = 300; break;
                                case 2: exp = 20; yb = 500; break;
                                case 3: exp = 30; yb = 600; break;
                                case 4: exp = 40; yb = 700; break;
                                case 5: exp = 50; yb = 800; break;
                                default: exp = 10; yb = 300; break;
                            }
                            if (user.User_VIPAttribute.MonExpInc > 0)
                            {
                                exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                            }
                            user.User_General.SumExp += exp;
                            user.User_General.YB += yb;
                            userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                            userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                            flag = true;
                        }
                    }
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}
