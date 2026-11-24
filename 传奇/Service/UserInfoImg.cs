using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using ImageMagick;
using 传奇.core;
using 传奇.Properties;

namespace 传奇
{
    public class UserInfoImg
    {
        #region 成语变量
        Random rnd = new Random();
        string UserInfoImgPath = LegendFilePath.imgPath;
        string LegendIMGDataPath = LegendFilePath.LegendIMGDataPath;
        string UserPath = LegendFilePath.UserPath;

        string humPath = string.Empty;
        string hum2Path = string.Empty;
        string hum3Path = string.Empty;
        string hum100Path = string.Empty;
        string chrSelPath = string.Empty;
        string newopUIPath = string.Empty;
        string ui1Path = string.Empty;
        string itemsPath = string.Empty;
        string stateitemPath = string.Empty;
        string prgusePath = string.Empty;
        string prguse3Path = string.Empty;
        string weaponPath = string.Empty;
        string weapon2Path = string.Empty;
        string weapon3Path = string.Empty;
        string weapon4Path = string.Empty;
        string weapon5Path = string.Empty;
        string weapon6Path = string.Empty;
        string weapon7Path = string.Empty;
        string weapon8Path = string.Empty;
        string weapon100Path = string.Empty;
        string dnItemsPath = string.Empty;
        string mapPath = string.Empty;
        string mapMonInfo = string.Empty;
        string magIcon2Path = string.Empty;
        string fenghaoBtnPath = string.Empty;

        List<StditemDB> stdDBList = new List<StditemDB>();

        List<string> WarriorList1 = new List<string>();//男战图片集合
        List<string> WizardList1 = new List<string>();//男法图片集合
        List<string> TaoistList1 = new List<string>();//道士列表

        List<string> WarriorList2 = new List<string>();//女战图片集合
        List<string> WizardList2 = new List<string>();//女法图片集合
        List<string> TaoistList2 = new List<string>();//道士列表

        Font font8 = new Font("黑体", 8);
        Font font9 = new Font("黑体", 9);
        Font font10 = new Font("黑体", 10);
        Font font12 = new Font("黑体", 12);
        Font font13 = new Font("黑体", 13);
        Font font14 = new Font("黑体", 14);
        Font font15 = new Font("黑体", 15);
        Font font16 = new Font("黑体", 16);
        Font font18 = new Font("黑体", 18);
        Font font20 = new Font("黑体", 20);


        Dictionary<int, string> HpImgDic = new Dictionary<int, string>();
        #endregion

        public UserInfoImg(UserInfo user)
        {
            humPath = LegendIMGDataPath + "Hum\\";
            hum2Path = LegendIMGDataPath + "hum2\\";
            hum3Path = LegendIMGDataPath + "hum3\\";
            hum100Path = LegendIMGDataPath + "Hum100\\";
            newopUIPath = LegendIMGDataPath + "NewopUI\\";
            itemsPath = LegendIMGDataPath + "Items\\";// 装备在背包的样子
            dnItemsPath = LegendIMGDataPath + "DnItems\\";// 装备在地上的样子
            stateitemPath = LegendIMGDataPath + "stateitem\\";// 装备在身上的样子
            prgusePath = LegendIMGDataPath + "Prguse\\";
            prguse3Path = LegendIMGDataPath + "Prguse3\\";
            weaponPath = LegendIMGDataPath + "Weapon\\";
            weapon2Path = LegendIMGDataPath + "Weapon2\\";
            weapon3Path = LegendIMGDataPath + "Weapon3\\";
            weapon4Path = LegendIMGDataPath + "Weapon4\\";
            weapon5Path = LegendIMGDataPath + "Weapon5\\";
            weapon6Path = LegendIMGDataPath + "Weapon6\\";
            weapon7Path = LegendIMGDataPath + "Weapon7\\";
            weapon8Path = LegendIMGDataPath + "Weapon8\\";
            weapon100Path = LegendIMGDataPath + "Weapon100\\";
            chrSelPath = LegendIMGDataPath + "ChrSel\\";
            ui1Path = LegendIMGDataPath + "ui1\\";
            magIcon2Path = LegendIMGDataPath + "MagIcon2\\";
            fenghaoBtnPath = LegendIMGDataPath + "封号按钮\\";

            mapPath = LegendFilePath.MapPath + "MapInfo.txt";
            mapMonInfo = LegendFilePath.MapPath + "MapMonInfo.txt";

            if (stdDBList.Count <= 0)
            {
                stdDBList = LegendApi.GetStdItemsList();
            }

            HpImgDic.Add(0, "000050.png");
            HpImgDic.Add(1, "000051.png");
            HpImgDic.Add(2, "000052.png");
            HpImgDic.Add(3, "000053.png");
            HpImgDic.Add(4, "000054.png");
            HpImgDic.Add(5, "000055.png");
            HpImgDic.Add(6, "000056.png");
            HpImgDic.Add(7, "000057.png");
            HpImgDic.Add(8, "000058.png");
            HpImgDic.Add(9, "000059.png");

            if (WarriorList1.Count <= 0)
                WarriorList1 = GetWaList1();//男战图片集合
            if (WizardList1.Count <= 0)
                WizardList1 = GetWiList1();//男法图片集合
            if (TaoistList1.Count <= 0)
                TaoistList1 = GetTaList1();//男道图片列表
            if (WarriorList2.Count <= 0)
                WarriorList2 = GetWaList2();//女战图片集合
            if (WizardList2.Count <= 0)
                WizardList2 = GetWiList2();//女法图片集合
            if (TaoistList2.Count <= 0)
                TaoistList2 = GetTaList2();//女道图片列表
        }

        /// <summary>
        /// 取外观武器
        /// </summary>
        /// <param name="iniFileHelper"></param>
        /// <param name="sex"></param>
        /// <param name="direction">朝向(1=上 2=右上 3=右 4=右下 5=下 6=左下 7=左 8=左上)</param>
        /// <param name="shapeCode"></param>
        /// <returns></returns>
        public List<string> GetPersonWeapon(IniFileHelper iniFileHelper, UserInfo user, int direction = 3, int shapeCode = 0)
        {
            List<string> res = new List<string>();
            List<string> weaponPos = new List<string>();

            string wqKey = iniFileHelper.Read("装备物品", "武器");//取武器
            if (!string.IsNullOrEmpty(wqKey) && wqKey != "0") // 根据不同的武器选择不同的外观
            {
                string std = iniFileHelper.ReadValue("装备属性", wqKey);//取武器
                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);
                if (shapeCode > 0) stditemDB.Shape = shapeCode;

                int shape = stditemDB.Shape * 1200;
                int newShape = 0;
                if (user.Sex == "女") shape = shape + 600;

                switch (direction)
                {
                    case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) newShape = shape + 408; else newShape = shape + 216; break;
                    case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) newShape = shape + 440; else newShape = shape + 248; break;
                }

                int startIndex = newShape;
                for (int i = startIndex; i < startIndex + 6; i++)
                {
                    res.Add(i.ToString().PadLeft(6, '0') + ".png");
                }
                res.Add(shape.ToString().PadLeft(6, '0') + ".png");

                for (int i = 0; i < res.Count; i++)
                {
                    weaponPos = FileHelper.ReadTxtReturnList(weaponPath + "Placements\\" + res[i].Replace(".png", ".txt"));
                    res[i] = res[i] + "," + string.Join(",", weaponPos);
                }
            }
            return res;
        }

        /// <summary>
        /// 取外观武器
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="iniFileHelper"></param>
        /// <returns></returns>
        public List<string> GetPersonWeaponPicture(IniFileHelper iniFileHelper)
        {
            List<string> list = new List<string>();

            string wqKey = iniFileHelper.Read("装备物品", "武器");//取武器

            if (!string.IsNullOrEmpty(wqKey) && wqKey != "0") // 根据不同的武器选择不同的外观
            {
                string std = iniFileHelper.ReadValue("装备属性", wqKey);//取武器
                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);

                int shape = 0;
                if (stditemDB.Shape < 100)
                    shape = stditemDB.Shape * 1200;
                else if (stditemDB.Shape >= 100 && stditemDB.Shape <= 149)
                    shape = (stditemDB.Shape - 100) * 1200;
                else if (stditemDB.Shape >= 150 && stditemDB.Shape <= 199)
                    shape = (stditemDB.Shape - 150) * 1200;
                else if (stditemDB.Shape >= 200 && stditemDB.Shape <= 249)
                    shape = (stditemDB.Shape - 200) * 1200;
                else if (stditemDB.Shape >= 250 && stditemDB.Shape <= 299)
                    shape = (stditemDB.Shape - 250) * 1200;
                else if (stditemDB.Shape >= 300 && stditemDB.Shape <= 349)
                    shape = (stditemDB.Shape - 300) * 1200;
                else if (stditemDB.Shape >= 350 && stditemDB.Shape <= 399)
                    shape = (stditemDB.Shape - 350) * 1200;
                else if (stditemDB.Shape >= 400 && stditemDB.Shape <= 449)
                    shape = (stditemDB.Shape - 400) * 1200;
                else
                    shape = stditemDB.Shape;

                List<string> weaponPos = new List<string>();
                int n = 0;
                string nWeapon = "";
                do
                {
                    if (stditemDB.Shape < 100)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weaponPath + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weaponPath + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},0");
                    }
                    else if (stditemDB.Shape >= 100 && stditemDB.Shape <= 149)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon2Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon2Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (stditemDB.Shape >= 150 && stditemDB.Shape <= 199)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon3Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon3Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (stditemDB.Shape >= 200 && stditemDB.Shape <= 249)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon4Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon4Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (stditemDB.Shape >= 250 && stditemDB.Shape <= 299)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon5Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon5Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (stditemDB.Shape >= 300 && stditemDB.Shape <= 349)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon6Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon6Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (stditemDB.Shape >= 350 && stditemDB.Shape <= 399)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon7Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon7Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (stditemDB.Shape >= 400 && stditemDB.Shape <= 449)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon8Path + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon8Path + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else
                    {
                        if (shape > 1000)
                        {
                            nWeapon = weapon100Path + shape + "\\";
                        }
                        shape = shape >= 1000 ? 0 : shape;
                        weaponPos = FileHelper.ReadTxtReturnList(nWeapon + "Placements\\" + shape.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(nWeapon + shape.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }

                    shape += 8;
                    n++;
                } while (n < 8);
            }
            return list;
        }

        /// <summary>
        /// 取攻击时的外观(含衣服)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="iniFileHelper"></param>
        /// <param name="direction">朝向(1=上 2=右上 3=右 4=右下 5=下 6=左下 7=左 8=左上)</param>
        /// <returns></returns>
        public List<string> GetPlayerAttackPicture(UserInfo user, IniFileHelper iniFileHelper, int looks = 0, int direction = 3)
        {
            List<string> list = new List<string>();
            List<int> forList = new List<int>();
            string yfKey = user.User_Items.DRESS;//取衣服
            int num = 0, lastNum = 0;
            if (yfKey != "0" && !string.IsNullOrEmpty(yfKey)) //取没有穿衣服的平砍和放技能图片
            {// 根据不同的衣服选择不同的外观
                string std = iniFileHelper.ReadValue("装备属性", yfKey);//取衣服
                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);
                if (looks > 0)// 指定衣服样式
                    stditemDB.Looks = looks;

                if (user.Sex == "男")
                {
                    switch (stditemDB.Looks)// 判断外观
                    {
                        case 80:
                        case 60://布衣
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 1608; else num = 1416; lastNum = 1216; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 1640; else num = 1448; lastNum = 1248; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 81:
                        case 61:// 轻型盔甲 - 中型盔甲
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 2808; else num = 2616; lastNum = 2416; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 2840; else num = 2648; lastNum = 2448; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 82:
                        case 62://  重盔甲 - 战神盔甲
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 4008; else num = 3816; lastNum = 3616; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 4040; else num = 3848; lastNum = 3648; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 83:
                        case 63:// 魔法长袍 - 恶魔长袍
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 5208; else num = 5016; lastNum = 4816; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 5240; else num = 5048; lastNum = 4848; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 84:
                        case 64:// 灵魂战衣 - 幽灵战衣
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 6408; else num = 6216; lastNum = 6016; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 6440; else num = 6248; lastNum = 6048; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 88:
                        case 85:// 天魔神甲
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 7608; else num = 7416; lastNum = 7216; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 7640; else num = 7448; lastNum = 7248; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 89:
                        case 86:// 法神披风
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 8808; else num = 8616; lastNum = 8416; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 8840; else num = 8648; lastNum = 8448; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 90:
                        case 87:// 天尊道袍
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 10008; else num = 9816; lastNum = 9616; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 10040; else num = 9848; lastNum = 9648; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                    }
                }
                else
                {
                    switch (stditemDB.Looks)// 判断外观
                    {
                        case 60:
                        case 80://布衣
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 2208; else num = 2016; lastNum = 1816; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 2240; else num = 2048; lastNum = 1848; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 61:
                        case 81:// 轻型盔甲 - 中型盔甲
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 3408; else num = 3216; lastNum = 3016; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 3440; else num = 3248; lastNum = 3048; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 62:
                        case 82://  重盔甲 - 战神盔甲
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 4608; else num = 4416; lastNum = 4216; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 4640; else num = 4448; lastNum = 4248; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 63:
                        case 83:// 魔法长袍 - 恶魔长袍
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 5808; else num = 5616; lastNum = 5416; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 5840; else num = 5648; lastNum = 5448; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 64:
                        case 84:// 灵魂战衣 - 幽灵战衣
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 7008; else num = 6816; lastNum = 6616; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 7040; else num = 6848; lastNum = 6648; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 85:
                        case 88:// 天魔神甲
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 8208; else num = 8016; lastNum = 7816; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 8240; else num = 8048; lastNum = 7848; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 86:
                        case 89:// 法神披风
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 9408; else num = 9216; lastNum = 9016; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 9440; else num = 9248; lastNum = 9048; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                        case 87:
                        case 90:// 天尊道袍
                            switch (direction)
                            {
                                case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 10608; else num = 10416; lastNum = 10216; break;//朝右 最后取一张站立不动的图片
                                case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 10640; else num = 10448; lastNum = 10248; break;//朝左 最后取一张站立不动的图片
                            }
                            break;
                    }
                }
            }
            else
            {
                if (user.Sex == "男")
                {
                    switch (direction)
                    {
                        case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 408; else num = 216; lastNum = 16; break;//朝右 最后取一张站立不动的图片
                        case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 440; else num = 248; lastNum = 48; break;//朝左 最后取一张站立不动的图片
                    }
                }
                else
                {
                    switch (direction)
                    {
                        case 3: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 1008; else num = 816; lastNum = 616; break;//朝右 最后取一张站立不动的图片
                        case 7: if (user.IsUseSkills == "是" && user.Job == "法师" && (user.Job == "道士" && user.User_General.Lv >= 19)) num = 1040; else num = 848; lastNum = 648; break;//朝左 最后取一张站立不动的图片
                    }
                }
            }

            for (int i = num; i < num + 6; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0') + ".png");
            }
            list.Add(lastNum.ToString().PadLeft(6, '0') + ".png");

            List<string> humPos = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                humPos = FileHelper.ReadTxtReturnList(humPath + "Placements\\" + list[i].Replace("png", "txt"));
                list[i] = $"{list[i]},{string.Join(",", humPos)}";
            }
            return list;
        }

        /// <summary>
        /// 取外观衣服
        /// </summary>
        public List<string> GetPersonStandPicture(string qq, string sex, string dress)
        {
            List<string> list = new List<string>();
            List<int> forList = new List<int>();
            string humDressPath = humPath;

            if (!string.IsNullOrEmpty(dress) && dress != "0") // 根据不同的衣服选择不同的外观
            {
                string uPath = LegendFilePath.UserPath + qq + ".ini";
                IniFileHelper userINI = new IniFileHelper(uPath);
                string std = userINI.ReadValue("装备属性", dress);//取衣服
                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);

                //if (stditemDB.Looks > 7484) stditemDB.Looks -= 7484;

                if (stditemDB != null)
                {
                    switch (stditemDB.Shape)// 判断外观
                    {
                        case 1:
                            if (sex == "男")
                            {
                                forList.Add(1200);
                                forList.Add(1208);
                                forList.Add(1216);
                                forList.Add(1224);
                                forList.Add(1232);
                                forList.Add(1240);
                                forList.Add(1248);
                                forList.Add(1256);
                            }
                            else
                            {
                                forList.Add(1800);
                                forList.Add(1808);
                                forList.Add(1816);
                                forList.Add(1824);
                                forList.Add(1832);
                                forList.Add(1840);
                                forList.Add(1848);
                                forList.Add(1856);
                            }
                            break; // 布衣
                        case 2:
                            if (sex == "男")
                            {
                                forList.Add(2400);
                                forList.Add(2408);
                                forList.Add(2416);
                                forList.Add(2424);
                                forList.Add(2432);
                                forList.Add(2440);
                                forList.Add(2448);
                                forList.Add(2456);
                            }
                            else
                            {
                                forList.Add(3000);
                                forList.Add(3008);
                                forList.Add(3016);
                                forList.Add(3024);
                                forList.Add(3032);
                                forList.Add(3040);
                                forList.Add(3048);
                                forList.Add(3056);
                            }

                            break; // 轻型盔甲 - 中型盔甲
                        case 3:
                            if (sex == "男")
                            {
                                forList.Add(3600);
                                forList.Add(3608);
                                forList.Add(3616);
                                forList.Add(3624);
                                forList.Add(3632);
                                forList.Add(3640);
                                forList.Add(3648);
                                forList.Add(3656);
                            }
                            else
                            {
                                forList.Add(4200);
                                forList.Add(4208);
                                forList.Add(4216);
                                forList.Add(4224);
                                forList.Add(4232);
                                forList.Add(4240);
                                forList.Add(4248);
                                forList.Add(4256);
                            }
                            break; // 重盔甲 - 战神盔甲
                        case 4:
                            if (sex == "男")
                            {
                                forList.Add(4800);
                                forList.Add(4808);
                                forList.Add(4816);
                                forList.Add(4824);
                                forList.Add(4832);
                                forList.Add(4840);
                                forList.Add(4848);
                                forList.Add(4856);
                            }
                            else
                            {
                                forList.Add(5400);
                                forList.Add(5408);
                                forList.Add(5416);
                                forList.Add(5424);
                                forList.Add(5432);
                                forList.Add(5440);
                                forList.Add(5448);
                                forList.Add(5456);
                            }
                            break; // 魔法长袍 - 恶魔长袍
                        case 5:
                            if (sex == "男")
                            {
                                forList.Add(6000);
                                forList.Add(6008);
                                forList.Add(6016);
                                forList.Add(6024);
                                forList.Add(6032);
                                forList.Add(6040);
                                forList.Add(6048);
                                forList.Add(6056);
                            }
                            else
                            {
                                forList.Add(6600);
                                forList.Add(6608);
                                forList.Add(6616);
                                forList.Add(6624);
                                forList.Add(6632);
                                forList.Add(6640);
                                forList.Add(6648);
                                forList.Add(6656);
                            }
                            break; // 灵魂战衣 - 幽灵战衣
                        case 6:
                            if (sex == "男")
                            {
                                forList.Add(7200);
                                forList.Add(7208);
                                forList.Add(7216);
                                forList.Add(7224);
                                forList.Add(7232);
                                forList.Add(7240);
                                forList.Add(7248);
                                forList.Add(7256);
                            }
                            else
                            {
                                forList.Add(7800);
                                forList.Add(7808);
                                forList.Add(7816);
                                forList.Add(7824);
                                forList.Add(7832);
                                forList.Add(7840);
                                forList.Add(7848);
                                forList.Add(7856);
                            }
                            break; // 天魔神甲
                        case 7:
                            if (sex == "男")
                            {
                                forList.Add(8400);
                                forList.Add(8408);
                                forList.Add(8416);
                                forList.Add(8424);
                                forList.Add(8432);
                                forList.Add(8440);
                                forList.Add(8448);
                                forList.Add(8456);
                            }
                            else
                            {
                                forList.Add(9000);
                                forList.Add(9008);
                                forList.Add(9016);
                                forList.Add(9024);
                                forList.Add(9032);
                                forList.Add(9040);
                                forList.Add(9048);
                                forList.Add(9056);
                            }
                            break; // 法神披风
                        case 8:
                            if (sex == "男")
                            {
                                forList.Add(9600);
                                forList.Add(9608);
                                forList.Add(9616);
                                forList.Add(9624);
                                forList.Add(9632);
                                forList.Add(9640);
                                forList.Add(9648);
                                forList.Add(9656);
                            }
                            else
                            {
                                forList.Add(10200);
                                forList.Add(10208);
                                forList.Add(10216);
                                forList.Add(10224);
                                forList.Add(10232);
                                forList.Add(10240);
                                forList.Add(10248);
                                forList.Add(10256);
                            }
                            break; // 天尊道袍

                        case 1000:
                            if (sex == "男")
                            {
                                forList.Add(0);
                                forList.Add(8);
                                forList.Add(16);
                                forList.Add(24);
                                forList.Add(32);
                                forList.Add(40);
                                forList.Add(48);
                                forList.Add(56);
                            }
                            else
                            {
                                forList.Add(600);
                                forList.Add(608);
                                forList.Add(616);
                                forList.Add(624);
                                forList.Add(632);
                                forList.Add(640);
                                forList.Add(648);
                                forList.Add(656);
                            }
                            humDressPath = hum100Path + "1000\\"; break; // 白虎战甲
                        case 1001:
                            if (sex == "男")
                            {
                                forList.Add(0);
                                forList.Add(8);
                                forList.Add(16);
                                forList.Add(24);
                                forList.Add(32);
                                forList.Add(40);
                                forList.Add(48);
                                forList.Add(56);
                            }
                            else
                            {
                                forList.Add(600);
                                forList.Add(608);
                                forList.Add(616);
                                forList.Add(624);
                                forList.Add(632);
                                forList.Add(640);
                                forList.Add(648);
                                forList.Add(656);
                            }
                            humDressPath = hum100Path + "1001\\"; break; // 朱雀神袍
                        case 1002:
                            if (sex == "男")
                            {
                                forList.Add(0);
                                forList.Add(8);
                                forList.Add(16);
                                forList.Add(24);
                                forList.Add(32);
                                forList.Add(40);
                                forList.Add(48);
                                forList.Add(56);
                            }
                            else
                            {
                                forList.Add(600);
                                forList.Add(608);
                                forList.Add(616);
                                forList.Add(624);
                                forList.Add(632);
                                forList.Add(640);
                                forList.Add(648);
                                forList.Add(656);
                            }
                            humDressPath = hum100Path + "1002\\"; break; // 青龙道袍
                        case 1003:
                            if (sex == "男")
                            {
                                forList.Add(0);
                                forList.Add(8);
                                forList.Add(16);
                                forList.Add(24);
                                forList.Add(32);
                                forList.Add(40);
                                forList.Add(48);
                                forList.Add(56);
                            }
                            else
                            {
                                forList.Add(600);
                                forList.Add(608);
                                forList.Add(616);
                                forList.Add(624);
                                forList.Add(632);
                                forList.Add(640);
                                forList.Add(648);
                                forList.Add(656);
                            }
                            humDressPath = hum100Path + "1003\\"; break; // 刑天战甲
                        case 1004:
                            if (sex == "男")
                            {
                                forList.Add(0);
                                forList.Add(8);
                                forList.Add(16);
                                forList.Add(24);
                                forList.Add(32);
                                forList.Add(40);
                                forList.Add(48);
                                forList.Add(56);
                            }
                            else
                            {
                                forList.Add(600);
                                forList.Add(608);
                                forList.Add(616);
                                forList.Add(624);
                                forList.Add(632);
                                forList.Add(640);
                                forList.Add(648);
                                forList.Add(656);
                            }
                            humDressPath = hum100Path + "1004\\"; break; // 焚天道甲
                        case 1005:
                            if (sex == "男")
                            {
                                forList.Add(0);
                                forList.Add(8);
                                forList.Add(16);
                                forList.Add(24);
                                forList.Add(32);
                                forList.Add(40);
                                forList.Add(48);
                                forList.Add(56);
                            }
                            else
                            {
                                forList.Add(600);
                                forList.Add(608);
                                forList.Add(616);
                                forList.Add(624);
                                forList.Add(632);
                                forList.Add(640);
                                forList.Add(648);
                                forList.Add(656);
                            }
                            humDressPath = hum100Path + "1005\\"; break; // 沧澜魔甲

                        case 106:
                            if (sex == "男")
                            {
                                forList.Add(9600);
                                forList.Add(9608);
                                forList.Add(9616);
                                forList.Add(9624);
                                forList.Add(9632);
                                forList.Add(9640);
                                forList.Add(9648);
                                forList.Add(9656);
                            }
                            else
                            {
                                forList.Add(10200);
                                forList.Add(10208);
                                forList.Add(10216);
                                forList.Add(10224);
                                forList.Add(10232);
                                forList.Add(10240);
                                forList.Add(10248);
                                forList.Add(10256);
                            }
                            humDressPath = hum3Path;
                            break; // 毁灭战甲
                    }
                }
            }
            else
            {
                switch (sex)// 没有穿衣服的样子
                {
                    case "男":
                        forList.Add(0);
                        forList.Add(8);
                        forList.Add(16);
                        forList.Add(24);
                        forList.Add(32);
                        forList.Add(40);
                        forList.Add(48);
                        forList.Add(56);
                        break;
                    case "女":
                        forList.Add(600);
                        forList.Add(608);
                        forList.Add(616);
                        forList.Add(624);
                        forList.Add(632);
                        forList.Add(640);
                        forList.Add(648);
                        forList.Add(656);
                        break;
                    default: break;
                }
            }
            List<string> humPos = new List<string>();

            foreach (int fl in forList)
            {
                string hdpath = humDressPath + "Placements\\" + fl.ToString().PadLeft(6, '0') + ".txt";
                humPos = FileHelper.ReadTxtReturnList(hdpath);
                list.Add($"{humDressPath + fl.ToString().PadLeft(6, '0')}.png,{string.Join(",", humPos)}");
            }

            return list;
        }

        /// <summary>
        /// 生成注册图片
        /// </summary>
        public string GetRegisterImg(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true);
                string monPath = "";
                using (Image imgMian = Image.FromFile(path))
                {
                    //加载主窗体背景图
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(385, 216, 120, 15);

                            #region 添加附近怪物
                            string mapStr = FileHelper.ReadTxt(mapPath);
                            var mapInfoList = Api.JsonDeserialize<List<LengendMapInfo>>(mapStr);
                            var mapInfo = mapInfoList.Where(mp => mp.LMI_MapCode == user.MapCode).FirstOrDefault();

                            string mapMonStr = FileHelper.ReadTxt(mapMonInfo);
                            var legMapMons = Api.JsonDeserialize<List<LegMapMonInfo>>(mapMonStr);
                            legMapMons = legMapMons.Where(t => t.LMI_ID == mapInfo.LMI_ID).ToList();

                            int nd = rnd.Next(1, legMapMons.Count);

                            for (int i = 0; i < nd; i++)
                            {
                                int nnd = rnd.Next(1, 3);
                                for (int j = 0; j < nnd; j++)
                                {
                                    var listMonStand = MonsterApi.GetMonsterStandIMG(legMapMons[i].MonID);
                                    int nnnd = rnd.Next(1, listMonStand.Count);
                                    monPath = listMonStand[nnnd];

                                    int lmmx = int.Parse(monPath.Split(',')[1]) + rnd.Next(1, 1000);
                                    int lmmy = int.Parse(monPath.Split(',')[2]) + rnd.Next(1, 500);
                                    using (Image img = Image.FromFile(monPath.Split(',')[0]))// 刷怪
                                    {
                                        gh.DrawImage(img, lmmx, lmmy, img.Width, img.Height);
                                    }

                                    using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 血条
                                    {
                                        gh.DrawImage(img, lmmx + 5, lmmy - 15, img.Width, img.Height);
                                    }
                                    switch (legMapMons[i].MonID)
                                    {
                                        case 39:
                                        case 40: rect = new Rectangle(lmmx + 15, lmmy + 30, 60, 15); break;
                                        default: rect = new Rectangle(lmmx + 15, lmmy + 10, 60, 15); break;
                                    }

                                    brush = new SolidBrush(Color.YellowGreen);
                                    TextHelper.DrawLeftString(gh, legMapMons[i].MonName, font9, brush, rect);//怪物名 
                                }
                            }
                            #endregion

                            monPath = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(monPath, ImageFormat.Png);//保存图片
                        }
                    }
                }

                return monPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看称号
        /// </summary>
        public string GetUserTitleIMG(UserInfo user, int num, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, false, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成称号图
                            using (Image img = Image.FromFile(newopUIPath + "000094.png"))
                            {
                                gh.DrawImage(img, 680, 0, img.Width, img.Height);
                            }
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            SolidBrush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(850, 70, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Name, font15, brush, rect);//用户名
                            #endregion

                            UserTitleAttribute title = null;
                            int posX = 743, posY = 132, index = num - 1;

                            for (int i = 0; i < user.User_TitleAttribute.Count; i++)
                            {
                                title = user.User_TitleAttribute[i];
                                switch (title.LevelType)
                                {
                                    case 1:
                                        string titlePath = "";
                                        switch (title.Level)
                                        {
                                            case 1: titlePath = ui1Path + "000692.png"; break;
                                            case 2: titlePath = ui1Path + "000696.png"; break;
                                            case 3: titlePath = ui1Path + "000702.png"; break;
                                            case 4: titlePath = ui1Path + "000706.png"; break;
                                            case 5: titlePath = ui1Path + "000712.png"; break;
                                            case 6: titlePath = ui1Path + "000716.png"; break;
                                            case 7: titlePath = ui1Path + "000722.png"; break;
                                            case 8: titlePath = ui1Path + "000726.png"; break;
                                            case 9: titlePath = ui1Path + "000732.png"; break;
                                            case 10: titlePath = ui1Path + "000736.png"; break;
                                            default: break;
                                        }
                                        using (Image img = Image.FromFile(titlePath))
                                        {
                                            gh.DrawImage(img, posX, posY + 38, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(posX + 62, posY + 52, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, title.TitleName, font14, brush, rect);
                                        break; // 永恒圣佛称号
                                    case 2:
                                        using (Image img = Image.FromFile(ui1Path + "003374.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(posX + 78, posY + 20, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, title.TitleName, font18, brush, rect);

                                        posX = 725;
                                        posY = 200;
                                        using (Image img = Image.FromFile(ui1Path + "003372.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(posX + 62, posY + 15, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, title.TitleName, font14, brush, rect);
                                        break; // 永恒鞭尸称号
                                    case 3:
                                        posX = 725;
                                        posY = 277;
                                        using (Image img = Image.FromFile(ui1Path + "003372.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(posX + 62, posY + 15, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, title.TitleName, font14, brush, rect);
                                        break;// 大彻大悟称号
                                    case 4:
                                        posX = 725;
                                        posY = 316;
                                        using (Image img = Image.FromFile(ui1Path + "003372.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(posX + 62, posY + 15, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, title.TitleName, font14, brush, rect);
                                        break;//永恒圣佛
                                    case 5:
                                        posX = 725;
                                        posY = 356;
                                        using (Image img = Image.FromFile(ui1Path + "003372.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(posX + 62, posY + 15, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, title.TitleName, font14, brush, rect);
                                        break;//走火入魔
                                    default: index = -1; break;
                                }
                            }
                            if (num <= user.User_TitleAttribute.Count)
                            {
                                #region 生成查看框-新
                                if (index >= 0)
                                {
                                    List<string> list = new List<string>();
                                    int boxWidth = 195, boxHeight = 160;
                                    var titleA = user.User_TitleAttribute[index];
                                    string newpath = "";
                                    switch (index)
                                    {
                                        case 0:
                                            posX = 756;
                                            posY = 235;
                                            list.Add($"1#{titleA.TitleName}#{posX + 5}#{posY}#10#OrangeRed");
                                            posY += 18;
                                            list.Add($"1#[物品来源]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#物品来源:{titleA.StdItemStuff}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品所属:{titleA.StdItemOwner}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品地图:{titleA.StdItemMap}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品时间:{titleA.StdItemTime}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#卖给商店:1000#{posX + 5}#{posY}#10#Orange");
                                            posY += 18;
                                            newpath = newopUIPath + "001181.png";
                                            list.Add($"2#{newpath}#{posX + 8}#{posY}");
                                            posX += 18;
                                            list.Add($"1#杀死怪物有几率触发鞭尸{titleA.MonDeadCount}次#{posX}#{posY}#10#Aquamarine");
                                            posY += 18;
                                            posX -= 13;
                                            list.Add($"1#(冷却时间60秒)#{posX}#{posY}#10#LightGray");
                                            break;
                                        case 1:
                                            posX = 756;
                                            posY = 275;
                                            list.Add($"1#{titleA.TitleName}#{posX + 5}#{posY}#10#OrangeRed");
                                            posY += 18;
                                            list.Add($"1#[元素属性]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#怪物爆率:{titleA.MonExpRate}#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#[物品来源]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#物品来源:{titleA.StdItemStuff}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品所属:{titleA.StdItemOwner}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品地图:{titleA.StdItemMap}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品时间:{titleA.StdItemTime}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#卖给商店:1000#{posX + 5}#{posY}#10#Orange");
                                            break;
                                        case 2:
                                            posX = 756;
                                            posY = 225;
                                            list.Add($"1#{titleA.TitleName}#{posX + 5}#{posY}#10#OrangeRed");
                                            posY += 18;
                                            list.Add($"1#[元素属性]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#暴击: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#倍功: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#暴击几率: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔法躲避: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#攻击速度: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#物伤减免: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔伤减免: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#物伤反射: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔伤反射: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#攻击伤害: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔法防御: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#忽视防御: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#伤害反弹: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#人物爆率: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#体力增加: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔力增加: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#怪物爆率: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#防爆出率: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#致命伤害: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#致命一击: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#卓越一击: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#会心一击: +{titleA.MonExpRate}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#[物品来源]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#物品来源:{titleA.StdItemStuff}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品所属:{titleA.StdItemOwner}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品地图:{titleA.StdItemMap}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品时间:{titleA.StdItemTime}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#卖给商店:1000#{posX + 5}#{posY}#10#Orange");
                                            break;
                                        case 3:
                                            posX = 756;
                                            posY = 352;
                                            list.Add($"1#{titleA.TitleName}#{posX + 5}#{posY}#10#OrangeRed");
                                            posY += 18;
                                            list.Add($"1#[元素属性]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#HP: +{titleA.Hp}#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#体力增加: +{titleA.HPIncrease}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#物伤减免: +{titleA.PhysicalDamageReduc}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔伤减免: +{titleA.MagicDamageReduc}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#攻击伤害: +{titleA.AttackPR}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#[物品来源]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#物品来源:{titleA.StdItemStuff}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品所属:{titleA.StdItemOwner}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品地图:{titleA.StdItemMap}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品时间:{titleA.StdItemTime}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#卖给商店:500#{posX + 5}#{posY}#10#Orange");
                                            break;
                                        case 4:
                                            posX = 756;
                                            posY = 392;
                                            list.Add($"1#{titleA.TitleName}#{posX + 5}#{posY}#10#OrangeRed");
                                            posY += 18;
                                            list.Add($"1#[元素属性]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#HP: +{titleA.Hp}#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#体力增加: +{titleA.HPIncrease}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#物伤反射: +{titleA.PhysicalDamageReflex}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#魔伤反射: +{titleA.MagicDamageReflex}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#忽视防御: +{titleA.IgnoreAC}%#{posX + 5}#{posY}#10#Cyan");
                                            posY += 18;
                                            list.Add($"1#[物品来源]#{posX + 5}#{posY}#10#LightSkyBlue");
                                            posY += 18;
                                            list.Add($"1#物品来源:{titleA.StdItemStuff}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品所属:{titleA.StdItemOwner}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品地图:{titleA.StdItemMap}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#物品时间:{titleA.StdItemTime}#{posX + 5}#{posY}#10#Yellow");
                                            posY += 18;
                                            list.Add($"1#卖给商店:500#{posX + 5}#{posY}#10#Orange");
                                            break;
                                        default: break;
                                    }

                                    // 画框                   
                                    string first = list[0];
                                    string last = list[list.Count - 1];
                                    var arr = last.Split('#');
                                    var arr2 = first.Split('#');
                                    int boxH = int.Parse(arr[3]) - int.Parse(arr2[3]) + 25;
                                    int boxW = boxWidth;
                                    posX = int.Parse(arr2[2]) - 5;
                                    posY = int.Parse(arr2[3]) - 8;
                                    // 设置矩形的填充颜色为半透明（Alpha = 100）
                                    Color transparentColor = Color.FromArgb(235, 0, 0, 0); // 透明度为100
                                    SolidBrush transparentBrush = new SolidBrush(transparentColor);
                                    // 绘制一个矩形
                                    gh.FillRectangle(transparentBrush, posX, posY, boxW, boxH);

                                    newpath = newopUIPath + "000044.png";

                                    using (Image img = Image.FromFile(newpath))// 线-上
                                    {
                                        gh.DrawImage(img, posX, posY, boxW, img.Height);
                                    }
                                    using (Image img = Image.FromFile(newpath))// 线-下
                                    {
                                        gh.DrawImage(img, posX, posY + boxH - (img.Height / 2), boxW, img.Height);
                                    }
                                    newpath = newopUIPath + "000045.png";
                                    using (Image img = Image.FromFile(newpath))// 线-左
                                    {
                                        gh.DrawImage(img, posX, posY, img.Width, boxH);
                                    }
                                    using (Image img = Image.FromFile(newpath))// 线-右
                                    {
                                        gh.DrawImage(img, posX + boxW, posY, img.Width, boxH);
                                    }
                                    newpath = newopUIPath + "000042.png";
                                    using (Image img = Image.FromFile(newpath))// 左上角框框
                                    {
                                        gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                    }
                                    newpath = newopUIPath + "000043.png";
                                    using (Image img = Image.FromFile(newpath))// 左下角框框
                                    {
                                        gh.DrawImage(img, posX, posY + boxH - img.Width, img.Width, img.Height);
                                    }
                                    newpath = newopUIPath + "000040.png";
                                    using (Image img = Image.FromFile(newpath))// 右上角框框
                                    {
                                        gh.DrawImage(img, posX + boxW - (img.Height / 2) - 1, posY, img.Width, img.Height);
                                    }
                                    newpath = newopUIPath + "000041.png";
                                    using (Image img = Image.FromFile(newpath))// 右下角框框
                                    {
                                        gh.DrawImage(img, posX + boxW - (img.Height / 2) - 1, posY + boxH - img.Width + 1, img.Width, img.Height);
                                    }

                                    // 加载内容
                                    foreach (var item in list)
                                    {
                                        arr = item.Split('#');
                                        int type = int.Parse(arr[0]);

                                        switch (type)
                                        {
                                            case 1:
                                                posX = int.Parse(arr[2]);
                                                posY = int.Parse(arr[3]);
                                                rect = new Rectangle(posX, posY, boxWidth, 0);
                                                Font font;
                                                switch (arr[4])
                                                {
                                                    case "8": font = new Font("黑体", 8); break;
                                                    case "9": font = new Font("黑体", 9); break;
                                                    case "10": font = new Font("黑体", 10); break;
                                                    case "11": font = new Font("黑体", 11); break;
                                                    case "12": font = new Font("黑体", 12); break;
                                                    case "13": font = new Font("黑体", 13); break;
                                                    case "14": font = new Font("黑体", 14); break;
                                                    default: font = new Font("黑体", 10); break;
                                                }
                                                switch (arr[5])
                                                {
                                                    case "OrangeRed": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.OrangeRed, rect); break;
                                                    case "LightSkyBlue": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.LightSkyBlue, rect); break;
                                                    case "Yellow": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Yellow, rect); break;
                                                    case "Orange": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Orange, rect); break;
                                                    case "Aquamarine": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Aquamarine, rect); break;
                                                    case "LightGray": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.LightGray, rect); break;
                                                    case "Cyan": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Cyan, rect); break;
                                                    default: TextHelper.DrawLeftString(gh, arr[1], font, Brushes.White, rect); break;
                                                }

                                                break;
                                            case 2:
                                                posX = int.Parse(arr[2]);
                                                posY = int.Parse(arr[3]);
                                                using (Image img = Image.FromFile(arr[1]))
                                                {
                                                    gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                                }
                                                break;
                                        }
                                    }
                                }
                                #endregion
                            }

                        }
                        path = $"{UserInfoImgPath + user.UserQQ}.png";
                        bitmap.Save(path, ImageFormat.Png);//保存图片
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看时装
        /// </summary>
        public string GetUserFashionIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, false, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成时装图
                            if (user.Sex == "女")
                            {
                                using (Image img = Image.FromFile(newopUIPath + "000092.png"))// 时装
                                {
                                    gh.DrawImage(img, 680, 0, img.Width, img.Height);
                                }
                            }
                            else
                            {
                                using (Image img = Image.FromFile(newopUIPath + "000093.png"))// 时装
                                {
                                    gh.DrawImage(img, 680, 0, img.Width, img.Height);
                                }
                            }
                            #endregion

                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(850, 70, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Name, font15, brush, rect);//用户名

                            //查询出佩戴的装备,生成装备图片
                            path = UserPath + user.UserQQ + ".ini";
                            IniFileHelper iniFileHelper = new IniFileHelper(path);
                            List<string> listBwg = iniFileHelper.GetAllKeys("装备物品");

                            string zbbh = "", std, stdPosPath;
                            List<string> stdPos = new List<string>();
                            int stdX, stdY;
                            StditemDB stditemDB = null;
                            foreach (string key in listBwg)
                            {
                                zbbh = iniFileHelper.Read("装备物品", key);
                                if (!string.IsNullOrEmpty(zbbh) && zbbh != "0")
                                {
                                    std = iniFileHelper.ReadValue("装备属性", zbbh);
                                    stditemDB = Api.JsonDeserialize<StditemDB>(std);
                                    if (stditemDB.Looks > 384)
                                        stditemDB.Looks = stditemDB.Looks - 7484 + 384;
                                    string stdUIPath = stateitemPath + stditemDB.Looks.ToString().PadLeft(6, '0') + ".png";
                                    stdPosPath = stateitemPath + "Placements\\" + stditemDB.Looks.ToString().PadLeft(6, '0') + ".txt";
                                    stdPos = FileHelper.ReadTxtReturnList(stdPosPath);

                                    switch (key)
                                    {
                                        case "时装衣服":
                                        case "时装武器":
                                            stdX = int.Parse(stdPos[0]) + 765;
                                            stdY = int.Parse(stdPos[1]) + 205;
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装右戒指":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 730;
                                                stdY = stdY - (img.Height / 2) + 406;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装左戒指":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 972;
                                                stdY = stdY - (img.Height / 2) + 406;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装右手镯":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 730;
                                                stdY = stdY - (img.Height / 2) + 364;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装左手镯":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 972;
                                                stdY = stdY - (img.Height / 2) + 364;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装项链":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 972;
                                                stdY = stdY - (img.Height / 2) + 320;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装头盔":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 853;
                                                stdY = stdY - (img.Height / 2) + 255;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装护身符":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 738;
                                                stdY = stdY - (img.Height / 2) + 450;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "时装宝石":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 875;
                                                stdY = stdY - (img.Height / 2) + 450;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }


                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看技能
        /// </summary>
        public string GetUserSkillIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, false, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成技能图                       
                            using (Image img = Image.FromFile(newopUIPath + "000091.png"))// 技能
                            {
                                gh.DrawImage(img, 680, 0, img.Width, img.Height);
                            }

                            Color color = ColorTranslator.FromHtml("#ffffff");
                            SolidBrush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(850, 70, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Name, font15, brush, rect);//用户名

                            List<MagicDB> magicDBList = LegendApi.GetMagicList();
                            MagicDB magicDB = null;

                            string uPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                            IniFileHelper ini_userSkill = new IniFileHelper(uPath);
                            string currStory = ini_userSkill.Read("人物信息", "所在商店");
                            int page = 0;
                            if (!string.IsNullOrEmpty(currStory))
                            {
                                if (currStory.Split('_')[1] == "个人技能")
                                {
                                    page = int.Parse(ini_userSkill.Read("人物信息", "所在页面"));
                                }
                                else
                                {
                                    ini_userSkill.Write("人物信息", "所在商店", user.Map + "_个人技能");
                                    ini_userSkill.Write("人物信息", "所在页面", "1");
                                    page = 1;
                                }
                            }
                            else
                            {
                                ini_userSkill.Write("人物信息", "所在商店", user.Map + "_个人技能");
                                ini_userSkill.Write("人物信息", "所在页面", "1");
                                page = 1;
                            }


                            var usKeyList = ini_userSkill.GetAllKeys("个人技能");
                            int total = usKeyList.Count;
                            int pageSize = 6;
                            int recordPage = (total + pageSize - 1) / pageSize;// 获取仓库页数
                            if (page > recordPage) page = recordPage;// 如果跳转的夜数大于总页数就等于总页数
                            usKeyList = usKeyList.Skip((page - 1) * pageSize).Take(pageSize).ToList();// 对仓库数据分页

                            UserSkill us;

                            string skill = string.Empty, skillPath = magIcon2Path;
                            int skillX = 731, skillY = 140, i = 0;
                            int skNameX = 768, skNameY = 138;
                            int lvX = 860, lvY = 138;
                            int sksldX = 770, sksldY = 157;
                            int skLvX = 900, skLvY = 138;
                            int sksld2X = 825, sksld2Y = 158;
                            int isckSkillX = 940, isckSkillY = 145;

                            usKeyList.ForEach(sk =>
                            {
                                if (i > 5) return;
                                skill = ini_userSkill.Read("个人技能", sk);
                                us = Api.JsonDeserialize<UserSkill>(skill);
                                if (us != null)
                                {
                                    skillPath = magIcon2Path + us.US_Img;
                                    using (Image img = Image.FromFile(skillPath))// 技能
                                    {
                                        gh.DrawImage(img, skillX, skillY + (i * 46), img.Width, img.Height);
                                    }
                                    rect = new Rectangle(skNameX, skNameY + (i * 46), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, us.US_Name, font14, brush, rect);//技能名称

                                    skillPath = ui1Path + "000920.png";
                                    using (Image img = Image.FromFile(skillPath))// 技能等级
                                    {
                                        gh.DrawImage(img, lvX, lvY + (i * 46), img.Width, img.Height);
                                    }
                                    rect = new Rectangle(skLvX, skLvY + (i * 46), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, us.US_Level.ToString(), font14, brush, rect);// 技能等级

                                    skillPath = ui1Path + "000921.png";
                                    using (Image img = Image.FromFile(skillPath))// 技能熟练度
                                    {
                                        gh.DrawImage(img, sksldX, sksldY + (i * 46), img.Width, img.Height);
                                    }
                                    magicDB = magicDBList.Where(m => m.MagID == us.US_MagID).FirstOrDefault();

                                    if (us.US_IsEnableSkill == "是")
                                    {
                                        skillPath = ui1Path + "003601.png";
                                        using (Image img = Image.FromFile(skillPath))// 使用技能
                                        {
                                            gh.DrawImage(img, isckSkillX, isckSkillY + (i * 46), img.Width, img.Height);
                                        }
                                    }
                                    else
                                    {
                                        skillPath = ui1Path + "003600.png";
                                        using (Image img = Image.FromFile(skillPath))// 没有使用技能
                                        {
                                            gh.DrawImage(img, isckSkillX, isckSkillY + (i * 46), img.Width, img.Height);
                                        }
                                    }
                                    // 技能熟练度
                                    switch (us.US_Level)
                                    {
                                        case 0:
                                            if (us.US_NeedLN1 < magicDB.NeedLN1)
                                            {
                                                rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, us.US_NeedLN1 + "/" + magicDB.NeedLN1, font12, brush, rect);
                                            }
                                            break;
                                        case 1:
                                            if (us.US_NeedLN2 < magicDB.NeedLN2)
                                            {
                                                rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, us.US_NeedLN2 + "/" + magicDB.NeedLN2, font12, brush, rect);
                                            }
                                            if (magicDB.NeedLN2 == 0)
                                            {
                                                rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, "-", font10, brush, rect);
                                            }
                                            break;
                                        case 2:
                                            if (us.US_NeedLN3 < magicDB.NeedLN3)
                                            {
                                                rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, us.US_NeedLN3 + "/" + magicDB.NeedLN3, font12, brush, rect);
                                            }
                                            if (magicDB.NeedLN3 == 0)
                                            {
                                                rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, "-", font10, brush, rect);
                                            }
                                            break;
                                        default:
                                            rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "-", font10, brush, rect);
                                            break;
                                    }

                                    i++;
                                }
                            });
                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }

                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看属性
        /// </summary>
        public string GetUserAttributeIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                string savePath = UserPath + user.UserQQ + ".ini";
                IniFileHelper config = new IniFileHelper(savePath);
                StditemDB stditemDB = null;
                int lvv = 0, wear = 0, wrist = 0;
                if (user.User_Items.DRESS.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.DRESS));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.WEAPON.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.WEAPON));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.HELMET.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HELMET));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.NECKLACE.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.NECKLACE));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RING_R.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_R));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RING_L.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_L));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.ARMRING_R.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_R));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.ARMRING_L.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_L));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.BELT.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.BELT));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.BOOTS.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.BOOTS));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.BUJUK.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.BUJUK));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.CHARM.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.CHARM));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.HAT.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HAT));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RIGHTHAND.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RIGHTHAND));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成属性图
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            SolidBrush brush = new SolidBrush(color);
                            SolidBrush brushLime = new SolidBrush(Color.Lime);
                            Rectangle rect;
                            using (Image img = Image.FromFile(newopUIPath + "000090.png"))// 状态
                            {
                                gh.DrawImage(img, 680, 0, img.Width, img.Height);
                            }

                            rect = new Rectangle(850, 70, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Name, font15, brush, rect);//用户名

                            rect = new Rectangle(810, 171, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Ac + "/" + user.User_Attribute.Ac2, font12, brush, rect);//防御
                            rect = new Rectangle(810, 189, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Mac + "/" + user.User_Attribute.Mac2, font12, brush, rect);//魔御
                            rect = new Rectangle(810, 207, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Dc + "/" + user.User_Attribute.DcMax, font12, brush, rect);//攻击
                            rect = new Rectangle(810, 225, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Mc + "/" + user.User_Attribute.McMax, font12, brush, rect);//魔法
                            rect = new Rectangle(810, 243, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Sc + "/" + user.User_Attribute.ScMax, font12, brush, rect);//道术
                            rect = new Rectangle(810, 261, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Acc.ToString(), font12, brush, rect);//准确
                            rect = new Rectangle(810, 279, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Agile.ToString(), font12, brush, rect);//敏捷
                            rect = new Rectangle(810, 297, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Heals.ToString(), font12, brush, rect);//暴击
                            rect = new Rectangle(810, 315, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Lucky.ToString(), font12, brush, rect);//幸运
                            rect = new Rectangle(830, 333, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.MagicEvas + "%", font12, brush, rect);//魔法躲避
                            rect = new Rectangle(830, 351, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.AttackSpeed + "%", font12, brush, rect);//攻击速度
                            rect = new Rectangle(830, 369, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.MoveSpeed + "%", font12, brush, rect);//移动速度 
                            rect = new Rectangle(830, 387, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.Bgw + "/" + user.User_General.BgwMax, font12, brush, rect);//背包重量
                            rect = new Rectangle(830, 404, 0, 0);
                            if (wear > 0)
                            {
                                TextHelper.DrawCenterStrokeString(gh, $"{user.User_General.Wear}/{user.User_General.WearMax + wear}(+{wear})", font12, brushLime, rect);//穿戴重量
                            }
                            else
                            {
                                TextHelper.DrawCenterStrokeString(gh, user.User_General.Wear + "/" + user.User_General.WearMax, font12, brush, rect);//穿戴重量
                            }

                            rect = new Rectangle(960, 171, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.HealsPR + "%", font12, brush, rect);//暴击几率
                            rect = new Rectangle(960, 189, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.AttackPR + "%", font12, brush, rect);//攻击伤害
                            rect = new Rectangle(960, 207, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.MagicAC + "%", font12, brush, rect);//魔法防御
                            rect = new Rectangle(960, 225, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.IgnoreAC + "%", font12, brush, rect);//忽视防御
                            rect = new Rectangle(960, 243, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.DamageRebound + "%", font12, brush, rect);//伤害反弹
                            rect = new Rectangle(960, 261, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.UserExpRate + "%", font12, brush, rect);//人物爆率
                            rect = new Rectangle(960, 279, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.MonExpRate + "%", font12, brush, rect);//怪物爆率
                            rect = new Rectangle(960, 297, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.HPIncrease + "%", font12, brush, rect);//体力增加
                            rect = new Rectangle(960, 315, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.MPIncrease + "%", font12, brush, rect);//魔力增加                           
                            rect = new Rectangle(960, 333, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.FatalInjury + "%", font12, brush, rect);//致命伤害
                            rect = new Rectangle(960, 351, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.FatalBlow + "%", font12, brush, rect);//致命一击
                            rect = new Rectangle(960, 369, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.ExcellentBlow + "%", font12, brush, rect);//卓越一击
                            rect = new Rectangle(960, 387, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.HeartfeltBlow + "%", font12, brush, rect);//会心一击

                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }

                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看状态
        /// </summary>
        public string GetUserStatusIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                string savePath = UserPath + user.UserQQ + ".ini";
                IniFileHelper config = new IniFileHelper(savePath);
                StditemDB stditemDB = null;
                int lvv = 0, wear = 0, wrist = 0;
                if (user.User_Items.DRESS.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.DRESS));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.WEAPON.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.WEAPON));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.HELMET.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HELMET));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.NECKLACE.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.NECKLACE));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RING_R.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_R));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RING_L.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_L));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.ARMRING_R.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_R));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.ARMRING_L.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_L));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        wear += stditemDB.StdHiddAttr.Wear;
                        wrist += stditemDB.StdHiddAttr.Wrist;
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }


                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成状态图
                            using (Image img = Image.FromFile(newopUIPath + "000089.png"))// 状态
                            {
                                gh.DrawImage(img, 680, 0, img.Width, img.Height);
                            }
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Brush brushLime = new SolidBrush(Color.Lime);
                            Rectangle rect = new Rectangle(850, 70, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Name, font15, brush, rect);//用户名

                            rect = new Rectangle(835, 165, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Job, font12, brush, rect);//职业

                            if (lvv > 0)
                            {
                                rect = new Rectangle(835, 187, 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, $"{user.User_General.Lv + lvv}(+{lvv})", font12, brushLime, rect);//等级                               
                            }
                            else
                            {
                                rect = new Rectangle(835, 187, 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, user.User_General.Lv.ToString(), font12, brush, rect);//等级
                            }

                            rect = new Rectangle(835, 206, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Hp.ToString(), font12, brush, rect);//体力值

                            rect = new Rectangle(835, 226, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Mp.ToString(), font12, brush, rect);//魔法值

                            rect = new Rectangle(835, 246, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.SumExp.ToString(), font12, brush, rect);//当前经验

                            rect = new Rectangle(835, 266, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.NextExp.ToString(), font12, brush, rect);//升级经验

                            rect = new Rectangle(835, 286, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.CurtYB.ToString(), font12, brush, rect);//签到元宝

                            rect = new Rectangle(835, 306, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.YB.ToString(), font12, brush, rect);//当前元宝

                            rect = new Rectangle(835, 326, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.JGS.ToString(), font12, brush, rect);//金刚石

                            rect = new Rectangle(835, 346, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.REP.ToString(), font12, brush, rect);//声望

                            rect = new Rectangle(835, 366, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.Bgw + "/" + user.User_General.BgwMax, font12, brush, rect);//背包重量

                            if (wear > 0)
                            {
                                rect = new Rectangle(795, 376, 100, 20);
                                TextHelper.DrawCenterStrokeString(gh, $"{user.User_General.Wear}/{user.User_General.WearMax + wear}(+{wear})", font12, brushLime, rect);//穿戴重量
                            }
                            else
                            {
                                rect = new Rectangle(810, 376, 60, 20);
                                TextHelper.DrawCenterStrokeString(gh, user.User_General.Wear + "/" + user.User_General.WearMax, font12, brush, rect);//穿戴重量
                            }

                            rect = new Rectangle(930, 156, 60, 20);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.PkPoint.ToString(), font12, brush, rect);//pk点数

                            if (wrist > 0)
                            {
                                rect = new Rectangle(960, 187, 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, $"{user.User_Attribute.Wrist}/{user.User_Attribute.WristMax + wrist}(+{wrist})", font12, brushLime, rect);//腕力
                            }
                            else
                            {
                                rect = new Rectangle(960, 187, 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, user.User_Attribute.Wrist + "/" + user.User_Attribute.WristMax, font12, brush, rect);//腕力
                            }
                            rect = new Rectangle(960, 206, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.ActivityQty.ToString(), font12, brush, rect);//活跃值

                            rect = new Rectangle(960, 227, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Map, font12, brush, rect);//所在地图

                            rect = new Rectangle(960, 246, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.IsOnCity, font12, brush, rect);//是否在城里

                            rect = new Rectangle(945, 368, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.PKWin.ToString(), font12, brush, rect);//pk赢

                            rect = new Rectangle(971, 368, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.PKLose.ToString(), font12, brush, rect);//pk输


                            rect = new Rectangle(945, 386, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.PKPwWin.ToString(), font12, brush, rect);//排位pk赢
                            rect = new Rectangle(971, 386, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.User_General.PKPwLose.ToString(), font12, brush, rect);//排位pk输

                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看装备
        /// </summary>
        public string GetEquipIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成装备图
                            switch (user.Sex)
                            {
                                case "男":
                                    using (Image img = Image.FromFile(newopUIPath + "000088.png"))// 人物
                                    {
                                        gh.DrawImage(img, 680, 0, img.Width, img.Height);
                                    }
                                    break;
                                case "女":
                                    using (Image img = Image.FromFile(newopUIPath + "000087.png"))// 人物
                                    {
                                        gh.DrawImage(img, 680, 0, img.Width, img.Height);
                                    }
                                    break;
                                default: break;
                            }
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(850, 70, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, user.Name, font15, brush, rect);//用户名

                            //查询出佩戴的装备,生成装备图片
                            path = UserPath + user.UserQQ + ".ini";
                            IniFileHelper iniFileHelper = new IniFileHelper(path);
                            List<string> listBwg = iniFileHelper.GetAllKeys("装备物品");

                            string zbbh = "", std, stdPosPath;
                            List<string> stdPos = new List<string>();
                            int stdX, stdY;
                            StditemDB stditemDB = null;
                            foreach (string key in listBwg)
                            {
                                zbbh = iniFileHelper.Read("装备物品", key);
                                if (!string.IsNullOrEmpty(zbbh) && zbbh != "0")
                                {
                                    std = iniFileHelper.ReadValue("装备属性", zbbh);
                                    stditemDB = Api.JsonDeserialize<StditemDB>(std);
                                    if (stditemDB.Looks > 384)
                                        stditemDB.Looks = stditemDB.Looks - 7484 + 384;
                                    string stdUIPath = stateitemPath + stditemDB.Looks.ToString().PadLeft(6, '0') + ".png";
                                    stdPosPath = stateitemPath + "Placements\\" + stditemDB.Looks.ToString().PadLeft(6, '0') + ".txt";
                                    stdPos = FileHelper.ReadTxtReturnList(stdPosPath);

                                    switch (key)
                                    {
                                        case "衣服":
                                        case "武器":
                                            stdX = int.Parse(stdPos[0]) + 765;
                                            stdY = int.Parse(stdPos[1]) + 205;
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "右戒指":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 730;
                                                stdY = stdY - (img.Height / 2) + 406;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "左戒指":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 972;
                                                stdY = stdY - (img.Height / 2) + 406;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "右手镯":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 730;
                                                stdY = stdY - (img.Height / 2) + 364;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "左手镯":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 972;
                                                stdY = stdY - (img.Height / 2) + 364;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "项链":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 972;
                                                stdY = stdY - (img.Height / 2) + 320;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "头盔":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 853;
                                                stdY = stdY - (img.Height / 2) + 255;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "斗笠":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 853;
                                                stdY = stdY - (img.Height / 2) + 255;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "护身符":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 738;
                                                stdY = stdY - (img.Height / 2) + 450;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "宝石":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 875;
                                                stdY = stdY - (img.Height / 2) + 450;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "腰带":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 783;
                                                stdY = stdY - (img.Height / 2) + 450;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "鞋子":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 827;
                                                stdY = stdY - (img.Height / 2) + 450;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "勋章":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 729;
                                                stdY = stdY - (img.Height / 2) + 322;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        case "翅膀":
                                            stdX = int.Parse(stdPos[0]);
                                            stdY = int.Parse(stdPos[1]);
                                            if (stdX != 7 && stdY != -44)
                                            {
                                                stdX = 7;
                                                stdY = -44;
                                            }
                                            using (Image img = Image.FromFile(stdUIPath))
                                            {
                                                stdX = stdX - (img.Width / 2) + 730;
                                                stdY = stdY - (img.Height / 2) + 280;
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看背包详情
        /// </summary>
        public string GetBWGDetailsIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成背包图片

                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))// 人物
                            {
                                int img02W = img.Width * 70 / 100;
                                int img02H = img.Height * 70 / 100;
                                gh.DrawImage(img, 10, 30, img02W, img02H);
                            }
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            //加载背包里的物品
                            Rectangle rect = new Rectangle(52, 225, 0, 0);
                            TextHelper.DrawLeftString(gh, user.User_General.JB.ToString(), font8, brush, rect);//金币

                            rect = new Rectangle(66, 247, 0, 0);
                            TextHelper.DrawLeftString(gh, "元宝：", font8, brush, rect);//元宝
                            rect = new Rectangle(100, 248, 0, 0);
                            TextHelper.DrawLeftString(gh, user.User_General.YB.ToString(), font8, brush, rect);//元宝

                            rect = new Rectangle(56, 262, 0, 0);
                            TextHelper.DrawLeftString(gh, "金刚石：" + user.User_General.JGS, font8, brush, rect);//金刚石
                            rect = new Rectangle(250, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  充值：" + user.User_General.RegAmt.ToString("N0", CultureInfo.InvariantCulture), font13, brush, rect);//真实充值

                            StditemDB stditemDB = null;

                            string newpath = UserPath + user.UserQQ + ".ini";
                            IniFileHelper iniFileHelper = new IniFileHelper(newpath);

                            List<string> listBwg = iniFileHelper.GetAllKeys("背包物品");
                            for (int i = 0; i < listBwg.Count; i++)
                            {
                                stditemDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("背包物品", listBwg[i]));
                                if (stditemDB != null)
                                {
                                    newpath = itemsPath + stditemDB.StdImgPath;
                                    using (Image img = Image.FromFile(newpath))   //加载背包里的物品
                                    {
                                        int img02W = img.Width * 70 / 100;
                                        int img02H = img.Height * 70 / 100;
                                        switch (i)
                                        {
                                            case 0: gh.DrawImage(img, 23, 40, img02W, img02H); break;
                                            case 1: gh.DrawImage(img, 50, 40, img02W, img02H); break;
                                            case 2: gh.DrawImage(img, 78, 40, img02W, img02H); break;
                                            case 3: gh.DrawImage(img, 102, 40, img02W, img02H); break;
                                            case 4: gh.DrawImage(img, 126, 40, img02W, img02H); break;
                                            case 5: gh.DrawImage(img, 150, 40, img02W, img02H); break;
                                            case 6: gh.DrawImage(img, 174, 40, img02W, img02H); break;
                                            case 7: gh.DrawImage(img, 199, 40, img02W, img02H); break;
                                            case 8: gh.DrawImage(img, 225, 40, img02W, img02H); break;
                                            case 9: gh.DrawImage(img, 250, 40, img02W, img02H); break;
                                            case 10: gh.DrawImage(img, 272, 40, img02W, img02H); break;
                                            case 11: gh.DrawImage(img, 298, 40, img02W, img02H); break;
                                            case 12: gh.DrawImage(img, 28, 63, img02W, img02H); break;
                                            case 13: gh.DrawImage(img, 53, 63, img02W, img02H); break;
                                            case 14: gh.DrawImage(img, 72, 63, img02W, img02H); break;
                                            case 15: gh.DrawImage(img, 101, 63, img02W, img02H); break;
                                            case 16: gh.DrawImage(img, 125, 63, img02W, img02H); break;
                                            case 17: gh.DrawImage(img, 150, 63, img02W, img02H); break;
                                            case 18: gh.DrawImage(img, 170, 63, img02W, img02H); break;
                                            case 19: gh.DrawImage(img, 194, 63, img02W, img02H); break;
                                            case 20: gh.DrawImage(img, 219, 63, img02W, img02H); break;
                                            case 21: gh.DrawImage(img, 246, 63, img02W, img02H); break;
                                            case 22: gh.DrawImage(img, 267, 63, img02W, img02H); break;
                                            case 23: gh.DrawImage(img, 292, 63, img02W, img02H); break;
                                            case 24: gh.DrawImage(img, 23, 85, img02W, img02H); break;
                                            case 25: gh.DrawImage(img, 52, 85, img02W, img02H); break;
                                            case 26: gh.DrawImage(img, 74, 85, img02W, img02H); break;
                                            case 27: gh.DrawImage(img, 102, 85, img02W, img02H); break;
                                            case 28: gh.DrawImage(img, 128, 85, img02W, img02H); break;
                                            case 29: gh.DrawImage(img, 151, 85, img02W, img02H); break;
                                            case 30: gh.DrawImage(img, 171, 85, img02W, img02H); break;
                                            case 31: gh.DrawImage(img, 195, 85, img02W, img02H); break;
                                            case 32: gh.DrawImage(img, 220, 85, img02W, img02H); break;
                                            case 33: gh.DrawImage(img, 245, 85, img02W, img02H); break;
                                            case 34: gh.DrawImage(img, 271, 85, img02W, img02H); break;
                                            case 35: gh.DrawImage(img, 298, 85, img02W, img02H); break;
                                            case 36: gh.DrawImage(img, 26, 107, img02W, img02H); break;
                                            case 37: gh.DrawImage(img, 52, 107, img02W, img02H); break;
                                            case 38: gh.DrawImage(img, 76, 107, img02W, img02H); break;
                                            case 39: gh.DrawImage(img, 101, 107, img02W, img02H); break;
                                            case 40: gh.DrawImage(img, 117, 107, img02W, img02H); break;
                                            case 41: gh.DrawImage(img, 146, 107, img02W, img02H); break;
                                            case 42: gh.DrawImage(img, 172, 107, img02W, img02H); break;
                                            case 43: gh.DrawImage(img, 197, 107, img02W, img02H); break;
                                            case 44: gh.DrawImage(img, 222, 107, img02W, img02H); break;
                                            case 45: gh.DrawImage(img, 246, 107, img02W, img02H); break;
                                            case 46: gh.DrawImage(img, 272, 107, img02W, img02H); break;
                                            case 47: gh.DrawImage(img, 296, 107, img02W, img02H); break;
                                            case 48: gh.DrawImage(img, 23, 130, img02W, img02H); break;
                                            case 49: gh.DrawImage(img, 50, 130, img02W, img02H); break;
                                            case 50: gh.DrawImage(img, 72, 130, img02W, img02H); break;
                                            case 51: gh.DrawImage(img, 100, 130, img02W, img02H); break;
                                            case 52: gh.DrawImage(img, 122, 130, img02W, img02H); break;
                                            case 53: gh.DrawImage(img, 148, 130, img02W, img02H); break;
                                            case 54: gh.DrawImage(img, 172, 130, img02W, img02H); break;
                                            case 55: gh.DrawImage(img, 195, 130, img02W, img02H); break;
                                            case 56: gh.DrawImage(img, 223, 130, img02W, img02H); break;
                                            case 57: gh.DrawImage(img, 244, 130, img02W, img02H); break;
                                            case 58: gh.DrawImage(img, 270, 130, img02W, img02H); break;
                                            case 59: gh.DrawImage(img, 296, 130, img02W, img02H); break;
                                            case 60: gh.DrawImage(img, 23, 151, img02W, img02H); break;
                                            case 61: gh.DrawImage(img, 50, 151, img02W, img02H); break;
                                            case 62: gh.DrawImage(img, 72, 151, img02W, img02H); break;
                                            case 63: gh.DrawImage(img, 100, 151, img02W, img02H); break;
                                            case 64: gh.DrawImage(img, 122, 151, img02W, img02H); break;
                                            case 65: gh.DrawImage(img, 146, 151, img02W, img02H); break;
                                            case 66: gh.DrawImage(img, 172, 151, img02W, img02H); break;
                                            case 67: gh.DrawImage(img, 196, 151, img02W, img02H); break;
                                            case 68: gh.DrawImage(img, 221, 151, img02W, img02H); break;
                                            case 69: gh.DrawImage(img, 244, 151, img02W, img02H); break;
                                            case 70: gh.DrawImage(img, 270, 151, img02W, img02H); break;
                                            case 71: gh.DrawImage(img, 295, 151, img02W, img02H); break;
                                            case 72: gh.DrawImage(img, 23, 175, img02W, img02H); break;
                                            case 73: gh.DrawImage(img, 50, 175, img02W, img02H); break;
                                            case 74: gh.DrawImage(img, 72, 175, img02W, img02H); break;
                                            case 75: gh.DrawImage(img, 100, 175, img02W, img02H); break;
                                            case 76: gh.DrawImage(img, 122, 175, img02W, img02H); break;
                                            case 77: gh.DrawImage(img, 146, 175, img02W, img02H); break;
                                            case 78: gh.DrawImage(img, 172, 175, img02W, img02H); break;
                                            case 79: gh.DrawImage(img, 196, 175, img02W, img02H); break;
                                            case 80: gh.DrawImage(img, 221, 175, img02W, img02H); break;
                                            case 81: gh.DrawImage(img, 244, 175, img02W, img02H); break;
                                            case 82: gh.DrawImage(img, 270, 175, img02W, img02H); break;
                                            case 83: gh.DrawImage(img, 295, 175, img02W, img02H); break;
                                            case 84: gh.DrawImage(img, 23, 197, img02W, img02H); break;
                                            case 85: gh.DrawImage(img, 50, 197, img02W, img02H); break;
                                            case 86: gh.DrawImage(img, 72, 197, img02W, img02H); break;
                                            case 87: gh.DrawImage(img, 100, 197, img02W, img02H); break;
                                            case 88: gh.DrawImage(img, 122, 197, img02W, img02H); break;
                                            case 89: gh.DrawImage(img, 146, 197, img02W, img02H); break;
                                            case 90: gh.DrawImage(img, 172, 197, img02W, img02H); break;
                                            case 91: gh.DrawImage(img, 196, 197, img02W, img02H); break;
                                            case 92: gh.DrawImage(img, 221, 197, img02W, img02H); break;
                                            case 93: gh.DrawImage(img, 244, 197, img02W, img02H); break;
                                            case 94: gh.DrawImage(img, 270, 197, img02W, img02H); break;
                                            case 95: gh.DrawImage(img, 295, 197, img02W, img02H); break;
                                        }
                                    }
                                }
                            }

                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看背包
        /// </summary>
        public string GetBWGIMG(UserInfo user, int lv = 0, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            int i = 0, humX = 0, humY = 0, itemX = 0, itemY = 0, textX = 0, textY = 0, jpX = 0, jpY = 0, bgIndexX = 0, bgIndexY = 0;
                            string itemPath = "";
                            string newpath = UserPath + user.UserQQ + ".ini";

                            #region 生成背包图片
                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))
                            //using (Image img = Image.FromFile(newopUIPath + "001910.png"))
                            {
                                gh.DrawImage(img, 8, 30, img.Width, img.Height);
                            }
                            Brush brushWhite = new SolidBrush(Color.White);
                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            //加载背包里的物品
                            Rectangle rect = new Rectangle(68, 308, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.User_General.JB.ToString("N0", CultureInfo.InvariantCulture), font13, brushWhite, rect);//金币
                            rect = new Rectangle(73, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  元宝：" + user.User_General.YB.ToString("N0", CultureInfo.InvariantCulture), font13, brushWhite, rect);//元宝
                            rect = new Rectangle(73, 365, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "金刚石：" + user.User_General.JGS.ToString("N0", CultureInfo.InvariantCulture), font13, brushWhite, rect);//金刚石
                            rect = new Rectangle(250, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  充值：" + user.User_General.RegAmt.ToString("N0", CultureInfo.InvariantCulture), font13, brushWhite, rect);//真实充值

                            StditemDB stditemDB = null, choiseStd = null;
                            IniFileHelper config = new IniFileHelper(newpath);
                            List<string> listBwg = config.GetAllKeys("背包物品");
                            if (lv > listBwg.Count || lv < 0) throw new Exception("查看数量过大或错误,请输入 查看背包 重新指定查看物品吧.");
                            listBwg.ForEach(stdKey =>
                            {
                                stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("背包物品", stdKey));

                                if (stditemDB != null)
                                {
                                    newpath = itemsPath + stditemDB.StdImgPath;
                                    using (Image img = Image.FromFile(newpath))   //加载背包里的物品
                                    {
                                        switch (i / 12)
                                        {
                                            case 0:
                                                humX = (i * 35) - (img.Width / 2) + 45;
                                                humY = 44 - (img.Height / 2) + 14;
                                                textX = (i * 35) + 63;
                                                textY = 40;
                                                jpX = (i * 35) + 47;
                                                jpY = 38;
                                                bgIndexX = (i * 35) + 26;
                                                bgIndexY = 62;
                                                break;
                                            case 1:
                                                humX = (i - 12) * 35 - (img.Width / 2) + 45;
                                                humY = 77 - (img.Height / 2) + 14;
                                                textX = (i - 12) * 35 + 63;
                                                textY = 73;
                                                jpX = (i - 12) * 35 + 47;
                                                jpY = 71;
                                                bgIndexX = (i - 12) * 35 + 26;
                                                bgIndexY = 95;
                                                break;
                                            case 2:
                                                humX = (i - 24) * 35 - (img.Width / 2) + 45;
                                                humY = 108 - (img.Height / 2) + 14;
                                                textX = (i - 24) * 35 + 63;
                                                textY = 106;
                                                jpX = (i - 24) * 35 + 47;
                                                jpY = 105;
                                                bgIndexX = (i - 24) * 35 + 26;
                                                bgIndexY = 124;
                                                break;
                                            case 3:
                                                humX = (i - 36) * 35 - (img.Width / 2) + 45;
                                                humY = 139 - (img.Height / 2) + 14;
                                                textX = (i - 36) * 35 + 63;
                                                textY = 134;
                                                jpX = (i - 36) * 35 + 47;
                                                jpY = 133;
                                                bgIndexX = (i - 36) * 35 + 26;
                                                bgIndexY = 156;
                                                break;
                                            case 4:
                                                humX = (i - 48) * 35 - (img.Width / 2) + 45;
                                                humY = 170 - (img.Height / 2) + 14;
                                                textX = (i - 48) * 35 + 63;
                                                textY = 167;
                                                jpX = (i - 48) * 35 + 47;
                                                jpY = 165;
                                                bgIndexX = (i - 48) * 35 + 26;
                                                bgIndexY = 190;
                                                break;
                                            case 5:
                                                humX = (i - 60) * 35 - (img.Width / 2) + 45;
                                                humY = 204 - (img.Height / 2) + 14;
                                                textX = (i - 60) * 35 + 63;
                                                textY = 200;
                                                jpX = (i - 60) * 35 + 47;
                                                jpY = 201;
                                                bgIndexX = (i - 60) * 35 + 26;
                                                bgIndexY = 222;
                                                break;
                                            case 6:
                                                humX = (i - 72) * 35 - (img.Width / 2) + 45;
                                                humY = 237 - (img.Height / 2) + 14;
                                                textX = (i - 72) * 35 + 63;
                                                textY = 233;
                                                jpX = (i - 72) * 35 + 47;
                                                jpY = 233;
                                                bgIndexX = (i - 72) * 35 + 26;
                                                bgIndexY = 255;
                                                break;
                                            case 7:
                                                humX = (i - 84) * 35 - (img.Width / 2) + 45;
                                                humY = 270 - (img.Height / 2) + 14;
                                                textX = (i - 84) * 35 + 63;
                                                textY = 266;
                                                jpX = (i - 84) * 35 + 47;
                                                jpY = 267;
                                                bgIndexX = (i - 84) * 35 + 26;
                                                bgIndexY = 288;
                                                break;
                                            default:

                                                break;
                                        }

                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                    }

                                    i++;

                                    // 背包位置
                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font12, brushWhite, rect);

                                    if (i == lv)
                                    {
                                        itemX = humX + 10;
                                        itemY = humY + 10;
                                        itemPath = newpath;

                                        choiseStd = stditemDB;
                                        //choiseStd.StdAC2 += choiseStd.SubAttr1;
                                        //choiseStd.StdMac2 += choiseStd.SubAttr2;
                                        //choiseStd.StdDc2 += choiseStd.SubAttr3;
                                        //choiseStd.StdMc2 += choiseStd.SubAttr4;
                                        //choiseStd.StdSc2 += choiseStd.SubAttr5;
                                        //choiseStd.Accuracy += choiseStd.SubAttr6;
                                        //choiseStd.Agile += choiseStd.SubAttr7;
                                        //choiseStd.Lucky += choiseStd.SubAttr8;
                                        //choiseStd.Strength += choiseStd.SubAttr13;
                                        //choiseStd.Accuracy += choiseStd.SubAttr6;

                                    }

                                    // 判断是否是极品,极品给个标识
                                    if ((stditemDB.SubAttr1 > 0
                                            || stditemDB.SubAttr2 > 0
                                            || stditemDB.SubAttr3 > 0
                                            || stditemDB.SubAttr4 > 0
                                            || stditemDB.SubAttr5 > 0
                                            || stditemDB.SubAttr6 > 0
                                            || stditemDB.SubAttr7 > 0
                                            || stditemDB.SubAttr8 > 0
                                            || stditemDB.SubAttr9 > 0
                                            || stditemDB.SubAttr10 > 0
                                            || stditemDB.SubAttr11 > 0
                                            || stditemDB.SubAttr13 > 0
                                            || stditemDB.StdHP > 0)
                                            && stditemDB.OverLap == 0)
                                    {
                                        newpath = newopUIPath + "001910.png";
                                        using (Image img = Image.FromFile(newpath)) //极品标识图
                                        {
                                            gh.DrawImage(img, jpX, jpY, img.Width, img.Height);
                                        }
                                    }

                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                    {
                                        rect = new Rectangle(textX, textY, 0, 0);
                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font12, brushLimeGreen, rect);
                                    }
                                }
                            });
                            #endregion

                            if (lv > 0)
                            {
                                #region 生成需要查看的物品
                                CreateDialogBoxByStdItem(gh, itemX, itemY, choiseStd, false, x, y);

                                // 找身上穿戴装备，穿了的同样加载出来
                                switch (choiseStd.StdMode)
                                {
                                    case 5:
                                    case 6: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.WEAPON)); break;
                                    case 10:
                                    case 11: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.DRESS)); break;
                                    case 12: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SHIELD)); break;
                                    case 15: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HELMET)); break;
                                    case 16: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HAT)); break;
                                    case 19:
                                    case 20:
                                    case 21: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.NECKLACE)); break;
                                    case 25: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.BUJUK)); break;
                                    case 28: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HORSE)); break;
                                    case 30: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RIGHTHAND)); break;
                                    case 52:
                                    case 62: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.BOOTS)); break;
                                    case 63: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.CHARM)); break;
                                    case 54:
                                    case 64: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.BELT)); break;
                                    case 65: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.DRUM)); break;
                                    case 66:
                                    case 67: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_DRESS)); break;
                                    case 68:
                                    case 69: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_WEAPON)); break;
                                    case 75:
                                    case 76:
                                    case 77: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_NECKLACE)); break;
                                    case 78: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_HELMET)); break;
                                    case 83: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_RIGHTHAND)); break;
                                    case 84:
                                    case 85: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_BELT)); break;
                                    case 86:
                                    case 87: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_BOOTS)); break;
                                    case 88:
                                    case 89: stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.CHARM)); break;
                                    default: stditemDB = null; break;
                                }

                                switch (choiseStd.StdMode)
                                {
                                    case 22:
                                    case 23:
                                        {
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_L));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_R));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                        }
                                        break;
                                    case 79:
                                    case 80:
                                        {
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_RING_L));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_RING_R));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                        }
                                        break;
                                    case 24:
                                    case 26:
                                        {
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_L));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_R));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                        }
                                        break;
                                    case 81:
                                    case 82:
                                        {
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_ARMRING_L));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                            stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.SZ_ARMRING_R));
                                            if (stditemDB != null)
                                            {
                                                itemX += 205;
                                                CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                            }
                                        }
                                        break;
                                    default:
                                        if (stditemDB != null)
                                        {
                                            itemX += 205;
                                            CreateDialogBoxByStdItem(gh, itemX, itemY, stditemDB, true, x, y);
                                        }
                                        break;
                                }
                                #endregion
                            }


                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }
                    return path;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建装备对话框
        /// </summary>
        private void CreateDialogBoxByStdItem(Graphics gh, int itemX, int itemY, StditemDB stdItem, bool isFlag = false, int x = 0, int y = 0)
        {
            int posX = itemX + 10;
            int posY = itemY + 10;
            string newpath = "";

            List<string> list = new List<string>();

            // 物品名称
            string stdName = stdItem.StdName;
            itemX += 25;
            itemY += 30;
            list.Add($"1#{stdName}#{itemX}#{itemY}#10#White");

            string stdModeName = "";
            switch (stdItem.StdMode)
            {
                case 0: stdModeName = "药品"; break;
                case 1: stdModeName = "食物"; break;
                case 2: stdModeName = "储存灌"; break;
                case 3: stdModeName = "卷"; break;
                case 7: stdModeName = "技能书"; break;
                case 5:
                case 6: stdModeName = "武器"; break;
                case 10: stdModeName = "衣服(男)"; break;
                case 11: stdModeName = "衣服(女)"; break;
                case 12: stdModeName = "盾牌"; break;
                case 15: stdModeName = "盾牌"; break;
                case 16: stdModeName = "斗笠"; break;
                case 19:
                case 20:
                case 21: stdModeName = "项链"; break;
                case 22:
                case 23: stdModeName = "戒指"; break;
                case 24:
                case 26: stdModeName = "手镯"; break;
                case 25: stdModeName = "符、毒药"; break;
                case 28: stdModeName = "马牌"; break;

                case 30: stdModeName = "勋章"; break;
                case 31: stdModeName = "捆装类"; break;
                case 40: stdModeName = "肉类"; break;
                case 41: stdModeName = "任务物品"; break;
                case 42: stdModeName = "制作原料"; break;
                case 43: stdModeName = "矿石"; break;
                case 44: stdModeName = "行会相关"; break;
                case 45: stdModeName = "特殊物品"; break;
                case 46: stdModeName = "宝石镶嵌"; break;
                case 47: stdModeName = "金钱"; break;
                case 48: stdModeName = "盾牌"; break;
                case 49: stdModeName = "聚灵珠"; break;
                case 53: stdModeName = "宝石"; break;
                case 55:
                case 56:
                case 57:
                case 58:
                case 59: stdModeName = "宝石升级物品"; break;
                case 52:
                case 62: stdModeName = "鞋子"; break;
                case 63: stdModeName = "宝石"; break;
                case 54:
                case 64: stdModeName = "腰带"; break;
                case 65: stdModeName = "军鼓"; break;
                case 66: stdModeName = "时装衣服(男)"; break;
                case 67: stdModeName = "时装衣服(女)"; break;
                case 68:
                case 69: stdModeName = "时装武器"; break;
                case 70:
                case 71:
                case 72:
                case 73:
                case 74: stdModeName = "称号"; break;
                case 75:
                case 76:
                case 77: stdModeName = "时装项链"; break;
                case 78: stdModeName = "时装头盔"; break;
                case 79:
                case 80: stdModeName = "时装手镯"; break;
                case 81:
                case 82: stdModeName = "时装戒指"; break;
                case 83: stdModeName = "时装勋章"; break;
                case 84:
                case 85: stdModeName = "时装腰带"; break;
                case 86:
                case 87: stdModeName = "时装靴子"; break;
                case 88:
                case 89: stdModeName = "时装宝石"; break;
                case 29:
                case 90: stdModeName = "翅膀"; break;
                case 96: stdModeName = "祝福罐"; break;
                case 111: stdModeName = "隐身"; break;
                case 112: stdModeName = "传送"; break;
                case 113: stdModeName = "麻痹"; break;
                case 114: stdModeName = "复活"; break;
                case 115: stdModeName = "火焰"; break;
                case 116: stdModeName = "治愈"; break;
                case 117:
                case 170: stdModeName = "不掉物品"; break;
                case 118: stdModeName = "护身"; break;
                case 119: stdModeName = "超负载"; break;
                case 120: stdModeName = "技巧"; break;
                case 121: stdModeName = "探测"; break;
                case 122: stdModeName = "记忆戒指"; break;
                case 124: stdModeName = "记忆手镯"; break;
                case 125: stdModeName = "记忆头盔"; break;
                case 126: stdModeName = "祈祷手镯"; break;
                case 127: stdModeName = "祈祷项链"; break;
                case 128: stdModeName = "祈祷戒指"; break;
                case 129: stdModeName = "祈祷头盔"; break;
                case 130: stdModeName = "神秘戒指"; break;
                case 131: stdModeName = "神秘腰带"; break;
                case 132: stdModeName = "神秘头盔"; break;
                case 133: stdModeName = "魔血戒指"; break;
                case 134: stdModeName = "魔血手镯"; break;
                case 135: stdModeName = "魔血项链"; break;
                case 136: stdModeName = "虹魔戒指"; break;
                case 137: stdModeName = "虹魔戒指"; break;
                case 138: stdModeName = "虹魔项链"; break;
                case 139: stdModeName = "防麻"; break;
                case 140: stdModeName = "无敌"; break;
                case 141: stdModeName = "经验"; break;//杀怪所得经验为指定倍数(倍数为持久除以 10000)，如果物品持久太小，所得经验将比正常还低
                case 142: stdModeName = "力量"; break;//人物攻击力倍数(倍数为持久除以 10000)，如果物品持久太小，所得攻击力将比正常还低
                case 182: stdModeName = "经验"; break;//杀怪所得经验为指定倍数(倍数为持久除以 10000)，不受物品持久太小影响
                case 183: stdModeName = "力量"; break;//人物攻击力倍数(倍数为持久除以 10000)，不受物品持久太小影响
                case 143: stdModeName = "防护身"; break;
                case 144: stdModeName = "防复活"; break;
                case 145: stdModeName = "行会召唤"; break;
                case 146: stdModeName = "防红绿毒"; break;
                case 147: stdModeName = "防诱惑"; break;
                case 148: stdModeName = "防火墙"; break;
                case 150: stdModeName = "麻痹护身"; break;
                case 151: stdModeName = "麻痹火球"; break;
                case 152: stdModeName = "麻痹防御"; break;
                case 153: stdModeName = "麻痹负载"; break;
                case 154: stdModeName = "护身火焰"; break;
                case 155: stdModeName = "护身防御"; break;
                case 156: stdModeName = "护身负载"; break;
                case 157: stdModeName = "传送麻痹"; break;
                case 158: stdModeName = "传送护身"; break;
                case 159: stdModeName = "传送探测"; break;
                case 160: stdModeName = "传送复活"; break;
                case 161: stdModeName = "复活麻痹"; break;
                case 162: stdModeName = "护身复活"; break;
                case 171: stdModeName = "不掉背包物品"; break;
                case 172: stdModeName = "不掉身上装备"; break;
                case 201: stdModeName = "倚天辟地技能(武器)"; break;
                case 200: stdModeName = "流星火雨技能(首饰)"; break;
                case 202: stdModeName = "防御"; break;
                case 203: stdModeName = "魔御"; break;
                case 204: stdModeName = "魔道麻痹"; break;
                case 205: stdModeName = "冰冻"; break;
                case 206: stdModeName = "蜘蛛网"; break;
                case 207: stdModeName = "防冰冻"; break;
                case 208: stdModeName = "防蜘蛛网"; break;
                case 209: stdModeName = "魔道麻痹 + 复活"; break;
            }
            itemX += 55;
            itemY += 15;
            list.Add($"1#类型:{stdModeName}#{itemX}#{itemY}#10#White");

            itemY += 18;
            switch (stdItem.StdMode)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 7:
                case 31:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 96: list.Add($"1#持久:0#{itemX}#{itemY}#10#White"); break;
                default: list.Add($"1#持久:{(stdItem.Duramax / 1000)}/{(stdItem.Duramax + stdItem.SubAttr12) / 1000}#{itemX}#{itemY}#10#White"); break;
            }

            #region 装备加星
            if (stdItem.StdStarAttr != null)
            {
                newpath = prgusePath + "001710.png";
                itemX -= 55;
                itemY += 20;
                for (int i = 0; i < stdItem.StdStarAttr.Count; i++)
                {
                    list.Add($"2#{newpath}#{itemX + (i * 18)}#{itemY}");
                }
                itemX -= 13;
                itemY += 15;
                list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");
            }
            else
            {
                itemX -= 69;
                itemY += 20;
                list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");
            }
            #endregion

            #region 基础属性
            //itemX += 10;
            //itemY += 5;
            //list.Add($"1#[基础属性]#{itemX}#{itemY}#10#White");

            itemX += 12;
            itemY += 2;
            newpath = fenghaoBtnPath + "000382.png";
            list.Add($"2#{newpath}#{itemX}#{itemY}");
            itemY += 3;
            list.Add($"1# #{itemX}#{itemY}#9#White");
            if (stdItem.StdAC > 0 || stdItem.StdAC2 > 0 || stdItem.SubAttr1 > 0)
            {
                switch (stdItem.StdMode)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 7:
                    case 31:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 49:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 70:
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                    case 96: break;
                    default:
                        itemY += 15;
                        list.Add($"1#防御:       {stdItem.StdAC}-{stdItem.StdAC2 + stdItem.SubAttr1}#{itemX + 2}#{itemY}#10#White");
                        if (stdItem.SubAttr1 > 0)
                        {
                            list.Add($"1#(+{stdItem.SubAttr1})#{itemX + 150}#{itemY}#10#LawnGreen");
                        }
                        break;
                }
            }

            if (stdItem.StdMac > 0 || stdItem.StdMac2 > 0 || stdItem.SubAttr2 > 0)
            {
                switch (stdItem.StdMode)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 7:
                    case 31:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 49:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 70:
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                    case 96: break;
                    default:
                        itemY += 15;
                        list.Add($"1#魔御:       {stdItem.StdMac}-{stdItem.StdMac2 + stdItem.SubAttr2}#{itemX + 2}#{itemY}#10#White");
                        if (stdItem.SubAttr2 > 0)
                        {
                            list.Add($"1#(+{stdItem.SubAttr2})#{itemX + 150}#{itemY}#10#LawnGreen");
                        }
                        break;
                }
            }

            if (stdItem.StdDc > 0 || stdItem.StdDc2 > 0 || stdItem.SubAttr3 > 0)
            {
                itemY += 15;

                list.Add($"1#攻击力:     {stdItem.StdDc}-{stdItem.StdDc2 + stdItem.SubAttr3}#{itemX + 2}#{itemY}#10#White");
                if (stdItem.SubAttr3 > 0)
                {
                    list.Add($"1#(+{stdItem.SubAttr3})#{itemX + 150}#{itemY}#10#LawnGreen");
                }
            }

            if (stdItem.StdMc > 0 || stdItem.StdMc2 > 0 || stdItem.SubAttr4 > 0)
            {
                itemY += 15;
                list.Add($"1#魔法:       {stdItem.StdMc}-{stdItem.StdMc2 + stdItem.SubAttr4}#{itemX + 2}#{itemY}#10#White");
                if (stdItem.SubAttr4 > 0)
                {
                    list.Add($"1#(+{stdItem.SubAttr4})#{itemX + 150}#{itemY}#10#LawnGreen");
                }
            }

            if (stdItem.StdSc > 0 || stdItem.StdSc2 > 0 || stdItem.SubAttr5 > 0)
            {
                itemY += 15;
                list.Add($"1#道术:       {stdItem.StdSc}-{stdItem.StdSc2 + stdItem.SubAttr5}#{itemX + 2}#{itemY}#10#White");
                if (stdItem.SubAttr5 > 0)
                {
                    list.Add($"1#(+{stdItem.SubAttr5})#{itemX + 150}#{itemY}#10#LawnGreen");
                }
            }

            if (stdItem.Accuracy > 0 || stdItem.SubAttr6 > 0)
            {
                itemY += 15;
                list.Add($"1#准确:#{itemX + 2}#{itemY}#10#White");

                if (stdItem.SubAttr6 > 0)
                    list.Add($"1#+{stdItem.Accuracy + stdItem.SubAttr6}#{itemX + 85}#{itemY}#10#LawnGreen");
                else
                    list.Add($"1#+{stdItem.Accuracy}#{itemX + 85}#{itemY}#10#White");
            }

            if (stdItem.Agile > 0 || stdItem.SubAttr7 > 0)
            {
                itemY += 15;
                list.Add($"1#敏捷:#{itemX + 2}#{itemY}#10#White");

                if (stdItem.SubAttr7 > 0)
                    list.Add($"1#+{stdItem.Agile + stdItem.SubAttr7}#{itemX + 85}#{itemY}#10#LawnGreen");
                else
                    list.Add($"1#+{stdItem.Agile}#{itemX + 85}#{itemY}#10#White");
            }

            if (stdItem.Strength > 0 || stdItem.SubAttr13 > 0)
            {
                itemY += 15;
                list.Add($"1#强度:#{itemX + 2}#{itemY}#10#White");
                if (stdItem.SubAttr13 > 0)
                    list.Add($"1#+{stdItem.Strength + stdItem.SubAttr13}#{itemX + 85}#{itemY}#10#LawnGreen");
                else
                    list.Add($"1#+{stdItem.Strength}#{itemX + 85}#{itemY}#10#White");
            }

            if (stdItem.Lucky > 0 || stdItem.SubAttr8 > 0)
            {
                itemY += 15;
                list.Add($"1#幸运:#{itemX + 2}#{itemY}#10#White");
                if (stdItem.SubAttr8 > 0)
                    list.Add($"1#+{stdItem.Lucky + stdItem.SubAttr8}#{itemX + 85}#{itemY}#10#LawnGreen");
                else
                    list.Add($"1#+{stdItem.Lucky}#{itemX + 85}#{itemY}#10#White");
            }

            if (stdItem.StdHP > 0)
            {
                itemY += 15;
                if (stdItem.StdMode == 0)
                {
                    list.Add($"1#HP:#{itemX + 2}#{itemY}#10#White");
                }
                else
                {
                    list.Add($"1#体力值:#{itemX + 2}#{itemY}#10#White");
                }
                list.Add($"1#+{stdItem.StdHP}#{itemX + 85}#{itemY}#10#White");
            }
            if (stdItem.StdMP > 0)
            {
                itemY += 15;
                if (stdItem.StdMode == 0)
                {
                    list.Add($"1#MP:#{itemX + 2}#{itemY}#10#White");
                }
                else
                {
                    list.Add($"1#魔法值:#{itemX + 2}#{itemY}#10#White");
                }
                list.Add($"1#+{stdItem.StdMP}#{itemX + 85}#{itemY}#10#White");
            }

            if (stdItem.SubAttr9 > 0)
            {
                itemY += 15;
                list.Add($"1#倍功:#{itemX + 2}#{itemY}#10#White");
                list.Add($"1#+{stdItem.SubAttr9}#{itemX + 85}#{itemY}#10#White");
            }

            if (stdItem.SubAttr10 > 0)
            {
                itemY += 15;

                list.Add($"1#爆率:#{itemX + 2}#{itemY}#10#White");
                list.Add($"1#+{stdItem.SubAttr10}#{itemX + 85}#{itemY}#10#White");
            }
            if (stdItem.SubAttr11 > 0)
            {
                itemY += 15;
                list.Add($"1#负重:#{itemX + 2}#{itemY}#10#White");
                list.Add($"1#+{stdItem.SubAttr11}#{itemX + 85}#{itemY}#10#White");
            }
            if (stdItem.SubAttr13 > 0)
            {
                itemY += 15;
                list.Add($"1#强度:#{itemX + 2}#{itemY}#10#White");
                list.Add($"1#+{stdItem.SubAttr13}#{itemX + 85}#{itemY}#10#White");
            }
            if (stdItem.SubAttr14 > 0)
            {
                itemY += 15;
                list.Add($"1#HP:#{itemX + 2}#{itemY}#10#White");
                list.Add($"1#+{stdItem.SubAttr14}#{itemX + 85}#{itemY}#10#White");
            }
            if (stdItem.OverLap > 0)
            {
                itemY += 15;
                list.Add($"1#数量:#{itemX + 2}#{itemY}#10#White");
                list.Add($"1#{stdItem.SubAttr1}#{itemX + 85}#{itemY}#10#White");
            }

            itemY += 15;
            list.Add($"1#重量:#{itemX + 2}#{itemY}#10#White");
            list.Add($"1#{stdItem.Weight}#{itemX + 85}#{itemY}#10#White");
            switch (stdItem.StdMode)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 7:
                case 31:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 96: break;
                case 49:
                    itemY += 15;
                    list.Add($"1#经验:{stdItem.SubAttr2}/{stdItem.Need * stdItem.Price}#{itemX + 2}#{itemY}#9#GreenYellow");
                    itemY += 15;
                    list.Add($"1#杀死怪物即可存储经验,存储满后自动释放,释放需要{stdItem.Price}金币#{itemX + 2}#{itemY}#9#Thistle");
                    itemY += 15;
                    list.Add($"1# #{itemX + 2}#{itemY}#9#Thistle");
                    break;
                case 4:
                    List<MagicDB> magicDBList = LegendApi.GetMagicList();
                    MagicDB magic = magicDBList.Where(m => m.MagName == stdName).First();

                    itemY += 15;
                    list.Add($"1#职业:#{itemX + 2}#{itemY}#10#White");
                    list.Add($"1#{magic.Job}:#{itemX + 85}#{itemY}#10#White");
                    itemY += 15;
                    list.Add($"1#需要等级:#{itemX + 2}#{itemY}#10#White");
                    list.Add($"1#{magic.NeedL1}#{itemX + 85}#{itemY}#10#White");
                    break;
                default:
                    switch (stdItem.Need)
                    {
                        case 0:
                            itemY += 15;
                            list.Add($"1#需要等级:#{itemX + 2}#{itemY}#10#White");
                            break;
                        case 1:
                            itemY += 15;
                            list.Add($"1#需要攻击力:#{itemX + 2}#{itemY}#10#White");
                            break;
                        case 2:
                            itemY += 15;
                            list.Add($"1#需要魔法:#{itemX + 2}#{itemY}#10#White");
                            break;
                        case 3:
                            itemY += 15;
                            list.Add($"1#需要道术:#{itemX + 2}#{itemY}#10#White");
                            break;
                    }
                    list.Add($"1#{stdItem.Needlevel}#{itemX + 85}#{itemY}#10#White");
                    break;
            }
            #endregion

            #region 元素属性
            if (stdItem.StdElemAttr != null)
            {
                if (stdItem.StdElemAttr.MagicEvas > 0
                    || stdItem.StdElemAttr.AttackSpeed > 0
                    || stdItem.StdElemAttr.MoveSpeed > 0
                    || stdItem.StdElemAttr.PhysicalDamageReduc > 0
                    || stdItem.StdElemAttr.MagicDamageReduc > 0
                    || stdItem.StdElemAttr.PhysicalDamageReflex > 0
                    || stdItem.StdElemAttr.MagicDamageReflex > 0
                    || stdItem.StdElemAttr.HealsPR > 0
                    || stdItem.StdElemAttr.AttackPR > 0
                    || stdItem.StdElemAttr.MagicAC > 0
                    || stdItem.StdElemAttr.IgnoreAC > 0
                    || stdItem.StdElemAttr.DamageRebound > 0
                    || stdItem.StdElemAttr.UserExpRate > 0
                    || stdItem.StdElemAttr.HPIncrease > 0
                    || stdItem.StdElemAttr.MPIncrease > 0
                    || stdItem.StdElemAttr.MonExpRate > 0
                    || stdItem.StdElemAttr.EquBurstRate > 0
                    || stdItem.StdElemAttr.FatalInjury > 0
                    || stdItem.StdElemAttr.FatalBlow > 0
                    || stdItem.StdElemAttr.ExcellentBlow > 0
                    || stdItem.StdElemAttr.HeartfeltBlow > 0)
                {
                    itemX -= 12;
                    itemY += 15;
                    list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");
                    //itemX += 10;
                    //itemY += 5;
                    //list.Add($"1#[元素属性]#{itemX}#{itemY}#10#Khaki");

                    itemX += 12;
                    itemY += 2;
                    newpath = fenghaoBtnPath + "000394.png";
                    list.Add($"2#{newpath}#{itemX}#{itemY}");
                    itemY += 5;
                    list.Add($"1# #{itemX}#{itemY}#9#White");

                }
                if (stdItem.StdElemAttr.MagicEvas > 0)
                {
                    itemY += 15;
                    list.Add($"1#魔法躲避     +{stdItem.StdElemAttr.MagicEvas}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.AttackSpeed > 0)
                {
                    itemY += 15;
                    list.Add($"1#攻击速度     +{stdItem.StdElemAttr.AttackSpeed}#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.MoveSpeed > 0)
                {
                    itemY += 15;
                    list.Add($"1#移动速度     +{stdItem.StdElemAttr.MoveSpeed}#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.PhysicalDamageReduc > 0)
                {
                    itemY += 15;
                    list.Add($"1#物理伤害减少 {stdItem.StdElemAttr.PhysicalDamageReduc}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.MagicDamageReduc > 0)
                {
                    itemY += 15;
                    list.Add($"1#魔法伤害减少 {stdItem.StdElemAttr.MagicDamageReduc}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.PhysicalDamageReflex > 0)
                {
                    itemY += 15;
                    list.Add($"1#人物物伤反射 {stdItem.StdElemAttr.PhysicalDamageReflex}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.MagicDamageReflex > 0)
                {
                    itemY += 15;
                    list.Add($"1#人物魔伤反射 {stdItem.StdElemAttr.MagicDamageReflex}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.HealsPR > 0)
                {
                    itemY += 15;
                    list.Add($"1#暴击几率增加 {stdItem.StdElemAttr.HealsPR}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.AttackPR > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加攻击伤害 {stdItem.StdElemAttr.AttackPR}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.MagicAC > 0)
                {
                    itemY += 15;
                    list.Add($"1#人物魔法防御 {stdItem.StdElemAttr.MagicAC}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.IgnoreAC > 0)
                {
                    itemY += 15;
                    list.Add($"1#忽视目标防御 {stdItem.StdElemAttr.IgnoreAC}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.DamageRebound > 0)
                {
                    itemY += 15;
                    list.Add($"1#所有伤害反弹 {stdItem.StdElemAttr.DamageRebound}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.UserExpRate > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加人物爆率 {stdItem.StdElemAttr.UserExpRate}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.HPIncrease > 0)
                {
                    itemY += 15;
                    list.Add($"1#人物体力增加 {stdItem.StdElemAttr.HPIncrease}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.MPIncrease > 0)
                {
                    itemY += 15;
                    list.Add($"1#人物魔力增加 {stdItem.StdElemAttr.MPIncrease}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.MonExpRate > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加怪物爆率 {stdItem.StdElemAttr.MonExpRate}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.EquBurstRate > 0)
                {
                    itemY += 15;
                    list.Add($"1#人物防爆出率 {stdItem.StdElemAttr.EquBurstRate}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.FatalInjury > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加致命伤害 {stdItem.StdElemAttr.FatalInjury}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.FatalBlow > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加致命一击 {stdItem.StdElemAttr.FatalBlow}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.ExcellentBlow > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加卓越一击 {stdItem.StdElemAttr.ExcellentBlow}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
                if (stdItem.StdElemAttr.HeartfeltBlow > 0)
                {
                    itemY += 15;
                    list.Add($"1#增加会心一击 {stdItem.StdElemAttr.HeartfeltBlow}%#{itemX + 2}#{itemY}#10#LightSkyBlue");
                }
            }
            #endregion

            #region 技能属性

            if (stdItem.StdMagicAttr != null)
            {
                itemX -= 12;
                itemY += 15;
                list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");

                //itemX += 10;
                //itemY += 5;
                //list.Add($"1#[技能属性]#{itemX}#{itemY}#10#Yellow");

                itemX += 12;
                itemY += 2;
                newpath = fenghaoBtnPath + "000388.png";
                list.Add($"2#{newpath}#{itemX}#{itemY}");
                itemY += 5;
                list.Add($"1# #{itemX}#{itemY}#9#White");

                for (int i = 0; i < stdItem.StdMagicAttr.Count; i++)
                {
                    switch (stdItem.StdMode)
                    {
                        case 5:
                        case 6:
                            newpath = magIcon2Path + stdItem.StdMagicAttr[i].MagImg;
                            if (i == 0)
                                itemY += 15;
                            else
                                itemY += 20;
                            list.Add($"2#{newpath}#{itemX + 5}#{itemY}");
                            list.Add($"1#{stdItem.StdMagicAttr[i].MagName}威力增加 +{stdItem.StdMagicAttr[i].AttackPR}%#{itemX + 25}#{itemY + 5}#10#Thistle");
                            break;
                        case 10:
                        case 11:
                            newpath = magIcon2Path + stdItem.StdMagicAttr[i].MagImg;

                            if (i == 0)
                                itemY += 15;
                            else
                                itemY += 20;
                            list.Add($"2#{newpath}#{itemX + 5}#{itemY}");
                            list.Add($"1#{stdItem.StdMagicAttr[i].MagName}威力减免 +{stdItem.StdMagicAttr[i].AttackPR}%#{itemX + 25}#{itemY + 5}#10#Thistle");
                            break;
                    }
                }
            }
            #endregion

            #region 隐藏属性
            int hidY = 0;
            if (stdItem.StdHiddAttr != null)
            {
                itemX -= 12;
                itemY += 22;
                list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");

                //itemX += 10;
                //itemY += 5;
                //list.Add($"1#[隐藏属性]#{itemX}#{itemY}#10#OrangeRed");

                itemX += 12;
                itemY += 2;
                newpath = fenghaoBtnPath + "000390.png";
                list.Add($"2#{newpath}#{itemX}#{itemY}");
                itemY += 5;
                list.Add($"1# #{itemX}#{itemY}#9#White");

                string[] hidTyle = null;
                string res = "";
                if (!string.IsNullOrEmpty(stdItem.StdHiddAttr.HidImg))
                {
                    newpath = magIcon2Path + stdItem.StdHiddAttr.HidImg;
                    itemX += 5;
                    itemY += 15;
                    list.Add($"2#{newpath}#{itemX}#{itemY}");
                    stdItem.StdHiddAttr.HidTitle = "     " + stdItem.StdHiddAttr.HidTitle;
                    res = LegendApi.InsertNewlinesByByteLength(gh, font10, stdItem.StdHiddAttr.HidTitle, 150);
                }
                else
                {
                    res = LegendApi.InsertNewlinesByByteLength(gh, font10, stdItem.StdHiddAttr.HidTitle, 150);
                }

                if (res.Split('#').Length > 0)
                    hidY = (res.Split('#').Length - 1) * 15;
                res = res.Replace("#", "");
                itemX -= 5;
                itemY += 15;

                list.Add($"1#{res}#{itemX}#{itemY}#10#OrangeRed");
            }

            #endregion

            itemX -= 12;
            itemY += 15 + hidY;
            list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");

            //itemX += 10;
            //itemY += 5;
            //list.Add($"1#[物品来源]#{itemX}#{itemY}#10#Yellow");

            itemX += 12;
            itemY += 2;
            newpath = fenghaoBtnPath + "000384.png";
            list.Add($"2#{newpath}#{itemX}#{itemY}");
            itemY += 5;
            list.Add($"1# #{itemX}#{itemY}#9#White");

            itemY += 15;
            list.Add($"1#怪物：{stdItem.StdItemStuff}#{itemX + 2}#{itemY}#10#Yellow");
            itemY += 15;
            list.Add($"1#地图：{stdItem.StdItemMap}#{itemX + 2}#{itemY}#10#Yellow");
            itemY += 15;
            list.Add($"1#角色：{stdItem.StdItemOwner}#{itemX + 2}#{itemY}#10#Yellow");
            itemY += 15;
            list.Add($"1#时间：{stdItem.StdItemTime}#{itemX + 2}#{itemY}#10#Yellow");

            itemX -= 12;
            itemY += 15;
            list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");

            itemX += 12;
            itemY += 5;
            list.Add($"1#卖给商店：{stdItem.Price}金币#{itemX + 2}#{itemY}#10#Wheat");

            itemX -= 12;
            itemY += 15;
            list.Add($"3#DimGray#{itemX + 12}#{itemY}#{itemX + 188}#{itemY}");

            itemX += 12;
            itemY += 5;
            list.Add($"4#1.76 咕咕鸡独家复古神技#{itemX + 2}#{itemY}#10#Yellow#200#20");

            // 设置矩形的填充颜色为半透明（Alpha = 100）
            Color transparentColor = Color.FromArgb(235, 0, 0, 0); // 透明度为100
            SolidBrush transparentBrush = new SolidBrush(transparentColor);

            newpath = newopUIPath + "000044.png";

            #region 背景
            string first = list[0];
            string last = list[list.Count - 1];
            var arr = last.Split('#');
            var arr2 = first.Split('#');
            int boxH = int.Parse(arr[3]) - int.Parse(arr2[3]) + 40;
            int boxW = 200;

            // 绘制一个矩形
            gh.FillRectangle(transparentBrush, posX, posY, boxW, boxH);

            using (Image img = Image.FromFile(newpath))// 线-上
            {
                gh.DrawImage(img, posX, posY, boxW, img.Height);
            }
            using (Image img = Image.FromFile(newpath))// 线-下
            {
                gh.DrawImage(img, posX, posY + boxH - (img.Height / 2), boxW, img.Height);
            }

            newpath = newopUIPath + "000045.png";
            using (Image img = Image.FromFile(newpath))// 线-左
            {
                gh.DrawImage(img, posX, posY, img.Width, boxH);
            }
            using (Image img = Image.FromFile(newpath))// 线-右
            {
                gh.DrawImage(img, posX + boxW, posY, img.Width, boxH);
            }

            newpath = newopUIPath + "000042.png";
            using (Image img = Image.FromFile(newpath))// 左上角框框
            {
                gh.DrawImage(img, posX, posY, img.Width, img.Height);
            }
            newpath = newopUIPath + "000043.png";
            using (Image img = Image.FromFile(newpath))// 左下角框框
            {
                gh.DrawImage(img, posX, posY + boxH - img.Width, img.Width, img.Height);
            }
            newpath = newopUIPath + "000040.png";
            using (Image img = Image.FromFile(newpath))// 右上角框框
            {
                gh.DrawImage(img, posX + boxW - (img.Height / 2) - 1, posY, img.Width, img.Height);
            }
            newpath = newopUIPath + "000041.png";
            using (Image img = Image.FromFile(newpath))// 右下角框框
            {
                gh.DrawImage(img, posX + boxW - (img.Height / 2) - 1, posY + boxH - img.Width + 1, img.Width, img.Height);
            }

            newpath = newopUIPath + "000100.png";
            using (Image img = Image.FromFile(newpath))// 物品名称框
            {
                gh.DrawImage(img, posX + 12, posY + 8, img.Width, img.Height);
            }
            newpath = newopUIPath + "000047.png";
            using (Image img = Image.FromFile(newpath))// 物品框
            {
                gh.DrawImage(img, posX + 25, posY + 30, img.Width, img.Height);
            }

            newpath = itemsPath + stdItem.StdImgPath;

            using (Image img = Image.FromFile(newpath))// 物品
            {
                gh.DrawImage(img, posX - (img.Width / 2) + 47, posY - (img.Height / 2) + 50, img.Width, img.Height);
            }

            if (isFlag)
            {
                newpath = newopUIPath + "001905.png";
                using (Image img = Image.FromFile(newpath))// 已装备
                {
                    gh.DrawImage(img, posX + 146, posY + 2, img.Width, img.Height);
                }
            }

            #endregion


            #region 加载内容
            Pen pen = new Pen(Color.DimGray, 1);
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            int c = 0;
            foreach (var item in list)
            {
                arr = item.Split('#');
                int type = int.Parse(arr[0]);

                switch (type)
                {
                    case 1:
                        itemX = int.Parse(arr[2]);
                        itemY = int.Parse(arr[3]);
                        rect = new Rectangle(itemX, itemY, 180, 0);
                        if (c == 0)
                        {
                            TextHelper.DrawCenteredString(gh, arr[1], font10, Brushes.White, rect);
                        }
                        else
                        {
                            Font font;
                            switch (arr[4])
                            {
                                case "8": font = new Font("黑体", 8); break;
                                case "9": font = new Font("黑体", 9); break;
                                case "10": font = new Font("黑体", 10); break;
                                case "11": font = new Font("黑体", 11); break;
                                case "12": font = new Font("黑体", 12); break;
                                case "13": font = new Font("黑体", 13); break;
                                case "14": font = new Font("黑体", 14); break;
                                default: font = new Font("黑体", 10); break;
                            }
                            switch (arr[5])
                            {
                                case "White": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.White, rect); break;
                                case "Thistle": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Thistle, rect); break;
                                case "LightSkyBlue": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.LightSkyBlue, rect); break;
                                case "Khaki": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Khaki, rect); break;
                                case "LawnGreen": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.LawnGreen, rect); break;
                                case "GreenYellow": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.GreenYellow, rect); break;
                                case "OrangeRed": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.OrangeRed, rect); break;
                                case "Yellow": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Yellow, rect); break;
                                case "Wheat": TextHelper.DrawLeftString(gh, arr[1], font, Brushes.Wheat, rect); break;
                                default: TextHelper.DrawLeftString(gh, arr[1], font, Brushes.White, rect); break;
                            }
                        }
                        break;
                    case 2:
                        itemX = int.Parse(arr[2]);
                        itemY = int.Parse(arr[3]);
                        using (Image img = Image.FromFile(arr[1]))
                        {
                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                        }
                        break;
                    case 3:
                        int x1 = int.Parse(arr[2]);
                        int y1 = int.Parse(arr[3]);
                        int x2 = int.Parse(arr[4]);
                        int y2 = int.Parse(arr[5]);
                        gh.DrawLine(pen, x1, y1, x2, y2);
                        break;
                    case 4:
                        itemX = int.Parse(arr[2]);
                        itemY = int.Parse(arr[3]);
                        rect = new Rectangle(itemX, itemY, 0, 0);
                        Rectangle ract = new Rectangle(itemX, itemY, boxH, 20);
                        TextHelper.DrawLinearGradientString(gh, arr[1], font10, rect, ract, Color.Red, Color.Yellow);
                        break;
                }
                c++;

            }
            #endregion
        }

        /// <summary>
        /// 卖肉
        /// </summary>
        public string GetSellingMeatIMG(UserInfo user, List<StditemDB> listResult, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 生成卖肉对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(12, 10, 0, 0);
                            TextHelper.DrawLeftString(gh, "最近我这里可以卖肉,你要卖肉吗？\n\n我会出高价钱购买!", font9, brush, rect);//文字

                            using (Image img = Image.FromFile(prgusePath + "000392.png"))// ok框
                            {
                                gh.DrawImage(img, 302, 188, img.Width, img.Height);
                            }
                            #endregion

                            #region 生成背包图片
                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))
                            {
                                gh.DrawImage(img, 557, 10, img.Width, img.Height);
                            }

                            //加载背包里的物品
                            rect = new Rectangle(618, 290, 0, 0);
                            TextHelper.DrawLeftString(gh, user.User_General.JB.ToString(), font8, brush, rect);//金币

                            rect = new Rectangle(625, 325, 0, 0);
                            TextHelper.DrawLeftString(gh, "  元宝：" + user.User_General.YB, font8, brush, rect);//元宝 

                            rect = new Rectangle(625, 345, 0, 0);
                            TextHelper.DrawLeftString(gh, "金刚石：" + user.User_General.JGS, font8, brush, rect);//金刚石

                            rect = new Rectangle(850, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  充值：" + user.User_General.RegAmt.ToString("N0", CultureInfo.InvariantCulture), font13, brush, rect);//真实充值

                            StditemDB stditemDB = null;

                            string newpath = UserPath + user.UserQQ + ".ini";
                            IniFileHelper iniFileHelper = new IniFileHelper(newpath);
                            List<string> listBwg = new List<string>();
                            listBwg = iniFileHelper.GetAllKeys("背包物品");

                            int i = 0, humX = 0, humY = 0, textX = 0, textY = 0;

                            listBwg.ForEach(bwg =>
                            {
                                stditemDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("背包物品", bwg));

                                if (stditemDB != null)
                                {
                                    newpath = itemsPath + stditemDB.StdImgPath;
                                    using (Image img = Image.FromFile(newpath))   //加载背包里的物品
                                    {
                                        switch (i / 12)
                                        {
                                            case 0:
                                                humX = (i * 35) - (img.Width / 2) + 594;
                                                humY = 44 - (img.Height / 2) - 6;
                                                textX = (i * 35) + 612;
                                                textY = 32;
                                                break;
                                            case 1:
                                                humX = (i - 12) * 35 - (img.Width / 2) + 594;
                                                humY = 77 - (img.Height / 2) - 6;
                                                textX = (i - 12) * 35 + 612;
                                                textY = 65;
                                                break;
                                            case 2:
                                                humX = (i - 24) * 35 - (img.Width / 2) + 594;
                                                humY = 108 - (img.Height / 2) - 6;
                                                textX = (i - 24) * 35 + 612;
                                                textY = 98;
                                                break;
                                            case 3:
                                                humX = (i - 36) * 35 - (img.Width / 2) + 594;
                                                humY = 139 - (img.Height / 2) - 6;
                                                textX = (i - 36) * 35 + 612;
                                                textY = 127;
                                                break;
                                            case 4:
                                                humX = (i - 48) * 35 - (img.Width / 2) + 594;
                                                humY = 170 - (img.Height / 2) - 6;
                                                textX = (i - 48) * 35 + 612;
                                                textY = 160;
                                                break;
                                            case 5:
                                                humX = (i - 60) * 35 - (img.Width / 2) + 594;
                                                humY = 204 - (img.Height / 2) - 6;
                                                textX = (i - 60) * 35 + 612;
                                                textY = 193;
                                                break;
                                            case 6:
                                                humX = (i - 72) * 35 - (img.Width / 2) + 594;
                                                humY = 237 - (img.Height / 2) - 6;
                                                textX = (i - 72) * 35 + 612;
                                                textY = 226;
                                                break;
                                            case 7:
                                                humX = (i - 84) * 35 - (img.Width / 2) + 594;
                                                humY = 270 - (img.Height / 2) - 6;
                                                textX = (i - 84) * 35 + 612;
                                                textY = 259;
                                                break;
                                            default:
                                                break;
                                        }

                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                    }

                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                    {
                                        rect = new Rectangle(textX, textY, 0, 0);
                                        TextHelper.DrawRightString(gh, stditemDB.SubAttr1.ToString(), font9, brushLimeGreen, rect);
                                    }
                                    i++;
                                    if (stditemDB.StdMode == 40)
                                    {
                                        iniFileHelper.Delete("背包物品", bwg);
                                    }
                                }
                            });

                            stditemDB = listResult[0];
                            newpath = itemsPath + stditemDB.StdImgPath;
                            using (Image img = Image.FromFile(newpath))
                            {
                                gh.DrawImage(img, 345, 270, img.Width, img.Height);
                            }

                            rect = new Rectangle(305, 195, 0, 0);
                            TextHelper.DrawLeftString(gh, "卖：" + stditemDB.Price, font8, brush, rect);//卖的金额

                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 用户打怪方法生成动图
        /// </summary>
        public async Task<string> GetAttackMonster(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, false, false, false, x, y, w, h, lv);

                int humX = 512, humY = 284, weaponX = 512, weaponY = 284, monX, monY, kmX = 640, kmY = 205, kmtX = 638, kmtY = 191,
                    hpX1 = 630, hpY1 = 250, hpX2 = 640, hpY2 = 250, index = 1, kmIndex = 1, magicX = 512, magicY = 284,
                    num = 0, monNum = 0, monHP = 0, playerNum = 0, playerHP = 0, att1 = 0, att2 = 0, att3 = 0, att4 = 0;
                double f = 0, t = 0;
                int delay = 10;
                string playerImg1 = "", weaponImg1 = "", magicImg = "", path = "";
                string userIniPath = UserPath + user.UserQQ + ".ini";
                IniFileHelper userINI = new IniFileHelper(userIniPath);
                user.KillMon.MonName = Regex.Replace(user.KillMon.MonName, @"\d+$", "");
                List<string> userImgList = userInfoImg.GetPlayerImgList(user, userINI, 0);// 人物图片集合
                List<string> userWeaponImgList = userInfoImg.GetPlayerWeaponImgList(user, userINI, 0);// 人物武器图片集合

                string player1 = userImgList[16];// 人物站立图
                string playerWeapon1 = "";// 人物站立武器图
                if (userWeaponImgList.Count > 0)
                    playerWeapon1 = userWeaponImgList[16];

                MemoryStream ms = new MemoryStream();
                MagickImage magickImage = null;
                MagickImage blendImage = null;
                Rectangle rect = new Rectangle(202, 472, 390, 15);
                Rectangle sourceRectangle = new Rectangle(202, 472, 390, 15);
                SolidBrush brushWhite = new SolidBrush(Color.White);
                SolidBrush brushGreenYellow = new SolidBrush(Color.GreenYellow);
                SolidBrush brushZB = new SolidBrush(ColorTranslator.FromHtml("#f13131"));
                List<string> magImgList = new List<string>();// 人物施法效果集合
                List<string> runImgList = new List<string>();// 人物跑动图片
                List<string> runWeaponImgList = new List<string>();// 人物跑动武器图片
                List<string> player1AttedList = userImgList.Skip(488).Take(3).ToList();// 人物挨打图片
                List<string> player1AttedWeaponList = new List<string>();// 人物挨打武器图片
                if (userWeaponImgList.Count > 0)
                    player1AttedWeaponList = userWeaponImgList.Skip(488).Take(3).ToList();

                List<string> player1DeadList = userImgList.Skip(552).Take(4).ToList();// 人物死亡图片
                List<string> player1DeadWeaponList = new List<string>();// 人物死亡武器图片
                if (userWeaponImgList.Count > 0)
                    player1DeadWeaponList = userWeaponImgList.Skip(488).Take(3).ToList();
                List<string> monImgList = MonsterApi.GetMonsterImgList(user.KillMon.MonID);// 怪物图片集合
                List<string> monAttedImgList = monImgList.Skip(252).Take(2).ToList();// 怪物挨打图片集合
                List<string> monAttctImgList = monImgList.Skip(220).Take(6).ToList();// 怪物攻击图片集合
                List<string> monDeadImgList = monImgList.Skip(320).Take(10).ToList();// 怪物死亡图片集合
                string monPath = monImgList[60];

                Font font13 = new Font("黑体", 13);
                SolidBrush brushLimeGreen = new SolidBrush(Color.Lime);

                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                string kulouPath = "";
                                MagicDB magic;
                                user.User_General.SumExp -= user.KillMon.Exp;
                                long p1HP = 0;
                                user.UserAttackRecordList.ForEach(userAttR =>
                                {
                                    index = 0;
                                    num = 0;
                                    blendImage = null;
                                    runImgList = userImgList.Skip(216).Take(6).ToList();// 取砍图(人物)
                                    if (userWeaponImgList.Count > 0)
                                        runWeaponImgList = userWeaponImgList.Skip(216).Take(6).ToList();// 取砍图(武器)

                                    magic = userAttR.MagicDBModel1;
                                    if (magic != null)// 如果魔法效果不为空则使用魔法攻击效果图
                                    {
                                        switch (magic.MagName)
                                        {
                                            case "基本剑法":
                                            case "精神力战法":
                                                break;
                                            case "攻杀剑术":
                                            case "刺杀剑术":
                                            case "半月弯刀":
                                            case "烈火剑法":
                                                magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取魔法效果
                                                break;
                                            case "野蛮冲撞":
                                                magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取魔法效果
                                                runImgList = userImgList.Skip(144).Take(6).ToList();// 取跑图（人物）
                                                runImgList.AddRange(userImgList.Skip(16).Take(4).ToList());// 取站图（人物）
                                                runWeaponImgList = userWeaponImgList.Skip(144).Take(6).ToList();// 取跑图（武器）
                                                runWeaponImgList.AddRange(userWeaponImgList.Skip(16).Take(4).ToList());// 取站图（武器）
                                                break;
                                            default:
                                                magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取魔法效果
                                                runImgList = userImgList.Skip(408).Take(6).ToList();// 取施法人物图（人物）
                                                runImgList.AddRange(userImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                runImgList.AddRange(userImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                runImgList.AddRange(userImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                runImgList.AddRange(userImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                runWeaponImgList = userWeaponImgList.Skip(408).Take(6).ToList();// 取施法武器图（武器）
                                                runWeaponImgList.AddRange(userWeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                runWeaponImgList.AddRange(userWeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                runWeaponImgList.AddRange(userWeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                runWeaponImgList.AddRange(userWeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                break;
                                        }
                                    }

                                    // 人物攻击
                                    runImgList.ForEach(p =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置怪物
                                        monX = 580 + int.Parse(monPath.Split(',')[1]);
                                        monY = 284 + int.Parse(monPath.Split(',')[2]);

                                        using (Image mon = Image.FromFile(monPath.Split(',')[0]))// 设置怪物位置
                                        {
                                            gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 怪物血条
                                        {
                                            gh.DrawImage(img, 587, 230, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 怪物血条-掉血
                                        {
                                            if (monHP > 0)
                                                rect = new Rectangle(0, 0, monHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, 587, 230, rect, GraphicsUnit.Pixel);
                                        }
                                        rect = new Rectangle(603, 265, 0, 0);

                                        TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名
                                        #endregion

                                        #region 设置自己
                                        if (user.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user.Sex == "男")
                                                weaponX = 512 + int.Parse(runWeaponImgList[num].Split(',')[1]);
                                            else
                                                weaponX = 515 + int.Parse(runWeaponImgList[num].Split(',')[1]);

                                            weaponY = 284 + int.Parse(runWeaponImgList[num].Split(',')[2]);
                                            weaponImg1 = runWeaponImgList[num].Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg1))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg1 = humPath + p.Split(',')[0];
                                        humX = 512 + int.Parse(p.Split(',')[1]);
                                        humY = 284 + int.Parse(p.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }
                                        int hX = 521, hY = 230;
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            if (playerHP > 0)
                                                rect = new Rectangle(0, 0, playerHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        if (user.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }

                                        rect = new Rectangle(hX + 15, hY - 5, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                        if (user.Job == "战士" && user.User_General.Lv < 28)
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                            using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                            {
                                                gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                            }
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(30, 714 - (int)t, img.Width, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, img.Width, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        else
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;

                                                rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                            // MP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(85, 714 - (int)t, 60, img.Height);
                                                sourceRectangle = new Rectangle(60, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }

                                        rect = new Rectangle(30, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                        rect = new Rectangle(535, 277, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 


                                        using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                        {
                                            f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                            t = 72 * f / 100; //满经验72
                                            if (f < 0) f = 0;
                                            rect = new Rectangle(0, 0, (int)t, img.Height);
                                            gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);
                                            rect = new Rectangle(968, 700, 0, 0);
                                            TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                        }

                                        rect = new Rectangle(920, 692, 0, 0);
                                        if (user.User_General.SumExp < 0) user.User_General.SumExp = 0;
                                        TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验

                                        #endregion

                                        #region 攻击提示
                                        if (userAttR.p_attack1 > 0)
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            if (magic != null)
                                                TextHelper.DrawCenteredString(gh, $"{user.Name} 使用 {magic.MagName} 打掉怪物 {user.KillMon.MonName} {userAttR.p_attack1}血!", font20, brushGreenYellow, rect);
                                            else
                                                TextHelper.DrawCenteredString(gh, $"{user.Name} 使用 普通攻击 打掉怪物 {user.KillMon.MonName} {userAttR.p_attack1}血!", font20, brushGreenYellow, rect);
                                        }
                                        else
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            TextHelper.DrawCenteredString(gh, $"{user.KillMon.MonName} {userAttR.p_msg1}", font20, brushGreenYellow, rect);
                                        }
                                        #endregion


                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = delay;

                                        if (user.IsUseSkills == "是" && magImgList.Count > 0)
                                        {
                                            // 显示技能特效图片
                                            magicImg = magImgList[0].Split(',')[0];
                                            if (user.Sex == "男")
                                                magicX = 512 + int.Parse(magImgList[0].Split(',')[1]);
                                            else
                                                magicX = 515 + int.Parse(magImgList[0].Split(',')[1]);
                                            if (magic != null)// 如果魔法效果不为空则使用魔法攻击效果图
                                            {
                                                switch (magic.MagName)
                                                {
                                                    case "灵魂火符":
                                                        break;
                                                    case "雷电术":
                                                        if (num >= 2 && num <= 4)
                                                        {
                                                            magicY = 284 + int.Parse(magImgList[0].Split(',')[2]);
                                                            using (Image magicIMG = Image.FromFile(magicImg))
                                                            {
                                                                gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                            }

                                                            bitmap.Save(ms, ImageFormat.Png);
                                                            ms.Seek(0, SeekOrigin.Begin);
                                                            blendImage = new MagickImage(ms);
                                                            blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                            blendImage.AnimationDelay = delay;
                                                            magImgList.RemoveAt(0);
                                                        }
                                                        break;
                                                    case "抗拒火环":
                                                        if (num >= 2 && num <= 7)
                                                        {
                                                            magicY = 284 + int.Parse(magImgList[0].Split(',')[2]);
                                                            using (Image magicIMG = Image.FromFile(magicImg))
                                                            {
                                                                gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                            }

                                                            bitmap.Save(ms, ImageFormat.Png);
                                                            ms.Seek(0, SeekOrigin.Begin);
                                                            blendImage = new MagickImage(ms);
                                                            blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                            blendImage.AnimationDelay = delay;
                                                            magImgList.RemoveAt(0);
                                                        }
                                                        break;
                                                    default:
                                                        magicY = 284 + int.Parse(magImgList[0].Split(',')[2]);
                                                        using (Image magicIMG = Image.FromFile(magicImg))
                                                        {
                                                            gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                        }

                                                        bitmap.Save(ms, ImageFormat.Png);
                                                        ms.Seek(0, SeekOrigin.Begin);
                                                        blendImage = new MagickImage(ms);
                                                        blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                        blendImage.AnimationDelay = delay;
                                                        magImgList.RemoveAt(0);
                                                        break;
                                                }
                                            }
                                        }

                                        if (blendImage == null) collection.Add(magickImage);
                                        else
                                        {
                                            collection.Add(blendImage);
                                            blendImage = null;
                                        }

                                        num++;
                                    });

                                    num = 0;
                                    blendImage = null;
                                    playerImg1 = humPath + runImgList[num].Split(',')[0];
                                    humX = 512 + int.Parse(runImgList[num].Split(',')[1]);
                                    humY = 284 + int.Parse(runImgList[num].Split(',')[2]);

                                    if (user.IsUseWeapon)// 设置人物武器
                                    {
                                        if (user.Sex == "男")
                                            weaponX = 512 + int.Parse(runWeaponImgList[num].Split(',')[1]);
                                        else
                                            weaponX = 515 + int.Parse(runWeaponImgList[num].Split(',')[1]);

                                        weaponY = 284 + int.Parse(runWeaponImgList[num].Split(',')[2]);
                                        weaponImg1 = runWeaponImgList[num].Split(',')[0];
                                    }

                                    // 魔法效果
                                    magImgList.ForEach(m =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置怪物
                                        monX = 580 + int.Parse(monPath.Split(',')[1]);
                                        monY = 284 + int.Parse(monPath.Split(',')[2]);

                                        using (Image mon = Image.FromFile(monPath.Split(',')[0]))// 设置怪物位置
                                        {
                                            gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 怪物血条
                                        {
                                            gh.DrawImage(img, 587, 230, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 怪物血条-掉血
                                        {
                                            if (monHP > 0)
                                                rect = new Rectangle(0, 0, monHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, 587, 230, rect, GraphicsUnit.Pixel);
                                        }
                                        rect = new Rectangle(603, 265, 0, 0);

                                        TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名
                                        #endregion

                                        #region 设置自己

                                        if (user.IsUseWeapon)// 设置人物武器
                                        {
                                            using (Image weapon = Image.FromFile(weaponImg1))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }
                                        int pWidth = 0;
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            pWidth = player.Width;
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }
                                        int hX = 521, hY = 230;
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            if (playerHP > 0)
                                                rect = new Rectangle(0, 0, playerHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX + 15, hY - 5, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                        if (user.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }
                                        if (user.Job == "战士" && user.User_General.Lv < 28)
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                            using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                            {
                                                gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                            }
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(30, 714 - (int)t, img.Width, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, img.Width, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        else
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;

                                                rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                            // MP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(85, 714 - (int)t, 60, img.Height);
                                                sourceRectangle = new Rectangle(60, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }

                                        rect = new Rectangle(30, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                        rect = new Rectangle(535, 277, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 

                                        using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                        {
                                            f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                            t = 72 * f / 100; //满经验72
                                            if (f < 0) f = 0;
                                            rect = new Rectangle(0, 0, (int)t, img.Height);
                                            gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);
                                            rect = new Rectangle(968, 700, 0, 0);
                                            TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                        }

                                        rect = new Rectangle(920, 692, 0, 0);
                                        if (user.User_General.SumExp < 0) user.User_General.SumExp = 0;
                                        TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验

                                        #region 攻击提示
                                        if (userAttR.p_attack1 > 0)
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            if (magic != null)
                                                TextHelper.DrawCenteredString(gh, $"{user.Name} 使用 {magic.MagName} 打掉怪物 {user.KillMon.MonName} {userAttR.p_attack1}血!", font20, brushGreenYellow, rect);
                                            else
                                                TextHelper.DrawCenteredString(gh, $"{user.Name} 使用 普通攻击 打掉怪物 {user.KillMon.MonName} {userAttR.p_attack1}血!", font20, brushGreenYellow, rect);
                                        }
                                        else
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            TextHelper.DrawCenteredString(gh, $"{user.KillMon.MonName} {userAttR.p_msg1}", font20, brushGreenYellow, rect);
                                        }
                                        #endregion

                                        #endregion

                                        // 显示技能特效图片
                                        magicImg = m.Split(',')[0];
                                        if (user.Sex == "男")
                                            magicX = 512 + int.Parse(m.Split(',')[1]);
                                        else
                                            magicX = 515 + int.Parse(m.Split(',')[1]);

                                        magicY = 294 + int.Parse(m.Split(',')[2]);
                                        if (magic != null)// 如果魔法效果不为空则使用魔法攻击效果图
                                        {
                                            switch (magic.MagName)
                                            {
                                                case "施毒术":
                                                case "冰咆哮":
                                                case "圣言术":
                                                case "爆裂火焰":
                                                case "火墙":
                                                case "诱惑之光":
                                                case "雷电术":
                                                    magicX += (pWidth / 2) + 16;
                                                    using (Image magicIMG = Image.FromFile(magicImg))
                                                    {
                                                        gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                    }

                                                    bitmap.Save(ms, ImageFormat.Png);
                                                    ms.Seek(0, SeekOrigin.Begin);
                                                    blendImage = new MagickImage(ms);
                                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                    blendImage.AnimationDelay = 3;
                                                    collection.Add(blendImage);
                                                    break;
                                                case "地狱火":
                                                case "火球术":
                                                case "大火球":
                                                case "灵魂火符":
                                                    if (num < 6) magicX += (pWidth / 2) + num * 2;
                                                    else
                                                    {
                                                        magicX += (pWidth / 2) + 15;
                                                        magicY -= 10;
                                                    }
                                                    using (Image magicIMG = Image.FromFile(magicImg))
                                                    {
                                                        gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                    }

                                                    bitmap.Save(ms, ImageFormat.Png);
                                                    ms.Seek(0, SeekOrigin.Begin);
                                                    blendImage = new MagickImage(ms);
                                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                    collection.Add(blendImage);
                                                    break;
                                                case "召唤骷髅":
                                                    magicX += 50;
                                                    magicY += 50;
                                                    kulouPath = magImgList[magImgList.Count - 1];
                                                    using (Image magicIMG = Image.FromFile(magicImg))
                                                    {
                                                        gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                    }

                                                    bitmap.Save(ms, ImageFormat.Png);
                                                    ms.Seek(0, SeekOrigin.Begin);
                                                    blendImage = new MagickImage(ms);
                                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                    blendImage.AnimationDelay = 8;
                                                    collection.Add(blendImage);
                                                    break;
                                                case "召唤神兽":
                                                    magicX += 50;
                                                    magicY += 50;
                                                    using (Image magicIMG = Image.FromFile(magicImg))
                                                    {
                                                        gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                    }

                                                    bitmap.Save(ms, ImageFormat.Png);
                                                    ms.Seek(0, SeekOrigin.Begin);
                                                    magickImage = new MagickImage(ms);
                                                    magickImage.AnimationDelay = 8;
                                                    collection.Add(magickImage);
                                                    break;
                                                default:
                                                    using (Image magicIMG = Image.FromFile(magicImg))
                                                    {
                                                        gh.DrawImage(magicIMG, magicX, magicY, magicIMG.Width, magicIMG.Height);
                                                    }

                                                    bitmap.Save(ms, ImageFormat.Png);
                                                    ms.Seek(0, SeekOrigin.Begin);
                                                    blendImage = new MagickImage(ms);
                                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                                    blendImage.AnimationDelay = 10;
                                                    collection.Add(blendImage);
                                                    break;
                                            }
                                        }
                                        num++;
                                    });

                                    num = 0;
                                    playerImg1 = humPath + runImgList[num].Split(',')[0];
                                    humX = 512 + int.Parse(runImgList[num].Split(',')[1]);
                                    humY = 284 + int.Parse(runImgList[num].Split(',')[2]);
                                    if (user.IsUseWeapon)// 设置人物武器
                                    {
                                        if (user.Sex == "男")
                                            weaponX = 512 + int.Parse(runWeaponImgList[num].Split(',')[1]);
                                        else
                                            weaponX = 515 + int.Parse(runWeaponImgList[num].Split(',')[1]);

                                        weaponY = 284 + int.Parse(runWeaponImgList[num].Split(',')[2]);
                                        weaponImg1 = runWeaponImgList[num].Split(',')[0];
                                    }
                                    hpX1 = 630;
                                    hpX2 = 640;
                                    // 怪物被打
                                    monAttedImgList.ForEach(m =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置怪物
                                        monX = 580 + int.Parse(m.Split(',')[1]);
                                        monY = 284 + int.Parse(m.Split(',')[2]);

                                        using (Image mon = Image.FromFile(m.Split(',')[0]))// 设置怪物位置
                                        {
                                            gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 怪物血条
                                        {
                                            gh.DrawImage(img, 587, 230, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 怪物血条-掉血
                                        {
                                            f = (double)userAttR.p_hp2 / user.KillMon.HP * 100;
                                            t = img.Width * f / 100;
                                            monHP = (int)t;
                                            if (monHP < 0) monHP = 0;
                                            rect = new Rectangle(0, 0, monHP, img.Height);
                                            gh.DrawImage(img, 587, 230, rect, GraphicsUnit.Pixel);
                                        }
                                        rect = new Rectangle(603, 265, 0, 0);

                                        TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名

                                        // 飘血
                                        if (userAttR.p_attack1 >= 1 && userAttR.p_attack1 < 10)// 攻击在10以内
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[userAttR.p_attack1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        else if (userAttR.p_attack1 >= 10 && userAttR.p_attack1 < 100)
                                        {
                                            att1 = int.Parse(userAttR.p_attack1.ToString().Substring(0, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att2 = int.Parse(userAttR.p_attack1.ToString().Substring(1, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        else if (userAttR.p_attack1 >= 100 && userAttR.p_attack1 < 1000)
                                        {
                                            att1 = int.Parse(userAttR.p_attack1.ToString().Substring(0, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att2 = int.Parse(userAttR.p_attack1.ToString().Substring(1, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att3 = int.Parse(userAttR.p_attack1.ToString().Substring(2, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 16, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        else if (userAttR.p_attack1 >= 1000 && userAttR.p_attack1 < 10000)
                                        {
                                            att1 = int.Parse(userAttR.p_attack1.ToString().Substring(0, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att2 = int.Parse(userAttR.p_attack1.ToString().Substring(1, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att3 = int.Parse(userAttR.p_attack1.ToString().Substring(2, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att3]))
                                            {
                                                gh.DrawImage(img, hpX2 + 16, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att4 = int.Parse(userAttR.p_attack1.ToString().Substring(3, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att4]))
                                            {
                                                gh.DrawImage(img, hpX2 + 24, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        #endregion

                                        #region 设置自己
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }
                                        if (user.IsUseWeapon)// 设置人物武器
                                        {
                                            using (Image weapon = Image.FromFile(weaponImg1))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }
                                        int hX = 521, hY = 230;
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            if (playerHP > 0)
                                                rect = new Rectangle(0, 0, playerHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX + 15, hY - 5, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                        if (user.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }
                                        if (user.Job == "战士" && user.User_General.Lv < 28)
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                            using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                            {
                                                gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                            }
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(30, 714 - (int)t, img.Width, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, img.Width, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        else
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;

                                                rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                            // MP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(85, 714 - (int)t, 60, img.Height);
                                                sourceRectangle = new Rectangle(60, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        rect = new Rectangle(30, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                        rect = new Rectangle(535, 277, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 

                                        using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                        {
                                            f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                            t = 72 * f / 100; //满经验72
                                            rect = new Rectangle(0, 0, (int)t, img.Height);
                                            gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);

                                            rect = new Rectangle(968, 700, 0, 0);
                                            TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                        }
                                        rect = new Rectangle(920, 692, 0, 0);
                                        TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验

                                        #endregion

                                        #region 攻击提示
                                        if (userAttR.p_attack1 > 0)
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "000060.png"))// 减号
                                            {
                                                gh.DrawImage(img, hpX1, hpY1 - (index - 1) * 5, img.Width, img.Height);
                                            }

                                            rect = new Rectangle(550, 150, 0, 40);
                                            if (magic != null)
                                                TextHelper.DrawCenteredString(gh, $"{user.Name} 使用 {magic.MagName} 打掉怪物 {user.KillMon.MonName} {userAttR.p_attack1}血!", font20, brushGreenYellow, rect);
                                            else
                                                TextHelper.DrawCenteredString(gh, $"{user.Name} 使用 普通攻击 打掉怪物 {user.KillMon.MonName} {userAttR.p_attack1}血!", font20, brushGreenYellow, rect);
                                        }
                                        else
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            TextHelper.DrawCenteredString(gh, $"{user.Name} 攻击 {user.KillMon.MonName} 触发 {userAttR.p_msg1}", font20, brushGreenYellow, rect);
                                        }
                                        #endregion

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = 18;
                                        collection.Add(magickImage);
                                        index++;
                                    });

                                    if (userAttR.p_hp2 > 0)
                                    {
                                        num = 0;
                                        playerImg1 = humPath + runImgList[num].Split(',')[0];
                                        humX = 512 + int.Parse(runImgList[num].Split(',')[1]);
                                        humY = 284 + int.Parse(runImgList[num].Split(',')[2]);

                                        if (user.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user.Sex == "男")
                                                weaponX = 512 + int.Parse(runWeaponImgList[num].Split(',')[1]);
                                            else
                                                weaponX = 515 + int.Parse(runWeaponImgList[num].Split(',')[1]);

                                            weaponY = 284 + int.Parse(runWeaponImgList[num].Split(',')[2]);
                                            weaponImg1 = runWeaponImgList[num].Split(',')[0];
                                        }
                                        // 怪物攻击
                                        monAttctImgList.ForEach(m =>
                                        {
                                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                            #region 设置怪物
                                            monX = 580 + int.Parse(m.Split(',')[1]);
                                            monY = 284 + int.Parse(m.Split(',')[2]);
                                            using (Image mon = Image.FromFile(m.Split(',')[0]))// 设置怪物位置
                                            {
                                                gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 怪物血条
                                            {
                                                gh.DrawImage(img, 587, 230, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 怪物血条-掉血
                                            {
                                                rect = new Rectangle(0, 0, monHP, img.Height);
                                                gh.DrawImage(img, 587, 230, rect, GraphicsUnit.Pixel);
                                            }
                                            rect = new Rectangle(603, 265, 0, 0);
                                            TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名
                                            #endregion

                                            #region 设置自己
                                            using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                            {
                                                gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                            }
                                            if (user.IsUseWeapon)// 设置人物武器
                                            {
                                                using (Image weapon = Image.FromFile(weaponImg1))
                                                {
                                                    gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                }
                                            }
                                            int hX = 521, hY = 230;
                                            using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                            {
                                                gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                            {
                                                if (playerHP > 0)
                                                    rect = new Rectangle(0, 0, playerHP, img.Height);
                                                else
                                                    rect = new Rectangle(0, 0, img.Width, img.Height);
                                                gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                            }

                                            if (user.IsOnYHZL == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                {
                                                    gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                }
                                            }

                                            if (user.IsOnTitle == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                {
                                                    gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                }
                                            }

                                            rect = new Rectangle(hX + 15, hY - 5, 0, 0);
                                            TextHelper.DrawCenterStrokeString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                            if (user.Job == "战士" && user.User_General.Lv < 28)
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                                using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                                {
                                                    gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                                }
                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;
                                                    rect = new Rectangle(30, 714 - (int)t, img.Width, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - (int)t, img.Width, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }
                                            }
                                            else
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;

                                                    rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }

                                                f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                                // MP
                                                using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;
                                                    rect = new Rectangle(85, 714 - (int)t, 60, img.Height);
                                                    sourceRectangle = new Rectangle(60, img.Height - (int)t, 60, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }
                                            }
                                            rect = new Rectangle(30, 730, 0, 0);
                                            TextHelper.DrawLeftString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                            rect = new Rectangle(100, 730, 0, 0);
                                            TextHelper.DrawLeftString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                            rect = new Rectangle(535, 277, 0, 0);
                                            TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 

                                            using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                            {
                                                f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                                t = 72 * f / 100; //满经验72
                                                rect = new Rectangle(0, 0, (int)t, img.Height);
                                                gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);

                                                rect = new Rectangle(968, 700, 0, 0);
                                                TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                            }
                                            rect = new Rectangle(920, 692, 0, 0);
                                            TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验
                                            #endregion

                                            #region 攻击提示
                                            if (userAttR.p_attack2 > 0)
                                            {
                                                rect = new Rectangle(550, 150, 0, 40);
                                                TextHelper.DrawCenteredString(gh, $"怪物 {user.KillMon.MonName} 使用 普通攻击 打掉你 {userAttR.p_attack2}血!", font20, brushGreenYellow, rect);
                                            }
                                            else
                                            {
                                                rect = new Rectangle(550, 150, 0, 40);
                                                TextHelper.DrawCenteredString(gh, $"怪物 {user.KillMon.MonName} 攻击 {user.Name} 触发 {userAttR.p_msg2}", font20, brushGreenYellow, rect);
                                            }
                                            #endregion

                                            bitmap.Save(ms, ImageFormat.Png);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            magickImage = new MagickImage(ms);
                                            magickImage.AnimationDelay = 15;
                                            collection.Add(magickImage);
                                        });

                                        num = 0;
                                        index = 0;
                                        // 人物被打
                                        player1AttedList.ForEach(p =>
                                        {
                                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                            #region 设置怪物
                                            monX = 580 + int.Parse(monPath.Split(',')[1]);
                                            monY = 284 + int.Parse(monPath.Split(',')[2]);

                                            using (Image mon = Image.FromFile(monPath.Split(',')[0]))// 设置怪物位置
                                            {
                                                gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 怪物血条
                                            {
                                                gh.DrawImage(img, 587, 230, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 怪物血条-掉血
                                            {
                                                rect = new Rectangle(0, 0, monHP, img.Height);
                                                gh.DrawImage(img, 587, 230, rect, GraphicsUnit.Pixel);
                                            }
                                            rect = new Rectangle(603, 265, 0, 0);

                                            TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名
                                            #endregion

                                            #region 设置自己
                                            playerImg1 = humPath + p.Split(',')[0];
                                            humX = 512 + int.Parse(p.Split(',')[1]);
                                            humY = 284 + int.Parse(p.Split(',')[2]);

                                            if (user.IsUseWeapon)// 设置人物武器
                                            {
                                                if (user.Sex == "男")
                                                    weaponX = 512 + int.Parse(player1AttedWeaponList[num].Split(',')[1]);
                                                else
                                                    weaponX = 515 + int.Parse(player1AttedWeaponList[num].Split(',')[1]);

                                                weaponY = 284 + int.Parse(player1AttedWeaponList[num].Split(',')[2]);
                                                weaponImg1 = player1AttedWeaponList[num].Split(',')[0];
                                            }

                                            using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                            {
                                                gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                            }
                                            if (user.IsUseWeapon)// 设置人物武器
                                            {
                                                using (Image weapon = Image.FromFile(weaponImg1))
                                                {
                                                    gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                }
                                            }

                                            int hX = 521, hY = 230;
                                            using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                            {
                                                gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;
                                                t = img.Width * f / 100;
                                                playerHP = (int)t;
                                                if (playerHP < 0) playerHP = 0;
                                                rect = new Rectangle(0, 0, playerHP, img.Height);
                                                gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                            }

                                            if (user.IsOnYHZL == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                {
                                                    gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                }
                                            }

                                            if (user.IsOnTitle == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                {
                                                    gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                }
                                            }

                                            rect = new Rectangle(hX + 15, hY - 5, 0, 0);
                                            TextHelper.DrawCenterStrokeString(gh, $"{user.User_Attribute.Hp}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                            if (user.Job == "战士" && user.User_General.Lv < 28)
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                                using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                                {
                                                    gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                                }
                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;
                                                    rect = new Rectangle(30, 714 - (int)t, img.Width, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - (int)t, img.Width, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }
                                            }
                                            else
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user.User_Attribute.MaxHp * 100;

                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;

                                                    rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }


                                                f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                                // MP
                                                using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;
                                                    rect = new Rectangle(85, 714 - (int)t, 60, img.Height);
                                                    sourceRectangle = new Rectangle(60, img.Height - (int)t, 60, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }
                                            }
                                            rect = new Rectangle(30, 730, 0, 0);
                                            TextHelper.DrawLeftString(gh, $"{userAttR.p_hp1}/{user.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                            rect = new Rectangle(100, 730, 0, 0);
                                            TextHelper.DrawLeftString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝
                                            rect = new Rectangle(535, 277, 0, 0);
                                            TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 

                                            using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                            {
                                                f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                                t = 72 * f / 100; //满经验72
                                                if (f < 0) f = 0;
                                                rect = new Rectangle(0, 0, (int)t, img.Height);
                                                gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);
                                                rect = new Rectangle(968, 700, 0, 0);
                                                TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                            }

                                            rect = new Rectangle(920, 692, 0, 0);
                                            if (user.User_General.SumExp < 0) user.User_General.SumExp = 0;
                                            TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验
                                            #endregion

                                            #region 飘血
                                            hpX1 = 490;
                                            hpX2 = 500;

                                            if (userAttR.p_attack2 >= 1 && userAttR.p_attack2 < 10)// 攻击在10以内
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[userAttR.p_attack2]))
                                                {
                                                    gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                            }
                                            else if (userAttR.p_attack2 >= 10 && userAttR.p_attack2 < 100)
                                            {
                                                att1 = int.Parse(userAttR.p_attack2.ToString().Substring(0, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                                {
                                                    gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                                att2 = int.Parse(userAttR.p_attack2.ToString().Substring(1, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                                {
                                                    gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                            }
                                            else if (userAttR.p_attack2 >= 100 && userAttR.p_attack2 < 1000)
                                            {
                                                att1 = int.Parse(userAttR.p_attack2.ToString().Substring(0, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                                {
                                                    gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                                att2 = int.Parse(userAttR.p_attack2.ToString().Substring(1, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                                {
                                                    gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                                att3 = int.Parse(userAttR.p_attack2.ToString().Substring(2, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                                {
                                                    gh.DrawImage(img, hpX2 + 16, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                            }
                                            else if (userAttR.p_attack2 >= 1000 && userAttR.p_attack2 < 10000)
                                            {
                                                att1 = int.Parse(userAttR.p_attack2.ToString().Substring(0, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                                {
                                                    gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                                att2 = int.Parse(userAttR.p_attack2.ToString().Substring(1, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                                {
                                                    gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                                att3 = int.Parse(userAttR.p_attack2.ToString().Substring(2, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att3]))
                                                {
                                                    gh.DrawImage(img, hpX2 + 16, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                                att4 = int.Parse(userAttR.p_attack2.ToString().Substring(3, 1));
                                                using (Image img = Image.FromFile(newopUIPath + HpImgDic[att4]))
                                                {
                                                    gh.DrawImage(img, hpX2 + 24, hpY2 - (index * 5), img.Width, img.Height);
                                                }
                                            }

                                            #endregion

                                            #region 攻击提示
                                            if (userAttR.p_attack2 > 0)
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "000060.png"))// 减号
                                                {
                                                    gh.DrawImage(img, hpX1, hpY1 - (index - 1) * 5, img.Width, img.Height);
                                                }
                                                rect = new Rectangle(550, 150, 0, 40);
                                                TextHelper.DrawCenteredString(gh, $"怪物 {user.KillMon.MonName} 使用 普通攻击 打掉你 {userAttR.p_attack2}血!", font20, brushGreenYellow, rect);
                                            }
                                            else
                                            {
                                                rect = new Rectangle(550, 150, 0, 40);
                                                TextHelper.DrawCenteredString(gh, $"怪物 {user.KillMon.MonName} 攻击 {user.Name} 触发 {userAttR.p_msg2}", font20, brushGreenYellow, rect);
                                            }
                                            #endregion

                                            bitmap.Save(ms, ImageFormat.Png);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            magickImage = new MagickImage(ms);
                                            magickImage.AnimationDelay = 18;
                                            collection.Add(magickImage);
                                            index++;
                                            num++;
                                        });
                                    }

                                    monNum = monHP;
                                    playerNum = playerHP;
                                    p1HP = userAttR.p_hp1;
                                });

                                if (user.User_Attribute.Hp > 0)
                                {
                                    playerImg1 = humPath + player1.Split(',')[0];
                                    humX = 512 + int.Parse(player1.Split(',')[1]);
                                    humY = 284 + int.Parse(player1.Split(',')[2]);

                                    if (user.IsUseWeapon)// 设置人物武器
                                    {
                                        if (user.Sex == "男")
                                            weaponX = 512 + int.Parse(playerWeapon1.Split(',')[1]);
                                        else
                                            weaponX = 515 + int.Parse(playerWeapon1.Split(',')[1]);

                                        weaponY = 284 + int.Parse(playerWeapon1.Split(',')[2]);
                                        weaponImg1 = playerWeapon1.Split(',')[0];
                                    }

                                    user.User_General.SumExp += user.KillMon.Exp;
                                    // 怪物死亡
                                    monDeadImgList.ForEach(m =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置怪物
                                        monX = 580 + int.Parse(m.Split(',')[1]);
                                        monY = 284 + int.Parse(m.Split(',')[2]);
                                        using (Image mon = Image.FromFile(m.Split(',')[0]))// 设置怪物位置
                                        {
                                            gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                        }
                                        rect = new Rectangle(603, 265, 0, 0);
                                        TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名
                                        #endregion

                                        #region 设置自己
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }
                                        if (user.IsUseWeapon)// 设置人物武器
                                        {
                                            using (Image weapon = Image.FromFile(weaponImg1))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        int hX = 521, hY = 230;
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            if (playerHP > 0)
                                                rect = new Rectangle(0, 0, playerHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        if (user.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }
                                        rect = new Rectangle(hX + 15, hY - 5, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{p1HP}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                        if (user.Job == "战士" && user.User_General.Lv < 28)
                                        {
                                            f = (double)user.User_Attribute.Hp / (double)user.User_Attribute.MaxHp * 100;

                                            using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                            {
                                                gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                            }
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(30, 714 - (int)t, img.Width, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, img.Width, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        else
                                        {
                                            f = (double)user.User_Attribute.Hp / (double)user.User_Attribute.MaxHp * 100;

                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;

                                                rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                            // MP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                rect = new Rectangle(85, 714 - (int)t, 60, img.Height);
                                                sourceRectangle = new Rectangle(60, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        rect = new Rectangle(30, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{p1HP}/{user.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        TextHelper.DrawLeftString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝
                                        rect = new Rectangle(535, 277, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 

                                        using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                        {
                                            f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                            t = 72 * f / 100; //满经验72
                                            rect = new Rectangle(0, 0, (int)t, img.Height);
                                            gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);

                                            rect = new Rectangle(968, 700, 0, 0);
                                            TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                        }
                                        rect = new Rectangle(920, 692, 0, 0);
                                        TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验
                                        #endregion

                                        #region 怪物死亡爆装备
                                        //为了防止装备漂移,初始位置要写怪物死亡循环里
                                        kmX = 640; kmY = 205; kmtX = 638; kmtY = 191;
                                        // 怪物爆的装备显示在地图上
                                        if (user.KillMon.MonItemList != null)
                                        {
                                            user.KillMon.MonItemList.ForEach(monItem =>
                                            {
                                                if (kmIndex <= 4)
                                                {
                                                    kmY += 25;
                                                    kmtY += 25;
                                                }
                                                else if (kmIndex > 4 && kmIndex <= 9)
                                                {
                                                    kmX -= 30;
                                                    kmtX -= 30;
                                                }
                                                else if (kmIndex > 9 && kmIndex <= 13)
                                                {
                                                    kmY -= 25;
                                                    kmtY -= 25;
                                                }
                                                else if (kmIndex > 13 && kmIndex <= 19)
                                                {
                                                    kmX += 30;
                                                    kmtX += 30;
                                                }
                                                else if (kmIndex > 19 && kmIndex <= 24)
                                                {
                                                    kmY += 25;
                                                    kmtY += 25;
                                                }
                                                else if (kmIndex > 24 && kmIndex <= 31)
                                                {
                                                    kmX -= 30;
                                                    kmtX -= 30;
                                                }
                                                else if (kmIndex > 31 && kmIndex <= 37)
                                                {
                                                    kmY -= 25;
                                                    kmtY -= 25;
                                                }
                                                else if (kmIndex > 37 && kmIndex <= 45)
                                                {
                                                    kmX += 30;
                                                    kmtX += 30;
                                                }
                                                else if (kmIndex > 45 && kmIndex <= 52)
                                                {
                                                    kmY += 25;
                                                    kmtY += 25;
                                                }
                                                else if (kmIndex > 52 && kmIndex <= 61)
                                                {
                                                    kmX -= 30;
                                                    kmtX -= 30;
                                                }
                                                else if (kmIndex > 61 && kmIndex <= 69)
                                                {
                                                    kmY -= 25;
                                                    kmtY -= 25;
                                                }
                                                else if (kmIndex > 69 && kmIndex <= 79)
                                                {
                                                    kmX += 30;
                                                    kmtX += 30;
                                                }
                                                else if (kmIndex > 79 && kmIndex <= 88)
                                                {
                                                    kmY += 25;
                                                    kmtY += 25;
                                                }
                                                else if (kmIndex > 88 && kmIndex <= 99)
                                                {
                                                    kmX -= 30;
                                                    kmtX -= 30;
                                                }
                                                else if (kmIndex > 99 && kmIndex <= 109)
                                                {
                                                    kmY -= 25;
                                                    kmtY -= 25;
                                                }
                                                else if (kmIndex > 109 && kmIndex <= 121)
                                                {
                                                    kmX += 30;
                                                    kmtX += 30;
                                                }
                                                else if (kmIndex > 121 && kmIndex <= 132)
                                                {
                                                    kmY += 25;
                                                    kmtY += 25;
                                                }
                                                else if (kmIndex > 132 && kmIndex <= 145)
                                                {
                                                    kmX -= 30;
                                                    kmtX -= 30;
                                                }
                                                else if (kmIndex > 145 && kmIndex <= 157)
                                                {
                                                    kmY -= 25;
                                                    kmtY -= 25;
                                                }
                                                else if (kmIndex > 157 && kmIndex <= 171)
                                                {
                                                    kmX += 30;
                                                    kmtX += 30;
                                                }
                                                else if (kmIndex > 171 && kmIndex <= 183)
                                                {
                                                    kmY += 25;
                                                    kmtY += 25;
                                                }
                                                using (Image img = Image.FromFile(dnItemsPath + monItem.StdImgPath))
                                                {
                                                    gh.DrawImage(img, kmX, kmY, img.Width, img.Height);
                                                }

                                                rect = new Rectangle(kmtX, kmtY, 0, 0);
                                                TextHelper.DrawLeftString(gh, monItem.StdName, font10, brushWhite, rect);

                                                kmIndex++;
                                            });
                                            kmIndex = 1;
                                        }
                                        rect = new Rectangle(520, 450, 0, 0);
                                        TextHelper.DrawCenteredString(gh, $"获得 {user.KillMon.Exp} 经验", font20, brushZB, rect);//获得经验
                                        #endregion

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = 12;
                                        collection.Add(magickImage);
                                    });
                                }
                                else
                                {
                                    // 人物死亡
                                    num = 0;
                                    player1DeadList.ForEach(p =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置怪物
                                        monX = 580 + int.Parse(monPath.Split(',')[1]);
                                        monY = 284 + int.Parse(monPath.Split(',')[2]);

                                        using (Image mon = Image.FromFile(monPath.Split(',')[0]))// 设置怪物位置
                                        {
                                            gh.DrawImage(mon, monX, monY, mon.Width, mon.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 怪物血条
                                        {
                                            gh.DrawImage(img, 587, 230, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 怪物血条-掉血
                                        {
                                            if (monHP > 0)
                                                rect = new Rectangle(0, 0, monHP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, 587, 230, rect, GraphicsUnit.Pixel);
                                        }
                                        rect = new Rectangle(603, 265, 0, 0);

                                        TextHelper.DrawCenteredString(gh, user.KillMon.MonName, font9, brushWhite, rect);//怪物名
                                        #endregion

                                        #region 设置自己
                                        if (user.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user.Sex == "男")
                                                weaponX = 512 + int.Parse(player1DeadWeaponList[num].Split(',')[1]);
                                            else
                                                weaponX = 515 + int.Parse(player1DeadWeaponList[num].Split(',')[1]);

                                            weaponY = 284 + int.Parse(player1DeadWeaponList[num].Split(',')[2]);
                                            weaponImg1 = player1DeadWeaponList[num].Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg1))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg1 = humPath + p.Split(',')[0];
                                        humX = 512 + int.Parse(p.Split(',')[1]);
                                        humY = 284 + int.Parse(p.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        rect = new Rectangle(535, 277, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brushWhite, rect);//用户名 

                                        rect = new Rectangle(humX + 15, humY - 22, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"0/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                        using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                        {
                                            f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                            t = 72 * f / 100; //满经验72
                                            if (f < 0) f = 0;
                                            rect = new Rectangle(0, 0, (int)t, img.Height);
                                            gh.DrawImage(img, 890, 698, rect, GraphicsUnit.Pixel);
                                            rect = new Rectangle(968, 700, 0, 0);
                                            TextHelper.DrawLeftString(gh, f.ToString("F2") + "%", font9, brushWhite, rect);//经验字
                                        }

                                        rect = new Rectangle(920, 692, 0, 0);
                                        if (user.User_General.SumExp < 0) user.User_General.SumExp = 0;
                                        TextHelper.DrawCenteredString(gh, user.User_General.SumExp.ToString(), font9, brushWhite, rect);//经验

                                        rect = new Rectangle(520, 450, 0, 0);
                                        TextHelper.DrawCenteredString(gh, $"你被 {user.KillMon.MonName} 杀死了!", font20, brushZB, rect);//获得经验
                                        #endregion

                                        num++;

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = 12;
                                        collection.Add(magickImage);
                                    });
                                }

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = 300;
                                if (user.User_Attribute.Hp <= 0)// 如果是人物死亡界面变灰
                                {
                                    magickImage.ColorSpace = ColorSpace.Gray;
                                    magickImage.Depth = 8; // 设置灰度深度为8位
                                }
                                collection.Add(magickImage);
                            }
                        }
                    }

                    path = UserInfoImgPath + user.UserQQ + ".gif";
                    ms.Dispose();
                    collection.OptimizeTransparency();// 优化gif
                    await collection.WriteAsync(path, MagickFormat.Gif);// 保存gif
                }
                if (magickImage != null)
                    magickImage.Dispose();
                if (blendImage != null)
                    blendImage.Dispose();

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查看附近怪物
        /// </summary>
        /// <returns></returns>
        public string GetNearbyMonsterIMG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string monPath = "";
                string path = "";
                if (user.User_General.Lv < 7)
                {
                    switch (user.MapCode)
                    {
                        case "1_0_1": user.UserImagePath = $"{LegendIMGDataPath}地图\\0\\银杏山谷\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                        case "1_0_2": user.UserImagePath = $"{LegendIMGDataPath}地图\\0\\边界村\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                        case "1_100": user.UserImagePath = $"{LegendIMGDataPath}地图\\0\\比奇城\\{user.MapCode}_{rnd.Next(1, 11)}.jpg"; break;
                    }

                    path = GamgeMainForm(user, true, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图

                }
                else
                {
                    path = Path.GetDirectoryName(user.UserImagePath);
                    var files = Directory.GetFiles(path);
                    user.UserImagePath = files[rnd.Next(0, files.Length - 1)];
                    path = GamgeMainForm(user, false, true, true, true, x, y, w, h, lv);//获取生成游戏主窗体图
                }

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 添加附近怪物
                            string mapStr = FileHelper.ReadTxt(mapPath);
                            var mapInfoList = Api.JsonDeserialize<List<LengendMapInfo>>(mapStr);
                            var mapInfo = mapInfoList.Where(mp => mp.LMI_MapCode == user.MapCode).FirstOrDefault();

                            string mapMonStr = FileHelper.ReadTxt(mapMonInfo);
                            var legMapMons = Api.JsonDeserialize<List<LegMapMonInfo>>(mapMonStr);
                            legMapMons = legMapMons.Where(t => t.LMI_ID == mapInfo.LMI_ID).ToList();

                            if (legMapMons.Count > 0)
                            {
                                int nd = rnd.Next(1, legMapMons.Count);//随机获取当前地图的怪物,最少1种怪
                                nd = legMapMons.Count - 1;
                                List<MonsterDB> monsterList = LegendApi.GetMonsterList();

                                Rectangle rect;
                                int monX, monY, nnnd;
                                for (int i = 0; i <= nd; i++)
                                {
                                    List<string> listMonStand = MonsterApi.GetMonsterStandIMG(legMapMons[i].MonID);
                                    if (listMonStand.Count > 0)
                                    {
                                        switch (legMapMons[i].MonID)
                                        {
                                            case 135:// 如果是祖玛教主就取第1张图
                                                nnnd = 0;
                                                break;
                                            default:
                                                nnnd = rnd.Next(1, listMonStand.Count);
                                                break;
                                        }
                                        monPath = listMonStand[nnnd].Split(',')[0];
                                        int monXX = int.Parse(listMonStand[nnnd].Split(',')[1]);
                                        int monYY = int.Parse(listMonStand[nnnd].Split(',')[2]);
                                        monX = rnd.Next(1, 900);
                                        monY = rnd.Next(1, 500);

                                        using (Image img = Image.FromFile(monPath))// 刷怪
                                        {
                                            gh.DrawImage(img, monX + monXX, monY + monYY, img.Width, img.Height);

                                            using (Image hpImg = Image.FromFile(newopUIPath + "000008.png"))// 血条
                                            {
                                                gh.DrawImage(hpImg, monX + 7, monY - 52, hpImg.Width, hpImg.Height);

                                                rect = new Rectangle(monX + 20, monY, 0, 0);
                                                if (legMapMons[i].HP > 1 && legMapMons[i].HP <= 1000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.White, rect);//怪物名
                                                }
                                                else if (legMapMons[i].HP > 1000 && legMapMons[i].HP <= 5000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.White, rect);//怪物名
                                                    using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"0000{rnd.Next(94, 98)}.png"))// 怪物封号标识
                                                    {
                                                        gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                    }
                                                }
                                                else if (legMapMons[i].HP > 5000 && legMapMons[i].HP <= 10000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.LimeGreen, rect);//怪物名
                                                    int n = rnd.Next(98, 102);
                                                    if (n < 100)
                                                    {
                                                        using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"0000{n}.png"))// 怪物封号标识
                                                        {
                                                            gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"000{n}.png"))// 怪物封号标识
                                                        {
                                                            gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                        }
                                                    }

                                                }
                                                else if (legMapMons[i].HP > 20000 && legMapMons[i].HP <= 50000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.DeepSkyBlue, rect);//怪物名
                                                    using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"000{rnd.Next(102, 106)}.png"))// 怪物封号标识
                                                    {
                                                        gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                    }
                                                }
                                                else if (legMapMons[i].HP > 50000 && legMapMons[i].HP <= 150000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.DeepPink, rect);//怪物名
                                                    using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"000{rnd.Next(106, 110)}.png"))// 怪物封号标识
                                                    {
                                                        gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                    }
                                                }
                                                else if (legMapMons[i].HP > 150000 && legMapMons[i].HP <= 500000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.Fuchsia, rect);//怪物名
                                                    using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"000{rnd.Next(110, 114)}.png"))// 怪物封号标识
                                                    {
                                                        gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                    }
                                                }
                                                else if (legMapMons[i].HP > 500000 && legMapMons[i].HP <= 1000000)
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.Yellow, rect);//怪物名
                                                    using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"000{rnd.Next(118, 122)}.png"))// 怪物封号标识
                                                    {
                                                        gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                    }
                                                }
                                                else
                                                {
                                                    TextHelper.DrawCenterStrokeString(gh, legMapMons[i].MonName, font13, Brushes.Red, rect);//怪物名
                                                    using (Image lxImg = Image.FromFile(fenghaoBtnPath + $"0000{rnd.Next(94, 98)}.png"))// 怪物封号标识
                                                    {
                                                        gh.DrawImage(lxImg, monX - 10, monY - 80, lxImg.Width, lxImg.Height);
                                                    }
                                                }
                                            }

                                            rect = new Rectangle(monX + 20, monY - 57, 0, 0);
                                            TextHelper.DrawCenterStrokeString(gh, $"{legMapMons[i].HP}/{legMapMons[i].HP}/D{legMapMons[i].Lvl}", font12, Brushes.White, rect);

                                        }
                                    }
                                    else
                                    {

                                    }
                                }

                            }

                            #endregion

                            path = $"{UserInfoImgPath + user.UserQQ}.png";
                            bitmap.Save(path, ImageFormat.Png);//保存图片
                        }
                    }

                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<string> GetWaList1()
        {
            List<string> list = new List<string>();
            for (int i = 380; i <= 392; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 360; i <= 375; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 360; i <= 375; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 360; i <= 375; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            return list;
        }
        private List<string> GetWaList2()
        {
            List<string> list = new List<string>();
            for (int i = 500; i <= 512; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 480; i <= 495; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 480; i <= 495; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 480; i <= 495; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            return list;
        }
        private List<string> GetWiList1()
        {
            List<string> list = new List<string>();
            for (int i = 420; i <= 432; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 400; i <= 415; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 400; i <= 415; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 400; i <= 415; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            return list;
        }
        private List<string> GetWiList2()
        {
            List<string> list = new List<string>();
            for (int i = 540; i <= 552; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 520; i <= 535; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 520; i <= 535; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 520; i <= 535; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            return list;
        }
        private List<string> GetTaList1()
        {
            List<string> list = new List<string>();
            for (int i = 460; i <= 472; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 440; i <= 455; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 440; i <= 455; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 440; i <= 455; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            return list;
        }
        private List<string> GetTaList2()
        {
            List<string> list = new List<string>();
            for (int i = 580; i <= 592; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 560; i <= 575; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 560; i <= 575; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            for (int i = 560; i <= 575; i++)
            {
                list.Add(i.ToString().PadLeft(6, '0'));
            }
            return list;
        }


        /// <summary>
        /// 创建主窗体
        /// </summary>
        public string GamgeMainForm(UserInfo user, bool isLoadOtherPlayer, bool isLoadMyself, bool isLoadExp = true, bool isLoadHP = true, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string savePath = UserPath + user.UserQQ + ".ini";
                IniFileHelper config = new IniFileHelper(savePath);

                #region 人物外观
                StditemDB stditemDB = null;
                int lvv = 0;
                if (user.User_Items.DRESS.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.DRESS));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.WEAPON.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.WEAPON));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.HELMET.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.HELMET));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.NECKLACE.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.NECKLACE));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RING_R.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_R));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.RING_L.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.RING_L));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.ARMRING_R.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_R));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                if (user.User_Items.ARMRING_L.Length > 1)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(config.ReadValue("装备属性", user.User_Items.ARMRING_L));
                    if (stditemDB.StdHiddAttr != null)
                    {
                        if (stditemDB.StdHiddAttr.Lv > 0)
                        {
                            lvv += stditemDB.StdHiddAttr.Lv;
                        }
                    }
                }
                #endregion

                using (Image mapImage = Image.FromFile(user.UserImagePath))//加载地图背景
                {
                    using (Bitmap bitmap = new Bitmap(mapImage.Width, mapImage.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(mapImage, 0, 0, mapImage.Width, mapImage.Height);// 定义地图画布

                            using (Image img = Image.FromFile(ui1Path + "004120.png"))// 主界面
                            {
                                gh.DrawImage(img, 0, 520, img.Width, img.Height);
                            }

                            int nd = rnd.Next(12, 16);
                            string dayANight = newopUIPath + nd.ToString().PadLeft(6, '0') + ".png";
                            using (Image img = Image.FromFile(dayANight))// 白天 晚上 随即取
                            {
                                gh.DrawImage(img, 972, 600, img.Width, img.Height);
                            }

                            using (Image img = Image.FromFile(prguse3Path + "000307.png"))//店铺
                            {
                                gh.DrawImage(img, 975, 723, img.Width, img.Height);
                            }

                            using (Image img = Image.FromFile(newopUIPath + "000003.png"))//内功能量
                            {
                                gh.DrawImage(img, 835, 610, img.Width, img.Height);
                            }

                            using (Image img = Image.FromFile(newopUIPath + "000020.png"))// 聊天框滚动条
                            {
                                gh.DrawImage(img, 805, 645, img.Width, img.Height);
                            }
                            using (Image img = Image.FromFile(newopUIPath + "000021.png"))// 聊天框滚动条
                            {
                                gh.DrawImage(img, 805, 638, img.Width, img.Height);
                            }
                            using (Image img = Image.FromFile(newopUIPath + "000025.png"))// 聊天框滚动条
                            {
                                gh.DrawImage(img, 805, 740, img.Width, img.Height);
                            }
                            using (Image img = Image.FromFile(newopUIPath + "000027.png"))// 聊天框滚动条
                            {
                                gh.DrawImage(img, 805, 726, img.Width, img.Height);
                            }

                            Color color = ColorTranslator.FromHtml("#136613");
                            Brush brush = new SolidBrush(color);
                            Brush brushWhite = new SolidBrush(Color.White);
                            Brush brushLime = new SolidBrush(Color.Lime);
                            Brush brushLimeGreen = new SolidBrush(Color.LawnGreen);
                            Rectangle rect = new Rectangle(205, 640, 0, 0);
                            double f = 0, n = 0;
                            Rectangle sourceRectangle = new Rectangle(0, 0, 0, 0);
                            GraphicsUnit unit = GraphicsUnit.Pixel;

                            #region 设置人物血量/蓝量
                            if (isLoadHP)
                            {
                                if (user.Job == "战士" && user.User_General.Lv < 28)
                                {
                                    f = (double)user.User_Attribute.Hp / (double)user.User_Attribute.MaxHp * 100;

                                    using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                    {
                                        gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                    }
                                    // HP
                                    using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                    {
                                        if (f < 0) f = 0;
                                        n = img.Height * f / 100;
                                        rect = new Rectangle(30, 714 - (int)n, img.Width, img.Height);
                                        sourceRectangle = new Rectangle(0, img.Height - (int)n, img.Width, img.Height);
                                        gh.DrawImage(img, rect, sourceRectangle, unit);
                                    }
                                }
                                else
                                {
                                    f = (double)user.User_Attribute.Hp / (double)user.User_Attribute.MaxHp * 100;

                                    // HP
                                    using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                    {
                                        if (f < 0) f = 0;
                                        n = img.Height * f / 100;

                                        rect = new Rectangle(28, 714 - (int)n, 57, img.Height);
                                        sourceRectangle = new Rectangle(0, img.Height - (int)n, 60, img.Height);
                                        gh.DrawImage(img, rect, sourceRectangle, unit);
                                    }

                                    f = (double)user.User_Attribute.Mp / (double)user.User_Attribute.MaxMp * 100;
                                    // MP
                                    using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                    {
                                        if (f < 0) f = 0;
                                        n = img.Height * f / 100;
                                        rect = new Rectangle(85, 714 - (int)n, 60, img.Height);
                                        sourceRectangle = new Rectangle(60, img.Height - (int)n, 60, img.Height);
                                        gh.DrawImage(img, rect, sourceRectangle, unit);
                                    }
                                }
                                int hX = 521, hY = 250;
                                using (Image img = Image.FromFile(newopUIPath + "000008.png"))
                                {
                                    if (f < 0) f = 0;
                                    n = img.Width * f / 100;
                                    rect = new Rectangle(0, 0, (int)n, img.Height);
                                    gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                }


                                rect = new Rectangle(25, 730, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, $"{user.User_Attribute.Hp}/{user.User_Attribute.MaxHp}", font13, brushWhite, rect);//红

                                rect = new Rectangle(90, 730, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, $"{user.User_Attribute.Mp}/{user.User_Attribute.MaxMp}", font13, brushWhite, rect);//蓝
                            }

                            #endregion

                            #region 设置文字
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("〖提示〗:[攻击模式:全体攻击]");
                            sb.AppendLine("〖提示〗:欢迎进入QQ引擎服务器进行游戏...");
                            sb.AppendLine("〖提示〗:当前服务器运行于正式开放模式,请不要开启加速、脱机等辅助外挂.(当然如果你能开的话)");
                            sb.AppendLine("〖提示〗:亲爱的玩家你好,欢迎加入【咕咕鸡传奇】,可加群玩服或者加机器人好友私聊玩服");
                            sb.AppendLine("〖提示〗:官方网站：暂无,有事联系QQ:745223613,910862143.QQ群：977049285");
                            rect = new Rectangle(205, 640, 0, 0);
                            TextHelper.DrawLeftString(gh, sb.ToString(), font9, brush, rect);

                            string qdpath = LegendFilePath.SignInPath + DateTime.Now.ToString("yyyyMMdd") + "_file.ini";
                            IniFileHelper iniFileHelper = new IniFileHelper(qdpath);
                            var list = iniFileHelper.GetAllKeys("用户签到列表");
                            var filelist = Directory.GetFiles(UserPath, "*.ini");
                            color = ColorTranslator.FromHtml("#ff0000");
                            brush = new SolidBrush(color);
                            rect = new Rectangle(210, 710, 0, 0);
                            TextHelper.DrawLeftString(gh, $"在线人数:{list.Count}/{filelist.Length}", font9, brush, rect);

                            color = ColorTranslator.FromHtml("#ffffff");
                            brush = new SolidBrush(color);


                            rect = new Rectangle(860, 752, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, DateTime.Now.ToString(), font12, brush, rect);

                            rect = new Rectangle(860, 635, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "[全体攻击模式]", font12, brush, rect);// 攻击模式

                            rect = new Rectangle(5, 752, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, $"{user.Map}({user.Pos})", font12, brush, rect);// 所在地图

                            if (lvv > 0)
                            {
                                rect = new Rectangle(895, 666, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, $"{user.User_General.Lv + lvv}(+{lvv})", font12, brushLime, rect);// 等级                               
                            }
                            else
                            {
                                rect = new Rectangle(895, 666, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, user.User_General.Lv.ToString(), font12, brush, rect);// 等级
                            }

                            if (isLoadExp)
                            {
                                rect = new Rectangle(900, 685, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, user.User_General.SumExp.ToString(), font12, brush, rect);// 经验

                                using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 经验图
                                {
                                    f = (double)user.User_General.SumExp / (double)user.User_General.NextExp * 100;
                                    n = img.Width * f / 100; //满经验72
                                    rect = new Rectangle(0, 0, (int)n, img.Height);

                                    gh.DrawImage(img, 890, 698, rect, unit);

                                    rect = new Rectangle(962, 696, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, f.ToString("F2") + "%", font12, brush, rect);// 经验字
                                }
                            }

                            using (Image img = Image.FromFile(newopUIPath + "000017.png"))// 背包图
                            {
                                f = (double)user.User_General.Bgw / user.User_General.BgwMax * 100;
                                n = img.Width * f / 100;//满背包72
                                rect = new Rectangle(0, 0, (int)n, img.Height);
                                gh.DrawImage(img, 890, 732, rect, unit);

                                rect = new Rectangle(905, 717, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, f.ToString("F2") + "%", font12, brush, rect);//背包字
                            }
                            #endregion

                            List<string> personStandList = new List<string>();
                            List<string> personWeaponList = new List<string>();
                            brush = new SolidBrush(color);
                            if (user.User_General.Lv >= 7)//大于7级不显示其它人
                                isLoadOtherPlayer = false;
                            if (isLoadOtherPlayer)
                            {
                                // 加载其它注册的人
                                foreach (var file in filelist)
                                {
                                    try
                                    {
                                        iniFileHelper = new IniFileHelper(file);
                                        //Api.ApiSendMsg("910862143", 2, "704187291", "704187291", "[@745223613] file：" + file, 3996);

                                        int xh = int.Parse(iniFileHelper.Read("人物信息", "序号"));
                                        if (xh == 54)
                                        {

                                        }
                                        string name = iniFileHelper.Read("人物信息", "角色名称");
                                        string sex = iniFileHelper.Read("人物信息", "性别");
                                        string dress = iniFileHelper.Read("装备物品", "衣服");
                                        int.TryParse(iniFileHelper.Read("普通数据", "等级"), out int level);
                                        int.TryParse(iniFileHelper.Read("普通数据", "VIP等级"), out int viplevel);
                                        //if (level < 7) continue;
                                        string username = iniFileHelper.Read("人物信息", "帐号");
                                        if (username == user.UserQQ) continue;

                                        personStandList = GetPersonStandPicture(username, sex, dress);
                                        personWeaponList = GetPersonWeaponPicture(iniFileHelper);
                                        if (xh > 0)//大于0 表示注册的人,就加载在地图上
                                        {
                                            int posx = rnd.Next(50, 1024);
                                            int posy = rnd.Next(0, 450);
                                            int humX, humY, weaponX = 0, weaponY = 0;

                                            //nd = rnd.Next(0, personStandList.Count);
                                            nd = 4;

                                            string psPath = personStandList[nd].Split(',')[0];
                                            humX = posx + int.Parse(personStandList[nd].Split(',')[1]);
                                            humY = posy + int.Parse(personStandList[nd].Split(',')[2]);

                                            string wpPath = string.Empty;
                                            if (personWeaponList.Count > 0)
                                            {
                                                wpPath = personWeaponList[nd].Split(',')[0];
                                                weaponX = posx + int.Parse(personWeaponList[nd].Split(',')[1]);
                                                weaponY = posy + int.Parse(personWeaponList[nd].Split(',')[2]);
                                            }
                                            using (Image img = Image.FromFile(psPath))// 人物
                                            {
                                                if (img.Height < 100)
                                                {
                                                    humY = humY - img.Height;
                                                    if (img.Height < 70)
                                                        humY -= 5;
                                                }
                                                else
                                                    humY = humY - img.Height / 2;
                                                gh.DrawImage(img, humX, humY, img.Width, img.Height);

                                                if (personWeaponList.Count > 0)
                                                {
                                                    using (Image wpimg = Image.FromFile(wpPath))// 武器外观
                                                    {
                                                        weaponY = weaponY - img.Height;
                                                        gh.DrawImage(wpimg, weaponX, weaponY, wpimg.Width, wpimg.Height);
                                                    }
                                                }
                                            }

                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 血条
                                            {
                                                gh.DrawImage(img, humX, humY - 18, img.Width, img.Height);
                                            }
                                            if (viplevel > 0)
                                            {
                                                Color[] colors = { Color.Gray, Color.Lime, Color.Green, Color.Fuchsia, Color.Pink, Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Blue, };
                                                brush = new SolidBrush(colors[rnd.Next(1, colors.Length)]);
                                            }
                                            else
                                            {
                                                brush = new SolidBrush(Color.White);
                                            }
                                            rect = new Rectangle(humX + 15, humY + 35, 0, 0);
                                            TextHelper.DrawCenteredString(gh, name, font9, brush, rect);//用户名
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                }
                            }

                            if (isLoadMyself)
                            {
                                // 加载自己
                                personStandList = GetPersonStandPicture(user.UserQQ, user.Sex, user.User_Items.DRESS);
                                personWeaponList = GetPersonWeaponPicture(config);
                                //nd = rnd.Next(0, personStandList.Count);
                                nd = 4;
                                int humX = 512, humY = 384, weaponX = 0, weaponY = 0, shape = 0;

                                string psPath = "";
                                if (personStandList.Count > 1)
                                {
                                    psPath = personStandList[nd].Split(',')[0];
                                    humX = 512 + int.Parse(personStandList[nd].Split(',')[1]);
                                    humY = 384 + int.Parse(personStandList[nd].Split(',')[2]);
                                }

                                string wpPath = string.Empty;
                                if (personWeaponList.Count > 0)
                                {
                                    wpPath = personWeaponList[nd].Split(',')[0];
                                    weaponX = 512 + int.Parse(personWeaponList[nd].Split(',')[1]);
                                    weaponY = 384 + int.Parse(personWeaponList[nd].Split(',')[2]);
                                    shape = int.Parse(personWeaponList[nd].Split(',')[3]);
                                }

                                using (Image img = Image.FromFile(psPath))// 人物
                                {
                                    if (img.Height < 100)
                                    {
                                        humY = humY - img.Height;
                                        if (img.Height < 70)
                                            humY -= 5;
                                    }
                                    else
                                        humY = humY - img.Height / 2;
                                    gh.DrawImage(img, humX, humY, img.Width, img.Height);

                                    if (personWeaponList.Count > 0)
                                    {
                                        using (Image wpimg = Image.FromFile(wpPath))// 武器外观
                                        {
                                            weaponY = weaponY - img.Height;
                                            gh.DrawImage(wpimg, weaponX, weaponY, wpimg.Width, wpimg.Height);
                                        }
                                    }
                                }

                                humX = 512;
                                humY = 384;
                                rect = new Rectangle(humX + 25, humY - 80, 0, 0);
                                if (user.User_General.VipLv > 0)
                                {
                                    Color[] colors = { Color.Gray, Color.Lime, Color.Green, Color.Fuchsia, Color.Pink, Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Blue, };
                                    brush = new SolidBrush(colors[rnd.Next(1, colors.Length)]);
                                    TextHelper.DrawCenterStrokeString(gh, user.Name, font13, brush, rect);//用户名 
                                }
                                else
                                {
                                    TextHelper.DrawCenterStrokeString(gh, user.Name, font13, Brushes.White, rect);//用户名 
                                }

                                rect = new Rectangle(humX + 25, humY - 140, 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, $"{user.User_Attribute.Hp}/{user.User_Attribute.MaxHp}/D{user.User_General.Lv}/V{user.User_General.VipLv}", font12, brushLimeGreen, rect);//红

                                if (user.IsOnYHZL == "是")
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                    {
                                        gh.DrawImage(img, humX - 30, humY - 170, img.Width, img.Height);
                                    }
                                }

                                if (user.IsOnTitle == "是")
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                    {
                                        gh.DrawImage(img, humX - 50, humY - 135, img.Width, img.Height);
                                    }
                                }
                            }

                            #region 添加上方数值 
                            rect = new Rectangle(5, 0, 0, 0);
                            string upMsg = $"防御: {user.User_Attribute.Ac}-{user.User_Attribute.Ac2}  魔御: {user.User_Attribute.Mac}-{user.User_Attribute.Mac2}  攻击: {user.User_Attribute.Dc}-{user.User_Attribute.DcMax}  魔法: {user.User_Attribute.Mc}-{user.User_Attribute.McMax}  道术: {user.User_Attribute.Sc}-{user.User_Attribute.ScMax}  金币: {user.User_General.JB}  元宝: {user.User_General.YB}  经验: ({user.User_General.SumExp}/{user.User_General.NextExp})";
                            TextHelper.DrawLeftStrokeString(gh, upMsg, font13, Brushes.Lime, rect);
                            #endregion
                        }
                        savePath = $"{UserInfoImgPath + user.UserQQ}_main.png";
                        bitmap.Save(savePath, ImageFormat.Png);//保存图片
                        bitmap.Dispose();
                    }
                }
                return savePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取签到的图片
        /// </summary>
        public string GetSignInImg(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string imgPath = "";
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    Bitmap bmpImage = new Bitmap(496, 361);
                    MemoryStream ms = new MemoryStream();
                    MagickImage magickImage = null;
                    Graphics gh;
                    int delay = 20;
                    List<string> assList = new List<string>();
                    Rectangle rect;
                    Color color = ColorTranslator.FromHtml("#ffffff");
                    Brush brush = new SolidBrush(color); // 创建画刷

                    //登录背景图加载
                    using (Image img = Image.FromFile(chrSelPath + "000700.png"))
                    {
                        gh = Graphics.FromImage(bmpImage);
                        gh.DrawImage(img, 0, 0, bmpImage.Width, bmpImage.Height);
                    }

                    for (int i = 0; i < user.UserQQ.Length; i++)
                    {
                        rect = new Rectangle(222 + (i * 6), 165, 0, 0);
                        TextHelper.DrawLeftString(gh, user.UserQQ[i].ToString(), font8, brush, rect);

                        bmpImage.Save(ms, ImageFormat.Png);
                        ms.Seek(0, System.IO.SeekOrigin.Begin);
                        magickImage = new MagickImage(ms);
                        if (i == user.UserQQ.Length - 1) delay = 200;

                        magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                        collection.Add(magickImage);
                    }

                    //点击提交
                    using (Image img = Image.FromFile(chrSelPath + "000701.png"))
                    {
                        gh = Graphics.FromImage(bmpImage);
                        gh.DrawImage(img, 0, 0, bmpImage.Width, bmpImage.Height);
                    }
                    rect = new Rectangle(222, 165, 0, 0);
                    TextHelper.DrawLeftString(gh, user.UserQQ, font8, brush, rect);

                    delay = 15;
                    bmpImage.Save(ms, ImageFormat.Png);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    magickImage = new MagickImage(ms);
                    magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                    collection.Add(magickImage);
                    brush.Dispose();

                    using (Image img = Image.FromFile(chrSelPath + "000800.png"))
                    {
                        gh = Graphics.FromImage(bmpImage);
                        gh.DrawImage(img, 0, 0, bmpImage.Width, bmpImage.Height);
                    }
                    delay = 100;
                    bmpImage.Save(ms, ImageFormat.Png);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    magickImage = new MagickImage(ms);
                    magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                    collection.Add(magickImage);
                    brush.Dispose();


                    using (Image img = Image.FromFile(chrSelPath + "000801.png"))
                    {
                        gh = Graphics.FromImage(bmpImage);
                        gh.DrawImage(img, 0, 0, bmpImage.Width, bmpImage.Height);
                    }
                    delay = 15;
                    bmpImage.Save(ms, ImageFormat.Png);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    magickImage = new MagickImage(ms);
                    magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                    collection.Add(magickImage);
                    brush.Dispose();

                    #region 添加开门动画
                    using (Image img = Image.FromFile(chrSelPath + "000022.png"))
                    {
                        gh = Graphics.FromImage(bmpImage);
                        gh.DrawImage(img, 0, 0, bmpImage.Width, bmpImage.Height);
                    }

                    for (int i = 24; i < 33; i++)
                    {
                        assList.Add($"{i.ToString().PadLeft(6, '0')}.png");
                    }
                    assList.ForEach(s =>
                    {
                        using (Image img = Image.FromFile(chrSelPath + s))
                        {
                            gh = Graphics.FromImage(bmpImage);
                            int ww = bmpImage.Width * 60 / 100;
                            int hh = bmpImage.Height * 60 / 100;
                            gh.DrawImage(img, 95, 70, ww, hh);
                        }

                        bmpImage.Save(ms, ImageFormat.Png);
                        ms.Seek(0, System.IO.SeekOrigin.Begin);
                        magickImage = new MagickImage(ms);
                        magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                        collection.Add(magickImage);
                    });
                    assList.Clear();
                    #endregion

                    string newImgPath = "";
                    int n = rnd.Next(1, 7);
                    switch (user.Job)
                    {
                        case "战士": if (user.Sex == "男") n = 1; else n = 2; break;
                        case "法师": if (user.Sex == "男") n = 3; else n = 4; break;
                        case "道士": if (user.Sex == "男") n = 5; else n = 6; break;
                        default: break;
                    }

                    #region 添加火特效
                    if (n == 1 || n == 3 || n == 5)
                    {
                        for (int i = 4; i < 18; i++)
                        {
                            assList.Add($"{i.ToString().PadLeft(6, '0')}.png");
                        }
                    }
                    else
                    {
                        for (int i = 600; i < 613; i++)
                        {
                            assList.Add($"{i.ToString().PadLeft(6, '0')}.png");
                        }
                    }

                    delay = 10;
                    assList.ForEach(s =>
                    {
                        using (Image img = Image.FromFile(chrSelPath + "000280.jpg"))
                        {
                            gh = Graphics.FromImage(bmpImage);
                            gh.DrawImage(img, 0, 0, bmpImage.Width, bmpImage.Height);

                            if (n == 1 || n == 3 || n == 5)
                            {
                                using (Image nPngImg = Image.FromFile(chrSelPath + s))
                                {
                                    int ww = nPngImg.Width * 60 / 100;
                                    int hh = nPngImg.Height * 60 / 100;
                                    gh.DrawImage(nPngImg, 60, 20, ww, hh);
                                }
                            }
                            else
                            {
                                using (Image nPngImg = Image.FromFile(chrSelPath + s))
                                {
                                    int ww = nPngImg.Width * 28 / 100;
                                    int hh = nPngImg.Height * 28 / 100;
                                    gh.DrawImage(nPngImg, 30, 25, ww, hh);
                                }
                            }

                            SetUserSignInText(user, gh, x, y);

                            bmpImage.Save(ms, ImageFormat.Png);
                            ms.Seek(0, System.IO.SeekOrigin.Begin);
                            magickImage = new MagickImage(ms);

                            magickImage.AnimationDelay = delay;
                            collection.Add(magickImage);
                        }
                    });
                    #endregion

                    Image pngImg = Image.FromFile(chrSelPath + "000280.jpg");//添加背景图片
                    switch (n)
                    {
                        case 1:// 男战
                            WarriorList1.ForEach(item =>
                            {
                                gh = Graphics.FromImage(bmpImage);

                                gh.DrawImage(pngImg, 0, 0, bmpImage.Width, bmpImage.Height);

                                SetUserSignInText(user, gh, x, y);
                                newImgPath = $"{chrSelPath + item}.png";
                                using (Image nPngImg = Image.FromFile(newImgPath))
                                {
                                    int ww = nPngImg.Width * 33 / 100;
                                    int hh = nPngImg.Height * 33 / 100;
                                    gh.DrawImage(nPngImg, 50, 30, ww, hh);
                                }
                                bmpImage.Save(ms, ImageFormat.Png);
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                // 添加帧到GIF集合
                                magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                                collection.Add(magickImage);
                            });
                            break;
                        case 2://女战
                            WarriorList2.ForEach(item =>
                            {
                                gh = Graphics.FromImage(bmpImage);

                                gh.DrawImage(pngImg, 0, 0, bmpImage.Width, bmpImage.Height);

                                SetUserSignInText(user, gh, x, y);
                                newImgPath = $"{chrSelPath + item}.png";
                                using (Image nPngImg = Image.FromFile(newImgPath))
                                {
                                    int ww = nPngImg.Width * 33 / 100;
                                    int hh = nPngImg.Height * 33 / 100;
                                    gh.DrawImage(nPngImg, 50, 20, ww, hh);
                                }
                                bmpImage.Save(ms, ImageFormat.Png);
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                // 添加帧到GIF集合
                                magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                                collection.Add(magickImage);
                            });
                            break;
                        case 3://男法
                            WizardList1.ForEach(item =>
                            {
                                gh = Graphics.FromImage(bmpImage);
                                gh.DrawImage(pngImg, 0, 0, bmpImage.Width, bmpImage.Height);
                                SetUserSignInText(user, gh, x, y);
                                newImgPath = $"{chrSelPath + item}.png";
                                using (Image nPngImg = Image.FromFile(newImgPath))
                                {
                                    int ww = nPngImg.Width * 33 / 100;
                                    int hh = nPngImg.Height * 33 / 100;
                                    gh.DrawImage(nPngImg, 50, 25, ww, hh);
                                }
                                bmpImage.Save(ms, ImageFormat.Png);
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                // 添加帧到GIF集合
                                magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                                collection.Add(magickImage);
                            });
                            break;
                        case 4://女法
                            WizardList2.ForEach(item =>
                            {
                                gh = Graphics.FromImage(bmpImage);
                                gh.DrawImage(pngImg, 0, 0, bmpImage.Width, bmpImage.Height);
                                SetUserSignInText(user, gh, x, y);
                                newImgPath = $"{chrSelPath + item}.png";
                                using (Image nPngImg = Image.FromFile(newImgPath))
                                {
                                    int ww = nPngImg.Width * 33 / 100;
                                    int hh = nPngImg.Height * 33 / 100;
                                    gh.DrawImage(nPngImg, 50, 25, ww, hh);
                                }
                                bmpImage.Save(ms, ImageFormat.Png);
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                // 添加帧到GIF集合
                                magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                                collection.Add(magickImage);
                            });
                            break;
                        case 5://男道
                            TaoistList1.ForEach(item =>
                            {
                                gh = Graphics.FromImage(bmpImage);

                                gh.DrawImage(pngImg, 0, 0, bmpImage.Width, bmpImage.Height);
                                SetUserSignInText(user, gh, x, y);
                                newImgPath = $"{chrSelPath + item}.png";
                                using (Image nPngImg = Image.FromFile(newImgPath))
                                {
                                    int ww = nPngImg.Width * 33 / 100;
                                    int hh = nPngImg.Height * 33 / 100;
                                    gh.DrawImage(nPngImg, 55, 20, ww, hh);
                                }
                                bmpImage.Save(ms, ImageFormat.Png);
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                // 添加帧到GIF集合
                                magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                                collection.Add(magickImage);
                            });
                            break;
                        case 6://女道
                            TaoistList2.ForEach(item =>
                            {
                                gh = Graphics.FromImage(bmpImage);
                                gh.DrawImage(pngImg, 0, 0, bmpImage.Width, bmpImage.Height);
                                SetUserSignInText(user, gh, x, y);
                                newImgPath = $"{chrSelPath + item}.png";
                                using (Image nPngImg = Image.FromFile(newImgPath))
                                {
                                    int ww = nPngImg.Width * 33 / 100;
                                    int hh = nPngImg.Height * 33 / 100;
                                    gh.DrawImage(nPngImg, 55, 20, ww, hh);
                                }
                                bmpImage.Save(ms, ImageFormat.Png);
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                // 添加帧到GIF集合
                                magickImage.AnimationDelay = delay; // 每帧延迟200毫秒
                                collection.Add(magickImage);
                            });
                            break;
                    }

                    pngImg.Dispose();

                    bmpImage.Dispose();
                    imgPath = UserInfoImgPath + user.UserQQ + ".gif";
                    // 3. 优化GIF动画
                    collection.OptimizeTransparency();
                    collection.Write(imgPath, MagickFormat.Gif);
                    magickImage.Dispose();

                }

                return imgPath;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设置用户注册信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="gh"></param>
        void SetUserSignInText(UserInfo user, Graphics gh, int x = 0, int y = 0)
        {
            Color color = ColorTranslator.FromHtml("#e1e1e1");// 使用ColorTranslator将HTML颜色代码转换为Color对象
                                                              // 设置名称
            using (Brush brush = new SolidBrush(color)) // 创建画刷
            {
                Rectangle rect = new Rectangle(40, 291, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, user.Name, font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(210, 3, 0, 0); // 设置文本的绘制区域
                TextHelper.DrawLeftString(gh, "咕咕鸡传奇[QQ引擎]", font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(280, 246, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, user.UserQQ, font8, brush, rect); // 调用方法居中绘制文本

                if (user.User_General.Lv > 0)
                {
                    rect = new Rectangle(23, 309, 120, 20); // 设置文本的绘制区域
                    TextHelper.DrawCenteredString(gh, user.User_General.Lv.ToString(), font8, brush, rect); // 调用方法居中绘制文本
                }
                if (!string.IsNullOrEmpty(user.Job))
                {
                    rect = new Rectangle(36, 328, 120, 20); // 设置文本的绘制区域
                    TextHelper.DrawCenteredString(gh, user.Job.ToString(), font8, brush, rect); // 调用方法居中绘制文本
                }

                rect = new Rectangle(195, 250, 0, 0); // 设置文本的绘制区域
                if (user.Id == 0)
                    TextHelper.DrawLeftString(gh, "未注册", font8, brush, rect); // 调用方法居中绘制文本
                else
                    TextHelper.DrawLeftString(gh, "已注册", font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(290, 45, 120, 25); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, "人物信息", font16, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(250, 80, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, "签到经验", font10, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(320, 79, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, user.User_General.Exp.ToString(), font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(250, 110, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, "当前经验", font10, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(320, 110, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, user.User_General.SumExp + "/" + user.User_General.NextExp, font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(250, 140, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, "签到元宝", font10, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(320, 138, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, user.User_General.CurtYB.ToString(), font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(250, 170, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, "当前元宝", font10, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(320, 170, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, user.User_General.YB.ToString(), font8, brush, rect); // 调用方法居中绘制文本

                rect = new Rectangle(250, 200, 120, 20); // 设置文本的绘制区域
                TextHelper.DrawCenteredString(gh, "签到等级", font10, brush, rect); // 调用方法居中绘制文本

                switch (user.User_General.Lv)
                {
                    case 1:
                        gh.DrawImage(Resources.xing, 340, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 2:
                        gh.DrawImage(Resources.xing, 340, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 3:
                        gh.DrawImage(Resources.xing, 340, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 4:
                        gh.DrawImage(Resources.yueliang1, 340, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 5:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 6:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 7:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 8:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        break;
                    case 9:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 10:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 11:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 12:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        break;
                    case 13:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 14:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 15:
                        gh.DrawImage(Resources.yueliang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 359, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 16:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        break;
                    case 17:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 18:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 19:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 361, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 20:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        break;
                    case 21:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 22:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 23:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 24:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        break;
                    case 25:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 26:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 27:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 359, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 28:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 404, 192, 25, 25);
                        break;
                    case 29:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 404, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 30:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 404, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 359, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 31:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 404, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 359, 212, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 380, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 32:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        break;
                    case 33:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 34:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 35:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 382, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 404, 192, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 36:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        break;
                    case 37:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 400, 190, Resources.xing.Width, Resources.xing.Height);

                        break;
                    case 38:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 400, 190, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 39:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.xing, 400, 190, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 338, 212, Resources.xing.Width, Resources.xing.Height);
                        gh.DrawImage(Resources.xing, 359, 212, Resources.xing.Width, Resources.xing.Height);
                        break;
                    case 40:
                        gh.DrawImage(Resources.taiyang1, 340, 192, 25, 25);
                        gh.DrawImage(Resources.taiyang1, 361, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 382, 192, 25, 25);
                        gh.DrawImage(Resources.yueliang1, 404, 192, 25, 25);
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 取玩家人物图片
        /// </summary>
        /// <param name="user"></param>
        /// <param name="iniFileHelper"></param>
        /// <param name="direction">朝向(1=上 2=右上 3=右 4=右下 5=下 6=左下 7=左 8=左上)</param>
        /// <returns></returns>
        public List<string> GetPlayerImgList(UserInfo user, IniFileHelper iniFileHelper, int shape = 0)
        {
            List<string> list = new List<string>();
            string yfKey = user.User_Items.DRESS;//取衣服
            int num = 0;
            #region 老的
            //if (looks > 0)
            //{
            //    switch (looks)// 判断外观
            //    {
            //        case 80:
            //        case 60: num = 1200; break;// 布衣
            //        case 81:
            //        case 61: num = 2400; break;// 轻型盔甲 - 中型盔甲
            //        case 82:
            //        case 62: num = 3600; break;// 重盔甲 - 战神盔甲
            //        case 83:
            //        case 63: num = 4800; break;// 魔法长袍 - 恶魔长袍
            //        case 84:
            //        case 64: num = 6000; break;// 灵魂战衣 - 幽灵战衣
            //        case 88:
            //        case 85: num = 7200; break;// 天魔神甲
            //        case 89:
            //        case 86: num = 8400; break;// 法神披风
            //        case 90:
            //        case 87: num = 9600; break;// 天尊道袍
            //    }
            //}
            //else
            //{
            //    if (yfKey != "0" && !string.IsNullOrEmpty(yfKey)) //取没有穿衣服的平砍和放技能图片
            //    {// 根据不同的衣服选择不同的外观
            //        string std = iniFileHelper.ReadValue("装备属性", yfKey);//取衣服
            //        StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);
            //        if (stditemDB.Looks > 7484) stditemDB.Looks -= 7484;
            //        switch (stditemDB.Looks)// 判断外观
            //        {
            //            case 80:
            //            case 60: num = 1200; break;// 布衣
            //            case 81:
            //            case 61: num = 2400; break;// 轻型盔甲 - 中型盔甲
            //            case 82:
            //            case 62: num = 3600; break;// 重盔甲 - 战神盔甲
            //            case 83:
            //            case 63: num = 4800; break;// 魔法长袍 - 恶魔长袍
            //            case 84:
            //            case 64: num = 6000; break;// 灵魂战衣 - 幽灵战衣
            //            case 88:
            //            case 85: num = 7200; break;// 天魔神甲
            //            case 89:
            //            case 86: num = 8400; break;// 法神披风
            //            case 90:
            //            case 87: num = 9600; break;// 天尊道袍
            //        }
            //    }
            //    else
            //    {
            //        num = 0;
            //    }
            //}
            //if (user.Sex == "女") num += 600;// 女性服装在男性上+600

            //List<string> humPos = new List<string>();
            //string placementsPath = "", path = "";
            //for (int i = num; i < num + 600; i++)
            //{
            //    placementsPath = humPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt";
            //    humPos = FileHelper.ReadTxtReturnList(placementsPath);
            //    path = humPath + i.ToString().PadLeft(6, '0') + ".png";
            //    if (File.Exists(path))
            //    {
            //        list.Add($"{i.ToString().PadLeft(6, '0')}.png,{string.Join(",", humPos)}");
            //    }
            //}
            #endregion
            string humDressPath = humPath;
            StditemDB stditemDB = null;
            if (shape == 0)
            {
                string strDress = iniFileHelper.ReadValue("装备属性", yfKey);//取衣服
                stditemDB = Api.JsonDeserialize<StditemDB>(strDress);
            }
            else
            {
                stditemDB = new StditemDB();
            }

            if (stditemDB != null)
            {
                if (shape > 0) stditemDB.Shape = shape;
                switch (stditemDB.Shape)// 判断外观
                {
                    case 1: num = 1200; break; // 布衣
                    case 2: num = 2400; break; // 轻型盔甲 - 中型盔甲
                    case 3: num = 3600; break; // 重盔甲 - 战神盔甲
                    case 4: num = 4800; break; // 魔法长袍 - 恶魔长袍
                    case 5: num = 6000; break; // 灵魂战衣 - 幽灵战衣
                    case 6: num = 7200; break; // 天魔神甲
                    case 7: num = 8400; break; // 法神披风
                    case 8: num = 9600; break; // 天尊道袍

                    case 1000: humDressPath = hum100Path + "1000\\"; break; // 白虎战甲
                    case 1001: humDressPath = hum100Path + "1001\\"; break; // 朱雀神袍
                    case 1002: humDressPath = hum100Path + "1002\\"; break; // 青龙道袍
                    case 1003: humDressPath = hum100Path + "1003\\"; break; // 刑天战甲
                    case 1004: humDressPath = hum100Path + "1004\\"; break; // 焚天道甲
                    case 1005: humDressPath = hum100Path + "1005\\"; break; // 沧澜魔甲

                    case 106: num = 9600; humDressPath = hum3Path; break; // 毁灭战甲
                }
            }
            if (user.Sex == "女") num += 600;// 女性服装在男性上+600

            List<string> humPos = new List<string>();
            string placementsPath = "", path = "";
            for (int i = num; i < num + 600; i++)
            {
                placementsPath = humDressPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt";
                humPos = FileHelper.ReadTxtReturnList(placementsPath);
                path = humDressPath + i.ToString().PadLeft(6, '0') + ".png";
                if (File.Exists(path))
                {
                    list.Add($"{path},{string.Join(",", humPos)}");
                }
            }
            return list;
        }

        /// <summary>
        /// 取玩家武器图片
        /// </summary>
        /// <param name="user"></param>
        /// <param name="iniFileHelper"></param>
        /// <param name="direction">朝向(1=上 2=右上 3=右 4=右下 5=下 6=左下 7=左 8=左上)</param>
        /// <returns></returns>
        public List<string> GetPlayerWeaponImgList(UserInfo user, IniFileHelper iniFileHelper, int shapeCode = 0)
        {
            List<string> list = new List<string>();
            List<string> weaponPos = new List<string>();

            string wqKey = iniFileHelper.Read("装备物品", "武器");//取武器
            #region 老的
            //if (shapeCode > 0)
            //{
            //    int shape = shapeCode * 1200;

            //    if (user.Sex == "女") shape += 600;
            //    int newShape = shape + 600;
            //    string placementsPath = "", path = "";
            //    for (int i = shape; i < newShape; i++)
            //    {
            //        placementsPath = weaponPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt";
            //        weaponPos = FileHelper.ReadTxtReturnList(placementsPath);
            //        path = weaponPath + i.ToString().PadLeft(6, '0') + ".png";
            //        if (File.Exists(path))
            //        {
            //            list.Add($"{i.ToString().PadLeft(6, '0')}.png,{string.Join(",", weaponPos)}");
            //        }
            //    }
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(wqKey) && wqKey != "0") // 根据不同的武器选择不同的外观
            //    {
            //        string std = iniFileHelper.ReadValue("装备属性", wqKey);//取武器
            //        StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);
            //        int shape = stditemDB.Shape * 1200;
            //        if (user.Sex == "女") shape += 600;
            //        int newShape = shape + 600;
            //        string placementsPath = "", path = "";
            //        for (int i = shape; i < newShape; i++)
            //        {
            //            placementsPath = weaponPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt";
            //            weaponPos = FileHelper.ReadTxtReturnList(placementsPath);
            //            path = weaponPath + i.ToString().PadLeft(6, '0') + ".png";
            //            if (File.Exists(path))
            //            {
            //                list.Add($"{i.ToString().PadLeft(6, '0')}.png,{string.Join(",", weaponPos)}");
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 新的
            if (shapeCode > 0)
            {
                int shape = 0;
                if (shapeCode < 100)
                    shape = shapeCode * 1200;
                else if (shapeCode >= 100 && shapeCode <= 149)
                    shape = (shapeCode - 100) * 1200;
                else if (shapeCode >= 150 && shapeCode <= 199)
                    shape = (shapeCode - 150) * 1200;
                else if (shapeCode >= 200 && shapeCode <= 249)
                    shape = (shapeCode - 200) * 1200;
                else if (shapeCode >= 250 && shapeCode <= 299)
                    shape = (shapeCode - 250) * 1200;
                else if (shapeCode >= 300 && shapeCode <= 349)
                    shape = (shapeCode - 300) * 1200;
                else if (shapeCode >= 350 && shapeCode <= 399)
                    shape = (shapeCode - 350) * 1200;
                else if (shapeCode >= 400 && shapeCode <= 449)
                    shape = (shapeCode - 400) * 1200;
                else
                    shape = shapeCode;
                if (user.Sex == "女") shape += 600;
                int newShape = shape + 600;
                for (int i = shape; i < newShape; i++)
                {
                    if (shapeCode < 100)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weaponPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weaponPath + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},0");
                    }
                    else if (shapeCode >= 100 && shapeCode <= 149)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon2Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon2Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (shapeCode >= 150 && shapeCode <= 199)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon3Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon3Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (shapeCode >= 200 && shapeCode <= 249)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon4Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon4Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (shapeCode >= 250 && shapeCode <= 299)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon5Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon5Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (shapeCode >= 300 && shapeCode <= 349)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon6Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon6Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (shapeCode >= 350 && shapeCode <= 399)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon7Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon7Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else if (shapeCode >= 400 && shapeCode <= 449)
                    {
                        weaponPos = FileHelper.ReadTxtReturnList(weapon8Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon8Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                    else
                    {
                        if (shape > 1000)
                        {
                            weapon100Path = weapon100Path + shape + "\\";
                        }
                        weaponPos = FileHelper.ReadTxtReturnList(weapon100Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                        list.Add(weapon100Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(wqKey) && wqKey != "0") // 根据不同的武器选择不同的外观
                {
                    string std = iniFileHelper.ReadValue("装备属性", wqKey);//取武器
                    StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(std);

                    int shape = 0;
                    if (stditemDB.Shape < 100)
                        shape = stditemDB.Shape * 1200;
                    else if (stditemDB.Shape >= 100 && stditemDB.Shape <= 149)
                        shape = (stditemDB.Shape - 100) * 1200;
                    else if (stditemDB.Shape >= 150 && stditemDB.Shape <= 199)
                        shape = (stditemDB.Shape - 150) * 1200;
                    else if (stditemDB.Shape >= 200 && stditemDB.Shape <= 249)
                        shape = (stditemDB.Shape - 200) * 1200;
                    else if (stditemDB.Shape >= 250 && stditemDB.Shape <= 299)
                        shape = (stditemDB.Shape - 250) * 1200;
                    else if (stditemDB.Shape >= 300 && stditemDB.Shape <= 349)
                        shape = (stditemDB.Shape - 300) * 1200;
                    else if (stditemDB.Shape >= 350 && stditemDB.Shape <= 399)
                        shape = (stditemDB.Shape - 350) * 1200;
                    else if (stditemDB.Shape >= 400 && stditemDB.Shape <= 449)
                        shape = (stditemDB.Shape - 400) * 1200;
                    else
                        shape = stditemDB.Shape;

                    if (user.Sex == "女") shape += 600;
                    int newShape = shape + 600;
                    for (int i = shape; i < newShape; i++)
                    {
                        if (stditemDB.Shape < 100)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weaponPath + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weaponPath + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},0");
                        }
                        else if (stditemDB.Shape >= 100 && stditemDB.Shape <= 149)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon2Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon2Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else if (stditemDB.Shape >= 150 && stditemDB.Shape <= 199)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon3Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon3Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else if (stditemDB.Shape >= 200 && stditemDB.Shape <= 249)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon4Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon4Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else if (stditemDB.Shape >= 250 && stditemDB.Shape <= 299)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon5Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon5Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else if (stditemDB.Shape >= 300 && stditemDB.Shape <= 349)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon6Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon6Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else if (stditemDB.Shape >= 350 && stditemDB.Shape <= 399)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon7Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon7Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else if (stditemDB.Shape >= 400 && stditemDB.Shape <= 449)
                        {
                            weaponPos = FileHelper.ReadTxtReturnList(weapon8Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon8Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                        else
                        {
                            if (shape > 1000)
                            {
                                weapon100Path = weapon100Path + shape + "\\";
                            }
                            weaponPos = FileHelper.ReadTxtReturnList(weapon100Path + "Placements\\" + i.ToString().PadLeft(6, '0') + ".txt");
                            list.Add(weapon100Path + i.ToString().PadLeft(6, '0') + $".png,{string.Join(",", weaponPos)},100");
                        }
                    }
                }
            }

            #endregion

            return list;
        }

    }
}
