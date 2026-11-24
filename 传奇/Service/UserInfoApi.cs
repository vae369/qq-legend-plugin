using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;
using System.Windows.Input;
using ImageMagick;
using 传奇.core;
using static System.Collections.Specialized.BitVector32;

namespace 传奇
{
    public class UserInfoApi
    {
        static Random rnd = new Random();
        static StditemConfig Std_Weapon = null;
        static StditemConfig Std_Dress = null;
        static StditemConfig Std_Necklace = null;
        static StditemConfig Std_NecklaceBracelet = null;
        static StditemConfig Std_Bracelet = null;
        static StditemConfig Std_Ring = null;
        static StditemConfig Std_Helmet = null;
        static StditemConfig Std_Boots = null;
        static StditemConfig Std_Belt = null;
        static int JP_Qty = 0, YSJP_Qty = 0;

        /// <summary>
        /// 用户领取充值
        /// </summary>
        public static List<string> UserClaimRecharge(UserInfo user)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(path);

                string userRechargePath = LegendFilePath.UserRechargePath + user.UserQQ + ".txt";
                var list = FileHelper.ReadTxtReturnList(userRechargePath);
                if (list == null) throw new Exception("哟哟哟,没有充钱就想来领充值了,谁给你的胆子,还不去充值去!");
                List<string> recList = new List<string>();
                StringBuilder sb = new StringBuilder();
                int qty = 0, yb = 0, jgs = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (!string.IsNullOrEmpty(list[i]))
                    {
                        var arr = list[i].Split(',');
                        if (arr[6] == "0")
                        {
                            qty += int.Parse(arr[2]);
                            yb += int.Parse(arr[3]);
                            jgs += int.Parse(arr[4]);
                            string msg = $"★[%s]:恭喜玩家[{user.Name}]通过在线充值获得元宝{arr[3]},金刚石{arr[4]},如果您也想获得,请赶快充值吧!";
                            recList.Add(msg);
                            arr[6] = "1";
                        }
                        sb.AppendLine($"{arr[0]},{arr[1]},{arr[2]},{arr[3]},{arr[4]},{arr[5]},{arr[6]}");
                    }
                }

                user.User_General.YB += yb;
                user.User_General.JGS += jgs;
                user.User_General.RegAmt += qty;

                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                userInI.Write("普通数据", "金刚石", user.User_General.JGS + "");
                userInI.Write("普通数据", "用户充值金额", user.User_General.RegAmt + "");

                FileHelper.DeleteFile(userRechargePath);
                FileHelper.WriteLine(userRechargePath, sb.ToString());

                return recList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 用户充值
        /// </summary>
        public static string UserRecharge(string userQQ, string regQQ, int num)
        {
            try
            {
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                string sa = config.Read("配置信息", "超级管理员");
                if (sa != userQQ) throw new Exception("指令错误或你没有GM权限!");

                UserInfo user = GetInfoByQQ(regQQ);
                int giftQty = 0;
                if (num >= 1 && num < 50)
                {
                    giftQty = num;
                }
                else if (num >= 50 && num < 100)
                {
                    giftQty = num * 2;
                }
                else if (num >= 100 && num < 300)
                {
                    giftQty = num * 3;
                }
                else if (num >= 30 && num < 500)
                {
                    giftQty = num * 4;
                }
                else if (num >= 500 && num < 1000)
                {
                    giftQty = num * 5;
                }
                else if (num >= 1000 && num < 2000)
                {
                    giftQty = num * 6;
                }
                else if (num >= 2000 && num < 3000)
                {
                    giftQty = num * 7;
                }
                else
                {
                    giftQty = num * 10;
                }
                int qty = giftQty + num;
                int jgs = qty;
                int yb = qty * 200;
                string userRechargePath = LegendFilePath.UserRechargePath + regQQ + ".txt";
                FileHelper.WriteLine(userRechargePath, $"{user.UserQQ},{user.Name},{num},{yb},{jgs},{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},0");
                return $"充值成功,[@{regQQ}]发送 领取充值 即可领取充值奖励!";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 设置活跃度
        /// </summary>
        /// <param name="formQQ"></param>
        /// <returns></returns>
        public static bool SetAllUserQty(string formQQ, int num)
        {
            try
            {
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                string sa = config.Read("配置信息", "超级管理员");
                if (sa != formQQ) throw new Exception("指令错误或你没有GM权限!");


                var filelist = Directory.GetFiles(LegendFilePath.UserPath, "*.ini");
                foreach (var file in filelist)
                {
                    config = new IniFileHelper(file);
                    int qty = int.Parse(config.Read("普通数据", "活跃值"));
                    qty += num;
                    config.Write("普通数据", "活跃值", qty.ToString());
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设置活跃度
        /// </summary>
        /// <param name="formQQ"></param>
        /// <returns></returns>
        public static bool SetAllUserQty(string formQQ, string toQQ, int num)
        {
            try
            {
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                string sa = config.Read("配置信息", "超级管理员");
                if (sa != formQQ) throw new Exception("指令错误或你没有GM权限!");

                var filelist = Directory.GetFiles(LegendFilePath.UserPath, "*.ini");
                string path = filelist.Where(f => f.EndsWith($"{toQQ}.ini")).FirstOrDefault();
                config = new IniFileHelper(path);

                int qty = int.Parse(config.Read("普通数据", "活跃值"));
                qty += num;
                config.Write("普通数据", "活跃值", qty.ToString());
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool ClearAllUserInfo(string formQQ)
        {
            try
            {
                IniFileHelper iniFileHelper = new IniFileHelper(LegendFilePath.ConfigString);
                string sa = iniFileHelper.Read("配置信息", "超级管理员");
                if (sa != formQQ)
                {
                    throw new Exception("指令错误或你不是超级管理员!");
                }

                var filelist = Directory.GetFiles(LegendFilePath.UserPath, "*.ini");
                foreach (var file in filelist)
                {
                    iniFileHelper = new IniFileHelper(file);
                    var list = iniFileHelper.GetAllKeys("人物信息");
                    foreach (var item in list)
                    {
                        switch (item)
                        {
                            case "序号": iniFileHelper.Write("人物信息", item, "0"); break;
                            case "是否在城里": iniFileHelper.Write("人物信息", item, "是"); break;
                            case "所在地图":
                            case "个人图片":
                            case "所在地图编号":
                            case "坐标":
                            case "职业":
                            case "性别": iniFileHelper.Write("人物信息", item, ""); break;
                            case "角色名称":
                            case "角色类型":
                            case "等级":
                            case "帐号":
                            case "元宝":
                            case "签到元宝数":
                            case "金币":
                            case "金刚石":
                            case "当前经验":
                            case "总经验":
                            case "下一级经验": break;
                            default: iniFileHelper.Write("人物信息", item, "0"); break;
                        }
                    }

                    list = iniFileHelper.GetAllKeys("属性点");
                    foreach (var item in list)
                    {
                        iniFileHelper.Write("属性点", item, "0");
                    }

                    list = iniFileHelper.GetAllKeys("装备物品");
                    foreach (var item in list)
                    {
                        iniFileHelper.Write("装备物品", item, "0");
                    }

                    list = iniFileHelper.GetAllKeys("背包物品");
                    foreach (var key in list)
                    {
                        if (!iniFileHelper.Delete("背包物品", key))
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// pk方法生成对战日志
        /// </summary>
        private static UserAttackRecord AttackPK(UserInfo user1, UserInfo user2, string msg, UserAttackRecord userAR)
        {
            IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
            int textmaxDc = int.Parse(config.Read("配置信息", "文字随机最高攻击"));

            List<MagicDB> magicDBList = LegendApi.GetMagicList();
            MagicDB magic;
            int dc = 0, maxdc = 0, skillKey = 0, magicQty = 0, hp = 0, userDc1 = 0, userDc2 = 0;

            #region 用户1攻击
            dc = user1.User_Attribute.Dc;//默认普通攻击下限
            maxdc = user1.User_Attribute.DcMax + 1;//默认普通攻击上限

            if (user1.IsUseSkills == "是")
            {
                magic = new MagicDB();
                skillKey = rnd.Next(0, user1.User_Skill.Count);
                var usK = user1.User_Skill[skillKey];
                if (user1.User_Attribute.Hp < user1.User_Attribute.MaxHp && user1.Job == "道士") usK = user1.User_Skill.Where(us => us.US_MagID == 2).FirstOrDefault();
                userAR.MagicDBModel1 = new MagicDB();
                switch (user1.Job)
                {
                    case "战士":
                        {
                            if (usK.US_IsEnableSkill == "是")
                            {
                                userAR.p_IsMagicAttck1 = true;
                                magicQty = rnd.Next(11);
                                if (magicQty < 4)
                                {
                                    magic = magicDBList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                    userAR.MagicDBModel1 = magic;
                                    dc = user1.User_Attribute.Dc * magic.Power;
                                    maxdc = user1.User_Attribute.DcMax * magic.MaxPower;
                                }
                            }
                        }
                        break;
                    case "法师":
                        {
                            if (usK.US_IsEnableSkill == "是")
                            {
                                userAR.p_IsMagicAttck1 = true;
                                magic = magicDBList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                userAR.MagicDBModel1 = magic;
                                dc = user1.User_Attribute.Mc * magic.Power;
                                maxdc = user1.User_Attribute.McMax * magic.MaxPower;
                            }
                        }
                        break;
                    case "道士":
                        {
                            if (user1.User_General.Lv >= 18)
                            {
                                if (usK.US_IsEnableSkill == "是")
                                {
                                    userAR.p_IsMagicAttck1 = true;
                                    magic = magicDBList.Where(mg => mg.MagName == "灵魂火符").FirstOrDefault();
                                    userAR.MagicDBModel1 = magic;
                                    dc = user1.User_Attribute.Sc * magic.Power;
                                    maxdc = user1.User_Attribute.ScMax * magic.MaxPower;
                                }
                            }
                            else
                            {
                                if (usK.US_IsEnableSkill == "是")
                                {
                                    magicQty = rnd.Next(11);
                                    if (magicQty < 4)
                                    {
                                        userAR.p_IsMagicAttck1 = true;
                                        magic = magicDBList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                        userAR.MagicDBModel1 = magic;
                                        switch (magic.MagName)
                                        {
                                            case "治愈术":
                                                dc = user1.User_Attribute.Sc * magic.Power;
                                                maxdc = user1.User_Attribute.ScMax * magic.MaxPower;
                                                hp = rnd.Next(dc, maxdc);
                                                user1.User_Attribute.Hp += hp;
                                                if (user1.User_Attribute.Hp > user1.User_Attribute.MaxHp) user1.User_Attribute.Hp = user1.User_Attribute.MaxHp;//  如果治愈术加的hp大于最大血量则等于当前等级的最大血量
                                                dc = 0;
                                                maxdc = 0;
                                                break;
                                            default:
                                                dc = user1.User_Attribute.Sc * magic.Power;
                                                maxdc = user1.User_Attribute.ScMax * magic.MaxPower;
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            userDc1 = rnd.Next(dc, maxdc);
            magicQty = rnd.Next(11);
            if (magicQty < 4)// 随机数1-3释放魔法,否则普通攻击,适用于道士
            {
                userDc1 -= rnd.Next(0, user2.User_Attribute.Agile) + rnd.Next(user2.User_Attribute.Ac, user2.User_Attribute.Ac2);
            }
            else
            {
                userDc1 -= rnd.Next(user2.User_Attribute.Ac, user2.User_Attribute.Ac2);
            }

            if (userDc1 < 0 && (userAR.MagicDBModel1 != null || userAR.MagicDBModel2 != null))
            {
                userAR.p_attack1 = -1;
                userAR.p_msg1 = "攻击无效";
            }
            else if (userDc1 == 0 && (userAR.MagicDBModel1 != null || userAR.MagicDBModel2 != null))
            {
                userAR.p_attack1 = 0;
                userAR.p_msg1 = "丢失";
            }
            else
            {
                if (!string.IsNullOrEmpty(msg) && user1.User_General.PKMsg != msg)
                {
                    user1.User_General.PKMsg = msg;
                }
                user1.User_General.PKdc = GenerateRandomNumber(user1.User_General.PKMsg, textmaxDc);
                userDc1 += user1.User_General.PKdc;
                userAR.p_attack1 = userDc1;
                user2.User_Attribute.Hp -= userDc1;
            }
            userAR.p_hp2 = user2.User_Attribute.Hp;
            #endregion

            if (userAR.p_hp2 > 0)
            {
                #region 用户2攻击
                dc = user2.User_Attribute.Dc;//默认普通攻击下限
                maxdc = user2.User_Attribute.DcMax + 1;//默认普通攻击上限

                if (user2.IsUseSkills == "是")
                {
                    magic = new MagicDB();
                    skillKey = rnd.Next(0, user2.User_Skill.Count);
                    var usK = user2.User_Skill[skillKey];
                    userAR.MagicDBModel2 = new MagicDB();

                    switch (user2.Job)
                    {
                        case "战士":
                            {
                                if (usK.US_IsEnableSkill == "是")
                                {
                                    userAR.p_IsMagicAttck2 = true;
                                    magicQty = rnd.Next(11);
                                    if (magicQty > 0 && magicQty < 4)
                                    {
                                        magic = magicDBList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                        userAR.MagicDBModel2 = magic;
                                        dc = user2.User_Attribute.Dc * magic.Power;
                                        maxdc = user2.User_Attribute.DcMax * magic.MaxPower;
                                    }
                                }
                            }
                            break;
                        case "法师":
                            {
                                if (usK.US_IsEnableSkill == "是")
                                {
                                    userAR.p_IsMagicAttck2 = true;
                                    magic = magicDBList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                    userAR.MagicDBModel2 = magic;
                                    dc = user2.User_Attribute.Mc * magic.Power;
                                    maxdc = user2.User_Attribute.McMax * magic.MaxPower;
                                }
                            }
                            break;
                        case "道士":
                            {
                                if (user2.User_General.Lv >= 18)
                                {
                                    if (usK.US_IsEnableSkill == "是")
                                    {
                                        userAR.p_IsMagicAttck2 = true;
                                        magic = magicDBList.Where(mg => mg.MagName == "灵魂火符").FirstOrDefault();
                                        userAR.MagicDBModel2 = magic;
                                        dc = user2.User_Attribute.Sc * magic.Power;
                                        maxdc = user2.User_Attribute.ScMax * magic.MaxPower;
                                    }
                                }
                                else
                                {
                                    if (usK.US_IsEnableSkill == "是")
                                    {
                                        magicQty = rnd.Next(11);
                                        if (magicQty > 0 && magicQty < 4)
                                        {
                                            userAR.p_IsMagicAttck2 = true;
                                            magic = magicDBList.Where(mg => mg.MagID == usK.US_MagID).FirstOrDefault();
                                            userAR.MagicDBModel2 = magic;
                                            switch (magic.MagName)
                                            {
                                                case "治愈术":
                                                    dc = 0;
                                                    maxdc = 0;
                                                    hp = rnd.Next(magic.Power, magic.MaxPower);
                                                    user2.User_Attribute.Hp += hp;
                                                    if (user2.User_Attribute.Hp > user2.User_Attribute.MaxHp) user2.User_Attribute.Hp = user2.User_Attribute.MaxHp;//  如果治愈术加的hp大于最大血量则等于当前等级的最大血量
                                                    break;
                                                default:
                                                    dc = user2.User_Attribute.Sc * magic.Power;
                                                    maxdc = user2.User_Attribute.ScMax * magic.MaxPower;
                                                    break;
                                            }

                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                userDc2 = rnd.Next(dc, maxdc);
                magicQty = rnd.Next(11);
                if (magicQty > 0 && magicQty < 4)// 随机数1-3释放魔法,否则普通攻击,适用于道士
                {
                    userDc2 -= rnd.Next(0, user1.User_Attribute.Agile) + rnd.Next(user1.User_Attribute.Ac, user1.User_Attribute.Ac2);
                }
                else
                {
                    userDc2 -= rnd.Next(user1.User_Attribute.Ac, user1.User_Attribute.Ac2);
                }

                if (userDc2 < 0 && (userAR.MagicDBModel1 != null || userAR.MagicDBModel2 != null))
                {
                    userAR.p_attack2 = -1;
                    userAR.p_msg2 = "攻击无效";
                }
                else if (userDc2 == 0 && (userAR.MagicDBModel1 != null || userAR.MagicDBModel2 != null))
                {
                    userAR.p_attack2 = 0;
                    userAR.p_msg2 = "丢失";
                }
                else
                {
                    if (!string.IsNullOrEmpty(user2.User_General.PKMsg))
                    {
                        userDc2 += user1.User_General.PKdc;
                    }
                    else
                    {
                        userDc2 += rnd.Next(0, textmaxDc / 3);
                    }

                    userAR.p_attack2 = userDc2;
                    user1.User_Attribute.Hp -= userDc2;
                }
                userAR.p_hp1 = user1.User_Attribute.Hp < 0 ? 0 : user1.User_Attribute.Hp;
                #endregion
            }

            return userAR;
        }

        // 计算中文文字的“厉害”程度,可以通过字符的 Unicode 编码值总和来衡量
        public static int CalculateTextStrength(string text)
        {
            int strength = 0;

            foreach (char c in text)
            {
                // 通过字符的 Unicode 编码值来累加,越厉害的字,Unicode 值越大
                strength += c;
            }

            return strength;
        }

        /// <summary>
        /// 根据文字生成一个随机数,越厉害的文本对应越高的随机数
        /// </summary>
        public static int GenerateRandomNumber(string text, int maxValue)
        {
            // 计算文本的“厉害”程度
            int strength = CalculateTextStrength(text);

            // 使用文本的“厉害”程度作为随机数生成的种子
            Random random = new Random(strength);
            int nd = random.Next(0, maxValue);
            //nd = rnd.Next(0, maxValue);
            // 根据计算出来的种子生成一个指定范围内的随机数
            return nd;
        }

        /// <summary>
        /// 获取用户PK的数据
        /// </summary>
        /// <param name="user1">用户1</param>
        /// <param name="user2">用户2</param>
        /// <param name="msg">话术</param>
        /// <returns></returns>
        public static List<UserAttackRecord> GetUserPKData(UserInfo user1, UserInfo user2, string msg)
        {
            try
            {
                List<UserAttackRecord> userAttackRecords = new List<UserAttackRecord>();
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                int deduct = int.Parse(config.Read("配置信息", "pk扣除活跃值"));

                user1.User_General.ActivityQty -= deduct;//PK的活跃值
                if (user1.User_General.ActivityQty < 0) throw new Exception("您当前活跃值不够,每天签到得20点活跃值 或 每3分钟得1点活跃值.");

                user1.User_General.ActivityTime = DateTime.Now;

                UserAttackRecord userAR;
                while (user1.User_Attribute.Hp > 0 && user2.User_Attribute.Hp > 0)
                {
                    userAR = new UserAttackRecord()
                    {
                        p_id1 = user1.Id,
                        p_id2 = user2.Id,
                        p_hp1 = user1.User_Attribute.Hp,
                        p_hp2 = user2.User_Attribute.Hp,
                    };
                    userAR = AttackPK(user1, user2, msg, userAR);
                    userAttackRecords.Add(userAR);
                }

                int reCount = int.Parse(config.Read("配置信息", "战斗记录回合数"));
                int skip = Math.Max(userAttackRecords.Count - reCount, 0);
                // 战斗记录只取最后10条
                userAttackRecords = userAttackRecords.Skip(skip).Take(reCount).ToList();
                string newPath = LegendFilePath.UserPath + user1.UserQQ + ".ini";
                IniFileHelper userINI = new IniFileHelper(newPath);
                userINI.Write("普通数据", "活跃值", user1.User_General.ActivityQty.ToString());
                userINI.Write("普通数据", "活跃时间", user1.User_General.ActivityTime.ToString());
                int.TryParse(userINI.Read("普通数据", "PK点数"), out int pk);
                pk += rnd.Next(1, 11);
                userINI.Write("普通数据", "PK点数", pk.ToString());
                if (!string.IsNullOrEmpty(msg) && user1.User_General.PKMsg != msg)
                    user1.User_General.PKMsg = msg;
                userINI.Write("普通数据", "PK消息", user1.User_General.PKMsg);

                if (user1.User_Attribute.Hp > 0)
                {
                    int.TryParse(userINI.Read("普通数据", "PK赢"), out int slCount);
                    slCount++;
                    userINI.Write("普通数据", "PK赢", slCount.ToString());
                }
                if (user2.User_Attribute.Hp > 0)
                {
                    int.TryParse(userINI.Read("普通数据", "PK输"), out int sbCount);
                    sbCount++;
                    userINI.Write("普通数据", "PK输", sbCount.ToString());
                }
                return userAttackRecords;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 物品叠加-重新整理
        /// </summary>
        /// <param name="user"></param>
        public static void RefreshOverlayStdItems(UserInfo user)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper iniUser = new IniFileHelper(path);
                var list = iniUser.GetAllKeys("背包物品");
                List<StditemDB> listDb = new List<StditemDB>();
                StditemDB db = null, ndb = null;
                List<string> keys = new List<string>();
                foreach (var key in list)
                {
                    db = Api.JsonDeserialize<StditemDB>(iniUser.ReadValue("背包物品", key));
                    // 判断物品是否有叠加,如果有叠加则从第1个加起来
                    if (db != null && db.OverLap > 0)
                    {
                        // 获取叠加物品是否存在,如果存在则累计,否则新添加叠加物品
                        ndb = listDb.Where(s => s.StdID == db.StdID && s.Duramax > s.SubAttr1).FirstOrDefault();

                        if (ndb == null)
                        {
                            if (db.SubAttr1 == 0)//背包里刚打到新物品添加到叠加以前物品上
                            {
                                db.SubAttr1 += 1;
                                listDb.Add(db);
                                keys.Add(key);//  保存叠加物品key,用于将后续叠加物品累计到当前key中
                            }
                            else
                            {
                                listDb.Add(db);//默认添加叠加物品,不累计数量
                                keys.Add(key);
                            }
                        }
                        else
                        {
                            if (ndb.SubAttr1 >= ndb.Duramax)
                            {
                                db.SubAttr1 += 1;
                                listDb.Add(db);
                                keys.Add(key);//  保存叠加物品key,用于将后续叠加物品累计到当前key中
                            }
                            else
                            {
                                ndb.SubAttr1 += db.SubAttr1;
                                iniUser.Delete("背包物品", key);// 删除重复的物品
                            }
                        }
                    }
                }
                // 重新将叠加的物品写入ini文件中
                for (int i = 0; i < listDb.Count; i++)
                {
                    iniUser.Write("背包物品", keys[i], Api.JsonSerialize(listDb[i]));
                }
                listDb = new List<StditemDB>();

                list = iniUser.GetAllKeys("背包物品");
                foreach (var key in list)
                {
                    db = Api.JsonDeserialize<StditemDB>(iniUser.ReadValue("背包物品", key));
                    if (db != null)
                    {
                        listDb.Add(db);
                    }
                }
                listDb = listDb.OrderBy(d => d.StdMode).ThenBy(d => d.StdID).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    iniUser.Write("背包物品", list[i], Api.JsonSerialize(listDb[i]));
                }
                list = iniUser.GetAllKeys("背包物品");
                user.PageQty = 96 - list.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 设置用户是否开启发送动图
        /// </summary>
        public static bool SetUserIsOpenGifImg(UserInfo user, bool isOpen)
        {
            try
            {
                if (isOpen)
                {
                    user.IsSendMsgImg = "是";
                }
                else
                {
                    user.IsSendMsgImg = "否";
                }
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper iniFileHelper = new IniFileHelper(path);
                iniFileHelper.Write("人物信息", "开启发送动图", user.IsSendMsgImg);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取当前QQ对应的传奇用户信息
        /// </summary>
        public static UserInfo GetInfoByQQ(string userQQ)
        {
            try
            {
                string path = LegendFilePath.UserPath + userQQ + ".ini";
                if (!File.Exists(path))
                {
                    throw new Exception("您还没有签到,先发送 签到 可领取元宝才能开始注册喔.");
                }

                IniFileHelper userINI = new IniFileHelper(path);
                
                #region 人物信息
                UserInfo user = new UserInfo()
                {
                    Id = int.Parse(userINI.Read("人物信息", "序号")),
                    Name = userINI.Read("人物信息", "角色名称"),
                    RoleType = userINI.Read("人物信息", "角色类型"),
                    Sex = userINI.Read("人物信息", "性别"),
                    Job = userINI.Read("人物信息", "职业"),
                    Map = userINI.Read("人物信息", "所在地图"),
                    Pos = userINI.Read("人物信息", "坐标"),
                    UserQQ = userINI.Read("人物信息", "帐号"),
                    IsOnCity = userINI.Read("人物信息", "是否在城里"),
                    MapCode = userINI.Read("人物信息", "所在地图编号"),
                    UserImagePath = userINI.Read("人物信息", "个人图片"),
                    IsSendMsgImg = userINI.Read("人物信息", "开启发送动图"),
                    IsSendMsgByLv7 = userINI.Read("人物信息", "是否发送等级消息7级"),
                    IsUseSkills = userINI.Read("人物信息", "是否使用技能攻击"),
                    IsOnTitle = userINI.Read("人物信息", "是否开启称号"),
                    IsOnYHZL = userINI.Read("人物信息", "是否开启永恒之力"),
                };
                if (string.IsNullOrEmpty(user.IsUseSkills)) user.IsUseSkills = "否";
               
                DateTime.TryParse(userINI.Read("人物信息", "补签时间"), out DateTime uctime);
                user.UserCountersignatureTime = uctime;

                var bpList = userINI.GetAllKeys("背包物品");
                user.PageQty = 96 - bpList.Count;

                int.TryParse(userINI.Read("人物信息", "自动吃药数额"), out int userSetAutoMedicationQTY);
                user.UserSetAutoMedicationQTY = userSetAutoMedicationQTY;
                user.UserSetAutoMedicationQTYType = userINI.Read("人物信息", "自动吃药类型");

                user.UserImagePath = LegendFilePath.LegendIMGDataPath + user.UserImagePath;
                int.TryParse(userINI.Read("人物信息", "城中杀怪数量"), out int citkillNum);
                user.KillCityMonNum = citkillNum;

                user.User_EquRecycleAttr = GetEquRecycleAttr(userINI);
                if (user.IsOnTitle == "是") user.User_TitleAttribute = GetTitleAttribute(userINI);
                
                string legenPath = "";
                switch (user.Job)
                {
                    case "战士":
                        user.IsSendMsgByLv19 = userINI.Read("人物信息", "是否发送等级消息19级");
                        user.IsSendMsgByLv25 = userINI.Read("人物信息", "是否发送等级消息25级");
                        user.IsSendMsgByLv28 = userINI.Read("人物信息", "是否发送等级消息28级");
                        user.IsSendMsgByLv30 = userINI.Read("人物信息", "是否发送等级消息30级");
                        user.IsSendMsgByLv35 = userINI.Read("人物信息", "是否发送等级消息35级");
                        legenPath = LegendFilePath.UserAttrPath + "战士.txt";
                        break;
                    case "法师":
                        user.IsSendMsgByLv12 = userINI.Read("人物信息", "是否发送等级消息12级");
                        user.IsSendMsgByLv13 = userINI.Read("人物信息", "是否发送等级消息13级");
                        user.IsSendMsgByLv16 = userINI.Read("人物信息", "是否发送等级消息16级");
                        user.IsSendMsgByLv17 = userINI.Read("人物信息", "是否发送等级消息17级");
                        user.IsSendMsgByLv19 = userINI.Read("人物信息", "是否发送等级消息19级");
                        user.IsSendMsgByLv20 = userINI.Read("人物信息", "是否发送等级消息20级");
                        user.IsSendMsgByLv22 = userINI.Read("人物信息", "是否发送等级消息22级");
                        user.IsSendMsgByLv24 = userINI.Read("人物信息", "是否发送等级消息24级");
                        user.IsSendMsgByLv26 = userINI.Read("人物信息", "是否发送等级消息26级");
                        user.IsSendMsgByLv30 = userINI.Read("人物信息", "是否发送等级消息30级");
                        user.IsSendMsgByLv31 = userINI.Read("人物信息", "是否发送等级消息31级");
                        user.IsSendMsgByLv32 = userINI.Read("人物信息", "是否发送等级消息32级");
                        user.IsSendMsgByLv35 = userINI.Read("人物信息", "是否发送等级消息35级");
                        legenPath = LegendFilePath.UserAttrPath + "法师.txt";
                        break;
                    case "道士":
                        user.IsSendMsgByLv9 = userINI.Read("人物信息", "是否发送等级消息9级");
                        user.IsSendMsgByLv14 = userINI.Read("人物信息", "是否发送等级消息14级");
                        user.IsSendMsgByLv18 = userINI.Read("人物信息", "是否发送等级消息18级");
                        user.IsSendMsgByLv19 = userINI.Read("人物信息", "是否发送等级消息19级");
                        user.IsSendMsgByLv20 = userINI.Read("人物信息", "是否发送等级消息20级");
                        user.IsSendMsgByLv21 = userINI.Read("人物信息", "是否发送等级消息21级");
                        user.IsSendMsgByLv22 = userINI.Read("人物信息", "是否发送等级消息22级");
                        user.IsSendMsgByLv25 = userINI.Read("人物信息", "是否发送等级消息25级");
                        user.IsSendMsgByLv26 = userINI.Read("人物信息", "是否发送等级消息26级");
                        user.IsSendMsgByLv28 = userINI.Read("人物信息", "是否发送等级消息28级");
                        user.IsSendMsgByLv33 = userINI.Read("人物信息", "是否发送等级消息33级");
                        user.IsSendMsgByLv35 = userINI.Read("人物信息", "是否发送等级消息35级");
                        legenPath = LegendFilePath.UserAttrPath + "道士.txt";
                        break;
                    default:
                        switch (rnd.Next(1, 4))
                        {
                            case 1: legenPath = LegendFilePath.UserAttrPath + "战士.txt"; break;
                            case 2: legenPath = LegendFilePath.UserAttrPath + "法师.txt"; break;
                            case 3: legenPath = LegendFilePath.UserAttrPath + "道士.txt"; break;
                        }

                        break;
                }
                #endregion

                #region 人物基本信息
                user.User_General = new UserGeneral();
                {
                    user.User_General.Lv = int.Parse(userINI.Read("普通数据", "等级"));
                    user.User_General.JB = long.Parse(userINI.Read("普通数据", "金币"));
                    user.User_General.REP = int.Parse(userINI.Read("普通数据", "声望"));
                    user.User_General.JGS = int.Parse(userINI.Read("普通数据", "金刚石"));
                    user.User_General.YB = long.Parse(userINI.Read("普通数据", "元宝"));
                    user.User_General.PkPoint = int.Parse(userINI.Read("普通数据", "PK点数"));
                    user.User_General.SumExp = long.Parse(userINI.Read("普通数据", "总经验"));
                    user.User_General.NextExp = long.Parse(userINI.Read("普通数据", "下一级经验"));
                    user.User_General.CurtYB = int.Parse(userINI.Read("普通数据", "签到元宝数"));
                    user.User_General.Exp = int.Parse(userINI.Read("普通数据", "当前经验"));
                    user.User_General.PKMsg = userINI.Read("普通数据", "PK消息");
                };
               
                int.TryParse(userINI.Read("普通数据", "VIP等级"), out int VipLv);
                user.User_General.VipLv = VipLv;
                int.TryParse(userINI.Read("普通数据", "用户充值金额"), out int regAmt);
                user.User_General.RegAmt = regAmt;
                int.TryParse(userINI.Read("普通数据", "PK输"), out int pklose);
                user.User_General.PKLose = pklose;
                int.TryParse(userINI.Read("普通数据", "PK赢"), out int pkwin);
                user.User_General.PKWin = pkwin;
                int.TryParse(userINI.Read("普通数据", "排位输"), out int pkpwlose);
                user.User_General.PKPwLose = pkpwlose;
                int.TryParse(userINI.Read("普通数据", "排位赢"), out int pkpwwin);
                user.User_General.PKPwWin = pkpwwin;
                int.TryParse(userINI.Read("普通数据", "背包重量"), out int bgw);
                user.User_General.Bgw = bgw;
                int.TryParse(userINI.Read("普通数据", "最大背包重量"), out int bgwmax);
                user.User_General.BgwMax = bgwmax;
                int.TryParse(userINI.Read("普通数据", "穿戴重量"), out int Wear);
                user.User_General.Wear = Wear;
                int.TryParse(userINI.Read("普通数据", "最大穿戴重量"), out int WearMax);
                user.User_General.WearMax = WearMax;
                int.TryParse(userINI.Read("普通数据", "活跃值"), out int activityQty);
                user.User_General.ActivityQty = activityQty;
                #endregion

                #region 人物基本属性
                
                string legenStr = FileHelper.ReadTxt(legenPath);
                
                List<LengendBase> LengendList = Api.JsonDeserialize<List<LengendBase>>(legenStr);
                LengendBase lb = LengendList.Where(l => l.Level == user.User_General.Lv).FirstOrDefault();
                
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
                    user.User_General.WearMax = lb.Wear;
                    user.User_General.BgwMax = lb.Bgw;
                }
                
                #endregion

                #region 添加人物装备属性
                
                int maxWeight = 0;
                var listzb = userINI.GetAllKeys("装备物品");
                foreach (string key in listzb)
                {
                    var zbKey = userINI.Read("装备物品", key);

                    if (zbKey != "0" && zbKey.Length > 0)
                    {
                        var zb = userINI.ReadValue("装备属性", zbKey);
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
                
                #endregion

                #region 添加VIP属性
                user.User_VIPAttribute = new UserVIPAttribute();
                int.TryParse(userINI.Read("VIP属性", "Hp"), out int vipHp);
                user.User_VIPAttribute.Hp = vipHp;
                int.TryParse(userINI.Read("VIP属性", "Mp"), out int vipMp);
                user.User_VIPAttribute.Mp = vipMp;
                int.TryParse(userINI.Read("VIP属性", "防御下限"), out int vipAc);
                user.User_VIPAttribute.Ac = vipAc;
                int.TryParse(userINI.Read("VIP属性", "防御上限"), out int vipAc2);
                user.User_VIPAttribute.Ac2 = vipAc2;
                int.TryParse(userINI.Read("VIP属性", "魔防下限"), out int vipMac);
                user.User_VIPAttribute.Mac = vipMac;
                int.TryParse(userINI.Read("VIP属性", "魔防上限"), out int vipMac2);
                user.User_VIPAttribute.Mac2 = vipMac2;
                int.TryParse(userINI.Read("VIP属性", "攻击下限"), out int vipDc);
                user.User_VIPAttribute.Dc = vipDc;
                int.TryParse(userINI.Read("VIP属性", "攻击上限"), out int vipDcMax);
                user.User_VIPAttribute.DcMax = vipDcMax;
                int.TryParse(userINI.Read("VIP属性", "魔法下限"), out int vipMc);
                user.User_VIPAttribute.Mc = vipMc;
                int.TryParse(userINI.Read("VIP属性", "魔法上限"), out int vipMcMax);
                user.User_VIPAttribute.McMax = vipMcMax;
                int.TryParse(userINI.Read("VIP属性", "道术下限"), out int vipSc);
                user.User_VIPAttribute.Sc = vipSc;
                int.TryParse(userINI.Read("VIP属性", "道术上限"), out int vipScMax);
                user.User_VIPAttribute.ScMax = vipScMax;
                int.TryParse(userINI.Read("VIP属性", "倍功"), out int douBPower);
                user.User_VIPAttribute.DouBPower = douBPower;
                int.TryParse(userINI.Read("VIP属性", "打怪经验"), out int vipMonExpInc);
                user.User_VIPAttribute.MonExpInc = vipMonExpInc;
                user.User_VIPAttribute.AutoAFK = userINI.Read("VIP属性", "自动挂机");
                user.User_VIPAttribute.AutoRecycl = userINI.Read("VIP属性", "自动回收");
                user.User_VIPAttribute.AutoBoosTime = userINI.Read("VIP属性", "专属BOSS击杀时间");
                int.TryParse(userINI.Read("VIP属性", "专属BOOS"), out int AutoBoosCount);
                user.User_VIPAttribute.AutoBoos = AutoBoosCount;
                int.TryParse(userINI.Read("VIP属性", "已击杀专属BOOS数量"), out int AutoPBoos);
                user.User_VIPAttribute.AutoPBoos = AutoPBoos;
                user.User_VIPAttribute.IsRecycleYB = userINI.Read("VIP属性", "是否回收元宝");
                int.TryParse(userINI.Read("VIP属性", "回收元宝倍率"), out int RecycleYBQty);
                user.User_VIPAttribute.RecycleYBQty = RecycleYBQty;

                if (!string.IsNullOrEmpty(user.User_VIPAttribute.AutoBoosTime))
                {
                    DateTime dateTime = DateTime.Parse(user.User_VIPAttribute.AutoBoosTime);
                    if (dateTime.Day != DateTime.Now.Day && AutoPBoos >= AutoBoosCount)// 如果打专属boss时间不一致表示不同一天打的,需要清零
                        user.User_VIPAttribute.AutoPBoos = 0;
                }

                user.User_Attribute.Hp += vipHp;
                user.User_Attribute.MaxHp += vipHp;
                user.User_Attribute.Mp += vipMp;
                user.User_Attribute.MaxMp += vipMp;
                user.User_Attribute.Ac += vipAc;
                user.User_Attribute.Ac2 += vipAc2;
                user.User_Attribute.Mac += vipMac;
                user.User_Attribute.Mac2 += vipMac2;
                user.User_Attribute.Dc += vipDc;
                user.User_Attribute.DcMax += vipDcMax;
                user.User_Attribute.Mc += vipMc;
                user.User_Attribute.McMax += vipMcMax;
                user.User_Attribute.Sc += vipSc;
                user.User_Attribute.ScMax += vipScMax;
                user.User_Attribute.DouBPower += douBPower;
               
                #endregion

                #region 添加永恒之力属性
                user.User_YHZLAttribute = new YHZLAttribute();
                if (user.IsOnYHZL == "是")
                {
                    int.TryParse(userINI.Read("永恒之力属性", "倍功"), out int yhzl);
                    user.User_YHZLAttribute.DouBPower = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "攻击下限"), out yhzl);
                    user.User_YHZLAttribute.Dc = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "攻击上限"), out yhzl);
                    user.User_YHZLAttribute.DcMax = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "魔法下限"), out yhzl);
                    user.User_YHZLAttribute.Mc = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "魔法上限"), out yhzl);
                    user.User_YHZLAttribute.McMax = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "道术下限"), out yhzl);
                    user.User_YHZLAttribute.Sc = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "道术上限"), out yhzl);
                    user.User_YHZLAttribute.ScMax = yhzl;
                    int.TryParse(userINI.Read("永恒之力属性", "Hp"), out yhzl);
                    user.User_YHZLAttribute.Hp = yhzl;

                    user.User_Attribute.Hp += user.User_YHZLAttribute.Hp;
                    user.User_Attribute.MaxHp += user.User_YHZLAttribute.Hp;
                    user.User_Attribute.DouBPower += user.User_YHZLAttribute.DouBPower;
                    user.User_Attribute.Dc += user.User_YHZLAttribute.Dc;
                    user.User_Attribute.DcMax += user.User_YHZLAttribute.DcMax;
                    user.User_Attribute.Mc += user.User_YHZLAttribute.Mc;
                    user.User_Attribute.McMax += user.User_YHZLAttribute.McMax;
                    user.User_Attribute.Sc += user.User_YHZLAttribute.DouBPower;
                    user.User_Attribute.ScMax += user.User_YHZLAttribute.DouBPower;
                }
               
                #endregion

                #region 添加转生属性
                int.TryParse(userINI.Read("转生属性", "倍功"), out int zsbg);
                int.TryParse(userINI.Read("转生属性", "等级"), out int zsdj);
                user.User_RenewLevelAttribute = new UserRenewLevelAttribute
                {
                    DouBPower = zsbg,
                    Level = zsdj,
                };
                user.User_Attribute.DouBPower += user.User_RenewLevelAttribute.DouBPower;
                #endregion

                #region 添加礼包属性
                int.TryParse(userINI.Read("礼包属性", "怪物爆率"), out int MonExpRate);
                user.User_GiftAttribute = new UserGiftAttribute
                {
                    MonExpRate = MonExpRate
                };
                user.User_Attribute.MonExpRate += MonExpRate;
                #endregion
               
                #region 添加人物称号属性
                if (user.User_TitleAttribute != null)
                {
                    user.User_TitleAttribute.ForEach(ut =>
                    {
                        user.User_Attribute.Hp += ut.Hp;
                        user.User_Attribute.MaxHp += ut.Hp;
                        user.User_Attribute.Mp += ut.Mp;
                        user.User_Attribute.MaxMp += ut.Mp;
                        user.User_Attribute.Acc += ut.Acc;
                        user.User_Attribute.Agile += ut.Agile;
                        user.User_Attribute.DouBPower += ut.DouBPower;
                        user.User_Attribute.MagicEvas += ut.MagicEvas;
                        user.User_Attribute.AttackSpeed += ut.AttackSpeed;
                        user.User_Attribute.PhysicalDamageReduc += ut.PhysicalDamageReduc;
                        user.User_Attribute.MagicDamageReduc += ut.MagicDamageReduc;
                        user.User_Attribute.PhysicalDamageReflex += ut.PhysicalDamageReflex;
                        user.User_Attribute.MagicDamageReflex += ut.MagicDamageReflex;
                        user.User_Attribute.HealsPR += ut.HealsPR;
                        user.User_Attribute.AttackPR += ut.AttackPR;
                        user.User_Attribute.MagicAC += ut.MagicAC;
                        user.User_Attribute.IgnoreAC += ut.IgnoreAC;
                        user.User_Attribute.DamageRebound += ut.DamageRebound;
                        user.User_Attribute.UserExpRate += ut.UserExpRate;
                        user.User_Attribute.HPIncrease += ut.HPIncrease;
                        user.User_Attribute.MPIncrease += ut.MPIncrease;
                        user.User_Attribute.MonExpRate += ut.MonExpRate;
                        user.User_Attribute.EquBurstRate += ut.EquBurstRate;
                        user.User_Attribute.FatalInjury += ut.FatalInjury;
                        user.User_Attribute.FatalBlow += ut.FatalBlow;
                        user.User_Attribute.ExcellentBlow += ut.ExcellentBlow;
                        user.User_Attribute.HeartfeltBlow += ut.HeartfeltBlow;
                    });
                }
                #endregion


                user.User_Items = new UserItems
                {
                    DRESS = userINI.Read("装备物品", "衣服"),
                    WEAPON = userINI.Read("装备物品", "武器"),
                    RIGHTHAND = userINI.Read("装备物品", "勋章"),
                    HELMET = userINI.Read("装备物品", "头盔"),
                    NECKLACE = userINI.Read("装备物品", "项链"),
                    RING_R = userINI.Read("装备物品", "右戒指"),
                    RING_L = userINI.Read("装备物品", "左戒指"),
                    ARMRING_R = userINI.Read("装备物品", "右手镯"),
                    ARMRING_L = userINI.Read("装备物品", "左手镯"),
                    BUJUK = userINI.Read("装备物品", "护身符"),
                    BELT = userINI.Read("装备物品", "腰带"),
                    BOOTS = userINI.Read("装备物品", "鞋子"),
                    CHARM = userINI.Read("装备物品", "宝石"),
                    HAT = userINI.Read("装备物品", "斗笠"),
                    DRUM = userINI.Read("装备物品", "军鼓"),
                    SHIELD = userINI.Read("装备物品", "盾牌"),
                    HORSE = userINI.Read("装备物品", "马牌"),
                    SZ_DRESS = userINI.Read("装备物品", "时装衣服"),
                    SZ_WEAPON = userINI.Read("装备物品", "时装武器"),
                    SZ_ARMRING_L = userINI.Read("装备物品", "时装左手镯"),
                    SZ_ARMRING_R = userINI.Read("装备物品", "时装右手镯"),
                    SZ_BELT = userINI.Read("装备物品", "时装腰带"),
                    SZ_BOOTS = userINI.Read("装备物品", "时装鞋子"),
                    SZ_BUJUK = userINI.Read("装备物品", "时装护身符"),
                    SZ_CHARM = userINI.Read("装备物品", "时装宝石"),
                    SZ_DRUM = userINI.Read("装备物品", "时装军鼓"),
                    SZ_HELMET = userINI.Read("装备物品", "时装头盔"),
                    SZ_NECKLACE = userINI.Read("装备物品", "时装项链"),
                    SZ_RING_L = userINI.Read("装备物品", "时装左戒指"),
                    SZ_RING_R = userINI.Read("装备物品", "时装右戒指"),
                };
                if (user.User_Items.WEAPON.Length > 5) user.IsUseWeapon = true;
                
                user.User_General.ActivityQty = activityQty;
                string strTime = userINI.Read("普通数据", "活跃时间");
               
                if (!string.IsNullOrEmpty(strTime))
                {
                   
                    IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                    int maxAt = int.Parse(config.Read("配置信息", "最大活跃值"));
                    int atTime = int.Parse(config.Read("配置信息", "获得活跃值间隔时间"));
                   
                    DateTime startTime = DateTime.Parse(strTime);
                    TimeSpan minuteSpan = new TimeSpan(DateTime.Now.Ticks - startTime.Ticks);
                    int aqTimeQty = (int)minuteSpan.TotalMinutes / atTime;
                    
                    int maxHHP = int.Parse(config.Read("配置信息", "每分钟回红"));
                    int maxHMP = int.Parse(config.Read("配置信息", "每分钟回蓝"));
                    user.User_Attribute.Hp += aqTimeQty * maxHHP;
                    user.User_Attribute.Mp += aqTimeQty * maxHMP;
                    if (user.User_Attribute.Hp > user.User_Attribute.MaxHp)
                    {
                        user.User_Attribute.Hp = user.User_Attribute.MaxHp;
                    }
                    if (user.User_Attribute.Mp > user.User_Attribute.MaxMp)
                    {
                        user.User_Attribute.Mp = user.User_Attribute.MaxMp;
                    }
                    if (maxAt > user.User_General.ActivityQty)
                        user.User_General.ActivityQty += 10;
                    else
                        user.User_General.ActivityQty = 200;
                    user.User_General.ActivityTime = DateTime.Now;
                    
                }
                
                #region 添加技能列表
                UserSkill us = null;
                var usKeyList = userINI.GetAllKeys("个人技能");
                user.User_Skill = new List<UserSkill>();
                foreach (var usKey in usKeyList)
                {
                    us = Api.JsonDeserialize<UserSkill>(userINI.Read("个人技能", usKey));
                    if (us.US_IsEnableSkill == "是") user.User_Skill.Add(us);
                }
                #endregion
 
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<UserTitleAttribute> GetTitleAttribute(IniFileHelper userINI)
        {
            List<UserTitleAttribute> list = new List<UserTitleAttribute>();
            var keyList = userINI.GetAllKeys("个人称号");
            foreach (var key in keyList)
            {
                UserTitleAttribute title = Api.JsonDeserialize<UserTitleAttribute>(userINI.ReadValue("个人称号", key));
                list.Add(title);
            }
            return list;
        }
        /// <summary>
        /// 获取装备回收属性
        /// </summary>
        static UserEquRecycleAttr GetEquRecycleAttr(IniFileHelper userINI)
        {
            UserEquRecycleAttr userERA = new UserEquRecycleAttr();
            userERA.AutoRecycl = userINI.Read("装备回收属性", "自动回收");
            if (string.IsNullOrEmpty(userERA.AutoRecycl)) userINI.Write("装备回收属性", "自动回收", "否");
            userERA.AutoRecyclType1 = userINI.Read("装备回收属性", "垃圾系列");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType1)) userINI.Write("装备回收属性", "垃圾系列", "否");
            userERA.AutoRecyclType2 = userINI.Read("装备回收属性", "沃玛系列");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType2)) userINI.Write("装备回收属性", "沃玛系列", "否");
            userERA.AutoRecyclType3 = userINI.Read("装备回收属性", "祖玛系列");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType3)) userINI.Write("装备回收属性", "祖玛系列", "否");
            userERA.AutoRecyclType4 = userINI.Read("装备回收属性", "赤月系列");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType4)) userINI.Write("装备回收属性", "赤月系列", "否");
            userERA.AutoRecyclType5 = userINI.Read("装备回收属性", "永恒首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType5)) userINI.Write("装备回收属性", "永恒首饰", "否");
            userERA.AutoRecyclType6 = userINI.Read("装备回收属性", "永恒剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType6)) userINI.Write("装备回收属性", "永恒剑甲", "否");
            userERA.AutoRecyclType7 = userINI.Read("装备回收属性", "毁灭首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType7)) userINI.Write("装备回收属性", "毁灭首饰", "否");
            userERA.AutoRecyclType8 = userINI.Read("装备回收属性", "毁灭剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType8)) userINI.Write("装备回收属性", "毁灭剑甲", "否");
            userERA.AutoRecyclType9 = userINI.Read("装备回收属性", "刑天首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType9)) userINI.Write("装备回收属性", "刑天首饰", "否");
            userERA.AutoRecyclType10 = userINI.Read("装备回收属性", "焚天首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType10)) userINI.Write("装备回收属性", "焚天首饰", "否");
            userERA.AutoRecyclType11 = userINI.Read("装备回收属性", "沧澜首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType11)) userINI.Write("装备回收属性", "沧澜首饰", "否");
            userERA.AutoRecyclType12 = userINI.Read("装备回收属性", "刑焚沧甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType12)) userINI.Write("装备回收属性", "刑焚沧甲", "否");
            userERA.AutoRecyclType13 = userINI.Read("装备回收属性", "刑焚沧剑");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType13)) userINI.Write("装备回收属性", "刑焚沧剑", "否");
            userERA.AutoRecyclType14 = userINI.Read("装备回收属性", "灭世首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType14)) userINI.Write("装备回收属性", "灭世首饰", "否");
            userERA.AutoRecyclType15 = userINI.Read("装备回收属性", "灭世剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType15)) userINI.Write("装备回收属性", "灭世剑甲", "否");
            userERA.AutoRecyclType16 = userINI.Read("装备回收属性", "轮回首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType16)) userINI.Write("装备回收属性", "轮回首饰", "否");
            userERA.AutoRecyclType17 = userINI.Read("装备回收属性", "轮回剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType17)) userINI.Write("装备回收属性", "轮回剑甲", "否");
            userERA.AutoRecyclType18 = userINI.Read("装备回收属性", "起源首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType18)) userINI.Write("装备回收属性", "起源首饰", "否");
            userERA.AutoRecyclType19 = userINI.Read("装备回收属性", "起源剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType19)) userINI.Write("装备回收属性", "起源剑甲", "否");
            userERA.AutoRecyclType20 = userINI.Read("装备回收属性", "回忆首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType20)) userINI.Write("装备回收属性", "回忆首饰", "否");
            userERA.AutoRecyclType21 = userINI.Read("装备回收属性", "回忆剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType21)) userINI.Write("装备回收属性", "回忆剑甲", "否");
            userERA.AutoRecyclType22 = userINI.Read("装备回收属性", "命运首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType22)) userINI.Write("装备回收属性", "命运首饰", "否");
            userERA.AutoRecyclType23 = userINI.Read("装备回收属性", "命运剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType23)) userINI.Write("装备回收属性", "命运剑甲", "否");
            userERA.AutoRecyclType24 = userINI.Read("装备回收属性", "边界首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType24)) userINI.Write("装备回收属性", "边界首饰", "否");
            userERA.AutoRecyclType25 = userINI.Read("装备回收属性", "边界剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType25)) userINI.Write("装备回收属性", "边界剑甲", "否");
            userERA.AutoRecyclType26 = userINI.Read("装备回收属性", "毒蛇首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType26)) userINI.Write("装备回收属性", "毒蛇首饰", "否");
            userERA.AutoRecyclType27 = userINI.Read("装备回收属性", "毒蛇剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType27)) userINI.Write("装备回收属性", "毒蛇剑甲", "否");
            userERA.AutoRecyclType28 = userINI.Read("装备回收属性", "沙漠首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType28)) userINI.Write("装备回收属性", "沙漠首饰", "否");
            userERA.AutoRecyclType29 = userINI.Read("装备回收属性", "沙漠剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType29)) userINI.Write("装备回收属性", "沙漠剑甲", "否");
            userERA.AutoRecyclType30 = userINI.Read("装备回收属性", "古城首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType30)) userINI.Write("装备回收属性", "古城首饰", "否");
            userERA.AutoRecyclType31 = userINI.Read("装备回收属性", "古城剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType31)) userINI.Write("装备回收属性", "古城剑甲", "否");
            userERA.AutoRecyclType32 = userINI.Read("装备回收属性", "孤岛首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType32)) userINI.Write("装备回收属性", "孤岛首饰", "否");
            userERA.AutoRecyclType33 = userINI.Read("装备回收属性", "孤岛剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType33)) userINI.Write("装备回收属性", "孤岛剑甲", "否");
            userERA.AutoRecyclType34 = userINI.Read("装备回收属性", "仙门首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType34)) userINI.Write("装备回收属性", "仙门首饰", "否");
            userERA.AutoRecyclType35 = userINI.Read("装备回收属性", "仙门剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType35)) userINI.Write("装备回收属性", "仙门剑甲", "否");
            userERA.AutoRecyclType36 = userINI.Read("装备回收属性", "战神首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType36)) userINI.Write("装备回收属性", "战神首饰", "否");
            userERA.AutoRecyclType37 = userINI.Read("装备回收属性", "战神剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType37)) userINI.Write("装备回收属性", "战神剑甲", "否");
            userERA.AutoRecyclType38 = userINI.Read("装备回收属性", "复古首饰");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType38)) userINI.Write("装备回收属性", "复古首饰", "否");
            userERA.AutoRecyclType39 = userINI.Read("装备回收属性", "复古剑甲");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType39)) userINI.Write("装备回收属性", "复古剑甲", "否");
            userERA.AutoRecyclType40 = userINI.Read("装备回收属性", "一级神技");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType40)) userINI.Write("装备回收属性", "一级神技", "否");
            userERA.AutoRecyclType41 = userINI.Read("装备回收属性", "二级神技");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType41)) userINI.Write("装备回收属性", "二级神技", "否");
            userERA.AutoRecyclType42 = userINI.Read("装备回收属性", "三级神技");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType42)) userINI.Write("装备回收属性", "三级神技", "否");
            userERA.AutoRecyclType43 = userINI.Read("装备回收属性", "四级神技");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType43)) userINI.Write("装备回收属性", "四级神技", "否");
            userERA.AutoRecyclType44 = userINI.Read("装备回收属性", "五级神技");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType44)) userINI.Write("装备回收属性", "五级神技", "否");
            userERA.AutoRecyclType45 = userINI.Read("装备回收属性", "六级神技");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType45)) userINI.Write("装备回收属性", "六级神技", "否");
            userERA.AutoRecyclType46 = userINI.Read("装备回收属性", "初级时装");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType46)) userINI.Write("装备回收属性", "初级时装", "否");
            userERA.AutoRecyclType47 = userINI.Read("装备回收属性", "复古时装");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType47)) userINI.Write("装备回收属性", "复古时装", "否");
            userERA.AutoRecyclType48 = userINI.Read("装备回收属性", "血牛时装");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType48)) userINI.Write("装备回收属性", "血牛时装", "否");
            userERA.AutoRecyclType49 = userINI.Read("装备回收属性", "特殊物品");
            if (string.IsNullOrEmpty(userERA.AutoRecyclType49)) userINI.Write("装备回收属性", "特殊物品", "否");
            return userERA;
        }

        /// <summary>
        /// 获取一个虚拟用户
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetVirtualInfoByQQ(string userQQ, string userName, int lv, int ac, int ac2, int agile)
        {
            try
            {
                int n = rnd.Next(1, 4);
                UserInfo user = new UserInfo
                {
                    Name = string.IsNullOrEmpty(userName) ? "咕咕鸡玩家" : userName,
                    Job = n == 1 ? "战士" : n == 2 ? "道士" : "法师",
                    Sex = rnd.Next(2) == 0 ? "男" : "女",
                    UserQQ = userQQ,
                    IsUseWeapon = true
                };
                user.User_General = new UserGeneral
                {
                    Lv = rnd.Next(1, lv),
                };
                user.User_Attribute = new UserAttribute();
                user.User_Attribute.Ac = rnd.Next(1, ac);
                user.User_Attribute.Ac2 = rnd.Next(user.User_Attribute.Ac, ac2);
                user.User_Attribute.Agile = rnd.Next(1, agile);

                string[] arrDRESS = { "1", "2", "3", "4", "5", "6", "7", "8" };
                string looks = arrDRESS[rnd.Next(arrDRESS.Length)];

                int[] arrWeapon = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 37, 107, 114, 115, 118, 119, 120, 164, 223, 256, 300, 352, 407, 413, 425, 1002, 1003, 1004, 1005, 1008 };
                string shapeCode = arrWeapon[rnd.Next(arrWeapon.Length)].ToString();
                user.User_Items = new UserItems
                {
                    DRESS = looks,
                    WEAPON = shapeCode,
                };
                Dictionary<int, string> dicUS = new Dictionary<int, string>();
                switch (user.Job)
                {
                    case "战士":
                        user.User_Attribute.Dc = rnd.Next(1, 5);
                        user.User_Attribute.DcMax = rnd.Next(1, 100);
                        dicUS.Add(3, "基本剑术");
                        dicUS.Add(7, "攻杀剑术");
                        dicUS.Add(12, "刺杀剑术");
                        dicUS.Add(25, "半月弯刀");
                        dicUS.Add(26, "烈火剑法");
                        dicUS.Add(27, "野蛮冲撞");
                        dicUS.Add(34, "护体神盾");
                        break;
                    case "道士":
                        user.User_Attribute.Sc = rnd.Next(1, 5);
                        user.User_Attribute.ScMax = rnd.Next(1, 100);
                        dicUS.Add(2, "治愈术");
                        dicUS.Add(4, "精神力战法");
                        dicUS.Add(6, "施毒术");
                        dicUS.Add(13, "灵魂火符");
                        dicUS.Add(14, "幽灵盾");
                        dicUS.Add(15, "神圣战甲术");
                        dicUS.Add(16, "困魔咒");
                        dicUS.Add(17, "召唤骷髅");
                        dicUS.Add(18, "隐身术");
                        dicUS.Add(19, "集体隐身术");
                        dicUS.Add(28, "心灵启示");
                        dicUS.Add(29, "群体治疗术");
                        dicUS.Add(30, "召唤神兽");
                        break;
                    case "法师":
                        user.User_Attribute.Mc = rnd.Next(1, 5);
                        user.User_Attribute.McMax = rnd.Next(1, 100);
                        dicUS.Add(1, "火球术");
                        dicUS.Add(5, "大火球");
                        dicUS.Add(8, "抗拒火环");
                        dicUS.Add(9, "地狱火");
                        dicUS.Add(10, "疾光电影");
                        dicUS.Add(11, "雷电术");
                        dicUS.Add(20, "诱惑之光");
                        dicUS.Add(21, "瞬息移动");
                        dicUS.Add(22, "火墙");
                        dicUS.Add(23, "爆裂火焰");
                        dicUS.Add(24, "地狱雷光");
                        dicUS.Add(31, "魔法盾");
                        dicUS.Add(32, "圣言术");
                        dicUS.Add(33, "冰咆哮");
                        break;
                }
                user.User_Attribute.Hp = rnd.Next(10, 500);
                user.User_Attribute.MaxHp = user.User_Attribute.Hp;
                UserSkill us;
                user.User_Skill = new List<UserSkill>();
                foreach (var item in dicUS)
                {
                    us = new UserSkill()
                    {
                        US_MagID = item.Key,
                        US_Name = item.Value,
                        US_Level = 3,
                        US_IsEnableSkill = "是"
                    };
                    user.User_Skill.Add(us);
                }
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pos"></param>
        public static void UseSkill(UserInfo user, int pos)
        {
            try
            {
                if (pos < 0) pos = 0;
                string uPath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(uPath);
                var usKeyList = userInI.GetAllKeys("个人技能");
                string store = userInI.Read("人物信息", "所在商店");
                int page = int.Parse(userInI.Read("人物信息", "所在页面"));
                if (store.Split('_')[1] != "个人技能") throw new Exception("你都没有这个技能,输入 查看技能 重新试试!");
                int total = usKeyList.Count;
                int pageSize = 6;
                int recordPage = (total + pageSize - 1) / pageSize;// 获取仓库页数
                if (page > recordPage) page = recordPage;// 如果跳转的夜数大于总页数就等于总页数
                usKeyList = usKeyList.Skip((page - 1) * pageSize).Take(pageSize).ToList();// 对仓库数据分页

                if (pos >= usKeyList.Count) throw new Exception("你都没有这个技能,输入 查看技能 重新试试!");

                string key = usKeyList[pos];
                string usk = userInI.Read("个人技能", key);
                UserSkill us = Api.JsonDeserialize<UserSkill>(usk);

                if (us.US_IsEnableSkill == "是")
                {
                    user.IsUseSkills = "否";
                    us.US_IsEnableSkill = "否";
                    UserSkill us1 = null;
                    usKeyList.ForEach(uk =>
                    {
                        usk = userInI.Read("个人技能", uk);
                        us1 = Api.JsonDeserialize<UserSkill>(usk);
                        if (us.US_MagID == us1.US_MagID)
                        {
                            us1 = us;
                        }

                        if (us1.US_IsEnableSkill == "是")
                            user.IsUseSkills = "是";
                    });

                }
                else
                {
                    us.US_IsEnableSkill = "是";
                    user.IsUseSkills = "是";
                }
                userInI.Write("人物信息", "是否使用技能攻击", user.IsUseSkills);
                userInI.Write("个人技能", key, Api.JsonSerialize<UserSkill>(us));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 卖肉
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<StditemDB> GetSellingMeat(UserInfo user)
        {
            try
            {
                if (user.IsOnCity != "是")
                {
                    throw new Exception("你都没有在城里,输入 回城 去城里卖肉吧!");
                }

                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper iniFileHelper = new IniFileHelper(path);
                List<string> list = iniFileHelper.GetAllKeys("背包物品");

                if (list.Count == 0)
                {
                    throw new Exception("噢,抱歉,你都没有一点肉,去打鹿 鸡 羊 试试吧!");
                }
                List<StditemDB> listResult = new List<StditemDB>();

                StditemDB stditemDB = null;
                list.ForEach(i =>
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("背包物品", i));
                    if (stditemDB.StdMode == 40)
                    {
                        listResult.Add(stditemDB);
                        user.User_General.JB += stditemDB.Price * stditemDB.SubAttr1;
                        user.User_General.Bgw -= stditemDB.Weight;
                    }
                });
                iniFileHelper.Write("普通数据", "金币", user.User_General.JB.ToString());
                iniFileHelper.Write("普通数据", "背包重量", user.User_General.Bgw.ToString());

                if (listResult.Count == 0)
                {
                    throw new Exception("噢,抱歉,你都没有一点肉,去打鹿 鸡 羊 试试吧!");
                }
                switch (user.Map)
                {
                    case "银杏山谷": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\银杏山谷\\1_0_1_4.jpg"; break;
                    case "边界村": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\边界村\\1_0_2_3.jpg"; break;
                    case "比奇城": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\0\\比奇城\\1_100_5.jpg"; break;
                    case "盟重土城": user.UserImagePath = $"{LegendFilePath.LegendIMGDataPath}地图\\4\\盟重土城\\4_1.jpg"; break;
                }
                return listResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 取下物品
        /// </summary>
        /// <param name="user"></param>
        public static bool TaskEquipment(UserInfo user, string msg, int x = 0, int y = 0, int w = 0, int h = 0, int lv = 0)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userINI = new IniFileHelper(path);
                string key = "", key2 = "";

                switch (msg)
                {
                    case "武器":
                        key = userINI.Read("装备物品", "武器");
                        userINI.Write("装备物品", "武器", "0");
                        break;
                    case "衣服":
                        key = userINI.Read("装备物品", "衣服");
                        userINI.Write("装备物品", "衣服", "0");
                        break;
                    case "勋章":
                        key = userINI.Read("装备物品", "勋章");
                        userINI.Write("装备物品", "勋章", "0");
                        break;
                    case "头盔":
                        key = userINI.Read("装备物品", "头盔");
                        userINI.Write("装备物品", "头盔", "0");
                        break;
                    case "项链":
                        key = userINI.Read("装备物品", "项链");
                        userINI.Write("装备物品", "项链", "0");
                        break;
                    case "右戒指":
                        key = userINI.Read("装备物品", "右戒指");
                        userINI.Write("装备物品", "右戒指", "0");
                        break;
                    case "左戒指":
                        key = userINI.Read("装备物品", "左戒指");
                        userINI.Write("装备物品", "左戒指", "0");
                        break;
                    case "戒指":
                        key = userINI.Read("装备物品", "右戒指");
                        userINI.Write("装备物品", "右戒指", "0");

                        key2 = userINI.Read("装备物品", "左戒指");
                        userINI.Write("装备物品", "左戒指", "0");
                        break;
                    case "右手镯":
                        key = userINI.Read("装备物品", "右手镯");
                        userINI.Write("装备物品", "右手镯", "0");
                        break;
                    case "左手镯":
                        key = userINI.Read("装备物品", "左手镯");
                        userINI.Write("装备物品", "左手镯", "0");
                        break;
                    case "手镯":
                        key = userINI.Read("装备物品", "右手镯");
                        userINI.Write("装备物品", "右手镯", "0");
                        key2 = userINI.Read("装备物品", "左手镯");
                        userINI.Write("装备物品", "左手镯", "0");
                        break;
                    case "护身符":
                        key = userINI.Read("装备物品", "护身符");
                        userINI.Write("装备物品", "护身符", "0");
                        break;
                    case "腰带":
                        key = userINI.Read("装备物品", "腰带");
                        userINI.Write("装备物品", "腰带", "0");
                        break;
                    case "鞋子":
                        key = userINI.Read("装备物品", "鞋子");
                        userINI.Write("装备物品", "鞋子", "0");
                        break;
                    case "宝石":
                        key = userINI.Read("装备物品", "宝石");
                        userINI.Write("装备物品", "宝石", "0");
                        break;
                    case "斗笠":
                        key = userINI.Read("装备物品", "斗笠");
                        userINI.Write("装备物品", "斗笠", "0");
                        break;
                    case "军鼓":
                        key = userINI.Read("装备物品", "军鼓");
                        userINI.Write("装备物品", "军鼓", "0");
                        break;
                    case "盾牌":
                        key = userINI.Read("装备物品", "盾牌");
                        userINI.Write("装备物品", "盾牌", "0");
                        break;
                    case "翅膀":
                        key = userINI.Read("装备物品", "翅膀");
                        userINI.Write("装备物品", "翅膀", "0");
                        break;
                }
                string stdStr = userINI.ReadValue("装备属性", key);
                string stdStr2 = userINI.ReadValue("装备属性", key2);
                if (string.IsNullOrEmpty(stdStr) && string.IsNullOrEmpty(stdStr2))
                {
                    throw new Exception("当前位置没有带装备,请输入 带装备1 进行操作.");
                }
                StditemDB db = Api.JsonDeserialize<StditemDB>(stdStr);
                if (db == null) db = new StditemDB();

                StditemDB db2 = Api.JsonDeserialize<StditemDB>(stdStr2);
                if (db2 == null) db2 = new StditemDB();

                user.User_General.Wear -= db.Weight + db2.Weight;
                user.User_General.Bgw += db.Weight + db2.Weight;
                userINI.Write("普通数据", "穿戴重量", user.User_General.Wear.ToString());
                userINI.Write("普通数据", "背包重量", user.User_General.Bgw.ToString());

                if (msg == "武器") user.User_Attribute.Wrist -= db.Weight;

                if (!string.IsNullOrEmpty(stdStr))
                {
                    userINI.Write("背包物品", key, Api.JsonSerialize(db));
                    userINI.Delete("装备属性", key);
                }

                if (!string.IsNullOrEmpty(stdStr2))
                {
                    userINI.Write("背包物品", key2, Api.JsonSerialize(db2));
                    userINI.Delete("装备属性", key2);
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 带装备,将背包里的数据生成到用户身上的数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pos">背包里物品位置</param>
        public static bool SetUseEquipment(UserInfo user, int pos)
        {
            try
            {
                // 查询出背包里所有的物品
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper iniFileHelper = new IniFileHelper(path);
                List<string> list = iniFileHelper.GetAllKeys("背包物品");
                if (pos > list.Count)
                {
                    throw new Exception("噢,抱歉,穿戴错误或该位置没有装备,请再试试吧!");
                }
                // 取对应物品
                string key = iniFileHelper.ReadValue("背包物品", list[pos - 1]);
                StditemDB stditemDB = Api.JsonDeserialize<StditemDB>(key);
                string newStdId = iniFileHelper.GetRndId();//生成新id
                int cdzl, zdcdzl, wrist;

                //验证装备能否佩戴,不能报异常
                CheckedZB(user, stditemDB, iniFileHelper, out cdzl, out zdcdzl, out wrist);

                string key1, key2;
                // 生成身上物品数据
                switch (stditemDB.StdMode)//装备类型
                {
                    case 5://武器(单手)
                    case 6://武器(双手)
                        key = iniFileHelper.Read("装备物品", "武器");
                        iniFileHelper.Write("装备物品", "武器", newStdId);
                        user.IsUseWeapon = true;
                        break;
                    case 7://补充物品(气血石,幻魔石,魔血石,千里传音等)
                        key = iniFileHelper.Read("装备物品", "补充物品");
                        iniFileHelper.Write("装备物品", "补充物品", newStdId);
                        break;
                    case 10://衣服(男)
                    case 11://衣服(女)
                        key = iniFileHelper.Read("装备物品", "衣服");
                        iniFileHelper.Write("装备物品", "衣服", newStdId);
                        break;
                    case 15://头盔
                        key = iniFileHelper.Read("装备物品", "头盔");
                        iniFileHelper.Write("装备物品", "头盔", newStdId);
                        break;
                    case 16://斗笠,面巾
                        key = iniFileHelper.Read("装备物品", "斗笠");
                        iniFileHelper.Write("装备物品", "斗笠", newStdId);
                        break;
                    case 19://项链(AC=魔法躲避,MAC=诅咒,MAC2=幸运)
                    case 20://项链(AC2=准确,MAC2=敏捷)
                    case 21://项链(AC=+速度,AC2=HP恢复,MAC=-速度,MAC2=MP恢复)
                        key = iniFileHelper.Read("装备物品", "项链");
                        iniFileHelper.Write("装备物品", "项链", newStdId);
                        break;
                    case 22://戒指
                    case 23://戒指(AC=速度+,AC2=毒物躲避,MAC=速度-,MAC2=中毒恢复)
                        {
                            key1 = iniFileHelper.Read("装备物品", "右戒指");
                            key2 = iniFileHelper.Read("装备物品", "左戒指");
                            if (key1 == "0" || string.IsNullOrEmpty(key1))
                            {
                                iniFileHelper.Write("装备物品", "右戒指", newStdId);
                                key = key1;
                            }
                            else
                            {
                                if (key2 == "0" || string.IsNullOrEmpty(key2))
                                {
                                    iniFileHelper.Write("装备物品", "左戒指", newStdId);
                                    key = key2;
                                }
                                else
                                {
                                    iniFileHelper.Write("装备物品", "右戒指", newStdId);
                                    key = key1;
                                }
                            }
                        }
                        break;
                    case 24://手镯(AC2=准确,MAC2=敏捷)
                    case 26://手镯
                        {
                            key1 = iniFileHelper.Read("装备物品", "右手镯");
                            key2 = iniFileHelper.Read("装备物品", "左手镯");
                            if (key1 == "0" || string.IsNullOrEmpty(key1))
                            {
                                iniFileHelper.Write("装备物品", "右手镯", newStdId);
                                key = key1;
                            }
                            else
                            {
                                if (key2 == "0" || string.IsNullOrEmpty(key2))
                                {
                                    iniFileHelper.Write("装备物品", "左手镯", newStdId);
                                    key = key2;
                                }
                                else
                                {
                                    iniFileHelper.Write("装备物品", "右手镯", newStdId);
                                    key = key1;
                                }
                            }
                        }
                        break;
                    case 30://勋章
                        key = iniFileHelper.Read("装备物品", "勋章");
                        iniFileHelper.Write("装备物品", "勋章", newStdId);
                        break;
                    case 51:
                    case 25://毒,符
                        key = iniFileHelper.Read("装备物品", "护身符");
                        iniFileHelper.Write("装备物品", "护身符", newStdId);
                        break;
                    case 27://腰带
                    case 64://腰带
                        key = iniFileHelper.Read("装备物品", "腰带");
                        iniFileHelper.Write("装备物品", "腰带", newStdId);
                        break;
                    case 28://鞋子(物品Aniount设置数字,表示可以增加负重)
                    case 62://鞋子(物品Aniount设置数字,表示可以增加负重)
                        key = iniFileHelper.Read("装备物品", "鞋子");
                        iniFileHelper.Write("装备物品", "鞋子", newStdId);
                        break;
                    case 63://宝石
                        key = iniFileHelper.Read("装备物品", "宝石");
                        iniFileHelper.Write("装备物品", "宝石", newStdId);
                        break;
                    case 81:
                        {
                            key1 = iniFileHelper.Read("装备物品", "时装右戒指");
                            key2 = iniFileHelper.Read("装备物品", "时装左戒指");
                            if (key1 == "0" || string.IsNullOrEmpty(key1))
                            {
                                iniFileHelper.Write("装备物品", "时装右戒指", newStdId);
                                key = key1;
                            }
                            else
                            {
                                if (key2 == "0" || string.IsNullOrEmpty(key2))
                                {
                                    iniFileHelper.Write("装备物品", "时装左戒指", newStdId);
                                    key = key2;
                                }
                                else
                                {
                                    iniFileHelper.Write("装备物品", "时装右戒指", newStdId);
                                    key = key1;
                                }
                            }
                        }
                        break;
                    case 29:
                    case 90:
                        key = iniFileHelper.Read("装备物品", "翅膀");
                        iniFileHelper.Write("装备物品", "翅膀", newStdId); break;
                    default: //不能佩戴的装备
                        throw new Exception("噢,抱歉,穿戴错误或该位置没有装备,请再试试吧!");
                }
                if (key.Length > 5)//将原来身上的物品返回到背包中
                {
                    StditemDB std = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", key));
                    if (std != null)
                    {
                        iniFileHelper.Write("背包物品", key, Api.JsonSerialize(std));
                        iniFileHelper.Delete("装备属性", key);
                    }
                }

                iniFileHelper.Write("装备属性", newStdId, Api.JsonSerialize<StditemDB>(stditemDB));//将身上的装备写入

                if (stditemDB.StdMode == 5 || stditemDB.StdMode == 6)//装备类型=武器就写入腕力
                {
                    user.User_Attribute.Wrist = wrist;
                    iniFileHelper.Write("属性点", "腕力", wrist.ToString());
                }
                user.User_General.Wear = cdzl;
                iniFileHelper.Write("普通数据", "穿戴重量", cdzl.ToString());

                // 去除背包内物品
                iniFileHelper.Delete("背包物品", list[pos - 1]);
                // 重新计算背包重量
                list.Remove(list[pos - 1]);
                zdcdzl = 0;
                foreach (var item in list)
                {
                    stditemDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("背包物品", item));
                    zdcdzl += stditemDB.Weight;
                }
                iniFileHelper.Write("普通数据", "背包重量", zdcdzl.ToString());

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 判断装备是否能佩戴,验证
        /// </summary>
        static void CheckedZB(UserInfo user, StditemDB stditemDB, IniFileHelper iniFileHelper, out int cdzl, out int zdcdzl, out int wrist)
        {
            string key1 = "", key2 = "-1";
            wrist = 0;
            cdzl = int.Parse(iniFileHelper.Read("普通数据", "穿戴重量"));
            zdcdzl = user.User_General.WearMax;
            int wlmax = int.Parse(iniFileHelper.Read("属性点", "最大腕力"));

            StditemDB stdDB = null;
            int lvv = 0;
            if (user.User_Items.DRESS.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.DRESS));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11 + stdDB.StdHiddAttr.Wear;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.WEAPON.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.WEAPON));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.HELMET.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.HELMET));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.NECKLACE.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.NECKLACE));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.RING_R.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.RING_R));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.RING_L.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.RING_L));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.ARMRING_R.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.ARMRING_R));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.ARMRING_L.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.ARMRING_L));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.BELT.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.BELT));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.BOOTS.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.BOOTS));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.CHARM.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.CHARM));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            if (user.User_Items.RIGHTHAND.Length > 1)
            {
                stdDB = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", user.User_Items.RIGHTHAND));
                if (stdDB.StdHiddAttr != null)
                {
                    zdcdzl += stdDB.StdHiddAttr.Wear + stdDB.SubAttr11;
                    wlmax += stdDB.StdHiddAttr.Wrist;
                    if (stdDB.StdHiddAttr.Lv > 0) lvv += stdDB.StdHiddAttr.Lv;
                }
            }
            // 生成身上物品数据
            switch (stditemDB.StdMode)//装备类型
            {
                case 5://武器(单手)
                case 6://武器(双手)
                    {
                        key1 = iniFileHelper.Read("装备物品", "武器");
                        if (stditemDB.Weight > wlmax)
                        {
                            throw new Exception("噢,大兄弟,腕力不够,升级再试试吧!");
                        }
                        wrist = stditemDB.Weight;
                    }
                    break;
                case 7://补充物品(气血石,幻魔石,魔血石,千里传音等)
                    key1 = iniFileHelper.Read("装备物品", "补充物品");
                    break;
                case 10://衣服(男)
                case 11: //衣服(女)
                    switch (user.Sex)
                    {
                        case "男":
                            if (stditemDB.StdMode == 11)
                            {
                                throw new Exception("对不起,你不能穿女性衣服,请换换别的衣服吧!");
                            }
                            break;
                        case "女":
                            if (stditemDB.StdMode == 10)
                            {
                                throw new Exception("对不起,你不能穿男性衣服,请换换别的衣服吧!");
                            }
                            break;
                    }
                    key1 = iniFileHelper.Read("装备物品", "衣服");
                    break;
                case 15://头盔 
                    key1 = iniFileHelper.Read("装备物品", "头盔");
                    break;
                case 16://斗笠,面巾
                    key1 = iniFileHelper.Read("装备物品", "斗笠");
                    break;
                case 19://项链(AC=魔法躲避,MAC=诅咒,MAC2=幸运)
                case 20://项链(AC2=准确,MAC2=敏捷)
                case 21://项链(AC=+速度,AC2=HP恢复,MAC=-速度,MAC2=MP恢复)
                    key1 = iniFileHelper.Read("装备物品", "项链");
                    break;
                case 22://戒指
                case 23://戒指(AC=速度+,AC2=毒物躲避,MAC=速度-,MAC2=中毒恢复)
                    key1 = iniFileHelper.Read("装备物品", "右戒指");
                    key2 = iniFileHelper.Read("装备物品", "左戒指");
                    break;
                case 24://手镯(AC2=准确,MAC2=敏捷)
                case 26://手镯
                    key1 = iniFileHelper.Read("装备物品", "右手镯");
                    key2 = iniFileHelper.Read("装备物品", "左手镯");
                    break;
                case 30://勋章
                    key1 = iniFileHelper.Read("装备物品", "勋章");
                    break;
                case 51:
                case 25://毒,符
                    key1 = iniFileHelper.Read("装备物品", "护身符");
                    break;
                case 27://腰带
                case 64://腰带
                    key1 = iniFileHelper.Read("装备物品", "腰带");
                    break;
                case 28://鞋子(物品Aniount设置数字,表示可以增加负重)
                case 62://鞋子(物品Aniount设置数字,表示可以增加负重)
                    key1 = iniFileHelper.Read("装备物品", "鞋子");
                    break;
                case 63://宝石
                    key1 = iniFileHelper.Read("装备物品", "宝石");
                    break;
                case 81:
                    key1 = iniFileHelper.Read("装备物品", "时装右戒指");
                    break;
                case 29:
                case 90: key1 = iniFileHelper.Read("装备物品", "翅膀"); break;
                default: //不能佩戴的装备
                    throw new Exception("噢,抱歉,穿戴错误或该位置没有装备,请再试试吧!");
            }

            switch (stditemDB.Need)
            {
                case 0: //0 为等级限制
                    if (user.User_General.Lv + lvv < stditemDB.Needlevel)
                    {
                        throw new Exception("噢,大兄弟,你等级不够,这件装备需要 " + stditemDB.Needlevel + " 级哦,先升级再试试吧!");
                    }
                    break;
                case 1://1 为攻击限制
                    if (user.User_Attribute.DcMax < stditemDB.Needlevel)
                    {
                        throw new Exception("噢,大兄弟,你攻击不够,这件装备需要 " + stditemDB.Needlevel + " 攻击,先戴更好的装备再试试吧!");
                    }
                    break;
                case 2: // 2 为魔法限制
                    if (user.User_Attribute.McMax < stditemDB.Needlevel)
                    {
                        throw new Exception("噢,大兄弟,你魔法不够,这件装备需要 " + stditemDB.Needlevel + " 魔法,先戴更好的装备再试试吧!");
                    }
                    break;
                case 3://3 为道术限制
                    if (user.User_Attribute.ScMax < stditemDB.Needlevel)
                    {
                        throw new Exception("噢,大兄弟,你道术不够,这件装备需要 " + stditemDB.Needlevel + " 道术,先戴更好的装备再试试吧!");
                    }
                    break;
            }

            StditemDB std = null;
            if (key1 == "0" || string.IsNullOrEmpty(key1))
            {
                cdzl += stditemDB.Weight;//加上装备重量
            }
            else
            {
                //判断key2 带戒指或者手镯的判断第二个有没有带
                if (key2 == "0" || string.IsNullOrEmpty(key2))
                {
                    cdzl += stditemDB.Weight;//加上装备重量                   
                }
                else
                {
                    //有戴武器,穿戴重量要减去戴的武器的重量然后再判断是否穿戴重量足够
                    std = Api.JsonDeserialize<StditemDB>(iniFileHelper.ReadValue("装备属性", key1));
                    if (stditemDB.StdMode != 5)
                    {
                        cdzl -= std.Weight;//减去穿的装备重量,比如原来12,武器8,12-8=4
                        cdzl = stditemDB.Weight;//加上装备重量 4+10=14
                    }
                }
            }
            if (cdzl > zdcdzl)// 如果 14<16就吧新装备带上,否则提示穿戴重量不够
            {
                throw new Exception("噢,大兄弟,穿戴重量不够,先升级再试试吧!");
            }

        }

        /// <summary>
        /// 设置管理员
        /// </summary>
        public static void SetAdmin(string FromQQ, string adminQQ, bool isSet)
        {
            try
            {
                IniFileHelper iniFileHelper = new IniFileHelper(LegendFilePath.ConfigString);
                string sa = iniFileHelper.Read("签到配置", "超级管理员");
                if (!sa.Contains(FromQQ))
                {
                    throw new Exception("您不是超级管理员,只有超级管理员才可以添加管理员.");
                }

                string admin = iniFileHelper.Read("签到配置", "管理员");

                if (isSet)
                {
                    if (admin.Contains(adminQQ))
                    {
                        throw new Exception("设置失败!当前管理员已添加.");
                    }
                    admin += "|" + adminQQ;
                }
                else
                {
                    admin = admin.Replace("|" + adminQQ, "");
                }

                iniFileHelper.Write("签到配置", "管理员", admin);
                iniFileHelper.Write("签到配置", "签到元宝数", "100");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设置签到元宝数
        /// </summary>
        public static void SetYbCount(string qq, int replyRate)
        {
            try
            {
                //List<string> list = FileHelper.ReadiniReturnList(LegendFilePath.ConfigString);
                //string ybName = "";
                //for (int i = 0; i < list.Count; i++)
                //{
                //    ybName = list[i].Split('=')[0];

                //    switch (ybName)
                //    {
                //        case "签到元宝数": ybName = "签到元宝数=" + replyRate; list[i] = ybName; break;
                //        case "管理员":
                //            if (!list[i].Split('=')[1].Contains(qq))
                //            {
                //                throw new Exception("对不起,当前QQ不是管理员,请联系超级管理员添加.");
                //            }
                //            break;
                //    }
                //}


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 用户签到
        /// </summary>
        public static bool SignIn(string qq, ref UserInfo user)
        {
            try
            {
                
                string path = LegendFilePath.SignInPath + DateTime.Now.ToString("yyyyMMdd") + "_file.ini";
                string directoryName = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
               
                IniFileHelper iniFileHelper = new IniFileHelper(path);
                string q = iniFileHelper.Read("用户签到列表", qq);
                
                if (!string.IsNullOrEmpty(q))
                {
                    return false;
                }

                iniFileHelper.Write("用户签到列表", qq, "成功签到");

                #region 给用户加元宝
                string newPath = LegendFilePath.UserPath + qq + ".ini";
                 
                if (!File.Exists(newPath))
                {
                    iniFileHelper = new IniFileHelper(newPath);
                    iniFileHelper.Write("人物信息", "序号", user.Id.ToString());
                    iniFileHelper.Write("人物信息", "角色名称", user.Name);
                    iniFileHelper.Write("人物信息", "角色类型", user.RoleType);
                    iniFileHelper.Write("人物信息", "性别", user.Sex);
                    iniFileHelper.Write("人物信息", "职业", user.Job);
                    iniFileHelper.Write("人物信息", "所在地图", user.Map);
                    iniFileHelper.Write("人物信息", "坐标", user.Pos);
                    iniFileHelper.Write("人物信息", "帐号", user.UserQQ);
                    iniFileHelper.Write("人物信息", "是否在城里", user.IsOnCity);

                    iniFileHelper.Write("普通数据", "等级", user.User_General.Lv.ToString());
                    iniFileHelper.Write("普通数据", "元宝", user.User_General.YB.ToString());
                    iniFileHelper.Write("普通数据", "签到元宝数", user.User_General.CurtYB.ToString());
                    iniFileHelper.Write("普通数据", "金币", user.User_General.JB.ToString());
                    iniFileHelper.Write("普通数据", "声望", user.User_General.REP.ToString());
                    iniFileHelper.Write("普通数据", "金刚石", user.User_General.JGS.ToString());
                    iniFileHelper.Write("普通数据", "当前经验", user.User_General.Exp.ToString());
                    iniFileHelper.Write("普通数据", "总经验", user.User_General.SumExp.ToString());
                    iniFileHelper.Write("普通数据", "下一级经验", user.User_General.NextExp.ToString());
                    iniFileHelper.Write("普通数据", "背包重量", user.User_General.Bgw.ToString());
                    iniFileHelper.Write("普通数据", "最大背包重量", user.User_General.BgwMax.ToString());
                    iniFileHelper.Write("普通数据", "PK点数", user.User_General.PkPoint.ToString());
                    iniFileHelper.Write("普通数据", "穿戴重量", user.User_General.Wear.ToString());
                    iniFileHelper.Write("普通数据", "最大穿戴重量", user.User_General.WearMax.ToString());

                    iniFileHelper.Write("属性点", "Hp", user.User_Attribute.Hp.ToString());
                    iniFileHelper.Write("属性点", "Mp", user.User_Attribute.Mp.ToString());
                    iniFileHelper.Write("属性点", "MaxHp", user.User_Attribute.MaxHp.ToString());
                    iniFileHelper.Write("属性点", "MaxMp", user.User_Attribute.MaxMp.ToString());
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
                    iniFileHelper.Write("属性点", "腕力", user.User_Attribute.Wrist.ToString());
                    iniFileHelper.Write("属性点", "最大腕力", user.User_Attribute.WristMax.ToString());
                    iniFileHelper.Write("属性点", "准确", user.User_Attribute.Acc.ToString());
                    iniFileHelper.Write("属性点", "敏捷", user.User_Attribute.Agile.ToString());

                    iniFileHelper.Write("装备物品", "衣服", user.User_Items.DRESS);
                    iniFileHelper.Write("装备物品", "武器", user.User_Items.WEAPON);
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
                }
                else
                {
                    
                    iniFileHelper = new IniFileHelper(newPath);
                   
                    user = GetInfoByQQ(user.UserQQ);
                    
                }

                #endregion
               
                IniFileHelper config = new IniFileHelper(LegendFilePath.ConfigString);
                int ybRate = int.Parse(config.Read("配置信息", "签到元宝数"));
                int expRate = int.Parse(config.Read("配置信息", "签到经验数"));
                ybRate = rnd.Next(1, ybRate + 1);
                expRate = rnd.Next(60, expRate + 1);

                int.TryParse(config.Read("配置信息", "签到获得的活跃值"), out int singQty);
                user.User_General.ActivityQty += singQty;
                user.User_General.ActivityTime = DateTime.Now;

                MonsterApi.SetUserUpLevel(user, iniFileHelper, ybRate, expRate);

               
                #region 获取连续签到和累计签到天数
                var files = Directory.GetFiles(LegendFilePath.SignInPath, "*.ini");
                singQty = 0;
                ybRate = 0;
                foreach (var file in files)
                {
                    iniFileHelper = new IniFileHelper(file);
                    q = iniFileHelper.Read("用户签到列表", qq);
                    if (!string.IsNullOrEmpty(q))
                    {
                        singQty++;// 总签到天数
                        ybRate++;
                    }
                    else
                    {
                        ybRate = 0;
                    }
                }
                user.User_General.SignDay = ybRate;
                user.User_General.SignDays = singQty;
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 用户补签
        /// </summary>
        public static string UserCountersignature(UserInfo user)
        {
            // 获取所有签到文件
            var files = Directory.GetFiles(LegendFilePath.SignInPath, "*.ini");

            List<string> list = new List<string>();
            foreach (var file in files)
            {
                IniFileHelper config = new IniFileHelper(file);
                string q = config.Read("用户签到列表", user.UserQQ);
                if (q != "成功签到")
                {
                    list.Add(file);
                }
            }
            int page = rnd.Next(1, 4);
            list = UserKey.GetPage(list, 1, page);
            foreach (var file in list)
            {
                IniFileHelper config = new IniFileHelper(file);
                config.Write("用户签到列表", user.UserQQ, "成功签到");
            }

            string res = $"玩家[{user.Name}]补签成功！";
            switch (list.Count)
            {
                case 1: res += "当前成功补签1天！"; break;
                case 2: res += "当前成功补签2天！"; break;
                case 3: res += "当前成功补签3天！"; break;
                default: break;
            }

            string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
            IniFileHelper userINI = new IniFileHelper(path);
            userINI.Write("人物信息", "补签时间", DateTime.Now.ToString());

            return res;
        }

        /// <summary>
        /// 设置自动吃药
        /// </summary>
        public static void SetAutoMedication(UserInfo user, string type, int num)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userINI = new IniFileHelper(path);
                user.UserSetAutoMedicationQTY = num;
                user.UserSetAutoMedicationQTYType = type;

                userINI.Write("人物信息", "自动吃药数额", user.UserSetAutoMedicationQTY.ToString());
                userINI.Write("人物信息", "自动吃药类型", user.UserSetAutoMedicationQTYType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 用户交易物品
        /// </summary>
        public static bool UserDeal(UserInfo user, string toQQ, int num, ref string refMsg)
        {
            try
            {
                string path = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userINI = new IniFileHelper(path);
                var list = userINI.GetAllKeys("背包物品");
                if (list.Count <= num) throw new Exception("对不起,物品未找到！");
                string key = list[num];
                string keyStr = userINI.ReadValue("背包物品", key);
                StditemDB std = Api.JsonDeserialize<StditemDB>(keyStr);

                path = LegendFilePath.UserPath + toQQ + ".ini";
                IniFileHelper user2INI = new IniFileHelper(path);
                var list2 = userINI.GetAllKeys("背包物品");
                if (list2.Count >= 96) throw new Exception("对不起,对方背包格子空间不够,叫他卖掉一些物品吧!");
                user2INI.Write("背包物品", key, keyStr);
                userINI.Delete("背包物品", key);

                refMsg = $"[@{toQQ}] 物品[{std.StdName}] 交易成功,请发送 查看背包 进行查看.";
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
