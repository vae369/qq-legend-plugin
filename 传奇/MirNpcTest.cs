using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 传奇
{
    public partial class MirNpcTest : Form
    {
        public MirNpcTest()
        {
            InitializeComponent();
        }

        private void BindImage(string path)
        {
            //让pictureBox显示图像,并且不被占用,能即时删除图片
            Image imgModel = Image.FromFile(path);   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            imgModel.Save(mstr, ImageFormat.Gif);// 保存这个对象
            pictureBox1.Image = Image.FromStream(mstr);//显示
            imgModel.Dispose();//释放占用
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Map != "盟重土城") throw new Exception("起步礼包需要在盟重土城才能查看,请先前往盟重土城.");
                user.UserImagePath = LegendFilePath.LegendIMGDataPath + "地图\\4\\盟重土城\\4_12.jpg";
                string path = NPCApi.GetNPCByName(user, "起步礼包", x, y, w, h);
                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Map != "盟重土城") throw new Exception("起步礼包需要在盟重土城才能查看,请先前往盟重土城.");
                string msg = "礼包码666666";
                msg = msg.Substring(3);
                msg = NPCApi.GetGifByCode(user, msg);
                string path = NPCApi.SendMessageImage(user, msg, x, y, w, h);
                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);

                string path = NPCApi.GetNPCByName(user, "会员特权", x, y, w, h);
                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                string msg = "设置普通自动吃药90";
                msg = "设置百分比自动吃药60";
                string type = "";
                int num = 0;
                if (msg.StartsWith("设置普通自动吃药"))
                {
                    type = "普通吃药";
                    int.TryParse(msg.Replace("设置普通自动吃药", ""), out num);
                }
                if (msg.StartsWith("设置百分比自动吃药"))
                {
                    type = "百分比吃药";
                    int.TryParse(msg.Replace("设置百分比自动吃药", ""), out num);
                }
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);

                UserInfoApi.SetAutoMedication(user, type, num);
                MessageBox.Show("设置成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Map != "盟重土城") throw new Exception("唯一赞助需要在盟重土城才能查看,请先前往盟重土城.");
                user.UserImagePath = LegendFilePath.LegendIMGDataPath + "地图\\4\\盟重土城\\4_12.jpg";
                string path = NPCApi.GetNPCByName(user, "唯一赞助", x, y, w, h);
                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                #region 查看商铺
                //string msg = "查看商铺";
                //msg = "查看商铺补给";
                //msg = "查看商铺强化";
                //msg = "查看商铺好友";
                //msg = "查看商铺限量";
                //string path = NPCApi.GetShopByName(user, msg, x, y, w, h);

                //BindImage(path);
                #endregion


                #region 购买
                string msg = "购买#水晶#10000";
                var goods = msg.Split('#');
                if (goods.Length != 3) throw new Exception("指令错误,请重新再试!");
                string goodsName = msg.Split('#')[1];
                int goodsNum = int.Parse(msg.Split('#')[2]);
                if (goodsNum <= 0) throw new Exception("指令错误,请重新再试!");
                NPCApi.PurchaseGoods(user, goodsName, goodsNum);
                MessageBox.Show($"物品[{goodsName}]购买成功!");
                #endregion

                #region 赠送
                //string msg = "赠送#[@532050072]    #水晶#1";
                //var goods = msg.Split('#');
                //if (goods.Length != 4) throw new Exception("指令错误,请重新再试!");
                //string goodsName = msg.Split('#')[2];
                //string giftQQ = msg.Split('#')[1].Substring(2).Trim().TrimEnd(']');
                //int goodsNum = int.Parse(msg.Split('#')[3]);
                //if (goodsNum <= 0) throw new Exception("指令错误,请重新再试!");
                //NPCApi.GiftGoods(user, giftQQ, goodsName, goodsNum);
                //MessageBox.Show($"物品[{goodsName}]赠送成功!");
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                string msg = "赞助领取礼包1";
                //msg = "赞助领取礼包2";
                //msg = "赞助领取礼包3";
                //msg = "白嫖领取礼包1";
                //msg = "白嫖领取礼包2";
                msg = "白嫖领取礼包3";
                msg = NPCApi.GetGifByCode2(user, msg);
                string path = NPCApi.SendMessageImage(user, msg, x, y, w, h);
                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                string msg = "赞助升级会员";
                msg = "元宝升级会员";
                msg = NPCApi.VIPUpgrade(user, msg);
                string path = NPCApi.SendMessageImage(user, msg, x, y, w, h);
                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int a = 30 / 100;
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                #region 充值使者
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);

                //string path = NPCApi.GetNPCByName(user, "充值使者", x, y, w, h);
                //BindImage(path);
                #endregion


                #region 用户充值
                //string msg = "充值#[@745223613]#30";
                //int num = int.Parse(msg.Split('#')[2]);
                //string userQQ = msg.Split('#')[1];
                //userQQ = userQQ.Substring(userQQ.IndexOf('@') + 1).TrimEnd(']');
                //msg = UserInfoApi.UserRecharge(fromQQ, userQQ, num);
                //MessageBox.Show(msg);
                #endregion


                #region 领取充值
                //if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                //var list = UserInfoApi.UserClaimRecharge(user);
                //foreach (var msg1 in list)
                //{
                //    string path = NPCApi.SendMessageImage(user, msg1, x, y, w, h);
                //    MessageBox.Show(msg1);
                //}
                #endregion

                #region 交易
                //string msg = "交易#[@532050072] #8";
                //string qq = msg.Substring(5);
                //qq = qq.Substring(0, qq.IndexOf(']'));
                //int num = int.Parse(msg.Split('#')[2]) - 1;
                //string refMsg = "";
                //UserInfoApi.UserDeal(user, qq, num, ref refMsg);
                #endregion

                #region 调金币 元宝 等级
                string msg = "@金币#10";
                msg = "@金币#10#[@532050072]";
                string[] arr = msg.Split('#');
                if (arr.Length < 2) throw new Exception("参数错误!");

                string type = arr[0].TrimStart('@');
                int.TryParse(arr[1], out int qty);
                string toqq = fromQQ;
                if (arr.Length == 3) toqq = arr[2].Substring(2).Trim().TrimEnd(']');

                LegendApi.GiveJBYBLevel(type, toqq, qty);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                #region 查看永恒之力
                //UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                //if (user.Map != "盟重土城") throw new Exception("永恒之力需要在盟重土城才能查看,请先前往盟重土城.");
                //user.UserImagePath = LegendFilePath.LegendIMGDataPath + "地图\\4\\盟重土城\\4_2.jpg";
                //string path = NPCApi.GetNPCByName(user, "永恒之力", x, y, w, h);
                //BindImage(path);
                #endregion

                #region 开启永恒之力
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                if (user.Map != "盟重土城") throw new Exception("永恒之力需要在盟重土城才能查看,请先前往盟重土城.");
                string msg = NPCApi.GetONYHZL(user, x, y, w, h);
                string path = NPCApi.SendMessageImage(user, msg, x, y, w, h);
                BindImage(path);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                #region 查看公益捐献
                //UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                //if (user.Map != "盟重土城") throw new Exception("公益捐献需要在盟重土城才能查看,请先前往盟重土城.");
                //user.UserImagePath = LegendFilePath.LegendIMGDataPath + "地图\\4\\盟重土城\\4_3.jpg";
                //string path = NPCApi.GetNPCByName(user, "公益捐献", x, y, w, h);
                //BindImage(path);
                #endregion

                #region 开启公益捐献
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                if (user.Map != "盟重土城") throw new Exception("开启公益捐献需要在盟重土城才能查看,请先前往盟重土城.");
                string msg = NPCApi.GetONGYJX(user);
                string path = NPCApi.SendMessageImage(user, msg, x, y, w, h);
                BindImage(path);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            Random rnd = new Random();
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");

                string path = string.Empty, msg = string.Empty;
                switch (comboBox1.Text)
                {
                    case "装备回收": path = StoreApi.GetEquRecycle(user, comboBox1.Text, x, y, w, h); break;
                    case "查看":
                        //path = StoreApi.GetEquRecycle(user, "查看垃圾系列", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看沃玛系列", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看祖玛系列", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看赤月系列", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看永恒首饰", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看永恒剑甲", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看毁灭首饰", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看毁灭剑甲", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看刑天首饰", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看焚天首饰", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看沧澜首饰", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看刑焚沧甲", x, y, w, h);
                        //path = StoreApi.GetEquRecycle(user, "查看刑焚沧剑", x, y, w, h);
                        path = StoreApi.GetEquRecycle(user, "查看特殊物品", x, y, w, h);
                        break;
                    case "勾选":
                        msg = "勾选垃圾系列";
                        msg = "勾选赤月系列";
                        msg = "勾选初级时装";
                        msg = "勾选灭世剑甲";
                        //msg = "勾选自动回收";
                        msg = "全部勾选";
                        msg = "勾选特殊物品";
                        StoreApi.CheckedEquRecycle(user, msg);
                        path = StoreApi.GetEquRecycle(user, "装备回收", x, y, w, h);
                        //path = StoreApi.CheckedEquRecycle(user, "沃玛系列", 48, 70, x, y, w, h);
                        break;
                    case "取消勾选":
                        msg = "取消勾选垃圾系列";
                        msg = "取消勾选赤月系列";
                        msg = "取消勾选初级时装";
                        //msg = "取消勾选灭世剑甲";
                        msg = "取消勾选自动回收";
                        msg = "取消全部勾选";
                        StoreApi.CheckedEquRecycle(user, msg);
                        path = StoreApi.GetEquRecycle(user, "装备回收", x, y, w, h);
                        //path = StoreApi.CheckedEquRecycle(user, "沃玛系列", 48, 70, x, y, w, h);
                        break;
                    case "一键回收":
                        if (StoreApi.EquRecycleByPb(user))
                        {
                            UserInfoImg userInfoImg = new UserInfoImg(user);
                            path = userInfoImg.GetBWGIMG(user);
                        }
                        break;



                }

                BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            try
            {
                #region 查看世外高人 高级宝石
                //UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                //if (user.Map != "盟重土城") throw new Exception("世外高人需要在盟重土城才能查看,请先前往盟重土城.");
                //user.UserImagePath = LegendFilePath.LegendIMGDataPath + "地图\\4\\盟重土城\\4_3.jpg";
                //string path = NPCApi.GetNPCByName(user, "世外高人", x, y, w, h);
                //string path = NPCApi.GetNPCByName(user, "灭世宝石", x, y, w, h);
                //string path = NPCApi.GetNPCByName(user, "灭世称号", x, y, w, h);
                //string path = NPCApi.GetNPCByName(user, "灭世神技", x, y, w, h);
                //string path = NPCApi.GetNPCByName(user, "高级宝石", x, y, w, h);
                //BindImage(path);
                #endregion

                #region 装备合成 升级
                //UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                //if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                //if (user.Map != "盟重土城") throw new Exception("合成灭世斗笠需要在盟重土城才能进行,请先前往盟重土城.");
                //string msg = NPCApi.SyntheticItems(user, "合成普通斗笠");
                //string msg = NPCApi.SyntheticItems(user, "合成赤月斗笠");
                //string msg = NPCApi.SyntheticItems(user, "合成永恒斗笠");
                //string msg = NPCApi.SyntheticItems(user, "合成毁灭斗笠");
                //string msg = NPCApi.SyntheticItems(user, "合成沧天斗笠");
                //string msg = NPCApi.SyntheticItems(user, "合成灭世斗笠");
                //string msg = NPCApi.SyntheticItems(user, "升级宝石10"); 
                //MessageBox.Show(msg);
                #endregion

                #region 提升称号
                //UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                //if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                //if (user.Map != "盟重土城") throw new Exception("提升称号需要在盟重土城才能进行,请先前往盟重土城.");

                //LegendApi.GetUpgradeTitle(user, "升级永恒白金");

                //UserInfoImg userInfoImg = new UserInfoImg(user);
                //string userImgPath = userInfoImg.GetUserTitleIMG(user, 0, x, y, w, h);
                //BindImage(userImgPath);
                #endregion


                #region 提升神技
                UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
                if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                if (user.Map != "盟重土城") throw new Exception("提升神技需要在盟重土城才能进行,请先前往盟重土城.");

                //string msg = MagicApiImg.GetAcquiringSkills(user, "提升神技");
                string msg = MagicApiImg.GetAcquiringSkills(user, "领取大彻大悟");
                MessageBox.Show(msg);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
            Random rnd = new Random();
            string path = "";
            try
            {
                #region 查看二大陆
                if (user.Map != "盟重土城") throw new Exception("二大陆的地图在盟重土城才能查看,请先前往盟重土城.");
                user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(3, 8)}.jpg";
                path = NPCApi.GetNPCByName(user, "二大陆", x, y, w, h);

                #endregion

                #region 前往二大陆及打怪地图
                //path = MapApi.GotoCityMap(user, "二大陆", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "虚空无尽", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "缥缈之巅", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "幽暗村庄", 0, x, y, w, h, 0);
                #endregion

                #region 二大陆NPC
                //if (user.Map != "天宫仙境") throw new Exception("虚空无尽需要在天宫仙境才能查看,请先前往天宫仙境(二大陆).");
                //if (user.Map != "天宫仙境" && user.Map != "盟重土城") throw new Exception("三大陆的地图在天宫仙境才能查看,请先前往天宫仙境(二大陆) 或 回盟重.");
                //path = NPCApi.GetNPCByName(user, "虚空无尽", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "缥缈之巅", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "幽暗村庄", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "三大陆", x, y, w, h);
                #endregion

                if (!string.IsNullOrEmpty(path))
                    BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
            Random rnd = new Random();
            string path = "";
            try
            {
                #region 查看三大陆
                //if (user.Map != "盟重土城") throw new Exception("三大陆的地图在盟重土城才能查看,请先前往盟重土城.");
                //user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(3, 8)}.jpg";
                //path = NPCApi.GetNPCByName(user, "三大陆", x, y, w, h);
                #endregion

                #region 前往三大陆及打怪地图
                path = MapApi.GotoCityMap(user, "三大陆", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "一转圣地", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "三转圣地", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "五转圣地", 0, x, y, w, h, 0);
                #endregion

                #region 三大陆NPC
                //if (user.Map != "沙漠荒原") throw new Exception("虚空无尽需要在沙漠荒原才能查看,请先前往沙漠荒原(三大陆).");
                //if (user.Map != "沙漠荒原" && user.Map != "盟重土城") throw new Exception("三大陆的地图在沙漠荒原才能查看,请先前往沙漠荒原(三大陆) 或 回盟重.");
                //path = NPCApi.GetNPCByName(user, "一转圣地", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "三转圣地", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "五转圣地", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "转生使者", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "我佛慈悲", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "三大神器合成", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "首饰盒神技", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "四大陆", x, y, w, h);
                #endregion

                #region 转生使者
                //if (user.Map != "沙漠荒原") throw new Exception("需要在沙漠荒原才能进行转生,请先前往沙漠荒原(三大陆).");
                //if (LegendApi.UserRenewLevel(user, 5))
                //    MessageBox.Show("转生成功！");
                #endregion

                #region 成佛_走火入魔
                //if (user.Map != "沙漠荒原") throw new Exception("需要在沙漠荒原才能进行转生,请先前往沙漠荒原(三大陆).");
                //string res = LegendApi.GetUserUpgradeGod(user);
                //MessageBox.Show(res);
                #endregion

                #region 三大神器合成
                //if (user.Map != "沙漠荒原") throw new Exception("需要在沙漠荒原才能进行转生,请先前往沙漠荒原(三大陆).");
                //string msg = NPCApi.SyntheticItems(user, "合成神器圣战");
                #endregion


                if (!string.IsNullOrEmpty(path))
                    BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            string fromQQ = txtQQ.Text;
            UserInfo user = UserInfoApi.GetInfoByQQ(fromQQ);
            Random rnd = new Random();
            string path = "";
            try
            {
                #region 查看四大陆
                //if (user.Map != "盟重土城") throw new Exception("三大陆的地图在盟重土城才能查看,请先前往盟重土城.");
                //user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(3, 8)}.jpg";
                //path = NPCApi.GetNPCByName(user, "天帝陵墓", x, y, w, h);
                #endregion

                #region 前往四大陆及打怪地图
                //path = MapApi.GotoCityMap(user, "四大陆", 0, x, y, w, h, 0);

                //path = MapApi.GotoNormalXianEMap(user, "天帝陵墓2", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "天帝炼狱3", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "天帝足球场1", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "天帝后花园2", 0, x, y, w, h, 0);
                //path = MapApi.GotoNormalXianEMap(user, "天帝奴隶场3", 0, x, y, w, h, 0);
                #endregion

                #region 四大陆NPC
                //if (user.Map != "大帝之城") throw new Exception("虚空无尽需要在大帝之城才能查看,请先前往大帝之城(四大陆).");
                //if (user.Map != "大帝之城" && user.Map != "盟重土城") throw new Exception("三大陆的地图在大帝之城才能查看,请先前往大帝之城(四大陆) 或 回盟重.");
                //path = NPCApi.GetNPCByName(user, "仙庭战鼓", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "情缘仙人", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "天帝陵墓", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "天帝炼狱", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "天帝足球场", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "天帝后花园", x, y, w, h);
                //path = NPCApi.GetNPCByName(user, "天帝奴隶场", x, y, w, h);

                //path = NPCApi.GetNPCByName(user, "五大陆", x, y, w, h);
                #endregion

                #region 合成
                //if (user.Map != "大帝之城") throw new Exception("需要在大帝之城才能进行转生,请先前往大帝之城(四大陆).");
                //string msg = NPCApi.SyntheticItems(user, "合成毁灭军鼓");
                //MessageBox.Show(msg);
                #endregion


                if (!string.IsNullOrEmpty(path))
                    BindImage(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //获取宣传码
            string msg = LegendApi.GetXCM();
            MessageBox.Show(msg);
        }
    }
}
