using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace 传奇
{
    /// <summary>
    /// 传奇用户属性
    /// </summary>
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserImagePath { get; set; }
        public string UserQQ { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Job { get; set; }
        public string RoleType { get; set; }
        /// <summary>
        /// 所在地图
        /// </summary>
        public string Map { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public string Pos { get; set; }
        /// <summary>
        /// 所在地图编号
        /// </summary>
        public string MapCode { get; set; }
        /// <summary>
        /// 是否在城里
        /// </summary>
        public string IsOnCity { get; set; }
        /// <summary>
        /// 是否开启永恒之力
        /// </summary>
        public string IsOnYHZL { get; set; }
        public UserGeneral User_General { get; set; }
        /// <summary>
        /// 是否开启称号
        /// </summary>
        public string IsOnTitle { get; set; }
        /// <summary>
        /// 个人称号属性
        /// </summary>
        public List<UserTitleAttribute> User_TitleAttribute { get; set; }
        /// <summary>
        /// 用户基本属性
        /// </summary>
        public UserAttribute User_Attribute { get; set; }
        /// <summary>
        /// VIP属性
        /// </summary>
        public UserVIPAttribute User_VIPAttribute { get; set; }
        /// <summary>
        /// 用户转生属性
        /// </summary>
        public UserRenewLevelAttribute User_RenewLevelAttribute { get; set; }
        /// <summary>
        /// 礼物属性
        /// </summary>
        public UserGiftAttribute User_GiftAttribute { get; set; }
        /// <summary>
        /// 永恒之力属性
        /// </summary>
        public YHZLAttribute User_YHZLAttribute { get; set; }
        public UserItems User_Items { get; set; }
        public List<UserSkill> User_Skill { get; set; }
        /// <summary>
        /// 装备回收属性
        /// </summary>
        public UserEquRecycleAttr User_EquRecycleAttr { get; set; }
        /// <summary>
        /// 人物面对的位置,关系到刷怪和PK对面怪物和人站什么地方朝向
        /// </summary>
        public int PosIndex { get; set; } = 0;
        
        /// <summary>
        /// 战斗记录
        /// </summary>
        public List<UserAttackRecord> UserAttackRecordList { get; set; }
        /// <summary>
        /// 打死的怪物
        /// </summary>
        public MonsterDB KillMon { get; set; }
        /// <summary>
        /// 打死的怪物列表
        /// </summary>
        public List<MonsterDB> KillMons { get; set; }
        /// <summary>
        /// 背包格子
        /// </summary>
        public int PageQty { get; set; } = 0;
        /// <summary>
        /// 是否打开发送图片,只控制gif动图
        /// </summary>
        public string IsSendMsgImg { get; set; }
        /// <summary>
        /// 7级提醒的消息发送
        /// </summary>
        public string IsSendMsgByLv7 { get; set; }
        public string IsSendMsgByLv19 { get; set; }
        public string IsSendMsgByLv25 { get; set; }
        public string IsSendMsgByLv28 { get; set; }
        public string IsSendMsgByLv30 { get; set; }


        public string IsSendMsgByLv12 { get; set; }
        public string IsSendMsgByLv13 { get; set; }
        public string IsSendMsgByLv16 { get; set; }
        public string IsSendMsgByLv17 { get; set; }
        public string IsSendMsgByLv20 { get; set; }
        public string IsSendMsgByLv22 { get; set; }
        public string IsSendMsgByLv24 { get; set; }
        public string IsSendMsgByLv26 { get; set; }
        public string IsSendMsgByLv31 { get; set; }
        public string IsSendMsgByLv32 { get; set; }
        public string IsSendMsgByLv9 { get; set; }


        public string IsSendMsgByLv14 { get; set; }
        public string IsSendMsgByLv18 { get; set; }
        public string IsSendMsgByLv21 { get; set; }
        public string IsSendMsgByLv33 { get; set; }
        public string IsSendMsgByLv35 { get; set; }

        /// <summary>
        /// 是否使用技能攻击
        /// </summary>
        public string IsUseSkills { get; set; } = "否";
        /// <summary>
        /// 是否使用武器
        /// </summary>
        public bool IsUseWeapon { get; set; }
        /// <summary>
        /// 城中杀怪数量
        /// </summary>
        public int KillCityMonNum { get; set; } = 0;
        /// <summary>
        /// 杀怪数量
        /// </summary>
        public int KillMonNum { get; set; } = 0;
        /// <summary>
        /// 自动吃药数量
        /// </summary>
        public int UserSetAutoMedicationQTY { get; set; } = 0;
        /// <summary>
        /// 自动吃药类型
        /// </summary>
        public string UserSetAutoMedicationQTYType { get; set; }
        /// <summary>
        /// 补签时间
        /// </summary>
        public DateTime UserCountersignatureTime { get; set; }
    }

    public class UserGeneral
    {
        /// <summary>
        /// 人物活跃值
        /// </summary>
        public int ActivityQty { get; set; } = 0;
        /// <summary>
        /// 活跃时间
        /// </summary>
        public DateTime ActivityTime { get; set; }
        /// <summary>
        /// 声望
        /// </summary>
        public int REP { get; set; } = 0;
        /// <summary>
        /// 等级
        /// </summary>
        public int Lv { get; set; } = 0;
        /// <summary>
        /// VIP用户等级
        /// </summary>
        public int VipLv { get; set; } = 0;
        /// <summary>
        ///  用户充值金额
        /// </summary>
        public int RegAmt { get; set; } = 0;
        /// <summary>
        /// 当前获得的元宝
        /// </summary>
        public int CurtYB { get; set; } = 0;
        /// <summary>
        /// 元宝
        /// </summary>
        public long YB { get; set; } = 0;
        /// <summary>
        /// 金币
        /// </summary>
        public long JB { get; set; } = 0;
        /// <summary>
        /// 金刚石
        /// </summary>
        public int JGS { get; set; } = 0;
        /// <summary>
        /// 当前经验
        /// </summary>
        public int Exp { get; set; } = 0;
        /// <summary>
        /// 总经验
        /// </summary>
        public long SumExp { get; set; } = 0;
        /// <summary>
        /// 下一级经验
        /// </summary>
        public long NextExp { get; set; } = 0;

        /// <summary>
        /// 背包重量
        /// </summary>
        public int Bgw { get; set; } = 0;
        /// <summary>
        /// 最大背包重量
        /// </summary>
        public int BgwMax { get; set; } = 0;
        /// <summary>
        /// 穿戴重量
        /// </summary>
        public int Wear { get; set; } = 0;
        /// <summary>
        /// 最大穿戴重量
        /// </summary>
        public int WearMax { get; set; } = 0;
        /// <summary>
        /// PK点数
        /// </summary>
        public int PkPoint { get; set; } = 0;
        /// <summary>
        /// pk消息
        /// </summary>
        public string PKMsg { get; set; }
        /// <summary>
        /// pk消息的攻击力
        /// </summary>
        public int PKdc { get; set; }
        /// <summary>
        /// pk输
        /// </summary>
        public int PKLose { get; set; }
        /// <summary>
        /// pk赢
        /// </summary>
        public int PKWin { get; set; }
        /// <summary>
        /// 排位pk输
        /// </summary>
        public int PKPwLose { get; set; }
        /// <summary>
        /// 排位pk赢
        /// </summary>
        public int PKPwWin { get; set; }
        /// <summary>
        /// 连续签到天数
        /// </summary>
        public int SignDay { get; set; }
        /// <summary>
        /// 累计签到天数
        /// </summary>
        public int SignDays { get; set; }

    }
    /// <summary>
    /// 永恒转生属性
    /// </summary>
    public class UserRenewLevelAttribute
    {
        /// <summary>
        /// 转生等级
        /// </summary>
        public int Level { get; set; } = 0;

        /// <summary>
        /// 血量
        /// </summary>
        public int Hp { get; set; } = 0;
        /// <summary>
        /// 蓝量
        /// </summary>
        public int Mp { get; set; } = 0;
        /// <summary>
        /// 防御下限
        /// </summary>
        public int Ac { get; set; } = 0;
        /// <summary>
        /// 防御上限
        /// </summary>
        public int Ac2 { get; set; } = 0;
        /// <summary>
        /// 魔法防御下限
        /// </summary>
        public int Mac { get; set; } = 0;
        /// <summary>
        /// 魔法防御上限
        /// </summary>
        public int Mac2 { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int Dc { get; set; } = 0;
        /// <summary>
        /// 最大物理攻击力
        /// </summary>
        public int DcMax { get; set; } = 0;
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Mc { get; set; } = 0;
        /// <summary>
        /// 最大魔法攻击力
        /// </summary>
        public int McMax { get; set; } = 0;
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int Sc { get; set; } = 0;
        /// <summary>
        /// 最大道术攻击力
        /// </summary>
        public int ScMax { get; set; } = 0;
        /// <summary>
        /// 准确
        /// </summary>
        public int Acc { get; set; } = 0;
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; set; } = 0;

        /// <summary>
        /// 暴击
        /// </summary>
        public int Heals { get; set; } = 0;
        /// <summary>
        /// 幸运
        /// </summary>
        public int Lucky { get; set; } = 0;
        /// <summary>
        /// 倍功
        /// </summary>
        public int DouBPower { get; set; } = 0;
        /// <summary>
        /// 魔法躲避
        /// </summary>
        public int MagicEvas { get; set; } = 0;
        /// <summary>
        /// 攻击速度
        /// </summary>
        public int AttackSpeed { get; set; } = 0;
        /// <summary>
        /// 移动速度
        /// </summary>
        public int MoveSpeed { get; set; } = 0;
        /// <summary>
        /// 物伤减免
        /// </summary>
        public int PhysicalDamageReduc { get; set; } = 0;
        /// <summary>
        /// 魔伤减免
        /// </summary>
        public int MagicDamageReduc { get; set; } = 0;
        /// <summary>
        /// 物伤反射
        /// </summary>
        public int PhysicalDamageReflex { get; set; } = 0;
        /// <summary>
        /// 魔伤反射
        /// </summary>
        public int MagicDamageReflex { get; set; } = 0;
        /// <summary>
        /// 暴击几率
        /// </summary>
        public int HealsPR { get; set; } = 0;
        /// <summary>
        /// 攻击伤害
        /// </summary>
        public int AttackPR { get; set; } = 0;
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int MagicAC { get; set; } = 0;
        /// <summary>
        /// 忽视防御
        /// </summary>
        public int IgnoreAC { get; set; } = 0;
        /// <summary>
        /// 伤害反弹
        /// </summary>
        public int DamageRebound { get; set; } = 0;
        /// <summary>
        /// 人物爆率
        /// </summary>
        public int UserExpRate { get; set; } = 0;
        /// <summary>
        /// 体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
        /// <summary>
        /// 防爆出率
        /// </summary>
        public int EquBurstRate { get; set; } = 0;
        /// <summary>
        /// 致命伤害
        /// </summary>
        public int FatalInjury { get; set; } = 0;
        /// <summary>
        /// 致命一击
        /// </summary>
        public int FatalBlow { get; set; } = 0;
        /// <summary>
        /// 卓越一击
        /// </summary>
        public int ExcellentBlow { get; set; } = 0;
        /// <summary>
        /// 会心一击
        /// </summary>
        public int HeartfeltBlow { get; set; } = 0;

    }

    public class UserTitleAttribute
    {
        /// <summary>
        /// 称号等级
        /// </summary>
        public int Level { get; set; } = 0;
        /// <summary>
        /// 称号类型
        /// </summary>
        public int LevelType { get; set; } = 0;
        /// <summary>
        /// 怪物死亡次数(鞭尸)
        /// </summary>
        public int MonDeadCount { get; set; } = 0;
        public string TitleName { get; set; }
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp { get; set; } = 0;
        /// <summary>
        /// 蓝量
        /// </summary>
        public int Mp { get; set; } = 0;
        /// <summary>
        /// 防御下限
        /// </summary>
        public int Ac { get; set; } = 0;
        /// <summary>
        /// 防御上限
        /// </summary>
        public int Ac2 { get; set; } = 0;
        /// <summary>
        /// 魔法防御下限
        /// </summary>
        public int Mac { get; set; } = 0;
        /// <summary>
        /// 魔法防御上限
        /// </summary>
        public int Mac2 { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int Dc { get; set; } = 0;
        /// <summary>
        /// 最大物理攻击力
        /// </summary>
        public int DcMax { get; set; } = 0;
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Mc { get; set; } = 0;
        /// <summary>
        /// 最大魔法攻击力
        /// </summary>
        public int McMax { get; set; } = 0;
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int Sc { get; set; } = 0;
        /// <summary>
        /// 最大道术攻击力
        /// </summary>
        public int ScMax { get; set; } = 0;
        /// <summary>
        /// 准确
        /// </summary>
        public int Acc { get; set; } = 0;
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; set; } = 0;

        /// <summary>
        /// 暴击
        /// </summary>
        public int Heals { get; set; } = 0;
        /// <summary>
        /// 幸运
        /// </summary>
        public int Lucky { get; set; } = 0;
        /// <summary>
        /// 倍功
        /// </summary>
        public int DouBPower { get; set; } = 0;
        /// <summary>
        /// 魔法躲避
        /// </summary>
        public int MagicEvas { get; set; } = 0;
        /// <summary>
        /// 攻击速度
        /// </summary>
        public int AttackSpeed { get; set; } = 0;
        /// <summary>
        /// 移动速度
        /// </summary>
        public int MoveSpeed { get; set; } = 0;
        /// <summary>
        /// 物伤减免
        /// </summary>
        public int PhysicalDamageReduc { get; set; } = 0;
        /// <summary>
        /// 魔伤减免
        /// </summary>
        public int MagicDamageReduc { get; set; } = 0;
        /// <summary>
        /// 物伤反射
        /// </summary>
        public int PhysicalDamageReflex { get; set; } = 0;
        /// <summary>
        /// 魔伤反射
        /// </summary>
        public int MagicDamageReflex { get; set; } = 0;
        /// <summary>
        /// 暴击几率
        /// </summary>
        public int HealsPR { get; set; } = 0;
        /// <summary>
        /// 攻击伤害
        /// </summary>
        public int AttackPR { get; set; } = 0;
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int MagicAC { get; set; } = 0;
        /// <summary>
        /// 忽视防御
        /// </summary>
        public int IgnoreAC { get; set; } = 0;
        /// <summary>
        /// 伤害反弹
        /// </summary>
        public int DamageRebound { get; set; } = 0;
        /// <summary>
        /// 人物爆率
        /// </summary>
        public int UserExpRate { get; set; } = 0;
        /// <summary>
        /// 体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
        /// <summary>
        /// 防爆出率
        /// </summary>
        public int EquBurstRate { get; set; } = 0;
        /// <summary>
        /// 致命伤害
        /// </summary>
        public int FatalInjury { get; set; } = 0;
        /// <summary>
        /// 致命一击
        /// </summary>
        public int FatalBlow { get; set; } = 0;
        /// <summary>
        /// 卓越一击
        /// </summary>
        public int ExcellentBlow { get; set; } = 0;
        /// <summary>
        /// 会心一击
        /// </summary>
        public int HeartfeltBlow { get; set; } = 0;

        /// <summary>
        /// 物品来源
        /// </summary>
        public string StdItemStuff { get; set; }
        /// <summary>
        /// 物品所属
        /// </summary>
        public string StdItemOwner { get; set; }
        /// <summary>
        /// 物品掉落地图
        /// </summary>
        public string StdItemMap { get; set; }
        /// <summary>
        /// 掉落时间
        /// </summary>
        public string StdItemTime { get; set; }
    }
    public class UserAttribute
    {
        /// <summary>
        /// 血量
        /// </summary>
        public long Hp { get; set; } = 0;
        public long MaxHp { get; set; } = 0;
        /// <summary>
        /// 蓝量
        /// </summary>
        public long Mp { get; set; } = 0;
        public long MaxMp { get; set; } = 0;
        /// <summary>
        /// 防御下限
        /// </summary>
        public int Ac { get; set; } = 0;
        /// <summary>
        /// 防御上限
        /// </summary>
        public int Ac2 { get; set; } = 0;
        /// <summary>
        /// 魔法防御下限
        /// </summary>
        public int Mac { get; set; } = 0;
        /// <summary>
        /// 魔法防御上限
        /// </summary>
        public int Mac2 { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int Dc { get; set; } = 0;
        /// <summary>
        /// 最大物理攻击力
        /// </summary>
        public int DcMax { get; set; } = 0;
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Mc { get; set; } = 0;
        /// <summary>
        /// 最大魔法攻击力
        /// </summary>
        public int McMax { get; set; } = 0;
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int Sc { get; set; } = 0;
        /// <summary>
        /// 最大道术攻击力
        /// </summary>
        public int ScMax { get; set; } = 0;
        /// <summary>
        /// 腕力
        /// </summary>
        public int Wrist { get; set; } = 0;
        /// <summary>
        /// 最大腕力
        /// </summary>
        public int WristMax { get; set; } = 0;
        /// <summary>
        /// 准确
        /// </summary>
        public int Acc { get; set; } = 0;
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; set; } = 0;

        /// <summary>
        /// 暴击
        /// </summary>
        public int Heals { get; set; } = 0;
        /// <summary>
        /// 幸运
        /// </summary>
        public int Lucky { get; set; } = 0;
        /// <summary>
        /// 倍功
        /// </summary>
        public int DouBPower { get; set; } = 0;
        /// <summary>
        /// 魔法躲避
        /// </summary>
        public int MagicEvas { get; set; } = 0;
        /// <summary>
        /// 攻击速度
        /// </summary>
        public int AttackSpeed { get; set; } = 0;
        /// <summary>
        /// 移动速度
        /// </summary>
        public int MoveSpeed { get; set; } = 0;
        /// <summary>
        /// 物伤减免
        /// </summary>
        public int PhysicalDamageReduc { get; set; } = 0;
        /// <summary>
        /// 魔伤减免
        /// </summary>
        public int MagicDamageReduc { get; set; } = 0;
        /// <summary>
        /// 物伤反射
        /// </summary>
        public int PhysicalDamageReflex { get; set; } = 0;
        /// <summary>
        /// 魔伤反射
        /// </summary>
        public int MagicDamageReflex { get; set; } = 0;
        /// <summary>
        /// 暴击几率
        /// </summary>
        public int HealsPR { get; set; } = 0;
        /// <summary>
        /// 攻击伤害
        /// </summary>
        public int AttackPR { get; set; } = 0;
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int MagicAC { get; set; } = 0;
        /// <summary>
        /// 忽视防御
        /// </summary>
        public int IgnoreAC { get; set; } = 0;
        /// <summary>
        /// 伤害反弹
        /// </summary>
        public int DamageRebound { get; set; } = 0;
        /// <summary>
        /// 人物爆率
        /// </summary>
        public int UserExpRate { get; set; } = 0;
        /// <summary>
        /// 体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
        /// <summary>
        /// 防爆出率
        /// </summary>
        public int EquBurstRate { get; set; } = 0;
        /// <summary>
        /// 致命伤害
        /// </summary>
        public int FatalInjury { get; set; } = 0;
        /// <summary>
        /// 致命一击
        /// </summary>
        public int FatalBlow { get; set; } = 0;
        /// <summary>
        /// 卓越一击
        /// </summary>
        public int ExcellentBlow { get; set; } = 0;
        /// <summary>
        /// 会心一击
        /// </summary>
        public int HeartfeltBlow { get; set; } = 0;
    }
    
    public class UserGiftAttribute
    {
        /// <summary>
        /// 怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
    }
    public class UserVIPAttribute
    {
        /// <summary>
        /// 回收元宝倍率
        /// </summary>
        public int RecycleYBQty { get; set; }
        /// <summary>
        /// 是否回收元宝
        /// </summary>
        public string IsRecycleYB { get; set; }
        /// <summary>
        /// 自动挂机
        /// </summary>
        public string AutoAFK { get; set; }
        /// <summary>
        /// 自动回收
        /// </summary>
        public string AutoRecycl { get; set; }
        /// <summary>
        /// 专属Boss数量
        /// </summary>
        public int AutoBoos { get; set; }
        /// <summary>
        /// 专属Boss最后击杀时间
        /// </summary>
        public string AutoBoosTime { get; set; }
        /// <summary>
        /// 已击杀专属BOOS数量
        /// </summary>
        public int AutoPBoos { get; set; }
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp { get; set; } = 0;
        /// <summary>
        /// 蓝量
        /// </summary>
        public int Mp { get; set; } = 0;
        /// <summary>
        /// 防御下限
        /// </summary>
        public int Ac { get; set; } = 0;
        /// <summary>
        /// 防御上限
        /// </summary>
        public int Ac2 { get; set; } = 0;
        /// <summary>
        /// 魔法防御下限
        /// </summary>
        public int Mac { get; set; } = 0;
        /// <summary>
        /// 魔法防御上限
        /// </summary>
        public int Mac2 { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int Dc { get; set; } = 0;
        /// <summary>
        /// 最大物理攻击力
        /// </summary>
        public int DcMax { get; set; } = 0;
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Mc { get; set; } = 0;
        /// <summary>
        /// 最大魔法攻击力
        /// </summary>
        public int McMax { get; set; } = 0;
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int Sc { get; set; } = 0;
        /// <summary>
        /// 最大道术攻击力
        /// </summary>
        public int ScMax { get; set; } = 0;
        /// <summary>
        /// 准确
        /// </summary>
        public int Acc { get; set; } = 0;
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; set; } = 0;
        /// <summary>
        /// 暴击
        /// </summary>
        public int Heals { get; set; } = 0;
        /// <summary>
        /// 倍功
        /// </summary>
        public int DouBPower { get; set; } = 0;
        /// <summary>
        /// 魔法躲避
        /// </summary>
        public int MagicEvas { get; set; } = 0;
        /// <summary>
        /// 攻击速度
        /// </summary>
        public int AttackSpeed { get; set; } = 0;
        /// <summary>
        /// 移动速度
        /// </summary>
        public int MoveSpeed { get; set; } = 0;
        /// <summary>
        /// 物伤减免
        /// </summary>
        public int PhysicalDamageReduc { get; set; } = 0;
        /// <summary>
        /// 魔伤减免
        /// </summary>
        public int MagicDamageReduc { get; set; } = 0;
        /// <summary>
        /// 物伤反射
        /// </summary>
        public int PhysicalDamageReflex { get; set; } = 0;
        /// <summary>
        /// 魔伤反射
        /// </summary>
        public int MagicDamageReflex { get; set; } = 0;
        /// <summary>
        /// 暴击几率
        /// </summary>
        public int HealsPR { get; set; } = 0;
        /// <summary>
        /// 攻击伤害
        /// </summary>
        public int AttackPR { get; set; } = 0;
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int MagicAC { get; set; } = 0;
        /// <summary>
        /// 忽视防御
        /// </summary>
        public int IgnoreAC { get; set; } = 0;
        /// <summary>
        /// 伤害反弹
        /// </summary>
        public int DamageRebound { get; set; } = 0;
        /// <summary>
        /// 人物爆率
        /// </summary>
        public int UserExpRate { get; set; } = 0;
        /// <summary>
        /// 体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
        /// <summary>
        /// 防爆出率
        /// </summary>
        public int EquBurstRate { get; set; } = 0;
        /// <summary>
        /// 致命伤害
        /// </summary>
        public int FatalInjury { get; set; } = 0;
        /// <summary>
        /// 致命一击
        /// </summary>
        public int FatalBlow { get; set; } = 0;
        /// <summary>
        /// 卓越一击
        /// </summary>
        public int ExcellentBlow { get; set; } = 0;
        /// <summary>
        /// 会心一击
        /// </summary>
        public int HeartfeltBlow { get; set; } = 0;
        /// <summary>
        /// 打怪经验增加
        /// </summary>
        public int MonExpInc { get; set; } = 0;
    }
    public class YHZLAttribute
    {
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp { get; set; } = 0;
        /// <summary>
        /// 蓝量
        /// </summary>
        public int Mp { get; set; } = 0;
        /// <summary>
        /// 防御下限
        /// </summary>
        public int Ac { get; set; } = 0;
        /// <summary>
        /// 防御上限
        /// </summary>
        public int Ac2 { get; set; } = 0;
        /// <summary>
        /// 魔法防御下限
        /// </summary>
        public int Mac { get; set; } = 0;
        /// <summary>
        /// 魔法防御上限
        /// </summary>
        public int Mac2 { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int Dc { get; set; } = 0;
        /// <summary>
        /// 最大物理攻击力
        /// </summary>
        public int DcMax { get; set; } = 0;
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Mc { get; set; } = 0;
        /// <summary>
        /// 最大魔法攻击力
        /// </summary>
        public int McMax { get; set; } = 0;
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int Sc { get; set; } = 0;
        /// <summary>
        /// 最大道术攻击力
        /// </summary>
        public int ScMax { get; set; } = 0;
        /// <summary>
        /// 准确
        /// </summary>
        public int Acc { get; set; } = 0;
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; set; } = 0;

        /// <summary>
        /// 暴击
        /// </summary>
        public int Heals { get; set; } = 0;
        /// <summary>
        /// 幸运
        /// </summary>
        public int Lucky { get; set; } = 0;
        /// <summary>
        /// 倍功
        /// </summary>
        public int DouBPower { get; set; } = 0;
        /// <summary>
        /// 魔法躲避
        /// </summary>
        public int MagicEvas { get; set; } = 0;
        /// <summary>
        /// 攻击速度
        /// </summary>
        public int AttackSpeed { get; set; } = 0;
        /// <summary>
        /// 移动速度
        /// </summary>
        public int MoveSpeed { get; set; } = 0;
        /// <summary>
        /// 物伤减免
        /// </summary>
        public int PhysicalDamageReduc { get; set; } = 0;
        /// <summary>
        /// 魔伤减免
        /// </summary>
        public int MagicDamageReduc { get; set; } = 0;
        /// <summary>
        /// 物伤反射
        /// </summary>
        public int PhysicalDamageReflex { get; set; } = 0;
        /// <summary>
        /// 魔伤反射
        /// </summary>
        public int MagicDamageReflex { get; set; } = 0;
        /// <summary>
        /// 暴击几率
        /// </summary>
        public int HealsPR { get; set; } = 0;
        /// <summary>
        /// 攻击伤害
        /// </summary>
        public int AttackPR { get; set; } = 0;
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int MagicAC { get; set; } = 0;
        /// <summary>
        /// 忽视防御
        /// </summary>
        public int IgnoreAC { get; set; } = 0;
        /// <summary>
        /// 伤害反弹
        /// </summary>
        public int DamageRebound { get; set; } = 0;
        /// <summary>
        /// 人物爆率
        /// </summary>
        public int UserExpRate { get; set; } = 0;
        /// <summary>
        /// 体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
        /// <summary>
        /// 防爆出率
        /// </summary>
        public int EquBurstRate { get; set; } = 0;
        /// <summary>
        /// 致命伤害
        /// </summary>
        public int FatalInjury { get; set; } = 0;
        /// <summary>
        /// 致命一击
        /// </summary>
        public int FatalBlow { get; set; } = 0;
        /// <summary>
        /// 卓越一击
        /// </summary>
        public int ExcellentBlow { get; set; } = 0;
        /// <summary>
        /// 会心一击
        /// </summary>
        public int HeartfeltBlow { get; set; } = 0;
    }

    /// <summary>
    /// 用户技能
    /// </summary>
    public class UserSkill
    {
        /// <summary>
        /// 技能id
        /// </summary>
        public int US_MagID { get; set; }
        /// <summary>
        /// 技能名称
        /// </summary>
        public string US_Name { get; set; }
        /// <summary>
        /// 1级技能熟练度
        /// </summary>
        public int US_NeedLN1 { get; set; }
        /// <summary>
        /// 2级技能熟练度
        /// </summary>
        public int US_NeedLN2 { get; set; }
        /// <summary>
        /// 3级技能熟练度
        /// </summary>
        public int US_NeedLN3 { get; set; }
        /// <summary>
        /// 技能等级
        /// </summary>
        public int US_Level { get; set; } = 0;
        /// <summary>
        /// 技能图片
        /// </summary>
        public string US_Img { get; set; }

        /// <summary>
        /// 是否启用技能
        /// </summary>
        public string US_IsEnableSkill { get; set; }
    }
    /// <summary>
    /// 身上装备物品
    /// </summary>
    public class UserItems
    {
        /// <summary>
        /// 衣服
        /// </summary>
        public string DRESS { get; set; } = "0";
        /// <summary>
        /// 武器
        /// </summary>
        public string WEAPON { get; set; } = "0";
        /// <summary>
        /// 勋章
        /// </summary>
        public string RIGHTHAND { get; set; } = "0";
        /// <summary>
        /// 头盔
        /// </summary>
        public string HELMET { get; set; } = "0";
        /// <summary>
        /// 项链
        /// </summary>
        public string NECKLACE { get; set; } = "0";
        /// <summary>
        /// 右戒指
        /// </summary>
        public string RING_R { get; set; } = "0";
        /// <summary>
        /// 左戒指
        /// </summary>
        public string RING_L { get; set; } = "0";
        /// <summary>
        /// 右手镯
        /// </summary>
        public string ARMRING_R { get; set; } = "0";
        /// <summary>
        /// 左手镯
        /// </summary>
        public string ARMRING_L { get; set; } = "0";
        /// <summary>
        /// 护身符
        /// </summary>
        public string BUJUK { get; set; } = "0";
        /// <summary>
        /// 腰带
        /// </summary>
        public string BELT { get; set; } = "0";
        /// <summary>
        /// 鞋子
        /// </summary>
        public string BOOTS { get; set; } = "0";
        /// <summary>
        /// 宝石
        /// </summary>
        public string CHARM { get; set; } = "0";
        /// <summary>
        /// 斗笠
        /// </summary>
        public string HAT { get; set; } = "0";
        /// <summary>
        /// 军鼓
        /// </summary>
        public string DRUM { get; set; } = "0";
        /// <summary>
        /// 盾牌
        /// </summary>
        public string SHIELD { get; set; } = "0";
        /// <summary>
        /// 马牌
        /// </summary>
        public string HORSE { get; set; } = "0";
        /// <summary>
        /// 时装衣服
        /// </summary>
        public string SZ_DRESS { get; set; } = "0";
        /// <summary>
        /// 时装武器
        /// </summary>
        public string SZ_WEAPON { get; set; } = "0";

        /// <summary>
        /// 头盔
        /// </summary>
        public string SZ_HELMET { get; set; } = "0";
        /// <summary>
        /// 项链
        /// </summary>
        public string SZ_NECKLACE { get; set; } = "0";
        /// <summary>
        /// 右戒指
        /// </summary>
        public string SZ_RING_R { get; set; } = "0";
        /// <summary>
        /// 左戒指
        /// </summary>
        public string SZ_RING_L { get; set; } = "0";
        /// <summary>
        /// 右手镯
        /// </summary>
        public string SZ_ARMRING_R { get; set; } = "0";
        /// <summary>
        /// 左手镯
        /// </summary>
        public string SZ_ARMRING_L { get; set; } = "0";
        /// <summary>
        /// 护身符
        /// </summary>
        public string SZ_BUJUK { get; set; } = "0";
        /// <summary>
        /// 腰带
        /// </summary>
        public string SZ_BELT { get; set; } = "0";
        /// <summary>
        /// 鞋子
        /// </summary>
        public string SZ_BOOTS { get; set; } = "0";
        /// <summary>
        /// 宝石
        /// </summary>
        public string SZ_CHARM { get; set; } = "0";
        /// <summary>
        /// 军鼓
        /// </summary>
        public string SZ_DRUM { get; set; } = "0";
        /// <summary>
        /// 时装勋章
        /// </summary>
        public string SZ_RIGHTHAND { get; set; } = "0";
    }
    /// <summary>
    /// 装备回收属性
    /// </summary>
    public class UserEquRecycleAttr
    {
        /// <summary>
        /// 自动回收
        /// </summary>
        public string AutoRecycl { get; set; }

        /// <summary>
        /// 垃圾系列
        /// </summary>
        public string AutoRecyclType1 { get; set; }
        /// <summary>
        /// 沃玛系列
        /// </summary>
        public string AutoRecyclType2 { get; set; }
        /// <summary>
        /// 祖玛系列
        /// </summary>
        public string AutoRecyclType3 { get; set; }
        /// <summary>
        /// 赤月系列
        /// </summary>
        public string AutoRecyclType4 { get; set; }
        /// <summary>
        /// 永恒首饰
        /// </summary>
        public string AutoRecyclType5 { get; set; }
        /// <summary>
        /// 永恒剑甲
        /// </summary>
        public string AutoRecyclType6 { get; set; }
        /// <summary>
        /// 毁灭首饰
        /// </summary>
        public string AutoRecyclType7 { get; set; }
        /// <summary>
        /// 毁灭剑甲
        /// </summary>
        public string AutoRecyclType8 { get; set; }
        /// <summary>
        /// 刑天首饰
        /// </summary>
        public string AutoRecyclType9 { get; set; }
        /// <summary>
        /// 焚天首饰
        /// </summary>
        public string AutoRecyclType10 { get; set; }
        /// <summary>
        /// 沧澜首饰
        /// </summary>
        public string AutoRecyclType11 { get; set; }
        /// <summary>
        /// 刑焚沧甲
        /// </summary>
        public string AutoRecyclType12 { get; set; }
        /// <summary>
        /// 刑焚沧剑
        /// </summary>
        public string AutoRecyclType13 { get; set; }
        /// <summary>
        /// 灭世首饰
        /// </summary>
        public string AutoRecyclType14 { get; set; }
        /// <summary>
        /// 灭世剑甲
        /// </summary>
        public string AutoRecyclType15 { get; set; }
        /// <summary>
        /// 轮回首饰
        /// </summary>
        public string AutoRecyclType16 { get; set; }
        /// <summary>
        /// 轮回剑甲
        /// </summary>
        public string AutoRecyclType17 { get; set; }
        /// <summary>
        /// 起源首饰
        /// </summary>
        public string AutoRecyclType18 { get; set; }
        /// <summary>
        /// 起源剑甲
        /// </summary>
        public string AutoRecyclType19 { get; set; }
        /// <summary>
        /// 回忆首饰
        /// </summary>
        public string AutoRecyclType20 { get; set; }
        /// <summary>
        /// 回忆剑甲
        /// </summary>
        public string AutoRecyclType21 { get; set; }
        /// <summary>
        /// 命运首饰
        /// </summary>
        public string AutoRecyclType22 { get; set; }
        /// <summary>
        /// 命运剑甲
        /// </summary>
        public string AutoRecyclType23 { get; set; }
        /// <summary>
        /// 边界首饰
        /// </summary>
        public string AutoRecyclType24 { get; set; }
        /// <summary>
        /// 边界剑甲
        /// </summary>
        public string AutoRecyclType25 { get; set; }
        /// <summary>
        /// 毒蛇首饰
        /// </summary>
        public string AutoRecyclType26 { get; set; }
        /// <summary>
        /// 毒蛇剑甲
        /// </summary>
        public string AutoRecyclType27 { get; set; }
        /// <summary>
        /// 沙漠首饰
        /// </summary>
        public string AutoRecyclType28 { get; set; }
        /// <summary>
        /// 沙漠剑甲
        /// </summary>
        public string AutoRecyclType29 { get; set; }
        /// <summary>
        /// 古城首饰
        /// </summary>
        public string AutoRecyclType30 { get; set; }
        /// <summary>
        /// 古城剑甲
        /// </summary>
        public string AutoRecyclType31 { get; set; }
        /// <summary>
        /// 孤岛首饰
        /// </summary>
        public string AutoRecyclType32 { get; set; }
        /// <summary>
        /// 孤岛剑甲
        /// </summary>
        public string AutoRecyclType33 { get; set; }
        /// <summary>
        /// 仙门首饰
        /// </summary>
        public string AutoRecyclType34 { get; set; }
        /// <summary>
        /// 仙门剑甲
        /// </summary>
        public string AutoRecyclType35 { get; set; }
        /// <summary>
        /// 战神首饰
        /// </summary>
        public string AutoRecyclType36 { get; set; }
        /// <summary>
        /// 战神剑甲
        /// </summary>
        public string AutoRecyclType37 { get; set; }
        /// <summary>
        /// 复古首饰
        /// </summary>
        public string AutoRecyclType38 { get; set; }
        /// <summary>
        /// 复古剑甲
        /// </summary>
        public string AutoRecyclType39 { get; set; }
        /// <summary>
        /// 一级神技
        /// </summary>
        public string AutoRecyclType40 { get; set; }
        /// <summary>
        /// 二级神技
        /// </summary>
        public string AutoRecyclType41 { get; set; }
        /// <summary>
        /// 三级神技
        /// </summary>
        public string AutoRecyclType42 { get; set; }
        /// <summary>
        /// 四级神技
        /// </summary>
        public string AutoRecyclType43 { get; set; }
        /// <summary>
        /// 五级神技
        /// </summary>
        public string AutoRecyclType44 { get; set; }
        /// <summary>
        /// 六级神技
        /// </summary>
        public string AutoRecyclType45 { get; set; }
        /// <summary>
        /// 初级时装
        /// </summary>
        public string AutoRecyclType46 { get; set; }
        /// <summary>
        /// 复古时装
        /// </summary>
        public string AutoRecyclType47 { get; set; }
        /// <summary>
        /// 血牛时装
        /// </summary>
        public string AutoRecyclType48 { get; set; }
        /// <summary>
        /// 特殊物品
        /// </summary>
        public string AutoRecyclType49 { get; set; }
    }
}



