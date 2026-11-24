using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using 传奇.core;
using 传奇.Models;

namespace 传奇
{
    /// <summary>
    /// 奖励类
    /// </summary>
    public class PrizeApi
    {
        static string newopUIPath = LegendFilePath.LegendIMGDataPath + "NewopUI\\";
        /// <summary>
        /// 查看装备首爆奖励图
        /// </summary>
        public static string GetFristPrizeImg(UserInfo user, int pageNum, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true, true, x, y, w, h, lv);

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            using (Image img = Image.FromFile(newopUIPath + "001907.png"))// 奖励背景图
                            {
                                gh.DrawImage(img, 220, 100, img.Width, img.Height);
                            }

                            Font font12 = new Font("黑体", 12);
                            Rectangle rect = new Rectangle(0, 0, 0, 0);
                            SolidBrush brushWhite = new SolidBrush(Color.NavajoWhite);
                            SolidBrush brushBlue = new SolidBrush(Color.Lime);
                            SolidBrush brushViolet = new SolidBrush(Color.PaleVioletRed);
                            SolidBrush brushGreen = new SolidBrush(Color.LawnGreen);
                            SolidBrush brushRed = new SolidBrush(Color.Red);
                            SolidBrush brushYellow = new SolidBrush(Color.Yellow);

                            var list = StdItemDropTip.GetStdItems();
                            int total = (list.Count + 10 - 1) / 10;
                            list = list.Skip(10 * (pageNum - 1)).Take(10).ToList();

                            int yy = 25;
                            for (int i = 0; i < list.Count; i++)
                            {
                                rect = new Rectangle(276, 220 + (i * yy), 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, "No." + (i + 10 * (pageNum - 1) + 1), font12, brushWhite, rect);// 编号

                                rect = new Rectangle(392, 220 + (i * yy), 0, 0);
                                if (list[i].IsCollect == "已领取")
                                    TextHelper.DrawCenterStrokeString(gh, list[i].StdName, font12, brushRed, rect);// 物品名称
                                else
                                    TextHelper.DrawCenterStrokeString(gh, list[i].StdName, font12, brushBlue, rect);// 物品名称

                                rect = new Rectangle(563, 220 + (i * yy), 0, 0);
                                if (list[i].IsCollect == "已领取")
                                    TextHelper.DrawCenterStrokeString(gh, $"{list[i].JB}/{list[i].YB}/{list[i].AcQty}/{list[i].QQHB}", font12, brushRed, rect);// 金币 / 元宝 / 活跃值 / 红包
                                else
                                    TextHelper.DrawCenterStrokeString(gh, $"{list[i].JB}/{list[i].YB}/{list[i].AcQty}/{list[i].QQHB}", font12, brushViolet, rect);// 金币 / 元宝 / 活跃值 / 红包

                                rect = new Rectangle(740, 220 + (i * yy), 0, 0);
                                if (list[i].IsCollect == "已领取")
                                    TextHelper.DrawCenterStrokeString(gh, list[i].DropUserName, font12, brushRed, rect);// 玩家名称
                                else
                                    TextHelper.DrawCenterStrokeString(gh, list[i].IsCollect, font12, brushGreen, rect);// 玩家名称
                            }

                            rect = new Rectangle(640, 463, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "当前第      页", font12, brushYellow, rect);// 页数

                            rect = new Rectangle(698, 471, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, pageNum.ToString(), font12, brushWhite, rect);// 页数

                            rect = new Rectangle(760, 463, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "共   页", font12, brushYellow, rect);// 页数

                            rect = new Rectangle(783, 471, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, total.ToString(), font12, brushWhite, rect);// 页数

                           
                            path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
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
        /// 查看怪物首杀奖励图
        /// </summary>
        public static string GetFristKillImg(UserInfo user, int pageNum, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true, true, x, y, w, h, lv);

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.CompositingQuality = CompositingQuality.HighQuality;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            using (Image img = Image.FromFile(newopUIPath + "001908.png"))// 奖励背景图
                            {
                                gh.DrawImage(img, 220, 100, img.Width, img.Height);
                            }

                            Font font12 = new Font("黑体", 12);
                            Rectangle rect = new Rectangle(0, 0, 0, 0);
                            SolidBrush brushWhite = new SolidBrush(Color.NavajoWhite);
                            SolidBrush brushBlue = new SolidBrush(Color.Lime);
                            SolidBrush brushViolet = new SolidBrush(Color.PaleVioletRed);
                            SolidBrush brushGreen = new SolidBrush(Color.LawnGreen);
                            SolidBrush brushRed = new SolidBrush(Color.Red);
                            SolidBrush brushYellow = new SolidBrush(Color.Yellow);

                            var list = FirstKillMon.GetFirstKillMons();
                            int total = (list.Count + 10 - 1) / 10;

                            list = list.Skip(10 * (pageNum - 1)).Take(10).ToList();

                            int yy = 25;
                            for (int i = 0; i < list.Count; i++)
                            {
                                rect = new Rectangle(270, 220 + (i * yy), 0, 0);
                                TextHelper.DrawCenterStrokeString(gh, "No." + (i + 10 * (pageNum - 1) + 1), font12, brushWhite, rect);// 编号

                                rect = new Rectangle(340, 220 + (i * yy), 0, 0);
                                if (list[i].IsKill == "已击杀")
                                    TextHelper.DrawCenterStrokeString(gh, list[i].MonsterName, font12, brushRed, rect);// 怪物名称
                                else
                                    TextHelper.DrawCenterStrokeString(gh, list[i].MonsterName, font12, brushGreen, rect);// 怪物名称

                                rect = new Rectangle(450, 220 + (i * yy), 0, 0);
                                if (list[i].IsKill == "已击杀")
                                    TextHelper.DrawCenterStrokeString(gh, list[i].DeadTime, font12, brushRed, rect);// 击杀时间
                                else
                                    TextHelper.DrawCenterStrokeString(gh, "暂无", font12, brushGreen, rect);// 击杀时间
                                
                                rect = new Rectangle(565, 220 + (i * yy), 0, 0);
                                if (list[i].IsKill == "已击杀")
                                    TextHelper.DrawCenterStrokeString(gh, list[i].MonsterMap, font12, brushRed, rect);// 所在地图
                                else
                                    TextHelper.DrawCenterStrokeString(gh, "暂无", font12, brushGreen, rect);// 所在地图

                                rect = new Rectangle(665, 220 + (i * yy), 0, 0);
                                if (list[i].IsKill == "已击杀")
                                    TextHelper.DrawCenterStrokeString(gh, $"{list[i].JB}/{list[i].YB}/{list[i].AcQty}/{list[i].QQHB}", font12, brushRed, rect);// 金币 / 元宝 / 活跃值 / 红包
                                else
                                    TextHelper.DrawCenterStrokeString(gh, $"{list[i].JB}/{list[i].YB}/{list[i].AcQty}/{list[i].QQHB}", font12, brushGreen, rect);// 金币 / 元宝 / 活跃值 / 红包

                                rect = new Rectangle(775, 220 + (i * yy), 0, 0);
                                if (list[i].IsKill == "已击杀")
                                    TextHelper.DrawCenterStrokeString(gh, list[i].UserName, font12, brushRed, rect);// 玩家名称
                                else
                                    TextHelper.DrawCenterStrokeString(gh, list[i].IsKill, font12, brushGreen, rect);// 玩家名称
                            }

                            rect = new Rectangle(640, 463, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "当前第      页", font12, brushYellow, rect);// 页数
                            
                            rect = new Rectangle(697, 471, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, pageNum.ToString(), font12, brushWhite, rect);// 页数

                            rect = new Rectangle(760, 463, 0, 0);
                            TextHelper.DrawLeftStrokeString(gh, "共   页", font12, brushYellow, rect);// 页数

                            rect = new Rectangle(783, 471, 0, 0);
                            TextHelper.DrawCenterStrokeString(gh, total.ToString(), font12, brushWhite, rect);// 页数

                            
                            path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
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
