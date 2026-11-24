using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Formats;
using 传奇.core;

namespace 传奇
{
    public class MapApi
    {
        #region  成员变量
        static Random rnd = new Random();
        static string UserInfoImgPath = LegendFilePath.imgPath;//图片数据
        static string LegendIMGDataPath = LegendFilePath.LegendIMGDataPath;//图片样式

        static string prgusePath = LegendFilePath.LegendIMGDataPath + "Prguse\\";
        static string magicPath = LegendFilePath.LegendIMGDataPath + "Magic\\";
        static string market_defPath = LegendFilePath.LegendIMGDataPath + "商人\\";
        static string newopUIPath = LegendFilePath.LegendIMGDataPath + "NewopUI\\";
        static string itemsPath = LegendFilePath.LegendIMGDataPath + "Items\\";// 装备在背包的样子
        #endregion

        /// <summary>
        /// 前往城市
        /// </summary>
        public static string GotoCityMap(UserInfo user, string mapName, long userJb, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                userJb = user.User_General.JB - userJb;
                string path = "";
                if (userJb < 0)
                {
                    using (Image imgMian = Image.FromFile(userImgPath))
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
                                Font font9 = new Font("黑体", 9);
                                Font font13 = new Font("黑体", 13);

                                Color color = ColorTranslator.FromHtml("#ffffff");
                                Brush brush = new SolidBrush(color);
                                Rectangle rect = new Rectangle(10, 12, 0, 0);
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine("你身上的钱不够!请准备好后再来.");
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字 

                                path = $"{UserInfoImgPath + user.UserQQ}.png";
                                bitmap.Save(path, ImageFormat.Png);//保存图片
                            }
                        }
                    }
                }
                else
                {
                    user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                    user.Map = mapName;
                    switch (mapName)
                    {
                        case "比奇城":
                            user.MapCode = "1_0_3";
                            user.UserImagePath = $"地图\\0\\比奇城\\1_100_{rnd.Next(1, 4)}.jpg";
                            break;
                        case "银杏山谷":
                        case "银杏小村":
                            user.MapCode = "1_0_1";
                            user.UserImagePath = $"地图\\0\\银杏山谷\\1_0_1_{rnd.Next(1, 5)}.jpg";
                            break;
                        case "边界村":
                        case "边界小城":
                            user.MapCode = "1_0_2";
                            user.UserImagePath = $"地图\\0\\边界村\\1_0_2_{rnd.Next(1, 4)}.jpg";
                            break;
                        case "毒蛇山谷":
                            user.MapCode = "3_1";
                            user.UserImagePath = $"地图\\3\\毒蛇山谷\\3_1_{rnd.Next(1, 5)}.jpg";
                            break;
                        case "盟重":
                        case "盟重土城":
                            user.MapCode = "4";
                            user.UserImagePath = $"地图\\4\\盟重土城\\4_{rnd.Next(1, 16)}.jpg";
                            break;

                        case "天宫仙境":
                        case "二大陆":
                            if (user.User_General.Lv < 45) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.MapCode = "10";
                            user.Map = "天宫仙境";
                            user.UserImagePath = $"\\地图\\5\\二大陆\\10_1.jpg";
                            break;

                        case "沙漠荒原":
                        case "三大陆":
                            if (user.User_General.Lv < 52) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.User_General.YB < 1000) throw new Exception("噢,大兄弟,元宝不够哦,你再去回收一点装备再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你还没有获取永恒之力呢,发送 永恒之力 查看一下吧!");
                            user.MapCode = "11";
                            user.Map = "沙漠荒原";
                            user.UserImagePath = $"\\地图\\6\\三大陆\\11_1.jpg";
                            break;

                        case "大帝之城":
                        case "四大陆":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.User_General.YB < 5000) throw new Exception("噢,大兄弟,元宝不够哦,你再去回收一点装备再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你还没有获取永恒之力呢,发送 永恒之力 查看一下吧!");
                            user.MapCode = "12";
                            user.Map = "大帝之城";
                            user.UserImagePath = $"\\地图\\7\\四大陆\\12_1.jpg";
                            break;
                        default: throw new Exception("噢,大兄弟,这个地图不存在噢!请输入 传送员 重新查看吧!");
                    }

                    string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                    IniFileHelper userInI = new IniFileHelper(newPath);
                    userInI.Write("人物信息", "所在地图", user.Map);
                    userInI.Write("人物信息", "所在地图编号", user.MapCode);
                    userInI.Write("人物信息", "坐标", user.Pos);
                    userInI.Write("人物信息", "个人图片", user.UserImagePath);
                    userInI.Write("人物信息", "是否在城里", "是");
                    userInI.Write("普通数据", "金币", userJb.ToString());

                    if (user.IsSendMsgImg == "否")
                    {
                        return "";
                    }

                    user.UserImagePath = LegendIMGDataPath + user.UserImagePath;
                    int delay = 10;
                    Font font13 = new Font("黑体", 13);
                    using (MagickImageCollection collection = new MagickImageCollection())
                    {
                        MemoryStream ms = new MemoryStream();
                        MagickImage magickImage = null;
                        MagickImage blendImage = null;
                        int magicX = 0, magicY = 0;
                        string posPath = string.Empty;
                        List<string> posList = new List<string>();
                        using (Image imgMian = Image.FromFile(userImgPath))
                        {
                            using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                            {
                                using (Graphics gh = Graphics.FromImage(bitmap))
                                {
                                    gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    magickImage = new MagickImage(ms);
                                    magickImage.AnimationDelay = delay;

                                    collection.Add(magickImage);

                                    for (int i = 250; i < 260; i++)
                                    {
                                        newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                        posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                        posList = FileHelper.ReadTxtReturnList(posPath);
                                        magicX = 512 + int.Parse(posList[0]);
                                        magicY = 314 + int.Parse(posList[1]);
                                        using (Image img = Image.FromFile(newPath))
                                        {
                                            gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                        }

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        blendImage = new MagickImage(ms);
                                        blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                        blendImage.AnimationDelay = delay;
                                        collection.Add(blendImage);
                                    }
                                }
                            }
                        }

                        userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                        using (Image imgMian = Image.FromFile(userImgPath))
                        {
                            using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                            {
                                using (Graphics gh = Graphics.FromImage(bitmap))
                                {
                                    gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    magickImage = new MagickImage(ms);
                                    magickImage.AnimationDelay = delay;
                                    collection.Add(magickImage);

                                    for (int i = 260; i < 270; i++)
                                    {
                                        newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                        posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                        posList = FileHelper.ReadTxtReturnList(posPath);
                                        magicX = 512 + int.Parse(posList[0]);
                                        magicY = 314 + int.Parse(posList[1]);
                                        using (Image img = Image.FromFile(newPath))
                                        {
                                            gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                        }

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        blendImage = new MagickImage(ms);
                                        blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                        blendImage.AnimationDelay = delay;
                                        collection.Add(blendImage);
                                    }

                                    gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    magickImage = new MagickImage(ms);
                                    magickImage.AnimationDelay = 300;
                                    collection.Add(magickImage);
                                }
                            }
                        }

                        path = UserInfoImgPath + user.UserQQ + ".gif";
                        // 3. 优化GIF动画
                        collection.OptimizeTransparency();
                        collection.Write(path, MagickFormat.Gif);
                        magickImage.Dispose();
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
        /// 前往普通险恶地图
        /// </summary>
        public static string GotoNormalXianEMap(UserInfo user, string mapName, long userJb, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);
                Font font13 = new Font("黑体", 13);
                SolidBrush brushLimeGreen = new SolidBrush(Color.Lime);

                userJb = user.User_General.JB - userJb;

                string path = "";
                if (userJb < 0)
                {
                    path = NoMoney(user.UserQQ, userImgPath, 0);
                }
                else
                {
                    user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                    user.Map = mapName.ToLower() + "1";
                    switch (mapName)
                    {
                        case "兽人古墓":
                            user.MapCode = "1_1_1";
                            user.UserImagePath = $"地图\\0\\兽人古墓1\\1_1_1_{rnd.Next(1, 11)}.jpg";
                            break;
                        case "天然洞穴":
                            user.MapCode = "1_2_1";
                            user.UserImagePath = $"地图\\0\\天然洞穴1\\1_2_1_{rnd.Next(1, 11)}.jpg";
                            break;
                        case "比奇矿区":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.MapCode = "1_3_1";
                            user.UserImagePath = $"地图\\0\\比奇矿区1\\1_3_1_{rnd.Next(1, 4)}.jpg";
                            break;
                        case "毒蛇矿区":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.MapCode = "3_5_1";
                            user.UserImagePath = $"地图\\3\\毒蛇矿区1\\3_5_1_{rnd.Next(1, 4)}.jpg";
                            break;
                        case "沃玛寺庙":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "沃玛寺庙入口";
                            user.MapCode = "2_1";
                            user.UserImagePath = $"地图\\2\\沃玛寺庙入口\\2_1_{rnd.Next(1, 4)}.jpg";
                            break;
                        case "蜈蚣洞":
                        case "蜈蚣洞口":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "蜈蚣洞1";
                            user.MapCode = "4_6_1";
                            user.UserImagePath = $"地图\\4\\蜈蚣洞1\\4_6_1_{rnd.Next(1, 3)}.jpg";
                            break;
                        case "猪洞":
                        case "石墓洞":
                        case "石墓洞口":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "石墓洞1";
                            user.MapCode = "4_5_1";
                            user.UserImagePath = $"地图\\4\\石墓洞1\\4_5_1_{rnd.Next(1, 3)}.jpg";
                            break;
                        case "祖玛":
                        case "祖玛寺庙":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "祖玛寺庙1";
                            user.MapCode = "4_4_1";
                            user.UserImagePath = $"地图\\4\\祖玛寺庙1\\4_4_1_1.jpg";
                            break;

                        //直达区域
                        case "沃玛寺庙3":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "沃玛寺庙3";
                            user.MapCode = "2_1_3";
                            user.UserImagePath = $"地图\\2\\沃玛寺庙3\\2_1_3_{rnd.Next(1, 11)}.jpg";
                            userJb = user.User_General.JB - 10000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "蜈蚣洞3":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "蜈蚣洞3";
                            user.MapCode = "4_6_3";
                            user.UserImagePath = $"地图\\4\\蜈蚣洞3\\4_6_3_{rnd.Next(1, 13)}.jpg";
                            userJb = user.User_General.JB - 10000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "石墓洞3":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "石墓洞3";
                            user.MapCode = "4_5_3";
                            user.UserImagePath = $"地图\\4\\石墓洞3\\4_5_3_{rnd.Next(1, 22)}.jpg";
                            userJb = user.User_General.JB - 10000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "祖玛教主大厅":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "祖玛教主大厅";
                            user.MapCode = "4_4_4";
                            user.UserImagePath = $"地图\\4\\祖玛教主大厅\\4_4_4_{rnd.Next(1, 21)}.jpg";
                            userJb = user.User_General.JB - 10000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "尸王殿":
                            if (user.User_General.Lv < 15) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.Map = "尸王殿";
                            user.MapCode = "1_3_8";
                            user.UserImagePath = $"地图\\0\\比奇尸王殿\\1_3_8_{rnd.Next(1, 11)}.jpg";
                            userJb = user.User_General.JB - 10000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;

                        case "玛雅大厅":
                            if (user.User_General.Lv < 42) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            //if (user.User_General.Lv < 12) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.User_General.JB < 1000000) throw new Exception("噢,大兄弟,金币不够呐,你先去凑齐100w再来找我吧!");
                            user.MapCode = "4_12_1";
                            user.UserImagePath = $"地图\\4\\玛雅大厅1\\4_12_1_{rnd.Next(1, 4)}.jpg";
                            userJb = user.User_General.JB - 1000000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "散人打宝":
                            if (user.User_General.Lv < 30) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.MapCode = "4_10_1";
                            user.UserImagePath = $"地图\\4\\散人打宝1\\4_10_1_{rnd.Next(1, 4)}.jpg";
                            userJb = user.User_General.JB - 10000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "神秘地图":
                            if (user.User_General.Lv < 44) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            //if (user.User_General.Lv < 14) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.User_General.JB < 2000000) throw new Exception("噢,大兄弟,金币不够呐,你先去凑齐200w再来找我吧!");
                            user.MapCode = "4_11_1";
                            user.UserImagePath = $"地图\\4\\神秘地图1\\4_11_1_{rnd.Next(1, 4)}.jpg";
                            userJb = user.User_General.JB - 2000000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;
                        case "boss之家":
                            if (user.User_General.JB < 200000) throw new Exception("噢,大兄弟,金币不够呐,你先去凑齐20w再来找我吧!");
                            user.MapCode = "4_13";
                            user.Map = "BOSS之家";
                            user.UserImagePath = $"地图\\4\\BOOS之家\\4_13_{rnd.Next(1, 31)}.jpg";
                            userJb = user.User_General.JB - 200000;
                            if (userJb < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 0);
                                return path;
                            }
                            break;

                        #region 二大陆
                        case "虚空无尽":
                            if (user.User_General.Lv < 50) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你没有开启永恒之力哦,请先到土城开启永恒之力。发送 开启永恒之力");
                            user.MapCode = "10_1";
                            user.Map = "虚空无尽";
                            user.UserImagePath = $"地图\\5\\虚空无尽\\10_1_{rnd.Next(1, 21)}.jpg";
                            break;
                        case "缥缈之巅":
                            if (user.User_General.Lv < 52) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你没有开启永恒之力哦,请先到土城开启永恒之力。发送 开启永恒之力");
                            user.MapCode = "10_2";
                            user.Map = "缥缈之巅";
                            user.UserImagePath = $"地图\\5\\缥缈之巅\\10_2_{rnd.Next(1, 21)}.jpg";
                            break;
                        case "幽暗村庄":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你没有开启永恒之力哦,请先到土城开启永恒之力。发送 开启永恒之力");
                            user.MapCode = "10_3";
                            user.Map = "幽暗村庄";
                            user.UserImagePath = $"地图\\5\\幽暗村庄\\10_3_{rnd.Next(1, 21)}.jpg";
                            break;
                        #endregion

                        #region 三大陆
                        case "一转圣地":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你没有开启永恒之力哦,请先到土城开启永恒之力。发送 开启永恒之力");
                            if (user.User_RenewLevelAttribute.Level < 1) throw new Exception("噢,大兄弟,转生等级不够,请先去找 转生使者 进行转生再来吧!");
                            user.MapCode = "11_1";
                            user.Map = "一转圣地";
                            user.UserImagePath = $"地图\\6\\一转圣地\\11_1_1.jpg";
                            break;
                        case "三转圣地":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你没有开启永恒之力哦,请先到土城开启永恒之力。发送 开启永恒之力");
                            if (user.User_RenewLevelAttribute.Level < 3) throw new Exception("噢,大兄弟,转生等级不够,请先去找 转生使者 进行转生再来吧!");
                            user.MapCode = "11_3";
                            user.Map = "三转圣地";
                            user.UserImagePath = $"地图\\6\\三转圣地\\11_3_{rnd.Next(1, 11)}.jpg";
                            break;
                        case "五转圣地":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            if (user.IsOnYHZL != "是") throw new Exception("噢,大兄弟,你没有开启永恒之力哦,请先到土城开启永恒之力。发送 开启永恒之力");
                            if (user.User_RenewLevelAttribute.Level < 5) throw new Exception("噢,大兄弟,转生等级不够,请先去找 转生使者 进行转生再来吧!");
                            user.MapCode = "11_5";
                            user.Map = "五转圣地";
                            user.UserImagePath = $"地图\\6\\五转圣地\\11_5_{rnd.Next(1, 11)}.jpg";
                            break;
                        #endregion

                        #region 四大陆
                        case "天帝陵墓1":
                        case "天帝陵墓2":
                        case "天帝陵墓3":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.User_General.YB -= 5000;
                            if (user.User_General.YB < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 1);
                                return path;
                            }
                            switch (mapName)
                            {
                                case "天帝陵墓1": user.MapCode = $"12_2_1"; user.Map = "真の天帝陵墓"; break;
                                case "天帝陵墓2": user.MapCode = $"12_2_1"; user.Map = "幻の天帝陵墓"; break;
                                case "天帝陵墓3": user.MapCode = $"12_2_1"; user.Map = "神の天帝陵墓"; break;
                            }
                            user.UserImagePath = $"地图\\7\\天帝陵墓\\12_2_{rnd.Next(1, 12)}.jpg";
                            break;
                        case "天帝炼狱1":
                        case "天帝炼狱2":
                        case "天帝炼狱3":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.User_General.YB -= 5000;
                            if (user.User_General.YB < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 1);
                                return path;
                            }

                            switch (mapName)
                            {
                                case "天帝炼狱1": user.MapCode = $"12_1_1"; user.Map = "真の天帝炼狱"; break;
                                case "天帝炼狱2": user.MapCode = $"12_1_1"; user.Map = "幻の天帝炼狱"; break;
                                case "天帝炼狱3": user.MapCode = $"12_1_1"; user.Map = "神の天帝炼狱"; break;
                            }
                            user.UserImagePath = $"地图\\7\\天帝炼狱\\12_1_{rnd.Next(1, 27)}.jpg";
                            break;
                        case "天帝足球场1":
                        case "天帝足球场2":
                        case "天帝足球场3":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.User_General.YB -= 5000;
                            if (user.User_General.YB < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 1);
                                return path;
                            }
                            switch (mapName)
                            {
                                case "天帝足球场1": user.MapCode = $"12_4_1"; user.Map = "真の天帝足球场"; break;
                                case "天帝足球场2": user.MapCode = $"12_4_1"; user.Map = "幻の天帝足球场"; break;
                                case "天帝足球场3": user.MapCode = $"12_4_1"; user.Map = "神の天帝足球场"; break;
                            }
                            user.UserImagePath = $"地图\\7\\天帝足球场\\12_4_{rnd.Next(1, 31)}.jpg";
                            break;
                        case "天帝后花园1":
                        case "天帝后花园2":
                        case "天帝后花园3":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.User_General.YB -= 5000;
                            if (user.User_General.YB < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 1);
                                return path;
                            }

                            switch (mapName)
                            {
                                case "天帝后花园1": user.MapCode = $"12_5_1"; user.Map = "真の天帝后花园"; break;
                                case "天帝后花园2": user.MapCode = $"12_5_1"; user.Map = "幻の天帝后花园"; break;
                                case "天帝后花园3": user.MapCode = $"12_5_1"; user.Map = "神の天帝后花园"; break;
                            }
                            user.UserImagePath = $"地图\\7\\天帝后花园\\12_5_{rnd.Next(1, 17)}.jpg";
                            break;
                        case "天帝奴隶场1":
                        case "天帝奴隶场2":
                        case "天帝奴隶场3":
                            if (user.User_General.Lv < 55) throw new Exception("噢,大兄弟,等级不够哦,你再提升提升等级再来找我吧!");
                            user.User_General.YB -= 5000;
                            if (user.User_General.YB < 0)
                            {
                                path = NoMoney(user.UserQQ, userImgPath, 1);
                                return path;
                            }

                            switch (mapName)
                            {
                                case "天帝奴隶场1": user.MapCode = $"12_3_1"; user.Map = "真の天帝奴隶场"; break;
                                case "天帝奴隶场2": user.MapCode = $"12_3_1"; user.Map = "幻の天帝奴隶场"; break;
                                case "天帝奴隶场3": user.MapCode = $"12_3_1"; user.Map = "神の天帝奴隶场"; break;
                            }
                            user.UserImagePath = $"地图\\7\\天帝奴隶场\\12_3_{rnd.Next(1, 20)}.jpg";
                            break;
                        #endregion

                        default: throw new Exception("噢,大兄弟,这个地图不存在噢!请输入 传送员 重新查看吧!");
                    }

                    string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                    IniFileHelper userInI = new IniFileHelper(newPath);
                    userInI.Write("人物信息", "所在地图", user.Map);
                    userInI.Write("人物信息", "所在地图编号", user.MapCode);
                    userInI.Write("人物信息", "坐标", user.Pos);
                    userInI.Write("人物信息", "个人图片", user.UserImagePath);
                    userInI.Write("人物信息", "是否在城里", "否");
                    userInI.Write("普通数据", "金币", userJb < 0 ? "0" : userJb.ToString());
                    userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());

                    if (user.IsSendMsgImg == "否")
                    {
                        return "";
                    }
                    user.UserImagePath = LegendIMGDataPath + user.UserImagePath;
                    int delay = 10;

                    using (MagickImageCollection collection = new MagickImageCollection())
                    {
                        MemoryStream ms = new MemoryStream();
                        Rectangle rect = new Rectangle(5, 0, 0, 0);
                        MagickImage magickImage = null;
                        MagickImage blendImage = null;
                        int magicX = 0, magicY = 0;
                        string posPath = string.Empty;
                        List<string> posList = new List<string>();
                        using (Image imgMian = Image.FromFile(userImgPath))
                        {
                            using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                            {
                                using (Graphics gh = Graphics.FromImage(bitmap))
                                {
                                    gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    magickImage = new MagickImage(ms);
                                    magickImage.AnimationDelay = delay;

                                    collection.Add(magickImage);

                                    for (int i = 250; i < 260; i++)
                                    {
                                        newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                        posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                        posList = FileHelper.ReadTxtReturnList(posPath);
                                        magicX = 512 + int.Parse(posList[0]);
                                        magicY = 314 + int.Parse(posList[1]);
                                        using (Image img = Image.FromFile(newPath))
                                        {
                                            gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                        }

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        blendImage = new MagickImage(ms);
                                        blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                        blendImage.AnimationDelay = delay;
                                        collection.Add(blendImage);
                                    }
                                }
                            }
                        }

                        userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                        using (Image imgMian = Image.FromFile(userImgPath))
                        {
                            using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                            {
                                using (Graphics gh = Graphics.FromImage(bitmap))
                                {
                                    gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    magickImage = new MagickImage(ms);
                                    magickImage.AnimationDelay = delay;
                                    collection.Add(magickImage);

                                    for (int i = 260; i < 270; i++)
                                    {
                                        newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                        posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                        posList = FileHelper.ReadTxtReturnList(posPath);
                                        magicX = 512 + int.Parse(posList[0]);
                                        magicY = 314 + int.Parse(posList[1]);
                                        using (Image img = Image.FromFile(newPath))
                                        {
                                            gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                        }

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        blendImage = new MagickImage(ms);
                                        blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                        blendImage.AnimationDelay = delay;
                                        collection.Add(blendImage);
                                    }

                                    gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    magickImage = new MagickImage(ms);
                                    magickImage.AnimationDelay = 300;
                                    collection.Add(magickImage);
                                }
                            }
                        }

                        path = UserInfoImgPath + user.UserQQ + ".gif";
                        // 3. 优化GIF动画
                        collection.OptimizeTransparency();
                        collection.Write(path, MagickFormat.Gif);
                        magickImage.Dispose();
                    }
                }
                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static string NoMoney(string qq, string userImgPath, int type)
        {
            string path = "";
            using (Image imgMian = Image.FromFile(userImgPath))
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
                        Font font9 = new Font("黑体", 9);
                        Color color = ColorTranslator.FromHtml("#ffffff");
                        Brush brush = new SolidBrush(color);
                        Rectangle rect = new Rectangle(10, 12, 0, 0);
                        StringBuilder sb = new StringBuilder();
                        switch (type)
                        {
                            case 0: sb.AppendLine("你身上的钱不够!请准备好后再来."); break;
                            case 1: sb.AppendLine("你身上的元宝不够!请准备好后再来."); break;
                        }

                        TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font9, brush, rect);//文字 

                        path = $"{UserInfoImgPath + qq}.png";
                        bitmap.Save(path, ImageFormat.Png);//保存图片
                    }
                }
            }
            return path;
        }
        /// <summary>
        /// 回城
        /// </summary>
        public static string GoBackHome(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);// 取原来的地图
                string path = "";
                Font font13 = new Font("黑体", 13);
                SolidBrush brushLimeGreen = new SolidBrush(Color.Lime);

                if (user.MapCode.StartsWith("1_") || user.MapCode.StartsWith("2_") || user.MapCode.StartsWith("3_"))//1_ 开头回比奇 2_沃玛 3_毒蛇
                {
                    user.MapCode = "1_100";
                    user.Map = "比奇城";
                    user.IsOnCity = "是";
                    user.UserImagePath = $"地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                }
                if (user.MapCode.StartsWith("4") || user.MapCode.StartsWith("4_")
                    || user.MapCode.StartsWith("10") || user.MapCode.StartsWith("10_")
                    || user.MapCode.StartsWith("11") || user.MapCode.StartsWith("11_")
                     || user.MapCode.StartsWith("12") || user.MapCode.StartsWith("12_"))//4_ 开头回盟重
                {
                    user.MapCode = "4";
                    user.Map = "盟重土城";
                    user.IsOnCity = "是";
                    user.UserImagePath = $"地图\\4\\盟重土城\\4_{rnd.Next(1, 16)}.jpg";
                }

                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                userInI.Write("人物信息", "所在地图", user.Map);
                userInI.Write("人物信息", "所在地图编号", user.MapCode);
                userInI.Write("人物信息", "个人图片", user.UserImagePath);
                userInI.Write("人物信息", "是否在城里", user.IsOnCity);
                if (user.User_General.Lv <= 7)
                    userInI.Write("人物信息", "城中杀怪数量", "0");

                if (user.IsSendMsgImg == "否")
                {
                    return "";
                }
                user.UserImagePath = LegendFilePath.LegendIMGDataPath + user.UserImagePath;
                int delay = 10;

                using (MagickImageCollection collection = new MagickImageCollection())
                {

                    MemoryStream ms = new MemoryStream();
                    MagickImage magickImage = null;
                    MagickImage blendImage = null;
                    int magicX = 0, magicY = 0;
                    string posPath = string.Empty;
                    List<string> posList = new List<string>();
                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = delay;
                                collection.Add(magickImage);
                                for (int i = 250; i < 260; i++)
                                {
                                    newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                    posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                    posList = FileHelper.ReadTxtReturnList(posPath);
                                    magicX = 512 + int.Parse(posList[0]);
                                    magicY = 314 + int.Parse(posList[1]);
                                    using (Image img = Image.FromFile(newPath))
                                    {
                                        gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                    }

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    blendImage = new MagickImage(ms);
                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);
                                    blendImage.AnimationDelay = delay;

                                    collection.Add(blendImage);
                                }
                            }
                        }
                    }

                    userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = delay;
                                collection.Add(magickImage);

                                for (int i = 260; i < 270; i++)
                                {
                                    newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                    posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                    posList = FileHelper.ReadTxtReturnList(posPath);
                                    magicX = 512 + int.Parse(posList[0]);
                                    magicY = 314 + int.Parse(posList[1]);
                                    using (Image img = Image.FromFile(newPath))
                                    {
                                        gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                    }

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    blendImage = new MagickImage(ms);
                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                    blendImage.AnimationDelay = delay;
                                    collection.Add(blendImage);
                                }

                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = 300;

                                collection.Add(magickImage);
                            }
                        }
                    }

                    path = UserInfoImgPath + user.UserQQ + ".gif";
                    // 3. 优化GIF动画
                    collection.OptimizeTransparency();
                    collection.Write(path, MagickFormat.Gif);
                    magickImage.Dispose();
                }

                return path;
            }
            catch (Exception ex)
            {
                FileHelper.Log("error:" + ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 回城数据,不生成图
        /// </summary>
        public static void GoBackHome2(UserInfo user, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                if (user.MapCode.StartsWith("1_") || user.MapCode.StartsWith("2_") || user.MapCode.StartsWith("3_"))//1_ 开头回比奇 2_沃玛 3_毒蛇
                {
                    user.MapCode = "1_100";
                    user.Map = "比奇城";
                    user.IsOnCity = "是";
                    user.UserImagePath = $"地图\\0\\比奇城\\1_100_{rnd.Next(1, 11)}.jpg";
                }
                if (user.MapCode.StartsWith("4") || user.MapCode.StartsWith("4_")
                     || user.MapCode.StartsWith("10") || user.MapCode.StartsWith("10_")
                     || user.MapCode.StartsWith("11") || user.MapCode.StartsWith("11_")
                      || user.MapCode.StartsWith("12") || user.MapCode.StartsWith("12_"))//4_ 开头回盟重
                {
                    user.MapCode = "4";
                    user.Map = "盟重土城";
                    user.IsOnCity = "是";
                    user.UserImagePath = $"地图\\4\\盟重土城\\4_{rnd.Next(1, 13)}.jpg";
                }

                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                userInI.Write("人物信息", "所在地图", user.Map);
                userInI.Write("人物信息", "所在地图编号", user.MapCode);
                userInI.Write("人物信息", "个人图片", user.UserImagePath);
                userInI.Write("人物信息", "是否在城里", user.IsOnCity);
                userInI.Write("人物信息", "城中杀怪数量", "0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 地图下层 1下2 2下3 .... 3下2 2下1....
        /// </summary>
        public static string GotoMap(UserInfo user, string mapName, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);// 取原来的地图
                string path = "";
                user.Map = mapName;
                switch (mapName)//下到新地图
                {
                    case "兽人古墓1":
                        if (user.MapCode != "1_1_2") throw new Exception("你不在兽人古墓2,请先前往当前地图.");
                        user.MapCode = "1_1_1";
                        user.UserImagePath = $"地图\\0\\兽人古墓1\\1_1_1_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "兽人古墓":
                    case "兽人古墓2":
                        if (user.MapCode != "1_1_1" && user.MapCode != "1_1_3") throw new Exception("你不在兽人古墓1层或兽人古墓3层,请先前往当前地图.");
                        user.MapCode = "1_1_2";
                        user.UserImagePath = $"地图\\0\\兽人古墓2\\1_1_2_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "兽人古墓3":
                        if (user.MapCode != "1_1_2") throw new Exception("你不在兽人古墓2,请先前往当前地图.");
                        user.MapCode = "1_1_3";
                        user.UserImagePath = $"地图\\0\\兽人古墓3\\1_1_3_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "天然洞穴1":
                        user.MapCode = "1_2_1";
                        user.UserImagePath = $"地图\\0\\天然洞穴1\\1_2_1_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "天然洞穴":
                    case "天然洞穴2":
                        user.MapCode = "1_2_4";
                        user.UserImagePath = $"地图\\0\\天然洞穴2\\1_2_4_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "比奇矿区1":
                        if (user.MapCode != "1_3_2") throw new Exception("你不在比奇矿区2层,请先前往当前地图.");
                        user.MapCode = "1_3_1";
                        user.UserImagePath = $"地图\\0\\比奇矿区1\\1_3_1_{rnd.Next(1, 11)}.jpg";
                        break;
                    case "比奇矿区":
                    case "比奇矿区2":
                        if (user.MapCode != "1_3_1" && user.MapCode != "1_3_3") throw new Exception("你不在比奇矿区1层或比奇矿区3层,请先前往当前地图.");
                        user.MapCode = "1_3_2";
                        user.UserImagePath = $"地图\\0\\比奇矿区2\\1_3_2_1.jpg";
                        break;
                    case "比奇矿区3":
                        if (user.MapCode != "1_3_2" && user.MapCode != "1_3_4") throw new Exception("你不在比奇矿区2层或比奇矿区4层,请先前往当前地图.");
                        user.MapCode = "1_3_3";
                        user.UserImagePath = $"地图\\0\\比奇矿区3\\1_3_3_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "毒蛇矿区1":
                        if (user.MapCode != "3_5_2") throw new Exception("你不在毒蛇矿区2层,请先前往当前地图.");
                        user.MapCode = "3_5_1";
                        user.UserImagePath = $"地图\\3\\毒蛇矿区1\\3_5_1_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "毒蛇矿区":
                    case "毒蛇矿区2":
                        if (user.MapCode != "3_5_1" && user.MapCode != "3_5_3") throw new Exception("你不在毒蛇矿区1层或毒蛇矿区3层,请先前往当前地图.");
                        user.MapCode = "3_5_2";
                        user.UserImagePath = $"地图\\3\\毒蛇矿区2\\3_5_2_{rnd.Next(1, 5)}.jpg";
                        break;
                    case "毒蛇矿区3":
                        if (user.MapCode != "3_5_2" && user.MapCode != "3_5_4") throw new Exception("你不在毒蛇矿区2层或毒蛇矿区4层,请先前往当前地图.");
                        user.MapCode = "3_5_3";
                        user.UserImagePath = $"地图\\3\\毒蛇矿区3\\3_5_3_{rnd.Next(1, 3)}.jpg";
                        break;

                    case "沃玛寺庙入口":
                        if (user.MapCode != "2_1_1") throw new Exception("你不在沃玛寺庙1层,请先前往当前地图.");
                        user.MapCode = "2_1";
                        user.UserImagePath = $"地图\\2\\沃玛寺庙入口\\2_1_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "沃玛寺庙":
                    case "沃玛寺庙1":
                        if (user.MapCode != "2_1" && user.MapCode != "2_1_2") throw new Exception("你不在沃玛寺庙入口或沃玛寺庙2层,请先前往当前地图.");
                        user.MapCode = "2_1_1";
                        user.UserImagePath = $"地图\\2\\沃玛寺庙1\\2_1_1_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "沃玛寺庙2":
                        if (user.MapCode != "2_1_1" && user.MapCode != "2_1_3") throw new Exception("你不在沃玛寺庙1层,请先前往当前地图.");
                        user.MapCode = "2_1_2";
                        user.UserImagePath = $"地图\\2\\沃玛寺庙2\\2_1_2_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "沃玛寺庙3":
                        if (user.MapCode != "2_1_2") throw new Exception("你不在沃玛寺庙2层,请先前往当前地图.");
                        user.MapCode = "2_1_3";
                        user.UserImagePath = $"地图\\2\\沃玛寺庙3\\2_1_3_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "蜈蚣洞1":
                        if (user.MapCode != "4_6_2") throw new Exception("你不在蜈蚣洞2层,请先前往当前地图.");
                        user.MapCode = "4_6_1";
                        user.UserImagePath = $"地图\\4\\蜈蚣洞1\\4_6_1_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "蜈蚣洞2":
                        if (user.MapCode != "4_6_1" && user.MapCode != "4_6_3") throw new Exception("你不在蜈蚣洞1层或蜈蚣洞3层,请先前往当前地图.");
                        user.MapCode = "4_6_2";
                        user.UserImagePath = $"地图\\4\\蜈蚣洞2\\4_6_2_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "蜈蚣洞3":
                        if (user.MapCode != "4_6_2") throw new Exception("你不在蜈蚣洞2层,请先前往当前地图.");
                        user.MapCode = "4_6_3";
                        user.UserImagePath = $"地图\\4\\蜈蚣洞3\\4_6_3_1.jpg";
                        break;
                    case "石墓洞1":
                        if (user.MapCode != "4_5_2") throw new Exception("你不在石墓洞2层,请先前往当前地图.");
                        user.MapCode = "4_5_1";
                        user.UserImagePath = $"地图\\4\\石墓洞1\\4_5_1_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "石墓洞2":
                        if (user.MapCode != "4_5_1" && user.MapCode != "4_5_3") throw new Exception("你不在石墓洞1层或石墓洞3层,请先前往当前地图.");
                        user.MapCode = "4_5_2";
                        user.UserImagePath = $"地图\\4\\石墓洞2\\4_5_2_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "石墓洞3":
                        if (user.MapCode != "4_5_2") throw new Exception("你不在石墓洞2层,请先前往当前地图.");
                        user.MapCode = "4_5_3";
                        user.UserImagePath = $"地图\\4\\石墓洞3\\4_5_3_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "祖玛寺庙1":
                        if (user.MapCode != "4_4_2") throw new Exception("你不在祖玛寺庙2层,请先前往当前地图.");
                        user.MapCode = "4_4_1";
                        user.UserImagePath = $"地图\\4\\祖玛寺庙1\\4_4_1_{rnd.Next(1, 13)}.jpg";
                        break;
                    case "祖玛寺庙2":
                        if (user.MapCode != "4_4_1" && user.MapCode != "4_4_3") throw new Exception("你不在祖玛寺庙1层或祖玛寺庙3层,请先前往当前地图.");
                        user.MapCode = "4_4_2";
                        user.UserImagePath = $"地图\\4\\祖玛寺庙2\\4_4_2_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "祖玛寺庙3":
                        if (user.MapCode != "4_4_2" && user.MapCode != "4_4_4") throw new Exception("你不在祖玛寺庙2层或祖玛教主大厅,请先前往当前地图.");
                        user.MapCode = "4_4_3";
                        user.UserImagePath = $"地图\\4\\祖玛寺庙3\\4_4_3_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "祖玛寺庙4":
                    case "祖玛教主大厅":
                        if (user.MapCode != "4_4_3") throw new Exception("你不在祖玛寺庙3层,请先前往当前地图.");
                        user.MapCode = "4_4_4";
                        user.Map = "祖玛教主大厅";
                        user.UserImagePath = $"地图\\4\\祖玛教主大厅\\4_4_4_{rnd.Next(1, 21)}.jpg";
                        break;

                    case "散人打宝1":
                        if (user.MapCode != "4_10_2") throw new Exception("你不在散人打宝2层,请先前往当前地图.");
                        user.MapCode = "4_10_1";
                        user.UserImagePath = $"地图\\4\\散人打宝1\\4_10_1_{rnd.Next(1, 3)}.jpg";
                        break;
                    case "散人打宝2":
                        if (user.MapCode != "4_10_1") throw new Exception("你不在散人打宝1层,请先前往当前地图.");
                        user.MapCode = "4_10_2";
                        user.UserImagePath = $"地图\\4\\散人打宝2\\4_10_2_{rnd.Next(1, 6)}.jpg";
                        break;

                    case "神秘地图1":
                        if (user.MapCode != "4_11_2") throw new Exception("你不在神秘地图2层,请先前往当前地图.");
                        user.MapCode = "4_11_1";
                        user.UserImagePath = $"地图\\4\\神秘地图1\\4_11_1_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "神秘地图2":
                        if (user.MapCode != "4_11_1") throw new Exception("你不在神秘地图1层,请先前往当前地图.");
                        user.MapCode = "4_11_2";
                        user.UserImagePath = $"地图\\4\\神秘地图2\\4_11_2_{rnd.Next(1, 18)}.jpg";
                        break;

                    case "玛雅大厅1":
                        if (user.MapCode != "4_12_2") throw new Exception("你不在玛雅大厅2层,请先前往当前地图.");
                        user.MapCode = "4_12_1";
                        user.UserImagePath = $"地图\\4\\玛雅大厅1\\4_12_1_{rnd.Next(1, 4)}.jpg";
                        break;
                    case "玛雅大厅2":
                        if (user.MapCode != "4_12_1" && user.MapCode != "4_12_3") throw new Exception("你不在玛雅大厅1层或玛雅大厅3层,请先前往当前地图.");
                        user.MapCode = "4_12_2";
                        user.UserImagePath = $"地图\\4\\玛雅大厅2\\4_12_2_{rnd.Next(1, 18)}.jpg";
                        break;
                    case "玛雅大厅3":
                        if (user.MapCode != "4_12_2") throw new Exception("你不在玛雅大厅2层,请先前往当前地图.");
                        user.MapCode = "4_12_3";
                        user.UserImagePath = $"地图\\4\\玛雅大厅3\\4_12_3_{rnd.Next(1, 4)}.jpg";
                        break;
                    default:
                        throw new Exception("噢,大兄弟,这个地图不存在噢!请输入 传送员 重新查看吧!");
                }
                user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                userInI.Write("人物信息", "所在地图", user.Map);
                userInI.Write("人物信息", "所在地图编号", user.MapCode);
                userInI.Write("人物信息", "坐标", user.Pos);
                userInI.Write("人物信息", "个人图片", user.UserImagePath);

                if (user.IsSendMsgImg == "否")
                {
                    return "";
                }

                int delay = 10;

                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    MemoryStream ms = new MemoryStream();
                    MagickImage magickImage = null;
                    MagickImage blendImage = null;
                    int magicX = 0, magicY = 0;
                    string posPath = string.Empty;
                    List<string> posList = new List<string>();
                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = delay;

                                collection.Add(magickImage);

                                for (int i = 250; i < 260; i++)
                                {
                                    newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                    posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                    posList = FileHelper.ReadTxtReturnList(posPath);
                                    magicX = 512 + int.Parse(posList[0]);
                                    magicY = 314 + int.Parse(posList[1]);
                                    using (Image img = Image.FromFile(newPath))
                                    {
                                        gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                    }

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    blendImage = new MagickImage(ms);
                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                    blendImage.AnimationDelay = delay;
                                    collection.Add(blendImage);
                                }
                            }
                        }
                    }
                    user.UserImagePath = LegendIMGDataPath + user.UserImagePath;
                    userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = delay;
                                collection.Add(magickImage);

                                for (int i = 260; i < 270; i++)
                                {
                                    newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                    posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                    posList = FileHelper.ReadTxtReturnList(posPath);
                                    magicX = 512 + int.Parse(posList[0]);
                                    magicY = 314 + int.Parse(posList[1]);
                                    using (Image img = Image.FromFile(newPath))
                                    {
                                        gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                    }

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    blendImage = new MagickImage(ms);
                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                    blendImage.AnimationDelay = delay;
                                    collection.Add(blendImage);
                                }

                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = 300;
                                collection.Add(magickImage);
                            }
                        }
                    }

                    path = UserInfoImgPath + user.UserQQ + ".gif";
                    // 3. 优化GIF动画
                    collection.OptimizeTransparency();
                    collection.Write(path, MagickFormat.Gif);
                    magickImage.Dispose();
                }

                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 前往商店
        /// </summary>
        public static string GoToStrStore(UserInfo user, string mapName, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                string path = "", msg = "";
                switch (user.Map)
                {
                    case "比奇城":
                        path = market_defPath + "比奇城\\";
                        break;
                    case "盟重土城":
                        path = market_defPath + "盟重土城\\";
                        break;
                }
                switch (mapName)
                {
                    case "书店":
                        path = path + "书店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_书店");
                        msg = "这里保存着学徒们需要练学的各种书籍. 你需要什么书？";
                        userInI.Write("人物信息", "所在页面", "1");
                        break;
                    case "药品店":
                        path = path + "药品店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_药品店");
                        msg = "请问你需要点什么吗？";
                        userInI.Write("人物信息", "所在页面", "2");
                        break;
                    case "炼药店":
                        path = path + "炼药店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_炼药店");
                        msg = "请问你要来炼什么药？";
                        userInI.Write("人物信息", "所在页面", "3");
                        break;
                    case "戒指店":
                    case "首饰店":
                        path = path + "戒指店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_戒指店");
                        msg = "请问我可以帮你什么吗？";
                        userInI.Write("人物信息", "所在页面", "4");
                        break;
                    case "手镯店":
                        path = path + "手镯店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_手镯店");
                        msg = "请问我可以帮你什么吗？";
                        userInI.Write("人物信息", "所在页面", "5");
                        break;
                    case "项链店":
                        path = path + "项链店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_项链店");
                        msg = "请问我可以帮你什么吗？";
                        userInI.Write("人物信息", "所在页面", "6");
                        break;
                    case "服装店":
                    case "衣服店":
                    case "布料店":
                        path = path + "服装店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_服装店");
                        msg = "请问什么事情需要我帮忙吗？";
                        userInI.Write("人物信息", "所在页面", "7");
                        break;
                    case "武器店":
                    case "武器铺":
                        path = path + "武器店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_武器店");
                        msg = "欢迎光临.   感谢您来到我们的铁匠铺.";
                        userInI.Write("人物信息", "所在页面", "8");
                        break;
                    case "杂货店":
                    case "杂货铺":
                        path = path + "杂货店.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_杂货店");
                        msg = "请问我可以为你做什么吗？";
                        userInI.Write("人物信息", "所在页面", "9");
                        break;
                    case "仓库":
                        path = path + "仓库.jpg";
                        userInI.Write("人物信息", "所在商店", user.Map + "_仓库");
                        msg = "我是仓库保管员.为了您的仓库又      又      .\n我为您提供以下仓库服务功能：";
                        userInI.Write("人物信息", "所在页面", "10");
                        break;
                    default: throw new Exception("没有这个店铺,请重新再试试.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);
                user.UserImagePath = path;
                int delay = 10;
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    MemoryStream ms = new MemoryStream();
                    MagickImage magickImage = null;
                    MagickImage blendImage = null;
                    int magicX = 0, magicY = 0;
                    string posPath = string.Empty;
                    List<string> posList = new List<string>();
                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = delay;

                                collection.Add(magickImage);

                                for (int i = 250; i < 260; i++)
                                {
                                    newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                    posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                    posList = FileHelper.ReadTxtReturnList(posPath);
                                    magicX = 512 + int.Parse(posList[0]);
                                    magicY = 314 + int.Parse(posList[1]);
                                    using (Image img = Image.FromFile(newPath))
                                    {
                                        gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                    }

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    blendImage = new MagickImage(ms);
                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                    blendImage.AnimationDelay = delay;
                                    collection.Add(blendImage);
                                }
                            }
                        }
                    }

                    userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = delay;
                                collection.Add(magickImage);

                                for (int i = 260; i < 270; i++)
                                {
                                    newPath = magicPath + i.ToString().PadLeft(6, '0') + ".png";
                                    posPath = $"{magicPath}Placements\\{i.ToString().PadLeft(6, '0')}.txt";
                                    posList = FileHelper.ReadTxtReturnList(posPath);
                                    magicX = 512 + int.Parse(posList[0]);
                                    magicY = 314 + int.Parse(posList[1]);
                                    using (Image img = Image.FromFile(newPath))
                                    {
                                        gh.DrawImage(img, magicX, magicY, img.Width, img.Height);
                                    }

                                    bitmap.Save(ms, ImageFormat.Png);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    blendImage = new MagickImage(ms);
                                    blendImage.Composite(magickImage, CompositeOperator.Lighten);

                                    blendImage.AnimationDelay = delay;
                                    collection.Add(blendImage);
                                }


                                gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//重新加载新地图

                                #region 对话框
                                using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                {
                                    gh.DrawImage(img, 0, 0, img.Width, img.Height);
                                }
                                #endregion

                                Font font9 = new Font("黑体", 9);
                                Font fontBold10 = new Font("黑体", 10, FontStyle.Underline);
                                Font fontB10 = new Font("黑体", 10);
                                Brush whiteBrush = new SolidBrush(Color.White);
                                Brush yellowBrush = new SolidBrush(Color.Yellow);
                                Rectangle rect = new Rectangle(15, 12, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, user.Name, fontB10, yellowBrush, rect);

                                rect = new Rectangle(15, 30, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, msg, font9, whiteBrush, rect);

                                switch (mapName)
                                {
                                    case "书店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "书籍", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "书籍", fontB10, whiteBrush, rect);
                                        break;
                                    case "药品店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "药品", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "药品", fontB10, whiteBrush, rect);
                                        break;
                                    case "炼药店":
                                        rect = new Rectangle(85, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(85, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "炼", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(85, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "说明", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(55, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "材料", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(55, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "材料", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(28, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "有关毒药", fontB10, whiteBrush, rect);
                                        break;
                                    case "服装店":
                                    case "衣服店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "修复", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "衣服", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "衣服", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(60, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "衣服", fontB10, whiteBrush, rect);
                                        break;
                                    case "武器店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "修理", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "武器", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "武器", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(60, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "武器", fontB10, whiteBrush, rect);
                                        break;
                                    case "戒指店":
                                    case "首饰店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "修理", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "戒指", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "戒指", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(60, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "戒指", fontB10, whiteBrush, rect);
                                        break;
                                    case "手镯店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "修理", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "手镯", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "手镯", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(60, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "手镯", fontB10, whiteBrush, rect);
                                        break;
                                    case "项链店":
                                        rect = new Rectangle(30, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "卖", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(30, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "修理", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(46, 50, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "项链", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(46, 70, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "项链", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(60, 90, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "项链", fontB10, whiteBrush, rect);
                                        break;
                                    case "杂货店":
                                    case "杂货铺":
                                        rect = new Rectangle(40, 60, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "购买", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(40, 80, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "出售", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(40, 100, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "特殊修复", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(72, 60, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "物品", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(72, 80, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "物品", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(100, 100, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "勋章", fontB10, whiteBrush, rect);
                                        break;
                                    case "仓库":
                                        rect = new Rectangle(40, 60, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "保管", fontBold10, yellowBrush, rect);
                                        rect = new Rectangle(40, 80, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "找回", fontBold10, yellowBrush, rect);

                                        rect = new Rectangle(72, 60, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "物品", fontB10, whiteBrush, rect);
                                        rect = new Rectangle(72, 80, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "物品", fontB10, whiteBrush, rect);

                                        rect = new Rectangle(198, 29, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "<方便>", fontB10, yellowBrush, rect);
                                        rect = new Rectangle(247, 29, 0, 0);
                                        TextHelper.DrawLeftStrokeString(gh, "<安全>", fontB10, yellowBrush, rect);

                                        break;
                                }

                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);
                                magickImage = new MagickImage(ms);
                                magickImage.AnimationDelay = 300;
                                collection.Add(magickImage);
                            }
                        }
                    }

                    path = UserInfoImgPath + user.UserQQ + ".gif";
                    // 3. 优化GIF动画
                    collection.OptimizeTransparency();
                    collection.Write(path, MagickFormat.Gif);
                    magickImage.Dispose();
                }

                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 买商店物品
        /// </summary>
        public static string BuyStore(UserInfo user, string op, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                userInI.Write("人物信息", "所在商店", user.Map + "_万事通买物品");
                string path = "", mud2Path = "";
                int num = 1;
                int index = int.Parse(userInI.Read("人物信息", "所在页面"));
                List<string> list = new List<string>();
                op = op.Replace("买物品", "");
                if (string.IsNullOrEmpty(op))
                {
                    mud2Path = LegendFilePath.Mud2Path + "万事通.txt";
                    list = FileHelper.ReadTxtReturnList(mud2Path);
                }
                else
                {
                    mud2Path = LegendFilePath.Mud2Path + "万事通.txt";
                    list = FileHelper.ReadTxtReturnList(mud2Path);

                    if (op.IndexOf('#') > 0)
                    {
                        num = int.Parse(op.Split('#')[1]);
                    }
                    if (op.IndexOf(',') >= 0 || op.IndexOf(',') >= 0)
                    {
                        index = int.Parse(op.Split(',')[1]);
                        userInI.Write("人物信息", "所在页面", index.ToString());
                    }
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(userImgPath))
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

                            #region 物品框
                            using (Image img = Image.FromFile(prgusePath + "000385.png"))// 物品框
                            {
                                gh.DrawImage(img, 0, 190, img.Width, img.Height);
                            }
                            #endregion

                            Font font12 = new Font("黑体", 12);
                            Font font11 = new Font("黑体", 11);
                            Font fontBold13 = new Font("黑体", 13, FontStyle.Underline);
                            Font fontB13 = new Font("黑体", 13);
                            Brush whiteBrush = new SolidBrush(Color.White);
                            Brush yellowBrush = new SolidBrush(Color.Yellow);
                            Brush yellowRed = new SolidBrush(Color.Red);
                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.Name, fontB13, yellowBrush, rect);

                            int pageSize = 10;
                            int total = list.Count;
                            int record = (total + pageSize - 1) / pageSize;
                            var listPage = list.Skip((index - 1) * pageSize).Take(pageSize).ToList();// 分页 

                            rect = new Rectangle(15, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "你需要什么物品呢？", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 47, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "发送 上一页/下一页 可以进行翻页,输入买物品+物品名称#数字 ", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 64, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "发送 买物品+物品名称#数字", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 81, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "发送 买物品,数字 查看某一页物品", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 95, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "     如：买物品强效金创药#3 就是购买3瓶强效金创药", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 109, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "     注意：药买完了数量会自动恢复10,其它物品则不能购买", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 160, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, $"托管物品列表({total}/99999)件,当前第{index}/{record}页,共{record}页.", font12, whiteBrush, rect);

                            rect = new Rectangle(42, 201, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "物品列表", font12, whiteBrush, rect);

                            rect = new Rectangle(177, 201, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "价格", font12, whiteBrush, rect);

                            rect = new Rectangle(244, 201, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "数量", font12, whiteBrush, rect);

                            List<StditemDB> stdList = LegendApi.GetStdItemsList();

                            for (int i = 0; i < listPage.Count; i++)
                            {
                                string itemName = listPage[i].Split(',')[0];
                                StditemDB std = stdList.Where(s => s.StdName == itemName).FirstOrDefault();
                                std.StdItemMap = user.Map;
                                std.StdItemOwner = user.Name;
                                std.StdItemStuff = "NPC制造";
                                std.StdItemTime = DateTime.Now.ToString();

                                int price = std.Price * 2;
                                int qty = int.Parse(listPage[i].Split(',')[1]);

                                if (!string.IsNullOrEmpty(op) && op.Split('#')[0] == itemName)
                                {
                                    if (qty == 0) num = 0;
                                    if (num > qty)
                                    {
                                        num = qty;
                                        qty = 0;
                                        if (std.StdMode == 0) qty = 10;// 如果是药到了0瓶自动恢复到10瓶
                                    }
                                    else qty -= num;

                                    user.User_General.JB -= num * price;
                                    if (user.User_General.JB < 0) throw new Exception("对不起,背包的金币不够,先多去回收点物品或者修改一下购买数量吧.");

                                    rect = new Rectangle(10, 218 + (i * 14), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, itemName, font11, yellowRed, rect);

                                    rect = new Rectangle(150, 218 + (i * 14), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, price.ToString(), font11, yellowRed, rect);

                                    rect = new Rectangle(240, 218 + (i * 14), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, qty.ToString(), font11, yellowRed, rect);

                                    index = list.FindIndex(s => s.StartsWith(itemName));
                                    list[index] = $"{itemName},{qty}";// 回写到文件集合中

                                    // 判断买的物品是否可以叠加
                                    // 如果可以叠加则直接叠加,否则按数量生成到背包里
                                    if (std.OverLap > 0)
                                    {
                                        if (num > 0)
                                        {
                                            std.SubAttr1 += num;
                                            userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(std));
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < num; k++)
                                        {
                                            userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(std));
                                        }
                                    }
                                    // 扣金币

                                    userInI.Write("普通数据", "金币", user.User_General.JB.ToString());
                                }
                                else
                                {
                                    rect = new Rectangle(10, 218 + (i * 14), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, itemName, font11, whiteBrush, rect);

                                    rect = new Rectangle(150, 218 + (i * 14), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, price.ToString(), font11, whiteBrush, rect);

                                    rect = new Rectangle(240, 218 + (i * 14), 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, qty.ToString(), font11, whiteBrush, rect);
                                }
                            }

                            File.WriteAllText(mud2Path, string.Join("\r\n", list));
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
        /// 卖商店物品
        /// </summary>
        public static string SellStore(UserInfo user, string op, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                if (op.StartsWith("肉"))
                {
                    return "";
                }
                string newPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(newPath);
                string path = "", msg = "", mud2Path = "", currStore = "";
                List<string> list = new List<string>();
                if (op == "卖物品")
                {
                    msg = "让我看看你要卖什么东西.";
                    mud2Path = LegendFilePath.Mud2Path + "万事通.txt";
                    list = FileHelper.ReadTxtReturnList(mud2Path);

                    userInI.Write("人物信息", "所在商店", user.Map + "_万事通");
                    userInI.Write("人物信息", "所在页面", "1");
                }
                else
                {
                    currStore = userInI.Read("人物信息", "所在商店");
                    path = market_defPath + currStore.Replace("_", "\\") + ".jpg";
                    if (currStore.Contains("书店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("书"))
                        {
                            throw new Exception("你当前位置在城里的书店,请前往需要卖物品的商店,输入 #前往武器店 即可");
                        }

                        msg = "如果你有不错的书籍,我也可以购买.请把要出售的书籍放入栏里.";
                        mud2Path = LegendFilePath.Mud2Path + "书店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);
                    }
                    if (currStore.Contains("药品店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("药"))
                        {
                            throw new Exception("你当前位置在城里的药品店,请前往需要卖物品的商店,输入 #前往武器店 即可");
                        }
                        msg = "让我看看你要卖什么东西.";
                        mud2Path = LegendFilePath.Mud2Path + "药店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);
                    }
                    if (currStore.Contains("戒指店") || currStore.Contains("首饰店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("戒指"))
                        {
                            throw new Exception("你当前位置在城里的戒指店,请前往需要卖物品的商店,输入 #前往武器店 即可");
                        }
                        msg = "您要出售哪种戒指？";
                        mud2Path = LegendFilePath.Mud2Path + "戒指店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);

                    }
                    if (currStore.Contains("手镯店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("手镯"))
                        {
                            throw new Exception("你当前位置在城里的手镯店,请前往需要卖物品的商店,输入 #前往武器店 即可");
                        }
                        msg = "您要出售哪种手镯？\n我们也回收手套.";
                        mud2Path = LegendFilePath.Mud2Path + "手镯店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);

                    }
                    if (currStore.Contains("项链店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("项链"))
                        {
                            throw new Exception("你当前位置在城里的项链店,请前往需要卖物品的商店,输入 #前往武器店 即可");
                        }
                        msg = "您要出售哪种项链？";
                        mud2Path = LegendFilePath.Mud2Path + "项链店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);

                    }
                    if (currStore.Contains("服装店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("衣服"))
                        {
                            throw new Exception("你当前位置在城里的服装店,请前往需要卖物品的商店,输入 #前往武器店 即可");
                        }
                        msg = "你想买什么样的衣服？";
                        mud2Path = LegendFilePath.Mud2Path + "服装店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);
                    }
                    if (currStore.Contains("武器店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("武器"))
                        {
                            throw new Exception("你当前位置在城里的武器店,请前往需要卖物品的商店,输入 #前往戒指店 即可");
                        }
                        msg = "给我您要卖的武器.";
                        mud2Path = LegendFilePath.Mud2Path + "武器店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);
                    }
                    if (currStore.Contains("杂货店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("材料"))
                        {
                            throw new Exception("你当前位置在城里的杂货店,请前往需要卖物品的商店,输入 #前往戒指店 即可");
                        }
                        msg = "要卖什么？很多的杂物我这里都可以卖哦.";
                        mud2Path = LegendFilePath.Mud2Path + "杂货店.txt";
                        list = FileHelper.ReadTxtReturnList(mud2Path);
                    }
                    if (currStore.Contains("炼药店"))
                    {
                        if (!string.IsNullOrEmpty(op) && !op.Contains("材料"))
                        {
                            throw new Exception("你当前位置在城里的炼药店,请前往需要卖物品的商店,输入 #前往戒指店 即可");
                        }
                        msg = "要卖什么？把不需要的炼药材料都卖给我吧.";
                    }
                    if (currStore.Contains("仓库"))
                    {
                        throw new Exception("你当前位置在城里的仓库中,请前往需要购买物品的商店,输入 #前往武器店 即可");
                    }
                    user.UserImagePath = path;
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(userImgPath))
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

                            #region 物品框
                            using (Image img = Image.FromFile(prgusePath + "000392.png"))// 物品框
                            {
                                gh.DrawImage(img, 300, 190, img.Width, img.Height);
                            }
                            #endregion
                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            Font font12 = new Font("黑体", 12);
                            Font font13 = new Font("黑体", 13);
                            Brush whiteBrush = new SolidBrush(Color.White);
                            Brush yellowBrush = new SolidBrush(Color.Yellow);
                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.Name, font13, yellowBrush, rect);

                            rect = new Rectangle(15, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, msg, font12, whiteBrush, rect);

                            #region 生成背包图片
                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))
                            {
                                gh.DrawImage(img, 557, 10, img.Width, img.Height);
                            }

                            //加载背包里的物品
                            rect = new Rectangle(618, 288, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.User_General.JB.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//金币

                            rect = new Rectangle(625, 325, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  元宝：" + user.User_General.YB.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//元宝 

                            rect = new Rectangle(625, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "金刚石：" + user.User_General.JGS.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//金刚石

                            StditemDB stditemDB = null;
                            StditemDB copyStditem = null;

                            List<string> listBwg = new List<string>();
                            List<string> listBwg2 = new List<string>();
                            listBwg = userInI.GetAllKeys("背包物品");

                            int i = 0, humX = 0, humY = 0, textX = 0, textY = 0, bgIndexX = 0, bgIndexY = 0;
                            string newpath = "";
                            string stdName = "";
                            int qty = 0;
                            listBwg.ForEach(itemKey =>
                            {
                                stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", itemKey));
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
                                                textX = (i * 35) + 611;
                                                textY = 20;
                                                bgIndexX = (i * 35) + 575;
                                                bgIndexY = 42;
                                                break;
                                            case 1:
                                                humX = (i - 12) * 35 - (img.Width / 2) + 594;
                                                humY = 77 - (img.Height / 2) - 6;
                                                textX = (i - 12) * 35 + 611;
                                                textY = 53;
                                                bgIndexX = (i - 12) * 35 + 575;
                                                bgIndexY = 75;
                                                break;
                                            case 2:
                                                humX = (i - 24) * 35 - (img.Width / 2) + 594;
                                                humY = 108 - (img.Height / 2) - 6;
                                                textX = (i - 24) * 35 + 611;
                                                textY = 86;
                                                bgIndexX = (i - 24) * 35 + 575;
                                                bgIndexY = 104;
                                                break;
                                            case 3:
                                                humX = (i - 36) * 35 - (img.Width / 2) + 594;
                                                humY = 139 - (img.Height / 2) - 6;
                                                textX = (i - 36) * 35 + 611;
                                                textY = 114;
                                                bgIndexX = (i - 36) * 35 + 575;
                                                bgIndexY = 137;
                                                break;
                                            case 4:
                                                humX = (i - 48) * 35 - (img.Width / 2) + 594;
                                                humY = 170 - (img.Height / 2) - 6;
                                                textX = (i - 48) * 35 + 611;
                                                textY = 147;
                                                bgIndexX = (i - 48) * 35 + 575;
                                                bgIndexY = 170;
                                                break;
                                            case 5:
                                                humX = (i - 60) * 35 - (img.Width / 2) + 594;
                                                humY = 204 - (img.Height / 2) - 6;
                                                textX = (i - 60) * 35 + 611;
                                                textY = 180;
                                                bgIndexX = (i - 60) * 35 + 575;
                                                bgIndexY = 202;
                                                break;
                                            case 6:
                                                humX = (i - 72) * 35 - (img.Width / 2) + 594;
                                                humY = 237 - (img.Height / 2) - 6;
                                                textX = (i - 72) * 35 + 611;
                                                textY = 213;
                                                bgIndexX = (i - 72) * 35 + 575;
                                                bgIndexY = 236;
                                                break;
                                            case 7:
                                                humX = (i - 84) * 35 - (img.Width / 2) + 594;
                                                humY = 270 - (img.Height / 2) - 6;
                                                textX = (i - 84) * 35 + 611;
                                                textY = 246;
                                                bgIndexX = (i - 84) * 35 + 575;
                                                bgIndexY = 269;
                                                break;
                                            default:
                                                break;
                                        }
                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                    }

                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                    {
                                        rect = new Rectangle(textX, textY, 0, 0);
                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font12, brushLimeGreen, rect);
                                    }

                                    if (op == "卖物品")
                                    {
                                        switch (stditemDB.StdMode)
                                        {
                                            case 0://药
                                                if (stditemDB.StdHP == 0 && stditemDB.StdMac > 0)//卖掉只加蓝的药
                                                {
                                                    if (stditemDB.SubAttr1 > 0)
                                                        user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                    else
                                                        user.User_General.JB += stditemDB.Price;// +钱
                                                    user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                    listBwg2.Add(stditemDB.StdName);
                                                    copyStditem = stditemDB;
                                                    userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                                }
                                                break;
                                            case 3://卷类 祝福油等
                                                if (stditemDB.Shape == 3 || stditemDB.Shape == 12)//卖掉回城卷 药水
                                                {
                                                    if (stditemDB.SubAttr1 > 0)
                                                        user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                    else
                                                        user.User_General.JB += stditemDB.Price;// +钱
                                                    user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                    listBwg2.Add(stditemDB.StdName);
                                                    copyStditem = stditemDB;
                                                    userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                                }
                                                break;
                                            case 31:// 捆装类
                                            case 41:// 任务类
                                            case 49:// 任务类
                                                break;// 非卖品
                                            default:
                                                if (stditemDB.SubAttr1 > 0)
                                                    user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                else
                                                    user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (op.Contains("书"))
                                        {
                                            if (stditemDB.StdMode == 4)
                                            {
                                                if (stditemDB.SubAttr1 > 0)
                                                    user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                else
                                                    user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("材料"))
                                        {
                                            if (currStore.Contains("炼药店"))// 炼药的卖的材料
                                            {
                                                if (stditemDB.StdMode == 42)
                                                {
                                                    if (stditemDB.SubAttr1 > 0)
                                                        user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                    else
                                                        user.User_General.JB += stditemDB.Price;// +钱
                                                    user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                    listBwg2.Add(stditemDB.StdName);
                                                    copyStditem = stditemDB;
                                                    userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                                }
                                            }
                                            if (currStore.Contains("杂货店"))// 杂货店卖的材料
                                            {
                                                if (stditemDB.StdMode == 3
                                                        || stditemDB.StdMode == 25
                                                        || stditemDB.StdMode == 30
                                                        || stditemDB.StdMode == 31
                                                        || stditemDB.StdMode == 41
                                                        || stditemDB.StdMode == 44
                                                        || stditemDB.StdMode == 45
                                                        || stditemDB.StdMode == 46
                                                )
                                                {
                                                    if (stditemDB.SubAttr1 > 0)
                                                        user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                    else
                                                        user.User_General.JB += stditemDB.Price;// +钱
                                                    user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                    listBwg2.Add(stditemDB.StdName);
                                                    copyStditem = stditemDB;
                                                    userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                                }
                                            }
                                        }
                                        if (op.Contains("药"))
                                        {
                                            if (stditemDB.StdMode == 0)
                                            {
                                                if (stditemDB.SubAttr1 > 0)
                                                    user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;// +钱
                                                else
                                                    user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("戒指"))
                                        {
                                            if (stditemDB.StdMode == 22 || stditemDB.StdMode == 23)
                                            {
                                                user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("手镯"))
                                        {
                                            if (stditemDB.StdMode == 24 || stditemDB.StdMode == 26)
                                            {
                                                user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("项链"))
                                        {
                                            if (stditemDB.StdMode == 19 || stditemDB.StdMode == 20 || stditemDB.StdMode == 21)
                                            {
                                                user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("头盔"))
                                        {
                                            if (stditemDB.StdMode == 15 || stditemDB.StdMode == 16)
                                            {
                                                user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("武器"))
                                        {
                                            if (stditemDB.StdMode == 5 || stditemDB.StdMode == 6 || stditemDB.StdMode == 43)
                                            {
                                                user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                        if (op.Contains("衣服"))
                                        {
                                            if (stditemDB.StdMode == 15 || stditemDB.StdMode == 16 || stditemDB.StdMode == 10 || stditemDB.StdMode == 11)
                                            {
                                                user.User_General.JB += stditemDB.Price;// +钱
                                                user.User_General.Bgw -= stditemDB.Weight;// -负重
                                                listBwg2.Add(stditemDB.StdName);
                                                copyStditem = stditemDB;
                                                userInI.Delete("背包物品", itemKey); // 删除背包里的物品
                                            }
                                        }
                                    }

                                    i++;

                                    // 背包位置
                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font12, whiteBrush, rect);
                                }
                            });

                            if (!string.IsNullOrEmpty(mud2Path))
                            {
                                var q = from p in listBwg2 group p by p into g select new { Key = g.Count(), name = g.Key };
                                Dictionary<string, int> kv = new Dictionary<string, int>();
                                foreach (var item in q)
                                {
                                    kv.Add(item.name, item.Key);
                                }

                                for (int j = kv.Count() - 1; j >= 0; j--)
                                {
                                    var temp = kv.ElementAt(j);

                                    if (list.Where(b => b.Split(',')[0].Equals(temp.Key)).Count() == 0)// 先加没有的物品,再循环加有的物品,将原来没有的删掉
                                    {
                                        list.Add(temp.Key + "," + temp.Value);
                                        kv.Remove(temp.Key);
                                    }
                                }

                                for (int j = 0; j < list.Count; j++)
                                {
                                    stdName = list[j].Split(',')[0];
                                    qty = int.Parse(list[j].Split(',')[1]);
                                    if (kv.ContainsKey(stdName))
                                        list[j] = stdName + "," + (kv[stdName] + qty);
                                }


                                File.WriteAllText(mud2Path, string.Join("\r\n", list));
                            }

                            userInI.Write("普通数据", "金币", user.User_General.JB.ToString());
                            userInI.Write("普通数据", "背包重量", user.User_General.Bgw.ToString());
                            if (copyStditem != null)
                            {
                                newpath = itemsPath + copyStditem.StdImgPath;
                                using (Image img = Image.FromFile(newpath))
                                {
                                    gh.DrawImage(img, 350, 270, img.Width, img.Height);
                                }

                                rect = new Rectangle(305, 196, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, "卖：" + copyStditem.Price, font13, whiteBrush, rect);//卖的金额
                            }
                            #endregion

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
        /// 保管物品
        /// </summary>
        public static string SafeStore(UserInfo user, int index, int endNum, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            string path = "";
            try
            {
                // 查询出背包里所有的物品
                path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                List<string> list = userInI.GetAllKeys("背包物品");
                if (index > list.Count) throw new Exception("噢,抱歉,物品保管错误或该位置没有装备,请再试试吧!");

                if (endNum < index && endNum > 0) throw new Exception("保存的物品数量发生错误,请重新再试！");

                // 取对应物品
                StditemDB stditemDB = null;
                string stdMsg = "";

                for (int i = index; i <= endNum; i++)
                {
                    string key = list[i - 1];
                    string value = userInI.ReadValue("背包物品", key);
                    stditemDB = Api.JsonDeserialize<StditemDB>(value);
                    stdMsg += stditemDB.StdName + ",";
                    string newStdId = userInI.GetRndId();//生成新id
                    userInI.Write("个人仓库", newStdId, value);
                    userInI.Delete("背包物品", key);
                    if (user.IsOnCity == "是")
                    {
                        userInI.Write("人物信息", "所在商店", user.Map + "_仓库");
                    }
                    userInI.Write("人物信息", "所在页面", "1");
                }


                if (user.IsSendMsgImg == "否")
                {
                    return stdMsg.TrimEnd(',');
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(userImgPath))
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

                            #region 物品框
                            using (Image img = Image.FromFile(prgusePath + "000392.png"))// 物品框
                            {
                                gh.DrawImage(img, 300, 190, img.Width, img.Height);
                            }
                            #endregion
                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            Font font12 = new Font("黑体", 12);
                            Font fontBold8 = new Font("黑体", 8);
                            Font font13 = new Font("黑体", 13);
                            Brush whiteBrush = new SolidBrush(Color.White);
                            Brush yellowBrush = new SolidBrush(Color.Yellow);
                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.Name, font13, yellowBrush, rect);

                            rect = new Rectangle(15, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "您要保存什么物品呢？我这里可以放心保管.", font12, whiteBrush, rect);

                            if (stditemDB != null)
                            {
                                string newpath = itemsPath + stditemDB.StdImgPath;
                                using (Image img = Image.FromFile(newpath))
                                {
                                    gh.DrawImage(img, 345, 270, img.Width, img.Height);
                                }

                                rect = new Rectangle(305, 196, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, stditemDB.StdName, font12, whiteBrush, rect);
                            }

                            #region 添加背包
                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))
                            {
                                gh.DrawImage(img, 557, 10, img.Width, img.Height);
                            }

                            //加载背包里的物品
                            rect = new Rectangle(618, 288, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.User_General.JB.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//金币

                            rect = new Rectangle(625, 325, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  元宝：" + user.User_General.YB.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//元宝 

                            rect = new Rectangle(625, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "金刚石：" + user.User_General.JGS.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//金刚石


                            List<string> listBwg = userInI.GetAllKeys("背包物品");
                            int i = 0, humX = 0, humY = 0, textX = 0, textY = 0, jpX = 0, jpY = 0, bgIndexX = 0, bgIndexY = 0;

                            listBwg.ForEach(itemKey =>
                            {
                                stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", itemKey));
                                if (stditemDB != null)
                                {
                                    string newpath = itemsPath + stditemDB.StdImgPath;
                                    using (Image img = Image.FromFile(newpath))   //加载背包里的物品
                                    {
                                        switch (i / 12)
                                        {
                                            case 0:
                                                humX = (i * 35) - (img.Width / 2) + 594;
                                                humY = 44 - (img.Height / 2) - 6;
                                                textX = (i * 35) + 611;
                                                textY = 20;
                                                bgIndexX = (i * 35) + 575;
                                                bgIndexY = 42;
                                                break;
                                            case 1:
                                                humX = (i - 12) * 35 - (img.Width / 2) + 594;
                                                humY = 77 - (img.Height / 2) - 6;
                                                textX = (i - 12) * 35 + 611;
                                                textY = 53;
                                                bgIndexX = (i - 12) * 35 + 575;
                                                bgIndexY = 75;
                                                break;
                                            case 2:
                                                humX = (i - 24) * 35 - (img.Width / 2) + 594;
                                                humY = 108 - (img.Height / 2) - 6;
                                                textX = (i - 24) * 35 + 611;
                                                textY = 86;
                                                bgIndexX = (i - 24) * 35 + 575;
                                                bgIndexY = 104;
                                                break;
                                            case 3:
                                                humX = (i - 36) * 35 - (img.Width / 2) + 594;
                                                humY = 139 - (img.Height / 2) - 6;
                                                textX = (i - 36) * 35 + 611;
                                                textY = 114;
                                                bgIndexX = (i - 36) * 35 + 575;
                                                bgIndexY = 137;
                                                break;
                                            case 4:
                                                humX = (i - 48) * 35 - (img.Width / 2) + 594;
                                                humY = 170 - (img.Height / 2) - 6;
                                                textX = (i - 48) * 35 + 611;
                                                textY = 147;
                                                bgIndexX = (i - 48) * 35 + 575;
                                                bgIndexY = 170;
                                                break;
                                            case 5:
                                                humX = (i - 60) * 35 - (img.Width / 2) + 594;
                                                humY = 204 - (img.Height / 2) - 6;
                                                textX = (i - 60) * 35 + 611;
                                                textY = 180;
                                                bgIndexX = (i - 60) * 35 + 575;
                                                bgIndexY = 202;
                                                break;
                                            case 6:
                                                humX = (i - 72) * 35 - (img.Width / 2) + 594;
                                                humY = 237 - (img.Height / 2) - 6;
                                                textX = (i - 72) * 35 + 611;
                                                textY = 213;
                                                bgIndexX = (i - 72) * 35 + 575;
                                                bgIndexY = 236;
                                                break;
                                            case 7:
                                                humX = (i - 84) * 35 - (img.Width / 2) + 594;
                                                humY = 270 - (img.Height / 2) - 6;
                                                textX = (i - 84) * 35 + 611;
                                                textY = 246;
                                                bgIndexX = (i - 84) * 35 + 575;
                                                bgIndexY = 269;
                                                break;
                                            default:
                                                break;
                                        }
                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                    }
                                    i++;

                                    // 背包位置
                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font12, whiteBrush, rect);

                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                    {
                                        rect = new Rectangle(textX, textY, 0, 0);
                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font12, brushLimeGreen, rect);
                                    }
                                }
                            });
                            #endregion

                        }

                        path = $"{UserInfoImgPath + user.UserQQ}.png";
                        bitmap.Save(path, ImageFormat.Png);//保存图片
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return path;
        }

        /// <summary>
        /// 找回物品
        /// </summary>
        public static string RecoverStore(UserInfo user, int index, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            string path = "";
            try
            {
                if (index <= 0) index = 1;
                int pageSize = 80;
                // 查询出背包里所有的物品
                path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                var stokList = userInI.GetAllKeys("个人仓库");
                int total = stokList.Count;
                int record = (total + pageSize - 1) / pageSize;
                if (index > record) index = record;
                stokList = stokList.Skip((index - 1) * pageSize).Take(pageSize).ToList();

                List<string> list = userInI.GetAllKeys("背包物品");

                userInI.Write("人物信息", "所在商店", user.Map + "_仓库");
                userInI.Write("人物信息", "所在页面", index.ToString());

                int i = 0, humX = 0, humY = 0, textX = 0, textY = 0, jpX = 0, jpY = 0, bgIndexX = 0, bgIndexY = 0;

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);
                using (Image imgMian = Image.FromFile(userImgPath))
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

                            #region 物品框
                            using (Image img = Image.FromFile(newopUIPath + "001909.png"))// 物品框
                            {
                                gh.DrawImage(img, 0, 190, img.Width, img.Height);
                            }
                            #endregion

                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            Font font12 = new Font("黑体", 12);
                            Font font13 = new Font("黑体", 13);
                            Brush whiteBrush = new SolidBrush(Color.White);
                            Brush yellowBrush = new SolidBrush(Color.Yellow);
                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.Name, font13, yellowBrush, rect);

                            rect = new Rectangle(15, 30, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "您要找回什么物品呢？请看目录决定找回什么东西.", font12, whiteBrush, rect);
                            rect = new Rectangle(15, 60, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "输入指令取回+编号,可以取出仓库里的物品.\r\n如：取回3 就是取出第3个物品,编号在格子右下角白色字母", font12, whiteBrush, rect);

                            rect = new Rectangle(15, 163, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, $"托管物品列表({total}/99999)件,当前第{index}/{record}页,共{record}页.", font12, whiteBrush, rect);

                            #region 仓库物品
                            stokList.ForEach(s =>
                            {
                                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("个人仓库", s));

                                if (stditemDB != null)
                                {
                                    string newpath = itemsPath + stditemDB.StdImgPath;
                                    using (Image img = Image.FromFile(newpath))
                                    {
                                        switch (i / 10)
                                        {
                                            case 0:
                                                humX = (i * 37) - (img.Width / 2) + 36;
                                                humY = 222 - (img.Height / 2);
                                                textX = (i * 37) + 50;
                                                textY = 205;
                                                bgIndexX = (i * 37) + 18;
                                                bgIndexY = 225;
                                                break;
                                            case 1:
                                                humX = (i - 10) * 37 - (img.Width / 2) + 36;
                                                humY = 255 - (img.Height / 2);
                                                textX = (i - 10) * 37 + 50;
                                                textY = 237;
                                                bgIndexX = (i - 10) * 37 + 18;
                                                bgIndexY = 260;
                                                break;
                                            case 2:
                                                humX = (i - 20) * 37 - (img.Width / 2) + 36;
                                                humY = 288 - (img.Height / 2);
                                                textX = (i - 20) * 37 + 50;
                                                textY = 270;
                                                bgIndexX = (i - 20) * 37 + 18;
                                                bgIndexY = 292;
                                                break;
                                            case 3:
                                                humX = (i - 30) * 37 - (img.Width / 2) + 36;
                                                humY = 321 - (img.Height / 2);
                                                textX = (i - 30) * 37 + 50;
                                                textY = 303;
                                                bgIndexX = (i - 30) * 37 + 18;
                                                bgIndexY = 325;
                                                break;
                                            case 4:
                                                humX = (i - 40) * 37 - (img.Width / 2) + 36;
                                                humY = 354 - (img.Height / 2);
                                                textX = (i - 40) * 37 + 50;
                                                textY = 336;
                                                bgIndexX = (i - 40) * 37 + 18;
                                                bgIndexY = 359;
                                                break;
                                            case 5:
                                                humX = (i - 50) * 37 - (img.Width / 2) + 36;
                                                humY = 387 - (img.Height / 2);
                                                textX = (i - 50) * 37 + 50;
                                                textY = 369;
                                                bgIndexX = (i - 50) * 37 + 18;
                                                bgIndexY = 392;
                                                break;
                                            case 6:
                                                humX = (i - 60) * 37 - (img.Width / 2) + 36;
                                                humY = 420 - (img.Height / 2);
                                                textX = (i - 60) * 37 + 50;
                                                textY = 402;
                                                bgIndexX = (i - 60) * 37 + 18;
                                                bgIndexY = 424;
                                                break;
                                            case 7:
                                                humX = (i - 70) * 37 - (img.Width / 2) + 36;
                                                humY = 453 - (img.Height / 2);
                                                textX = (i - 70) * 37 + 50;
                                                textY = 435;
                                                bgIndexX = (i - 70) * 37 + 18;
                                                bgIndexY = 457;
                                                break;
                                            default:
                                                break;
                                        }
                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                    }

                                    i++;
                                    // 背包位置
                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font12, whiteBrush, rect);

                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                    {
                                        rect = new Rectangle(textX, textY, 0, 0);
                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font12, brushLimeGreen, rect);
                                    }
                                }
                            });
                            #endregion

                            #region 添加背包
                            i = 0;
                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))
                            {
                                gh.DrawImage(img, 557, 10, img.Width, img.Height);
                            }

                            //加载背包里的物品
                            rect = new Rectangle(618, 288, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, user.User_General.JB.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//金币

                            rect = new Rectangle(625, 325, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "  元宝：" + user.User_General.YB.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//元宝 

                            rect = new Rectangle(625, 345, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "金刚石：" + user.User_General.JGS.ToString("N0", CultureInfo.InvariantCulture), font13, whiteBrush, rect);//金刚石

                            List<string> listBwg = userInI.GetAllKeys("背包物品");

                            listBwg.ForEach(itemKey =>
                            {
                                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", itemKey));
                                if (stditemDB != null)
                                {
                                    string newpath = itemsPath + stditemDB.StdImgPath;
                                    using (Image img = Image.FromFile(newpath))   //加载背包里的物品
                                    {
                                        switch (i / 12)
                                        {
                                            case 0:
                                                humX = (i * 35) - (img.Width / 2) + 594;
                                                humY = 44 - (img.Height / 2) - 6;
                                                textX = (i * 35) + 611;
                                                textY = 20;
                                                bgIndexX = (i * 35) + 575;
                                                bgIndexY = 42;
                                                break;
                                            case 1:
                                                humX = (i - 12) * 35 - (img.Width / 2) + 594;
                                                humY = 77 - (img.Height / 2) - 6;
                                                textX = (i - 12) * 35 + 611;
                                                textY = 53;
                                                bgIndexX = (i - 12) * 35 + 575;
                                                bgIndexY = 75;
                                                break;
                                            case 2:
                                                humX = (i - 24) * 35 - (img.Width / 2) + 594;
                                                humY = 108 - (img.Height / 2) - 6;
                                                textX = (i - 24) * 35 + 611;
                                                textY = 86;
                                                bgIndexX = (i - 24) * 35 + 575;
                                                bgIndexY = 104;
                                                break;
                                            case 3:
                                                humX = (i - 36) * 35 - (img.Width / 2) + 594;
                                                humY = 139 - (img.Height / 2) - 6;
                                                textX = (i - 36) * 35 + 611;
                                                textY = 114;
                                                bgIndexX = (i - 36) * 35 + 575;
                                                bgIndexY = 137;
                                                break;
                                            case 4:
                                                humX = (i - 48) * 35 - (img.Width / 2) + 594;
                                                humY = 170 - (img.Height / 2) - 6;
                                                textX = (i - 48) * 35 + 611;
                                                textY = 147;
                                                bgIndexX = (i - 48) * 35 + 575;
                                                bgIndexY = 170;
                                                break;
                                            case 5:
                                                humX = (i - 60) * 35 - (img.Width / 2) + 594;
                                                humY = 204 - (img.Height / 2) - 6;
                                                textX = (i - 60) * 35 + 611;
                                                textY = 180;
                                                bgIndexX = (i - 60) * 35 + 575;
                                                bgIndexY = 202;
                                                break;
                                            case 6:
                                                humX = (i - 72) * 35 - (img.Width / 2) + 594;
                                                humY = 237 - (img.Height / 2) - 6;
                                                textX = (i - 72) * 35 + 611;
                                                textY = 213;
                                                bgIndexX = (i - 72) * 35 + 575;
                                                bgIndexY = 236;
                                                break;
                                            case 7:
                                                humX = (i - 84) * 35 - (img.Width / 2) + 594;
                                                humY = 270 - (img.Height / 2) - 6;
                                                textX = (i - 84) * 35 + 611;
                                                textY = 246;
                                                bgIndexX = (i - 84) * 35 + 575;
                                                bgIndexY = 269;
                                                break;
                                            default:
                                                break;
                                        }
                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                    }
                                    i++;

                                    // 背包位置
                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font12, whiteBrush, rect);

                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                    {
                                        rect = new Rectangle(textX, textY, 0, 0);
                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font12, brushLimeGreen, rect);
                                    }
                                }
                            });
                            #endregion


                        }

                        path = $"{UserInfoImgPath + user.UserQQ}.png";
                        bitmap.Save(path, ImageFormat.Png);//保存图片
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return path;
        }

        /// <summary>
        /// 取出仓库里的物品
        /// </summary>
        public static string GetStdItemByStore(UserInfo user, int index, int endNum, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                if (index <= 0) throw new Exception("取出的编号不能小于等于0,取出物品失败.");
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                var stokList = userInI.GetAllKeys("个人仓库");
                if (index > stokList.Count) throw new Exception("取出的编号不能大于仓库数量,取出物品失败.");
                if (endNum < index && endNum > 0) throw new Exception("保存的物品数量发生错误,请重新再试！");

                int page = int.Parse(userInI.Read("人物信息", "所在页面"));
                int pageSize = 80;
                stokList = stokList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                string stdMsg = "";
                for (int i = index; i <= endNum; i++)
                {
                    string key = stokList[i - 1];
                    string stditem = userInI.ReadValue("个人仓库", key);
                    StditemDB std = Api.JsonDeserialize<StditemDB>(stditem);
                    if (std != null)
                    {
                        stdMsg += std.StdName + ",";
                        var bpList = userInI.GetAllKeys("背包物品");
                        if (bpList.Count >= 96) throw new Exception("背包已满,请保存或卖掉一些物品吧,保存请输入 保存+编号,卖装备请输入 卖物品 一键卖掉背包所有物品,请谨慎操作.");
                        userInI.Delete("个人仓库", key);
                        key = userInI.GetRndId();
                        userInI.Write("背包物品", key, stditem);
                    }
                }


                return $"物品 [{stdMsg.TrimEnd(',')}] 取出成功,请输入 查看背包 查看取出的物品吧!";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        public static string JumpPage(UserInfo user, int index, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                string store = userInI.Read("人物信息", "所在商店");
                int page = int.Parse(userInI.Read("人物信息", "所在页面"));

                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true, true);

                if (!string.IsNullOrEmpty(store) && store.IndexOf("_") > 0)
                {
                    page += index;
                    if (page <= 0) page = 1;
                    int pageSize = 80;
                    store = store.Substring(store.IndexOf('_') + 1);

                    Brush brushLimeGreen = new SolidBrush(Color.Lime);
                    Font font9 = new Font("黑体", 9);
                    Font font13 = new Font("黑体", 13);
                    Font fontBold8 = new Font("黑体", 8);
                    Font fontB10 = new Font("黑体", 10);
                    Brush whiteBrush = new SolidBrush(Color.White);
                    Brush yellowBrush = new SolidBrush(Color.Yellow);
                    Brush yellowRed = new SolidBrush(Color.Red);
                    switch (store)
                    {
                        case "仓库":
                            {
                                var list = userInI.GetAllKeys("个人仓库");
                                int total = list.Count;
                                int recordPage = (total + pageSize - 1) / pageSize;// 获取仓库页数
                                //if (page > recordPage) page = recordPage;// 如果跳转的夜数大于总页数就等于总页数
                                if (page > recordPage) page = 1;// 如果跳转的夜数大于总页数就等于总页数
                                list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();// 对仓库数据分页
                                int i = 0, humX = 0, humY = 0, textX = 0, textY = 0, bgIndexX = 0, bgIndexY = 0;
                                using (Image imgMian = Image.FromFile(userImgPath))
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

                                            #region 物品框
                                            using (Image img = Image.FromFile(newopUIPath + "001909.png"))// 物品框
                                            {
                                                gh.DrawImage(img, 0, 190, img.Width, img.Height);
                                            }
                                            #endregion

                                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, user.Name, fontB10, yellowBrush, rect);

                                            rect = new Rectangle(15, 30, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "您要找回什么物品呢？请看目录决定找回什么东西.", font9, whiteBrush, rect);
                                            rect = new Rectangle(15, 60, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "输入指令取回+编号,可以取出仓库里的物品.\r\n如：取回3 就是取出第3个物品,编号在格子右下角白色字母", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 160, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, $"托管物品列表({total}/99999)件,当前第{page}/{recordPage}页,共{recordPage}页.", font9, whiteBrush, rect);

                                            #region 仓库物品
                                            StditemDB stditemDB = new StditemDB();
                                            list.ForEach(s =>
                                            {
                                                stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("个人仓库", s));

                                                if (stditemDB != null)
                                                {
                                                    string newpath = itemsPath + stditemDB.StdImgPath;
                                                    using (Image img = Image.FromFile(newpath))
                                                    {
                                                        switch (i / 10)
                                                        {
                                                            case 0:
                                                                humX = (i * 37) - (img.Width / 2) + 36;
                                                                humY = 222 - (img.Height / 2);
                                                                textX = (i * 37) + 50;
                                                                textY = 220;
                                                                bgIndexX = (i * 37) + 18;
                                                                bgIndexY = 225;
                                                                break;
                                                            case 1:
                                                                humX = (i - 10) * 37 - (img.Width / 2) + 36;
                                                                humY = 255 - (img.Height / 2);
                                                                textX = (i - 10) * 37 + 50;
                                                                textY = 251;
                                                                bgIndexX = (i - 10) * 37 + 18;
                                                                bgIndexY = 260;
                                                                break;
                                                            case 2:
                                                                humX = (i - 20) * 37 - (img.Width / 2) + 36;
                                                                humY = 288 - (img.Height / 2);
                                                                textX = (i - 20) * 37 + 50;
                                                                textY = 283;
                                                                bgIndexX = (i - 20) * 37 + 18;
                                                                bgIndexY = 292;
                                                                break;
                                                            case 3:
                                                                humX = (i - 30) * 37 - (img.Width / 2) + 36;
                                                                humY = 321 - (img.Height / 2);
                                                                textX = (i - 30) * 37 + 50;
                                                                textY = 317;
                                                                bgIndexX = (i - 30) * 37 + 18;
                                                                bgIndexY = 325;
                                                                break;
                                                            case 4:
                                                                humX = (i - 40) * 37 - (img.Width / 2) + 36;
                                                                humY = 354 - (img.Height / 2);
                                                                textX = (i - 40) * 37 + 50;
                                                                textY = 351;
                                                                bgIndexX = (i - 40) * 37 + 18;
                                                                bgIndexY = 359;
                                                                break;
                                                            case 5:
                                                                humX = (i - 50) * 37 - (img.Width / 2) + 36;
                                                                humY = 387 - (img.Height / 2);
                                                                textX = (i - 50) * 37 + 50;
                                                                textY = 383;
                                                                bgIndexX = (i - 50) * 37 + 18;
                                                                bgIndexY = 392;
                                                                break;
                                                            case 6:
                                                                humX = (i - 60) * 37 - (img.Width / 2) + 36;
                                                                humY = 420 - (img.Height / 2);
                                                                textX = (i - 60) * 37 + 50;
                                                                textY = 417;
                                                                bgIndexX = (i - 60) * 37 + 18;
                                                                bgIndexY = 424;
                                                                break;
                                                            case 7:
                                                                humX = (i - 70) * 37 - (img.Width / 2) + 36;
                                                                humY = 453 - (img.Height / 2);
                                                                textX = (i - 70) * 37 + 50;
                                                                textY = 450;
                                                                bgIndexX = (i - 70) * 37 + 18;
                                                                bgIndexY = 457;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                                    }

                                                    i++;
                                                    // 背包位置
                                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font9, whiteBrush, rect);

                                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                                    {
                                                        rect = new Rectangle(textX, textY, 0, 0);
                                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font9, brushLimeGreen, rect);
                                                    }
                                                }
                                            });
                                            #endregion

                                            #region 添加背包
                                            i = 0;
                                            using (Image img = Image.FromFile(newopUIPath + "000002.png"))
                                            {
                                                gh.DrawImage(img, 557, 10, img.Width, img.Height);
                                            }

                                            //加载背包里的物品
                                            rect = new Rectangle(618, 290, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, user.User_General.JB.ToString(), fontBold8, whiteBrush, rect);//金币

                                            rect = new Rectangle(625, 325, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "  元宝：" + user.User_General.YB, fontBold8, whiteBrush, rect);//元宝 

                                            rect = new Rectangle(625, 345, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "金刚石：" + user.User_General.JGS, fontBold8, whiteBrush, rect);//金刚石

                                            List<string> listBwg = userInI.GetAllKeys("背包物品");


                                            listBwg.ForEach(itemKey =>
                                            {
                                                stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", itemKey));
                                                if (stditemDB != null)
                                                {
                                                    string newpath = itemsPath + stditemDB.StdImgPath;
                                                    using (Image img = Image.FromFile(newpath))   //加载背包里的物品
                                                    {
                                                        switch (i / 12)
                                                        {
                                                            case 0:
                                                                humX = (i * 35) - (img.Width / 2) + 594;
                                                                humY = 44 - (img.Height / 2) - 6;
                                                                textX = (i * 35) + 612;
                                                                textY = 32;
                                                                bgIndexX = (i * 35) + 575;
                                                                bgIndexY = 42;
                                                                break;
                                                            case 1:
                                                                humX = (i - 12) * 35 - (img.Width / 2) + 594;
                                                                humY = 77 - (img.Height / 2) - 6;
                                                                textX = (i - 12) * 35 + 612;
                                                                textY = 65;
                                                                bgIndexX = (i - 12) * 35 + 575;
                                                                bgIndexY = 75;
                                                                break;
                                                            case 2:
                                                                humX = (i - 24) * 35 - (img.Width / 2) + 594;
                                                                humY = 108 - (img.Height / 2) - 6;
                                                                textX = (i - 24) * 35 + 612;
                                                                textY = 98;
                                                                bgIndexX = (i - 24) * 35 + 575;
                                                                bgIndexY = 104;
                                                                break;
                                                            case 3:
                                                                humX = (i - 36) * 35 - (img.Width / 2) + 594;
                                                                humY = 139 - (img.Height / 2) - 6;
                                                                textX = (i - 36) * 35 + 612;
                                                                textY = 127;
                                                                bgIndexX = (i - 36) * 35 + 575;
                                                                bgIndexY = 137;
                                                                break;
                                                            case 4:
                                                                humX = (i - 48) * 35 - (img.Width / 2) + 594;
                                                                humY = 170 - (img.Height / 2) - 6;
                                                                textX = (i - 48) * 35 + 612;
                                                                textY = 160;
                                                                bgIndexX = (i - 48) * 35 + 575;
                                                                bgIndexY = 170;
                                                                break;
                                                            case 5:
                                                                humX = (i - 60) * 35 - (img.Width / 2) + 594;
                                                                humY = 204 - (img.Height / 2) - 6;
                                                                textX = (i - 60) * 35 + 612;
                                                                textY = 193;
                                                                bgIndexX = (i - 60) * 35 + 575;
                                                                bgIndexY = 202;
                                                                break;
                                                            case 6:
                                                                humX = (i - 72) * 35 - (img.Width / 2) + 594;
                                                                humY = 237 - (img.Height / 2) - 6;
                                                                textX = (i - 72) * 35 + 612;
                                                                textY = 226;
                                                                bgIndexX = (i - 72) * 35 + 575;
                                                                bgIndexY = 236;
                                                                break;
                                                            case 7:
                                                                humX = (i - 84) * 35 - (img.Width / 2) + 594;
                                                                humY = 270 - (img.Height / 2) - 6;
                                                                textX = (i - 84) * 35 + 612;
                                                                textY = 259;
                                                                bgIndexX = (i - 84) * 35 + 575;
                                                                bgIndexY = 269;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        gh.DrawImage(img, humX, humY, img.Width, img.Height);
                                                    }
                                                    i++;

                                                    // 背包位置
                                                    rect = new Rectangle(bgIndexX, bgIndexY, 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, i.ToString(), font9, whiteBrush, rect);

                                                    if (stditemDB.SubAttr1 > 0 && stditemDB.OverLap > 0)
                                                    {
                                                        rect = new Rectangle(textX, textY, 0, 0);
                                                        TextHelper.DrawRightStrokeString(gh, stditemDB.SubAttr1.ToString(), font9, brushLimeGreen, rect);
                                                    }
                                                }
                                            });
                                            #endregion


                                        }

                                        path = $"{UserInfoImgPath + user.UserQQ}.png";
                                        bitmap.Save(path, ImageFormat.Png);//保存图片
                                    }
                                }
                            }
                            break;
                        case "万事通买物品":
                            {
                                pageSize = 10;
                                string mud2Path = LegendFilePath.Mud2Path + "万事通.txt";
                                var list = FileHelper.ReadTxtReturnList(mud2Path);
                                int total = list.Count;
                                int recordPage = (total + pageSize - 1) / pageSize;// 获取仓库页数
                                //if (page > recordPage) page = recordPage;// 如果跳转的夜数大于总页数就等于总页数
                                if (page > recordPage) page = 1;// 如果跳转的夜数大于总页数就等于总页数
                                list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();// 对仓库数据分页
                                using (Image imgMian = Image.FromFile(userImgPath))
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

                                            #region 物品框
                                            using (Image img = Image.FromFile(prgusePath + "000385.png"))// 物品框
                                            {
                                                gh.DrawImage(img, 0, 190, img.Width, img.Height);
                                            }
                                            #endregion


                                            Rectangle rect = new Rectangle(15, 12, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, user.Name, fontB10, yellowBrush, rect);

                                            rect = new Rectangle(15, 30, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "你需要什么物品呢？", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 47, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "发送 上一页/下一页 可以进行翻页,输入买物品+物品名称#数字 ", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 64, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "发送 买物品+物品名称#数字", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 81, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "发送 买物品,数字 查看某一页物品", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 95, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "     如：买物品强效金创药#3 就是购买3瓶强效金创药", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 109, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "     注意：药买完了数量会自动恢复10,其它物品则不能购买", font9, whiteBrush, rect);

                                            rect = new Rectangle(15, 160, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, $"托管物品列表({total}/99999)件,当前第{page}/{recordPage}页,共{recordPage}页.", font9, whiteBrush, rect);

                                            rect = new Rectangle(45, 201, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "物品列表", font9, whiteBrush, rect);

                                            rect = new Rectangle(180, 201, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "价格", font9, whiteBrush, rect);

                                            rect = new Rectangle(247, 201, 0, 0);
                                            TextHelper.DrawLeftStrokeString(gh, "数量", font9, whiteBrush, rect);

                                            List<StditemDB> stdList = LegendApi.GetStdItemsList();

                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                string itemName = list[i].Split(',')[0];
                                                StditemDB std = stdList.Where(s => s.StdName == itemName).FirstOrDefault();
                                                if (std == null)
                                                {
                                                    continue;
                                                }

                                                int price = std.Price * 2;
                                                int qty = int.Parse(list[i].Split(',')[1]);

                                                rect = new Rectangle(15, 218 + (i * 14), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, itemName, font9, whiteBrush, rect);

                                                rect = new Rectangle(155, 218 + (i * 14), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, price.ToString(), font9, whiteBrush, rect);

                                                rect = new Rectangle(245, 218 + (i * 14), 0, 0);
                                                TextHelper.DrawLeftStrokeString(gh, qty.ToString(), font9, whiteBrush, rect);
                                            }


                                        }
                                        path = $"{UserInfoImgPath + user.UserQQ}.png";
                                        bitmap.Save(path, ImageFormat.Png);//保存图片
                                    }
                                }
                            }
                            break;
                        case "个人技能":
                            {
                                pageSize = 6;
                                var list = userInI.GetAllKeys(store);
                                int total = list.Count;
                                int recordPage = (total + pageSize - 1) / pageSize;// 获取仓库页数
                                //if (page > recordPage) page = recordPage;// 如果跳转的夜数大于总页数就等于总页数
                                if (page > recordPage) page = 1;// 如果跳转的夜数大于总页数就等于总页数
                                list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();// 对仓库数据分页
                                using (Image imgMian = Image.FromFile(userImgPath))
                                {
                                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                                    {
                                        using (Graphics gh = Graphics.FromImage(bitmap))
                                        {
                                            gh.CompositingQuality = CompositingQuality.HighQuality;
                                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                            using (Image img = Image.FromFile(newopUIPath + "000091.png"))// 技能
                                            {
                                                gh.DrawImage(img, 680, 0, img.Width, img.Height);
                                            }
                                            Font font12 = new Font("黑体", 12);
                                            Font font10 = new Font("黑体", 10);
                                            Color color = ColorTranslator.FromHtml("#ffffff");
                                            SolidBrush brush = new SolidBrush(color);
                                            Rectangle rect = new Rectangle(750, 60, 200, 20);
                                            TextHelper.DrawCenterStrokeString(gh, user.Name, font12, brush, rect);//用户名

                                            List<MagicDB> magicDBList = LegendApi.GetMagicList();
                                            MagicDB magicDB = null;

                                            UserSkill us;
                                            string magIcon2Path = LegendIMGDataPath + "MagIcon2\\";
                                            string ui1Path = LegendIMGDataPath + "ui1\\";
                                            string skill = string.Empty, skillPath = magIcon2Path;
                                            int skillX = 731, skillY = 140, i = 0;
                                            int skNameX = 768, skNameY = 138;
                                            int lvX = 860, lvY = 138;
                                            int sksldX = 770, sksldY = 157;
                                            int skLvX = 900, skLvY = 140;
                                            int sksld2X = 825, sksld2Y = 158;
                                            int isckSkillX = 940, isckSkillY = 145;

                                            list.ForEach(sk =>
                                            {
                                                if (i > 5) return;
                                                skill = userInI.Read("个人技能", sk);
                                                us = Api.JsonDeserialize<UserSkill>(skill);
                                                if (us != null)
                                                {
                                                    skillPath = magIcon2Path + us.US_Img;
                                                    using (Image img = Image.FromFile(skillPath))// 技能
                                                    {
                                                        gh.DrawImage(img, skillX, skillY + (i * 46), img.Width, img.Height);
                                                    }
                                                    rect = new Rectangle(skNameX, skNameY + (i * 46), 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, us.US_Name, font12, brush, rect);//技能名称

                                                    skillPath = ui1Path + "000920.png";
                                                    using (Image img = Image.FromFile(skillPath))// 技能等级
                                                    {
                                                        gh.DrawImage(img, lvX, lvY + (i * 46), img.Width, img.Height);
                                                    }
                                                    rect = new Rectangle(skLvX, skLvY + (i * 46), 0, 0);
                                                    TextHelper.DrawLeftStrokeString(gh, us.US_Level.ToString(), font10, brush, rect);// 技能等级

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
                                                                TextHelper.DrawLeftStrokeString(gh, us.US_NeedLN1 + "/" + magicDB.NeedLN1, font10, brush, rect);
                                                            }
                                                            break;
                                                        case 1:
                                                            if (us.US_NeedLN2 < magicDB.NeedLN2)
                                                            {
                                                                rect = new Rectangle(sksld2X, sksld2Y + (i * 46), 0, 0);
                                                                TextHelper.DrawLeftStrokeString(gh, us.US_NeedLN2 + "/" + magicDB.NeedLN2, font10, brush, rect);
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
                                                                TextHelper.DrawLeftStrokeString(gh, us.US_NeedLN3 + "/" + magicDB.NeedLN3, font10, brush, rect);
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

                                        }

                                        path = $"{UserInfoImgPath + user.UserQQ}.png";
                                        bitmap.Save(path, ImageFormat.Png);//保存图片
                                    }
                                }
                            }
                            break;
                    }

                    userInI.Write("人物信息", "所在页面", page.ToString());
                }

                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 金币换金条
        /// </summary>
        public static string GetTigToGold(UserInfo user, int qty, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                string msg = "";
                int money = qty * 1000000;
                if (user.User_General.JB > money && money > 0)
                {
                    user.User_General.JB -= money;
                    userInI.Write("普通数据", "金币", user.User_General.JB.ToString());

                    List<StditemDB> stdList = LegendApi.GetStdItemsList();

                    for (int i = 0; i < qty; i++)
                    {
                        StditemDB std = stdList.Where(s => s.StdName == "金条").First();
                        std.StdItemMap = user.Map;
                        std.StdItemOwner = user.Name;
                        std.StdItemStuff = "NPC制造";
                        std.StdItemTime = DateTime.Now.ToString();
                        std.SubAttr1 = 1;
                        userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(std));
                    }
                }
                else
                {
                    msg = "年轻人,你身上的钱不够,还是出去多打打装备来卖钱吧!";
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true);

                using (Image imgMian = Image.FromFile(userImgPath))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            Font font14 = new Font("黑体", 14);
                            Font fontBold14 = new Font("黑体", 14, FontStyle.Bold);
                            Brush whiteBrush = new SolidBrush(Color.White);
                            Brush yellowBrush = new SolidBrush(Color.Yellow);
                            Rectangle rect = new Rectangle(0, 0, 0, 0);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                #region 对话框
                                using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                {
                                    gh.DrawImage(img, 0, 0, img.Width, img.Height);
                                }
                                #endregion

                                rect = new Rectangle(15, 12, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, user.Name, fontBold14, yellowBrush, rect);

                                rect = new Rectangle(15, 30, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, msg, font14, whiteBrush, rect);
                            }
                            else
                            {
                                for (int i = 0; i < qty; i++)
                                {
                                    rect = new Rectangle(20, (i * 15) + 30, 0, 0);
                                    TextHelper.DrawLeftStrokeString(gh, "金条 被发现", font14, yellowBrush, rect);
                                }
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
        /// 金条换金币
        /// </summary>
        public static string GetGoldToTig(UserInfo user, int qty, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                var list = userInI.GetAllKeys("背包物品");
                StditemDB stditem = null;
                string okey = "";
                foreach (var key in list)
                {
                    var std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));

                    if (std.StdName == "金条")
                    {
                        if (stditem != null)
                            stditem.SubAttr1 += std.SubAttr1;
                        else
                            stditem = std;

                        okey += key + ",";
                    }
                }
                string msg = "";
                if (stditem == null)
                    msg = "卧槽!年轻人,你身上没有金条,兑换失败哦。";
                else
                {
                    if (qty > stditem.SubAttr1) msg = "对不起，兑换的数量大于金条数量,你最多只能兑换" + stditem.SubAttr1 + "根金条。";
                }


                int money = qty * 1000000;
                if (string.IsNullOrEmpty(msg))
                {
                    user.User_General.JB += money;
                    userInI.Write("普通数据", "金币", user.User_General.JB.ToString());

                    stditem.SubAttr1 -= qty;
                    foreach (var key in okey.TrimEnd(',').Split(','))
                    {
                        userInI.Delete("背包物品", key);
                    }

                    if (stditem.SubAttr1 > 0)
                        userInI.Write("背包物品", userInI.GetRndId(), Api.JsonSerialize(stditem));
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GamgeMainForm(user, false, true);

                using (Image imgMian = Image.FromFile(userImgPath))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);

                            Brush brushLimeGreen = new SolidBrush(Color.Lime);
                            Font font14 = new Font("黑体", 14);
                            Font fontB14 = new Font("黑体", 14, FontStyle.Bold);
                            Brush whiteBrush = new SolidBrush(Color.White);
                            Brush yellowBrush = new SolidBrush(Color.Yellow);
                            Rectangle rect = new Rectangle(0, 0, 0, 0);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                #region 对话框
                                using (Image img = Image.FromFile(prgusePath + "000384.png"))// 对话框
                                {
                                    gh.DrawImage(img, 0, 0, img.Width, img.Height);
                                }
                                #endregion

                                rect = new Rectangle(15, 12, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, user.Name, fontB14, yellowBrush, rect);

                                rect = new Rectangle(15, 30, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, msg, font14, whiteBrush, rect);
                            }
                            else
                            {
                                rect = new Rectangle(20, 30, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, money + "金币 被发现", font14, yellowBrush, rect);
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

    }
}
