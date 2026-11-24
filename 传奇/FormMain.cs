using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 传奇;
using 传奇.core;
using 传奇.Models;

namespace 传奇
{
    public partial class FormMain : Form
    {
        List<LengendBase> WarriorList = new List<LengendBase>();//战士数据
        List<LengendBase> WizardList = new List<LengendBase>();//法师数据
        List<LengendBase> TaoistList = new List<LengendBase>();//道士数据

        List<MagicDB> magicDBList = new List<MagicDB>();//技能数据
        List<MonsterDB> monsterDBList = new List<MonsterDB>();//怪物数据
        List<StditemDB> stditemDBList = new List<StditemDB>();//物品数据
        List<LengendMapInfo> mapInfoList = new List<LengendMapInfo>();//地图数据
        List<LegMapMonInfo> legMapMonInfos = new List<LegMapMonInfo>();//地图中怪物的数据
        List<MonQuest> monQuestList = new List<MonQuest>();//爆率的数据

        List<MonsterDB> listMon = new List<MonsterDB>();
        List<MonQuest> listMonQuest = new List<MonQuest>();

        LengendMapInfo MapInfo = null;

        string cellContent = "";
        int lbLevelSelectIndex1 = 0, MagID = 1, MonID = 1, cbItemIndex = 0, stdID = 1;

        public FormMain()
        {
            InitializeComponent();
        }

        private void cbJob1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cbText = cbJob1.Text;
            LengendBase lengendBase = null;
            if (WizardList.Count == 0)
            {
                WizardList = new List<LengendBase>();
                int ac = 0, ac2 = 0, mac = 0, mac2 = 0, dc = 0, dcmax = 1, hp = 14, mp = 12, bgw = 47, wear = 14, wrist = 11;
                for (int i = 1; i <= 100; i++)
                {
                    //防御计算
                    if (i % 7 == 0)
                    {
                        ac2++;
                        if ((i + 7) % 9 == 0) ac++;
                    }
                    //魔御计算
                    if (i % 12 == 0)
                    {
                        mac2++;
                        if ((i + 15) % 15 == 0) mac++;
                    }
                    //攻击力计算
                    if (i % 5 == 0)
                    {
                        dcmax++;
                        if ((i + 5) % 5 == 0) dc++;
                    }
                    hp += 2;
                    mp += 6;
                    bgw += 3;
                    wear += 1;
                    wrist += 1;
                    lengendBase = new LengendBase()
                    {
                        Level = i,
                        Job = "法师",
                        LvExp = i * 100,
                        Ac = ac,
                        Ac2 = ac2,
                        Mac = mac,
                        Mac2 = mac2,
                        Dc = dc,
                        DcMax = dcmax,
                        Mc = dc,
                        McMax = dcmax,
                        Hp = hp,
                        Mp = mp,
                        Bgw = bgw,
                        Wear = wear,
                        Wrist = wrist
                    };

                    WizardList.Add(lengendBase);
                }
            }
            if (TaoistList.Count == 0)
            {
                TaoistList = new List<LengendBase>();
                int ac = 0, ac2 = 0, mac = 0, mac2 = 0, dc = 0, dcmax = 1, hp = 14, mp = 12, bgw = 47, wear = 14, wrist = 11;
                for (int i = 1; i <= 100; i++)
                {
                    //防御计算
                    if (i % 7 == 0)
                    {
                        ac2++;
                        if ((i + 7) % 9 == 0) ac++;
                    }
                    //魔御计算
                    if (i % 4 == 0)
                    {
                        mac2++;
                        if ((i + 12) % 12 == 0) mac++;
                    }
                    //攻击力计算
                    if (i % 5 == 0)
                    {
                        dcmax++;
                        if ((i + 5) % 5 == 0) dc++;
                    }
                    hp += 3;
                    mp += 4;
                    bgw += 3;
                    wear += 1;
                    wrist += 1;
                    lengendBase = new LengendBase()
                    {
                        Level = i,
                        Job = "道士",
                        LvExp = i * 100,
                        Ac = ac,
                        Ac2 = ac2,
                        Mac = mac,
                        Mac2 = mac2,
                        Dc = dc,
                        DcMax = dcmax,
                        Sc = dc,
                        ScMax = dcmax,
                        Hp = hp,
                        Mp = mp,
                        Bgw = bgw,
                        Wear = wear,
                        Wrist = wrist
                    };

                    TaoistList.Add(lengendBase);
                }
            }

            switch (cbText)
            {
                case "战士": if (WarriorList.Count > 0) lengendBase = WarriorList[lbLevelSelectIndex1]; break;
                case "法师": if (WizardList.Count > 0) lengendBase = WizardList[lbLevelSelectIndex1]; break;
                case "道士": if (TaoistList.Count > 0) lengendBase = TaoistList[lbLevelSelectIndex1]; break;
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Cells[1].Value = lengendBase.LvExp;
                dataGridView1.Rows[1].Cells[1].Value = $"{lengendBase.Ac}/{lengendBase.Ac2}";
                dataGridView1.Rows[2].Cells[1].Value = $"{lengendBase.Mac}/{lengendBase.Mac2}";
                dataGridView1.Rows[3].Cells[1].Value = $"{lengendBase.Dc}/{lengendBase.DcMax}";
                dataGridView1.Rows[4].Cells[1].Value = $"{lengendBase.Mc}/{lengendBase.McMax}";
                dataGridView1.Rows[5].Cells[1].Value = $"{lengendBase.Sc}/{lengendBase.ScMax}";
                dataGridView1.Rows[6].Cells[1].Value = lengendBase.Hp;
                dataGridView1.Rows[7].Cells[1].Value = lengendBase.Mp;
                dataGridView1.Rows[8].Cells[1].Value = lengendBase.Bgw;
                dataGridView1.Rows[9].Cells[1].Value = lengendBase.Wear;
                dataGridView1.Rows[10].Cells[1].Value = lengendBase.Wrist;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            BindtabPage0Data();
        }

        private void BindtabPage0Data()
        {

            string[] txtFiles = Directory.GetFiles(LegendFilePath.UserAttrPath, "*.txt", SearchOption.AllDirectories);
            foreach (var file in txtFiles)
            {
                if (file.Contains("战士"))
                {
                    string s = FileHelper.ReadTxt(file);
                    WarriorList = Api.JsonDeserialize<List<LengendBase>>(s);
                }
                else if (file.Contains("法师"))
                {
                    string s = FileHelper.ReadTxt(file);
                    WizardList = Api.JsonDeserialize<List<LengendBase>>(s);
                }
                else if (file.Contains("道士"))
                {
                    string s = FileHelper.ReadTxt(file);
                    TaoistList = Api.JsonDeserialize<List<LengendBase>>(s);
                }
                else
                {
                    //其它txt文件不管
                }
            }

            cbJob1.SelectedIndex = 0;
            LengendBase lengendBase = null;
            if (WarriorList.Count == 0)
            {
                WarriorList = new List<LengendBase>();
                int ac = 0, ac2 = 0, mac = 0, mac2 = 0, dc = 0, dcmax = 1, hp = 14, mp = 12, bgw = 47, wear = 14, wrist = 11;
                for (int i = 1; i <= 100; i++)
                {
                    //防御计算
                    if (i % 7 == 0)
                    {
                        ac2++;
                        if ((i + 7) % 9 == 0) ac++;
                    }
                    //魔御计算
                    if (i % 12 == 0)
                    {
                        mac2++;
                        if ((i + 15) % 15 == 0) mac++;
                    }
                    //攻击力计算
                    if (i % 5 == 0)
                    {
                        dcmax++;
                        if ((i + 5) % 5 == 0) dc++;
                    }

                    hp += i - 1 + 5;
                    mp += 3;
                    bgw += 3;
                    wear += 1;
                    wrist += 1;

                    lengendBase = new LengendBase()
                    {
                        Level = i,
                        Job = cbJob1.Text,
                        LvExp = i * 100,
                        Ac = ac,
                        Ac2 = ac2,
                        Mac = mac,
                        Mac2 = mac2,
                        Dc = dc,
                        DcMax = dcmax,
                        Hp = hp,
                        Mp = mp,
                        Bgw = bgw,
                        Wear = wear,
                        Wrist = wrist
                    };
                    WarriorList.Add(lengendBase);
                    lbLevel1.Items.Add($"{i}级");

                }
            }
            else
            {
                var items = new string[WarriorList.Count];
                int i = 0;
                WarriorList.ForEach(w =>
                {
                    items[i] = $"{w.Level}级";
                    i++;
                });
                lbLevel1.BeginUpdate();
                lbLevel1.Items.AddRange(items);
                lbLevel1.EndUpdate();

            }
            lengendBase = WarriorList[0];
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Rows.Insert(0, "经验值", lengendBase.LvExp);
                dataGridView1.Rows.Insert(1, "防御", $"{lengendBase.Ac}/{lengendBase.Ac2}");
                dataGridView1.Rows.Insert(2, "魔防", $"{lengendBase.Mac}/{lengendBase.Mac2}");
                dataGridView1.Rows.Insert(3, "攻击力", $"{lengendBase.Dc}/{lengendBase.DcMax}");
                dataGridView1.Rows.Insert(4, "魔法", $"{lengendBase.Mc}/{lengendBase.McMax}");
                dataGridView1.Rows.Insert(5, "道术", $"{lengendBase.Sc}/{lengendBase.ScMax}");
                dataGridView1.Rows.Insert(6, "生命值", lengendBase.Hp);
                dataGridView1.Rows.Insert(7, "魔法值", lengendBase.Mp);
                dataGridView1.Rows.Insert(8, "背包", lengendBase.Bgw);
                dataGridView1.Rows.Insert(9, "负重", lengendBase.Wear);
                dataGridView1.Rows.Insert(10, "腕力", lengendBase.Wrist);
                dataGridView1.CellValueChanged += CellValueChanged;
            }


            lbLevel1.SelectedIndex = 0;
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string cellstr = cell.FormattedValue.ToString();
            int num1 = 0, num2 = 0;
            if (cellContent.IndexOf('/') > 0)
            {
                if (cellstr.IndexOf('/') < 1)
                {
                    cell.Value = cellContent;
                    MessageBox.Show("数据填写错误!");
                    return;
                }
            }
            else
            {
                bool flag = false;

                if (cellstr.IndexOf('/') >= 1)
                {
                    flag = int.TryParse(cellstr.Split('/')[0], out num1);
                    flag = int.TryParse(cellstr.Split('/')[1], out num2);
                }
                else
                {
                    flag = int.TryParse(cellstr, out num1);
                }

                if (!flag)
                {
                    cell.Value = cellContent;
                    MessageBox.Show("数据填写错误!");
                    return;
                }
            }
            string cbText = cbJob1.Text;
            switch (cbText)
            {
                case "战士":
                    switch (e.RowIndex)
                    {
                        case 0: WarriorList[lbLevel1.SelectedIndex].LvExp = num1; break;//经验值
                        case 1: WarriorList[lbLevel1.SelectedIndex].Ac = num1; WarriorList[lbLevel1.SelectedIndex].Ac2 = num2; break;//防御
                        case 2: WarriorList[lbLevel1.SelectedIndex].Mac = num1; WarriorList[lbLevel1.SelectedIndex].Mac2 = num2; break;//魔防
                        case 3: WarriorList[lbLevel1.SelectedIndex].Dc = num1; WarriorList[lbLevel1.SelectedIndex].DcMax = num2; break;//攻击力
                        case 4: WarriorList[lbLevel1.SelectedIndex].Mc = num1; WarriorList[lbLevel1.SelectedIndex].McMax = num2; break;//魔法
                        case 5: WarriorList[lbLevel1.SelectedIndex].Sc = num1; WarriorList[lbLevel1.SelectedIndex].ScMax = num2; break;//道术
                        case 6: WarriorList[lbLevel1.SelectedIndex].Hp = num1; break;//生命值
                        case 7: WarriorList[lbLevel1.SelectedIndex].Mp = num1; break;//魔法值
                        case 8: WarriorList[lbLevel1.SelectedIndex].Bgw = num1; break;//背包
                        case 9: WarriorList[lbLevel1.SelectedIndex].Wear = num1; break;//负重
                        case 10: WarriorList[lbLevel1.SelectedIndex].Wrist = num1; break;//腕力
                    }
                    break;
                case "法师":
                    switch (e.RowIndex)
                    {
                        case 0: WizardList[lbLevel1.SelectedIndex].LvExp = num1; break;//经验值
                        case 1: WizardList[lbLevel1.SelectedIndex].Ac = num1; WizardList[lbLevel1.SelectedIndex].Ac2 = num2; break;//防御
                        case 2: WizardList[lbLevel1.SelectedIndex].Mac = num1; WizardList[lbLevel1.SelectedIndex].Mac2 = num2; break;//魔防
                        case 3: WizardList[lbLevel1.SelectedIndex].Dc = num1; WizardList[lbLevel1.SelectedIndex].DcMax = num2; break;//攻击力
                        case 4: WizardList[lbLevel1.SelectedIndex].Mc = num1; WizardList[lbLevel1.SelectedIndex].McMax = num2; break;//魔法
                        case 5: WizardList[lbLevel1.SelectedIndex].Sc = num1; WizardList[lbLevel1.SelectedIndex].ScMax = num2; break;//道术
                        case 6: WizardList[lbLevel1.SelectedIndex].Hp = num1; break;//生命值
                        case 7: WizardList[lbLevel1.SelectedIndex].Mp = num1; break;//魔法值
                        case 8: WizardList[lbLevel1.SelectedIndex].Bgw = num1; break;//背包
                        case 9: WizardList[lbLevel1.SelectedIndex].Wear = num1; break;//负重
                        case 10: WizardList[lbLevel1.SelectedIndex].Wrist = num1; break;//腕力
                    }
                    break;
                case "道士":
                    switch (e.RowIndex)
                    {
                        case 0: TaoistList[lbLevel1.SelectedIndex].LvExp = num1; break;//经验值
                        case 1: TaoistList[lbLevel1.SelectedIndex].Ac = num1; TaoistList[lbLevel1.SelectedIndex].Ac2 = num2; break;//防御
                        case 2: TaoistList[lbLevel1.SelectedIndex].Mac = num1; TaoistList[lbLevel1.SelectedIndex].Mac2 = num2; break;//魔防
                        case 3: TaoistList[lbLevel1.SelectedIndex].Dc = num1; TaoistList[lbLevel1.SelectedIndex].DcMax = num2; break;//攻击力
                        case 4: TaoistList[lbLevel1.SelectedIndex].Mc = num1; TaoistList[lbLevel1.SelectedIndex].McMax = num2; break;//魔法
                        case 5: TaoistList[lbLevel1.SelectedIndex].Sc = num1; TaoistList[lbLevel1.SelectedIndex].ScMax = num2; break;//道术
                        case 6: TaoistList[lbLevel1.SelectedIndex].Hp = num1; break;//生命值
                        case 7: TaoistList[lbLevel1.SelectedIndex].Mp = num1; break;//魔法值
                        case 8: TaoistList[lbLevel1.SelectedIndex].Bgw = num1; break;//背包
                        case 9: TaoistList[lbLevel1.SelectedIndex].Wear = num1; break;//负重
                        case 10: TaoistList[lbLevel1.SelectedIndex].Wrist = num1; break;//腕力
                    }
                    break;
            }


        }

        private void btnTab1_Click(object sender, EventArgs e)
        {
            if (rbLevel1.Checked)
            {
                string warrior = Api.JsonSerialize<List<LengendBase>>(WarriorList);
                string wizard = Api.JsonSerialize<List<LengendBase>>(WizardList);
                string taoist = Api.JsonSerialize<List<LengendBase>>(TaoistList);

                string warriorPath = LegendFilePath.UserAttrPath + "战士.txt";
                FileHelper.DeleteFile(warriorPath);
                FileHelper.WriteLine(warriorPath, warrior);

                string wizardPath = LegendFilePath.UserAttrPath + "法师.txt";
                FileHelper.DeleteFile(wizardPath);
                FileHelper.WriteLine(wizardPath, wizard);

                string taoistPath = LegendFilePath.UserAttrPath + "道士.txt";
                FileHelper.DeleteFile(taoistPath);
                FileHelper.WriteLine(taoistPath, taoist);
                MessageBox.Show("保存成功!");
            }

            if (rbLevel2.Checked)
            {
                MessageBox.Show("嘿!这个功能貌似没多大用!");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex > 0)
            {
                cellContent = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: BindtabPage0Data(); break;//加载属性数据
                case 1: BindtabPage1Data(); break;//加载技能数据
                case 2: BindtabPage2Data(); break;//加载怪物数据
                case 3: BindtabPage3Data(); break;//加载物品数据
                case 4: BindtabPage4Data(); break;//加载地图数据
                case 5: BindtabPage5Data(); break;//加载怪物爆率数据
                case 6: BindtabPage6Data(); break;//加载用户数据
            }
            cbBookJob.SelectedIndex = 0;
        }

        private void BindtabPage6Data()
        {
            string name = txtUserName.Text;
            string[] iniFiles = Directory.GetFiles(LegendFilePath.UserPath, "*.ini", SearchOption.AllDirectories);
            string SignInPath = LegendFilePath.SignInPath + DateTime.Now.ToString("yyyyMMdd") + "_file.ini";
            IniFileHelper SignInINI = new IniFileHelper(SignInPath);


            IniFileHelper userIni;
            UserInfo user;
            UserInfo_Manager user_Manager;
            List<UserInfo_Manager> userList = new List<UserInfo_Manager>();
            string qq = "";
            foreach (string ini in iniFiles)
            {
                userIni = new IniFileHelper(ini);
                qq = userIni.Read("人物信息", "帐号");
                user = UserInfoApi.GetInfoByQQ(qq);
                if (user != null)
                {
                    user_Manager = new UserInfo_Manager
                    {
                        Id = user.Id,
                        JB = user.User_General.JB,
                        JGS = user.User_General.JGS,
                        PkPoint = user.User_General.PkPoint,
                        REP = user.User_General.REP,
                        Lv = user.User_General.Lv,
                        YB = user.User_General.YB,
                        Job = user.Job,
                        UserStatus = "离线",
                        KillCityMonNum = user.KillCityMonNum,
                        KillMonNum = user.KillMonNum,
                        Map = user.Map,
                        Name = user.Name,
                        RoleType = user.RoleType,
                        Sex = user.Sex,
                        UserQQ = user.UserQQ,
                    };

                    int.TryParse(userIni.Read("人物信息", "杀怪数量"), out int killMonNum);
                    user_Manager.KillMonNum = killMonNum;

                    if (SignInINI.Read("用户签到列表", qq) == "成功签到")
                    {
                        user_Manager.UserStatus = "在线";
                    }

                    userList.Add(user_Manager);
                }
            }
            dataGridView8.DataSource = userList.OrderByDescending(x => x.UserStatus).ThenBy(x => x.Id).ToList();
        }

        private void BindtabPage1Data()
        {
            magicDBList = LegendApi.GetMagicList();
            dataGridView2.DataSource = magicDBList;
        }
        private void BindtabPage2Data()
        {
            monsterDBList = LegendApi.GetMonsterList();
            dataGridView3.DataSource = monsterDBList;
        }

        private void BindtabPage3Data()
        {
            stditemDBList = LegendApi.GetStdItemsList();
            for (int i = 0; i < stditemDBList.Count; i++)
            {
                try
                {
                    if (File.Exists(stditemDBList[i].StdImgPath))
                    {

                        stditemDBList[i].StdImg = System.Drawing.Image.FromFile(stditemDBList[i].StdImgPath);
                    }
                    else
                    {
                        stditemDBList[i].StdImgPath = stditemDBList[i].Looks.ToString().PadLeft(6, '0') + ".png";
                        string path = LegendFilePath.LegendIMGDataPath + "Items\\" + stditemDBList[i].StdImgPath;
                        if (File.Exists(path)) stditemDBList[i].StdImg = System.Drawing.Image.FromFile(path);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("物品[" + stditemDBList[i].StdName + "]没有找到编号(" + stditemDBList[i].Looks + ")");
                }
            }
            dataGridView4.DataSource = stditemDBList;
            cbItem.SelectedIndex = 0;
        }

        private void BindtabPage4Data()
        {
            var map = new LengendMapInfo()
            {
                LMI_MapName = "顶级地图",
                LMI_ID = 1,
                LMI_MapCode = "0",
            };
            mapInfoList.Add(map);

            string mapPath = LegendFilePath.MapPath + "MapInfo.txt";
            if (File.Exists(mapPath))
            {
                string s = FileHelper.ReadTxt(mapPath);
                if (!string.IsNullOrEmpty(s))
                {
                    mapInfoList = Api.JsonDeserialize<List<LengendMapInfo>>(s);
                }
            }

            LoadCombox();
            tvMap.Nodes.Clear();
            LoadTree(0, (TreeNode)null);
            LoadMonster();
        }

        private void BindtabPage5Data()
        {
            monsterDBList = LegendApi.GetMonsterList();
            dataGridView6.AutoGenerateColumns = false;
            dataGridView6.DataSource = monsterDBList;


            BindGridView7();
        }

        private void BindGridView7()
        {

            var stdDBList = LegendApi.GetStdItemsList();
            MonQuest mq = null;
            int i = 1;
            monQuestList = new List<MonQuest>();
            foreach (var std in stdDBList)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(LegendFilePath.LegendIMGDataPath + "Items\\" + std.StdImgPath);
                mq = new MonQuest()
                {
                    MQ_ID = i++,
                    MQ_StdImg = img,
                    MQ_StdName = std.StdName,
                    StdImgPath = std.StdImgPath,
                    MQ_StdID = std.StdID,
                };
                monQuestList.Add(mq);
            }

            if (listMonQuest.Count == 0)
            {
                listMonQuest = LegendApi.GetMonQuestList();
                if (listMonQuest == null) listMonQuest = new List<MonQuest>();

                monQuestList.RemoveAll(y => listMonQuest.Where(t => t.MQ_MonID == 1).Select(t => t.MQ_ID).ToList().Contains(y.MQ_ID));
                monQuestList.AddRange(listMonQuest.Where(t => t.MQ_MonID == 1).ToList());
                monQuestList.ForEach(y =>
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(LegendFilePath.LegendIMGDataPath + "Items\\" + y.StdImgPath);
                    y.MQ_StdImg = img;
                });
            }
            dataGridView7.AutoGenerateColumns = false;
            dataGridView7.DataSource = monQuestList.OrderByDescending(t => t.MQ_IsChecked).ThenBy(t => t.MQ_ID).ToList();
        }


        private void LoadCombox()
        {
            mapInfoList.ForEach(s =>
            {
                s.LMI_DisplayMember = $"{s.LMI_MapName}({s.LMI_MapCode})";
            });
            cbParentArea.DataSource = null;
            cbParentArea.DataSource = mapInfoList;
            cbParentArea.DisplayMember = "LMI_DisplayMember";
            cbParentArea.ValueMember = "LMI_ID";
        }

        private void LoadTree(int pid, TreeNode pNode)
        {
            try
            {
                //查询当前pid下是否有子节点
                List<LengendMapInfo> maps = new List<LengendMapInfo>();
                if (pNode == null)
                {
                    maps = mapInfoList.Where(s => s.LMI_PID == pid).ToList();
                }
                else
                {
                    maps = mapInfoList.Where(s => s.LMI_PID == Convert.ToInt32(pNode.Name)).ToList();
                }
                foreach (LengendMapInfo map in maps)   //遍历填充节点的所有子节点,如果传入的节点node为叶子节点,遍历要退出,不再进行递归
                {
                    TreeNode no = new TreeNode()
                    {
                        Name = map.LMI_ID.ToString(),
                        Text = $"{map.LMI_MapName}(编号:{map.LMI_MapCode})",
                        Tag = map,
                    };

                    LoadTree(map.LMI_PID, no);  //填充no节点的子节点
                    if (pNode == null)
                    {
                        tvMap.Nodes.Add(no);
                    }
                    else
                    {
                        pNode.Nodes.Add(no);
                    }
                }

                tvMap.EndUpdate();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void cbBookJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bookName = txtBook.Text;

            switch (cbBookJob.Text)
            {
                case "所有": dataGridView2.DataSource = magicDBList.Where(s => s.MagName.Contains(bookName)).ToList(); break;
                case "战士": dataGridView2.DataSource = magicDBList.Where(s => s.Job == "战士" && s.MagName.Contains(bookName)).ToList(); break;
                case "法师": dataGridView2.DataSource = magicDBList.Where(s => s.Job == "法师" && s.MagName.Contains(bookName)).ToList(); break;
                case "道士": dataGridView2.DataSource = magicDBList.Where(s => s.Job == "道士" && s.MagName.Contains(bookName)).ToList(); break;
            }

        }

        private void btnBookSearch_Click(object sender, EventArgs e)
        {
            string bookName = txtBook.Text;

            switch (cbBookJob.Text)
            {
                case "所有": dataGridView2.DataSource = magicDBList.Where(s => s.MagName.Contains(bookName)).ToList(); break;
                case "战士": dataGridView2.DataSource = magicDBList.Where(s => s.Job == "战士" && s.MagName.Contains(bookName)).ToList(); break;
                case "法师": dataGridView2.DataSource = magicDBList.Where(s => s.Job == "法师" && s.MagName.Contains(bookName)).ToList(); break;
                case "道士": dataGridView2.DataSource = magicDBList.Where(s => s.Job == "道士" && s.MagName.Contains(bookName)).ToList(); break;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;

                DataGridViewCell cell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellstr = cell.FormattedValue.ToString();
                string colName = dataGridView2.CurrentCell.OwningColumn.Name;
                switch (colName)
                {
                    case "Job": magicDBList[e.RowIndex].Job = cellstr; break;
                    case "MagName": magicDBList[e.RowIndex].MagName = cellstr; break;
                    case "Spell": magicDBList[e.RowIndex].Spell = Convert.ToInt32(cellstr); break;
                    case "Power": magicDBList[e.RowIndex].Power = Convert.ToInt32(cellstr); break;
                    case "MaxPower": magicDBList[e.RowIndex].MaxPower = Convert.ToInt32(cellstr); break;
                    case "DefSpell": magicDBList[e.RowIndex].DefSpell = Convert.ToInt32(cellstr); break;
                    case "DefPower": magicDBList[e.RowIndex].DefPower = Convert.ToInt32(cellstr); break;
                    case "DefMaxPower": magicDBList[e.RowIndex].DefMaxPower = Convert.ToInt32(cellstr); break;
                    case "NeedL1": magicDBList[e.RowIndex].NeedL1 = Convert.ToInt32(cellstr); break;
                    case "NeedL2": magicDBList[e.RowIndex].NeedL2 = Convert.ToInt32(cellstr); break;
                    case "NeedL3": magicDBList[e.RowIndex].NeedL3 = Convert.ToInt32(cellstr); break;
                    case "NeedLN1": magicDBList[e.RowIndex].NeedLN1 = Convert.ToInt32(cellstr); break;
                    case "NeedLN2": magicDBList[e.RowIndex].NeedLN2 = Convert.ToInt32(cellstr); break;
                    case "NeedLN3": magicDBList[e.RowIndex].NeedLN3 = Convert.ToInt32(cellstr); break;
                    case "Descr": magicDBList[e.RowIndex].Descr = cellstr; break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBookSave_Click(object sender, EventArgs e)
        {
            string magic = Api.JsonSerialize(magicDBList);
            string magicPath = LegendFilePath.Mud2Path + "magic.txt";
            FileHelper.DeleteFile(magicPath);
            FileHelper.WriteLine(magicPath, magic);

            MessageBox.Show("保存成功!");
        }

        private void btnMonSearch_Click(object sender, EventArgs e)
        {
            string MonName = txtMon.Text;
            dataGridView3.DataSource = monsterDBList;
            if (!string.IsNullOrEmpty(MonName))
            {
                dataGridView3.DataSource = monsterDBList.Where(s => s.MonName.Contains(MonName)).ToList();
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;

                DataGridViewCell cell = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellstr = cell.FormattedValue.ToString();
                string colName = dataGridView3.CurrentCell.OwningColumn.Name;
                switch (colName)
                {
                    case "MonName": monsterDBList[e.RowIndex].MonName = cellstr; break;
                    case "Lvl": monsterDBList[e.RowIndex].Lvl = Convert.ToInt32(cellstr); break;
                    case "Exp": monsterDBList[e.RowIndex].Exp = Convert.ToInt32(cellstr); break;
                    case "HP": monsterDBList[e.RowIndex].HP = Convert.ToInt32(cellstr); break;
                    case "MP": monsterDBList[e.RowIndex].MP = Convert.ToInt32(cellstr); break;
                    case "AC": monsterDBList[e.RowIndex].AC = Convert.ToInt32(cellstr); break;
                    case "MAC": monsterDBList[e.RowIndex].MAC = Convert.ToInt32(cellstr); break;
                    case "Dc": monsterDBList[e.RowIndex].Dc = Convert.ToInt32(cellstr); break;
                    case "MC": monsterDBList[e.RowIndex].MC = Convert.ToInt32(cellstr); break;
                    case "SC": monsterDBList[e.RowIndex].SC = Convert.ToInt32(cellstr); break;
                    case "Hit": monsterDBList[e.RowIndex].Hit = Convert.ToInt32(cellstr); break;
                    case "MonDescr": monsterDBList[e.RowIndex].MonDescr = cellstr; break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ofdMain.Multiselect = false;
            ofdMain.Filter = "文本文件(*.txt)|*.txt";
            ofdMain.Title = "文件内容请查看怪物示例数据文件";
            //设置默认打开路径(绝对路径)
            ofdMain.InitialDirectory = LegendFilePath.Mud2Path;
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofdMain.FileName;
                List<string> list = FileHelper.GetFileInfo(fileName);
                MonsterDB monsterDB = null;
                monsterDBList = new List<MonsterDB>();
                for (int i = 0; i < list.Count; i++)
                {
                    string[] strArray = null;
                    if (list[i].IndexOf(',') > 0)
                        strArray = list[i].Split(',');
                    if (list[i].IndexOf(';') > 0)
                        strArray = list[i].Split(';');

                    monsterDB = new MonsterDB
                    {
                        MonID = MonID++,
                        MonName = strArray[0],
                        Appr = Convert.ToInt32(strArray[3]),
                        Lvl = Convert.ToInt32(strArray[4]),
                        Exp = Convert.ToInt32(strArray[7]),
                        HP = Convert.ToInt32(strArray[8]),
                        MP = Convert.ToInt32(strArray[10]),
                        AC = Convert.ToInt32(strArray[11]),
                        MAC = Convert.ToInt32(strArray[12]),
                        Dc = Convert.ToInt32(strArray[13]),
                        Dc2 = Convert.ToInt32(strArray[14]),
                        MC = Convert.ToInt32(strArray[15]),
                        SC = Convert.ToInt32(strArray[16]),
                        Hit = Convert.ToInt32(strArray[17])
                    };
                    monsterDBList.Add(monsterDB);
                }
                dataGridView3.DataSource = monsterDBList;

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 获取选中的文件路径
            string filePath = LegendFilePath.Mud2Path + "怪物示例数据.txt";

            // 在这里添加你打开文件的逻辑,比如用默认程序打开
            System.Diagnostics.Process.Start(filePath);
        }

        private void btnMonClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有怪物数据?", "数据清空", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                monsterDBList = new List<MonsterDB>();
                dataGridView3.DataSource = monsterDBList;
            }
        }

        private void btnMonSave_Click(object sender, EventArgs e)
        {
            string mon = Api.JsonSerialize(monsterDBList);
            string monPath = LegendFilePath.Mud2Path + "monster.txt";
            FileHelper.DeleteFile(monPath);
            FileHelper.WriteLine(monPath, mon);

            MessageBox.Show("保存成功!");
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGridView4();
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            BindDataGridView4();
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;

                DataGridViewCell cell = dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellstr = cell.FormattedValue.ToString();
                string colName = dataGridView4.CurrentCell.OwningColumn.Name;
                switch (colName)
                {
                    case "StdName": stditemDBList[e.RowIndex].StdName = cellstr; break;
                    case "StdMode": stditemDBList[e.RowIndex].StdMode = Convert.ToInt32(cellstr); break;
                    case "Weight": stditemDBList[e.RowIndex].Weight = Convert.ToInt32(cellstr); break;
                    case "Looks": stditemDBList[e.RowIndex].Looks = Convert.ToInt32(cellstr); break;
                    case "Duramax": stditemDBList[e.RowIndex].Duramax = Convert.ToInt32(cellstr); break;
                    case "StdAC": stditemDBList[e.RowIndex].StdAC = Convert.ToInt32(cellstr); break;
                    case "StdAC2": stditemDBList[e.RowIndex].StdAC2 = Convert.ToInt32(cellstr); break;
                    case "StdMac": stditemDBList[e.RowIndex].StdMac = Convert.ToInt32(cellstr); break;
                    case "StdMac2": stditemDBList[e.RowIndex].StdMac2 = Convert.ToInt32(cellstr); break;
                    case "StdDc": stditemDBList[e.RowIndex].StdDc = Convert.ToInt32(cellstr); break;
                    case "StdDc2": stditemDBList[e.RowIndex].StdDc2 = Convert.ToInt32(cellstr); break;
                    case "StdMc": stditemDBList[e.RowIndex].StdMc = Convert.ToInt32(cellstr); break;
                    case "StdMc2": stditemDBList[e.RowIndex].StdMc2 = Convert.ToInt32(cellstr); break;
                    case "StdSc": stditemDBList[e.RowIndex].StdSc = Convert.ToInt32(cellstr); break;
                    case "StdSc2": stditemDBList[e.RowIndex].StdSc2 = Convert.ToInt32(cellstr); break;
                    case "Need": stditemDBList[e.RowIndex].Need = Convert.ToInt32(cellstr); break;
                    case "Needlevel": stditemDBList[e.RowIndex].Needlevel = Convert.ToInt32(cellstr); break;
                    case "Price": stditemDBList[e.RowIndex].Price = Convert.ToInt32(cellstr); break;
                    case "OverLap": stditemDBList[e.RowIndex].OverLap = Convert.ToInt32(cellstr); break;
                    case "StdHP": stditemDBList[e.RowIndex].StdHP = Convert.ToInt32(cellstr); break;
                    case "StdMP": stditemDBList[e.RowIndex].StdMP = Convert.ToInt32(cellstr); break;
                    case "Accuracy": stditemDBList[e.RowIndex].Accuracy = Convert.ToInt32(cellstr); break;
                    case "Agile": stditemDBList[e.RowIndex].Agile = Convert.ToInt32(cellstr); break;
                    case "Lucky": stditemDBList[e.RowIndex].Lucky = Convert.ToInt32(cellstr); break;
                    case "Strength": stditemDBList[e.RowIndex].Strength = Convert.ToInt32(cellstr); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnItemImport_Click(object sender, EventArgs e)
        {
            ofdMain.Multiselect = false;
            ofdMain.Filter = "文本文件(*.txt)|*.txt";
            ofdMain.Title = "文件内容请查看怪物示例数据文件";
            //设置默认打开路径(绝对路径)
            ofdMain.InitialDirectory = LegendFilePath.Mud2Path;
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = ofdMain.FileName;
                    string data = FileHelper.ReadTxt(fileName);
                    stditemDBList = Api.JsonDeserialize<List<StditemDB>>(data);
                    stditemDBList.ForEach(s =>
                    {
                        if (!string.IsNullOrEmpty(s.StdImgPath))
                        {
                            s.StdImg = System.Drawing.Image.FromFile(LegendFilePath.LegendIMGDataPath + "Items\\" + s.StdImgPath);
                        }
                        else
                        {

                        }

                    });

                    dataGridView4.DataSource = stditemDBList;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 获取选中的文件路径
            string filePath = LegendFilePath.Mud2Path + "物品示例数据.txt";

            // 在这里添加你打开文件的逻辑,比如用默认程序打开
            System.Diagnostics.Process.Start(filePath);
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有物品数据?", "数据清空", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                stditemDBList = new List<StditemDB>();
                dataGridView4.DataSource = stditemDBList;
            }
        }

        private void btnItemSave_Click(object sender, EventArgs e)
        {
            foreach (var item in stditemDBList)
            {
                item.StdImg = null;
            }
            string std = Api.JsonSerialize(stditemDBList);
            string stdPath = LegendFilePath.Mud2Path + "stditem.txt";
            FileHelper.DeleteFile(stdPath);
            FileHelper.WriteLine(stdPath, std);

            MessageBox.Show("保存成功!");
        }

        private void btnAreaSave_Click(object sender, EventArgs e)
        {
            try
            {
                string mapName = txtMapName.Text;
                string mapCode = txtMapCode.Text;
                if (string.IsNullOrEmpty(mapName))
                {
                    MessageBox.Show("地图名称不能为空!");
                    return;
                }
                if (string.IsNullOrEmpty(mapCode))
                {
                    MessageBox.Show("地图编号不能为空!");
                    return;
                }
                int maxId = mapInfoList.Max(s => s.LMI_ID);
                LengendMapInfo mapInfo = cbParentArea.SelectedItem as LengendMapInfo;
                if (mapInfo == null)
                {
                    mapInfo = new LengendMapInfo()
                    {
                        LMI_ID = maxId + 1,
                        LMI_MapName = mapName,
                        LMI_MapCode = mapCode,
                        LMI_PID = 0,
                        LMI_FontSize = 10,
                        LMI_Xpoint = 0,
                        LMI_Ypoint = 0,
                        LMI_FrontHeight = 10,
                        LMI_FrontWidth = 10,
                        LMI_DisplayMember = $"{mapName}({mapCode})"
                    };
                }
                else
                {
                    mapInfo = new LengendMapInfo()
                    {
                        LMI_ID = maxId + 1,
                        LMI_MapName = mapName,
                        LMI_MapCode = mapCode,
                        LMI_PID = mapInfo.LMI_ID,
                        LMI_FontSize = 10,
                        LMI_Xpoint = 0,
                        LMI_Ypoint = 0,
                        LMI_FrontHeight = 10,
                        LMI_FrontWidth = 10,
                        LMI_DisplayMember = $"{mapName}({mapCode})"
                    };
                }

                int count = mapInfoList.Where(s => s.LMI_MapCode == mapInfo.LMI_MapCode).Count();
                if (count > 0)
                {
                    MessageBox.Show("当前地图编号已存在!");
                    return;
                }

                mapInfoList.Add(mapInfo);

                LoadCombox();
                tvMap.Nodes.Clear();
                LoadTree(0, (TreeNode)null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvMap_DoubleClick(object sender, EventArgs e)
        {
            MapEdit();
        }
        private void MapEdit()
        {
            var mapInfo = tvMap.SelectedNode.Tag as LengendMapInfo;

            if (mapInfo != null)
            {
                if (mapInfo.LMI_MapCode == "0")
                {
                    MessageBox.Show("不能修改顶级地图!");
                    return;
                }

                FrmEditMap frmEditMap = new FrmEditMap();
                frmEditMap.MapInfo = mapInfo;
                DialogResult dr = frmEditMap.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    tvMap.SelectedNode.Text = $"{frmEditMap.MapInfo.LMI_MapName}(编号:{frmEditMap.MapInfo.LMI_MapCode})";
                }
            }
            else
            {
                MessageBox.Show("异常问题,请重新操作.");
            }
        }

        private void pbAarea_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = tvMap.SelectedNode;
                if (node == null)
                {
                    MessageBox.Show("请先选择左侧地图名称再添加图片.");
                    return;
                }

                var m = tvMap.SelectedNode.Tag as LengendMapInfo;

                ofdMain.Multiselect = false;
                ofdMain.Filter = "图片文件(*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png";
                ofdMain.Title = "只能选择图片文件类型";
                ofdMain.FilterIndex = 1;
                ofdMain.RestoreDirectory = true;
                //设置默认打开路径(绝对路径)
                ofdMain.InitialDirectory = LegendFilePath.LegendIMGDataPath;
                if (ofdMain.ShowDialog() == DialogResult.OK)
                {
                    string fileName = ofdMain.FileName;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
                    pbAarea.Image = img;

                    foreach (var map in mapInfoList)
                    {
                        if (map.LMI_ID == m.LMI_ID)
                        {
                            map.LMI_MapFile = fileName;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAreaMon_Click(object sender, EventArgs e)
        {
            TreeNode node = tvMap.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请先选择左侧地图再进行操作.");
                return;
            }
            else
            {
                var lengedMapInfo = node.Tag as LengendMapInfo;
                List<LegMapMonInfo> legMapMons = new List<LegMapMonInfo>();
                string mapPath = LegendFilePath.MapPath + "MapMonInfo.txt";
                if (File.Exists(mapPath))
                {
                    string s = FileHelper.ReadTxt(mapPath);
                    if (!string.IsNullOrEmpty(s))
                    {
                        legMapMons = Api.JsonDeserialize<List<LegMapMonInfo>>(s);
                        legMapMons = legMapMons.Where(l => l.LMI_ID != lengedMapInfo.LMI_ID).ToList();// 将原来本地图的怪都清理掉
                    }
                }
                // 重新加勾选的怪物
                var mmifList = dataGridView5.DataSource as List<LegMapMonInfo>;
                mmifList = mmifList.Where(m => m.IsCheckedMon).ToList();
                for (int i = 0; i < mmifList.Count; i++)
                {
                    mmifList[i].LMI_ID = lengedMapInfo.LMI_ID;
                    mmifList[i].MonSort = i + 1;
                    legMapMons.Add(mmifList[i]);
                }

                string lmm = Api.JsonSerialize(legMapMons);
                if (File.Exists(mapPath))
                {
                    FileHelper.DeleteFile(mapPath);
                }

                FileHelper.WriteLine(mapPath, lmm);

                MessageBox.Show("操作成功!");
                BindMapMonster();
            }
        }

        private void BindMapMonster()
        {
            List<LegMapMonInfo> legMapMons = new List<LegMapMonInfo>();
            string mapPath = LegendFilePath.MapPath + "MapMonInfo.txt";
            if (File.Exists(mapPath))
            {

                string s = FileHelper.ReadTxt(mapPath);
                if (!string.IsNullOrEmpty(s))
                {
                    legMapMons = Api.JsonDeserialize<List<LegMapMonInfo>>(s);
                    legMapMons = legMapMons.Where(t => t.LMI_ID == MapInfo.LMI_ID).ToList();
                }
            }
            var lgMapMons = legMapMonInfos.ToList();
            for (int i = 0; i < lgMapMons.Count; i++)
            {
                var lmm = legMapMons.Where(s => s.MonID == lgMapMons[i].MonID).FirstOrDefault();
                if (lmm != null)
                {
                    lgMapMons[i] = lmm;
                }
            }

            List<LegMapMonInfo> list = new List<LegMapMonInfo>();
            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.DataSource = list;
            dataGridView5.DataSource = lgMapMons.OrderByDescending(s => s.IsCheckedMon).ThenBy(s => s.MonSort).ToList();
        }


        private void cbOtMapName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var map = cbOtMapName.SelectedItem as LengendMapInfo;
            if (map != null)
            {
                txtOtMapName.Text = map.LMI_MapName;
                txtXpoint.Text = map.LMI_Xpoint.ToString();
                txtYpoint.Text = map.LMI_Ypoint.ToString();
                txtFrontHeight.Text = map.LMI_FrontHeight.ToString();
                txtFrontWidth.Text = map.LMI_FrontWidth.ToString();
                txtFontSize.Text = map.LMI_FontSize.ToString();
            }
        }

        private void btnLookBigImg_Click(object sender, EventArgs e)
        {
            TreeNode node = tvMap.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请先选择左侧地图再点击查看.");
                return;
            }
            //DrawFont();

            var lengedMapInfo = tvMap.SelectedNode.Tag as LengendMapInfo;

            FrmImage frmImage = new FrmImage();
            frmImage.lengedMapInfo = lengedMapInfo;
            frmImage.ShowDialog();
        }

        private void btnMapRefresh_Click(object sender, EventArgs e)
        {
            DrawFont();
        }
        /// <summary>
        /// 加载地图中导航字体
        /// </summary>
        private void DrawFont()
        {
            if (MapInfo == null)
            {
                MessageBox.Show("请先选择地图再进行操作!");
                return;
            }
            var map = cbOtMapName.SelectedItem as LengendMapInfo;
            if (map != null)
            {
                map.LMI_Xpoint = Convert.ToInt32(txtXpoint.Text);
                map.LMI_Ypoint = Convert.ToInt32(txtYpoint.Text);
                map.LMI_FrontHeight = Convert.ToInt32(txtFrontHeight.Text);
                map.LMI_FrontWidth = Convert.ToInt32(txtFrontWidth.Text);
                map.LMI_FontSize = Convert.ToInt32(txtFontSize.Text);
                map.LMI_MapName = txtOtMapName.Text;
            }
            string imgPath = LegendFilePath.LegendIMGDataPath + "地图\\" + MapInfo.LMI_MapFile;

            if (!string.IsNullOrEmpty(MapInfo.LMI_MapFile))
            {
                if (File.Exists(MapInfo.LMI_MapFile)) imgPath = MapInfo.LMI_MapFile;
                System.Drawing.Image pbAareaImage = System.Drawing.Image.FromFile(imgPath);  //加载图片
                var maps = mapInfoList.Where(s => s.LMI_PID == MapInfo.LMI_ID).ToList();
                if (maps.Count > 0)
                {
                    using (Bitmap bitmap = new Bitmap(pbAareaImage.Width, pbAareaImage.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.DrawImage(pbAareaImage, 0, 0, pbAareaImage.Width, pbAareaImage.Height);//定义画布
                            for (int i = 0; i < maps.Count; i++)
                            {
                                if (map != null)
                                {
                                    if (maps[i].LMI_ID == map.LMI_ID)
                                    {
                                        maps[i] = map;
                                    }
                                }

                                using (Font font = new Font("Segoe UI", maps[i].LMI_FontSize.Value)) // 创建字体
                                {
                                    // 使用ColorTranslator将HTML颜色代码转换为Color对象
                                    Color color = ColorTranslator.FromHtml("#EBE30C");
                                    using (Brush brush = new SolidBrush(color)) // 创建画刷
                                    {
                                        Rectangle rect = new Rectangle(maps[i].LMI_Xpoint.Value, maps[i].LMI_Ypoint.Value, maps[i].LMI_FrontWidth.Value, maps[i].LMI_FrontHeight.Value); // 设置文本的绘制区域
                                        DrawCenteredString(gh, maps[i].LMI_MapName, font, brush, rect); // 调用方法居中绘制文本
                                    }
                                }
                            }
                        }
                        imgPath = $"{LegendFilePath.LegendIMGDataPath + "地图\\" + MapInfo.LMI_ID}_{MapInfo.LMI_MapName}.png";
                        try
                        {
                            bitmap.Save(imgPath, ImageFormat.Png);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        bindImage(imgPath);
                    }

                }
                else
                {
                    if (File.Exists(imgPath))
                        bindImage(imgPath);
                }
            }
            else
            {
                //暂无图片
            }
        }
        private void bindImage(string imgPath)
        {
            //让pictureBox显示图像,并且不被占用,能即时删除图片
            System.Drawing.Image imgModel = System.Drawing.Image.FromFile(imgPath);   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            imgModel.Save(mstr, ImageFormat.Png);// 保存这个对象

            pbAarea.Image = System.Drawing.Image.FromStream(mstr);

            imgModel.Dispose();//释放占用
        }

        public static void DrawCenteredString(Graphics gh, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center; // 设置水平居中
            format.LineAlignment = StringAlignment.Center; // 设置垂直居中
            gh.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            gh.DrawString(text, font, Brushes.Black, layoutRectangle.X - 1, layoutRectangle.Y, format);
            gh.DrawString(text, font, Brushes.Black, layoutRectangle.X + 1, layoutRectangle.Y, format);
            gh.DrawString(text, font, Brushes.Black, layoutRectangle.X, layoutRectangle.Y - 1, format);
            gh.DrawString(text, font, Brushes.Black, layoutRectangle.X, layoutRectangle.Y + 1, format);

            gh.DrawString(text, font, brush, layoutRectangle.X, layoutRectangle.Y, format);

            //gh.DrawString(text, font, brush, layoutRectangle, format);
        }
        private void btnOtherSave_Click(object sender, EventArgs e)
        {
            foreach (var map in mapInfoList)
            {
                if (!string.IsNullOrEmpty(map.LMI_MapFile))
                {
                    var mapArray = map.LMI_MapFile.Split('\\');
                    map.LMI_MapFile = mapArray[mapArray.Length - 1];
                }
            }
            string mps = Api.JsonSerialize(mapInfoList);
            string mapPath = LegendFilePath.MapPath + "MapInfo.txt";
            if (File.Exists(mapPath))
            {
                FileHelper.DeleteFile(mapPath);
            }

            FileHelper.WriteLine(mapPath, mps);

            MessageBox.Show("保存成功!");
        }

        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            string StdName = txtItemName.Text;
            if (!string.IsNullOrEmpty(StdName))
            {
                stdID = stditemDBList.Count + 1;
                StditemDB stditemDB = new StditemDB()
                {
                    StdID = stdID,
                    StdName = StdName
                };

                stditemDBList.Add(stditemDB);
                List<StditemDB> list = new List<StditemDB>();
                dataGridView4.DataSource = list;
                dataGridView4.DataSource = stditemDBList;
                dataGridView4.Rows[stditemDBList.Count - 1].Selected = true;
                //dataGridView4.Rows.Insert(dgvIndex4++, stditemDB.StdID, stditemDB.StdName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            else
            {
                MessageBox.Show("物品名称不能为空!");
            }
        }

        private void lbLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbLevelSelectIndex1 = lbLevel1.SelectedIndex;
            string cbText = cbJob1.Text;
            LengendBase lb = null;
            switch (cbText)
            {
                case "战士": lb = WarriorList[lbLevelSelectIndex1]; break;
                case "法师": lb = WizardList[lbLevelSelectIndex1]; break;
                case "道士": lb = TaoistList[lbLevelSelectIndex1]; break;
            }
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Insert(0, "经验值", lb.LvExp);
                dataGridView1.Rows.Insert(1, "防御", $"{lb.Ac}/{lb.Ac2}");
                dataGridView1.Rows.Insert(2, "魔防", $"{lb.Mac}/{lb.Mac2}");
                dataGridView1.Rows.Insert(3, "攻击力", $"{lb.Dc}/{lb.DcMax}");
                dataGridView1.Rows.Insert(4, "魔法", $"{lb.Mc}/{lb.McMax}");
                dataGridView1.Rows.Insert(5, "道术", $"{lb.Sc}/{lb.ScMax}");
                dataGridView1.Rows.Insert(6, "生命值", lb.Hp);
                dataGridView1.Rows.Insert(7, "魔法值", lb.Mp);
                dataGridView1.Rows.Insert(8, "背包", lb.Bgw);
                dataGridView1.Rows.Insert(9, "负重", lb.Wear);
                dataGridView1.Rows.Insert(10, "腕力", lb.Wrist);

                //dataGridView1.Rows[0].Cells[1].Value = lb.LvExp;
                //dataGridView1.Rows[1].Cells[1].Value = $"{lb.Ac}/{lb.Ac2}";
                //dataGridView1.Rows[2].Cells[1].Value = $"{lb.Mac}/{lb.Mac2}";
                //dataGridView1.Rows[3].Cells[1].Value = $"{lb.Dc}/{lb.DcMax}";
                //dataGridView1.Rows[4].Cells[1].Value = $"{lb.Mc}/{lb.McMax}";
                //dataGridView1.Rows[5].Cells[1].Value = $"{lb.Sc}/{lb.ScMax}";
                //dataGridView1.Rows[6].Cells[1].Value = lb.Hp;
                //dataGridView1.Rows[7].Cells[1].Value = lb.Mp;
                //dataGridView1.Rows[8].Cells[1].Value = lb.Bgw;
                //dataGridView1.Rows[9].Cells[1].Value = lb.Wear;
                //dataGridView1.Rows[10].Cells[1].Value = lb.Wrist;
            }
        }

        private void tvMap_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                LengendMapInfo map = e.Node.Tag as LengendMapInfo;
                MapInfo = map;

                txtMapName.Text = map.LMI_MapName;
                txtMapCode.Text = map.LMI_MapCode;
                lbMapName.Text = map.LMI_MapName;
                lbMapCode.Text = map.LMI_MapCode;
                lbSelcMapName.Text = map.LMI_MapName;

                var maps = mapInfoList.Where(s => s.LMI_PID == map.LMI_ID).ToList();

                cbOtMapName.DataSource = null;
                cbOtMapName.DataSource = maps;
                cbOtMapName.DisplayMember = "LMI_MapName";
                cbOtMapName.ValueMember = "LMI_ID";

                DrawFont();
                LoadMonster();
                BindMapMonster();
            }
        }

        private void dataGridView7_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            MonsterDB monDB = null;
            var monRow = dataGridView6.SelectedRows;
            if (monRow != null)
            {
                monDB = new MonsterDB
                {
                    MonID = (int)monRow[0].Cells[0].Value,
                    MonName = monRow[0].Cells[1].Value.ToString(),
                    Lvl = (int)monRow[0].Cells[2].Value,
                    HP = (int)monRow[0].Cells[3].Value,
                };
                if (!listMon.Where(s => s.MonID == monDB.MonID).Any())
                    listMon.Add(monDB);
            }

            DataGridViewCell itemCell = dataGridView7.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string colName = dataGridView7.CurrentCell.OwningColumn.Name;

            int MQ_StdID = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_StdID"].Value;
            MonQuest mq = null, monQ = listMonQuest.Where(s => s.MQ_MonID == monDB.MonID && s.MQ_StdID == MQ_StdID).FirstOrDefault();
            int MQ_MonQty = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_MonQty"].Value;

            switch (colName)
            {
                case "MQ_IsChecked":
                    mq = new MonQuest()
                    {
                        MQ_ID = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_ID"].Value,
                        MQ_MonID = monDB.MonID,
                        MQ_StdID = MQ_StdID,
                        StdImgPath = dataGridView7.Rows[e.RowIndex].Cells["MQ_StdImgPath"].Value.ToString(),
                        MQ_StdImg = (System.Drawing.Image)dataGridView7.Rows[e.RowIndex].Cells["MQ_StdImg"].Value,
                        MQ_StdName = dataGridView7.Rows[e.RowIndex].Cells["MQ_StdName"].Value.ToString(),
                        MQ_MonExpRate = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_MonExpRate"].Value,
                        MQ_ItemFalling = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_ItemFalling"].Value,
                        MQ_IsBB = (bool)dataGridView7.Rows[e.RowIndex].Cells["MQ_IsBB"].Value,
                        MQ_MonQty = MQ_MonQty,
                        MQ_IsChecked = true,
                    };
                    if ((bool)itemCell.Value)
                    {
                        listMonQuest.Add(mq);
                    }
                    else
                    {
                        listMonQuest.Remove(monQ);
                    }
                    break;
                case "MQ_MonExpRate":
                    listMonQuest.ForEach(s =>
                    {
                        if (monQ != null)
                        {
                            if (s.MQ_ID == monQ.MQ_ID)
                            {
                                s.MQ_MonExpRate = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_MonExpRate"].Value;
                                return;
                            }
                        }

                    });
                    break;
                case "MQ_IsBB":
                    listMonQuest.ForEach(s =>
                    {
                        if (monQ != null)
                        {
                            if (s.MQ_ID == monQ.MQ_ID)
                            {
                                s.MQ_IsBB = (bool)dataGridView7.Rows[e.RowIndex].Cells["MQ_IsBB"].Value;
                                return;
                            }
                        }
                    });
                    break;
                case "MQ_ItemFalling":
                    listMonQuest.ForEach(s =>
                    {
                        if (monQ != null)
                        {
                            if (s.MQ_ID == monQ.MQ_ID)
                            {
                                s.MQ_ItemFalling = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_ItemFalling"].Value;
                                return;
                            }
                        }
                    });
                    break;
                case "MQ_MonQty":
                    listMonQuest.ForEach(s =>
                    {
                        if (monQ != null)
                        {
                            if (s.MQ_ID == monQ.MQ_ID)
                            {
                                s.MQ_MonQty = (int)dataGridView7.Rows[e.RowIndex].Cells["MQ_MonQty"].Value;
                                return;
                            }
                        }
                    });
                    break;
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            BindGridView7();
            GetMonQuestData();
        }
        private void GetMonQuestData()
        {
            var monRow = dataGridView6.SelectedRows;
            int monID = 0;
            if (monRow != null)
            {
                monID = (int)monRow[0].Cells[0].Value;
            }
            //先吧所有的数据重新查一遍,然后吧对应的物品修改

            string MQ_StdName = txtMQ_StdName.Text.Trim();
            var list = new List<MonQuest>();
            dataGridView7.DataSource = list;
            if (!string.IsNullOrEmpty(MQ_StdName))
            {
                list = monQuestList.Where(s => s.MQ_StdName.Contains(MQ_StdName)).ToList();
                list.AddRange(listMonQuest.Where(s => s.MQ_MonID == monID).ToList());
                dataGridView7.DataSource = list.OrderByDescending(s => s.MQ_IsChecked).ThenBy(s => s.MQ_ID).ToList();
            }
            else
            {
                list = monQuestList.ToList();
                var MonQuestArray = listMonQuest.Where(t => t.MQ_MonID == monID).ToList();

                list.RemoveAll(s => MonQuestArray.Select(t => t.MQ_ID).Contains(s.MQ_ID));

                list.AddRange(MonQuestArray);
                list.ForEach(s =>
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(LegendFilePath.LegendIMGDataPath + "Items\\" + s.StdImgPath);
                    s.MQ_StdImg = img;
                });
                //选中的物品过滤                 
                dataGridView7.DataSource = list.OrderByDescending(s => s.MQ_IsChecked).ThenBy(s => s.MQ_ID).ToList();
            }
        }

        private void btnBLText_Click(object sender, EventArgs e)
        {
            var monRow = dataGridView6.SelectedRows;
            int monID = 0;
            if (monRow != null)
            {
                monID = (int)monRow[0].Cells[0].Value;
            }
            if (monID > 0)
            {
                var list = new List<MonQuest>();
                list = listMonQuest.Where(s => s.MQ_MonID == monID).ToList();
                Random rnd = new Random();
                string result = "";
                //计算爆率
                foreach (var item in list)
                {
                    for (int i = 0; i < item.MQ_MonQty; i++)
                    {
                        if (item.MQ_IsBB)
                        {
                            //必爆物品几率计算,0为该装备必爆,否则计算爆率
                            if (item.MQ_ItemFalling == 0)
                            {
                                result += item.MQ_StdName + ",";
                            }
                            else
                            {
                                //计算概率
                                double min = 0;
                                double max = item.MQ_ItemFalling;
                                double num = rnd.NextDouble() * (max - min) + min;
                                if (num <= 1)
                                {
                                    result += item.MQ_StdName + ",";
                                }
                            }
                        }
                        else
                        {
                            //必爆物品几率计算,0为该装备必爆,否则计算爆率
                            if (item.MQ_MonExpRate == 0)
                            {
                                result += item.MQ_StdName + ",";
                            }
                            else
                            {
                                //计算概率
                                bool isDrop = false;
                                if (item.MQ_MonExpRate == 1)// 1/1的爆率
                                {
                                    isDrop = true;
                                }
                                else
                                {
                                    int roll = rnd.Next(1, item.MQ_MonExpRate + 1);
                                    if (roll == 1) isDrop = true;
                                }
                                if (isDrop)
                                {
                                    result += item.MQ_StdName + ",";
                                }
                            }
                        }
                    }
                }

                lbText.Text = result.TrimEnd(',');
            }
        }

        private void btnMonQSave_Click(object sender, EventArgs e)
        {
            string result = "";
            var list = listMonQuest.ToList();
            foreach (var item in list)
            {
                item.MQ_StdImg = null;
                if (item.MQ_MonQty == 0)
                {
                    result += item.MQ_StdName + ",";
                }
            }
            if (!string.IsNullOrEmpty(result))
            {
                MessageBox.Show("当前物品数量为0,请修改：" + result);
                return;
            }
            string monq = Api.JsonSerialize(list);
            string MonQuestPath = LegendFilePath.MonQuestPath + "MonQuest.txt";
            FileHelper.DeleteFile(MonQuestPath);
            FileHelper.WriteLine(MonQuestPath, monq);

            MessageBox.Show("保存成功!");
        }

        private void dataGridView8_DoubleClick(object sender, EventArgs e)
        {
            string qq = (string)dataGridView8.SelectedRows[0].Cells[2].Value;
            FrmEditUser frmEditUser = new FrmEditUser();
            frmEditUser.UserQQ = qq;
            frmEditUser.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvMap.SelectedNode;
            while (node.FirstNode != null)
            {
                tvMap.Nodes.Remove(node.FirstNode);
            }
        }

        private void btnStdNameSearch_Click(object sender, EventArgs e)
        {
            GetMonQuestData();
        }

        private void btnMonQt_Click(object sender, EventArgs e)
        {
            string MQ_StdName = txtMonQtName.Text.Trim();

            var list = new List<MonsterDB>();
            dataGridView6.DataSource = list;
            dataGridView6.AutoGenerateColumns = false;
            dataGridView6.DataSource = monsterDBList.Where(s => s.MonName.Contains(MQ_StdName)).ToList();
        }

        private void btnSetLevel_Click(object sender, EventArgs e)
        {
            int level = int.Parse(txtLevel.Text);
            SetLevel(level);
        }

        void SetLevel(int level)
        {
            List<LengendBase> listWarrior = new List<LengendBase>();//战士数据
            List<LengendBase> listWizard = new List<LengendBase>();//法师数据
            List<LengendBase> listTaoist = new List<LengendBase>();//道士数据
            LengendBase lengendBase;

            #region 生成战士数据
            int ac = 0, ac2 = 0, mac = 0, mac2 = 0, dc = 0, dcmax = 1, hp = 14, mp = 12, bgw = 47, wear = 14, wrist = 11, exp = 100;
            long lvExp = 0;
            for (int i = 1; i <= level; i++)
            {
                //防御计算
                if (i % 7 == 0)
                {
                    ac2++;
                    if ((i + 7) % 9 == 0) ac++;
                }
                //魔御计算
                if (i % 12 == 0)
                {
                    mac2++;
                    if ((i + 15) % 15 == 0) mac++;
                }
                //攻击力计算
                if (i % 5 == 0)
                {
                    dcmax++;
                    if ((i + 5) % 5 == 0) dc++;
                }
                hp += i - 1 + 5;
                mp += 3;
                bgw += 3;
                wear += 1;
                wrist += 1;
                switch (i)
                {
                    case 1: lvExp = 100; break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: lvExp = exp * i * 2; break;
                    case 11: lvExp = 2200; break;
                    case 12: lvExp = 4000; break;
                    case 13: lvExp = 8000; break;
                    case 14: lvExp = 16000; break;
                    case 15: lvExp = 32000; break;
                    case 16: lvExp = 50000; break;
                    case 17: lvExp = 70000; break;
                    case 18: lvExp = 100000; break;
                    case 19: lvExp = 120000; break;
                    case 20: lvExp = 140000; break;
                    case 21: lvExp = 250000; break;
                    case 22: lvExp = 300000; break;
                    case 23: lvExp = 350000; break;
                    case 24: lvExp = 400000; break;
                    case 25: lvExp = 500000; break;
                    case 26: lvExp = 700000; break;
                    case 27: lvExp = 1000000; break;
                    case 28: lvExp = 1400000; break;
                    case 29: lvExp = 1800000; break;
                    case 30: lvExp = 2000000; break;
                    case 31: lvExp = 2400000; break;
                    case 32: lvExp = 2800000; break;
                    case 33: lvExp = 3200000; break;
                    case 34: lvExp = 3600000; break;
                    case 35: lvExp = 4000000; break;
                    case 36: lvExp = 4800000; break;
                    case 37: lvExp = 5600000; break;
                    case 38: lvExp = 8200000; break;
                    case 39: lvExp = 9000000; break;
                    case 40: lvExp = 24000000; break;
                    case 41: lvExp = 48000000; break;
                    case 42: lvExp = 96000000; break;
                    case 43: lvExp = 200000000; break;
                    case 44: lvExp = 220000000; break;
                    case 45: lvExp = 240000000; break;
                    case 46: lvExp = 260000000; break;
                    case 47: lvExp = 280000000; break;
                    case 48: lvExp = 300000000; break;
                    case 49: lvExp = 1000000000; break;
                    case 50: lvExp = 1200000000; break;
                    case 51: lvExp = 1400000000; break;
                    case 52: lvExp = 1600000000; break;
                    case 53: lvExp = 1800000000; break;
                    case 54: lvExp = 2000000000; break;
                    case 55: lvExp = 2650000000; break;
                    case 56: lvExp = 3250000000; break;
                    case 57: lvExp = 3850000000; break;
                    case 58: lvExp = 4000000000; break;
                    case 59: lvExp = 4200000000; break;
                    case 60: lvExp = 5000000000; break;
                    default: lvExp = 5000000000; break;
                }

                lengendBase = new LengendBase()
                {
                    Level = i,
                    Job = cbJob1.Text,
                    LvExp = lvExp,
                    Ac = ac,
                    Ac2 = ac2,
                    Mac = mac,
                    Mac2 = mac2,
                    Dc = dc,
                    DcMax = dcmax,
                    Hp = hp,
                    Mp = mp,
                    Bgw = bgw,
                    Wear = wear,
                    Wrist = wrist
                };
                listWarrior.Add(lengendBase);
            }
            #endregion

            #region 生成法师数据s
            ac = 0; ac2 = 0; mac = 0; mac2 = 0; dc = 0; dcmax = 1; hp = 14; mp = 12; bgw = 47; wear = 14; wrist = 11;
            for (int j = 1; j <= level; j++)
            {
                //防御计算
                if (j % 7 == 0)
                {
                    ac2++;
                    if ((j + 7) % 9 == 0) ac++;
                }
                //魔御计算
                if (j % 12 == 0)
                {
                    mac2++;
                    if ((j + 15) % 15 == 0) mac++;
                }
                //攻击力计算
                if (j % 5 == 0)
                {
                    dcmax++;
                    if ((j + 5) % 5 == 0) dc++;
                }
                hp += 2;
                mp += 6;
                bgw += 3;
                wear += 1;
                wrist += 1;
                switch (j)
                {
                    case 1: lvExp = 100; break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: lvExp = exp * j * 2; break;
                    case 11: lvExp = 2200; break;
                    case 12: lvExp = 4000; break;
                    case 13: lvExp = 8000; break;
                    case 14: lvExp = 16000; break;
                    case 15: lvExp = 32000; break;
                    case 16: lvExp = 50000; break;
                    case 17: lvExp = 70000; break;
                    case 18: lvExp = 100000; break;
                    case 19: lvExp = 120000; break;
                    case 20: lvExp = 140000; break;
                    case 21: lvExp = 250000; break;
                    case 22: lvExp = 300000; break;
                    case 23: lvExp = 350000; break;
                    case 24: lvExp = 400000; break;
                    case 25: lvExp = 500000; break;
                    case 26: lvExp = 700000; break;
                    case 27: lvExp = 1000000; break;
                    case 28: lvExp = 1400000; break;
                    case 29: lvExp = 1800000; break;
                    case 30: lvExp = 2000000; break;
                    case 31: lvExp = 2400000; break;
                    case 32: lvExp = 2800000; break;
                    case 33: lvExp = 3200000; break;
                    case 34: lvExp = 3600000; break;
                    case 35: lvExp = 4000000; break;
                    case 36: lvExp = 4800000; break;
                    case 37: lvExp = 5600000; break;
                    case 38: lvExp = 8200000; break;
                    case 39: lvExp = 9000000; break;
                    case 40: lvExp = 24000000; break;
                    case 41: lvExp = 48000000; break;
                    case 42: lvExp = 96000000; break;
                    case 43: lvExp = 200000000; break;
                    case 44: lvExp = 220000000; break;
                    case 45: lvExp = 240000000; break;
                    case 46: lvExp = 260000000; break;
                    case 47: lvExp = 280000000; break;
                    case 48: lvExp = 300000000; break;
                    case 49: lvExp = 1000000000; break;
                    case 50: lvExp = 1200000000; break;
                    case 51: lvExp = 1400000000; break;
                    case 52: lvExp = 1600000000; break;
                    case 53: lvExp = 1800000000; break;
                    case 54: lvExp = 2000000000; break;
                    case 55: lvExp = 2650000000; break;
                    case 56: lvExp = 3250000000; break;
                    case 57: lvExp = 3850000000; break;
                    case 58: lvExp = 4000000000; break;
                    case 59: lvExp = 4200000000; break;
                    case 60: lvExp = 5000000000; break;
                    default: lvExp = 5000000000; break;
                }
                lengendBase = new LengendBase()
                {
                    Level = j,
                    Job = "法师",
                    LvExp = lvExp,
                    Ac = ac,
                    Ac2 = ac2,
                    Mac = mac,
                    Mac2 = mac2,
                    Dc = dc,
                    DcMax = dcmax,
                    Mc = dc,
                    McMax = dcmax,
                    Hp = hp,
                    Mp = mp,
                    Bgw = bgw,
                    Wear = wear,
                    Wrist = wrist
                };

                listWizard.Add(lengendBase);
            }
            #endregion 

            #region 生成道士数据
            ac = 0; ac2 = 0; mac = 0; mac2 = 0; dc = 0; dcmax = 1; hp = 14; mp = 12; bgw = 47; wear = 14; wrist = 11;
            for (int k = 1; k <= level; k++)
            {
                //防御计算
                if (k % 7 == 0)
                {
                    ac2++;
                    if ((k + 7) % 9 == 0) ac++;
                }
                //魔御计算
                if (k % 4 == 0)
                {
                    mac2++;
                    if ((k + 12) % 12 == 0) mac++;
                }
                //攻击力计算
                if (k % 5 == 0)
                {
                    dcmax++;
                    if ((k + 5) % 5 == 0) dc++;
                }
                hp += 3;
                mp += 4;
                bgw += 3;
                wear += 1;
                wrist += 1;
                switch (k)
                {
                    case 1: lvExp = 100; break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: lvExp = exp * k * 2; break;
                    case 11: lvExp = 2200; break;
                    case 12: lvExp = 4000; break;
                    case 13: lvExp = 8000; break;
                    case 14: lvExp = 16000; break;
                    case 15: lvExp = 32000; break;
                    case 16: lvExp = 50000; break;
                    case 17: lvExp = 70000; break;
                    case 18: lvExp = 100000; break;
                    case 19: lvExp = 120000; break;
                    case 20: lvExp = 140000; break;
                    case 21: lvExp = 250000; break;
                    case 22: lvExp = 300000; break;
                    case 23: lvExp = 350000; break;
                    case 24: lvExp = 400000; break;
                    case 25: lvExp = 500000; break;
                    case 26: lvExp = 700000; break;
                    case 27: lvExp = 1000000; break;
                    case 28: lvExp = 1400000; break;
                    case 29: lvExp = 1800000; break;
                    case 30: lvExp = 2000000; break;
                    case 31: lvExp = 2400000; break;
                    case 32: lvExp = 2800000; break;
                    case 33: lvExp = 3200000; break;
                    case 34: lvExp = 3600000; break;
                    case 35: lvExp = 4000000; break;
                    case 36: lvExp = 4800000; break;
                    case 37: lvExp = 5600000; break;
                    case 38: lvExp = 8200000; break;
                    case 39: lvExp = 9000000; break;
                    case 40: lvExp = 24000000; break;
                    case 41: lvExp = 48000000; break;
                    case 42: lvExp = 96000000; break;
                    case 43: lvExp = 200000000; break;
                    case 44: lvExp = 220000000; break;
                    case 45: lvExp = 240000000; break;
                    case 46: lvExp = 260000000; break;
                    case 47: lvExp = 280000000; break;
                    case 48: lvExp = 300000000; break;
                    case 49: lvExp = 1000000000; break;
                    case 50: lvExp = 1200000000; break;
                    case 51: lvExp = 1400000000; break;
                    case 52: lvExp = 1600000000; break;
                    case 53: lvExp = 1800000000; break;
                    case 54: lvExp = 2000000000; break;
                    case 55: lvExp = 2650000000; break;
                    case 56: lvExp = 3250000000; break;
                    case 57: lvExp = 3850000000; break;
                    case 58: lvExp = 4000000000; break;
                    case 59: lvExp = 4200000000; break;
                    case 60: lvExp = 5000000000; break;
                    default: lvExp = 5000000000; break;
                }
                lengendBase = new LengendBase()
                {
                    Level = k,
                    Job = "道士",
                    LvExp = lvExp,
                    Ac = ac,
                    Ac2 = ac2,
                    Mac = mac,
                    Mac2 = mac2,
                    Dc = dc,
                    DcMax = dcmax,
                    Sc = dc,
                    ScMax = dcmax,
                    Hp = hp,
                    Mp = mp,
                    Bgw = bgw,
                    Wear = wear,
                    Wrist = wrist
                };

                listTaoist.Add(lengendBase);
            }
            #endregion


            string warriorPath = LegendFilePath.UserAttrPath + "战士.txt";
            FileHelper.DeleteFile(warriorPath);
            FileHelper.WriteLine(warriorPath, Api.JsonSerialize(listWarrior));

            string wizardPath = LegendFilePath.UserAttrPath + "法师.txt";
            FileHelper.DeleteFile(wizardPath);
            FileHelper.WriteLine(wizardPath, Api.JsonSerialize(listWizard));

            string taoistPath = LegendFilePath.UserAttrPath + "道士.txt";
            FileHelper.DeleteFile(taoistPath);
            FileHelper.WriteLine(taoistPath, Api.JsonSerialize(listTaoist));
            MessageBox.Show("设置成功!");
        }

        private void BindDataGridView4()
        {
            cbItemIndex = cbItem.SelectedIndex;
            List<StditemDB> list = new List<StditemDB>();
            switch (cbItemIndex)
            {
                case 1: list = stditemDBList.Where(s => s.StdMode == 0).ToList(); break;
                case 2: list = stditemDBList.Where(s => s.StdMode == 1).ToList(); break;
                case 3: list = stditemDBList.Where(s => s.StdMode == 2).ToList(); break;
                case 4: list = stditemDBList.Where(s => s.StdMode == 3).ToList(); break;
                case 5: list = stditemDBList.Where(s => s.StdMode == 4).ToList(); break;
                case 6: list = stditemDBList.Where(s => s.StdMode == 5).ToList(); break;
                case 7: list = stditemDBList.Where(s => s.StdMode == 6).ToList(); break;
                case 8: list = stditemDBList.Where(s => s.StdMode == 7).ToList(); break;
                case 9: list = stditemDBList.Where(s => s.StdMode == 10).ToList(); break;
                case 10: list = stditemDBList.Where(s => s.StdMode == 11).ToList(); break;
                case 11: list = stditemDBList.Where(s => s.StdMode == 15).ToList(); break;
                case 12: list = stditemDBList.Where(s => s.StdMode == 16).ToList(); break;
                case 13: list = stditemDBList.Where(s => s.StdMode == 19).ToList(); break;
                case 14: list = stditemDBList.Where(s => s.StdMode == 20).ToList(); break;
                case 15: list = stditemDBList.Where(s => s.StdMode == 21).ToList(); break;
                case 16: list = stditemDBList.Where(s => s.StdMode == 22).ToList(); break;
                case 17: list = stditemDBList.Where(s => s.StdMode == 23).ToList(); break;
                case 18: list = stditemDBList.Where(s => s.StdMode == 24).ToList(); break;
                case 19: list = stditemDBList.Where(s => s.StdMode == 25).ToList(); break;
                case 20: list = stditemDBList.Where(s => s.StdMode == 26).ToList(); break;
                case 21: list = stditemDBList.Where(s => s.StdMode == 27).ToList(); break;
                case 22: list = stditemDBList.Where(s => s.StdMode == 28).ToList(); break;
                case 23: list = stditemDBList.Where(s => s.StdMode == 29).ToList(); break;
                case 24: list = stditemDBList.Where(s => s.StdMode == 30).ToList(); break;
                case 25: list = stditemDBList.Where(s => s.StdMode == 31).ToList(); break;
                case 26: list = stditemDBList.Where(s => s.StdMode == 40).ToList(); break;
                case 27: list = stditemDBList.Where(s => s.StdMode == 41).ToList(); break;
                case 28: list = stditemDBList.Where(s => s.StdMode == 42).ToList(); break;
                case 29: list = stditemDBList.Where(s => s.StdMode == 43).ToList(); break;
                case 30: list = stditemDBList.Where(s => s.StdMode == 44).ToList(); break;
                case 31: list = stditemDBList.Where(s => s.StdMode == 45).ToList(); break;
                case 32: list = stditemDBList.Where(s => s.StdMode == 46).ToList(); break;
                case 33: list = stditemDBList.Where(s => s.StdMode == 47).ToList(); break;
                case 34: list = stditemDBList.Where(s => s.StdMode == 62).ToList(); break;
                case 35: list = stditemDBList.Where(s => s.StdMode == 63).ToList(); break;
                case 36: list = stditemDBList.Where(s => s.StdMode == 64).ToList(); break;
                default: list = stditemDBList; break;
            }
            string itemName = txtItem.Text;
            if (!string.IsNullOrEmpty(itemName)) list = list.Where(s => s.StdName.Contains(itemName)).ToList();
            list.ForEach(s =>
            {
                string path = LegendFilePath.LegendIMGDataPath + "Items\\" + s.StdImgPath;
                if (File.Exists(path)) s.StdImg = System.Drawing.Image.FromFile(path);
            });
            dataGridView4.DataSource = list;
        }

        private void btnMonAdd_Click(object sender, EventArgs e)
        {
            string MonName = txtMonName.Text;
            if (!string.IsNullOrEmpty(MonName))
            {
                MonID = monsterDBList.Count + 1;
                MonsterDB monsterDB = new MonsterDB()
                {
                    MonID = MonID,
                    MonName = MonName
                };

                monsterDBList.Add(monsterDB);

                List<MonsterDB> list = new List<MonsterDB>();
                dataGridView3.DataSource = list;
                dataGridView3.DataSource = monsterDBList;

                //dataGridView3.Rows.Insert(dgvIndex3++, monsterDB.MonID, monsterDB.MonName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");
            }
            else
            {
                MessageBox.Show("怪物名称不能为空!");
            }
        }

        private void btnBookAdd_Click(object sender, EventArgs e)
        {
            string MagName = txtBookName.Text;
            if (!string.IsNullOrEmpty(MagName))
            {
                MagID = magicDBList.Count + 1;
                //dgvIndex2 = magicDBList.Count + 1;

                MagicDB magicDB = new MagicDB()
                {
                    MagID = MagID,
                    MagName = MagName,
                };

                magicDBList.Add(magicDB);
                List<MagicDB> list = new List<MagicDB>();
                dataGridView2.DataSource = list;
                dataGridView2.DataSource = magicDBList;
                //dataGridView2.Rows.Insert(dgvIndex2++, magicDB.MagID, "", magicDB.MagName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            else
            {
                MessageBox.Show("技能名称不能为空!");
            }
        }


        /// <summary>
        /// 加载怪物
        /// </summary>
        private void LoadMonster()
        {
            legMapMonInfos = new List<LegMapMonInfo>();
            monsterDBList = LegendApi.GetMonsterList();

            LegMapMonInfo legMapMonInfo = null;
            int i = 1;
            foreach (var mon in monsterDBList)
            {
                legMapMonInfo = new LegMapMonInfo()
                {
                    LMMI_ID = i++,
                    LMI_ID = 0,
                    MonID = mon.MonID,
                    MonName = mon.MonName,
                    DisMonName = $"{mon.MonName}(Lv:{mon.Lvl} HP:{mon.HP})",
                    MonQty = 100,
                    MonSort = 0,
                    HP = mon.HP,
                    Lvl = mon.Lvl,
                    IsCheckedMon = false,
                };
                legMapMonInfos.Add(legMapMonInfo);
            }

            List<LegMapMonInfo> list = new List<LegMapMonInfo>();
            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.DataSource = list;
            dataGridView5.DataSource = legMapMonInfos;


        }

    }
}
