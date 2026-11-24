using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using 传奇.core;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace 传奇
{
    public class MagicApiImg
    {
        #region  成员变量
        static Random rnd = new Random();
        static string UserInfoImgPath = LegendFilePath.imgPath;//图片数据
        static string LegendIMGDataPath = LegendFilePath.LegendIMGDataPath;//图片样式

        static string prgusePath = string.Empty;
        static string magicPath = string.Empty;
        static string magicPlacementsPath = string.Empty;

        static string magic2Path = string.Empty;
        static string magic2PlacementsPath = string.Empty;

        static string magic5Path = string.Empty;
        static string magic5PlacementsPath = string.Empty;

        static string humPath = string.Empty;
        static string weaponPath = string.Empty;

        static string mon4Path = string.Empty;
        static string mon4PlacementsPath = string.Empty;

        static string mon18Path = string.Empty;
        static string mon18PlacementsPath = string.Empty;
        #endregion

        /// <summary>
        /// 获取魔法攻击效果图及偏移量
        /// </summary>
        public static List<string> GetAttackMagicImg(MagicDB magic, int direction)
        {
            try
            {
                magicPath = LegendIMGDataPath + "Magic\\";
                magicPlacementsPath = LegendIMGDataPath + "Magic\\Placements\\";

                magic5Path = LegendIMGDataPath + "magic5\\";
                magic5PlacementsPath = LegendIMGDataPath + "Magic5\\Placements\\";

                magic2Path = LegendIMGDataPath + "magic2\\";
                magic2PlacementsPath = LegendIMGDataPath + "Magic2\\Placements\\";


                List<string> posList = new List<string>();
                List<string> list = new List<string>();
                string path = "", placementsPath = "";
                switch (magic.MagID)
                {
                    #region 战士技能
                    case 3: case 4: break;// 基本剑法 精神力战法
                    case 7:
                        if (direction == 3)
                        {
                            for (int i = 2020; i < 2026; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 2060; i < 2066; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        break;// 攻杀剑术
                    case 12:
                        if (direction == 3)
                        {
                            for (int i = 2390; i < 2396; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 2430; i < 2436; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        break;// 刺杀剑术
                    case 25:
                        if (direction == 3)
                        {
                            for (int i = 2760; i < 2766; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 2880; i < 2886; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        break;// 半月
                    case 26:
                        if (direction == 3)
                        {
                            for (int i = 3500; i < 3506; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 3540; i < 3546; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        break;// 烈火
                    case 27:
                        if (direction == 3)
                        {
                            for (int i = 30; i < 40; i++)
                            {
                                path = magic5Path + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magic5PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 70; i < 80; i++)
                            {
                                path = magic5Path + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magic5PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        break;// 野蛮
                    case 34:
                        for (int i = 790; i < 800; i++)
                        {
                            path = magic5Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magic5PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 护体神盾
                    #endregion


                    #region 道士技能
                    case 2:
                        for (int i = 200; i < 210; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        for (int i = 370; i < 380; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 治愈术
                    case 6:
                        for (int i = 600; i < 610; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        for (int i = 760; i < 770; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        for (int i = 770; i < 780; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 施毒术
                    case 13:
                        int[] nArray = new int[10];
                        if (direction == 3)
                        {
                            int[] mArray = { 1220, 1221, 1222 };
                            nArray = mArray;
                        }
                        else
                        {
                            int[] mArray = { 1280, 1281, 1282 };
                            nArray = mArray;
                        }
                        foreach (int item in nArray)
                        {
                            path = magicPath + item.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + item.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        for (int i = 1360; i < 1370; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 灵魂火符
                    case 14:
                        for (int i = 1320; i < 1336; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 幽灵盾
                    case 15:
                        for (int i = 1340; i < 1356; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 神圣战甲术
                    case 16:
                        for (int i = 1380; i < 1390; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 困魔咒
                    case 17:
                        for (int i = 1500; i < 1510; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }

                        mon4Path = LegendIMGDataPath + "Mon4\\";
                        mon4PlacementsPath = LegendIMGDataPath + "Mon4\\Placements\\";
                        for (int i = 2860; i < 2870; i++)
                        {
                            path = mon4Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = mon4PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }

                        break;// 召唤骷髅
                    case 18:
                        for (int i = 1520; i < 1530; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 隐身术
                    case 19:
                        for (int i = 1540; i < 1550; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 集体隐身术
                    case 28:
                        for (int i = 3960; i < 3970; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 心灵启示
                    case 29:
                        for (int i = 1790; i < 1810; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 群体治疗术
                    case 30:
                        for (int i = 0; i < 10; i++)
                        {
                            path = magic2Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magic2PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }

                        mon18Path = LegendIMGDataPath + "Mon18\\";
                        mon18PlacementsPath = LegendIMGDataPath + "Mon18\\Placements\\";
                        for (int i = 10; i < 20; i++)
                        {
                            path = mon18Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = mon18PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        for (int i = 360; i < 370; i++)
                        {
                            path = mon18Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = mon18PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 召唤神兽
                    #endregion

                    #region 法师技能
                    case 1:
                        for (int i = 0; i < 10; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        if (direction == 3)
                        {
                            for (int i = 50; i < 53; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 130; i < 133; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }

                        for (int i = 170; i < 180; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 火球术
                    case 5:
                        for (int i = 400; i < 410; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        if (direction == 3)
                        {
                            for (int i = 450; i < 453; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 430; i < 433; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }

                        for (int i = 570; i < 580; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 大火球
                    case 8:
                        for (int i = 900; i < 906; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 抗拒火环
                    case 9:
                        for (int i = 920; i < 933; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 地狱火
                    case 10:
                        for (int i = 1140; i < 1150; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        if (direction == 3)
                        {
                            for (int i = 1010; i < 1016; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        else
                        {
                            for (int i = 1090; i < 1096; i++)
                            {
                                path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                                posList = FileHelper.ReadTxtReturnList(placementsPath);
                                list.Add(path + "," + string.Join(",", posList));
                            }
                        }
                        break;// 疾光电影
                    case 11:
                        for (int i = 20; i < 23; i++)
                        {
                            path = magic2Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magic2PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        for (int i = 10; i < 15; i++)
                        {
                            path = magic2Path + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magic2PlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 雷电术
                    case 20:
                        for (int i = 1560; i < 1580; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 诱惑之光
                    case 21:
                        for (int i = 1590; i < 1610; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 瞬息移动
                    case 22:
                        for (int i = 1620; i < 1637; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 火墙
                    case 23:
                        for (int i = 1650; i < 1680; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 爆裂火焰
                    case 24:
                        for (int i = 1680; i < 1690; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 地狱雷光
                    case 31:
                        for (int i = 3880; i < 3893; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 魔法盾
                    case 32:
                        for (int i = 3920; i < 3946; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 圣言术
                    case 33:
                        for (int i = 3840; i < 3870; i++)
                        {
                            path = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                            placementsPath = magicPlacementsPath + i.ToString().PadLeft(6, '0') + ".txt";
                            posList = FileHelper.ReadTxtReturnList(placementsPath);
                            list.Add(path + "," + string.Join(",", posList));
                        }
                        break;// 冰咆哮
                    #endregion


                    default: break;
                }

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 提升神技 获取称号
        /// </summary>
        public static string GetAcquiringSkills(UserInfo user, string magicName, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                string msg = "";
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                UserSkill us;
                switch (magicName)
                {
                    case "领取大彻大悟":
                        us = user.User_Skill.Where(u => u.US_Name == "毒雾献祭" || u.US_Name == "天雷尸爆" || u.US_Name == "亡灵归来").FirstOrDefault();
                        if (us == null) throw new Exception("土贼,技能都没满级,你还领取个球!发送 提升神技 先提升一下专属技能吧。");
                        if (us.US_Level != 3) throw new Exception("土贼,技能都没满级,你还领取个球!发送 提升神技 先提升一下专属技能吧。");
                        var title = user.User_TitleAttribute.Where(u => u.TitleName == "大彻大悟").FirstOrDefault();
                        if (title != null) throw new Exception("土贼,你已经领取了大彻大悟称号,你在搞什么鬼?");
                        UserTitleAttribute userTitle = new UserTitleAttribute
                        {
                            LevelType = 3,
                            TitleName = "大彻大悟",
                            FatalBlow = 10,
                            FatalInjury = 10,
                            AttackPR = 10,
                            AttackSpeed = 10,
                            DamageRebound = 10,
                            DouBPower = 10,
                            EquBurstRate = 10,
                            ExcellentBlow = 10,
                            Heals = 10,
                            HealsPR = 10,
                            HeartfeltBlow = 10,
                            HPIncrease = 10,
                            IgnoreAC = 10,
                            MagicAC = 10,
                            MagicDamageReduc = 10,
                            MagicDamageReflex = 10,
                            MagicEvas = 10,
                            MPIncrease = 10,
                            PhysicalDamageReduc = 10,
                            PhysicalDamageReflex = 10,
                            MonExpRate = 10,
                            UserExpRate = 10,
                            StdItemMap = user.Map,
                            StdItemOwner = user.Name,
                            StdItemStuff = "NPC制造",
                            StdItemTime = DateTime.Now.ToString()
                        };
                        userInI.Write("个人称号", "称号3", Api.JsonSerialize(userTitle));
                        msg = $"恭喜,玩家[{user.Name}]领取了称号[{userTitle.TitleName}],真是太强了,神技大陆一定有你实力展现的舞台。";
                        break;
                    case "提升神技":
                        string usStr = userInI.Read("个人技能", "技能42");
                        int magId = 0;
                        string US_Img = "", magName = "";
                        if (string.IsNullOrEmpty(usStr))
                        {
                            user.User_General.JB -= 30000000;
                            if (user.User_General.JB < 0) throw new Exception("臭小子,没钱你来我这吆喝什么,快去钱赚钱去吧!");
                            if (user.User_General.Lv < 42) throw new Exception("噢哟,没有42级你来凑什么热闹?哪里凉快哪里躺着去吧!");

                            switch (user.Job)
                            {
                                case "战士": magId = 35; magName = "毒雾献祭"; US_Img = "000928.png"; break;
                                case "法师": magId = 36; magName = "天雷尸爆"; US_Img = "000930.png"; break;
                                case "道士": magId = 37; magName = "亡灵归来"; US_Img = "000922.png"; break;
                            }
                            us = new UserSkill()
                            {
                                US_MagID = magId,
                                US_Name = magName,
                                US_Img = US_Img,
                                US_IsEnableSkill = "是"
                            };
                        }
                        else
                        {
                            us = user.User_Skill.Where(u => u.US_Name == "毒雾献祭" || u.US_Name == "天雷尸爆" || u.US_Name == "亡灵归来").First();
                            switch (us.US_Level)
                            {
                                case 1: user.User_General.JB -= 50000000; if (user.User_General.Lv < 45) throw new Exception("噢哟,没有45级你来凑什么热闹?哪里凉快哪里躺着去吧!"); break;// 1级升2级 
                                case 2: user.User_General.JB -= 80000000; if (user.User_General.Lv < 48) throw new Exception("噢哟,没有48级你来凑什么热闹?哪里凉快哪里躺着去吧!"); break;// 2级升3级
                                case 3: throw new Exception("土贼,技能已满级,你还提升个球!");
                            }
                            if (user.User_General.JB < 0) throw new Exception("臭小子,没钱你来我这吆喝什么,快去赚钱去吧!");
                        }

                        us.US_Level += 1;

                        userInI.Write("普通数据", "金币", user.User_General.JB.ToString());
                        userInI.Write("个人技能", "技能42", Api.JsonSerialize(us));
                        msg = $"恭喜,玩家[{user.Name}]升级了技能[{us.US_Name}],释放其它技能都会有加成,真是居家旅行必备神技啊!";
                        break;
                }

                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
