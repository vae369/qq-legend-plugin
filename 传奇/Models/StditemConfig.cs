using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 极品物品配置类
    /// </summary>
    public class StditemConfig
    {
        /// <summary>
        /// 防御最高点数
        /// </summary>
        public int AC_Max { get;set; }
        /// <summary>
        /// 魔御最高点数
        /// </summary>
        public int MC_Max { get; set; }
        /// <summary>
        /// 攻击最高点数
        /// </summary>
        public int DC_Max { get; set; }
        /// <summary>
        /// 魔法最高点数
        /// </summary>
        public int MAC_Max { get; set; }
        /// <summary>
        /// 道术最高点数
        /// </summary>
        public int SC_Max { get; set; }
        /// <summary>
        /// 加血最高点数
        /// </summary>
        public int HP_Max { get; set; }
        /// <summary>
        /// 持久最高点数
        /// </summary>
        public int DUR_Max { get; set; }
        /// <summary>
        /// 准确最高点数
        /// </summary>
        public int ACC_Max { get; set; }
        /// <summary>
        /// 强度最高点数
        /// </summary>
        public int STR_Max { get; set; }
        /// <summary>
        /// 幸运最高点数
        /// </summary>
        public int LUK_Max { get; set; }
        /// <summary>
        /// 敏捷最高点数
        /// </summary>
        public int AG_Max { get; set; }
        /// <summary>
        /// 负重最高点数
        /// </summary>
        public int FZ_Max { get; set; }

        /// <summary>
        /// 防御属性几率
        /// </summary>
        public int AC_MaxQty { get; set; }
        /// <summary>
        /// 魔御属性几率
        /// </summary>
        public int MC_MaxQty { get; set; }
        /// <summary>
        /// 攻击属性几率
        /// </summary>
        public int DC_MaxQty { get; set; }
        /// <summary>
        /// 魔法属性几率
        /// </summary>
        public int MAC_MaxQty { get; set; }
        /// <summary>
        /// 道术属性几率
        /// </summary>
        public int SC_MaxQty { get; set; }
        /// <summary>
        /// 加血属性几率
        /// </summary>
        public int HP_MaxQty { get; set; }
        /// <summary>
        /// 持久属性几率
        /// </summary>
        public int DUR_MaxQty { get; set; }
        /// <summary>
        /// 准确属性几率
        /// </summary>
        public int ACC_MaxQty { get; set; }
        /// <summary>
        /// 强度属性几率
        /// </summary>
        public int STR_MaxQty { get; set; }
        /// <summary>
        /// 幸运属性几率
        /// </summary>
        public int LUK_MaxQty { get; set; }
        /// <summary>
        /// 敏捷属性几率
        /// </summary>
        public int AG_MaxQty { get; set; }
        /// <summary>
        /// 负重属性几率
        /// </summary>
        public int FZ_MaxQty { get; set; }



        /// <summary>
        /// 人物魔法躲避
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
        /// 人物物伤反射
        /// </summary>
        public int PhysicalDamageReflex { get; set; } = 0;
        /// <summary>
        /// 人物魔伤反射
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
        /// 增加魔法防御
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
        /// 增加怪物爆率
        /// </summary>
        public int MonExpRate { get; set; } = 0;
        /// <summary>
        /// 增加致命伤害
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
        /// 人物魔法躲避几率
        /// </summary>
        public int MagicEvas_Qty { get; set; } = 0;
        /// <summary>
        /// 增加攻击速度
        /// </summary>
        public int AttackSpeed_Qty { get; set; } = 0;
        /// <summary>
        /// 增加移动速度几率
        /// </summary>
        public int MoveSpeed_Qty { get; set; } = 0;
        /// <summary>
        /// 物理伤害减少几率
        /// </summary>
        public int PhysicalDamageReduc_Qty { get; set; } = 0;
        /// <summary>
        /// 魔法伤害减少几率
        /// </summary>
        public int MagicDamageReduc_Qty { get; set; } = 0;
        /// <summary>
        /// 人物物伤反射几率
        /// </summary>
        public int PhysicalDamageReflex_Qty { get; set; } = 0;
        /// <summary>
        /// 人物魔伤反射几率
        /// </summary>
        public int MagicDamageReflex_Qty { get; set; } = 0;
        /// <summary>
        /// 暴击几率增加几率
        /// </summary>
        public int HealsPR_Qty { get; set; } = 0;
        /// <summary>
        /// 增加攻击伤害几率
        /// </summary>
        public int AttackPR_Qty { get; set; } = 0;
        /// <summary>
        /// 增加魔法防御几率
        /// </summary>
        public int MagicAC_Qty { get; set; } = 0;
        /// <summary>
        /// 忽视目标防御
        /// </summary>
        public int IgnoreAC_Qty { get; set; } = 0;
        /// <summary>
        /// 所有伤害反弹几率
        /// </summary>
        public int DamageRebound_Qty { get; set; } = 0;
        /// <summary>
        /// 增加人物爆率几率
        /// </summary>
        public int UserExpRate_Qty { get; set; } = 0;
        /// <summary>
        /// 人物体力增加几率
        /// </summary>
        public int HPIncrease_Qty { get; set; } = 0;
        /// <summary>
        /// 增加怪物爆率几率
        /// </summary>
        public int MonExpRate_Qty { get; set; } = 0;
        /// <summary>
        /// 增加致命伤害几率
        /// </summary>
        public int FatalInjury_Qty { get; set; } = 0;
        /// <summary>
        /// 致命一击几率
        /// </summary>
        public int FatalBlow_Qty { get; set; } = 0;
        /// <summary>
        /// 卓越一击几率
        /// </summary>
        public int ExcellentBlow_Qty { get; set; } = 0;
        /// <summary>
        /// 会心一击几率
        /// </summary>
        public int HeartfeltBlow_Qty { get; set; } = 0;
    }

}
