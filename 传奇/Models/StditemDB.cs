using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 传奇物品属性
    /// </summary>
    public class StditemDB
    {

        /// <summary>
        /// 序号
        /// </summary>
        public int StdID { get; set; }
        /// <summary>
        /// 物品图片地址
        /// </summary>
        public string StdImgPath { get; set; }
        /// <summary>
        /// 物品图片
        /// </summary>
        public Image StdImg { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string StdName { get; set; }
        /// <summary>
        /// 外观
        /// 武器 * 1200取图片
        /// </summary>
        public int Shape { get; set; }
        /// <summary>
        /// 属性类型
        /// 10-男衣
        ///11-女衣
        ///5-单手
        ///6-双手
        ///30-蜡烛类
        ///22-毒药,符类
        ///42-制作原料
        /// </summary>
        public int StdMode { get; set; }
        /// <summary>
        /// 物件重量
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 显示衣服物品栏里样子和在装备栏里的样子
        /// </summary>
        public int Looks { get; set; }
        /// <summary>
        /// 物件持久
        /// </summary>
        public int Duramax { get; set; }
        /// <summary>
        /// 防御力下限
        /// </summary>
        public int StdAC { get; set; }
        /// <summary>
        /// 防御力上限
        /// </summary>
        public int StdAC2 { get; set; }
        /// <summary>
        /// 魔防下限
        /// </summary>
        public int StdMac { get; set; }
        /// <summary>
        /// 魔防上限
        /// </summary>
        public int StdMac2 { get; set; }
        /// <summary>
        /// 物理攻击力下限 
        /// </summary>
        public int StdDc { get; set; }
        /// <summary>
        /// 物理攻击力上限
        /// </summary>
        public int StdDc2 { get; set; }
        /// <summary>
        /// 魔法攻击力下限
        /// </summary>
        public int StdMc { get; set; }
        /// <summary>
        /// 魔法攻击力上限
        /// </summary>
        public int StdMc2 { get; set; }
        /// <summary>
        /// 道术下限
        /// </summary>
        public int StdSc { get; set; }
        /// <summary>
        /// 道术上限
        /// </summary>
        public int StdSc2 { get; set; }
        /// <summary>
        /// 限制种类
        /// 需要等级
        /// 0 为等级限制
        /// 1 为攻击限制
        /// 2 为魔法限制
        /// 3 为道术限制
        /// </summary>
        public int Need { get; set; }
        /// <summary>
        /// 需要等级
        /// </summary>
        public int Needlevel { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 物品叠加
        /// </summary>
        public int OverLap { get; set; }
        /// <summary>
        /// 加血
        /// </summary>
        public int StdHP { get; set; }
        /// <summary>
        /// 加蓝
        /// </summary>
        public int StdMP { get; set; }
        /// <summary>
        /// 准确
        /// </summary>
        public int Accuracy { get; set; }
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; set; }
        /// <summary>
        /// 幸运
        /// </summary>
        public int Lucky { get; set; }
        /// <summary>
        /// 强度
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// 附加属性-防御
        /// </summary>
        public int SubAttr1 { get; set; }
        /// <summary>
        /// 附加属性-魔御
        /// </summary>
        public int SubAttr2 { get; set; }
        /// <summary>
        /// 附加属性-攻击
        /// </summary>
        public int SubAttr3 { get; set; }
        /// <summary>
        /// 附加属性-魔法
        /// </summary>
        public int SubAttr4 { get; set; }
        /// <summary>
        /// 附加属性-道术
        /// </summary>
        public int SubAttr5 { get; set; }
        /// <summary>
        /// 附加属性-准确
        /// </summary>
        public int SubAttr6 { get; set; }
        /// <summary>
        /// 附加属性-敏捷
        /// </summary>
        public int SubAttr7 { get; set; }
        /// <summary>
        /// 附加属性-幸运
        /// </summary>
        public int SubAttr8 { get; set; }
        /// <summary>
        /// 附加属性-倍功
        /// </summary>
        public int SubAttr9 { get; set; }
        /// <summary>
        /// 附加属性-爆率
        /// </summary>
        public int SubAttr10 { get; set; }
        /// <summary>
        /// 附加属性-负重
        /// </summary>
        public int SubAttr11{ get; set; }
        /// <summary>
        /// 附加属性-持久
        /// </summary>
        public int SubAttr12 { get; set; }
        /// <summary>
        /// 附加属性-强度
        /// </summary>
        public int SubAttr13 { get; set; }
        /// <summary>
        /// 附加属性-HP
        /// </summary>
        public int SubAttr14 { get; set; }
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
        /// <summary>
        /// 装备类型(普通=0,沃玛=1,祖玛=2,赤月=3)
        /// </summary>
        public int StdItemType { get; set; }
        /// <summary>
        /// 装备品质(普通,稀有,罕世,传说,绝品 )
        /// </summary>
        public int StdItemQuality { get; set; }
        /// <summary>
        /// 元素属性类
        /// </summary>
        public ElemAttr StdElemAttr { get; set; }
        /// <summary>
        /// 技能属性类
        /// </summary>
        public List<MagicAttr> StdMagicAttr { get; set; }
        /// <summary>
        /// 隐藏属性类
        /// </summary>
        public HiddAttr StdHiddAttr { get; set; }
        /// <summary>
        /// 装备星星等级类
        /// </summary>
        public List<StarAttr> StdStarAttr { get; set; }
    }

    /// <summary>
    /// 装备星星等级类
    /// </summary>
    public class StarAttr
    {
        /// <summary>
        /// 星星等级1-????
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 防御增加
        /// </summary>
        public int ACPR { get; set; }
        /// <summary>
        /// 魔御增加
        /// </summary>
        public int MacPR { get; set; } = 0;
        /// <summary>
        /// 人物体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 人物魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int DcPR { get; set; } = 0;
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int McPR { get; set; } = 0;
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int ScPR { get; set; } = 0;
    }

    /// <summary>
    /// 元素属性类
    /// </summary>
    public class ElemAttr
    {
        /// <summary>
        /// 魔法躲避
        /// </summary>
        public int MagicEvas { get; set; } = 0;
        /// <summary>
        /// 增加攻击速度
        /// </summary>
        public int AttackSpeed { get; set; } = 0;
        /// <summary>
        /// 增加移动速度
        /// </summary>
        public int MoveSpeed { get; set; } = 0;
        /// <summary>
        /// 物理伤害减少
        /// </summary>
        public int PhysicalDamageReduc { get; set; } = 0;
        /// <summary>
        /// 魔法伤害减少
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
        /// 暴击几率增加
        /// </summary>
        public int HealsPR { get; set; } = 0;
        /// <summary>
        /// 增加攻击伤害
        /// </summary>
        public int AttackPR { get; set; } = 0;
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int MagicAC { get; set; } = 0;
        /// <summary>
        /// 忽视目标防御
        /// </summary>
        public int IgnoreAC { get; set; } = 0;
        /// <summary>
        /// 所有伤害反弹
        /// </summary>
        public int DamageRebound { get; set; } = 0;
        /// <summary>
        /// 增加人物爆率
        /// </summary>
        public int UserExpRate { get; set; } = 0;
        /// <summary>
        /// 人物体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 人物魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 增加怪物爆率
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
    /// 技能属性类
    /// </summary>
    public class MagicAttr
    {
        /// <summary>
        /// 技能ID
        /// </summary>
        public int MagId { get; set; }
        /// <summary>
        /// 技能名称
        /// </summary>
        public string MagName { get; set; }
        /// <summary>
        /// 技能图标
        /// </summary>
        public string MagImg { get; set; }
        /// <summary>
        /// 攻击伤害增加
        /// </summary>
        public int AttackPR { get; set; }
        /// <summary>
        /// 受到伤害减少
        /// </summary>
        public int AttackRD { get; set; }
    }

    /// <summary>
    /// 隐藏属性类
    /// </summary>
    public class  HiddAttr
    {
        public int Hid { get; set; }
        /// <summary>
        /// 隐藏属性标题
        /// </summary>
        public string HidTitle { get; set; }
        /// <summary>
        /// 隐藏属性图标
        /// </summary>
        public string HidImg { get; set; }
        /// <summary>
        /// 技能ID
        /// </summary>
        public int MagId { get; set; }
        /// <summary>
        /// 技能名称
        /// </summary>
        public string MagName { get; set; }
        /// <summary>
        /// 搭配的物品ID
        /// </summary>
        public int DPStdId { get; set; }
        /// <summary>
        /// 搭配的物品名称
        /// </summary>
        public string DPStdName { get; set; }
        /// <summary>
        /// 怪物ID
        /// </summary>
        public int MonId { get; set; }
        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonName { get; set; }
        /// <summary>
        /// 武器ID
        /// </summary>
        public int WeaponId { get; set; }
        /// <summary>
        /// 武器名称
        /// </summary>
        public string WeaponName { get; set; }
        /// <summary>
        /// 隐藏属性触发的概率
        /// </summary>
        public int HidProbability { get; set; }
        /// <summary>
        /// 隐藏属性触发持续时间（秒）
        /// </summary>
        public int HidDuration { get; set; }
        /// <summary>
        /// 隐藏类型
        /// 0=普通攻击触发
        /// 1=技能攻击触发
        /// 2=佩戴触发
        /// 3=使用触发(仅限药品)
        /// 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 声望
        /// </summary>
        public int REP { get; set; } = 0;
        /// <summary>
        /// 等级
        /// </summary>
        public int Lv { get; set; } = 0;
        /// <summary>
        /// 元宝
        /// </summary>
        public int YB { get; set; } = 0;
        /// <summary>
        /// 金币
        /// </summary>
        public int JB { get; set; } = 0;
        /// <summary>
        /// 金刚石
        /// </summary>
        public int JGS { get; set; } = 0;
        /// <summary>
        /// 防御增加
        /// </summary>
        public int ACPR { get; set; }
        /// <summary>
        /// 防御减少
        /// </summary>
        public int ACRD { get; set; }
        /// <summary>
        /// 魔御增加
        /// </summary>
        public int MacPR { get; set; } = 0;
        /// <summary>
        /// 魔御减少
        /// </summary>
        public int MacRD { get; set; } = 0;
        /// <summary>
        /// 人物体力增加
        /// </summary>
        public int HPIncrease { get; set; } = 0;
        /// <summary>
        /// 人物魔力增加
        /// </summary>
        public int MPIncrease { get; set; } = 0;
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int DcPR { get; set; } = 0; 
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int McPR { get; set; } = 0; 
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int ScPR { get; set; } = 0;
        /// <summary>
        /// 穿戴重量
        /// </summary>
        public int Wear { get; set; } = 0;
        /// <summary>
        /// 腕力
        /// </summary>
        public int Wrist { get; set; } = 0;
    }
}
