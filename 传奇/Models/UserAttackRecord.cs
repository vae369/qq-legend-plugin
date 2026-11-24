using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    public class UserAttackRecord
    {
        /// <summary>
        /// 用户1
        /// </summary>
        public long p_id1 { get; set; } 
        /// <summary>
        /// 怪物/用户id
        /// </summary>
        public long p_id2 { get; set; }
        /// <summary>
        /// 用户1的血量
        /// </summary>
        public long p_hp1 { get; set; }
        /// <summary>
        /// 用户2的血量
        /// </summary>
        public long p_hp2 { get; set; }
        /// <summary>
        /// 用户1攻击的伤害
        /// </summary>
        public int p_attack1 { get; set; }
        /// <summary>
        /// 用户2攻击的伤害
        /// </summary>
        public int p_attack2 { get; set; }
        /// <summary>
        /// 用户1状态(丢失,闪避,攻击无效)
        /// </summary>
        public string p_msg1 { get; set; }
        /// <summary>
        /// 用户2状态(丢失,闪避,攻击无效)
        /// </summary>
        public string p_msg2 { get; set; }
        /// <summary>
        /// 用户1是否是魔法攻击
        /// </summary>
        public bool p_IsMagicAttck1 { get; set; }
        /// <summary>
        /// 用户2是否是魔法攻击
        /// </summary>
        public bool p_IsMagicAttck2 { get; set; }

        public MagicDB MagicDBModel1 { get; set; }

        public MagicDB MagicDBModel2 { get; set; }

    }
}
