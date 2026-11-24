using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using 传奇.core;

namespace 传奇
{
    public class Market_DefApi
    {
        #region  成员变量
        static Random rnd = new Random();
        static string UserInfoImgPath = LegendFilePath.imgPath;//图片数据
        static string LegendIMGDataPath = LegendFilePath.LegendIMGDataPath;//图片样式
        static string prgusePath = LegendFilePath.LegendIMGDataPath + "Prguse\\";
        #endregion

        /// <summary>
        /// 万事通
        /// </summary>
        public static string WanShiTong(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string th = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(th);
                switch (user.Map)
                {
                    case "比奇城":
                        user.MapCode = "1_100";
                        user.UserImagePath = $"地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "盟重土城":
                        user.MapCode = "4";
                        user.UserImagePath = $"地图\\4\\盟重土城\\4_{rnd.Next(1, 3)}.jpg";
                        break;
                }
                userInI.Write("人物信息", "所在商店", user.Map + "_万事通");
                userInI.Write("人物信息", "所在页面", "1");
                userInI.Write("人物信息", "个人图片", user.UserImagePath);

                user.UserImagePath = LegendIMGDataPath + user.UserImagePath;
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font12 = new Font("黑体", 12);
                            Font fontBold13 = new Font("黑体", 13, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(10, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "请问我可以为你做什么吗？\n只要你付钱,什么问题不能解决呢？\n来吧,小伙子,是想买什么还是想卖什么？", font12, brush, rect);//文字

                            rect = new Rectangle(56, 61, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "物品", font12, brush, rect);//文字

                            rect = new Rectangle(56, 81, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "物品", font12, brush, rect);//文字

                            rect = new Rectangle(120, 140, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "命令:金币换金条#3 #后面是兑换数量,不带#兑换1根", font12, brush, rect);//文字

                            rect = new Rectangle(120, 160, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "命令:金币换金条#3 #后面是兑换数量,不带#兑换1根", font12, brush, rect);//文字

                            brush = new SolidBrush(Color.Yellow);
                            rect = new Rectangle(40, 60, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "买", fontBold13, brush, rect);//文字

                            rect = new Rectangle(40, 80, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "卖", fontBold13, brush, rect);//文字

                            rect = new Rectangle(40, 100, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "保管", fontBold13, brush, rect);//文字

                            rect = new Rectangle(40, 120, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "找回", fontBold13, brush, rect);//文字

                            rect = new Rectangle(40, 140, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "金币换金条", fontBold13, brush, rect);//文字

                            rect = new Rectangle(40, 160, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "金条换金币", fontBold13, brush, rect);//文字


                           
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
        /// 对话老兵
        /// </summary>
        public static string LaoBinG(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                switch (user.Map)
                {
                    case "比奇城":
                        user.MapCode = "1_100";
                        user.UserImagePath = $"{LegendIMGDataPath}地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "盟重土城":
                        user.MapCode = "4";
                        user.UserImagePath = $"{LegendIMGDataPath}地图\\4\\盟重土城\\4_{rnd.Next(1, 16)}.jpg";
                        break;
                }
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font12 = new Font("黑体", 12);
                            Font fontBold12 = new Font("黑体", 12, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(10, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "不要相信什么凌波微步,其实这个东西很简单,\n只要你付钱,什么问题不能解决呢？\n来吧,小伙子这个世界充满危险,让我送你一程吧!", font12, brush, rect);//文字

                            rect = new Rectangle(30, 60, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "关于", font12, brush, rect);//文字

                            brush = new SolidBrush(Color.Yellow);

                            rect = new Rectangle(55, 59, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "PK", fontBold12, brush, rect);//文字

                            rect = new Rectangle(55, 80, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "向商店移动", fontBold12, brush, rect);//文字

                          
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
        /// 老兵前往商店
        /// </summary>
        public static string LaoBinGoToStore(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                switch (user.Map)
                {
                    case "比奇城":
                        user.MapCode = "1_100";
                        user.UserImagePath = $"{LegendIMGDataPath}地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "盟重土城":
                        user.MapCode = "4";
                        user.UserImagePath = $"{LegendIMGDataPath}地图\\4\\盟重土城\\4_{rnd.Next(1, 16)}.jpg";
                        break;
                }
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font12 = new Font("黑体", 12);
                            Font fontBold12 = new Font("黑体", 12, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "你要去哪个商店？这个是免费服务!", font12, brush, rect);//文字

                            brush = new SolidBrush(Color.Yellow);

                            rect = new Rectangle(20, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "书店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(100, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "药品店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(180, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "炼药店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(20, 60, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "首饰店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(100, 60, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "服装店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(180, 60, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "武器店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(20, 90, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "杂货店", fontBold12, brush, rect);//文字

                            rect = new Rectangle(100, 90, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "仓库", fontBold12, brush, rect);//文字
                           
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
        /// 老兵PK
        /// </summary>
        public static string LaoBinGPK(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font12 = new Font("黑体", 12);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(10, 12, 0, 0);
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("如果遇到PK的人,可要自己保重哦!");
                            sb.AppendLine("虽然说PK的人会受到流放的处罚,可这是在他们死亡以后进行的.");
                            sb.AppendLine("奉劝大家不要PK,流放到边疆以后的日子可不好过,相信每一个在流");
                            sb.AppendLine("放村里孤苦伶仃,没有装备,没有补给的度过日子的人都会同意我这");
                            sb.AppendLine("句话.差点忘了告诉大家,沙巴克离流放地最近,是一个流放犯出没");
                            sb.AppendLine("的区域,千万小心财不露白哦!");
                            TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font12, brush, rect);//文字
                           
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
        /// 对话传送员
        /// </summary>
        public static string ChuanSongYuan(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                switch (user.Map)
                {
                    case "比奇城":
                        user.UserImagePath = $"{LegendIMGDataPath}地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "盟重土城":
                        user.UserImagePath = $"{LegendIMGDataPath}地图\\4\\盟重土城\\4_{rnd.Next(10, 13)}.jpg";
                        break;
                }
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font12 = new Font("黑体", 12);
                            Font fontBold13 = new Font("黑体", 13, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(10, 12, 0, 0);
                            StringBuilder sb = new StringBuilder();
                            if (user.User_General.Lv < 8)
                            {
                                sb.AppendLine("照你现在这个级别,我没什么能帮的上你!");
                                sb.AppendLine("请你练到8级再来找我吧,祝你好运!");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font12, brush, rect);//文字
                            }
                            else if (user.User_General.Lv >= 8)
                            {
                                sb.AppendLine("噢,亲爱的勇士!");
                                sb.AppendLine("欢迎光临!你可以在我这里享受以下服务.");
                                sb.AppendLine("请选择你要前往的地区：");
                                sb.AppendLine("┏━━━━┳━━━━┳━━━━┓");
                                sb.AppendLine("┃        ┃        ┃        ┃");
                                sb.AppendLine("┗━━━━┻━━━━┻━━━━┛");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font12, brush, rect);//文字

                                brush = new SolidBrush(Color.Yellow);
                                rect = new Rectangle(19, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "城市传送", fontBold13, brush, rect);//文字

                                rect = new Rectangle(81, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "险恶区域", fontBold13, brush, rect);//文字

                                rect = new Rectangle(144, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "危险区域", fontBold13, brush, rect);//文字
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
        /// 对话传送员 传送城市
        /// </summary>
        public static string ChuanSongYuanGotoCity(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font12 = new Font("黑体", 12);
                            Font fontBold12 = new Font("黑体", 12);
                            Font fontBold13 = new Font("黑体", 13, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(10, 12, 0, 0);
                            StringBuilder sb = new StringBuilder();
                            if (user.User_General.Lv < 8)
                            {
                                sb.AppendLine("照你现在这个级别,我没什么能帮的上你!");
                                sb.AppendLine("请你练到8级再来找我吧,祝你好运!");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font12, brush, rect);//文字
                            }
                            else
                            {
                                sb.AppendLine("噢,亲爱的勇士!");
                                sb.AppendLine("这里是<        >服务,你必须给我3000金币的报酬!");
                                sb.AppendLine("请选择你要前往的城市：");
                                sb.AppendLine("┏━━━━┳━━━━┳━━━━┳━━━━┓");
                                sb.AppendLine("┃        ┃        ┃        ┃        ┃");
                                sb.AppendLine("┣━━━━╋━━━━╋━━━━╋━━━━┫");
                                sb.AppendLine("┃        ┃        ┃        ┃        ┃");
                                sb.AppendLine("┗━━━━┻━━━━┻━━━━┻━━━━┛");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font12, brush, rect);//文字

                                brush = new SolidBrush(Color.Yellow);
                                rect = new Rectangle(54, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "城市传送", fontBold12, brush, rect);//文字

                                rect = new Rectangle(20, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "银杏小村", fontBold13, brush, rect);//文字

                                rect = new Rectangle(81, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "边界小城", fontBold13, brush, rect);//文字

                                rect = new Rectangle(142, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "毒蛇山谷", fontBold13, brush, rect);//文字

                                rect = new Rectangle(210, 65, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "比齐城", fontBold13, brush, rect);//文字

                                rect = new Rectangle(25, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "白日门", fontBold13, brush, rect);//文字

                                rect = new Rectangle(81, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "盟重土城", fontBold13, brush, rect);//文字

                                rect = new Rectangle(150, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "封魔谷", fontBold13, brush, rect);//文字

                                rect = new Rectangle(210, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "苍月岛", fontBold13, brush, rect);//文字
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
        /// 对话传送员 传送险恶区域
        /// </summary>
        public static string ChuanSongYuanGotXianE(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font9 = new Font("黑体", 12);
                            Font fontBold9 = new Font("黑体", 12);
                            Font fontBold10 = new Font("黑体", 13, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(12, 12, 0, 0);
                            StringBuilder sb = new StringBuilder();
                            if (user.User_General.Lv < 8)
                            {
                                sb.AppendLine("照你现在这个级别,我没什么能帮的上你!");
                                sb.AppendLine("请你练到8级再来找我吧,祝你好运!");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字
                            }
                            else if (user.User_General.Lv >= 8 && user.User_General.Lv < 15)
                            {
                                sb.AppendLine("噢,亲爱的勇士!");
                                sb.AppendLine("这里是<        >服务,按照你的级别  级前你只能前往以下地区：");
                                sb.AppendLine("当然你还得付给我5000金币的报酬!");
                                sb.AppendLine("┏━━━━━━━┳━━━━━━━┓");
                                sb.AppendLine("┃              ┃              ┃");
                                sb.AppendLine("┗━━━━━━━┻━━━━━━━┛");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字

                                brush = new SolidBrush(Color.Yellow);
                                rect = new Rectangle(55, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "险恶地区", fontBold9, brush, rect);//文字

                                rect = new Rectangle(210, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "15", fontBold9, brush, rect);//文字

                                rect = new Rectangle(22, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "兽人古墓(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(119, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "天然洞穴(2层)", fontBold10, brush, rect);//文字
                            }
                            else
                            {
                                sb.AppendLine("噢,亲爱的勇士!");
                                sb.AppendLine("这里是<        >服务,按照你的级别  级后你能前往以下地区：");
                                sb.AppendLine("当然你还得付给我5000金币的报酬!");
                                sb.AppendLine("┏━━━━━━━┳━━━━━━━┳━━━━━━━┳━━━━━━━┓");
                                sb.AppendLine("┃              ┃              ┃              ┃              ┃");
                                sb.AppendLine("┣━━━━━━━╋━━━━━━━╋━━━━━━━╋━━━━━━━┫");
                                sb.AppendLine("┃              ┃              ┃              ┃              ┃");
                                sb.AppendLine("┗━━━━━━━┻━━━━━━━┻━━━━━━━┻━━━━━━━┛");
                                ;

                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字

                                brush = new SolidBrush(Color.Yellow);
                                rect = new Rectangle(56, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "险恶地区", fontBold9, brush, rect);//文字

                                rect = new Rectangle(215, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "15", fontBold9, brush, rect);//文字

                                rect = new Rectangle(23, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "兽人古墓(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(121, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "天然洞穴(2层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(220, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "比奇矿区(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(320, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "毒蛇矿区(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(23, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "沃玛寺庙(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(121, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "蜈蚣洞口(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(220, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "石墓洞口(3层)", fontBold10, brush, rect);//文字

                                rect = new Rectangle(320, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "祖玛寺庙(4层)", fontBold10, brush, rect);//文字
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
        /// 对话传送员 传送危险区域
        /// </summary>
        public static string ChuanSongYuanGotWeiX(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            #region 对话框
                            using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                            {
                                gh.DrawImage(img, 0, 0, img.Width, img.Height);
                            }
                            #endregion
                            Font font9 = new Font("黑体", 12);
                            Font fontBold9 = new Font("黑体", 12);
                            Font fontBold10 = new Font("黑体", 13, FontStyle.Underline);
                            Color color = ColorTranslator.FromHtml("#ffffff");
                            Brush brush = new SolidBrush(color);
                            Rectangle rect = new Rectangle(12, 12, 0, 0);
                            StringBuilder sb = new StringBuilder();
                            if (user.User_General.Lv < 8)
                            {
                                sb.AppendLine("照你现在这个级别,我没什么能帮的上你!");
                                sb.AppendLine("请你练到8级再来找我吧,祝你好运!");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字
                            }
                            else
                            {
                                sb.AppendLine("噢,亲爱的勇士!");
                                sb.AppendLine("这里是<        >服务,按照你的级别  级后你能前往以下地区：");
                                sb.AppendLine("当然你还得付给我10000金币的报酬!");
                                sb.AppendLine("┏━━━━━━━┳━━━━━━━┳━━━━━━━┳━━━━━━━┓");
                                sb.AppendLine("┃              ┃              ┃              ┃              ┃");
                                sb.AppendLine("┣━━━━━━━╋━━━━━━━╋━━━━━━━╋━━━━━━━┫");
                                sb.AppendLine("┃              ┃              ┃              ┃              ┃");
                                sb.AppendLine("┣━━━━━━━╋━━━━━━━╋━━━━━━━╋━━━━━━━┫");
                                sb.AppendLine("┃              ┃              ┃              ┃              ┃");
                                sb.AppendLine("┗━━━━━━━┻━━━━━━━┻━━━━━━━┻━━━━━━━┛");

                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字

                                brush = new SolidBrush(Color.Yellow);
                                rect = new Rectangle(56, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "危险区域", fontBold9, brush, rect);//文字

                                rect = new Rectangle(215, 26, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "15", fontBold9, brush, rect);//文字

                                rect = new Rectangle(50, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "尸王殿", fontBold10, brush, rect);//文字

                                rect = new Rectangle(138, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "沃玛寺庙3", fontBold10, brush, rect);//文字

                                rect = new Rectangle(240, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "封魔殿", fontBold10, brush, rect);//文字

                                rect = new Rectangle(340, 66, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "蜈蚣洞3", fontBold10, brush, rect);//文字

                                rect = new Rectangle(46, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "石墓洞3", fontBold10, brush, rect);//文字

                                rect = new Rectangle(126, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "祖玛教主大厅", fontBold10, brush, rect);//文字

                                rect = new Rectangle(240, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "骨魔洞3", fontBold10, brush, rect);//文字

                                rect = new Rectangle(340, 93, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "牛魔洞3", fontBold10, brush, rect);//文字

                                rect = new Rectangle(43, 120, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "赤月巢穴", fontBold10, brush, rect);//文字

                                rect = new Rectangle(140, 120, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "恶魔祭坛", fontBold10, brush, rect);//文字

                                rect = new Rectangle(245, 120, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "幻境7", fontBold10, brush, rect);//文字

                                rect = new Rectangle(345, 120, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "屠龙殿", fontBold10, brush, rect);//文字
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

    }
}
