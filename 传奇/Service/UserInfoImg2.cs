using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using 传奇.core;

namespace 传奇
{
    public class UserInfoImg2
    {
        #region 成语变量
        Random rnd = new Random();
        string UserInfoImgPath = LegendFilePath.imgPath;
        string LegendIMGDataPath = LegendFilePath.LegendIMGDataPath;
        string UserPath = LegendFilePath.UserPath;

        string humPath = string.Empty;
        string chrSelPath = string.Empty;
        string newopUIPath = string.Empty;
        string ui1Path = string.Empty;
        string itemsPath = string.Empty;
        string stateitemPath = string.Empty;
        string prgusePath = string.Empty;
        string prguse3Path = string.Empty;
        string weaponPath = string.Empty;
        string dnItemsPath = string.Empty;
        string mapPath = string.Empty;
        string mapMonInfo = string.Empty;
        string magIcon2Path = string.Empty;


        List<StditemDB> stdDBList = new List<StditemDB>();

        Font font6 = new Font("黑体", 6);
        Font font7 = new Font("黑体", 7);
        Font font8 = new Font("黑体", 8);
        Font font9 = new Font("黑体", 9);
        Font font10 = new Font("黑体", 10);
        Font font11 = new Font("黑体", 11);
        Font font12 = new Font("黑体", 12);
        Font font13 = new Font("黑体", 13);
        Font font14 = new Font("黑体", 14);
        Font font15 = new Font("黑体", 15);
        Font font16 = new Font("黑体", 16);
        Font fontBold6 = new Font("黑体", 6, FontStyle.Bold);
        Font fontBold7 = new Font("黑体", 7, FontStyle.Bold);
        Font fontBold8 = new Font("黑体", 8, FontStyle.Bold);
        Font fontBold9 = new Font("黑体", 9, FontStyle.Bold);
        Font fontBold10 = new Font("黑体", 10, FontStyle.Bold);
        Font fontBold11 = new Font("黑体", 11, FontStyle.Bold);
        Font fontBold12 = new Font("黑体", 12, FontStyle.Bold);
        Font fontBold13 = new Font("黑体", 13, FontStyle.Bold);
        Font fontBold14 = new Font("黑体", 14, FontStyle.Bold);
        Font fontBold15 = new Font("黑体", 15, FontStyle.Bold);
        Font fontBold16 = new Font("黑体", 16, FontStyle.Bold);
        Font fontBold20 = new Font("黑体", 20, FontStyle.Bold);

        Dictionary<int, string> HpImgDic = new Dictionary<int, string>();
        #endregion

        public UserInfoImg2(UserInfo user)
        {
            humPath = LegendIMGDataPath + "Hum\\";
            newopUIPath = LegendIMGDataPath + "NewopUI\\";
            itemsPath = LegendIMGDataPath + "Items\\";// 装备在背包的样子
            dnItemsPath = LegendIMGDataPath + "DnItems\\";// 装备在地上的样子
            stateitemPath = LegendIMGDataPath + "stateitem\\";// 装备在身上的样子
            prgusePath = LegendIMGDataPath + "Prguse\\";
            prguse3Path = LegendIMGDataPath + "Prguse3\\";
            weaponPath = LegendIMGDataPath + "Weapon\\";
            chrSelPath = LegendIMGDataPath + "ChrSel\\";
            ui1Path = LegendIMGDataPath + "ui1\\";
            magIcon2Path = LegendIMGDataPath + "MagIcon2\\";

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
        }


        /// <summary>
        /// 用户PK生成动图
        /// </summary>
        public async Task<string> GetUserPK(List<UserAttackRecord> userAttackRecords, UserInfo user1, UserInfo user2, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user1);
                string userImgPath = userInfoImg.GamgeMainForm(user1, false, false, true, false, x, y, w, h, lv);

                user1.User_General.PKMsg = user1.Name + ":" + user1.User_General.PKMsg;
                int ugMsgLength = user1.User_General.PKMsg.Length;
                string nMsg = user1.User_General.PKMsg;

                for (int k = 0; k < ugMsgLength; k++)
                {
                    if (k % 16 == 0 && k > 0)
                    {
                        user1.User_General.PKMsg = user1.User_General.PKMsg.Insert(k, "\n");
                    }
                }
                for (int k = 0; k < ugMsgLength; k++)
                {
                    if (k % 45 == 0 && k > 0)
                    {
                        nMsg = nMsg.Insert(k, "\n");
                    }
                }

                user2.User_General.PKMsg = user2.Name + ":" + user2.User_General.PKMsg;
                ugMsgLength = user2.User_General.PKMsg.Length;
                string nMsg2 = user2.User_General.PKMsg;
                for (int k = 0; k < ugMsgLength; k++)
                {
                    if (k % 16 == 0 && k > 0)
                    {
                        user2.User_General.PKMsg = user2.User_General.PKMsg.Insert(k, "\n");
                    }
                }
                for (int k = 0; k < ugMsgLength; k++)
                {
                    if (k % 45 == 0 && k > 0)
                    {
                        nMsg2 = nMsg2.Insert(k, "\n");
                    }
                }


                int humX = 512, humY = 284, weaponX = 512, weaponY = 284,
                   hpX1 = 630, hpY1 = 250, hpX2 = 640, hpY2 = 250, index = 1, magicX = 512, magicY = 284,
                   num = 0, playerNum = 0, player1HP = 0, player2HP = 0, att1 = 0, att2 = 0, att3 = 0, att4 = 0;
                double f = 0, t = 0;
                int delay = 10;
                string playerImg1 = "", weaponImg1 = "", playerImg2 = "", weaponImg2 = "", magicImg = "", path = "";
                string user1IniPath = UserPath + user1.UserQQ + ".ini";
                string user2IniPath = UserPath + user2.UserQQ + ".ini";
                IniFileHelper user1INI = new IniFileHelper(user1IniPath);
                IniFileHelper user2INI = new IniFileHelper(user2IniPath);
                List<string> user1ImgList = userInfoImg.GetPlayerImgList(user1, user1INI, 0);// 人物1图片集合              
                List<string> user1WeaponImgList = userInfoImg.GetPlayerWeaponImgList(user1, user1INI, 0);// 人物1武器图片集合

                List<string> user2ImgList = null;
                if (user2.User_Items.DRESS.Length < 8)
                {
                    user2ImgList = userInfoImg.GetPlayerImgList(user2, user2INI, int.Parse(user2.User_Items.DRESS));// 人物2图片集合
                }
                else
                {
                    user2ImgList = userInfoImg.GetPlayerImgList(user2, user2INI, 0);// 人物2图片集合
                }
                List<string> user2WeaponImgList = null;
                if (user2.User_Items.WEAPON.Length < 8)
                {
                    user2WeaponImgList = userInfoImg.GetPlayerWeaponImgList(user2, user2INI, int.Parse(user2.User_Items.DRESS));// 人物2武器图片集合
                }
                else
                {
                    user2WeaponImgList = userInfoImg.GetPlayerWeaponImgList(user2, user2INI, 0);// 人物2武器图片集合
                }

                MagicDB magic;
                MemoryStream ms = new MemoryStream();
                MagickImage magickImage = null;
                MagickImage blendImage = null;
                Rectangle rect = new Rectangle(0, 0, 0, 0);
                Rectangle sourceRectangle = new Rectangle(0, 0, 0, 0);
                SolidBrush brushWhite = new SolidBrush(Color.White);

                SolidBrush brushBlack = new SolidBrush(Color.Black);
                SolidBrush brushGreenYellow = new SolidBrush(Color.GreenYellow);
                SolidBrush brushZB = new SolidBrush(ColorTranslator.FromHtml("#f13131"));
                List<string> magImgList = new List<string>();// 人物施法效果集合
                List<string> magWeaponImgList = new List<string>();// 人物施法武器集合
                List<string> runImgList = new List<string>();// 人物跑动图片
                List<string> runWeaponImgList = new List<string>();// 人物跑动武器图片

                List<string> player1AttedList = user1ImgList.Skip(488).Take(3).ToList();// 人物1挨打图片
                List<string> player1AttedWeaponList = null;
                if (user1WeaponImgList.Count > 0)
                    player1AttedWeaponList = user1WeaponImgList.Skip(488).Take(3).ToList();// 人物1挨打武器图片

                List<string> player1DeadList = user1ImgList.Skip(552).Take(4).ToList();// 人物1死亡图片
                List<string> player1DeadWeaponList = null;
                if (user1WeaponImgList.Count > 0)
                    player1DeadWeaponList = user1WeaponImgList.Skip(552).Take(4).ToList();// 人物1死亡武器图片

                List<string> player2AttedList = user2ImgList.Skip(520).Take(3).ToList();// 人物2挨打图片
                List<string> player2AttedWeaponList = null;
                if (user2WeaponImgList.Count > 0)
                    player2AttedWeaponList = user2WeaponImgList.Skip(520).Take(3).ToList();// 人物2挨打武器图片

                List<string> player2DeadList = user2ImgList.Skip(584).Take(4).ToList();// 人物2死亡图片
                List<string> player2DeadWeaponList = null;
                if (user2WeaponImgList.Count > 0)
                    player2DeadWeaponList = user2WeaponImgList.Skip(584).Take(4).ToList();// 人物2死亡武器图片
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                string kulouPath = "";
                                userAttackRecords.ForEach(userAttR =>
                                {
                                    #region 判断用户是普通攻击还是魔法攻击
                                    index = 0;
                                    num = 0;
                                    blendImage = null;
                                    runImgList = user1ImgList.Skip(216).Take(6).ToList();// 普通攻击(人物)
                                    if (user1WeaponImgList.Count > 0)
                                        runWeaponImgList = user1WeaponImgList.Skip(216).Take(6).ToList();// 普通攻击(武器)
                                    magic = userAttR.MagicDBModel1;// 用户1使用技能
                                    if (magic != null)
                                    {
                                        if (magic.MagID > 0)// 取用户1施法效果图
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
                                                    magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取效果图
                                                    break;
                                                case "野蛮冲撞":
                                                    magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取效果图
                                                    runImgList = user1ImgList.Skip(144).Take(6).ToList();// 取跑动图（人物）
                                                    runImgList.AddRange(user1ImgList.Skip(16).Take(4).ToList());// 取站图（人物）
                                                    runWeaponImgList = user1WeaponImgList.Skip(144).Take(6).ToList();// 取跑图（武器）
                                                    runWeaponImgList.AddRange(user1WeaponImgList.Skip(16).Take(4).ToList());// 取站图（武器）
                                                    break;
                                                default:
                                                    magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取效果图
                                                    runImgList = user1ImgList.Skip(408).Take(6).ToList();// 取施法人物图（人物）
                                                    runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                    runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                    runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                    runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                                    runWeaponImgList = user1WeaponImgList.Skip(408).Take(6).ToList();// 取施法武器图（武器）
                                                    runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                    runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                    runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                    runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region 用户1攻击
                                    runImgList.ForEach(u =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 用户1设置
                                        if (user1.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user1.Sex == "男")
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

                                        playerImg1 = u.Split(',')[0];
                                        humX = 512 + int.Parse(u.Split(',')[1]);
                                        humY = 284 + int.Parse(u.Split(',')[2]);
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
                                            if (player1HP > 0)
                                                rect = new Rectangle(0, 0, player1HP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}/D{user1.User_General.Lv}/V{user1.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                        if (user1.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user1.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }
                                        if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                            using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                            {
                                                gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                            }
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                playerNum = (int)t;
                                                rect = new Rectangle(30, 714 - playerNum, img.Width, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - playerNum, img.Width, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        else
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                playerNum = (int)t;
                                                rect = new Rectangle(28, 714 - playerNum, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - playerNum, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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
                                        DrawLeftString(gh, $"{userAttR.p_hp1}/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                        rect = new Rectangle(535, 277, 0, 0);
                                        DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名 

                                        DrawCenteredString1(gh, user1.User_General.PKMsg, font10, brushWhite, 430, 200);
                                        DrawCenteredString2(gh, nMsg, font10, brushBlack, 208, 724);
                                        #endregion

                                        #region 用户2位置
                                        if (user2.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user2.Sex == "男")
                                                weaponX = 565 + int.Parse(user2WeaponImgList[48].Split(',')[1]);
                                            else
                                                weaponX = 568 + int.Parse(user2WeaponImgList[48].Split(',')[1]);

                                            weaponY = 284 + int.Parse(user2WeaponImgList[48].Split(',')[2]);
                                            weaponImg2 = user2WeaponImgList[48].Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg2))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg2 = user2ImgList[48].Split(',')[0];
                                        humX = 565 + int.Parse(user2ImgList[48].Split(',')[1]);
                                        humY = 284 + int.Parse(user2ImgList[48].Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        hX = 574; hY = 230;
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            if (player2HP > 0)
                                                rect = new Rectangle(0, 0, player2HP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"{user2.User_Attribute.Hp}/{user2.User_Attribute.MaxHp}/D{user2.User_General.Lv}/V{user2.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                        if (user2.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user2.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }

                                        rect = new Rectangle(588, 277, 0, 0);
                                        DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 攻击提示
                                        if (userAttR.p_attack1 > 0)
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            if (magic != null)
                                                DrawCenteredString(gh, $"{user1.Name} 使用技能 {magic.MagName} 打掉 {user2.Name} {userAttR.p_attack1}血!", fontBold20, brushGreenYellow, rect);
                                            else
                                                DrawCenteredString(gh, $"{user1.Name} 使用 普通攻击 打掉 {user2.Name} {userAttR.p_attack1}血!", fontBold20, brushGreenYellow, rect);
                                        }
                                        else
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            DrawCenteredString(gh, $"{user1.Name} 的攻击 {userAttR.p_msg1}", fontBold20, brushGreenYellow, rect);
                                        }
                                        #endregion


                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = delay;

                                        #region 加载特效图                                        
                                        if (user1.IsUseSkills == "是" && magImgList.Count > 0)
                                        {
                                            // 显示技能特效图片
                                            magicImg = magImgList[0].Split(',')[0];
                                            if (user1.Sex == "男")
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
                                        #endregion

                                        num++;
                                    });
                                    #endregion

                                    num = 0;

                                    playerImg1 = runImgList[num].Split(',')[0];
                                    humX = 512 + int.Parse(runImgList[num].Split(',')[1]);
                                    humY = 284 + int.Parse(runImgList[num].Split(',')[2]);

                                    if (user1.IsUseWeapon)// 设置人物武器
                                    {
                                        if (user1.Sex == "男")
                                            weaponX = 512 + int.Parse(runWeaponImgList[num].Split(',')[1]);
                                        else
                                            weaponX = 515 + int.Parse(runWeaponImgList[num].Split(',')[1]);

                                        weaponY = 284 + int.Parse(runWeaponImgList[num].Split(',')[2]);
                                        weaponImg1 = runWeaponImgList[num].Split(',')[0];
                                    }

                                    #region 魔法效果
                                    magImgList.ForEach(m =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 用户1设置

                                        if (user1.IsUseWeapon)// 设置人物武器
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
                                            if (player1HP > 0)
                                                rect = new Rectangle(0, 0, player1HP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}/D{user1.User_General.Lv}/V{user1.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                        if (user1.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user1.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }
                                        if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                            using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                            {
                                                gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                            }
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                playerNum = (int)t;
                                                rect = new Rectangle(30, 714 - playerNum, img.Width, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - playerNum, img.Width, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }
                                        }
                                        else
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;
                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;
                                                playerNum = (int)t;
                                                rect = new Rectangle(28, 714 - playerNum, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - playerNum, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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
                                        DrawLeftString(gh, $"{userAttR.p_hp1}/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                        rect = new Rectangle(535, 277, 0, 0);
                                        DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 攻击提示
                                        if (userAttR.p_attack1 > 0)
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            if (magic != null)
                                                DrawCenteredString(gh, $"{user1.Name} 使用技能 {magic.MagName} 打掉 {user2.Name} {userAttR.p_attack1}血!", fontBold20, brushGreenYellow, rect);
                                            else
                                                DrawCenteredString(gh, $"{user1.Name} 使用 普通攻击 打掉 {user2.Name} {userAttR.p_attack1}血!", fontBold20, brushGreenYellow, rect);
                                        }
                                        else
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            DrawCenteredString(gh, $"{user1.Name} 的攻击 {userAttR.p_msg1}", fontBold20, brushGreenYellow, rect);
                                        }
                                        #endregion

                                        // 显示技能特效图片
                                        magicImg = m.Split(',')[0];
                                        if (user1.Sex == "男")
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
                                                    if (num < 6)
                                                    {
                                                        magicX += (pWidth / 2) + num * 2;
                                                    }
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
                                    blendImage = null;
                                    #endregion

                                    #region 用户2被攻击
                                    num = 0;
                                    index = 0;
                                    player2AttedList.ForEach(u =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 用户1设置
                                        if (user1.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user1.Sex == "男")
                                                weaponX = 512 + int.Parse(user1WeaponImgList[194].Split(',')[1]);
                                            else
                                                weaponX = 515 + int.Parse(user1WeaponImgList[194].Split(',')[1]);

                                            weaponY = 284 + int.Parse(user1WeaponImgList[194].Split(',')[2]);
                                            weaponImg1 = user1WeaponImgList[194].Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg1))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg1 = user1ImgList[194].Split(',')[0];
                                        humX = 512 + int.Parse(user1ImgList[194].Split(',')[1]);
                                        humY = 284 + int.Parse(user1ImgList[194].Split(',')[2]);

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
                                            if (player1HP > 0)
                                                rect = new Rectangle(0, 0, player1HP, img.Height);
                                            else
                                                rect = new Rectangle(0, 0, img.Width, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}/D{user1.User_General.Lv}/V{user1.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                        if (user1.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user1.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }

                                        if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                        {
                                            f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

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
                                            f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                            // HP
                                            using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                            {
                                                if (f < 0) f = 0;
                                                t = img.Height * f / 100;

                                                rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                            }

                                            f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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
                                        DrawLeftString(gh, $"{userAttR.p_hp1}/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                        rect = new Rectangle(100, 730, 0, 0);
                                        DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                        rect = new Rectangle(535, 277, 0, 0);
                                        DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名

                                        DrawCenteredString1(gh, user1.User_General.PKMsg, font10, brushWhite, 430, 200);
                                        DrawCenteredString2(gh, nMsg, font10, brushBlack, 208, 724);
                                        #endregion

                                        #region 用户2位置
                                        if (user2.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user2.Sex == "男")
                                                weaponX = 565 + int.Parse(player2AttedWeaponList[num].Split(',')[1]);
                                            else
                                                weaponX = 568 + int.Parse(player2AttedWeaponList[num].Split(',')[1]);

                                            weaponY = 284 + int.Parse(player2AttedWeaponList[num].Split(',')[2]);
                                            weaponImg2 = player2AttedWeaponList[num].Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg2))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg2 = u.Split(',')[0];
                                        humX = 565 + int.Parse(u.Split(',')[1]);
                                        humY = 284 + int.Parse(u.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        hX = 574; hY = 230;
                                        using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            f = (double)userAttR.p_hp2 / (double)user2.User_Attribute.MaxHp * 100;
                                            t = img.Width * f / 100;
                                            player2HP = (int)t;
                                            if (player2HP < 0) player2HP = 0;
                                            rect = new Rectangle(0, 0, player2HP, img.Height);
                                            gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                        }

                                        rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"{user2.User_Attribute.Hp}/{user2.User_Attribute.MaxHp}/D{user2.User_General.Lv}/V{user2.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                        if (user2.IsOnYHZL == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                            {
                                                gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                            }
                                        }

                                        if (user2.IsOnTitle == "是")
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                            {
                                                gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                            }
                                        }
                                        rect = new Rectangle(588, 277, 0, 0);
                                        DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 飘血1
                                        hpX2 = 640;
                                        hpY2 = 250;
                                        hpX1 = 630;
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

                                        #region 攻击提示
                                        if (userAttR.p_attack1 > 0)
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "000060.png"))// 减号
                                            {
                                                gh.DrawImage(img, hpX1, hpY1 - (index - 1) * 5, img.Width, img.Height);
                                            }

                                            rect = new Rectangle(550, 150, 0, 40);
                                            if (magic != null)
                                                DrawCenteredString(gh, $"{user1.Name} 使用技能 {magic.MagName} 打掉 {user2.Name} {userAttR.p_attack1}血!", fontBold20, brushGreenYellow, rect);
                                            else
                                                DrawCenteredString(gh, $"{user1.Name} 使用 普通攻击 打掉 {user2.Name} {userAttR.p_attack1}血!", fontBold20, brushGreenYellow, rect);
                                        }
                                        else
                                        {
                                            rect = new Rectangle(550, 150, 0, 40);
                                            DrawCenteredString(gh, $"{user1.Name}  攻击 {userAttR.p_attack1} 触发 {userAttR.p_msg1}", fontBold20, brushGreenYellow, rect);
                                        }
                                        #endregion

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = delay;
                                        collection.Add(magickImage);
                                        index++;
                                        num++;
                                    });
                                    #endregion

                                    if (userAttR.p_hp2 > 0)
                                    {
                                        #region 判断用户是普通攻击还是魔法攻击
                                        num = 0;
                                        blendImage = null;
                                        runImgList = user2ImgList.Skip(248).Take(6).ToList();// 普通攻击(人物)
                                        if (user2WeaponImgList.Count > 0)
                                            runWeaponImgList = user2WeaponImgList.Skip(248).Take(6).ToList();// 普通攻击(武器)
                                        magic = userAttR.MagicDBModel2;// 用户1使用技能
                                        if (magic != null)
                                        {
                                            if (magic.MagID > 0)// 取用户1施法效果图
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
                                                        magImgList = MagicApiImg.GetAttackMagicImg(magic, 7);// 取效果图
                                                        break;
                                                    case "野蛮冲撞":
                                                        magImgList = MagicApiImg.GetAttackMagicImg(magic, 7);// 取效果图
                                                        runImgList = user2ImgList.Skip(176).Take(6).ToList();// 取跑动图（人物）
                                                        runImgList.AddRange(user2ImgList.Skip(16).Take(4).ToList());// 取站图（人物）
                                                        runWeaponImgList = user2WeaponImgList.Skip(176).Take(6).ToList();// 取跑图（武器）
                                                        runWeaponImgList.AddRange(user2WeaponImgList.Skip(16).Take(4).ToList());// 取站图（武器）
                                                        break;
                                                    default:
                                                        magImgList = MagicApiImg.GetAttackMagicImg(magic, 7);// 取效果图
                                                        runImgList = user2ImgList.Skip(440).Take(6).ToList();// 取施法人物图（人物）
                                                        runImgList.AddRange(user2ImgList.Skip(198).Take(1).ToList());// 取站图（人物）
                                                        runImgList.AddRange(user2ImgList.Skip(198).Take(1).ToList());// 取站图（人物）
                                                        runImgList.AddRange(user2ImgList.Skip(198).Take(1).ToList());// 取站图（人物）
                                                        runImgList.AddRange(user2ImgList.Skip(198).Take(1).ToList());// 取站图（人物）
                                                        runWeaponImgList = user2WeaponImgList.Skip(440).Take(6).ToList();// 取施法武器图（武器）
                                                        runWeaponImgList.AddRange(user2WeaponImgList.Skip(198).Take(1).ToList());// 取站图（武器）
                                                        runWeaponImgList.AddRange(user2WeaponImgList.Skip(198).Take(1).ToList());// 取站图（武器）
                                                        runWeaponImgList.AddRange(user2WeaponImgList.Skip(198).Take(1).ToList());// 取站图（武器）
                                                        runWeaponImgList.AddRange(user2WeaponImgList.Skip(198).Take(1).ToList());// 取站图（武器）
                                                        break;
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 用户2攻击
                                        num = 0;

                                        runImgList.ForEach(u =>
                                        {
                                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                            #region 用户1设置
                                            if (user1.IsUseWeapon)// 设置人物武器
                                            {
                                                if (user1.Sex == "男")
                                                    weaponX = 512 + int.Parse(user1WeaponImgList[194].Split(',')[1]);
                                                else
                                                    weaponX = 515 + int.Parse(user1WeaponImgList[194].Split(',')[1]);

                                                weaponY = 284 + int.Parse(user1WeaponImgList[194].Split(',')[2]);
                                                weaponImg1 = user1WeaponImgList[194].Split(',')[0];
                                            }

                                            playerImg1 = user1ImgList[194].Split(',')[0];
                                            humX = 512 + int.Parse(user1ImgList[194].Split(',')[1]);
                                            humY = 284 + int.Parse(user1ImgList[194].Split(',')[2]);
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
                                                if (player1HP > 0)
                                                    rect = new Rectangle(0, 0, player1HP, img.Height);
                                                else
                                                    rect = new Rectangle(0, 0, img.Width, img.Height);
                                                gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                            }

                                            rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}/D{user1.User_General.Lv}/V{user1.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                            if (user1.IsOnYHZL == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                {
                                                    gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                }
                                            }

                                            if (user1.IsOnTitle == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                {
                                                    gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                }
                                            }
                                            if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                                using (Image img = Image.FromFile(newopUIPath + "000007.png"))// 战士空血
                                                {
                                                    gh.DrawImage(img, 30, 600, img.Width, img.Height);
                                                }
                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000006.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;
                                                    playerNum = (int)t;
                                                    rect = new Rectangle(30, 714 - playerNum, img.Width, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - playerNum, img.Width, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }
                                            }
                                            else
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;
                                                    playerNum = (int)t;
                                                    rect = new Rectangle(28, 714 - playerNum, 57, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - playerNum, 60, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }

                                                f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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
                                            DrawLeftString(gh, $"{userAttR.p_hp1}/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                            rect = new Rectangle(100, 730, 0, 0);
                                            DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                            rect = new Rectangle(535, 277, 0, 0);
                                            DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名

                                            #endregion

                                            #region 用户2位置
                                            if (user2.IsUseWeapon)// 设置人物武器
                                            {
                                                if (user2.Sex == "男")
                                                    weaponX = 565 + int.Parse(runWeaponImgList[num].Split(',')[1]);
                                                else
                                                    weaponX = 568 + int.Parse(runWeaponImgList[num].Split(',')[1]);

                                                weaponY = 284 + int.Parse(runWeaponImgList[num].Split(',')[2]);
                                                weaponImg2 = runWeaponImgList[num].Split(',')[0];
                                                using (Image weapon = Image.FromFile(weaponImg2))
                                                {
                                                    gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                }
                                            }

                                            playerImg2 =  u.Split(',')[0];
                                            humX = 565 + int.Parse(u.Split(',')[1]);
                                            humY = 284 + int.Parse(u.Split(',')[2]);
                                            using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                            {
                                                gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                            }

                                            hX = 574; hY = 230;
                                            using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                            {
                                                gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                            {
                                                if (player2HP > 0)
                                                    rect = new Rectangle(0, 0, player2HP, img.Height);
                                                else
                                                    rect = new Rectangle(0, 0, img.Width, img.Height);
                                                gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                            }

                                            rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, $"{user2.User_Attribute.Hp}/{user2.User_Attribute.MaxHp}/D{user2.User_General.Lv}/V{user2.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                            if (user2.IsOnYHZL == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                {
                                                    gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                }
                                            }

                                            if (user2.IsOnTitle == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                {
                                                    gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                }
                                            }

                                            rect = new Rectangle(588, 277, 0, 0);
                                            DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                            DrawCenteredString1(gh, user2.User_General.PKMsg, font10, brushWhite, 530, 200);
                                            DrawCenteredString2(gh, nMsg2, font10, brushBlack, 208, 724);
                                            #endregion

                                            #region 攻击提示
                                            if (userAttR.p_attack2 > 0)
                                            {
                                                rect = new Rectangle(550, 150, 0, 40);
                                                if (magic != null)
                                                    DrawCenteredString(gh, $"{user2.Name} 使用技能 {magic.MagName} 打掉 {user1.Name} {userAttR.p_attack2}血!", fontBold20, brushGreenYellow, rect);
                                                else
                                                    DrawCenteredString(gh, $"{user2.Name} 使用 普通攻击 打掉 {user1.Name} {userAttR.p_attack2}血!", fontBold20, brushGreenYellow, rect);
                                            }
                                            else
                                            {
                                                rect = new Rectangle(550, 150, 0, 40);
                                                DrawCenteredString(gh, $"{user2.Name} 的攻击 {userAttR.p_msg2}", fontBold20, brushGreenYellow, rect);
                                            }
                                            #endregion

                                            bitmap.Save(ms, ImageFormat.Png);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            magickImage = new MagickImage(ms);
                                            magickImage.AnimationDelay = delay;

                                            #region 加载特效图                                        
                                            if (user2.IsUseSkills == "是" && magImgList.Count > 0)
                                            {
                                                // 显示技能特效图片
                                                magicImg = magImgList[0].Split(',')[0];
                                                if (user2.Sex == "男")
                                                    magicX = 565 + int.Parse(magImgList[0].Split(',')[1]);
                                                else
                                                    magicX = 568 + int.Parse(magImgList[0].Split(',')[1]);
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
                                            #endregion

                                            num++;
                                        });
                                        #endregion

                                        if (userAttR.p_hp1 > 0)
                                        {
                                            #region 用户1被攻击
                                            num = 0;
                                            index = 0;
                                            player1AttedList.ForEach(u =>
                                            {
                                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                                #region 用户1设置
                                                if (user1.IsUseWeapon)// 设置人物武器
                                                {
                                                    if (user1.Sex == "男")
                                                        weaponX = 512 + int.Parse(player1AttedWeaponList[num].Split(',')[1]);
                                                    else
                                                        weaponX = 515 + int.Parse(player1AttedWeaponList[num].Split(',')[1]);

                                                    weaponY = 284 + int.Parse(player1AttedWeaponList[num].Split(',')[2]);
                                                    weaponImg1 = player1AttedWeaponList[num].Split(',')[0];
                                                    using (Image weapon = Image.FromFile(weaponImg1))
                                                    {
                                                        gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                    }
                                                }

                                                playerImg1 =  u.Split(',')[0];
                                                humX = 512 + int.Parse(u.Split(',')[1]);
                                                humY = 284 + int.Parse(u.Split(',')[2]);

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
                                                    f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;
                                                    t = img.Width * f / 100;
                                                    player1HP = (int)t;
                                                    if (player1HP < 0) player1HP = 0;
                                                    rect = new Rectangle(0, 0, player1HP, img.Height);
                                                    gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                                }

                                                rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}/D{user1.User_General.Lv}/V{user1.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                                if (user1.IsOnYHZL == "是")
                                                {
                                                    using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                    {
                                                        gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                    }
                                                }

                                                if (user1.IsOnTitle == "是")
                                                {
                                                    using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                    {
                                                        gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                    }
                                                }


                                                if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                                {
                                                    f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

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
                                                    f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                                    // HP
                                                    using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                    {
                                                        if (f < 0) f = 0;
                                                        t = img.Height * f / 100;

                                                        rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                        sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                        gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                    }

                                                    f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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
                                                DrawLeftString(gh, $"{userAttR.p_hp1}/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                                rect = new Rectangle(100, 730, 0, 0);
                                                DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                                rect = new Rectangle(535, 277, 0, 0);
                                                DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名

                                                #endregion

                                                #region 用户2设置
                                                if (user2.IsUseWeapon)// 设置人物武器
                                                {
                                                    if (user2.Sex == "男")
                                                        weaponX = 565 + int.Parse(user2WeaponImgList[198].Split(',')[1]);
                                                    else
                                                        weaponX = 568 + int.Parse(user2WeaponImgList[198].Split(',')[1]);

                                                    weaponY = 284 + int.Parse(user2WeaponImgList[198].Split(',')[2]);
                                                    weaponImg2 = user2WeaponImgList[198].Split(',')[0];
                                                    using (Image weapon = Image.FromFile(weaponImg2))
                                                    {
                                                        gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                    }
                                                }

                                                playerImg2 =  user2ImgList[198].Split(',')[0];
                                                humX = 565 + int.Parse(user2ImgList[198].Split(',')[1]);
                                                humY = 284 + int.Parse(user2ImgList[198].Split(',')[2]);

                                                using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                                {
                                                    gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                                }

                                                hX = 574; hY = 230;
                                                using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                                {
                                                    gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                                }
                                                using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                                {
                                                    if (player2HP > 0)
                                                        rect = new Rectangle(0, 0, player2HP, img.Height);
                                                    else
                                                        rect = new Rectangle(0, 0, img.Width, img.Height);
                                                    gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                                }

                                                rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, $"{user2.User_Attribute.Hp}/{user2.User_Attribute.MaxHp}/D{user2.User_General.Lv}/V{user2.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                                if (user2.IsOnYHZL == "是")
                                                {
                                                    using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                    {
                                                        gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                    }
                                                }

                                                if (user2.IsOnTitle == "是")
                                                {
                                                    using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                    {
                                                        gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                    }
                                                }

                                                rect = new Rectangle(535, 277, 0, 0);
                                                DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                                DrawCenteredString1(gh, user2.User_General.PKMsg, font10, brushWhite, 530, 200);
                                                DrawCenteredString2(gh, nMsg2, font10, brushBlack, 208, 724);

                                                #endregion

                                                #region 飘血2
                                                hpX1 = 490;
                                                hpX2 = 500;
                                                hpY2 = 250;
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
                                                    if (magic != null)
                                                        DrawCenteredString(gh, $"{user2.Name} 使用技能 {magic.MagName} 打掉 {user1.Name} {userAttR.p_attack2}血!", fontBold20, brushGreenYellow, rect);
                                                    else
                                                        DrawCenteredString(gh, $"{user2.Name} 使用 普通攻击 打掉 {user1.Name} {userAttR.p_attack2}血!", fontBold20, brushGreenYellow, rect);
                                                }
                                                else
                                                {
                                                    rect = new Rectangle(550, 150, 0, 40);
                                                    DrawCenteredString(gh, $"{user2.Name}  攻击 {userAttR.p_attack2} 触发 {userAttR.p_msg2}", fontBold20, brushGreenYellow, rect);
                                                }
                                                #endregion

                                                bitmap.Save(ms, ImageFormat.Png);
                                                ms.Seek(0, SeekOrigin.Begin);
                                                magickImage = new MagickImage(ms);
                                                magickImage.AnimationDelay = delay;
                                                collection.Add(magickImage);
                                                index++;
                                                num++;
                                            });
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 用户1死亡
                                            num = 0;
                                            player1DeadList.ForEach(u =>
                                            {
                                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                                #region 用户1设置
                                                if (user1.IsUseWeapon)// 设置人物武器
                                                {
                                                    if (user1.Sex == "男")
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

                                                playerImg1 =  u.Split(',')[0];
                                                humX = 512 + int.Parse(u.Split(',')[1]);
                                                humY = 284 + int.Parse(u.Split(',')[2]);

                                                using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                                {
                                                    gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                                }
                                                if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                                {
                                                    f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

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
                                                    f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                                    // HP
                                                    using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                    {
                                                        if (f < 0) f = 0;
                                                        t = img.Height * f / 100;

                                                        rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                        sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                        gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                    }

                                                    f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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
                                                DrawLeftString(gh, $"0/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                                rect = new Rectangle(100, 730, 0, 0);
                                                DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                                rect = new Rectangle(535, 277, 0, 0);
                                                DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名

                                                #endregion

                                                #region 用户2位置
                                                if (user2.IsUseWeapon)// 设置人物武器
                                                {
                                                    if (user2.Sex == "男")
                                                        weaponX = 565 + int.Parse(user2WeaponImgList[48].Split(',')[1]);
                                                    else
                                                        weaponX = 568 + int.Parse(user2WeaponImgList[48].Split(',')[1]);

                                                    weaponY = 284 + int.Parse(user2WeaponImgList[48].Split(',')[2]);
                                                    weaponImg2 = user2WeaponImgList[48].Split(',')[0];
                                                    using (Image weapon = Image.FromFile(weaponImg2))
                                                    {
                                                        gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                    }
                                                }

                                                playerImg2 = user2ImgList[48].Split(',')[0];
                                                humX = 565 + int.Parse(user2ImgList[48].Split(',')[1]);
                                                humY = 284 + int.Parse(user2ImgList[48].Split(',')[2]);
                                                using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                                {
                                                    gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                                }

                                                int hX = 574, hY = 230;
                                                using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                                {
                                                    gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                                }
                                                using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                                {
                                                    if (player2HP > 0)
                                                        rect = new Rectangle(0, 0, player2HP, img.Height);
                                                    else
                                                        rect = new Rectangle(0, 0, img.Width, img.Height);
                                                    gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                                }

                                                rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, $"{user2.User_Attribute.Hp}/{user2.User_Attribute.MaxHp}/D{user2.User_General.Lv}/V{user2.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                                if (user2.IsOnYHZL == "是")
                                                {
                                                    using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                    {
                                                        gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                    }
                                                }

                                                if (user2.IsOnTitle == "是")
                                                {
                                                    using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                    {
                                                        gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                    }
                                                }
                                                rect = new Rectangle(588, 277, 0, 0);
                                                DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名

                                                DrawCenteredString1(gh, user2.User_General.PKMsg, font10, brushWhite, 530, 200);
                                                DrawCenteredString2(gh, nMsg2, font10, brushBlack, 208, 724);
                                                #endregion

                                                rect = new Rectangle(520, 450, 0, 0);
                                                DrawCenteredString(gh, $"你被 玩家 {user2.Name} 杀死了!", fontBold20, brushZB, rect);

                                                num++;
                                                bitmap.Save(ms, ImageFormat.Png);
                                                ms.Seek(0, SeekOrigin.Begin);
                                                magickImage = new MagickImage(ms);
                                                magickImage.AnimationDelay = 12;
                                                collection.Add(magickImage);
                                            });

                                            bitmap.Save(ms, ImageFormat.Png);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            magickImage = new MagickImage(ms);
                                            magickImage.AnimationDelay = 300;
                                            if (user1.User_Attribute.Hp <= 0)// 如果是人物死亡界面变灰
                                            {
                                                magickImage.ColorSpace = ColorSpace.Gray;
                                                magickImage.Depth = 8; // 设置灰度深度为8位
                                            }
                                            collection.Add(magickImage);
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        #region 用户2死亡
                                        num = 0;
                                        player2DeadList.ForEach(u =>
                                        {
                                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                            #region 用户1设置
                                            if (user1.IsUseWeapon)// 设置人物武器
                                            {
                                                if (user1.Sex == "男")
                                                    weaponX = 512 + int.Parse(user1WeaponImgList[16].Split(',')[1]);
                                                else
                                                    weaponX = 515 + int.Parse(user1WeaponImgList[16].Split(',')[1]);

                                                weaponY = 284 + int.Parse(user1WeaponImgList[16].Split(',')[2]);
                                                weaponImg1 = user1WeaponImgList[16].Split(',')[0];
                                                using (Image weapon = Image.FromFile(weaponImg1))
                                                {
                                                    gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                }
                                            }


                                            playerImg1 =  user1ImgList[16].Split(',')[0];
                                            humX = 512 + int.Parse(user1ImgList[16].Split(',')[1]);
                                            humY = 284 + int.Parse(user1ImgList[16].Split(',')[2]);

                                            using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                            {
                                                gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                            }
                                            if (user1.Job == "战士" && user1.User_General.Lv < 28)
                                            {
                                                f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

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
                                                f = (double)userAttR.p_hp1 / (double)user1.User_Attribute.MaxHp * 100;

                                                // HP
                                                using (Image img = Image.FromFile(newopUIPath + "000004.png"))
                                                {
                                                    if (f < 0) f = 0;
                                                    t = img.Height * f / 100;

                                                    rect = new Rectangle(28, 714 - (int)t, 57, img.Height);
                                                    sourceRectangle = new Rectangle(0, img.Height - (int)t, 60, img.Height);
                                                    gh.DrawImage(img, rect, sourceRectangle, GraphicsUnit.Pixel);
                                                }

                                                f = (double)user1.User_Attribute.Mp / (double)user1.User_Attribute.MaxMp * 100;
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

                                            int hX = 521, hY = 230;
                                            using (Image img = Image.FromFile(newopUIPath + "000009.png"))// 人物血条
                                            {
                                                gh.DrawImage(img, hX, hY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                            {
                                                if (player1HP > 0)
                                                    rect = new Rectangle(0, 0, player1HP, img.Height);
                                                else
                                                    rect = new Rectangle(0, 0, img.Width, img.Height);
                                                gh.DrawImage(img, hX, hY, rect, GraphicsUnit.Pixel);
                                            }

                                            rect = new Rectangle(hX - 40, hY - 13, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}/D{user1.User_General.Lv}/V{user1.User_General.VipLv}", font12, Brushes.LimeGreen, rect);

                                            if (user1.IsOnYHZL == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001914.png"))// 开启永恒之力
                                                {
                                                    gh.DrawImage(img, hX - 38, hY - 35, img.Width, img.Height);
                                                }
                                            }

                                            if (user1.IsOnTitle == "是")
                                            {
                                                using (Image img = Image.FromFile(newopUIPath + "001913.png"))// 开启称号
                                                {
                                                    gh.DrawImage(img, hX - 55, hY - 5, img.Width, img.Height);
                                                }
                                            }

                                            rect = new Rectangle(30, 730, 0, 0);
                                            DrawLeftString(gh, $"{user1.User_Attribute.Hp}/{user1.User_Attribute.MaxHp}", font10, brushWhite, rect);//红

                                            rect = new Rectangle(100, 730, 0, 0);
                                            DrawLeftString(gh, $"{user1.User_Attribute.Mp}/{user1.User_Attribute.MaxMp}", font10, brushWhite, rect);//蓝

                                            rect = new Rectangle(535, 277, 0, 0);
                                            DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名

                                            DrawCenteredString1(gh, user1.User_General.PKMsg, font10, brushWhite, 430, 200);
                                            DrawCenteredString2(gh, nMsg, font10, brushBlack, 208, 724);
                                            #endregion

                                            #region 用户2位置
                                            if (user2.IsUseWeapon)// 设置人物武器
                                            {
                                                if (user2.Sex == "男")
                                                    weaponX = 565 + int.Parse(player2DeadWeaponList[num].Split(',')[1]);
                                                else
                                                    weaponX = 568 + int.Parse(player2DeadWeaponList[num].Split(',')[1]);

                                                weaponY = 284 + int.Parse(player2DeadWeaponList[num].Split(',')[2]);
                                                weaponImg2 = player2DeadWeaponList[num].Split(',')[0];
                                                using (Image weapon = Image.FromFile(weaponImg2))
                                                {
                                                    gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                                }
                                            }
                                            playerImg2 =  u.Split(',')[0];
                                            humX = 565 + int.Parse(u.Split(',')[1]);
                                            humY = 284 + int.Parse(u.Split(',')[2]);
                                            using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                            {
                                                gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                            }


                                            rect = new Rectangle(588, 277, 0, 0);
                                            DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                            #endregion

                                            rect = new Rectangle(520, 450, 0, 0);
                                            DrawCenteredString(gh, $"玩家 {user2.Name} 被你杀死了!", fontBold20, brushZB, rect);

                                            num++;
                                            bitmap.Save(ms, ImageFormat.Png);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            magickImage = new MagickImage(ms);
                                            magickImage.AnimationDelay = 12;
                                            collection.Add(magickImage);
                                        });

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        magickImage.AnimationDelay = 300;
                                        collection.Add(magickImage);
                                        #endregion
                                    }
                                });
                            }
                        }
                    }

                    path = UserInfoImgPath + user1.UserQQ + ".gif";
                    ms.Dispose();
                    collection.OptimizeTransparency();// 优化gif
                    await collection.WriteAsync(path, MagickFormat.Gif);// 保存gif
                }
                if (magickImage != null) magickImage.Dispose();
                if (blendImage != null) blendImage.Dispose();

                return path;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        void DrawCenteredString(Graphics graphics, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center; // 设置水平居中
            format.LineAlignment = StringAlignment.Center; // 设置垂直居中
            graphics.SmoothingMode = SmoothingMode.HighQuality;//设置字体质量
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.DrawString(text, font, brush, layoutRectangle, format);
        }

        void DrawLeftString(Graphics graphics, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;

            graphics.SmoothingMode = SmoothingMode.HighQuality;//设置字体质量
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.DrawString(text, font, brush, layoutRectangle, format);
        }

        void DrawCenteredString1(Graphics graphics, string text, Font font, Brush brush, int x = 0, int y = 0)
        {
            // 创建一个StringFormat对象,并设置换行选项
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;

            // 绘制换行文本
            graphics.DrawString(text, font, brush, new PointF(x, y), format);
        }

        void DrawCenteredString2(Graphics graphics, string text, Font font, Brush brush, int x = 0, int y = 0)
        {
            // 创建一个StringFormat对象,并设置换行选项
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Near;
            format.Alignment = StringAlignment.Near;

            // 绘制换行文本
            graphics.DrawString(text, font, brush, new PointF(x, y), format);
        }
    }
}
