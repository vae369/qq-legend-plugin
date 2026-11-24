using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;
using 传奇.core;
using ImageMagick;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Collections;

namespace 传奇
{
    public class NPCApi
    {
        static string prgusePath = LegendFilePath.LegendIMGDataPath + "Prguse\\";
        static string prguse3Path = LegendFilePath.LegendIMGDataPath + "Prguse3\\";
        static string newopUIPath = LegendFilePath.LegendIMGDataPath + "NewopUI\\";
        static string itemsPath = LegendFilePath.LegendIMGDataPath + "Items\\";// 装备在背包的样子 


        static Font font14Bold = new Font("黑体", 14, FontStyle.Bold);
        static Font font9 = new Font("黑体", 9);
        static Font font10 = new Font("黑体", 10);
        static Font fontUnderline9 = new Font("黑体", 9, FontStyle.Underline);
        static Font fontUnderline10 = new Font("黑体", 10, FontStyle.Underline);
        static Font fontUnderline12 = new Font("黑体", 12, FontStyle.Underline);
        static Font fontUnderline13 = new Font("黑体", 13, FontStyle.Underline);
        static Font fontUnderline14 = new Font("黑体", 14, FontStyle.Underline);
        static Font font12 = new Font("黑体", 12);
        static Font font13 = new Font("黑体", 13);
        static Font font14 = new Font("黑体", 14);
        static Font font15 = new Font("黑体", 15);
        static Font font16 = new Font("黑体", 16);
        static Font font28 = new Font("黑体", 28);
        static Font fontUnderline16 = new Font("黑体", 16, FontStyle.Underline);

        /// <summary>
        /// 通过名称获取npc图进行对话
        /// </summary>
        public static string GetNPCByName(UserInfo user, string npc, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true, true, x, y, w, h);

                Rectangle rect = new Rectangle(50, 50, 0, 0);

                switch (npc)
                {
                    case "起步礼包":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, 0, 0, img.Width, img.Height);
                                        }

                                        TextHelper.DrawLeftStrokeString(gh, "客服QQ:910862143", font12, Brushes.Red, rect);
                                        rect = new Rectangle(50, 65, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "领取礼包方式,输入 礼包码666666", font12, Brushes.Khaki, rect);
                                        rect = new Rectangle(50, 80, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "本服所有官方群都不禁言,方便大家交流", font12, Brushes.Lime, rect);
                                        rect = new Rectangle(50, 95, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "本服更新内容或者开区时间都在群里发布", font12, Brushes.Lime, rect);
                                        rect = new Rectangle(50, 110, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "欢迎来到咕咕鸡神技复古,本服是属于特色复古", font12, Brushes.Khaki, rect);
                                        rect = new Rectangle(200, 140, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "领取礼包", fontUnderline16, Brushes.Yellow, rect);

                                        rect = new Rectangle(270, 142, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "礼包码666666", font12, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);//保存图片
                                }
                            }
                        }
                        break;
                    case "会员特权":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        if (user.User_General.VipLv == 1)
                                        {
                                            using (Image img = Image.FromFile(prgusePath + "001907.png"))// 会员框
                                            {
                                                gh.DrawImage(img, 300, 150, img.Width, img.Height);
                                            }
                                        }
                                        if (user.User_General.VipLv >= 2)
                                        {
                                            using (Image img = Image.FromFile(prgusePath + "001910.png"))// 会员框
                                            {
                                                gh.DrawImage(img, 300, 150, img.Width, img.Height);
                                            }
                                        }


                                        using (Image img = Image.FromFile(prgusePath + "001908.png"))// 当前
                                        {
                                            switch (user.User_General.VipLv)
                                            {
                                                case 1: gh.DrawImage(img, 338, 221, img.Width, img.Height); break;
                                                case 2: gh.DrawImage(img, 338, 256, img.Width, img.Height); break;
                                                case 3: gh.DrawImage(img, 338, 294, img.Width, img.Height); break;
                                                case 4: gh.DrawImage(img, 338, 333, img.Width, img.Height); break;
                                                case 5: gh.DrawImage(img, 338, 369, img.Width, img.Height); break;
                                            }
                                        }

                                        rect = new Rectangle(550, 224, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"+{user.User_VIPAttribute.Hp}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(550, 250, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"+{user.User_VIPAttribute.Mp}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(550, 276, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.Ac}/{user.User_VIPAttribute.Ac2}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(550, 304, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.Mac}/{user.User_VIPAttribute.Mac2}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(550, 333, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.Dc}/{user.User_VIPAttribute.DcMax}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(550, 363, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.Mc}/{user.User_VIPAttribute.McMax}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(550, 392, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.Sc}/{user.User_VIPAttribute.ScMax}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(700, 225, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.MonExpInc}%", font14Bold, Brushes.White, rect);

                                        if (user.User_VIPAttribute.DouBPower > 0)
                                        {
                                            rect = new Rectangle(700, 332, 0, 0);
                                            TextHelper.DrawCenterStrokeString(gh, $"{1 + user.User_VIPAttribute.DouBPower / 100.0}", font14Bold, Brushes.White, rect);
                                        }
                                        rect = new Rectangle(700, 253, 0, 0);
                                        if (user.User_VIPAttribute.AutoAFK == "否")
                                        {
                                            path = LegendFilePath.LegendIMGDataPath + "ui1\\003600.png";
                                            using (Image img = Image.FromFile(path))
                                            {
                                                gh.DrawImage(img, 692, 242, img.Width, img.Height);
                                            }
                                        }
                                        else
                                        {
                                            path = LegendFilePath.LegendIMGDataPath + "ui1\\003601.png";
                                            using (Image img = Image.FromFile(path))
                                            {
                                                gh.DrawImage(img, 692, 242, img.Width, img.Height);
                                            }
                                        }
                                        rect = new Rectangle(700, 280, 0, 0);
                                        if (user.User_VIPAttribute.AutoRecycl == "否")
                                        {
                                            path = LegendFilePath.LegendIMGDataPath + "ui1\\003600.png";
                                            using (Image img = Image.FromFile(path))
                                            {
                                                gh.DrawImage(img, 692, 270, img.Width, img.Height);
                                            }
                                        }
                                        else
                                        {
                                            path = LegendFilePath.LegendIMGDataPath + "ui1\\003601.png";
                                            using (Image img = Image.FromFile(path))
                                            {
                                                gh.DrawImage(img, 692, 270, img.Width, img.Height);
                                            }
                                        }
                                        rect = new Rectangle(700, 304, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"{user.User_VIPAttribute.AutoPBoos}/{user.User_VIPAttribute.AutoBoos}", font14Bold, Brushes.White, rect);

                                        rect = new Rectangle(575, 432, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"赞助升级会员", font16, Brushes.Yellow, rect);

                                        rect = new Rectangle(697, 432, 0, 0);
                                        TextHelper.DrawCenterStrokeString(gh, $"元宝升级会员", font16, Brushes.Yellow, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "唯一赞助":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000402.png"))// 唯一赞助
                                        {
                                            gh.DrawImage(img, 0, 12, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(10, 21, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "本服不做任何隐形消费和套路!消费明面上绝无其它坑钱设置!", font12, Brushes.GreenYellow, rect);

                                        rect = new Rectangle(10, 35, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "为了维持服务器添加赞助礼包,散人也可打怪回收元宝领礼包!", font12, Brushes.Cyan, rect);

                                        rect = new Rectangle(10, 49, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "购买称号：", font12, Brushes.Yellow, rect);

                                        rect = new Rectangle(66, 49, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "送0.25倍功,注意领取下阶赞助要把上阶斗笠,宝石,隔山打牛放背包.", font12, Brushes.Linen, rect);

                                        int stdX = 50, stdY = 70, posX = 55, posY = 75, textX = 0, textY = 0;

                                        #region 礼包1
                                        // 循环5个物品框物品框
                                        for (int i = 0; i < 5; i++)
                                        {
                                            if (i > 0) stdX += 60;
                                            using (Image img = Image.FromFile(newopUIPath + "000047.png"))
                                            {
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            textX = stdX + 42;
                                            textY = stdY + 28;
                                            rect = new Rectangle(textX, textY, 0, 0);
                                            switch (i)
                                            {
                                                case 0:
                                                case 1:
                                                case 2: TextHelper.DrawRightStrokeString(gh, "1", font12, Brushes.White, rect); break;
                                                case 3: TextHelper.DrawRightStrokeString(gh, "3", font12, Brushes.White, rect); break;
                                                case 4: TextHelper.DrawRightStrokeString(gh, "10", font12, Brushes.White, rect); break;
                                            }
                                        }
                                        using (Image img = Image.FromFile(itemsPath + "008672.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 115;
                                        using (Image img = Image.FromFile(itemsPath + "011244.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 174;
                                        using (Image img = Image.FromFile(itemsPath + "009157.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 238;
                                        posY = 78;
                                        using (Image img = Image.FromFile(itemsPath + "008304.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 295;
                                        using (Image img = Image.FromFile(itemsPath + "011235.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(50, 115, 0, 0);
                                        TextHelper.DrawLeftString(gh, "爆率加成10%", font10, Brushes.Orange, rect);

                                        rect = new Rectangle(170, 115, 0, 0);
                                        TextHelper.DrawLeftString(gh, "赞助领取礼包1", fontUnderline9, Brushes.Yellow, rect);

                                        rect = new Rectangle(280, 115, 0, 0);
                                        TextHelper.DrawLeftString(gh, "白嫖领取礼包1", fontUnderline9, Brushes.Yellow, rect);

                                        #endregion

                                        #region 礼包2
                                        stdX = 50; stdY = 140; posX = 55; posY = 145; textX = 0; textY = 0;
                                        // 循环5个物品框物品框
                                        for (int i = 0; i < 5; i++)
                                        {
                                            if (i > 0) stdX += 60;
                                            using (Image img = Image.FromFile(newopUIPath + "000047.png"))
                                            {
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            textX = stdX + 42;
                                            textY = stdY + 28;
                                            rect = new Rectangle(textX, textY, 0, 0);
                                            switch (i)
                                            {
                                                case 0:
                                                case 1:
                                                case 2:
                                                case 3: TextHelper.DrawRightStrokeString(gh, "1", font12, Brushes.White, rect); break;
                                                case 4: TextHelper.DrawRightStrokeString(gh, "20", font12, Brushes.White, rect); break;
                                            }
                                        }

                                        using (Image img = Image.FromFile(itemsPath + "009565.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 115;
                                        using (Image img = Image.FromFile(itemsPath + "011245.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 174;
                                        using (Image img = Image.FromFile(itemsPath + "009156.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 238;
                                        posY = 147;
                                        using (Image img = Image.FromFile(itemsPath + "007780.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 295;
                                        using (Image img = Image.FromFile(itemsPath + "011235.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(50, 183, 0, 0);
                                        TextHelper.DrawLeftString(gh, "爆率加成20%", font10, Brushes.Orange, rect);

                                        rect = new Rectangle(170, 183, 0, 0);
                                        TextHelper.DrawLeftString(gh, "赞助领取礼包2", fontUnderline9, Brushes.Yellow, rect);

                                        rect = new Rectangle(280, 183, 0, 0);
                                        TextHelper.DrawLeftString(gh, "白嫖领取礼包2", fontUnderline9, Brushes.Yellow, rect);
                                        #endregion

                                        #region 礼包3
                                        stdX = 50; stdY = 205; posX = 55; posY = 211; textX = 0; textY = 0;
                                        // 循环5个物品框物品框
                                        for (int i = 0; i < 5; i++)
                                        {
                                            if (i > 0) stdX += 60;
                                            using (Image img = Image.FromFile(newopUIPath + "000047.png"))
                                            {
                                                gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                            }
                                            textX = stdX + 42;
                                            textY = stdY + 28;
                                            rect = new Rectangle(textX, textY, 0, 0);
                                            switch (i)
                                            {
                                                case 0:
                                                case 1:
                                                case 2:
                                                case 3: TextHelper.DrawRightStrokeString(gh, "1", font12, Brushes.White, rect); break;
                                                case 4: TextHelper.DrawRightStrokeString(gh, "30", font12, Brushes.White, rect); break;
                                            }
                                        }

                                        using (Image img = Image.FromFile(itemsPath + "009071.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 115;
                                        using (Image img = Image.FromFile(itemsPath + "011246.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 174;
                                        using (Image img = Image.FromFile(itemsPath + "009155.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 240;
                                        posY = 215;
                                        using (Image img = Image.FromFile(itemsPath + "007658.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        posX = 295;
                                        posY = 212;
                                        using (Image img = Image.FromFile(itemsPath + "011235.png"))
                                        {
                                            gh.DrawImage(img, posX, posY, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(50, 248, 0, 0);
                                        TextHelper.DrawLeftString(gh, "爆率加成30%", font10, Brushes.Orange, rect);

                                        rect = new Rectangle(170, 248, 0, 0);
                                        TextHelper.DrawLeftString(gh, "赞助领取礼包3", fontUnderline9, Brushes.Yellow, rect);

                                        rect = new Rectangle(280, 248, 0, 0);
                                        TextHelper.DrawLeftString(gh, "白嫖领取礼包3", fontUnderline9, Brushes.Yellow, rect);
                                        #endregion

                                        rect = new Rectangle(15, 280, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "赞助介绍:", font13, Brushes.DeepSkyBlue, rect);

                                        rect = new Rectangle(80, 280, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "本服崇尚理性消费,如果有能力建议最高充值首冲!", font13, Brushes.Orange, rect);

                                        rect = new Rectangle(80, 295, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "如果条件比较好,建议您多多支持我们能继续坚持下去!", font13, Brushes.Orange, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "永恒之力":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 永恒之力
                                        {
                                            gh.DrawImage(img, 0, 0, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(20, 20, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "永恒之力:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(20, 55, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "持续时间:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(20, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "失效规则:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(20, 125, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "开启费用:", font13, Brushes.Khaki, rect);

                                        rect = new Rectangle(85, 20, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "开启后人物攻击倍数+0.1 攻魔道+10 生命增加200", font13, Brushes.DeepSkyBlue, rect);
                                        rect = new Rectangle(85, 55, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "永久生效,小退下线及被怪物杀死均不受影响", font13, Brushes.DeepSkyBlue, rect);
                                        rect = new Rectangle(85, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "被人物杀死则失效,杀人者奖励100元宝", font13, Brushes.DeepSkyBlue, rect);
                                        rect = new Rectangle(85, 125, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "开启永恒之力需要200元宝", font13, Brushes.Lime, rect);

                                        rect = new Rectangle(260, 125, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"你当前元宝:{user.User_General.YB}个", font13, Brushes.MistyRose, rect);

                                        rect = new Rectangle(160, 155, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "开启永恒之力", fontUnderline16, Brushes.Yellow, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "公益捐献":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 永恒之力
                                        {
                                            gh.DrawImage(img, 0, 0, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(20, 20, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "本游戏推出公益捐献每人仅限一次，捐献者可永久获得永恒鞭尸称\n号，此称号击杀怪物有几率触发怪物二次爆率对所有怪物都有效果\n永久有效", font13, Brushes.Lime, rect);

                                        rect = new Rectangle(140, 100, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "当前捐献元宝数量为:", font13, Brushes.Red, rect);
                                        int jxCount = int.Parse(config.Read("配置信息", "公益捐献"));
                                        rect = new Rectangle(270, 101, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"{jxCount}个", font13, Brushes.Khaki, rect);

                                        rect = new Rectangle(20, 150, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "捐献需要2000元宝，其中1000元宝作为拿沙奖励:", font13, Brushes.Cyan, rect);

                                        rect = new Rectangle(315, 147, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "开启公益捐献", fontUnderline16, Brushes.Yellow, rect);


                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "充值使者":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000402.png"))// 永恒之力
                                        {
                                            gh.DrawImage(img, 0, 12, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(10, 20, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, user.Name, font15, Brushes.Yellow, rect);
                                        rect = new Rectangle(10, 36, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "您好,欢迎来到咕咕鸡传奇,很高兴为您服务", font14, Brushes.White, rect);
                                        rect = new Rectangle(10, 52, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "您当前持有:", font14, Brushes.White, rect);
                                        rect = new Rectangle(90, 52, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, user.User_General.YB + "元宝", font14, Brushes.Lime, rect);
                                        rect = new Rectangle(10, 68, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "①支持微信或支付宝充值,下方扫码即可", font14, Brushes.Cyan, rect);
                                        rect = new Rectangle(10, 84, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "②请在充值后联系管理员,核验后直接发送 领取充值 即可", font14, Brushes.Cyan, rect);
                                        rect = new Rectangle(10, 100, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "③当前充值比例为1:200,即1:200元宝+1金刚石", font14, Brushes.Cyan, rect);

                                        rect = new Rectangle(110, 116, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "冲10送10    冲50送2倍", font14, Brushes.MistyRose, rect);
                                        rect = new Rectangle(108, 132, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "冲100送3倍  冲300送4倍", font14, Brushes.MistyRose, rect);
                                        rect = new Rectangle(104, 148, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "冲500送5倍  冲1000送6倍", font14, Brushes.MistyRose, rect);
                                        rect = new Rectangle(96, 164, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "妈个鸡,还没人充值就关服了", font14, Brushes.Yellow, rect);

                                        using (Image img = Image.FromFile(newopUIPath + "001918.jpg"))// 支付宝收款码
                                        {
                                            gh.DrawImage(img, 40, 182, img.Width, img.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "001919.jpg"))// 微信收款码
                                        {
                                            gh.DrawImage(img, 220, 182, img.Width, img.Height);
                                        }


                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "神秘地图":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))
                                        {
                                            gh.DrawImage(img, 0, 12, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(20, 40, 0, 0);
                                        TextHelper.DrawLeftString(gh, "×┅┅∽┅┅∽┅┅∽┅           ┅-∽┅┅-∽┅┅∽┅┅×", font10, Brushes.LightGoldenrodYellow, rect);

                                        rect = new Rectangle(20, 120, 0, 0);
                                        TextHelper.DrawLeftString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━×", font10, Brushes.Khaki, rect);

                                        rect = new Rectangle(168, 40, 0, 0);
                                        TextHelper.DrawLeftString(gh, "〖神秘地图〗", font10, Brushes.DeepSkyBlue, rect);

                                        rect = new Rectangle(70, 75, 0, 0);
                                        TextHelper.DrawLeftString(gh, "主要BOSS：\n进入条件：", font10, Brushes.Lime, rect);

                                        rect = new Rectangle(140, 75, 0, 0);
                                        TextHelper.DrawLeftString(gh, "不多做解释,进入绝对不亏", font10, Brushes.Cyan, rect);

                                        rect = new Rectangle(140, 91, 0, 0);
                                        TextHelper.DrawLeftString(gh, "需要200万金币 等级44级", font10, Brushes.Salmon, rect);

                                        rect = new Rectangle(310, 91, 0, 0);
                                        TextHelper.DrawLeftString(gh, "前往神秘地图", fontUnderline9, Brushes.Yellow, rect);

                                        rect = new Rectangle(40, 140, 0, 0);
                                        TextHelper.DrawLeftString(gh, "提示:最后一层全屏BOSS非常凶残，建议组队前往，小心！", font10, Brushes.Red, rect);
                                        rect = new Rectangle(70, 160, 0, 0);
                                        TextHelper.DrawLeftString(gh, "刷新时间:60分钟(地图共2层)", font10, Brushes.White, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "玛雅大厅":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))
                                        {
                                            gh.DrawImage(img, 0, 12, img.Width, img.Height);
                                        }
                                        rect = new Rectangle(20, 50, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 所有BOSS刷新时间减半为30分钟!", font10, Brushes.Fuchsia, rect);

                                        rect = new Rectangle(20, 90, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 进入条件42级+100万金币！", font10, Brushes.Fuchsia, rect);

                                        rect = new Rectangle(20, 130, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 地图共3层，每层BOSS都很厉害！勇士小心！", font10, Brushes.Fuchsia, rect);

                                        rect = new Rectangle(310, 165, 0, 0);
                                        TextHelper.DrawLeftString(gh, "前往玛雅大厅", fontUnderline12, Brushes.Yellow, rect);


                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "散人打宝":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))
                                        {
                                            gh.DrawImage(img, 0, 12, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(20, 50, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 散人福利,无任何消费要求！！！", font10, Brushes.GreenYellow, rect);

                                        rect = new Rectangle(20, 70, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 等级要求:30级！", font10, Brushes.GreenYellow, rect);

                                        rect = new Rectangle(20, 90, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 龙王4小时刷一只,玄武2小时,其他BOSS 1小时", font10, Brushes.GreenYellow, rect);

                                        rect = new Rectangle(20, 110, 0, 0);
                                        TextHelper.DrawLeftString(gh, "-- 地图共2层,没有实力一定不要前往！", font10, Brushes.GreenYellow, rect);

                                        rect = new Rectangle(310, 165, 0, 0);
                                        TextHelper.DrawLeftString(gh, "前往散人打宝", fontUnderline12, Brushes.Yellow, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "boss之家":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))
                                        {
                                            gh.DrawImage(img, 0, 12, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(20, 50, 0, 0);
                                        TextHelper.DrawLeftString(gh, "[地图说明]:外面地图击杀的BOSS都将在BOSS之家刷新一只", font10, Brushes.GreenYellow, rect);

                                        rect = new Rectangle(20, 70, 0, 0);
                                        TextHelper.DrawLeftString(gh, "[地图说明]:每种BOSS最多存在100只，进入地图扣除20万元宝", font10, Brushes.GreenYellow, rect);


                                        rect = new Rectangle(310, 165, 0, 0);
                                        TextHelper.DrawLeftString(gh, "前往BOSS之家", fontUnderline12, Brushes.Yellow, rect);


                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "灭世斗笠":
                    case "世外高人":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 25;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世斗笠", font10, Brushes.Red, rect);
                                        itemX += 90;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世宝石", fontUnderline10, Brushes.Yellow, rect);
                                        itemX += 87;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世称号", fontUnderline10, Brushes.Yellow, rect);
                                        itemX += 85;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世神技", fontUnderline10, Brushes.Yellow, rect);
                                        itemX -= 274;
                                        itemY += 17;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "注意:“永恒结晶”是媲美元宝的存在，弥足珍贵", font10, Brushes.MistyRose, rect);

                                        itemX -= 20;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            string newpath = newopUIPath + "000047.png";
                                            using (Image img = Image.FromFile(newpath))// 物品框
                                            {
                                                if (i == 0) itemY += 18;
                                                else itemY += img.Height + 5;
                                                gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                            }
                                            switch (i)
                                            {
                                                case 0:
                                                    rect = new Rectangle(itemX + 45, itemY + 5, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成普通斗笠需要永恒结晶*5", font12, Brushes.MistyRose, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 20, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[100%几率成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 270, itemY + 19, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成普通斗笠", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\008672.png";
                                                    break;
                                                case 1:
                                                    rect = new Rectangle(itemX + 45, itemY + 5, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成赤月斗笠需要普通斗笠*3 + 永恒结晶*50", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 20, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[90%几率成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 270, itemY + 19, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成赤月斗笠", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\009565.png";
                                                    break;
                                                case 2:
                                                    rect = new Rectangle(itemX + 45, itemY + 5, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒斗笠需要赤月斗笠*3 + 永恒结晶*88", font12, Brushes.DeepSkyBlue, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 20, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[90%几率成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 270, itemY + 19, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒斗笠", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\009071.png";
                                                    break;
                                                case 3:
                                                    rect = new Rectangle(itemX + 45, itemY + 5, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成毁灭斗笠需要永恒斗笠*3 + 永恒结晶*188", font12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 20, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[90%几率成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 270, itemY + 19, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成毁灭斗笠", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\009564.png";
                                                    break;
                                                case 4:
                                                    rect = new Rectangle(itemX + 45, itemY + 5, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成沧天斗笠需要毁灭斗笠*1 + 永恒结晶*288", font12, Brushes.Khaki, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 20, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[90%几率成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 270, itemY + 19, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成沧天斗笠", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\010840.png";
                                                    break;
                                                case 5:
                                                    rect = new Rectangle(itemX + 45, itemY + 5, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成灭世斗笠需要沧天斗笠*1 + 永恒结晶*388", font12, Brushes.Red, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 20, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[30%几率成功,失败不扣除沧天斗笠]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 270, itemY + 19, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成灭世斗笠", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011374.png";
                                                    break;
                                            }

                                            using (Image img = Image.FromFile(newpath))
                                            {
                                                gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                            }
                                        }
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "灭世宝石":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 25;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世斗笠", font10, Brushes.Yellow, rect);
                                        itemX += 90;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世宝石", fontUnderline10, Brushes.Red, rect);
                                        itemX += 87;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世称号", fontUnderline10, Brushes.Yellow, rect);
                                        itemX += 85;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世神技", fontUnderline10, Brushes.Yellow, rect);
                                        itemX -= 274;
                                        itemY += 17;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "注意:“永恒结晶”是媲美元宝的存在，弥足珍贵", font10, Brushes.MistyRose, rect);

                                        itemX -= 20;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            string newpath = newopUIPath + "000047.png";
                                            using (Image img = Image.FromFile(newpath))// 物品框
                                            {
                                                if (i == 0) itemY += 18;
                                                else itemY += img.Height + 5;
                                                gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                            }
                                            switch (i)
                                            {
                                                case 0:
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "+4升级条件:伤害宝石福利+3免费升级", font12, Brushes.MistyRose, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "升级宝石4", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011247.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 1:
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "+5升级条件:伤害宝石福利+4免费升级", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "升级宝石5", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011248.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 2:
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "+6升级条件:伤害宝石福利+5免费升级", font12, Brushes.DeepSkyBlue, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "升级宝石6", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011249.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 3:
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "+7升级条件:伤害宝石+6+50000元宝", font12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "升级宝石7", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011250.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 4:
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "+8升级条件:伤害宝石+7+50000元宝", font12, Brushes.Khaki, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "升级宝石8", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011251.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 5:
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "+9升级条件:伤害宝石+7+50000元宝", font12, Brushes.Red, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "升级宝石9", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011252.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "灭世称号":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 25;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世斗笠", font10, Brushes.Yellow, rect);
                                        itemX += 90;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世宝石", fontUnderline10, Brushes.Yellow, rect);
                                        itemX += 87;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世称号", fontUnderline10, Brushes.Red, rect);
                                        itemX += 85;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世神技", fontUnderline10, Brushes.Yellow, rect);
                                        itemX -= 274;
                                        itemY += 17;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "注意:“永恒结晶”是媲美元宝的存在，弥足珍贵", font10, Brushes.MistyRose, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒青铜", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要200万金币,怪物爆率+3%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒白银", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+400万金币,怪物爆率+6%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒黄金", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+600万金币,怪物爆率+9%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒白金", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+800万金币,怪物爆率+12%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒钻石", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+1000万金币,怪物爆率+15%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒星耀", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+1200万金币,怪物爆率+18%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒王者", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+1400万金币,怪物爆率+21%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒星辰", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+1600万金币,怪物爆率+24%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒魔王", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+1800万金币,怪物爆率+27%", font12, Brushes.Cyan, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级永恒圣佛", fontUnderline12, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 50, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "获得此称号需要上级称号+2000万金币,怪物爆率+30%", font12, Brushes.Cyan, rect);
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "灭世神技":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 25;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世斗笠", font10, Brushes.Yellow, rect);
                                        itemX += 90;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世宝石", fontUnderline10, Brushes.Yellow, rect);
                                        itemX += 87;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世称号", fontUnderline10, Brushes.Yellow, rect);
                                        itemX += 85;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "◆        ◆", font10, Brushes.White, rect);
                                        itemX += 14;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "灭世神技", fontUnderline10, Brushes.Red, rect);
                                        itemX -= 274;
                                        itemY += 17;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "注意:“永恒结晶”是媲美元宝的存在，弥足珍贵", font10, Brushes.MistyRose, rect);

                                        itemY += 20;
                                        itemX -= 26;
                                        rect = new Rectangle(itemX - 25, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "战士神技:        (每秒毒物燃烧自身周围造成范围物理伤害!)", font12, Brushes.Bisque, rect);
                                        rect = new Rectangle(itemX + 32, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "毒雾献祭", font12, Brushes.GreenYellow, rect);
                                        itemX -= 25;
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "0级:每秒触发\"初级毒雾\"对自身2*2范围的怪物造成20%最大攻击力的物理伤害且2秒绿毒效果!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "1级:每秒触发\"中级毒雾\"对自身3*3范围的怪物造成30%最大攻击力的物理伤害且2秒绿毒效果!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "2级:每秒触发\"高级毒雾\"对自身4*4范围的怪物造成40%最大攻击力的物理伤害且5秒绿毒效果!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "3级:每秒触发\"神级毒雾\"对自身6*6范围的怪物造成60%最大攻击力的物理伤害且8秒绿毒效果!", font12, Brushes.Lime, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "法师神技:        (击杀怪物触发天降巨雷造成范围魔法伤害!)", font12, Brushes.Bisque, rect);
                                        rect = new Rectangle(itemX + 56, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "天雷尸爆", font12, Brushes.GreenYellow, rect);

                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "0级:杀怪必定触发\"初级天雷\",尸体3*3范围的怪物造成50%最大魔法力的魔法伤害!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "1级:杀怪必定触发\"中级天雷\",尸体4*4范围的怪物造成80%最大魔法力的魔法伤害!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "2级:杀怪必定触发\"高级天雷\",尸体5*5范围的怪物造成100%最大魔法力的魔法伤害!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "3级:杀怪必定触发\"神级天雷\",尸体6*6范围的怪物造成150%最大魔法力的魔法伤害!", font12, Brushes.Lime, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "道士神技:        (怪物死亡后触发\"复生骷髅\"为己用!满级30+只!)", font12, Brushes.Bisque, rect);
                                        rect = new Rectangle(itemX + 56, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "亡灵归来", font12, Brushes.GreenYellow, rect);

                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "0级:杀怪触发\"亡灵归来\",召唤初级复生骷髅持续1分钟，最多10只!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "1级:杀怪触发\"亡灵归来\",召唤中级复生骷髅持续1分钟，最多15只!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "2级:杀怪触发\"亡灵归来\",召唤高级复生骷髅（远程）持续2分钟，最多20只!", font12, Brushes.Lime, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "3级:杀怪触发\"亡灵归来\",召唤神级复生骷髅（远程）持续2分钟，最多30只!", font12, Brushes.Lime, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级条件:", font12, Brushes.Bisque, rect);
                                        rect = new Rectangle(itemX + 56, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "技能1-3级分别需要42级、45级、48级！", font12, Brushes.Cyan, rect);
                                        rect = new Rectangle(itemX + 56, itemY + 15, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "每次提升需要3000万,5000万,8000万金币。", font12, Brushes.Cyan, rect);

                                        itemX += 30;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提升神技", fontUnderline12, Brushes.Yellow, rect);

                                        itemX += 70;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "达到满级", font12, Brushes.Bisque, rect);
                                        itemX += 50;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "点击奖励\"            \"称号,属性爆棚", font12, Brushes.Cyan, rect);

                                        itemX += 56;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "领取大彻大悟", fontUnderline12, Brushes.Yellow, rect);
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "高级宝石":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 60;
                                        itemY += 40;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "注意:“+15阶将拥有防爆背包物品”，弥足珍贵", font13, Brushes.Lime, rect);

                                        itemX -= 40;
                                        itemY += 35;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成宝石", fontUnderline14, Brushes.Yellow, rect);


                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石1", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石2", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石3", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石10", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石11", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石12", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石13", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石14", fontUnderline14, Brushes.Yellow, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "升级宝石15", fontUnderline14, Brushes.Yellow, rect);


                                        itemX += 85;
                                        itemY -= 225;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石需要宝石碎片*10    +200元宝", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+1需要伤害宝石*3 +宝石碎片*100", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+2需要伤害宝石+1*3 +宝石碎片*100", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+3需要伤害宝石+2*3 +宝石碎片*1000", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+10需要伤害宝石+9*1 +30万元宝", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+11需要伤害宝石+10*1 +30万元宝", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+12需要伤害宝石+11*1 +30万元宝", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+13需要伤害宝石+12*1 +30万元宝", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+14需要伤害宝石+13*1 +30万元宝", font14, Brushes.Cyan, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成伤害宝石+15需要伤害宝石+14*1 +30万元宝", font14, Brushes.Cyan, rect);

                                        itemX -= 15;
                                        itemY += 20;
                                        for (int i = 0; i < 5; i++)
                                        {
                                            string newpath = newopUIPath + "000047.png";
                                            using (Image img = Image.FromFile(newpath))
                                            {
                                                if (i == 0) itemX += 18;
                                                else itemX += img.Width + 5;
                                                gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                            }
                                            switch (i)
                                            {
                                                case 0:
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\011384.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX, itemY + 2, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 1:
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\012202.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 2, itemY + 3, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 2:
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\012145.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX - 1, itemY + 3, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 3:
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\012269.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 4:
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\012120.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 4, itemY + 4, img.Width, img.Height);
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "天宫仙境":
                    case "二大陆":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 45;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖天境仙宫〗", font13, Brushes.Cyan, rect);

                                        itemX -= 100;
                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "刷新灭世天帝世界BOSS(6小时刷新)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "等级45级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往天宫仙境(二大陆)", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 192;
                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:内有几张地图爆率超高", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "虚空无尽":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖虚空无尽〗", font13, Brushes.Cyan, rect);

                                        itemX -= 100;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "小怪即爆永恒、毁灭、刑天、灭世", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大BOSS刷新120分钟，小BOSS刷新60分钟", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要开启永恒之力+等级50级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往虚空无尽", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 192;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "缥缈之巅":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖缥缈之巅〗", font13, Brushes.Cyan, rect);

                                        itemX -= 100;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "小怪即爆永恒、毁灭、刑天、灭世(双倍爆率)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大BOSS刷新120分钟，小BOSS刷新60分钟", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要开启永恒之力+等级52级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往缥缈之巅", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 192;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "幽暗村庄":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖幽暗村庄〗", font13, Brushes.Cyan, rect);

                                        itemX -= 100;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "小怪即爆永恒、毁灭、刑天、灭世(三倍爆率)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大BOSS刷新120分钟，小BOSS刷新60分钟", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要开启永恒之力+等级55级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往幽暗村庄", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 192;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;

                    case "沙漠荒原":
                    case "三大陆":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖沙漠荒原〗", font13, Brushes.Cyan, rect);

                                        itemX -= 130;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "小怪即爆永恒、毁灭、刑天、灭世(五倍爆率)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "暗之沙漠BOSS爆终极衣服,大BOSS爆终极武器", font13, Brushes.LightCyan, rect);
                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大小BOSS,60分钟光速刷新", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要开启永恒之力+1000元宝+等级52级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往沙漠荒原", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 165;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "一转圣地":
                    case "三转圣地":
                    case "五转圣地":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 15;
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        switch (npc)
                                        {
                                            case "一转圣地": TextHelper.DrawLeftStrokeString(gh, "组队简单，单兵作战难度大！\n进入条件：一转+永恒之力+55级", font13, Brushes.White, rect); break;
                                            case "三转圣地": TextHelper.DrawLeftStrokeString(gh, "组队简单，单兵作战难度大！\n进入条件：三转+永恒之力+58级", font13, Brushes.White, rect); break;
                                            case "五转圣地": TextHelper.DrawLeftStrokeString(gh, "组队简单，单兵作战难度大！\n进入条件：五转+永恒之力+60级", font13, Brushes.White, rect); break;
                                        }

                                        itemX += 200;
                                        itemY += 100;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往" + npc, fontUnderline14, Brushes.Yellow, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "转生使者":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 15;
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "转身使者—获得神力以及进入转身地图的能力", font13, Brushes.Yellow, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "52级进行一转", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 85, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要等级52级+5万元宝+1转证明*10", font13, Brushes.Red, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "53级进行二转", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 85, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要等级53级+6万元宝+2转证明*10", font13, Brushes.Red, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "54级进行三转", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 85, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要等级54级+7万元宝+3转证明*10", font13, Brushes.Red, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "55级进行四转", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 85, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要等级55级+8万元宝+4转证明*10", font13, Brushes.Red, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "56级进行五转", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 85, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要等级56级+10万元宝+5转证明*10", font13, Brushes.Red, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "查看当前人物转身等级", fontUnderline13, Brushes.Yellow, rect);

                                        itemX += 100;
                                        itemY -= 5;
                                        for (int i = 1; i <= 5; i++)
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "000047.png"))
                                            {
                                                gh.DrawImage(img, itemX + (i * 50), itemY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(itemsPath + $"00106{i}.png"))
                                            {
                                                gh.DrawImage(img, itemX + (i * 50) + 5, itemY + 5, img.Width, img.Height);
                                            }
                                        }
                                        itemX -= 100;
                                        itemY += 43;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "每重转生获得0.1倍攻击,需要小退,5重获得0.5倍攻击加成", font13, Brushes.Yellow, rect);
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "我佛慈悲":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 15;
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "我佛慈悲:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(itemX + 70, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "要求55级", font13, Brushes.LimeGreen, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "我佛慈悲:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(itemX + 70, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "等级+1", font13, Brushes.LimeGreen, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "我佛慈悲:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(itemX + 70, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "成佛后成为金人", font13, Brushes.Gold, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "我佛慈悲:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(itemX + 70, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "几率走火入魔,等级不变但给予封号\"走火入魔\"成为血人", font13, Brushes.OrangeRed, rect);

                                        itemY += 18;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "我佛慈悲:", font13, Brushes.Khaki, rect);
                                        rect = new Rectangle(itemX + 70, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "走火入魔后还可成佛！", font13, Brushes.OrangeRed, rect);

                                        itemX += 100;
                                        itemY += 50;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "我要成佛", fontUnderline13, Brushes.Yellow, rect);

                                        //itemX += 150;
                                        //rect = new Rectangle(itemX, itemY, 0, 0);
                                        //TextHelper.DrawLeftStrokeString(gh, "我要成佛(必成佛)", fontUnderline13, Brushes.Yellow, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "三大神器合成":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 15;
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "玛法大陆失传的神器锻造手段再现，三大圣人往日神器在线合成。", font13, Brushes.Fuchsia, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成神器圣战", fontUnderline13, Brushes.Yellow, rect);

                                        rect = new Rectangle(itemX + 130, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "永恒结晶*1888+20万元宝合成", font13, Brushes.Red, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成神器法神", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 130, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "永恒结晶*1888+20万元宝合成", font13, Brushes.Red, rect);

                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "合成神器天尊", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 130, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "永恒结晶*1888+20万元宝合成", font13, Brushes.Red, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "三大神器展示", font13, Brushes.Red, rect);

                                        itemY += 20;
                                        for (int i = 0; i < 3; i++)
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "000047.png"))
                                            {
                                                gh.DrawImage(img, itemX + (i * 50), itemY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(itemsPath + $"007974.png"))
                                            {
                                                gh.DrawImage(img, itemX + (i * 50) + 8, itemY + 8, img.Width, img.Height);
                                            }
                                            rect = new Rectangle(itemX + (i * 50) + 30, itemY + 27, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "1", font13, Brushes.White, rect);
                                        }

                                        // 画框
                                        itemX = 0;
                                        itemY += 55;
                                        int boxH = 250;
                                        int boxW = 165;

                                        for (int i = 0; i < 3; i++)
                                        {
                                            // 设置矩形的填充颜色为半透明（Alpha = 100）
                                            Color transparentColor = Color.FromArgb(235, 0, 0, 0); // 透明度为100
                                            SolidBrush transparentBrush = new SolidBrush(transparentColor);
                                            // 绘制一个矩形
                                            gh.FillRectangle(transparentBrush, itemX, itemY, boxW, boxH);

                                            string newpath = newopUIPath + "000044.png";

                                            using (Image img = Image.FromFile(newpath))// 线-上
                                            {
                                                gh.DrawImage(img, itemX, itemY, boxW, img.Height);
                                            }
                                            using (Image img = Image.FromFile(newpath))// 线-下
                                            {
                                                gh.DrawImage(img, itemX, itemY + boxH - (img.Height / 2), boxW, img.Height);
                                            }
                                            newpath = newopUIPath + "000045.png";
                                            using (Image img = Image.FromFile(newpath))// 线-左
                                            {
                                                gh.DrawImage(img, itemX, itemY, img.Width, boxH);
                                            }
                                            using (Image img = Image.FromFile(newpath))// 线-右
                                            {
                                                gh.DrawImage(img, itemX + boxW, itemY, img.Width, boxH);
                                            }
                                            newpath = newopUIPath + "000042.png";
                                            using (Image img = Image.FromFile(newpath))// 左上角框框
                                            {
                                                gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                            }
                                            newpath = newopUIPath + "000043.png";
                                            using (Image img = Image.FromFile(newpath))// 左下角框框
                                            {
                                                gh.DrawImage(img, itemX, itemY + boxH - img.Width, img.Width, img.Height);
                                            }
                                            newpath = newopUIPath + "000040.png";
                                            using (Image img = Image.FromFile(newpath))// 右上角框框
                                            {
                                                gh.DrawImage(img, itemX + boxW - (img.Height / 2) - 1, itemY, img.Width, img.Height);
                                            }
                                            newpath = newopUIPath + "000041.png";
                                            using (Image img = Image.FromFile(newpath))// 右下角框框
                                            {
                                                gh.DrawImage(img, itemX + boxW - (img.Height / 2) - 1, itemY + boxH - img.Width + 1, img.Width, img.Height);
                                            }

                                            itemY += 5;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            switch (i)
                                            {
                                                case 0: TextHelper.DrawLeftStrokeString(gh, "圣※战", font13, Brushes.Yellow, rect); break;
                                                case 1: TextHelper.DrawLeftStrokeString(gh, "法※神", font13, Brushes.Yellow, rect); break;
                                                case 2: TextHelper.DrawLeftStrokeString(gh, "天※尊", font13, Brushes.Yellow, rect); break;
                                            }

                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            switch (i)
                                            {
                                                case 0: TextHelper.DrawLeftStrokeString(gh, "战士唯一神器", font13, Brushes.Yellow, rect); break;
                                                case 1: TextHelper.DrawLeftStrokeString(gh, "法师唯一神器", font13, Brushes.Yellow, rect); break;
                                                case 2: TextHelper.DrawLeftStrokeString(gh, "道士唯一神器", font13, Brushes.Yellow, rect); break;
                                            }


                                            itemY += 15;
                                            newpath = newopUIPath + $"00165{i}.png";
                                            using (Image img = Image.FromFile(newpath))
                                            {
                                                gh.DrawImage(img, itemX + 3, itemY + 4, img.Width, img.Height);
                                            }
                                            rect = new Rectangle(itemX + 8, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "三大陆锻造", font13, Brushes.Lime, rect);

                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "[基础属性]", font13, Brushes.LightSkyBlue, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "重量:1", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "持久:10/10", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "防御:2-2", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "魔御:2-2", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "攻击:2-2", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "魔法:2-2", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "道术:2-2", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "需要等级:52", font13, Brushes.White, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "卖给商店:50000", font13, Brushes.Wheat, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "[功能]", font13, Brushes.DeepSkyBlue, rect);
                                            itemY += 15;
                                            rect = new Rectangle(itemX, itemY, 0, 0);

                                            switch (i)
                                            {
                                                case 0: TextHelper.DrawLeftStrokeString(gh, "攻击30%几率触发30%吸血以\n及攻击速度+10%,持续10S", font13, Brushes.DeepSkyBlue, rect); break;
                                                case 1: TextHelper.DrawLeftStrokeString(gh, "法师所有攻击触发额外伤\n害10%!", font13, Brushes.DeepSkyBlue, rect); break;
                                                case 2: TextHelper.DrawLeftStrokeString(gh, "道士召唤兽攻击50%几率触\n发道术上限真实伤害！", font13, Brushes.DeepSkyBlue, rect); break;
                                            }

                                            itemX += 170;
                                            itemY -= 215;
                                        }
                                    }

                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "首饰盒神技":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 15;
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "转生圣地掉落1级神技装备", font13, Brushes.Fuchsia, rect);

                                        itemY += 25;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "2级神技合成", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 80, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "对应1级神技装备*10+1万元宝合成2级神技", font13, Brushes.Red, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "3级神技合成", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 80, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "对应2级神技装备*10+2万元宝合成3级神技", font13, Brushes.Red, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "4级神技合成", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 80, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "对应3级神技装备*10+30万元宝合成4级神技", font13, Brushes.Red, rect);
                                        itemY += 15;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "5级神技合成", fontUnderline13, Brushes.Yellow, rect);
                                        rect = new Rectangle(itemX + 80, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "对应4级神技装备*10+30万元宝合成5级神技", font13, Brushes.Red, rect);

                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "6-7-8-9 对应神技装备*1+元宝+结晶+神珠*3888合成高级神技", font13, Brushes.Red, rect);

                                        itemY += 15;
                                        for (int i = 0; i < 8; i++)
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + "000047.png"))
                                            {
                                                gh.DrawImage(img, itemX + (i * 50), itemY, img.Width, img.Height);
                                            }
                                            using (Image img = Image.FromFile(itemsPath + $"0{11244 + i}.png"))
                                            {
                                                gh.DrawImage(img, itemX + (i * 50) + 5, itemY + 5, img.Width, img.Height);
                                            }
                                            rect = new Rectangle(itemX + (i * 50) + 30, itemY + 27, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "1", font13, Brushes.White, rect);
                                        }

                                    }

                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;

                    case "大帝之城":
                    case "四大陆":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 10;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖大帝之城〗", font13, Brushes.Cyan, rect);

                                        itemX -= 130;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "小怪即爆永恒、毁灭、刑天、灭世(十倍爆率)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "暗之沙漠BOSS爆终极衣服,大BOSS爆终极武器", font13, Brushes.LightCyan, rect);
                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "BOSS爆终极衣服,大BOSS爆终极武器", font13, Brushes.LightCyan, rect);
                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大小BOSS,60分钟光速刷新", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "需要开启永恒之力+5000元宝+等级55级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往大帝之城", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 165;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                        break;
                    case "仙庭战鼓":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 15;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--～～吾乃天帝亲传弟子.为捍卫天庭，驱除邪魔，自愿化为战鼓～～", font10, Brushes.Yellow, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--号声吹战鼓响，我辈出仙庭狂！", font10, Brushes.White, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--尔等邪魔，速速受死！", font10, Brushes.White, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--待到王师回朝日，莫忘当年少年郎！", font10, Brushes.White, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--情难自制！泪湿满襟！", font10, Brushes.White, rect);

                                        itemX += 10;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            string newpath = newopUIPath + "000047.png";
                                            using (Image img = Image.FromFile(newpath))// 物品框
                                            {
                                                if (i == 0) itemY += 18;
                                                else itemY += img.Height + 5;
                                                gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                            }
                                            switch (i)
                                            {
                                                case 0:
                                                    rect = new Rectangle(itemX + 45, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒军鼓需要水晶+永恒结晶", font12, Brushes.LightSkyBlue, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "需要水晶*688 + 永恒结晶*688[100%成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 280, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒军鼓", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\010956.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 1:
                                                    rect = new Rectangle(itemX + 45, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成毁灭军鼓需要永恒军鼓*3", font12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "需要永恒军鼓*3 + 水晶*688 + 永恒结晶*688[100%成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 280, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成毁灭军鼓", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\010957.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 2:
                                                    rect = new Rectangle(itemX + 45, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成沧天军鼓需要毁灭军鼓*1", font12, Brushes.Orange, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "需要毁灭军鼓*1 + 水晶*1888 + 永恒结晶*1888[100%成功]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 280, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成沧天军鼓", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\010958.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 3:
                                                    rect = new Rectangle(itemX + 45, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成秀儿军鼓需要沧天军鼓*1", font12, Brushes.Red, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 15, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "[秀儿军鼓不会爆出]  需要沧天军鼓*1 + 水晶*2888 + 永恒", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 45, itemY + 30, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "结晶*2888[50%成功]  [失败不扣除沧天军鼓]", font12, Brushes.Lime, rect);
                                                    rect = new Rectangle(itemX + 280, itemY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成秀儿军鼓", fontUnderline12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\010959.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "情缘仙人":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景
                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(newopUIPath + "001911.png"))
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }
                                        using (Image img = Image.FromFile(newopUIPath + "000245.png"))
                                        {
                                            gh.DrawImage(img, itemX + 425, itemY + 3, img.Width, img.Height);
                                        }
                                        itemX += 15;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--～～我是天帝门下情缘仙人.有什么需要帮助的吗～～", font10, Brushes.Yellow, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--问世间情为何物，只叫人生死相许！", font10, Brushes.White, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--看你情种深种，特允许兑换永恒鲜花治疗情伤！", font10, Brushes.White, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--上天有好生之德，特允许兑换永恒灵玉温养身体！", font10, Brushes.White, rect);
                                        itemY += 30;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftString(gh, "--爱而不得！是为情丝！ ", font10, Brushes.White, rect);

                                        itemX += 10;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            string newpath = newopUIPath + "000047.png";
                                            using (Image img = Image.FromFile(newpath))// 物品框
                                            {
                                                if (i == 0) itemY += 18;
                                                else itemY += img.Height + 5;
                                                gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                            }
                                            switch (i)
                                            {
                                                case 0:
                                                    rect = new Rectangle(itemX + 45, itemY + 10, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒鲜花需要永恒结晶*1888+水晶*1888+200000元宝", font12, Brushes.DeepSkyBlue, rect);                                                    
                                                    rect = new Rectangle(itemX + 280, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒鲜花", fontUnderline12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 268, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "「            」", font12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\007974.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 1:
                                                    rect = new Rectangle(itemX + 45, itemY + 10, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒灵玉需要永恒结晶*1888+水晶*1888+200000元宝", font12, Brushes.DeepSkyBlue, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒灵玉", fontUnderline12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 268, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "「            」", font12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\010959.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 2:
                                                    rect = new Rectangle(itemX + 45, itemY + 10, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒靴子需要永恒结晶*888+水晶*888+100000元宝", font12, Brushes.DeepSkyBlue, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒靴子", fontUnderline12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 268, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "「            」", font12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\008048.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                                case 3:
                                                    rect = new Rectangle(itemX + 45, itemY + 10, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒腰带需要永恒结晶*888+水晶*888+100000元宝", font12, Brushes.DeepSkyBlue, rect);
                                                    rect = new Rectangle(itemX + 280, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "合成永恒腰带", fontUnderline12, Brushes.Yellow, rect);
                                                    rect = new Rectangle(itemX + 268, itemY + 24, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, "「            」", font12, Brushes.Yellow, rect);
                                                    newpath = LegendFilePath.LegendIMGDataPath + "Items\\008038.png";
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        gh.DrawImage(img, itemX + 5, itemY + 5, img.Width, img.Height);
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                    case "天帝陵墓":
                    case "天帝炼狱":
                    case "天帝足球场":
                    case "天帝后花园":
                    case "天帝奴隶场":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 10;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"〖{npc}〗", font13, Brushes.Cyan, rect);

                                        itemX -= 130;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "追寻刑天,灭世的终极之地(十倍爆率)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大小BOSS,60分钟光速刷新", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "5000元宝+等级55级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"前往{npc}1", fontUnderline14, Brushes.Yellow, rect);
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"前往{npc}2", fontUnderline14, Brushes.Yellow, rect);
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, $"前往{npc}3", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 165;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                        break;
                    case "轮回之地":

                    case "五大陆":
                        {
                            using (Image imgMian = Image.FromFile(path))
                            {
                                using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                {
                                    using (Graphics gh = Graphics.FromImage(bitmap))
                                    {
                                        gh.CompositingQuality = CompositingQuality.HighQuality;
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        int itemX = 0, itemY = 15;
                                        using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                        {
                                            gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                        }

                                        itemX += 24;
                                        itemY += 10;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×┅┅∽┅┅∽┅┅∽┅            ┅-∽┅┅-∽┅┅∽┅┅×", font13, Brushes.Khaki, rect);

                                        itemX += 148;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "〖轮回之地〗", font13, Brushes.Cyan, rect);

                                        itemX -= 130;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "怪物爆率:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "追寻轮回的终极之地(十倍爆率)", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "来这就对了,小伙子", font13, Brushes.LightCyan, rect);
                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "主要BOSS:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "大小BOSS,60分钟光速刷新", font13, Brushes.LightCyan, rect);

                                        itemX -= 80;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "进入条件:", font13, Brushes.Lime, rect);
                                        itemX += 80;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "转生等级5+10000元宝+等级60级", font13, Brushes.MistyRose, rect);
                                        itemX += 64;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "前往轮回之地", fontUnderline14, Brushes.Yellow, rect);

                                        itemX -= 165;
                                        itemY += 20;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "×━━━━━━━━━━━━━━━━━━━━━━━━━━━×", font13, Brushes.Khaki, rect);

                                        itemX += 47;
                                        itemY += 12;
                                        rect = new Rectangle(itemX, itemY, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "提示:BOSS非常凶残，建议组队前往，小心!", font13, Brushes.Red, rect);

                                    }
                                    path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                                    bitmap.Save(path, ImageFormat.Png);
                                }
                            }
                        }
                        break;
                        break;
                }

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 礼包码获取礼物
        /// </summary>
        public static string GetGifByCode(UserInfo user, string code)
        {
            string msg = "";
            try
            {
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);


                string lbm = config.Read("配置信息", "礼包码");
                if (string.IsNullOrEmpty(code)) throw new Exception("礼包码不能为空!");
                if (lbm != code) throw new Exception("礼包码输入错误!");

                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                string isLQ = userInI.Read("人物信息", "是否领取礼包码");
                if (isLQ == "是") throw new Exception("对不起,您已领取过起步礼包.");

                List<StditemDB> stdList = LegendApi.GetStdItemsList();
                List<StditemDB> std2List = new List<StditemDB>();

                #region 领取对应职业装备
                StditemDB std = stdList.Where(s => s.StdName == "黑铁头盔").First();
                std.StdItemMap = user.Map;
                std.StdItemOwner = user.Name;
                std.StdItemStuff = "NPC制造";
                std.StdItemTime = DateTime.Now.ToString();
                std2List.Add(std);
                switch (user.Job)
                {
                    case "战士":
                        {
                            std = stdList.Where(s => s.StdName == "幽灵手套").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std2List.Add(std);
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "龙之戒指").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std2List.Add(std);
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "幽灵项链").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "井中月").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std2List.Add(std);
                            if (user.Sex == "男")
                            {
                                std = stdList.Where(s => s.StdName == "战神盔甲(男)").First();
                            }
                            else
                            {
                                std = stdList.Where(s => s.StdName == "战神盔甲(女)").First();
                            }

                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std2List.Add(std);

                            msg = $"恭喜玩家[{user.Name}],成功领取新手礼包【战士套装】并开通了会员特权!输入 会员特权 查看更多.";
                        }
                        break;
                    case "道士":
                        {
                            std = stdList.Where(s => s.StdName == "心灵手镯").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "铂金戒指").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "天珠项链").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "银蛇").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            if (user.Sex == "男")
                            {
                                std = stdList.Where(s => s.StdName == "幽灵战衣(男)").First();
                            }
                            else
                            {
                                std = stdList.Where(s => s.StdName == "幽灵战衣(女)").First();
                            }

                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            msg = $"恭喜玩家[{user.Name}],成功领取新手礼包【道士套装】并开通了会员特权!输入 会员特权 查看更多.";
                        }
                        break;
                    case "法师":
                        {
                            std = stdList.Where(s => s.StdName == "思贝儿手镯").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "红宝石戒指").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "生命项链").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "魔杖").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            if (user.Sex == "男")
                            {
                                std = stdList.Where(s => s.StdName == "恶魔长袍(男)").First();
                            }
                            else
                            {
                                std = stdList.Where(s => s.StdName == "恶魔长袍(女)").First();
                            }
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);
                            msg = $"恭喜玩家[{user.Name}],成功领取新手礼包【法师套装】并开通了会员特权!输入 会员特权 查看更多.";
                        }
                        break;
                    default: throw new Exception("对不起,您当前未注册,请先注册游戏帐号,输入 注册 查看更多.");
                }
                #endregion

                // 将对应职业装备放入背包中
                var pblist = userInI.GetAllKeys("背包物品");
                if (pblist.Count > 88) throw new Exception("对不起,背包内不满8个以上的空格.");
                foreach (var item in std2List)
                {
                    userInI.Write("背包物品", config.GetRndId(), Api.JsonSerialize(item));
                }
                user.User_General.VipLv = 1;
                userInI.Write("普通数据", "VIP等级", user.User_General.VipLv.ToString());
                userInI.Write("人物信息", "是否领取礼包码", "是");

                userInI.Write("VIP属性", "Hp", "10");
                userInI.Write("VIP属性", "Mp", "10");
                userInI.Write("VIP属性", "防御下限", "3");
                userInI.Write("VIP属性", "防御上限", "3");
                userInI.Write("VIP属性", "魔防下限", "3");
                userInI.Write("VIP属性", "魔防上限", "3");
                userInI.Write("VIP属性", "攻击下限", "3");
                userInI.Write("VIP属性", "攻击上限", "3");
                userInI.Write("VIP属性", "魔法下限", "3");
                userInI.Write("VIP属性", "魔法上限", "3");
                userInI.Write("VIP属性", "道术下限", "3");
                userInI.Write("VIP属性", "道术上限", "3");
                userInI.Write("VIP属性", "倍功", "0");
                userInI.Write("VIP属性", "打怪经验", "30");
                userInI.Write("VIP属性", "自动挂机", "否");
                userInI.Write("VIP属性", "自动回收", "否");
                userInI.Write("VIP属性", "专属BOOS", "3");
                userInI.Write("VIP属性", "已击杀专属BOOS数量", "0");
                userInI.Write("VIP属性", "专属BOSS击杀时间", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        /// <summary>
        /// 商铺物品查看
        /// </summary>
        public static string GetShopByName(UserInfo user, string name, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                List<StditemDB> listStditemDB = LegendApi.GetStdItemsList();

                UserInfoImg userInfoImg = new UserInfoImg(user);
                path = userInfoImg.GamgeMainForm(user, false, true, true, true, x, y, w, h);

                Rectangle rect = new Rectangle(50, 50, 0, 0);

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            using (Image img = Image.FromFile(prguse3Path + "000298.png"))// 商铺框
                            {
                                gh.DrawImage(img, 230, 100, img.Width, img.Height);
                            }
                            using (Image img = Image.FromFile(prguse3Path + "000293.png"))
                            {
                                gh.DrawImage(img, 246, 136, img.Width, img.Height);
                            }

                            rect = new Rectangle(455, 452, 0, 0);
                            TextHelper.DrawLeftString(gh, "(1/1)页", font10, Brushes.White, rect);

                            List<string> list = new List<string>();
                            int stdX = 0, stdY = 0;
                            switch (name)
                            {
                                case "查看商铺装饰":
                                case "查看商铺":
                                    using (Image img = Image.FromFile(prguse3Path + "000299.png"))// 装饰
                                    {
                                        gh.DrawImage(img, 407, 114, img.Width, img.Height);
                                    }
                                    path = LegendFilePath.ShopPath + "商铺装饰.txt";
                                    list = FileHelper.ReadTxtReturnList(path);
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        var arrShop = list[i].Split(',');
                                        var std = listStditemDB.Where(s => s.StdName == arrShop[0]).First();
                                        int price = int.Parse(arrShop[3]);
                                        switch (i)
                                        {
                                            case 0:
                                                stdX = 415;
                                                stdY = 170;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 1:
                                                stdX = 585;
                                                stdY = 170;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 2:
                                                stdX = 415;
                                                stdY = 225;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 3:
                                                stdX = 585;
                                                stdY = 230;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 11;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 4:
                                                stdX = 420;
                                                stdY = 280;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 35;
                                                stdY -= 7;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 5:
                                                stdX = 587;
                                                stdY = 283;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 10;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 6:
                                                stdX = 416;
                                                stdY = 334;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 7:
                                                stdX = 590;
                                                stdY = 335;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 35;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 8:
                                                stdX = 416;
                                                stdY = 390;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 12;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 9:
                                                stdX = 587;
                                                stdY = 387;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 8;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                        }
                                    }
                                    break;
                                case "查看商铺补给":
                                    using (Image img = Image.FromFile(prguse3Path + "000300.png"))// 补给
                                    {
                                        gh.DrawImage(img, 465, 114, img.Width, img.Height);
                                    }
                                    path = LegendFilePath.ShopPath + "商铺补给.txt";
                                    list = FileHelper.ReadTxtReturnList(path);
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        var arrShop = list[i].Split(',');
                                        var std = listStditemDB.Where(s => s.StdName == arrShop[0]).First();
                                        int price = int.Parse(arrShop[3]);
                                        switch (i)
                                        {
                                            case 0:
                                                stdX = 421;
                                                stdY = 172;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 1:
                                                stdX = 592;
                                                stdY = 172;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 2:
                                                stdX = 420;
                                                stdY = 228;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 7;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 3:
                                                stdX = 591;
                                                stdY = 228;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 4:
                                                stdX = 420;
                                                stdY = 282;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 5:
                                                stdX = 591;
                                                stdY = 282;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 6:
                                                stdX = 416;
                                                stdY = 334;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 7:
                                                stdX = 590;
                                                stdY = 335;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 35;
                                                stdY -= 9;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 8:
                                                stdX = 416;
                                                stdY = 390;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 12;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 9:
                                                stdX = 587;
                                                stdY = 387;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 8;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                        }
                                    }
                                    break;
                                case "查看商铺强化":
                                    using (Image img = Image.FromFile(prguse3Path + "000301.png"))// 强化
                                    {
                                        gh.DrawImage(img, 523, 114, img.Width, img.Height);
                                    }
                                    path = LegendFilePath.ShopPath + "商铺强化.txt";
                                    list = FileHelper.ReadTxtReturnList(path);
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        var arrShop = list[i].Split(',');
                                        var std = listStditemDB.Where(s => s.StdName == arrShop[0]).First();
                                        int price = int.Parse(arrShop[3]);
                                        switch (i)
                                        {
                                            case 0:
                                                stdX = 416;
                                                stdY = 170;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 1:
                                                stdX = 592;
                                                stdY = 170;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 2:
                                                stdX = 416;
                                                stdY = 225;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 3:
                                                stdX = 592;
                                                stdY = 224;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 4:
                                                stdX = 416;
                                                stdY = 278;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 5:
                                                stdX = 592;
                                                stdY = 278;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 6:
                                                stdX = 416;
                                                stdY = 332;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 7:
                                                stdX = 592;
                                                stdY = 332;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 8:
                                                stdX = 416;
                                                stdY = 388;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 9:
                                                stdX = 587;
                                                stdY = 390;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 8;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                        }
                                    }
                                    break;
                                case "查看商铺好友":
                                    using (Image img = Image.FromFile(prguse3Path + "000302.png"))// 好友
                                    {
                                        gh.DrawImage(img, 581, 114, img.Width, img.Height);
                                    }
                                    path = LegendFilePath.ShopPath + "商铺好友.txt";
                                    list = FileHelper.ReadTxtReturnList(path);
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        var arrShop = list[i].Split(',');
                                        var std = listStditemDB.Where(s => s.StdName == arrShop[0]).First();
                                        int price = int.Parse(arrShop[3]);
                                        switch (i)
                                        {
                                            case 0:
                                                stdX = 416;
                                                stdY = 171;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 1:
                                                stdX = 592;
                                                stdY = 170;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 2:
                                                stdX = 422;
                                                stdY = 225;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 3:
                                                stdX = 586;
                                                stdY = 222;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 44;
                                                stdY -= 2;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 4:
                                                stdX = 420;
                                                stdY = 280;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 5:
                                                stdX = 589;
                                                stdY = 285;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 11;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 6:
                                                stdX = 415;
                                                stdY = 334;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 44;
                                                stdY -= 6;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 7:
                                                stdX = 591;
                                                stdY = 338;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 10;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 8:
                                                stdX = 416;
                                                stdY = 388;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 9:
                                                stdX = 587;
                                                stdY = 390;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 8;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                        }
                                    }
                                    break;
                                case "查看商铺限量":
                                    using (Image img = Image.FromFile(prguse3Path + "000303.png"))// 限量
                                    {
                                        gh.DrawImage(img, 638, 114, img.Width, img.Height);
                                    }
                                    path = LegendFilePath.ShopPath + "商铺限量.txt";
                                    list = FileHelper.ReadTxtReturnList(path);
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        var arrShop = list[i].Split(',');
                                        var std = listStditemDB.Where(s => s.StdName == arrShop[0]).First();
                                        int price = int.Parse(arrShop[3]);
                                        switch (i)
                                        {
                                            case 0:
                                                stdX = 419;
                                                stdY = 173;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 8;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 1:
                                                stdX = 586;
                                                stdY = 170;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 45;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 2:
                                                stdX = 423;
                                                stdY = 226;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 35;
                                                stdY -= 7;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 3:
                                                stdX = 592;
                                                stdY = 226;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 6;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 4:
                                                stdX = 423;
                                                stdY = 280;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 35;
                                                stdY -= 6;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 5:
                                                stdX = 592;
                                                stdY = 280;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 7;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 6:
                                                stdX = 423;
                                                stdY = 335;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 35;
                                                stdY -= 7;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 7:
                                                stdX = 592;
                                                stdY = 335;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 38;
                                                stdY -= 7;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 8:
                                                stdX = 416;
                                                stdY = 388;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 40;
                                                stdY -= 5;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                            case 9:
                                                stdX = 587;
                                                stdY = 390;
                                                using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                                {
                                                    gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                                }
                                                stdX += 42;
                                                stdY -= 8;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, arrShop[1], font9, Brushes.White, rect);

                                                stdY += 14;
                                                rect = new Rectangle(stdX, stdY, 0, 0);
                                                TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                                break;
                                        }
                                    }
                                    break;
                            }

                            path = LegendFilePath.ShopPath + "商铺奇珍榜.txt";
                            list = FileHelper.ReadTxtReturnList(path);

                            for (int i = 0; i < list.Count; i++)
                            {
                                var arrShop = list[i].Split(',');
                                var std = listStditemDB.Where(s => s.StdName == arrShop[0]).First();
                                int price = int.Parse(arrShop[3]);
                                switch (i)
                                {
                                    case 0:
                                        stdX = 790;
                                        stdY = 168;
                                        using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                        {
                                            gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                        }
                                        stdX -= 40;
                                        stdY += 32;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                        stdY += 12;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                        break;
                                    case 1:
                                        stdX = 800;
                                        stdY = 232;
                                        using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                        {
                                            gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                        }
                                        stdX -= 50;
                                        stdY += 33;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                        stdY += 12;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                        break;
                                    case 2:
                                        stdX = 790;
                                        stdY = 297;
                                        using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                        {
                                            gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                        }
                                        stdX -= 40;
                                        stdY += 33;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                        stdY += 12;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                        break;
                                    case 3:
                                        stdX = 800;
                                        stdY = 362;
                                        using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                        {
                                            gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                        }
                                        stdX -= 50;
                                        stdY += 33;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                        stdY += 12;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                        break;
                                    case 4:
                                        stdX = 790;
                                        stdY = 427;
                                        using (Image img = Image.FromFile(itemsPath + std.StdImgPath))
                                        {
                                            gh.DrawImage(img, stdX, stdY, img.Width, img.Height);
                                        }
                                        stdX -= 40;
                                        stdY += 32;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, std.StdName, font9, Brushes.White, rect);

                                        stdY += 12;
                                        rect = new Rectangle(stdX, stdY, 0, 0);
                                        TextHelper.DrawLeftString(gh, price.ToString("N0", CultureInfo.InvariantCulture) + " " + arrShop[4], font9, Brushes.White, rect);
                                        break;
                                }
                            }

                        }
                        path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
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
        /// 获取赞助礼包
        /// </summary>
        public static string GetGifByCode2(UserInfo user, string code)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                List<StditemDB> stdList = LegendApi.GetStdItemsList();
                List<StditemDB> std2List = new List<StditemDB>();

                switch (code)
                {
                    case "白嫖领取礼包1":
                    case "赞助领取礼包1":
                        {
                            string isFlag = userInI.Read("人物信息", "是否领取唯一赞助1");
                            if (user.User_General.VipLv <= 0) throw new Exception("对不起,您未领取起步礼包,请在盟重输入 起步礼包 再来兑换吧!");
                            if (isFlag == "是") throw new Exception("对不起,您已领取过当前赞助礼包!");
                            if (user.PageQty < 5) throw new Exception("对不起,您背包格子小于5个,请先预留下5个格子吧!");
                            string isby = userInI.Read("人物信息", "是否升级白银VIP");
                            if (isby != "是")
                            {
                                if (code == "赞助领取礼包1")
                                {

                                    if (user.User_General.JGS < 10) throw new Exception("对不起,您的金刚石数量不够!当前赞助需要10金刚石!");
                                    user.User_General.JGS -= 10;
                                    userInI.Write("普通数据", "金刚石", user.User_General.JGS.ToString());
                                }
                                if (code == "白嫖领取礼包1")
                                {
                                    if (user.User_General.YB < 2000) throw new Exception("对不起,您的元宝数量不够!当前赞助需要2000元宝!");
                                    user.User_General.YB -= 2000;
                                    userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());
                                }
                            }
                            user.User_Attribute.MonExpRate = 10;
                            userInI.Write("属性点", "怪物爆率", user.User_Attribute.MonExpRate + "");

                            path = $"恭喜玩家[{user.Name}]领取了10元赞助礼包,会员等级可升为白银会员,发送 会员特权 查看更多.";
                            userInI.Write("人物信息", "是否领取唯一赞助1", "是");

                            StditemDB std = stdList.Where(s => s.StdName == "普通斗笠").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "伤害宝石+1").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "隔山打牛一阶").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "超级聚灵珠").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std.SubAttr1 = 3;
                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "无字天书").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std.SubAttr1 = 10;
                            std2List.Add(std);
                        }
                        break;
                    case "白嫖领取礼包2":
                    case "赞助领取礼包2":
                        {
                            string isFlag1 = userInI.Read("人物信息", "是否领取唯一赞助1");
                            string isFlag2 = userInI.Read("人物信息", "是否领取唯一赞助2");
                            if (user.User_General.VipLv <= 0) throw new Exception("对不起,您未领取起步礼包,请在盟重输入 起步礼包 再来兑换吧!");
                            if (string.IsNullOrEmpty(isFlag1)) throw new Exception("对不起,您未领取赞助礼包1,请先领取赞助领取礼包1再来吧!");
                            if (isFlag1 == "否") throw new Exception("对不起,您未领取赞助礼包1,请先领取赞助领取礼包1再来吧!");
                            if (isFlag2 == "是") throw new Exception("对不起,您已领取过当前赞助礼包!");
                            if (user.PageQty < 5) throw new Exception("对不起,您背包格子小于5个,请先预留下5个格子吧!");
                            var pbList = userInI.GetAllKeys("背包物品");
                            StditemDB std = null;
                            Dictionary<string, bool> kvPbList = new Dictionary<string, bool>();
                            kvPbList.Add("普通斗笠", false);
                            kvPbList.Add("伤害宝石+1", false);
                            kvPbList.Add("隔山打牛一阶", false);
                            foreach (var key in pbList)
                            {
                                std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                                if (kvPbList.ContainsKey(std.StdName))
                                    kvPbList[std.StdName] = true;
                            }
                            foreach (var key in kvPbList)
                            {
                                if (!key.Value) throw new Exception($"领取失败,您背包内没有物品[{key.Key}]!");
                            }
                            string isby = userInI.Read("人物信息", "是否升级黄金VIP");
                            if (isby != "是")
                            {
                                if (code == "赞助领取礼包2")
                                {
                                    if (user.User_General.JGS < 20) throw new Exception("对不起,您的金刚石数量不够!当前赞助需要20金刚石!");
                                    user.User_General.JGS -= 20;
                                    userInI.Write("普通数据", "金刚石", user.User_General.JGS.ToString());
                                }
                                if (code == "白嫖领取礼包2")
                                {
                                    if (user.User_General.YB < 4000) throw new Exception("对不起,您的元宝数量不够!当前赞助需要4000元宝!");
                                    user.User_General.YB -= 4000;
                                    userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());
                                }
                            }
                            user.User_Attribute.MonExpRate = 20;
                            userInI.Write("属性点", "怪物爆率", user.User_Attribute.MonExpRate + "");

                            path = $"恭喜玩家[{user.Name}]领取了20元赞助礼包,会员等级可升为黄金会员,发送 会员特权 查看更多.";
                            foreach (var key in pbList)
                            {
                                std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                                if (kvPbList.ContainsKey(std.StdName))
                                    userInI.Delete("背包物品", key);
                            }
                            userInI.Write("人物信息", "是否领取唯一赞助2", "是");

                            std = stdList.Where(s => s.StdName == "赤月斗笠").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "伤害宝石+2").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "隔山打牛二阶").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "幸运神链").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "无字天书").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std.SubAttr1 = 20;
                            std2List.Add(std);
                        }
                        break;
                    case "白嫖领取礼包3":
                    case "赞助领取礼包3":
                        {
                            string isFlag1 = userInI.Read("人物信息", "是否领取唯一赞助1");
                            string isFlag2 = userInI.Read("人物信息", "是否领取唯一赞助2");
                            string isFlag3 = userInI.Read("人物信息", "是否领取唯一赞助3");
                            if (user.User_General.VipLv <= 0) throw new Exception("对不起,您未领取起步礼包,请在盟重输入 起步礼包 再来兑换吧!");
                            if (string.IsNullOrEmpty(isFlag1)) throw new Exception("对不起,您未领取赞助礼包1,请先领取赞助领取礼包1再来吧!");
                            if (string.IsNullOrEmpty(isFlag2)) throw new Exception("对不起,您未领取赞助礼包2,请先领取赞助领取礼包2再来吧!");

                            if (isFlag1 == "否") throw new Exception("对不起,您未领取赞助礼包1,请先领取赞助领取礼包1再来吧!");
                            if (isFlag2 == "否") throw new Exception("对不起,您未领取赞助礼包2,请先领取赞助领取礼包2再来吧!");
                            if (isFlag3 == "是") throw new Exception("对不起,您已领取过当前赞助礼包!");
                            if (user.PageQty < 5) throw new Exception("对不起,您背包格子小于5个,请先预留下5个格子吧!");
                            var pbList = userInI.GetAllKeys("背包物品");
                            StditemDB std = null;
                            Dictionary<string, bool> kvPbList = new Dictionary<string, bool>();
                            kvPbList.Add("赤月斗笠", false);
                            kvPbList.Add("伤害宝石+2", false);
                            kvPbList.Add("隔山打牛二阶", false);
                            foreach (var key in pbList)
                            {
                                std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                                if (kvPbList.ContainsKey(std.StdName))
                                    kvPbList[std.StdName] = true;
                            }
                            foreach (var key in kvPbList)
                            {
                                if (!key.Value) throw new Exception($"领取失败,您背包内没有物品[{key.Key}]!");
                            }
                            string isby = userInI.Read("人物信息", "是否升级钻石VIP");
                            if (isby != "是")
                            {
                                if (code == "赞助领取礼包3")
                                {
                                    if (user.User_General.JGS < 30) throw new Exception("对不起,您的金刚石数量不够!当前赞助需要30金刚石!");
                                    user.User_General.JGS -= 30;
                                    userInI.Write("普通数据", "金刚石", user.User_General.JGS.ToString());
                                }
                                if (code == "白嫖领取礼包3")
                                {
                                    if (user.User_General.YB < 6000) throw new Exception("对不起,您的元宝数量不够!当前赞助需要6000元宝!");
                                    user.User_General.YB -= 6000;
                                    userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());
                                }
                            }
                            user.User_Attribute.MonExpRate = 30;
                            userInI.Write("属性点", "怪物爆率", user.User_Attribute.MonExpRate + "");

                            path = $"恭喜玩家[{user.Name}]领取了30元赞助礼包,会员等级可升为钻石会员,发送 会员特权 查看更多.";
                            foreach (var key in pbList)
                            {
                                std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                                if (kvPbList.ContainsKey(std.StdName))
                                    userInI.Delete("背包物品", key);
                            }
                            userInI.Write("人物信息", "是否领取唯一赞助3", "是");

                            std = stdList.Where(s => s.StdName == "永恒斗笠").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "伤害宝石+3").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "隔山打牛三阶").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "隐身戒指").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();

                            std2List.Add(std);

                            std = stdList.Where(s => s.StdName == "无字天书").First();
                            std.StdItemMap = user.Map;
                            std.StdItemOwner = user.Name;
                            std.StdItemStuff = "NPC制造";
                            std.StdItemTime = DateTime.Now.ToString();
                            std.SubAttr1 = 30;
                            std2List.Add(std);
                        }
                        break;
                }

                foreach (var item in std2List)
                {
                    userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(item));
                }

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 会员升级
        /// </summary>
        public static string VIPUpgrade(UserInfo user, string vipName, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                if (user.User_General.VipLv <= 0) throw new Exception("对不起,您未领取起步礼包,请在盟重输入 起步礼包 再来升级吧!");

                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                string zanzhu1 = userInI.Read("人物信息", "是否领取唯一赞助1");
                string zanzhu2 = userInI.Read("人物信息", "是否领取唯一赞助2");
                string zanzhu3 = userInI.Read("人物信息", "是否领取唯一赞助3");

                if (vipName == "赞助升级会员")
                {
                    switch (user.User_General.VipLv)
                    {
                        case 1: if (zanzhu1 != "是") throw new Exception("小子,你都未领取过赞助礼包就想来坑蒙拐骗,玩球去吧!"); break;
                        case 2: if (zanzhu1 != "是" || zanzhu2 != "是") throw new Exception("小子,你都未领取过赞助礼包就想来坑蒙拐骗,玩球去吧!"); break;
                        case 3: if (zanzhu1 != "是" || zanzhu2 != "是" || zanzhu3 != "是") throw new Exception("小子,你都未领取过赞助礼包就想来坑蒙拐骗,玩球去吧!"); break;
                        case 4:
                            if (user.User_General.YB < 10000) throw new Exception("小子,升级至尊会员需要10000元宝,你看看你多少,玩球去吧!");
                            user.User_General.YB -= 10000;
                            break;
                        default: throw new Exception("噢,大佬,你已经升级为最高等级的会员了!");
                    }
                }
                if (vipName == "元宝升级会员")
                {
                    switch (user.User_General.VipLv)
                    {
                        case 1:
                            if (user.User_General.YB < 2000) throw new Exception("小子,你连2000元宝都没有就想来坑蒙拐骗,玩球去吧!");
                            user.User_General.YB -= 2000;
                            break;
                        case 2:
                            if (user.User_General.YB < 4000) throw new Exception("小子,你连4000元宝都没有就想来坑蒙拐骗,玩球去吧!");
                            user.User_General.YB -= 4000;
                            break;
                        case 3:
                            if (user.User_General.YB < 6000) throw new Exception("小子,你连6000元宝都没有就想来坑蒙拐骗,玩球去吧!");
                            user.User_General.YB -= 6000;
                            break;
                        case 4:
                            if (user.User_General.YB < 10000) throw new Exception("小子,升级至尊会员需要10000元宝,你看看你多少,玩球去吧!");
                            user.User_General.YB -= 10000;
                            break;
                        default: throw new Exception("噢,大佬,你已经升级为最高等级的会员了!");
                    }
                }

                user.User_General.VipLv += 1;
                switch (user.User_General.VipLv)
                {
                    case 2:
                        user.User_VIPAttribute.Hp = 30;
                        user.User_VIPAttribute.Mp = 30;
                        user.User_VIPAttribute.Ac = 6;
                        user.User_VIPAttribute.Ac2 = 6;
                        user.User_VIPAttribute.Mac = 6;
                        user.User_VIPAttribute.Mac2 = 6;
                        user.User_VIPAttribute.Dc = 6;
                        user.User_VIPAttribute.DcMax = 6;
                        user.User_VIPAttribute.Mc = 6;
                        user.User_VIPAttribute.McMax = 6;
                        user.User_VIPAttribute.Sc = 6;
                        user.User_VIPAttribute.ScMax = 6;
                        user.User_VIPAttribute.DouBPower = 50;
                        user.User_VIPAttribute.MonExpInc = 100;
                        user.User_VIPAttribute.AutoBoos = 6;
                        userInI.Write("人物信息", "是否升级白银VIP", "是");
                        path = $"恭喜玩家[{user.Name}]升级为白银VIP会员,快人一步发展";
                        break;
                    case 3:
                        user.User_VIPAttribute.Hp = 100;
                        user.User_VIPAttribute.Mp = 100;
                        user.User_VIPAttribute.Ac = 10;
                        user.User_VIPAttribute.Ac2 = 10;
                        user.User_VIPAttribute.Mac = 10;
                        user.User_VIPAttribute.Mac2 = 10;
                        user.User_VIPAttribute.Dc = 10;
                        user.User_VIPAttribute.DcMax = 10;
                        user.User_VIPAttribute.Mc = 10;
                        user.User_VIPAttribute.McMax = 10;
                        user.User_VIPAttribute.Sc = 10;
                        user.User_VIPAttribute.ScMax = 10;
                        user.User_VIPAttribute.DouBPower = 80;
                        user.User_VIPAttribute.MonExpInc = 200;
                        user.User_VIPAttribute.AutoBoos = 10;
                        user.User_VIPAttribute.AutoRecycl = "是";
                        user.User_VIPAttribute.IsRecycleYB = "是";
                        user.User_VIPAttribute.RecycleYBQty = 100;
                        userInI.Write("人物信息", "是否升级黄金VIP", "是");
                        path = $"恭喜玩家[{user.Name}]升级为黄金VIP会员,快人一步发展";
                        break;
                    case 4:
                        user.User_VIPAttribute.Hp = 200;
                        user.User_VIPAttribute.Mp = 200;
                        user.User_VIPAttribute.Ac = 20;
                        user.User_VIPAttribute.Ac2 = 20;
                        user.User_VIPAttribute.Mac = 20;
                        user.User_VIPAttribute.Mac2 = 20;
                        user.User_VIPAttribute.Dc = 20;
                        user.User_VIPAttribute.DcMax = 20;
                        user.User_VIPAttribute.Mc = 20;
                        user.User_VIPAttribute.McMax = 20;
                        user.User_VIPAttribute.Sc = 20;
                        user.User_VIPAttribute.ScMax = 20;
                        user.User_VIPAttribute.DouBPower = 150;
                        user.User_VIPAttribute.MonExpInc = 300;
                        user.User_VIPAttribute.AutoBoos = 20;
                        user.User_VIPAttribute.AutoAFK = "是";
                        user.User_VIPAttribute.RecycleYBQty = 200;
                        userInI.Write("人物信息", "是否升级钻石VIP", "是");
                        path = $"恭喜玩家[{user.Name}]升级为钻石VIP会员,快人一步发展";
                        break;
                    case 5:
                        user.User_VIPAttribute.Hp = 500;
                        user.User_VIPAttribute.Mp = 500;
                        user.User_VIPAttribute.Ac = 30;
                        user.User_VIPAttribute.Ac2 = 30;
                        user.User_VIPAttribute.Mac = 30;
                        user.User_VIPAttribute.Mac2 = 30;
                        user.User_VIPAttribute.Dc = 30;
                        user.User_VIPAttribute.DcMax = 30;
                        user.User_VIPAttribute.Mc = 30;
                        user.User_VIPAttribute.McMax = 30;
                        user.User_VIPAttribute.Sc = 30;
                        user.User_VIPAttribute.ScMax = 30;
                        user.User_VIPAttribute.DouBPower = 300;
                        user.User_VIPAttribute.MonExpInc = 500;
                        user.User_VIPAttribute.AutoBoos = 30;
                        user.User_VIPAttribute.RecycleYBQty = 300;
                        userInI.Write("人物信息", "是否升级至尊VIP", "是");
                        path = $"恭喜玩家[{user.Name}]升级为至尊VIP会员,快人一步发展";

                        user.IsOnTitle = "是";
                        userInI.Write("人物信息", "是否开启称号", "是");
                        break;
                }
                userInI.Write("普通数据", "VIP等级", user.User_General.VipLv.ToString());
                userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());

                userInI.Write("VIP属性", "Hp", user.User_VIPAttribute.Hp.ToString());
                userInI.Write("VIP属性", "Mp", user.User_VIPAttribute.Mp.ToString());
                userInI.Write("VIP属性", "防御下限", user.User_VIPAttribute.Ac.ToString());
                userInI.Write("VIP属性", "防御上限", user.User_VIPAttribute.Ac2.ToString());
                userInI.Write("VIP属性", "魔防下限", user.User_VIPAttribute.Mac.ToString());
                userInI.Write("VIP属性", "魔防上限", user.User_VIPAttribute.Mac2.ToString());
                userInI.Write("VIP属性", "攻击下限", user.User_VIPAttribute.Dc.ToString());
                userInI.Write("VIP属性", "攻击上限", user.User_VIPAttribute.DcMax.ToString());
                userInI.Write("VIP属性", "魔法下限", user.User_VIPAttribute.Mc.ToString());
                userInI.Write("VIP属性", "魔法上限", user.User_VIPAttribute.McMax.ToString());
                userInI.Write("VIP属性", "道术下限", user.User_VIPAttribute.Sc.ToString());
                userInI.Write("VIP属性", "道术上限", user.User_VIPAttribute.ScMax.ToString());
                userInI.Write("VIP属性", "倍功", user.User_VIPAttribute.DouBPower.ToString());
                userInI.Write("VIP属性", "打怪经验", user.User_VIPAttribute.MonExpInc.ToString());
                userInI.Write("VIP属性", "自动挂机", user.User_VIPAttribute.AutoAFK);
                userInI.Write("VIP属性", "自动回收", user.User_VIPAttribute.AutoRecycl);
                userInI.Write("VIP属性", "专属BOOS", user.User_VIPAttribute.AutoBoos.ToString());
                userInI.Write("VIP属性", "是否回收元宝", user.User_VIPAttribute.IsRecycleYB);
                userInI.Write("VIP属性", "回收元宝倍率", user.User_VIPAttribute.RecycleYBQty.ToString());

                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 发送特殊消息图片
        /// </summary>
        public static string SendMessageImage(UserInfo user, string msg, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true, true, x, y, w, h);

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                            // 矩形参数                            
                            int width = 1000;
                            int height = 36;

                            // 创建半透明黄色画刷 (60%透明度 = 255*0.6 ≈ 153)
                            using (SolidBrush sb = new SolidBrush(Color.FromArgb(90, Color.Yellow)))
                            {
                                gh.FillRectangle(sb, 50, 100, width, height);
                            }

                            using (SolidBrush sb = new SolidBrush(Color.Fuchsia))
                            {
                                Rectangle rect = new Rectangle(100, 103, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, msg, font28, sb, rect);
                            }

                        }

                        path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                        bitmap.Save(path, ImageFormat.Png);
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
        /// 开启永恒之力
        /// </summary>
        public static string GetONYHZL(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                if (user.IsOnYHZL == "是") throw new Exception("开启失败,你已开启了永恒之力,你在开玩笑吗？");
                if (user.User_General.YB < 200) throw new Exception("开启失败,开启永恒之力需要200元宝,你连这点都没有吗？");

                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                user.User_YHZLAttribute.DouBPower = 15;
                user.User_YHZLAttribute.Dc = 10;
                user.User_YHZLAttribute.DcMax = 10;
                user.User_YHZLAttribute.Mc = 10;
                user.User_YHZLAttribute.McMax = 10;
                user.User_YHZLAttribute.Sc = 10;
                user.User_YHZLAttribute.ScMax = 10;
                user.User_YHZLAttribute.Hp = 200;
                user.IsOnYHZL = "是";
                userInI.Write("人物信息", "是否开启永恒之力", user.IsOnYHZL);

                userInI.Write("永恒之力属性", "攻击下限", user.User_YHZLAttribute.Dc + "");
                userInI.Write("永恒之力属性", "攻击上限", user.User_YHZLAttribute.DcMax + "");
                userInI.Write("永恒之力属性", "魔法下限", user.User_YHZLAttribute.Mc + "");
                userInI.Write("永恒之力属性", "魔法上限", user.User_YHZLAttribute.McMax + "");
                userInI.Write("永恒之力属性", "道术下限", user.User_YHZLAttribute.Sc + "");
                userInI.Write("永恒之力属性", "道术上限", user.User_YHZLAttribute.ScMax + "");
                userInI.Write("永恒之力属性", "倍功", user.User_YHZLAttribute.DouBPower + "");
                userInI.Write("永恒之力属性", "Hp", user.User_YHZLAttribute.Hp + "");

                return $"恭喜玩家[{user.Name}]成功开启了永恒之力！";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 开启公益捐献
        /// </summary>
        public static string GetONGYJX(UserInfo user)
        {
            try
            {
                user.User_General.YB -= 2000;
                if (user.User_General.YB < 0) throw new Exception("对不起,开启公益捐献需要2000元宝,如果没有请多签到和回收元宝吧.");
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                string titleName = userInI.ReadValue("个人称号", "称号2");
                if (!string.IsNullOrEmpty(titleName)) throw new Exception("对不起,您已开启了公益捐献称号,请勿重复开启.");

                user.IsOnTitle = "是";
                userInI.Write("人物信息", "是否开启称号", "是");
                userInI.Write("普通数据", "元宝", user.User_General.YB + "");

                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                int jxCount = int.Parse(config.Read("配置信息", "公益捐献"));
                jxCount += 1000;
                config.Write("配置信息", "公益捐献", jxCount + "");
                if (user.User_TitleAttribute == null) user.User_TitleAttribute = new List<UserTitleAttribute>();
                UserTitleAttribute uta = new UserTitleAttribute
                {
                    Level = 1,
                    LevelType = 2,
                    MonDeadCount = 1,
                    TitleName = "永恒鞭尸",
                    StdItemMap = user.Map,
                    StdItemOwner = user.Name,
                    StdItemStuff = "NPC制造",
                    StdItemTime = DateTime.Now.ToString()
                };
                user.User_TitleAttribute.Add(uta);
                foreach (var title in user.User_TitleAttribute)
                {
                    userInI.Write("个人称号", "称号2", Api.JsonSerialize<UserTitleAttribute>(title));
                }

                return $"恭喜玩家[{user.Name}]捐献了2000元宝获得了《永恒鞭尸》称号";
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 商铺购买
        /// </summary>
        public static void PurchaseGoods(UserInfo user, string goodsName, int num)
        {
            try
            {
                if (string.IsNullOrEmpty(goodsName)) throw new Exception("对不起,购买的商品名称不能为空!");
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                int goodsNum = 0, price = 0;
                string type = "";
                path = LegendFilePath.ShopPath + "商铺装饰.txt";
                var list = FileHelper.ReadTxtReturnList(path);
                list.ForEach(s =>
                {
                    string[] arrGoods = s.Split(',');
                    if (arrGoods[0] == goodsName)
                    {
                        goodsNum = int.Parse(arrGoods[5]);
                        price = int.Parse(arrGoods[3]);
                        type = arrGoods[4];
                        return;
                    }
                });
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺补给.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺强化.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺好友.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺限量.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺奇珍榜.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (num > goodsNum) throw new Exception($"对不起,物品[{goodsName}]最多购买数量为:{num}");
                switch (type)
                {
                    case "金币":
                        user.User_General.JB -= price * num;
                        if (user.User_General.JB < 0) throw new Exception($"对不起,金币不够!物品[{goodsName}]购买数量为{num},总共需要{price * num}金币");
                        break;
                    case "元宝":
                        user.User_General.YB -= price * num;
                        if (user.User_General.YB < 0) throw new Exception($"对不起,元宝不够!物品[{goodsName}]购买数量为{num},总共需要{price * num}元宝");
                        break;
                    default: break;
                }

                List<StditemDB> listStditemDB = LegendApi.GetStdItemsList();
                StditemDB std = listStditemDB.Where(s => s.StdName == goodsName).FirstOrDefault();
                if (std == null) throw new Exception("对不起,购买的商品不存在!");
                std.StdItemMap = user.Map;
                std.StdItemOwner = user.Name;
                std.StdItemStuff = "NPC制造";
                std.StdItemTime = DateTime.Now.ToString();

                if (std.OverLap > 0)
                {
                    std.SubAttr1 += num;
                }

                userInI.Write("普通数据", "金币", user.User_General.JB.ToString());
                userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());
                userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(std));
                UserInfoApi.RefreshOverlayStdItems(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 商铺赠送
        /// </summary>
        public static void GiftGoods(UserInfo user, string giftQQ, string goodsName, int num)
        {
            try
            {
                if (string.IsNullOrEmpty(goodsName)) throw new Exception("对不起,购买的商品名称不能为空!");
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                path = LegendFilePath.UserPath + giftQQ + ".ini";
                IniFileHelper userGiftInI = new IniFileHelper(path);
                if (userGiftInI == null) throw new Exception("哦豁,赠送对象未找到!");

                int goodsNum = 0, price = 0;
                string type = "";
                path = LegendFilePath.ShopPath + "商铺装饰.txt";
                var list = FileHelper.ReadTxtReturnList(path);
                list.ForEach(s =>
                {
                    string[] arrGoods = s.Split(',');
                    if (arrGoods[0] == goodsName)
                    {
                        goodsNum = int.Parse(arrGoods[5]);
                        price = int.Parse(arrGoods[3]);
                        type = arrGoods[4];
                        return;
                    }
                });
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺补给.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺强化.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺好友.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺限量.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (goodsNum == 0)
                {
                    path = LegendFilePath.ShopPath + "商铺奇珍榜.txt";
                    list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s =>
                    {
                        string[] arrGoods = s.Split(',');
                        if (arrGoods[0] == goodsName)
                        {
                            goodsNum = int.Parse(arrGoods[5]);
                            price = int.Parse(arrGoods[3]);
                            type = arrGoods[4];
                            return;
                        }
                    });
                }
                if (num > goodsNum) throw new Exception($"对不起,物品[{goodsName}]最多赠送数量为:{goodsNum}");
                switch (type)
                {
                    case "金币":
                        user.User_General.JB -= price * num;
                        if (user.User_General.JB < 0) throw new Exception($"对不起,金币不够!物品[{goodsName}]赠送数量为{num},总共需要{price * num}金币");
                        break;
                    case "元宝":
                        user.User_General.YB -= price * num;
                        if (user.User_General.YB < 0) throw new Exception($"对不起,元宝不够!物品[{goodsName}]赠送数量为{num},总共需要{price * num}元宝");
                        break;
                    default: break;
                }

                List<StditemDB> listStditemDB = LegendApi.GetStdItemsList();
                StditemDB std = listStditemDB.Where(s => s.StdName == goodsName).FirstOrDefault();
                if (std == null) throw new Exception("对不起,购买的商品不存在!");
                std.StdItemMap = user.Map;
                std.StdItemOwner = user.Name;
                std.StdItemStuff = "NPC制造";
                std.StdItemTime = DateTime.Now.ToString();

                if (std.OverLap > 0)
                {
                    std.SubAttr1 += num;
                }

                userInI.Write("普通数据", "金币", user.User_General.JB.ToString());
                userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());
                userGiftInI.Write("背包物品", userGiftInI.GetRndId(), Api.JsonSerialize(std));
                //UserInfoApi.RefreshOverlayStdItems(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 合成物品
        /// </summary>
        public static string SyntheticItems(UserInfo user, string itemsName)
        {
            try
            {
                string msg = "";
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                var list = userInI.GetAllKeys("背包物品");

                List<StditemDB> stdList = LegendApi.GetStdItemsList();
                string key1 = "", key2 = "", key3 = "";
                StditemDB stdItem = null, stdItem1 = null, stdItem2 = null, stdItem3 = null;
                List<StditemDB> originalItemList = new List<StditemDB>();
                Random rnd = new Random();
                int n = 0;
                long yb = 0;
                switch (itemsName)
                {
                    case "合成永恒鲜花":
                    case "合成永恒灵玉":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                                case "水晶": key2 = key; stdItem2 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem2 == null) throw new Exception("骚年,你背包没有水晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");

                        if (stdItem1.SubAttr1 < 1888) throw new Exception("骚年,你背包的永恒结晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        if (stdItem2.SubAttr1 < 1888) throw new Exception("骚年,你背包的水晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        yb = user.User_General.YB - 200000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够200000啊！去多回收一点装备再来和我说话吧！");

                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 1888;
                        stdItem2.SubAttr1 -= 1888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("背包物品", key2, Api.JsonSerialize(stdItem2));
                        userInI.Write("普通数据", "元宝", Api.JsonSerialize(stdItem2));

                        n = rnd.Next(1, 101);
                        if (n < 30) return "噢,骚年,运气不佳啊,赤月斗笠合成失败!";

                        if (itemsName == "合成永恒灵玉")
                        {
                            stdItem = stdList.Where(p => p.StdName == "永恒灵玉").First();
                            msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[永恒灵玉],其它人请再接再厉,加油吧!";
                        }
                        if (itemsName == "合成永恒鲜花")
                        {
                            stdItem = stdList.Where(p => p.StdName == "永恒鲜花").First();
                            msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[永恒鲜花],其它人请再接再厉,加油吧!";
                        }
                        //stdItem2.StdHiddAttr = new HiddAttr
                        //{
                        //    HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                        //    HidProbability = 50,
                        //    ScPR = 90,
                        //    Type = 2,
                        //};
                        
                        break;
                    case "合成永恒靴子":
                    case "合成永恒腰带":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                                case "水晶": key2 = key; stdItem2 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem2 == null) throw new Exception("骚年,你背包没有水晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");

                        if (stdItem1.SubAttr1 < 888) throw new Exception("骚年,你背包的永恒结晶少于888个啊！想坑蒙拐骗蒙混过关吗？");
                        if (stdItem2.SubAttr1 < 888) throw new Exception("骚年,你背包的水晶少于888个啊！想坑蒙拐骗蒙混过关吗？");
                        yb = user.User_General.YB - 100000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够100000啊！去多回收一点装备再来和我说话吧！");

                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 888;
                        stdItem2.SubAttr1 -= 888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("背包物品", key2, Api.JsonSerialize(stdItem2));
                        userInI.Write("普通数据", "元宝", Api.JsonSerialize(stdItem2));

                        n = rnd.Next(1, 101);
                        if (n < 30) return "噢,骚年,运气不佳啊,赤月斗笠合成失败!";
 
                        if (itemsName == "合成永恒靴子")
                        {
                            stdItem = stdList.Where(p => p.StdName == "永恒靴子").First();
                            msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[永恒靴子],其它人请再接再厉,加油吧!";
                        }
                        if (itemsName == "合成永恒腰带")
                        {
                            stdItem = stdList.Where(p => p.StdName == "永恒腰带").First();
                            msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[永恒腰带],其它人请再接再厉,加油吧!";
                        }
                        //stdItem2.StdHiddAttr = new HiddAttr
                        //{
                        //    HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                        //    HidProbability = 50,
                        //    ScPR = 90,
                        //    Type = 2,
                        //}; 
                        break;
                    case "合成秀儿军鼓":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                                case "水晶": key2 = key; stdItem2 = std; break;
                                case "沧天军鼓": key3 = key; stdItem3 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem2 == null) throw new Exception("骚年,你背包没有水晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem3 == null) throw new Exception("骚年,你背包没有沧天军鼓啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 2888) throw new Exception("骚年,你背包的永恒结晶少于2888个啊！想坑蒙拐骗蒙混过关吗？");
                        if (stdItem2.SubAttr1 < 2888) throw new Exception("骚年,你背包的水晶少于2888个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 2888;
                        stdItem2.SubAttr1 -= 2888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("背包物品", key2, Api.JsonSerialize(stdItem2));

                        n = rnd.Next(1, 101);
                        if (n < 30) return "噢,骚年,运气不佳啊,赤月斗笠合成失败!";

                        userInI.Delete("背包物品", key3);
                        stdItem = stdList.Where(p => p.StdName == "秀儿军鼓").First();
                        //stdItem2.StdHiddAttr = new HiddAttr
                        //{
                        //    HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                        //    HidProbability = 50,
                        //    ScPR = 90,
                        //    Type = 2,
                        //};
                        msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[秀儿军鼓],其它人请再接再厉,加油吧!";
                        break;
                    case "合成沧天军鼓":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                                case "水晶": key2 = key; stdItem2 = std; break;
                                case "毁灭军鼓": key3 = key; stdItem3 = std; break;
                            }

                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem2 == null) throw new Exception("骚年,你背包没有水晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem3 == null) throw new Exception("骚年,你背包没有毁灭军鼓啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 1888) throw new Exception("骚年,你背包的永恒结晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        if (stdItem2.SubAttr1 < 1888) throw new Exception("骚年,你背包的水晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 1888;
                        stdItem2.SubAttr1 -= 1888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("背包物品", key2, Api.JsonSerialize(stdItem2));
                        userInI.Delete("背包物品", key3);

                        stdItem = stdList.Where(p => p.StdName == "沧天军鼓").First();
                        //stdItem2.StdHiddAttr = new HiddAttr
                        //{
                        //    HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                        //    HidProbability = 50,
                        //    ScPR = 90,
                        //    Type = 2,
                        //};
                        msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[沧天军鼓],其它人请再接再厉,加油吧!";
                        break;
                    case "合成毁灭军鼓":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                                case "水晶": key2 = key; stdItem2 = std; break;
                                case "永恒军鼓": key3 += key + ","; originalItemList.Add(std); break;
                            }

                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem2 == null) throw new Exception("骚年,你背包没有水晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 688) throw new Exception("骚年,你背包的永恒结晶少于688个啊！想坑蒙拐骗蒙混过关吗？");
                        if (stdItem2.SubAttr1 < 688) throw new Exception("骚年,你背包的水晶少于688个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的永恒军鼓少于3个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 688;
                        stdItem2.SubAttr1 -= 688;


                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("背包物品", key2, Api.JsonSerialize(stdItem2));

                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key3.Split(',')[i]);
                        }

                        stdItem = stdList.Where(p => p.StdName == "毁灭军鼓").First();
                        //stdItem2.StdHiddAttr = new HiddAttr
                        //{
                        //    HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                        //    HidProbability = 50,
                        //    ScPR = 90,
                        //    Type = 2,
                        //};
                        msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[毁灭军鼓],其它人请再接再厉,加油吧!";
                        break;
                    case "合成永恒军鼓":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                                case "水晶": key2 = key; stdItem2 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem2 == null) throw new Exception("骚年,你背包没有水晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 688) throw new Exception("骚年,你背包的永恒结晶少于688个啊！想坑蒙拐骗蒙混过关吗？");
                        if (stdItem2.SubAttr1 < 688) throw new Exception("骚年,你背包的水晶少于688个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 688;
                        stdItem2.SubAttr1 -= 688;

                        //stdItem2.StdHiddAttr = new HiddAttr
                        //{
                        //    HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                        //    HidProbability = 50,
                        //    ScPR = 90,
                        //    Type = 2,
                        //};
                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("背包物品", key2, Api.JsonSerialize(stdItem2));

                        stdItem = stdList.Where(p => p.StdName == "永恒军鼓").First();
                        msg = $"恭喜玩家[{user.Name}]在\"大帝之城[四大陆]\"处合成[永恒军鼓],其它人请再接再厉,加油吧!";
                        break;
                    case "合成神器天尊":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "永恒结晶")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 1888) throw new Exception("骚年,你背包的永恒结晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 1888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        stdItem = stdList.Where(p => p.StdName == "天※尊").First();
                        stdItem.StdHiddAttr = new HiddAttr
                        {
                            HidTitle = "道士的召唤兽攻击50%几率触 发道术上限真实伤害！",
                            HidProbability = 50,
                            ScPR = 90,
                            Type = 2,
                        };
                        msg = $"恭喜玩家[{user.Name}]在\"三大神器合成\"处合成了神器[天※尊],其它人请再接再厉,加油吧!";
                        break;
                    case "合成神器法神":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "永恒结晶")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 1888) throw new Exception("骚年,你背包的永恒结晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 1888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        stdItem = stdList.Where(p => p.StdName == "法※神").First();
                        stdItem.StdHiddAttr = new HiddAttr
                        {
                            HidTitle = "法师的所有攻击触发额外伤 害10%！",
                            HidProbability = 100,
                            McPR = 10,
                            Type = 1,
                        };
                        msg = $"恭喜玩家[{user.Name}]在\"三大神器合成\"处合成了神器[法※神],其它人请再接再厉,加油吧!";
                        break;
                    case "合成神器圣战":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "永恒结晶")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 1888) throw new Exception("骚年,你背包的永恒结晶少于1888个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 1888;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        stdItem = stdList.Where(p => p.StdName == "圣※战").First();
                        stdItem.StdHiddAttr = new HiddAttr
                        {
                            HidTitle = "战士的攻击30%几率触发30%吸血以及攻击速度加10%！",
                            HidProbability = 30,
                            DcPR = 30,
                            Type = 0,
                        };
                        msg = $"恭喜玩家[{user.Name}]在\"三大神器合成\"处合成了神器[圣※战],其它人请再接再厉,加油吧!";
                        break;
                    case "合成普通斗笠":
                        // 取永恒结晶物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "永恒结晶")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 5) throw new Exception("骚年,你背包的永恒结晶少于5个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 5;

                        // 修改新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        stdItem = stdList.Where(p => p.StdName == "普通斗笠").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[普通斗笠],其它人请再接再厉,加油吧!";
                        break;
                    case "合成赤月斗笠":
                        // 取3个普通斗笠
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "普通斗笠": key2 += key + ","; originalItemList.Add(std); break;
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 50) throw new Exception("骚年,你背包的永恒结晶少于50个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的普通斗笠少于3个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 50;

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }

                        n = rnd.Next(0, 101);
                        if (n < 30) return "噢,骚年,运气不佳啊,赤月斗笠合成失败!";

                        stdItem = stdList.Where(p => p.StdName == "赤月斗笠").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[赤月斗笠],其它人请再接再厉,加油吧!";
                        break;
                    case "合成永恒斗笠":
                        // 取3个赤月斗笠
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "赤月斗笠": key2 += key + ","; originalItemList.Add(std); break;
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 88) throw new Exception("骚年,你背包的永恒结晶少于88个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的赤月斗笠少于3个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 88;

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }

                        n = rnd.Next(0, 101);
                        if (n < 20) return "噢,骚年,运气不佳啊,永恒斗笠合成失败!";

                        stdItem = stdList.Where(p => p.StdName == "永恒斗笠").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[永恒斗笠],其它人请再接再厉,加油吧!";
                        break;
                    case "合成毁灭斗笠":
                        // 取3个永恒斗笠
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "永恒斗笠": key2 += key + ","; originalItemList.Add(std); break;
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 188) throw new Exception("骚年,你背包的永恒结晶少于188个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的永恒斗笠少于3个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 188;

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }
                        n = rnd.Next(0, 101);
                        if (n < 20) return "噢,骚年,运气不佳啊,毁灭斗笠合成失败!";

                        stdItem = stdList.Where(p => p.StdName == "毁灭斗笠").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[毁灭斗笠],其它人请再接再厉,加油吧!";
                        break;
                    case "合成沧天斗笠":
                        // 取1个毁灭斗笠
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "毁灭斗笠": key2 += key + ","; originalItemList.Add(std); break;
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 288) throw new Exception("骚年,你背包的永恒结晶少于288个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 1) throw new Exception("骚年,你背包的毁灭斗笠少于1个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 288;

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        for (int i = 0; i < 1; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }
                        n = rnd.Next(0, 101);
                        if (n < 20) return "噢,骚年,运气不佳啊,沧天斗笠合成失败!";

                        stdItem = stdList.Where(p => p.StdName == "沧天斗笠").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[沧天斗笠],其它人请再接再厉,加油吧!";
                        break;
                    case "合成灭世斗笠":
                        // 取1个沧天斗笠
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "沧天斗笠": key2 += key + ","; originalItemList.Add(std); break;
                                case "永恒结晶": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有永恒结晶啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 388) throw new Exception("骚年,你背包的永恒结晶少于288个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 1) throw new Exception("骚年,你背包的沧天斗笠少于1个啊！想坑蒙拐骗蒙混过关吗？");
                        // 扣减物品数量，给新物品
                        stdItem.SubAttr1 -= 388;

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));

                        n = rnd.Next(0, 101);
                        if (n < 50) return "噢,骚年,运气不佳啊,灭世斗笠合成失败!";

                        stdItem = stdList.Where(p => p.StdName == "灭世斗笠").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[灭世斗笠],其它人请再接再厉,加油吧!";
                        break;
                    case "合成宝石":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "宝石碎片")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有宝石碎片啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 10) throw new Exception("骚年,你背包的宝石碎片少于10个啊！想坑蒙拐骗蒙混过关吗？");
                        yb = user.User_General.YB - 200;
                        if (yb < 0) throw new Exception("骚年,你元宝不够200啊！去多回收一点装备再来和我说话吧！");
                        // 扣减物品数量，给新物品
                        stdItem1.SubAttr1 -= 10;
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        userInI.Write("普通数据", "元宝", yb + "");
                        stdItem2 = stdList.Where(p => p.StdName == "伤害宝石").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[伤害宝石],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石1":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "伤害宝石": key2 += key + ","; originalItemList.Add(std); break;
                                case "宝石碎片": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有宝石碎片啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 100) throw new Exception("骚年,你背包的宝石碎片少于100个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的伤害宝石少于3个啊！想坑蒙拐骗蒙混过关吗？");

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+1").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[伤害宝石+1],其它人请再接再厉,加油吧!"; break;
                    case "升级宝石2":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "伤害宝石+1": key2 += key + ","; originalItemList.Add(std); break;
                                case "宝石碎片": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有宝石碎片啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 100) throw new Exception("骚年,你背包的宝石碎片少于100个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的伤害宝石+1少于3个啊！想坑蒙拐骗蒙混过关吗？");

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }
                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+2").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[伤害宝石+2],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石3": // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            switch (std.StdName)
                            {
                                case "伤害宝石+2": key2 += key + ","; originalItemList.Add(std); break;
                                case "宝石碎片": key1 = key; stdItem1 = std; break;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有宝石碎片啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        if (stdItem1.SubAttr1 < 1000) throw new Exception("骚年,你背包的宝石碎片少于100个啊！想坑蒙拐骗蒙混过关吗？");
                        if (originalItemList.Count < 3) throw new Exception("骚年,你背包的伤害宝石+2少于3个啊！想坑蒙拐骗蒙混过关吗？");

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Write("背包物品", key1, Api.JsonSerialize(stdItem1));
                        for (int i = 0; i < 3; i++)
                        {
                            userInI.Delete("背包物品", key2.Split(',')[i]);
                        }

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+3").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处合成了[伤害宝石+3],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石4":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+3")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+3啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+4").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+4],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石5":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+4")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+4啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+5").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+5],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石6":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+5")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+5啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");

                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+6").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+6],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石7":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+6")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 50000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够50000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+6啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                       
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+7").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+7],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石8":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+7")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 50000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够50000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+7啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                        
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+8").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+8],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石9":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+8")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 50000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够50000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+8啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                       
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+9").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+9],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石10":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+9")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 300000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够300000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+9啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                        
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+10").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+10],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石11":
                        // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+10")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 300000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够300000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+10啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                        
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+11").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+11],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石12": // 取物品
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+11")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 300000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够300000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+11啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品

                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+12").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+12],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石13":
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+12")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 300000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够300000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+12啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                        
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+13").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+13],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石14":
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+13")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 300000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够300000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+13啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品                        
                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+14").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+14],其它人请再接再厉,加油吧!";
                        break;
                    case "升级宝石15":
                        foreach (string key in list)
                        {
                            var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                            if (std != null && std.StdName == "伤害宝石+14")
                            {
                                key1 = key;
                                stdItem1 = std;
                            }
                        }
                        // 判断物品是否存在，数量是否足够
                        yb = user.User_General.YB - 300000;
                        if (yb < 0) throw new Exception("骚年,你元宝不够300000啊！去多回收一点装备再来和我说话吧！");
                        if (stdItem1 == null) throw new Exception("骚年,你背包没有伤害宝石+14啊！想坑蒙拐骗蒙混过关吗？哪里来滚哪里去吧！");
                        // 扣减物品数量，给新物品

                        userInI.Write("普通数据", "元宝", yb.ToString());
                        // 删除背包里原来物品，添加新扣减的物品
                        userInI.Delete("背包物品", key1);

                        stdItem = stdList.Where(p => p.StdName == "伤害宝石+15").First();
                        msg = $"恭喜玩家[{user.Name}]在\"世外高人\"处升级了[伤害宝石+15],其它人请再接再厉,加油吧!";
                        break;

                    default: throw new Exception("骚年,你是不是打错口令了,发送 菜单 看看你要干啥！");
                }
                if (stdItem != null)
                {
                    stdItem.StdItemMap = user.Map;
                    stdItem.StdItemOwner = user.Name;
                    stdItem.StdItemStuff = "NPC制造";
                    stdItem.StdItemTime = DateTime.Now.ToString();
                    userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(stdItem));
                }
                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
                throw;
            }
        }


    }
}

