using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 传奇.core;
using 传奇.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace 传奇
{
    public partial class MirTest : Form
    {
        public MirTest()
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
            try
            {
                #region 签到
                //UserInfo user = new UserInfo()
                //{
                //    Id = 0,
                //    Name = txtName.Text,
                //    UserQQ = txtQQ1.Text,
                //    RoleType = "玩家",
                //    IsOnCity = "是",
                //    User_Attribute = new UserAttribute(),
                //    User_General = new UserGeneral(),
                //    User_Items = new UserItems()
                //};
                //if (UserInfoApi.SignIn(user.UserQQ, ref user))
                //{
                //    MessageBox.Show($"签到成功!\r\n当前等级[{user.User_General.Lv}]\r\n当前元宝{user.User_General.CurtYB}/{user.User_General.YB}\r\n获得经验[{user.User_General.Exp}],经验{user.User_General.SumExp}/{user.User_General.NextExp}\r\n累计签到：{user.User_General.SignDays}天\r\n连续签到：{user.User_General.SignDay}天. ");
                //}
                //else
                //{
                //    MessageBox.Show("签到失败!您今天该是已签过到了吧.");
                //}
                #endregion


                #region 补签
                UserInfo user = UserInfoApi.GetInfoByQQ(txtQQ1.Text);
                if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");

                if (DateTime.Now.Day == user.UserCountersignatureTime.Day) throw new Exception("对不起,每天只能补签一次。");

                string res = UserInfoApi.UserCountersignature(user);
                MessageBox.Show(res);
                #endregion
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
            int lv = int.Parse(txtLv.Text);

            string FromQQ = txtQQ2.Text;
            UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);

            UserInfoImg userInfoImg = new UserInfoImg(user);
            string userImgPath = userInfoImg.GetSignInImg(user, x, y, w, h, lv);
            BindImage(userImgPath);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string FromQQ = txtQQ2.Text;
                string[] arrUser = txtRegisterMsg.Text.Substring(3).Replace(",", ",").Split(',');
                if (arrUser.Length < 3) throw new Exception("注册格式错误!\n如：注册 枯木大师,战士,男(每个qq号只能注册一次,所有群通用)");

                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id > 0)
                {
                    //throw new Exception("对不起,您已经注册过游戏了!请勿重复注册.");
                }
                user.Name = arrUser[0].Trim();
                user.Job = arrUser[1].Trim();
                user.Sex = arrUser[2].Trim();
                if (LegendApi.Register(user))
                {
                    int x = int.Parse(txtx.Text);
                    int y = int.Parse(txty.Text);
                    int w = int.Parse(txtw.Text);
                    int h = int.Parse(txth.Text);
                    int lv = int.Parse(txtLv.Text);
                    UserInfoImg userInfoImg = new UserInfoImg(user);
                    string userImgPath = userInfoImg.GetRegisterImg(user, x, y, w, h, lv);
                    BindImage(userImgPath);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string formQQ = txtQQ2.Text;

            try
            {
                //UserInfo user = DrawHelper.GetInfoByQQ(formQQ);
                //// 如果用户只签到没有注册就进行注册,否则提示已注册
                //if (user.Id == 0)
                //{
                //    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                //}
                if (UserInfoApi.ClearAllUserInfo(formQQ))
                {
                    MessageBox.Show("清理成功!");
                }
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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoApi.RefreshOverlayStdItems(user);

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GetBWGIMG(user, lv, x, y, w, h);
                BindImage(userImgPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {

            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                Random rnd = new Random();
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                string msg = "打怪"; 
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0) throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                int killCount = 0;
                string mon = msg.Split('#')[0].Replace("打怪", "");
                if (msg.IndexOf('#') > 0)
                {
                    int.TryParse(msg.Split('#')[1], out killCount);
                    if (killCount > 10) killCount = 10;
                }
                else
                {
                    if (mon == "专属BOSS") killCount = rnd.Next(1, user.User_VIPAttribute.AutoBoos + 1);
                }


                if (mon == "专属BOSS" && killCount > user.User_VIPAttribute.AutoBoos)
                {
                    throw new Exception($"您当前的会员等级每天最多只能打{user.User_VIPAttribute.AutoBoos}只专属BOSS.");
                }
                if (mon == "专属BOSS" && user.User_VIPAttribute.AutoBoos <= user.User_VIPAttribute.AutoPBoos)
                {
                    throw new Exception($"对不起,您当天的专属BOSS已打完,想打更多请提升会员等级吧.");
                }

                int reCount = int.Parse(config.Read("配置信息", "战斗记录回合数"));
                var monBossList = MonBoss.GetMonBossByTXT();

                user = MonsterApi.GetPlayerAttackMonster(user, mon, killCount, reCount, monBossList);
                UserInfoApi.RefreshOverlayStdItems(user);//叠加物品

                var firstKillMonList = FirstKillMon.GetFirstKillMons();
                string supNotifyMsg = "", supNotifyMsg2 = "";

                var dropStdList = StdItemDropTip.GetStdItems();
                List<string> dropStdMsgList = new List<string>();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("信息如下：");
                Dictionary<string, int> itemDic = new Dictionary<string, int>();
                foreach (var monster in user.KillMons)
                {
                    if (monster.MonItemList != null)
                    {
                        sb.Append($"怪物：{monster.MonName}\t");
                        int exp = monster.Exp + (int)(monster.Exp * (user.User_VIPAttribute.MonExpInc / 100.0));
                        sb.Append($"经验：{exp} ");

                        if (monster.MonItemList.Count > 0)
                        {
                            var iList = monster.MonItemList.GroupBy(s => s).Select(s => new { ItemName = s.Key.StdName, Count = s.Count() }).ToList();
                            foreach (var item in iList)
                            {
                                if (!itemDic.ContainsKey(item.ItemName)) itemDic.Add(item.ItemName, item.Count);
                                else itemDic[item.ItemName] += item.Count;
                            }
                        }

                        // 取第一个击杀boss的信息
                        if (string.IsNullOrEmpty(supNotifyMsg) && mon != "专属BOSS")
                            supNotifyMsg = FirstKillMon.FirstKillMons(user, firstKillMonList, monster.MonName, user.Map);

                        sb.AppendLine();
                    }
                    else
                    {
                        sb.AppendLine("哦豁,你被怪物[" + monster.MonName + "]杀害了.");
                        sb.AppendLine("你已回到城中,HP已恢复,MP已恢复.");
                    }
                }
                sb.Append($"掉落装备：");
                foreach (var item in itemDic)
                {
                    supNotifyMsg2 = StdItemDropTip.SetStdItems(user, dropStdList, item.Key, user.Map);
                    if (!string.IsNullOrEmpty(supNotifyMsg2))
                        dropStdMsgList.Add(supNotifyMsg2);

                    sb.Append($"{item.Key} +{item.Value},");
                }
                sb.Remove(sb.Length - 1, 1);

                MessageBox.Show(sb.ToString());
                if (!string.IsNullOrEmpty(supNotifyMsg)) MessageBox.Show(supNotifyMsg);
                foreach (var item in dropStdMsgList) MessageBox.Show(item);


                if (user.IsSendMsgImg == "是")
                {
                    UserInfoImg userInfoImg = new UserInfoImg(user);
                    string userImgPath = await userInfoImg.GetAttackMonster(user, x, y, w, h, lv);
                    BindImage(userImgPath);
                }

                //string userImgPath = userInfoImg.GetAttackMonsterIMG(user, x, y, w, h, lv);
                if (user.IsSendMsgByLv7 == "否" && user.User_General.Lv == 7)
                {
                    MessageBox.Show("你已经7级了,技能已自动学习,快去城外闯闯吧!");
                }

                if (user.PageQty <= 0)
                {
                    throw new Exception("对不起,您背包格子已满,地上的装备捡不起来,请先回城里卖点东西 或 回收一点装备吧.");
                }

                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                MessageBox.Show($"共花费{ts2.TotalSeconds}秒");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        List<string> listFile;
        private void button6_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = Application.StartupPath;
                folderBrowserDialog.Description = "选择文件夹";
                DialogResult dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    string folderPath = folderBrowserDialog.SelectedPath;
                    txtImagePath.Text = folderPath;
                    listFile = Directory.GetFiles(folderPath).ToList();
                    label12.Text = $"共 {listFile.Count} 张";
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Thread TMain = new Thread(new ThreadStart(Run));
            TMain.IsBackground = true;
            TMain.Start();
        }

        private void Run()
        {
            string name = txtImageName.Text;
            string path = txtImagePath.Text;
            var ns = name.Split('_');
            int n = int.Parse(ns[ns.Length - 1]);
            int num = 0;
            while (true)
            {
                num++;
                if (listFile.Count > 0)
                {
                    string newName = string.Join("_", ns);
                    string newPath = $"{path}\\{newName}.jpg";
                    File.Move(listFile[0], newPath);
                    n++;
                    ns[ns.Length - 1] = n + "";
                    listFile.RemoveAt(0);
                    this.Invoke((EventHandler)delegate { label11.Text = $"修改 {num} 张"; });
                }
                else
                {
                    break;
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }
                #region 补偿活跃值
                //string msg = "补偿活跃值[@745223613] #10";
                //string nMsg = msg.Substring(7);
                //if (nMsg.Length < 4) throw new Exception("对不起,用户未找到,请重新输入.");
                //string qq = nMsg.Substring(0, nMsg.IndexOf(']'));
                //int num = int.Parse(nMsg.Split('#')[1]);
                //if (UserInfoApi.SetAllUserQty(FromQQ, qq, num))
                //    MessageBox.Show($"成功补偿{num}点活跃值!");
                #endregion

                #region 取装备

                //if (UserInfoApi.TaskEquipment(user, "手镯", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "戒指", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "衣服", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "武器", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "项链", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "头盔", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "斗笠", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "鞋子", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "腰带", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "护身符", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                //if (UserInfoApi.TaskEquipment(user, "勋章", x, y, w, h, lv))
                //{
                //    MessageBox.Show($"装备已取下!输入 查看装备 或 查看属性 查看吧!");
                //}
                #endregion

                #region 看附近怪物
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GetNearbyMonsterIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);
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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GetEquipIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);

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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GetUserStatusIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);

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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GetUserAttributeIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);

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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GetUserSkillIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);

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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);

                string userImgPath = userInfoImg.GetUserFashionIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);

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
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                UserInfoImg userInfoImg = new UserInfoImg(user);
                string titleName = "永恒鞭尸";
                string userImgPath = userInfoImg.GetUserTitleIMG(user, lv, x, y, w, h, lv);
                BindImage(userImgPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }
                string msg = "带装备" + lv;

                int.TryParse(msg.Substring(3).Trim(), out int pos);
                if (pos <= 0)
                {
                    throw new Exception("口令错误或未获取到物品,请重试.");
                }
                if (UserInfoApi.SetUseEquipment(user, pos))
                {
                    MessageBox.Show($"穿戴成功!输入 查看装备 或 查看属性 查看吧!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;

            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                // 如果用户只签到没有注册就进行注册,否则提示已注册
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }


                string userImgPath = MapApi.GoBackHome(user, x, y, w, h, lv);
                if (!string.IsNullOrEmpty(userImgPath))
                    BindImage(userImgPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            #region 改个人数据
            var filelist = Directory.GetFiles(LegendFilePath.UserPath, "*.ini");
            IniFileHelper config;

            List<StditemDB> listStdItems = LegendApi.GetStdItemsList();

            //path = LegendFilePath.UserAttrPath + "战士.txt";
            //string legenStr = FileHelper.ReadTxt(path);
            //List<LengendBase> WarriorList = Api.JsonDeserialize<List<LengendBase>>(legenStr);

            //path = LegendFilePath.UserAttrPath + "法师.txt";
            //legenStr = FileHelper.ReadTxt(path);
            //List<LengendBase> WizardList = Api.JsonDeserialize<List<LengendBase>>(legenStr);

            //path = LegendFilePath.UserAttrPath + "道士.txt";
            //legenStr = FileHelper.ReadTxt(path);
            //List<LengendBase> TaoistList = Api.JsonDeserialize<List<LengendBase>>(legenStr);

            //LengendBase lengendBase = null;

            foreach (var file in filelist)
            {
                config = new IniFileHelper(file);
                int id = int.Parse(config.Read("人物信息", "序号"));
                if (id > 1)
                {
                    string zz1 = config.Read("人物信息", "是否领取唯一赞助1");
                    string zz2 = config.Read("人物信息", "是否领取唯一赞助2");
                    string zz3 = config.Read("人物信息", "是否领取唯一赞助3");

                    string bp1 = config.Read("人物信息", "白嫖领取礼包1");
                    string bp2 = config.Read("人物信息", "白嫖领取礼包2");
                    string bp3 = config.Read("人物信息", "白嫖领取礼包3");
                    if (zz1 == "是" || bp1 == "是")
                    {
                        config.Write("礼包属性", "怪物爆率", "10");
                    }
                    if (zz2 == "是" || bp2 == "是")
                    {
                        config.Write("礼包属性", "怪物爆率", "20");
                    }
                    if (zz3 == "是" || bp3 == "是")
                    {
                        config.Write("礼包属性", "怪物爆率", "30");
                    }
                    string legenPath = "";
                    switch (config.Read("人物信息", "职业"))
                    {
                        case "战士": legenPath = LegendFilePath.UserAttrPath + "战士.txt"; break;
                        case "法师": legenPath = LegendFilePath.UserAttrPath + "法师.txt"; break;
                        case "道士": legenPath = LegendFilePath.UserAttrPath + "道士.txt"; break;
                    }
                    UserInfo user = new UserInfo() { Id = id };
                    var legenStr = FileHelper.ReadTxt(legenPath);
                    List<LengendBase> LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                    var level = int.Parse(config.Read("普通数据", "等级"));
                    LengendBase lb = LengendList.Where(l => l.Level == level).FirstOrDefault();
                    if (lb != null)
                    {
                        user.User_Attribute = new UserAttribute
                        {
                            Hp = lb.Hp,
                            MaxHp = lb.Hp,
                            Mp = lb.Mp,
                            MaxMp = lb.Mp,
                            Ac = lb.Ac,
                            Ac2 = lb.Ac2,
                            Mac = lb.Mac,
                            Mac2 = lb.Mac2,
                            Dc = lb.Dc,
                            DcMax = lb.DcMax,
                            Mc = lb.Mc,
                            McMax = lb.McMax,
                            Sc = lb.Sc,
                            ScMax = lb.ScMax,
                            WristMax = lb.Wrist,
                        };
                        user.User_General = new UserGeneral
                        {
                            WearMax = lb.Wear,
                            BgwMax = lb.Bgw
                        };
                    }
                    int maxWeight = 0;
                    var listzb = config.GetAllKeys("装备物品");
                    foreach (string key in listzb)
                    {
                        var zbKey = config.Read("装备物品", key);

                        if (zbKey != "0" && zbKey.Length > 0)
                        {
                            var zb = config.ReadValue("装备属性", zbKey);
                            StditemDB stdItem = Api.JsonDeserialize<StditemDB>(zb);
                            user.User_Attribute.Hp += stdItem.StdHP + stdItem.SubAttr14;
                            user.User_Attribute.MaxHp += stdItem.StdHP;
                            user.User_Attribute.Mp += stdItem.StdMP;
                            user.User_Attribute.MaxMp += stdItem.StdMP;
                            if (key == "武器") user.User_Attribute.Wrist += stdItem.Weight;
                            maxWeight += stdItem.Weight;

                            user.User_Attribute.Ac += stdItem.StdAC;
                            user.User_Attribute.Ac2 += stdItem.StdAC2 + stdItem.SubAttr1;
                            user.User_Attribute.Mac += stdItem.StdMac;
                            user.User_Attribute.Mac2 += stdItem.StdMac2 + stdItem.SubAttr2;

                            user.User_Attribute.Dc += stdItem.StdDc;
                            user.User_Attribute.DcMax += stdItem.StdDc2 + stdItem.SubAttr3;
                            user.User_Attribute.Mc += stdItem.StdMc;
                            user.User_Attribute.McMax += stdItem.StdMc2 + stdItem.SubAttr4;
                            user.User_Attribute.Sc += stdItem.StdSc;
                            user.User_Attribute.ScMax += stdItem.StdSc2 + stdItem.SubAttr5;

                            user.User_Attribute.Acc += stdItem.SubAttr6 + stdItem.Accuracy;
                            user.User_Attribute.Agile += stdItem.SubAttr7 + stdItem.Agile;
                            user.User_Attribute.Lucky += stdItem.SubAttr8 + stdItem.Lucky;
                            user.User_Attribute.DouBPower += stdItem.SubAttr9;
                            user.User_Attribute.MonExpRate += stdItem.SubAttr10;
                            user.User_General.BgwMax += stdItem.SubAttr11;
                            if (stdItem.StdElemAttr != null)
                            {
                                user.User_Attribute.MonExpRate += stdItem.StdElemAttr.MonExpRate;
                                user.User_Attribute.MagicEvas += stdItem.StdElemAttr.MagicEvas;
                                user.User_Attribute.AttackSpeed += stdItem.StdElemAttr.AttackSpeed;
                                user.User_Attribute.MoveSpeed += stdItem.StdElemAttr.MoveSpeed;
                                user.User_Attribute.PhysicalDamageReduc += stdItem.StdElemAttr.PhysicalDamageReduc;
                                user.User_Attribute.MagicDamageReduc += stdItem.StdElemAttr.MagicDamageReduc;
                                user.User_Attribute.PhysicalDamageReflex += stdItem.StdElemAttr.PhysicalDamageReflex;
                                user.User_Attribute.MagicDamageReflex += stdItem.StdElemAttr.MagicDamageReflex;
                                user.User_Attribute.HealsPR += stdItem.StdElemAttr.HealsPR;
                                user.User_Attribute.AttackPR += stdItem.StdElemAttr.AttackPR;
                                user.User_Attribute.MagicAC += stdItem.StdElemAttr.MagicAC;
                                user.User_Attribute.IgnoreAC += stdItem.StdElemAttr.IgnoreAC;
                                user.User_Attribute.DamageRebound += stdItem.StdElemAttr.DamageRebound;
                                user.User_Attribute.UserExpRate += stdItem.StdElemAttr.UserExpRate;
                                user.User_Attribute.HPIncrease += stdItem.StdElemAttr.HPIncrease;
                                user.User_Attribute.MPIncrease += stdItem.StdElemAttr.MPIncrease;
                                user.User_Attribute.EquBurstRate += stdItem.StdElemAttr.EquBurstRate;
                                user.User_Attribute.FatalInjury += stdItem.StdElemAttr.FatalInjury;
                                user.User_Attribute.FatalBlow += stdItem.StdElemAttr.FatalBlow;
                                user.User_Attribute.ExcellentBlow += stdItem.StdElemAttr.ExcellentBlow;
                                user.User_Attribute.HeartfeltBlow += stdItem.StdElemAttr.HeartfeltBlow;
                            }
                        }
                    }
                    config.Write("普通数据", "穿戴重量", maxWeight + "");
                    config.Write("普通数据", "最大穿戴重量", user.User_General.WearMax + "");
                    config.Write("普通数据", "最大背包重量", user.User_General.BgwMax + "");

                    config.Write("属性点", "腕力", user.User_Attribute.Wrist + "");
                    config.Write("属性点", "最大腕力", user.User_Attribute.WristMax + "");
                    config.Write("属性点", "Hp", user.User_Attribute.Hp + "");
                    config.Write("属性点", "Mp", user.User_Attribute.Mp + "");
                    config.Write("属性点", "MaxHp", user.User_Attribute.Hp + "");
                    config.Write("属性点", "MaxMp", user.User_Attribute.Mp + "");
                    config.Write("属性点", "防御下限", user.User_Attribute.Ac + "");
                    config.Write("属性点", "防御上限", user.User_Attribute.Ac2 + "");
                    config.Write("属性点", "魔法防御下限", user.User_Attribute.Mac + "");
                    config.Write("属性点", "魔法防御上限", user.User_Attribute.Mac2 + "");
                    config.Write("属性点", "物理攻击力", user.User_Attribute.Dc + "");
                    config.Write("属性点", "最大物理攻击力", user.User_Attribute.DcMax + "");
                    config.Write("属性点", "魔法攻击力", user.User_Attribute.Mc + "");
                    config.Write("属性点", "最大魔法攻击力", user.User_Attribute.McMax + "");
                    config.Write("属性点", "道术攻击力", user.User_Attribute.Sc + "");
                    config.Write("属性点", "最大道术攻击力", user.User_Attribute.ScMax + "");
                    config.Write("属性点", "准确", user.User_Attribute.Acc + "");
                    config.Write("属性点", "敏捷", user.User_Attribute.Agile + "");
                    config.Write("属性点", "暴击", user.User_Attribute.Heals + "");
                    config.Write("属性点", "幸运", user.User_Attribute.Lucky + "");
                    config.Write("属性点", "倍功", user.User_Attribute.DouBPower + "");

                    config.Write("属性点", "魔法躲避", user.User_Attribute.MagicEvas + "");
                    config.Write("属性点", "攻击速度", user.User_Attribute.AttackSpeed + "");
                    config.Write("属性点", "移动速度", user.User_Attribute.MoveSpeed + "");
                    config.Write("属性点", "物伤减免", user.User_Attribute.PhysicalDamageReduc + "");
                    config.Write("属性点", "魔伤减免", user.User_Attribute.MagicDamageReduc + "");
                    config.Write("属性点", "物伤反射", user.User_Attribute.PhysicalDamageReflex + "");
                    config.Write("属性点", "魔伤反射", user.User_Attribute.MagicDamageReflex + "");
                    config.Write("属性点", "暴击几率", user.User_Attribute.HealsPR + "");
                    config.Write("属性点", "攻击伤害", user.User_Attribute.AttackPR + "");
                    config.Write("属性点", "魔法防御", user.User_Attribute.MagicAC + "");
                    config.Write("属性点", "忽视防御", user.User_Attribute.IgnoreAC + "");
                    config.Write("属性点", "伤害反弹", user.User_Attribute.DamageRebound + "");
                    config.Write("属性点", "人物爆率", user.User_Attribute.UserExpRate + "");
                    config.Write("属性点", "体力增加", user.User_Attribute.HPIncrease + "");
                    config.Write("属性点", "魔力增加", user.User_Attribute.MPIncrease + "");
                    config.Write("属性点", "怪物爆率", user.User_Attribute.MonExpRate + "");
                    config.Write("属性点", "防爆出率", user.User_Attribute.EquBurstRate + "");
                    config.Write("属性点", "致命伤害", user.User_Attribute.FatalInjury + "");
                    config.Write("属性点", "致命一击", user.User_Attribute.FatalBlow + "");
                    config.Write("属性点", "卓越一击", user.User_Attribute.ExcellentBlow + "");
                    config.Write("属性点", "会心一击", user.User_Attribute.HeartfeltBlow + "");
                }
            }


            #endregion

            #region 改图片名称
            //string path = "D:\\神技\\Items";
            //var filelist = Directory.GetFiles(path, "*.png");

            //string placementsPath = "D:\\神技\\Items\\Placements";
            //var placementsList = Directory.GetFiles(placementsPath, "*.txt");
            //int id = 7484;
            //for (int i = filelist.Length - 1; i >= 0; i--)
            //{
            //    if (File.Exists(filelist[i]))
            //    {
            //        FileInfo file = new FileInfo(filelist[i]);
            //        if (file.Exists)
            //        {
            //            int nameNum = int.Parse(file.Name.Split('.')[0]) + id;
            //            string newPath = $"{path}\\{nameNum.ToString().PadLeft(6, '0')}.png";
            //            File.Move(filelist[i], newPath);

            //            string newPlacePath = $"{placementsPath}\\{nameNum.ToString().PadLeft(6, '0')}.txt";
            //            File.Move(placementsList[i], newPlacePath);
            //        }
            //    }
            //}
            #endregion

            #region 改图片数据
            //string stdPath = LegendFilePath.Mud2Path + "stditem.txt";
            //string s = FileHelper.ReadTxt(stdPath);
            //List<StditemDB> stditemDBList = Api.JsonDeserialize<List<StditemDB>>(s);
            //for (int i = 0; i < stditemDBList.Count; i++)
            //{
            //    if (stditemDBList[i].Looks >= 10000)// 大于等于10000从0开始取
            //    {
            //        stditemDBList[i].Looks -= 10000;
            //    }
            //    else
            //    {
            //        stditemDBList[i].Looks += 7484;
            //    }
            //    stditemDBList[i].StdImgPath = stditemDBList[i].Looks.ToString().PadLeft(6, '0') + ".png";
            //    stditemDBList[i].StdName = stditemDBList[i].StdName.Trim();
            //}

            //string std = Api.JsonSerialize(stditemDBList);
            //FileHelper.DeleteFile(stdPath);
            //FileHelper.WriteLine(stdPath, std);
            #endregion

            #region 改怪物爆率文件
            //string path = LegendFilePath.MonQuestPath + "MonQuest.txt";

            //var MQList = Api.JsonDeserialize<List<MonQuest>>(FileHelper.ReadTxt(path));

            //for (int i = 0; i < MQList.Count; i++)
            //{
            //    switch (MQList[i].MQ_StdID)
            //    {
            //        case 358:// 中华
            //        case 539: if (MQList[i].MQ_MonExpRate >= 500) MQList[i].MQ_MonExpRate = 500; break;// 红包

            //        case 356:// 农夫山泉
            //        case 527:// 时装羽毛
            //        case 473: if (MQList[i].MQ_MonExpRate >= 100) MQList[i].MQ_MonExpRate = 100; break;// 书页

            //        case 357: if (MQList[i].MQ_MonExpRate >= 200) MQList[i].MQ_MonExpRate = 200; break;// 杜蕾斯

            //        case 457:// 宝石碎片
            //        case 487:// 升星宝石
            //        case 355: if (MQList[i].MQ_MonExpRate >= 75) MQList[i].MQ_MonExpRate = 75; break;// 火腿肠

            //    }

            //}

            //string strMQ = Api.JsonSerialize<List<MonQuest>>(MQList);

            //FileHelper.DeleteFile(path);
            //FileHelper.WriteLine(path, strMQ);
            #endregion

            #region 改怪物数据库
            //string path2 = LegendFilePath.Mud2Path + "monster.txt";
            //List<MonsterDB> monsterList = Api.JsonDeserialize<List<MonsterDB>>(FileHelper.ReadTxt(path2));//找所有怪物
            //for (int i = 0; i < monsterList.Count; i++)
            //{
            //    if (monsterList[i].HP >= 30000)
            //    {
            //        monsterList[i].Exp = monsterList[i].HP;
            //    }
            //}

            //string mon = Api.JsonSerialize(monsterList);
            //string monPath = LegendFilePath.Mud2Path + "monster.txt";
            //FileHelper.DeleteFile(monPath);
            //FileHelper.WriteLine(monPath, mon);

            #endregion


            MessageBox.Show("ok!");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                List<StditemDB> listResult = UserInfoApi.GetSellingMeat(user);
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GetSellingMeatIMG(user, listResult, x, y, w, h, lv);
                BindImage(userImgPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }
                string msg = "使用技能" + lv;

                int.TryParse(msg.Substring(4).Trim(), out int pos);
                if (pos <= 0)
                {
                    throw new Exception("口令错误或未获取到技能编号,请重试.");
                }
                pos -= 1;
                UserInfoApi.UseSkill(user, pos);
                UserInfoImg userInfoImg = new UserInfoImg(user);
                string userImgPath = userInfoImg.GetUserSkillIMG(user, x, y, w, h, lv);
                BindImage(userImgPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbChoiseNPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            string FromQQ = txtQQ2.Text;
            Random rnd = new Random();
            try
            {
                UserInfo user = UserInfoApi.GetInfoByQQ(FromQQ);
                if (user.Id == 0)
                {
                    throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                }

                string userImgPath = string.Empty;
                int mapLv = 1;
                switch (cbChoiseNPC.Text)
                {
                    case "下图":
                        if (user.Map.EndsWith("入口"))
                        {
                            string mapName = user.Map.Substring(0, 4) + mapLv;
                            userImgPath = MapApi.GotoMap(user, mapName, x, y, w, h, lv);
                        }
                        else
                        {
                            mapLv = int.Parse(user.Map.Substring(user.Map.Length - 1)) + 1;

                            string mapName = user.Map.Substring(0, user.Map.Length - 1) + mapLv;
                            userImgPath = MapApi.GotoMap(user, mapName, x, y, w, h, lv);
                        }
                        break;
                    case "万事通":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.WanShiTong(user, x, y, w, h, lv);
                        break;
                    case "----卖物品":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.SellStore(user, "卖物品", x, y, w, h, lv);
                        break;
                    case "----买物品":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.BuyStore(user, "买物品超级聚灵珠#2", x, y, w, h, lv);
                        //userImgPath = MapApi.BuyStore(user, "买物品铂金戒指#" + lv, x, y, w, h, lv);
                        break;
                    case "----保管":
                        string msg = "保管8#10";
                        msg = msg.Substring(2).Trim();
                        int stnum = 0, ednum = 0;
                        if (msg.IndexOf('#') >= 0)
                        {
                            var arrM = msg.Split('#');
                            int.TryParse(arrM[0], out stnum);
                            int.TryParse(arrM[1], out ednum);
                        }
                        else
                        {
                            stnum = int.Parse(msg);
                            ednum = stnum;
                        }
                        if (stnum < 0) throw new Exception("口令错误或未获取到保管物品编号,请重试.");
                        if (ednum > 80) throw new Exception("取出数量大于仓库最大数量,请重试.");
                        if (user.Id == 0)
                        {
                            throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                        }
                        if (user.User_General.VipLv == 0) if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和万事通对话吧.");

                        stnum = stnum == 0 ? 1 : stnum;
                        userImgPath = MapApi.SafeStore(user, stnum, ednum);
                        break;
                    case "----找回":
                        if (user.User_General.VipLv == 0) if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和万事通对话吧.");
                        userImgPath = MapApi.RecoverStore(user, 0, x, y, w, h, lv);
                        break;
                    case "----取回":
                        msg = "取回3#5";
                        msg = msg.Substring(2).Trim();
                        stnum = 0; ednum = 0;
                        if (msg.IndexOf('#') >= 0)
                        {
                            var arrM = msg.Split('#');
                            int.TryParse(arrM[0], out stnum);
                            int.TryParse(arrM[1], out ednum);
                        }
                        else
                        {
                            stnum = int.Parse(msg);
                            ednum = stnum;
                        }
                        if (stnum < 0) throw new Exception("口令错误或未获取到保管物品编号,请重试.");

                        if (user.Id == 0)
                        {
                            throw new Exception("对不起,您没有注册哦!请在输入框中输入 注册 名称,职业,性别 即可.");
                        }
                        if (user.User_General.VipLv == 0) if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和万事通对话吧.");

                        stnum = stnum == 0 ? 1 : stnum;
                        MessageBox.Show(MapApi.GetStdItemByStore(user, stnum, ednum, x, y, w, h, lv));
                        break;
                    case "----金币换金条":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GetTigToGold(user, lv, x, y, w, h, lv);
                        break;
                    case "----金条换金币":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GetGoldToTig(user, lv, x, y, w, h, lv);
                        break;
                    case "老兵":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.LaoBinG(user, x, y, w, h, lv);
                        break;
                    case "--pk":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.LaoBinGPK(user, x, y, w, h, lv);
                        break;
                    case "--向商店移动":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.LaoBinGoToStore(user, x, y, w, h, lv);
                        break;
                    case "传送员":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.ChuanSongYuan(user, x, y, w, h, lv);
                        break;
                    case "--城市传送":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.ChuanSongYuanGotoCity(user, x, y, w, h, lv);
                        break;
                    case "----前往银杏山谷":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoCityMap(user, "银杏山谷", 3000, x, y, w, h, lv);
                        break;
                    case "----前往边界村":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoCityMap(user, "边界村", 3000, x, y, w, h, lv);
                        break;
                    case "----前往毒蛇山谷":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoCityMap(user, "毒蛇山谷", 3000, x, y, w, h, lv);
                        break;
                    case "----前往比奇城":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoCityMap(user, "比奇城", 3000, x, y, w, h, lv);
                        break;
                    case "----前往盟重土城":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoCityMap(user, "盟重土城", 3000, x, y, w, h, lv);
                        break;
                    case "--险恶区域":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.ChuanSongYuanGotXianE(user, x, y, w, h, lv);
                        break;
                    case "--危险区域":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = Market_DefApi.ChuanSongYuanGotWeiX(user, x, y, w, h, lv);
                        break;
                    case "----前往兽人古墓":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "兽人古墓", 5000, x, y, w, h, lv);
                        break;
                    case "----前往天然洞穴":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "天然洞穴", 5000, x, y, w, h, lv);
                        break;
                    case "----前往比奇矿区":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "比奇矿区", 5000, x, y, w, h, lv);
                        break;
                    case "----前往毒蛇矿区":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "毒蛇矿区", 5000, x, y, w, h, lv);
                        break;
                    case "----前往沃玛寺庙":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "沃玛寺庙", 5000, x, y, w, h, lv);
                        break;
                    case "----前往蜈蚣洞口":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "蜈蚣洞口", 5000, x, y, w, h, lv);
                        break;
                    case "----前往石墓洞口":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "石墓洞口", 5000, x, y, w, h, lv);
                        break;
                    case "----前往祖玛寺庙":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "祖玛寺庙", 5000, x, y, w, h, lv);
                        break;
                    case "----前往尸王殿":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "尸王殿", 5000, x, y, w, h, lv);
                        break;
                    case "----前往沃玛寺庙3":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "沃玛寺庙3", 5000, x, y, w, h, lv);
                        break;



                    case "神秘地图":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        if (user.Map != "盟重土城") throw new Exception("神秘地图在盟重土城才能查看,请先前往盟重土城.");
                        user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(3, 8)}.jpg";
                        userImgPath = NPCApi.GetNPCByName(user, "神秘地图", x, y, w, h);
                        break;
                    case "玛雅大厅":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        if (user.Map != "盟重土城") throw new Exception("神秘地图在盟重土城才能查看,请先前往盟重土城.");
                        user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(3, 11)}.jpg";
                        userImgPath = NPCApi.GetNPCByName(user, "玛雅大厅", x, y, w, h);
                        break;
                    case "散人打宝":
                        if (user.Map != "盟重土城") throw new Exception("散人打宝在盟重土城才能查看,请先前往盟重土城.");
                        user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(7, 13)}.jpg";
                        userImgPath = NPCApi.GetNPCByName(user, "散人打宝", x, y, w, h);
                        break;
                    case "BOSS之家":
                        if (user.Map != "盟重土城") throw new Exception("BOSS之家在盟重土城才能查看,请先前往盟重土城.");
                        user.UserImagePath = LegendFilePath.LegendIMGDataPath + $"地图\\4\\盟重土城\\4_{rnd.Next(6, 11)}.jpg";
                        userImgPath = NPCApi.GetNPCByName(user, "BOSS之家", x, y, w, h);
                        break;

                    case "----前往散人打宝":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "散人打宝", 5000, x, y, w, h, lv);
                        break;
                    case "----前往玛雅大厅":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "玛雅大厅", 5000, x, y, w, h, lv);
                        break;
                    case "----前往神秘地图":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "神秘地图", 5000, x, y, w, h, lv);
                        break;
                    case "----前往BOSS之家":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GotoNormalXianEMap(user, "boss之家", 5000, x, y, w, h, lv);
                        break;



                    case "----书店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "书店", x, y, w, h, lv);
                        break;
                    case "----药品店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "药品店", x, y, w, h, lv);
                        break;
                    case "----炼药店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "炼药店", x, y, w, h, lv);
                        break;
                    case "----首饰店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "首饰店", x, y, w, h, lv);
                        break;
                    case "----手镯店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "手镯店", x, y, w, h, lv);
                        break;
                    case "----项链店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "项链店", x, y, w, h, lv);
                        break;
                    case "----服装店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "服装店", x, y, w, h, lv);
                        break;
                    case "----武器店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "武器店", x, y, w, h, lv);
                        break;
                    case "----仓库":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "仓库", x, y, w, h, lv);
                        break;
                    case "----杂货店":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "杂货店", x, y, w, h, lv);
                        break;
                    case "----修理":
                        if (user.IsOnCity == "否") throw new Exception("你不在城中,请输入 回城 再和老兵对话吧.");
                        userImgPath = MapApi.GoToStrStore(user, "修理", x, y, w, h, lv);
                        break;

                    case "上一页":
                        userImgPath = MapApi.JumpPage(user, -1, x, y, w, h, lv);
                        break;
                    case "下一页":
                        userImgPath = MapApi.JumpPage(user, 1, x, y, w, h, lv);
                        break;

                }
                if (!string.IsNullOrEmpty(userImgPath))
                    BindImage(userImgPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}