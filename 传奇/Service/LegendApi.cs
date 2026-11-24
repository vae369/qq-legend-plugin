using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using System.Windows.Media;
using 传奇.core;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace 传奇
{
    /// <summary>
    /// 传奇API
    /// </summary>
    public class LegendApi
    {
        static List<string> jobList = new List<string>() { "战士", "道士", "法师" };
        static List<string> sexList = new List<string>() { "男", "女" };
        static Random rnd = new Random();
        /// <summary>
        /// 字符串中插入换行符
        /// </summary>
        public static string InsertNewlinesByByteLength(Graphics gh, Font font, string input, int maxWidth)
        {
            SizeF textSize;
            using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
            {
                textSize = g.MeasureString(input, font);
            }

            string[] words = input.Split(' ');
            string result = "";
            string line = "";

            using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
            {
                foreach (string word in words)
                {
                    string testLine = line + (line == "" ? "" : " ") + word;
                    if (g.MeasureString(testLine, font).Width <= maxWidth)
                    {
                        line = testLine;
                    }
                    else
                    {
                        if (line != "")
                        {
                            result += line + "#";
                        }
                        line = word;
                    }
                }
            }

            if (line != "")
            {
                result += line;
            }

            return result;
        }

        /// <summary>
        /// 注册
        /// </summary>
        public static bool Register(UserInfo user)
        {
            try
            {
                if (user.Name.Length > 7) throw new Exception("对不起,用户名长度错误,用户名最多7个汉字长度");
                if (!jobList.Contains(user.Job)) throw new Exception("对不起,职业最多2个汉字长度.如:战士|道士|法师");
                if (!sexList.Contains(user.Sex)) throw new Exception("对不起,性别最多1个字长度.如:男 或 女");

                string legenPath = "", legenStr = "";
                List<LengendBase> LengendList = new List<LengendBase>();

                int n = rnd.Next(2);
                if (n == 0)
                {
                    user.MapCode = "1_0_1";
                    user.Map = "银杏山谷";
                    n = rnd.Next(1, 5);//3张初始图里选一个出生点
                    user.UserImagePath = $"地图\\0\\银杏山谷\\{user.MapCode}_{n}.jpg";
                }
                else
                {
                    user.MapCode = "1_0_2";
                    user.Map = "边界村";
                    n = rnd.Next(1, 4);//3张初始图里选一个出生点
                    user.UserImagePath = $"地图\\0\\边界村\\{user.MapCode}_{n}.jpg";
                }

                if (user.Sex == "男")
                {
                    switch (user.Job)
                    {
                        case "战士":
                            legenPath = LegendFilePath.UserAttrPath + "战士.txt";
                            legenStr = FileHelper.ReadTxt(legenPath);
                            LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                            break;
                        case "道士":
                            legenPath = LegendFilePath.UserAttrPath + "法师.txt";
                            legenStr = FileHelper.ReadTxt(legenPath);
                            LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                            break;
                        case "法师":
                            legenPath = LegendFilePath.UserAttrPath + "道士.txt";
                            legenStr = FileHelper.ReadTxt(legenPath);
                            LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                            break;
                    }
                }
                else
                {
                    switch (user.Job)
                    {
                        case "战士":
                            legenPath = LegendFilePath.UserAttrPath + "战士.txt";
                            legenStr = FileHelper.ReadTxt(legenPath);
                            LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                            break;
                        case "道士":
                            legenPath = LegendFilePath.UserAttrPath + "道士.txt";
                            legenStr = FileHelper.ReadTxt(legenPath);
                            LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                            break;
                        case "法师":
                            legenPath = LegendFilePath.UserAttrPath + "法师.txt";
                            legenStr = FileHelper.ReadTxt(legenPath);
                            LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                            break;
                    }
                }

                // 创建用户默认数据
                LengendBase lb = null;
                foreach (var item in LengendList)
                {
                    if (user.User_General.Lv == item.Level)
                    {
                        lb = item;
                        break;
                    }
                }
                user.Pos = $"{rnd.Next(1, 666)},{rnd.Next(1, 666)}";
                user.User_Attribute.Hp = lb.Hp;
                user.User_Attribute.Mp = lb.Mp;
                user.User_Attribute.Ac = lb.Ac;
                user.User_Attribute.Ac2 = lb.Ac2 + 2;
                user.User_Attribute.Mac = lb.Mac;
                user.User_Attribute.Mac2 = lb.Mac2 + 1;
                user.User_Attribute.Dc = lb.Dc + 2;
                user.User_Attribute.DcMax = lb.DcMax + 5;
                user.User_Attribute.Mc = lb.Mc;
                user.User_Attribute.McMax = lb.McMax;
                user.User_Attribute.Sc = lb.Sc;
                user.User_Attribute.ScMax = lb.ScMax;
                user.User_Attribute.Wrist = 0;
                user.User_Attribute.WristMax = lb.Wrist;

                user.User_General.Bgw = 0;
                user.User_General.BgwMax = lb.Bgw;
                user.User_General.Wear = 0;
                user.User_General.WearMax = lb.Wear;
                user.User_General.NextExp = lb.LvExp;
                user.User_General.Lv = lb.Level;

                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                string dirName = Path.GetDirectoryName(path);
                var fileList = Directory.GetFiles(dirName).Where(s => !s.Contains(".txt")).ToList();//去除txt文件
                int id = 0, maxId = 0;
                IniFileHelper iniFileHelper;
                fileList.ForEach(file =>
                {
                    iniFileHelper = new IniFileHelper(file);
                    int.TryParse(iniFileHelper.Read("人物信息", "序号"), out id);

                    if (id > maxId) maxId = id;
                });

                iniFileHelper = new IniFileHelper(path);
                id = int.Parse(iniFileHelper.Read("人物信息", "序号"));
                if (id == 0) id = maxId + 1;

                iniFileHelper.Write("人物信息", "序号", id.ToString());
                iniFileHelper.Write("人物信息", "角色名称", user.Name);
                iniFileHelper.Write("人物信息", "角色类型", user.RoleType);
                iniFileHelper.Write("人物信息", "性别", user.Sex);
                iniFileHelper.Write("人物信息", "职业", user.Job);
                iniFileHelper.Write("人物信息", "所在地图", user.Map);
                iniFileHelper.Write("人物信息", "所在地图编号", user.MapCode);
                iniFileHelper.Write("人物信息", "坐标", user.Pos);
                iniFileHelper.Write("人物信息", "帐号", user.UserQQ);
                iniFileHelper.Write("人物信息", "是否在城里", user.IsOnCity);
                iniFileHelper.Write("人物信息", "个人图片", user.UserImagePath);
                iniFileHelper.Write("人物信息", "是否领取唯一赞助1", "否");
                iniFileHelper.Write("人物信息", "是否领取唯一赞助2", "否");
                iniFileHelper.Write("人物信息", "是否领取唯一赞助3", "否");
                iniFileHelper.Write("人物信息", "开启发送动图", "否");
                iniFileHelper.Write("人物信息", "是否领取礼包码", "否");

                user.UserImagePath = LegendFilePath.LegendIMGDataPath + user.UserImagePath;

                // 创建默认普通数据
                iniFileHelper.Write("普通数据", "等级", user.User_General.Lv.ToString());
                iniFileHelper.Write("普通数据", "金币", user.User_General.JB.ToString());
                iniFileHelper.Write("普通数据", "声望", user.User_General.REP.ToString());
                iniFileHelper.Write("普通数据", "金刚石", user.User_General.JGS.ToString());
                iniFileHelper.Write("普通数据", "元宝", user.User_General.YB.ToString());
                iniFileHelper.Write("普通数据", "PK点数", user.User_General.PkPoint.ToString());
                iniFileHelper.Write("普通数据", "总经验", user.User_General.SumExp.ToString());
                iniFileHelper.Write("普通数据", "下一级经验", user.User_General.NextExp.ToString());

                // 创建默认属性点
                iniFileHelper.Write("属性点", "防御下限", user.User_Attribute.Ac.ToString());
                iniFileHelper.Write("属性点", "防御上限", user.User_Attribute.Ac2.ToString());
                iniFileHelper.Write("属性点", "魔法防御下限", user.User_Attribute.Mac.ToString());
                iniFileHelper.Write("属性点", "魔法防御上限", user.User_Attribute.Mac2.ToString());
                iniFileHelper.Write("属性点", "物理攻击力", user.User_Attribute.Dc.ToString());
                iniFileHelper.Write("属性点", "最大物理攻击力", user.User_Attribute.DcMax.ToString());
                iniFileHelper.Write("属性点", "魔法攻击力", user.User_Attribute.Mc.ToString());
                iniFileHelper.Write("属性点", "最大魔法攻击力", user.User_Attribute.McMax.ToString());
                iniFileHelper.Write("属性点", "道术攻击力", user.User_Attribute.Sc.ToString());
                iniFileHelper.Write("属性点", "最大道术攻击力", user.User_Attribute.ScMax.ToString());
                iniFileHelper.Write("属性点", "Hp", user.User_Attribute.Hp.ToString());
                iniFileHelper.Write("属性点", "Mp", user.User_Attribute.Mp.ToString());

                // 创建默认装备物品
                iniFileHelper.Write("装备物品", "衣服", user.User_Items.DRESS.ToString());

                iniFileHelper.Write("装备物品", "勋章", user.User_Items.RIGHTHAND);
                iniFileHelper.Write("装备物品", "头盔", user.User_Items.HELMET);
                iniFileHelper.Write("装备物品", "项链", user.User_Items.NECKLACE);
                iniFileHelper.Write("装备物品", "右戒指", user.User_Items.RING_R);
                iniFileHelper.Write("装备物品", "左戒指", user.User_Items.RING_L);
                iniFileHelper.Write("装备物品", "右手镯", user.User_Items.ARMRING_R);
                iniFileHelper.Write("装备物品", "左手镯", user.User_Items.ARMRING_L);
                iniFileHelper.Write("装备物品", "护身符", user.User_Items.BUJUK);
                iniFileHelper.Write("装备物品", "腰带", user.User_Items.BELT);
                iniFileHelper.Write("装备物品", "鞋子", user.User_Items.BOOTS);
                iniFileHelper.Write("装备物品", "宝石", user.User_Items.CHARM);
                iniFileHelper.Write("装备物品", "斗笠", user.User_Items.HAT);
                iniFileHelper.Write("装备物品", "军鼓", user.User_Items.DRUM);
                iniFileHelper.Write("装备物品", "盾牌", user.User_Items.SHIELD);
                iniFileHelper.Write("装备物品", "马牌", user.User_Items.HORSE);
                iniFileHelper.Write("装备物品", "时装衣服", user.User_Items.SZ_DRESS);
                iniFileHelper.Write("装备物品", "时装武器", user.User_Items.SZ_WEAPON);

                // 获取背包物品列表

                var stdDBList = GetStdItemsList();

                // 创建背包内的默认物品
                stdDBList.ForEach(std =>
                {
                    switch (std.StdName)
                    {
                        case "金创药(小量)":// 红
                        case "魔法药(小量)":// 蓝
                            user.User_General.Bgw += std.Weight;
                            iniFileHelper.Write("背包物品", iniFileHelper.GetRndId(), Api.JsonSerialize<StditemDB>(std));
                            break;
                        case "木剑":
                            user.User_Items.WEAPON = iniFileHelper.GetRndId();
                            iniFileHelper.Write("装备物品", "武器", user.User_Items.WEAPON);
                            iniFileHelper.Write("装备属性", user.User_Items.WEAPON, Api.JsonSerialize<StditemDB>(std));
                            break;
                        case "布衣(男)":
                            if (user.Sex == "男")
                            {
                                user.User_Items.DRESS = iniFileHelper.GetRndId();
                                iniFileHelper.Write("装备物品", "衣服", user.User_Items.DRESS);
                                iniFileHelper.Write("装备属性", user.User_Items.DRESS, Api.JsonSerialize<StditemDB>(std));
                            }
                            break;
                        case "布衣(女)":
                            if (user.Sex == "女")
                            {
                                user.User_Items.DRESS = iniFileHelper.GetRndId();
                                iniFileHelper.Write("装备物品", "衣服", user.User_Items.DRESS);
                                iniFileHelper.Write("装备属性", user.User_Items.DRESS, Api.JsonSerialize<StditemDB>(std));
                            }
                            break;
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 修改名称
        /// </summary>
        public static bool ChangeName(UserInfo user)
        {
            try
            {
                if (System.Text.Encoding.Default.GetBytes(user.Name).Length > 14 || string.IsNullOrEmpty(user.Name)) throw new Exception("对不起,用户名长度错误,用户名最多7个汉字长度");
                string newPath = user.UserQQ + ".txt";
                if (!File.Exists(newPath)) throw new Exception("对不起,您未注册帐号,请输入 传奇 或 传奇说明 查看");

                List<string> userConfigList = FileHelper.ReadTxtReturnList(newPath);
                string ynstr = userConfigList.Where(s => s.StartsWith("元宝=")).FirstOrDefault();
                string uname = userConfigList.Where(s => s.StartsWith("名称=")).FirstOrDefault();
                int u_yb = Convert.ToInt32(ynstr.Substring(3));
                u_yb -= 100;
                if (u_yb < 0) throw new Exception("对不起,您元宝不足!请联系管理充值");
                uname = uname.Substring(0, 3) + user.Name;
                ynstr = "元宝=" + u_yb;
                for (int i = 0; i < userConfigList.Count; i++)
                {
                    if (userConfigList[i].StartsWith("元宝=")) userConfigList[i] = ynstr;
                    if (userConfigList[i].StartsWith("名称=")) userConfigList[i] = uname;
                }
                //user.UserImagePath = newPath;
                //WriteFile(newPath, userConfigList);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改性别
        /// </summary>
        public static bool ChangeSex(UserInfo user)
        {
            try
            {
                string newPath = user.UserQQ + ".txt";
                if (!File.Exists(newPath)) throw new Exception("对不起,您未注册帐号,请输入 传奇 或 传奇说明 查看");
                List<string> userConfigList = FileHelper.ReadTxtReturnList(newPath);
                string usex = userConfigList.Where(s => s.StartsWith("性别=")).FirstOrDefault();
                string ynstr = userConfigList.Where(s => s.StartsWith("元宝=")).FirstOrDefault();

                int u_yb = Convert.ToInt32(ynstr.Substring(3));
                u_yb -= 100;
                if (u_yb < 0) throw new Exception("对不起,您元宝不足!请联系管理充值");
                string sex = usex.Substring(3);

                usex = sex == "男" ? "性别=女" : "性别=男";
                ynstr = "元宝=" + u_yb;
                for (int i = 0; i < userConfigList.Count; i++)
                {
                    if (userConfigList[i].StartsWith("元宝=")) userConfigList[i] = ynstr;
                    if (userConfigList[i].StartsWith("性别=")) userConfigList[i] = usex;
                }

                //WriteFile(newPath, userConfigList);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 修改职业
        /// </summary>
        public static bool ChangeJob(UserInfo user)
        {
            try
            {
                string newPath = user.UserQQ + ".txt";
                if (!File.Exists(newPath)) throw new Exception("对不起,您未注册帐号,请输入 传奇 或 传奇说明 查看");
                if (!jobList.Contains(user.Job)) throw new Exception("对不起,职业最多2个汉字长度.如:战士|道士|法师");

                List<string> userConfigList = FileHelper.ReadTxtReturnList(newPath);
                string ujob = userConfigList.Where(s => s.StartsWith("职业=")).FirstOrDefault();
                string ynstr = userConfigList.Where(s => s.StartsWith("元宝=")).FirstOrDefault();

                ujob = ujob.Substring(3);
                if (ujob == user.Job) throw new Exception($"您原本职业就是{ujob},不需要改");
                int u_yb = Convert.ToInt32(ynstr.Substring(3));
                u_yb -= 500;
                if (u_yb < 0) throw new Exception("对不起,您元宝不足!请联系管理充值");
                ujob = "职业=" + user.Job;
                ynstr = "元宝=" + u_yb;
                for (int i = 0; i < userConfigList.Count; i++)
                {
                    if (userConfigList[i].StartsWith("元宝=")) userConfigList[i] = ynstr;
                    if (userConfigList[i].StartsWith("职业=")) userConfigList[i] = ujob;
                }

                //WriteFile(newPath, userConfigList);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 屏蔽消息
        /// </summary>
        public static void BlockMessage(string groupQQ)
        {
            try
            {
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                string groups = config.Read("配置信息", "屏蔽消息群");
                groups += groupQQ + ",";
                config.Write("配置信息", "屏蔽消息群", groups);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 制造装备
        /// </summary>
        public static string CreateStditems(UserInfo user, string msg)
        {
            try
            {
                var arrMsg = msg.Split(' ');
                msg = "";
                if (arrMsg.Length == 3)
                {
                    List<StditemDB> listStdItems = GetStdItemsList();

                    string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                    IniFileHelper config = new IniFileHelper(path);

                    StditemDB std = listStdItems.Where(s => s.StdName == arrMsg[1]).First();
                    int num = int.Parse(arrMsg[2]);
                    for (int i = 0; i < num; i++)
                    {
                        std.StdItemMap = user.Map;
                        std.StdItemOwner = user.Name;
                        std.StdItemStuff = "GM制造";
                        std.StdItemTime = DateTime.Now.ToString();
                        config.Write("背包物品", config.GetRndId(), Api.JsonSerialize(std));
                        msg += std.StdName + ",";
                    }
                }
                msg = $"物品[{msg.TrimEnd(',')}]制造成功!";
                return msg;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 我要成佛
        /// </summary>
        public static string GetUserUpgradeGod(UserInfo user)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                string key = "";
                var list = userInI.GetAllKeys("背包物品");
                List<StditemDB> stdList = new List<StditemDB>();
                StditemDB stditemDB = null;
                foreach (var item in list)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", item));
                    if (stditemDB.StdName == "永恒结晶") { key = item; break; }
                }
                if (stditemDB == null) throw new Exception("你身上永恒结晶小于800,为何如此没有诚意,请回吧!!");
                if (stditemDB.SubAttr1 < 800) throw new Exception("你身上永恒结晶小于800,为何如此没有诚意,请回吧!!");
                if (user.User_General.Lv < 55) throw new Exception("等级小于55级,成佛失败!");
                if (user.User_General.YB < 50000) throw new Exception("元宝少于5万,为何如此没有诚意,请回吧!!");

                stditemDB.SubAttr1 -= 800;
                user.User_General.YB -= 50000;
                userInI.Write("背包物品", key, Api.JsonSerialize(stditemDB));
                userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());

                string res = "诶,小兄弟,看来佛祖没有保佑你啊,是不是平时亏心事做多了?";
                var title = user.User_TitleAttribute.Where(t => t.TitleName == "我佛慈悲").FirstOrDefault();
                if (title == null)
                {
                    title = new UserTitleAttribute
                    {
                        LevelType = 4,
                        TitleName = "我佛慈悲",
                        PhysicalDamageReduc = 10,
                        MagicDamageReduc = 10,
                        AttackPR = 10,
                        HPIncrease = 10,
                        Hp = 500,
                        StdItemStuff = "NPC制造",
                        StdItemOwner = user.Name,
                        StdItemTime = DateTime.Now.ToString(),
                        StdItemMap = user.Map,
                    };
                    userInI.Write("个人称号", "称号4", Api.JsonSerialize(title));
                    res = $"恭喜玩家[{user.Name}]获得称号【我佛慈悲】!上天入地,无所不能,向绝世强者又进了一步!";
                }
                else
                {
                    title = user.User_TitleAttribute.Where(t => t.TitleName == "走火入魔").FirstOrDefault();
                    if (title == null)
                    {
                        if (rnd.Next(1, 100) < 30)
                        {
                            title = new UserTitleAttribute
                            {
                                LevelType = 5,
                                TitleName = "走火入魔",
                                PhysicalDamageReflex = 10,
                                MagicDamageReflex = 10,
                                IgnoreAC = 10,
                                HPIncrease = 20,
                                Hp = 1000,
                                StdItemStuff = "NPC制造",
                                StdItemOwner = user.Name,
                                StdItemTime = DateTime.Now.ToString(),
                                StdItemMap = user.Map,
                            };
                            userInI.Write("个人称号", "称号5", Api.JsonSerialize(title));
                            res = $"恭喜玩家[{user.Name}]获得称号【走火入魔】!超强超强的属性,超强超强的绝世强者即将出世!";
                        }
                    }
                    else
                    {
                        res = $"小兄弟,既然你已经成佛入魔了,但是材料我还是一个不少的收了嗷...";
                    }
                }

                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取称号升级
        /// </summary>
        public static void GetUpgradeTitle(UserInfo user, string msg)
        {
            try
            {
                var title = user.User_TitleAttribute.Where(s => s.LevelType == 1).FirstOrDefault();
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                msg = msg.Replace("升级", "");
                switch (msg)
                {
                    case "永恒青铜":
                        if (title != null) throw new Exception($"土贼!你都是{title.TitleName}称号了,升级毛线呢!");
                        user.User_General.JB -= 2000000;
                        title = new UserTitleAttribute
                        {
                            Level = 1,
                            LevelType = 1,
                            MonExpRate = 3,
                            StdItemMap = user.Map,
                            StdItemOwner = user.Name,
                            StdItemStuff = "NPC制造",
                            StdItemTime = DateTime.Now.ToString()
                        };
                        break;
                    case "永恒白银":
                        if (title == null) throw new Exception("土贼!你都没有永恒青铜称号,先升级永恒青铜之后再来和我说话吧!");
                        if (title.Level != 1) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 4000000;

                        title.Level = 2;
                        title.MonExpRate = 6;
                        break;
                    case "永恒黄金":
                        if (title == null) throw new Exception("土贼!你都没有永恒白银称号,先升级永恒白银之后再来和我说话吧!");
                        if (title.Level != 2) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 6000000;
                        title.Level = 3;
                        title.MonExpRate = 9;
                        break;
                    case "永恒白金":
                        if (title == null) throw new Exception("土贼!你都没有永恒黄金称号,先升级永恒黄金之后再来和我说话吧!");
                        if (title.Level != 3) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 8000000;
                        title.Level = 4;
                        title.MonExpRate = 12;
                        break;
                    case "永恒钻石":
                        if (title == null) throw new Exception("土贼!你都没有永恒白金称号,先升级永恒白金之后再来和我说话吧!");
                        if (title.Level != 4) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 10000000;
                        title.Level = 5;
                        title.MonExpRate = 15;
                        break;
                    case "永恒星耀":
                        if (title == null) throw new Exception("土贼!你都没有永恒钻石称号,先升级永恒钻石之后再来和我说话吧!");
                        if (title.Level != 5) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 12000000;
                        title.Level = 6;
                        title.MonExpRate = 18;
                        break;
                    case "永恒王者":
                        if (title == null) throw new Exception("土贼!你都没有永恒星耀称号,先升级永恒星耀之后再来和我说话吧!");
                        if (title.Level != 6) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 14000000;
                        if (user.User_General.JB < 0) throw new Exception("臭小子,没钱你来我这吆喝什么,快去钱赚钱去吧!");
                        title.Level = 7;
                        title.MonExpRate = 21;
                        break;
                    case "永恒星辰":
                        if (title == null) throw new Exception("土贼!你都没有永恒王者称号,先升级永恒王者之后再来和我说话吧!");
                        if (title.Level != 7) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 16000000;
                        title.Level = 8;
                        title.MonExpRate = 24;
                        break;
                    case "永恒魔王":
                        if (title == null) throw new Exception("土贼!你都没有永恒星辰称号,先升级永恒星辰之后再来和我说话吧!");
                        if (title.Level != 8) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 18000000;
                        title.Level = 9;
                        title.MonExpRate = 27;
                        break;
                    case "永恒圣佛":
                        if (title == null) throw new Exception("土贼!你都没有永恒魔王称号,先升级永恒魔王之后再来和我说话吧!");
                        if (title.Level != 9) throw new Exception($"厚礼蟹!你自己啥称号不知道吗?睁大眼睛看清楚了,你现在称号是[{title.TitleName}]");
                        user.User_General.JB -= 20000000;
                        title.Level = 10;
                        title.MonExpRate = 30;
                        break;
                    default: throw new Exception("卧擦!没有这个称号!");
                }
                if (user.User_General.JB < 0) throw new Exception("臭小子,没钱你来我这吆喝什么,快去钱赚钱去吧!");

                title.TitleName = msg;
                userInI.Write("个人称号", "称号1", Api.JsonSerialize(title));
                userInI.Write("普通数据", "金币", user.User_General.JB.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有怪物
        /// </summary>
        /// <returns></returns>
        public static List<MonsterDB> GetMonsterList()
        {
            // 取数据库中的怪物
            string stdPath = LegendFilePath.Mud2Path + "monster.txt";
            string res = FileHelper.ReadTxt(stdPath);
            var list = Api.JsonDeserialize<List<MonsterDB>>(res);
            return list;
        }

        /// <summary>
        /// 获取所有技能
        /// </summary>
        /// <returns></returns>
        public static List<MagicDB> GetMagicList()
        {
            string magicPath = LegendFilePath.Mud2Path + "magic.txt";
            string s = FileHelper.ReadTxt(magicPath);
            var list = Api.JsonDeserialize<List<MagicDB>>(s);
            return list;
        }

        /// <summary>
        /// 获取所有物品
        /// </summary>
        /// <returns></returns>
        public static List<StditemDB> GetStdItemsList()
        {
            string stdPath = LegendFilePath.Mud2Path + "stdItem.txt";
            string stdStr = FileHelper.ReadTxt(stdPath);
            var list = Api.JsonDeserialize<List<StditemDB>>(stdStr);
            return list;
        }

        /// <summary>
        /// 取所有怪物爆率文件
        /// </summary>
        /// <returns></returns>
        public static List<MonQuest> GetMonQuestList()
        {
            string MonQuestPath = LegendFilePath.MonQuestPath + "MonQuest.txt";
            string mqStr = FileHelper.ReadTxt(MonQuestPath);
            var list = Api.JsonDeserialize<List<MonQuest>>(mqStr);

            return list;
        }

        /// <summary>
        /// 取所有地图
        /// </summary>
        /// <returns></returns>
        public static List<LengendMapInfo> GetMapInfoList()
        {
            string mapPath = LegendFilePath.MapPath + "MapInfo.txt";
            string mapStr = FileHelper.ReadTxt(mapPath);
            var list = Api.JsonDeserialize<List<LengendMapInfo>>(mapStr);
            return list;
        }

        /// <summary>
        /// 取所有地图的怪物
        /// </summary>
        /// <returns></returns>
        public static List<LegMapMonInfo> GetLegMapMonInfoList()
        {
            string mapPath = LegendFilePath.MapPath + "MapMonInfo.txt";
            string mapMonStr = FileHelper.ReadTxt(mapPath);
            var list = Api.JsonDeserialize<List<LegMapMonInfo>>(mapMonStr);

            return list;
        }


        /// <summary>
        /// 调整等级，元宝，金币
        /// </summary>
        public static bool GiveJBYBLevel(string type, string toQQ, int qty)
        {
            string path = LegendFilePath.UserPath + toQQ + ".ini";
            IniFileHelper userInI = new IniFileHelper(path);
            if (userInI == null) throw new Exception("抱歉,用户不存在!");
            switch (type)
            {
                case "金币":
                    int.TryParse(userInI.Read("普通数据", "金币"), out int jb);
                    jb += qty;
                    userInI.Write("普通数据", "金币", jb + ""); break;
                case "等级":
                    int.TryParse(userInI.Read("普通数据", "等级"), out int level);
                    level += qty;
                    userInI.Write("普通数据", "等级", level + ""); break;
                case "元宝":
                    int.TryParse(userInI.Read("普通数据", "元宝"), out int yb);
                    yb += qty;
                    userInI.Write("普通数据", "元宝", yb + ""); break;
                case "声望":
                    int.TryParse(userInI.Read("普通数据", "声望"), out int sw);
                    sw += qty;
                    userInI.Write("普通数据", "声望", sw + ""); break;
                case "金刚石":
                    int.TryParse(userInI.Read("普通数据", "金刚石"), out int jgs);
                    jgs += qty;
                    userInI.Write("普通数据", "金刚石", jgs + ""); break;
                default: throw new Exception("抱歉,参数错误!");
            }

            return true;
        }

        /// <summary>
        /// 用户转生
        /// </summary>
        public static bool UserRenewLevel(UserInfo user, int level)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
                string key = "";
                var list = userInI.GetAllKeys("背包物品");
                List<StditemDB> stdList = new List<StditemDB>();
                StditemDB stditemDB = null;
                list.ForEach(i =>
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", i));
                    switch (stditemDB.StdName)
                    {
                        case "一转证明": stdList.Add(stditemDB); keyValuePairs.Add(1, i); break;
                        case "二转证明": stdList.Add(stditemDB); keyValuePairs.Add(2, i); break;
                        case "三转证明": stdList.Add(stditemDB); keyValuePairs.Add(3, i); break;
                        case "四转证明": stdList.Add(stditemDB); keyValuePairs.Add(4, i); break;
                        case "五转证明": stdList.Add(stditemDB); keyValuePairs.Add(5, i); break;
                    }
                });
                switch (level)
                {
                    case 1:
                        if (user.User_RenewLevelAttribute.Level >= 1) throw new Exception("你已经是" + user.User_RenewLevelAttribute.Level + "转了,还转个屁啊!");
                        if (user.User_General.Lv < 52) throw new Exception("转生失败,需要人物52级!");
                        if (user.User_General.YB < 50000) throw new Exception("转生失败,需要元宝5万!");
                        stditemDB = stdList.Where(s => s.StdName == "一转证明").FirstOrDefault();
                        if (stditemDB == null) throw new Exception("转生失败,需要一转证明*10..");
                        if (stditemDB.SubAttr1 < 10) throw new Exception("转生失败,需要一转证明*10..");

                        key = keyValuePairs[1];
                        user.User_General.YB -= 50000;
                        user.User_RenewLevelAttribute.DouBPower = 10;
                        user.User_RenewLevelAttribute.Level = 1;
                        break;
                    case 2:
                        if (user.User_RenewLevelAttribute.Level >= 2) throw new Exception("你已经是" + user.User_RenewLevelAttribute.Level + "转了,还转个屁啊!");
                        if (user.User_General.Lv < 53) throw new Exception("转生失败,需要人物53级!");
                        if (user.User_General.YB < 60000) throw new Exception("转生失败,需要元宝6万!");
                        stditemDB = stdList.Where(s => s.StdName == "二转证明").FirstOrDefault();
                        if (stditemDB == null) throw new Exception("转生失败,需要二转证明*10..");
                        if (stditemDB.SubAttr1 < 10) throw new Exception("转生失败,需要二转证明*10..");

                        key = keyValuePairs[2];
                        user.User_General.YB -= 60000;
                        user.User_RenewLevelAttribute.DouBPower = 20;
                        user.User_RenewLevelAttribute.Level = 2;
                        break;
                    case 3:
                        if (user.User_RenewLevelAttribute.Level >= 3) throw new Exception("你已经是" + user.User_RenewLevelAttribute.Level + "转了,还转个屁啊!");
                        if (user.User_General.Lv < 54) throw new Exception("转生失败,需要人物4级!");
                        if (user.User_General.YB < 70000) throw new Exception("转生失败,需要元宝7万!");
                        stditemDB = stdList.Where(s => s.StdName == "三转证明").FirstOrDefault();
                        if (stditemDB == null) throw new Exception("转生失败,需要三转证明*10..");
                        if (stditemDB.SubAttr1 < 10) throw new Exception("转生失败,需要三转证明*10..");

                        key = keyValuePairs[3];
                        user.User_General.YB -= 70000;
                        user.User_RenewLevelAttribute.DouBPower = 30;
                        user.User_RenewLevelAttribute.Level = 3;
                        break;
                    case 4:
                        if (user.User_RenewLevelAttribute.Level >= 4) throw new Exception("你已经是" + user.User_RenewLevelAttribute.Level + "转了,还转个屁啊!");
                        if (user.User_General.Lv < 55) throw new Exception("转生失败,需要人物55级!");
                        if (user.User_General.YB < 80000) throw new Exception("转生失败,需要元宝8万!");
                        stditemDB = stdList.Where(s => s.StdName == "四转证明").FirstOrDefault();
                        if (stditemDB == null) throw new Exception("转生失败,需要四转证明*10..");
                        if (stditemDB.SubAttr1 < 10) throw new Exception("转生失败,需要四转证明*10..");

                        key = keyValuePairs[4];
                        user.User_General.YB -= 80000;
                        user.User_RenewLevelAttribute.DouBPower = 40;
                        user.User_RenewLevelAttribute.Level = 4;
                        break;
                    case 5:
                        if (user.User_RenewLevelAttribute.Level >= 5) throw new Exception("你已经是" + user.User_RenewLevelAttribute.Level + "转了,还转个屁啊!");
                        if (user.User_General.Lv < 56) throw new Exception("转生失败,需要人物56级!");
                        if (user.User_General.YB < 100000) throw new Exception("转生失败,需要元宝10万!");
                        stditemDB = stdList.Where(s => s.StdName == "五转证明").FirstOrDefault();
                        if (stditemDB == null) throw new Exception("转生失败,需要五转证明*10..");
                        if (stditemDB.SubAttr1 < 10) throw new Exception("转生失败,需要五转证明*10..");

                        key = keyValuePairs[5];
                        user.User_General.YB -= 100000;
                        user.User_RenewLevelAttribute.DouBPower = 50;
                        user.User_RenewLevelAttribute.Level = 5;
                        break;
                    case 6: break;
                    case 7: break;
                    case 8: break;
                    case 9: break;
                    case 10: break;
                }

                stditemDB.SubAttr1 -= 10;
                userInI.Write("转生属性", "等级", user.User_RenewLevelAttribute.Level.ToString());
                userInI.Write("转生属性", "倍功", user.User_RenewLevelAttribute.DouBPower.ToString());
                userInI.Write("背包物品", key, Api.JsonSerialize(stditemDB));
                userInI.Write("普通数据", "元宝", user.User_General.YB.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 获取宣传码
        /// </summary>
        /// <returns></returns>
        public static string GetXCM()
        {
            try
            {
                string path = LegendFilePath.BaseConfigPath + "宣传码\\普通宣传码.txt";
                var list = FileHelper.ReadTxtReturnList(path);
                if (list.Count < 10)
                {
                    CreateXCM(10, 20);
                    list = FileHelper.ReadTxtReturnList(path);
                }
                Random rnd = new Random();
                string xcm = list[rnd.Next(0, list.Count)];

                return xcm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 创建宣传码
        /// </summary>
        /// <returns></returns>
        public static void CreateXCM(int count, int totalLength)
        {
            try
            {
                string prefix = "XCM_";
                int randomPartLength = totalLength - prefix.Length;

                var random = new Random();
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                StringBuilder stringBuilder = null;
                string path = LegendFilePath.BaseConfigPath + "宣传码\\普通宣传码.txt";
                for (int i = 0; i < count; i++)
                {
                    stringBuilder = new StringBuilder(prefix);
                    for (int j = 0; j < randomPartLength; j++)
                    {
                        stringBuilder.Append(chars[random.Next(chars.Length)]);
                    }
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(stringBuilder.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
