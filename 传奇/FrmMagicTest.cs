using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using 传奇.core;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace 传奇
{
    public partial class FrmMagicTest : Form
    {
        Dictionary<int, string> HpImgDic = new Dictionary<int, string>();
        Random rnd = new Random();
        string UserInfoImgPath = LegendFilePath.imgPath;
        string LegendIMGDataPath = LegendFilePath.LegendIMGDataPath;
        string UserPath = LegendFilePath.UserPath;


        string newopUIPath = string.Empty;
        string humPath = string.Empty;
        string weaponPath = string.Empty;

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

        public FrmMagicTest()
        {
            InitializeComponent();

            humPath = LegendIMGDataPath + "Hum\\";
            newopUIPath = LegendIMGDataPath + "NewopUI\\";
            weaponPath = LegendIMGDataPath + "Weapon\\";

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

        private void FrmMagicTest_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            rbSex1.Checked = true;
            rbmagic1.Checked = true;
            rbweapon1.Checked = true;

            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            List<string> list = new List<string>();
            switch (comboBox1.Text)
            {
                case "战士":
                    list.Add("基本剑术,3");
                    list.Add("攻杀剑术,7");
                    list.Add("刺杀剑术,12");
                    list.Add("半月弯刀,25");
                    list.Add("烈火剑法,26");
                    list.Add("野蛮冲撞,27");
                    list.Add("护体神盾,34");
                    break;
                case "法师":
                    list.Add("火球术,1");
                    list.Add("大火球,5");
                    list.Add("抗拒火环,8");
                    list.Add("地狱火,9");
                    list.Add("疾光电影,10");
                    list.Add("雷电术,11");
                    list.Add("诱惑之光,20");
                    list.Add("瞬息移动,21");
                    list.Add("火墙,22");
                    list.Add("爆裂火焰,23");
                    list.Add("地狱雷光,24");
                    list.Add("魔法盾,31");
                    list.Add("圣言术,32");
                    list.Add("冰咆哮,33");
                    break;
                case "道士":
                    list.Add("治愈术,2");
                    list.Add("精神力战法,4");
                    list.Add("施毒术,6");
                    list.Add("灵魂火符,13");
                    list.Add("幽灵盾,14");
                    list.Add("神圣战甲术,15");
                    list.Add("困魔咒,16");
                    list.Add("召唤骷髅,17");
                    list.Add("隐身术,18");
                    list.Add("集体隐身术,19");
                    list.Add("心灵启示,28");
                    list.Add("群体治疗术,29");
                    list.Add("召唤神兽,30");
                    break;
            }
            list.ForEach(item =>
            {
                comboBox2.Items.Add(item);
            });
            comboBox2.SelectedIndex = 0;
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


        private void ShowPlayer(UserInfo user1, UserInfo user2, string msg, int x, int y, int w, int h, int lv, int count, int looks, int shapeCode, MagicDB magic)
        {
            try
            {
                UserInfoImg userInfoImg = new UserInfoImg(user1);
                string userImgPath = userInfoImg.GamgeMainForm(user1, false, false, true, false, x, y, w, h, lv);

                int humX = 512, humY = 284, weaponX = 512, weaponY = 284,
                    hpX1 = 630, hpY1 = 250, hpX2 = 640, hpY2 = 250, index = 1, magicX = 512, magicY = 284,
                    num = 0, att1 = 0, att2 = 0, att3 = 0, att4 = 0;
                int delay = 12;
                string playerImg1 = "", playerImg2 = "", weaponImg1 = "", weaponImg2 = "", magicImg = "", path = "";
                string userIniPath = UserPath + user1.UserQQ + ".ini";
                IniFileHelper userINI = new IniFileHelper(userIniPath);

                userIniPath = UserPath + user2.UserQQ + ".ini";
                IniFileHelper user2INI = new IniFileHelper(userIniPath);

                List<string> user1ImgList = userInfoImg.GetPlayerImgList(user1, userINI, looks);
                List<string> user1WeaponImgList = userInfoImg.GetPlayerWeaponImgList(user1, userINI, shapeCode);

                List<string> user2ImgList = user1ImgList;
                List<string> user2WeaponImgList = user1WeaponImgList;
                if (user2.Sex == "女")
                {
                    user2ImgList = userInfoImg.GetPlayerImgList(user2, user2INI, 0);
                    user2WeaponImgList = userInfoImg.GetPlayerWeaponImgList(user2, user2INI, 0);
                }

                string player1 = user1ImgList[16];
                string playerWeapon1 = user1WeaponImgList[16];

                string player2 = user1ImgList[48];
                string playerWeapon2 = user1WeaponImgList[48];

                string nMsg = user1.Name + ":" + msg;
                msg = user1.Name + ":" + msg;
                int n = msg.Length;

                for (int k = 0; k < n; k++)
                {
                    if (k % 15 == 0 && k > 0)
                    {
                        msg = msg.Insert(k, "\n");
                    }
                }
                n = nMsg.Length;
                for (int k = 0; k < n; k++)
                {
                    if (k % 52 == 0 && k > 0)
                    {
                        nMsg = nMsg.Insert(k, "\n");
                    }
                }


                MemoryStream ms = new MemoryStream();
                MagickImage magickImage = null;
                MagickImage blendImage = null;
                Rectangle rect = new Rectangle(202, 472, 390, 15);
                SolidBrush brushWhite = new SolidBrush(Color.White);
                SolidBrush brushBlack = new SolidBrush(Color.Black);
                List<string> magImgList = new List<string>();// 魔法效果集合
                List<string> runImgList = new List<string>();
                List<string> runWeaponImgList = new List<string>();
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    using (Image imgMian = Image.FromFile(userImgPath))
                    {
                        using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                        {
                            using (Graphics gh = Graphics.FromImage(bitmap))
                            {
                                string kulouPath = "";
                                int attack = rnd.Next(100);
                                for (int i = 0; i < count; i++) //攻击次数
                                {
                                    attack = rnd.Next(100);
                                    index = 0;
                                    num = 0;

                                    blendImage = null;
                                    runImgList = user1ImgList.Skip(216).Take(6).ToList();// 取砍图(人物)
                                    runWeaponImgList = user1WeaponImgList.Skip(216).Take(6).ToList();// 取砍图(武器)
                                    switch (magic.MagName)
                                    {
                                        case "攻杀剑术":
                                        case "刺杀剑术":
                                        case "半月弯刀":
                                        case "烈火剑法":
                                            magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取魔法效果
                                            break;
                                        case "野蛮冲撞":
                                            magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取魔法效果

                                            runImgList = user1ImgList.Skip(144).Take(6).ToList();// 取跑图（人物）
                                            runImgList.AddRange(user1ImgList.Skip(16).Take(4).ToList());// 取站图（人物）

                                            runWeaponImgList = user1WeaponImgList.Skip(144).Take(6).ToList();// 取跑图（武器）
                                            runWeaponImgList.AddRange(user1WeaponImgList.Skip(16).Take(4).ToList());// 取站图（武器）
                                            break;
                                        default:
                                            magImgList = MagicApiImg.GetAttackMagicImg(magic, 3);// 取魔法效果
                                            runImgList = new List<string>();

                                            runImgList.AddRange(user1ImgList.Skip(408).Take(6).ToList());// 取施法图（人物）
                                            runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                            runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                            runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）
                                            runImgList.AddRange(user1ImgList.Skip(194).Take(1).ToList());// 取站图（人物）

                                            runWeaponImgList = new List<string>();

                                            runWeaponImgList.AddRange(user1WeaponImgList.Skip(408).Take(6).ToList());// 取施法图（武器）
                                            runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                            runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                            runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                            runWeaponImgList.AddRange(user1WeaponImgList.Skip(194).Take(1).ToList());// 取站图（武器）
                                            break;
                                    }

                                    runImgList.ForEach(p =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置其它人
                                        if (user2.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user2.Sex == "男")
                                                weaponX = 565 + int.Parse(playerWeapon2.Split(',')[1]);
                                            else
                                                weaponX = 568 + int.Parse(playerWeapon2.Split(',')[1]);

                                            weaponY = 284 + int.Parse(playerWeapon2.Split(',')[2]);
                                            weaponImg2 = playerWeapon2.Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg2))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg2 = humPath + player2.Split(',')[0];
                                        humX = 565 + int.Parse(player2.Split(',')[1]);
                                        humY = 284 + int.Parse(player2.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, 574, 230, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(588, 277, 0, 0);
                                        DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 设置自己
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

                                        playerImg1 = humPath + p.Split(',')[0];
                                        humX = 512 + int.Parse(p.Split(',')[1]);
                                        humY = 284 + int.Parse(p.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, 521, 230, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(535, 277, 0, 0);
                                        DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名


                                        DrawCenteredString1(gh, msg, font10, brushWhite, rect, 530, 200);
                                        DrawCenteredString2(gh, nMsg, font10, brushBlack, rect, 208, 724);
                                        #endregion

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        //magickImage.AnimationDelay = delay;

                                        if (user1.IsUseSkills == "是" && magImgList.Count > 0)
                                        {
                                            // 显示技能特效图片
                                            magicImg = magImgList[0].Split(',')[0];
                                            if (user1.Sex == "男")
                                                magicX = 512 + int.Parse(magImgList[0].Split(',')[1]);
                                            else
                                                magicX = 515 + int.Parse(magImgList[0].Split(',')[1]);
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
                                                        //blendImage.AnimationDelay = delay;
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
                                                        //blendImage.AnimationDelay = delay;
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
                                                    //blendImage.AnimationDelay = delay;
                                                    magImgList.RemoveAt(0);
                                                    break;
                                            }
                                        }

                                        if (blendImage == null)
                                            collection.Add(magickImage);
                                        else
                                        {
                                            collection.Add(blendImage);
                                            blendImage = null;
                                        }

                                        num++;
                                    });

                                    num = 0;
                                    blendImage = null;
                                    magImgList.ForEach(m =>
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置其它人
                                        if (user2.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user2.Sex == "男")
                                                weaponX = 565 + int.Parse(player2.Split(',')[1]);
                                            else
                                                weaponX = 568 + int.Parse(player2.Split(',')[1]);

                                            weaponY = 284 + int.Parse(player2.Split(',')[2]);
                                            weaponImg2 = weaponPath + player2.Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg2))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg2 = humPath + player2.Split(',')[0];
                                        humX = 565 + int.Parse(player2.Split(',')[1]);
                                        humY = 284 + int.Parse(player2.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, 574, 230, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(588, 277, 0, 0);
                                        DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 设置自己

                                        using (Image weapon = Image.FromFile(weaponImg1))
                                        {
                                            gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                        }
                                        int pWidth = 0;
                                        playerImg1 = humPath + runImgList[runImgList.Count - 1].Split(',')[0];
                                        humX = 512 + int.Parse(runImgList[runImgList.Count - 1].Split(',')[1]);
                                        humY = 284 + int.Parse(runImgList[runImgList.Count - 1].Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            pWidth = player.Width;
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, 521, 230, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(535, 277, 0, 0);
                                        DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        // 显示技能特效图片
                                        magicImg = m.Split(',')[0];
                                        if (user1.Sex == "男")
                                            magicX = 512 + int.Parse(m.Split(',')[1]);
                                        else
                                            magicX = 515 + int.Parse(m.Split(',')[1]);

                                        magicY = 294 + int.Parse(m.Split(',')[2]);

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
                                                //blendImage.AnimationDelay = 5;
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
                                                //blendImage.AnimationDelay = 8;
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
                                                //magickImage.AnimationDelay = 8;
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
                                                //blendImage.AnimationDelay = 10;
                                                collection.Add(blendImage);
                                                break;
                                        }

                                        num++;
                                    });

                                    // 飘血
                                    for (int j = 0; j < 5; j++)
                                    {
                                        gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                                        #region 设置其它人
                                        if (user2.IsUseWeapon)// 设置人物武器
                                        {
                                            if (user2.Sex == "男")
                                                weaponX = 565 + int.Parse(player2.Split(',')[1]);
                                            else
                                                weaponX = 568 + int.Parse(player2.Split(',')[1]);

                                            weaponY = 284 + int.Parse(player2.Split(',')[2]);
                                            weaponImg2 = weaponPath + player2.Split(',')[0];
                                            using (Image weapon = Image.FromFile(weaponImg2))
                                            {
                                                gh.DrawImage(weapon, weaponX, weaponY, weapon.Width, weapon.Height);
                                            }
                                        }

                                        playerImg2 = humPath + player2.Split(',')[0];
                                        humX = 565 + int.Parse(player2.Split(',')[1]);
                                        humY = 284 + int.Parse(player2.Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg2))// 设置人物位置
                                        {
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, 574, 230, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(588, 277, 0, 0);
                                        DrawCenteredString(gh, user2.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 设置自己
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
                                        int pWidth = 0;
                                        playerImg1 = humPath + user1ImgList[194].Split(',')[0];
                                        humX = 512 + int.Parse(user1ImgList[194].Split(',')[1]);
                                        humY = 284 + int.Parse(user1ImgList[194].Split(',')[2]);
                                        using (Image player = Image.FromFile(playerImg1))// 设置人物位置
                                        {
                                            pWidth = player.Width;
                                            gh.DrawImage(player, humX, humY, player.Width, player.Height);
                                        }

                                        using (Image img = Image.FromFile(newopUIPath + "000008.png"))// 人物血条
                                        {
                                            gh.DrawImage(img, 521, 230, img.Width, img.Height);
                                        }

                                        rect = new Rectangle(535, 277, 0, 0);
                                        DrawCenteredString(gh, user1.Name, font9, brushWhite, rect);//用户名
                                        #endregion

                                        #region 飘血

                                        hpX1 = 490;
                                        hpX2 = 500;
                                        using (Image img = Image.FromFile(newopUIPath + "000060.png"))// 减号
                                        {
                                            gh.DrawImage(img, hpX1, hpY1 - (index - 1) * 5, img.Width, img.Height);
                                        }
                                        if (attack < 10)// 攻击在10以内
                                        {
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[attack]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        else if (attack >= 10 && attack < 100)
                                        {
                                            att1 = int.Parse(attack.ToString().Substring(0, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att2 = int.Parse(attack.ToString().Substring(1, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        else if (attack >= 100 && attack < 1000)
                                        {
                                            att1 = int.Parse(attack.ToString().Substring(0, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att2 = int.Parse(attack.ToString().Substring(1, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att3 = int.Parse(attack.ToString().Substring(2, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 16, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        else if (attack >= 1000 && attack < 10000)
                                        {
                                            att1 = int.Parse(attack.ToString().Substring(0, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att1]))
                                            {
                                                gh.DrawImage(img, hpX2, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att2 = int.Parse(attack.ToString().Substring(1, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att2]))
                                            {
                                                gh.DrawImage(img, hpX2 + 8, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att3 = int.Parse(attack.ToString().Substring(2, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att3]))
                                            {
                                                gh.DrawImage(img, hpX2 + 16, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                            att4 = int.Parse(attack.ToString().Substring(3, 1));
                                            using (Image img = Image.FromFile(newopUIPath + HpImgDic[att4]))
                                            {
                                                gh.DrawImage(img, hpX2 + 24, hpY2 - (index * 5), img.Width, img.Height);
                                            }
                                        }
                                        index++;
                                        #endregion

                                        bitmap.Save(ms, ImageFormat.Png);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        magickImage = new MagickImage(ms);
                                        //magickImage.AnimationDelay = 15;
                                        collection.Add(magickImage);
                                    }
                                }
                            }
                        }
                    }

                    path = UserInfoImgPath + user1.UserQQ + ".gif";
                    ms.Dispose();
                    collection.OptimizeTransparency();// 优化gif                    
                    collection.WriteAsync(path, MagickFormat.Gif).Wait();

                }
                if (magickImage != null)
                    magickImage.Dispose();
                if (blendImage != null)
                    blendImage.Dispose();

                BindImage(path);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            int count = int.Parse(txtci.Text);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            UserInfo user1 = UserInfoApi.GetInfoByQQ(txtQQ.Text);
            UserInfo user2 = UserInfoApi.GetInfoByQQ(txtQQ2.Text);
            if (rbSex1.Checked)
                user1.Sex = "男";
            else
                user1.Sex = "女";

            MagicDB magic = null;
            if (rbmagic1.Checked)
            {
                user1.IsUseSkills = "是";
                magic = GetMagIdByCombox();
            }
            else
                user1.IsUseSkills = "否";

            int shapeCode = 0;
            if (rbweapon1.Checked)
            {
                user1.IsUseWeapon = true;
                shapeCode = GetshapeCodeByCombox();
            }
            else
            {
                user1.IsUseWeapon = false;
            }


            user1.UserImagePath = $"{LegendIMGDataPath}地图\\pk房{rnd.Next(1, 7)}.jpg";

            int looks = GetLooksByCombox();

            string msg = "pk[@530647702] 超级无敌宇宙无敌芭芭拉魔法,给我死!";

            ShowPlayer(user1, user2, msg, x, y, w, h, lv, count, looks, shapeCode, magic);

            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            this.Text = $"共花费{ts2.TotalSeconds.ToString("F2")}秒";
        }

        private MagicDB GetMagIdByCombox()
        {
            string item = comboBox2.SelectedItem.ToString();
            int magid = int.Parse(item.Split(',')[1]);
            List<MagicDB> magicDBList = LegendApi.GetMagicList();
            MagicDB magicDB = magicDBList.Where(m => m.MagID == magid).FirstOrDefault();

            return magicDB;
        }

        private int GetshapeCodeByCombox()
        {
            string item = comboBox4.SelectedItem.ToString();
            int res = int.Parse(item.Split(',')[1]);
            return res;
        }

        private int GetLooksByCombox()
        {
            string item = comboBox3.SelectedItem.ToString();
            int res = int.Parse(item.Split(',')[1]);
            return res;
        }

        private void rbSex2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("布衣(女),1");
            comboBox3.Items.Add("轻型盔甲(女),2");
            comboBox3.Items.Add("中型盔甲(女),2");
            comboBox3.Items.Add("重盔甲(女),3");
            comboBox3.Items.Add("战神盔甲(女),3");
            comboBox3.Items.Add("魔法长袍(女),4");
            comboBox3.Items.Add("恶魔长袍(女),4");
            comboBox3.Items.Add("灵魂战衣(女),5");
            comboBox3.Items.Add("幽灵战衣(女),5");
            comboBox3.Items.Add("圣战宝甲,6");
            comboBox3.Items.Add("霓裳羽衣,7");
            comboBox3.Items.Add("天师长袍,8");
            comboBox3.SelectedIndex = 0;

        }

        private void rbSex1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("布衣(男),1");
            comboBox3.Items.Add("轻型盔甲(男),2");
            comboBox3.Items.Add("中型盔甲(男),2");
            comboBox3.Items.Add("重盔甲(男),3");
            comboBox3.Items.Add("战神盔甲(男),3");
            comboBox3.Items.Add("魔法长袍(男),4");
            comboBox3.Items.Add("恶魔长袍(男),4");
            comboBox3.Items.Add("灵魂战衣(男),5");
            comboBox3.Items.Add("幽灵战衣(男),5");
            comboBox3.Items.Add("天魔神甲,6");
            comboBox3.Items.Add("法神披风,7");
            comboBox3.Items.Add("天尊道袍,8");
            comboBox3.SelectedIndex = 0;
        }


        void DrawCenteredString1(Graphics graphics, string text, Font font, Brush brush, Rectangle rect, int x = 0, int y = 0)
        {
            // 创建一个StringFormat对象,并设置换行选项
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Near;
            format.Alignment = StringAlignment.Center;

            // 绘制换行文本
            graphics.DrawString(text, font, brush, new PointF(x, y), format);
        }

        void DrawCenteredString2(Graphics graphics, string text, Font font, Brush brush, Rectangle rect, int x = 0, int y = 0)
        {
            // 创建一个StringFormat对象,并设置换行选项
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;

            // 绘制换行文本
            graphics.DrawString(text, font, brush, new PointF(x, y), format);
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

        private void button2_Click(object sender, EventArgs e)
        {
            MirTest mirTest = new MirTest();
            mirTest.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtx.Text);
            int y = int.Parse(txty.Text);
            int w = int.Parse(txtw.Text);
            int h = int.Parse(txth.Text);
            int lv = int.Parse(txtLv.Text);
            int count = int.Parse(txtci.Text);
            Random rnd = new Random();

            try
            {
                string msg = "pk[@171355694] 超级无敌宇宙无敌芭芭拉魔法";
                string nMsg = msg.Substring(4);
                string qq2 = nMsg.Substring(0, nMsg.IndexOf(']'));
                if (msg.ToLower().StartsWith("pk"))
                {
                    UserInfo user1 = UserInfoApi.GetInfoByQQ(txtQQ.Text);
                    if (nMsg.Length < 4) throw new Exception("对不起,您的对手未找到,请重新pk挑战其它对手吧.");

                    string userImgPath = LegendFilePath.UserPath + qq2 + ".ini";
                    UserInfo user2 = null;
                    if (!File.Exists(userImgPath))
                    {
                        user2 = UserInfoApi.GetVirtualInfoByQQ(qq2, "", user1.User_General.Lv + 1, user1.User_Attribute.Ac + 1, user1.User_Attribute.Ac2 + 1, user1.User_Attribute.Agile + 1);
                    }
                    else
                    {
                        user2 = UserInfoApi.GetInfoByQQ(qq2);
                    }

                    msg = nMsg.Substring(nMsg.IndexOf(']') + 1).Trim();
                    msg = "";
                    List<UserAttackRecord> userAttackRecords = UserInfoApi.GetUserPKData(user1, user2, msg);

                    user1.UserImagePath = $"{LegendIMGDataPath}地图\\pk房{rnd.Next(1, 7)}.jpg";
                    UserInfoImg2 userInfoImg = new UserInfoImg2(user1);
                    user1.Map = "PK挑战房";
                    Task.Run(async () =>
                    {
                        userImgPath = await userInfoImg.GetUserPK(userAttackRecords, user1, user2);
                        BindImage(userImgPath);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region 首爆物品添加
            StdItemDropTip.AddStdItems();
            MessageBox.Show("ok!");
            #endregion
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(txtx.Text);
                int y = int.Parse(txty.Text);
                int w = int.Parse(txtw.Text);
                int h = int.Parse(txth.Text);
                int lv = int.Parse(txtLv.Text);
                int count = int.Parse(txtci.Text);

                UserInfo user = UserInfoApi.GetInfoByQQ(txtQQ.Text);
                string userImgPath = PrizeApi.GetFristPrizeImg(user, count, x, y, w, h, lv);
                BindImage(userImgPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            #region 首爆物品添加
            FirstKillMon.AddFristKillMonster();
            MessageBox.Show("ok!");
            #endregion
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(txtx.Text);
                int y = int.Parse(txty.Text);
                int w = int.Parse(txtw.Text);
                int h = int.Parse(txth.Text);
                int lv = int.Parse(txtLv.Text);
                int count = int.Parse(txtci.Text);

                UserInfo user = UserInfoApi.GetInfoByQQ(txtQQ.Text);
                string userImgPath = PrizeApi.GetFristKillImg(user, count, x, y, w, h, lv);
                BindImage(userImgPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MirNpcTest mirNpcTest = new MirNpcTest();
            mirNpcTest.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string res = "1,6,2,4,3,7,15,16,5,6,14,10,7,13,11,9,17,21,24,25,22,26,29,30,30,29,23,31,32,33,30,30,107,118,120,119,115,425,223,1002,1004,1005,1008,1005,29,31,31,300,256,352,36,413,407,34,37,19,8,18,12,28,27,114,164,1003";

            List<string> stringList = res.Split(',').ToList();

            List<int> intList = stringList.GroupBy(s => s).Select(s => int.Parse(s.Key)).OrderBy(s => s).ToList();
            res = "";
            foreach (var item in intList)
            {
                res += item + ",";
            }
            res = res.TrimEnd(',');
        }

    }
}
