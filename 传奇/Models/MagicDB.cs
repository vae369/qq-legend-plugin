using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 传奇技能属性
    /// </summary>
    public class MagicDB
    {
        /// <summary>
        /// 技能序号
        /// </summary>
        public int MagID { get; set; }
        /// <summary>
        /// 技能名字
        /// </summary>
        public string MagName { get; set; }
        /// <summary>
        /// 每次使用技能使用的魔法值
        /// </summary>
        public int Spell { get; set; }
        /// <summary>
        /// 技能的伤害值下限
        /// </summary>
        public int Power { get; set; }
        /// <summary>
        /// 技能的伤害值上限
        /// </summary>
        public int MaxPower { get; set; }
        /// <summary>
        /// 每次技能升级后增加使用的魔法值
        /// </summary>
        public int DefSpell { get; set; }
        /// <summary>
        /// 每次技能升级后增加的伤害值下限
        /// </summary>
        public int DefPower { get; set; }
        /// <summary>
        /// 每次技能升级后增加的伤害值上限
        /// </summary>
        public int DefMaxPower { get; set; }
        /// <summary>
        /// 可以学习技能的职业（0-战士,1-法师,2-道士）
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 技能升到1级需要的级别
        /// </summary>
        public int NeedL1 { get; set; }
        /// <summary>
        /// 技能升到2级需要的级别
        /// </summary>
        public int NeedL2 { get; set; }
        /// <summary>
        /// 技能升到3级需要的级别
        /// </summary>
        public int NeedL3 { get; set; }
        /// <summary>
        /// 技能升到1级需要的点数
        /// </summary>
        public int NeedLN1 { get; set; }
        /// <summary>
        /// 技能升到2级需要的点数
        /// </summary>
        public int NeedLN2 { get; set; }
        /// <summary>
        /// 技能升到3级需要的点数
        /// </summary>
        public int NeedLN3 { get; set; }
        /// <summary>
        /// 简单备注
        /// </summary>
        public string Descr { get; set; }



    }
}
