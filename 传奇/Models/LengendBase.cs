using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 传奇基础属性
    /// </summary>
    public class LengendBase
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 升级经验
        /// </summary>
        public long LvExp { get; set; }
        /// <summary>
        /// 血量
        /// </summary>
        public long Hp { get; set; }
        /// <summary>
        /// 蓝量
        /// </summary>
        public long Mp { get; set; }
        /// <summary>
        /// 防御下限
        /// </summary>
        public int Ac { get; set; }
        /// <summary>
        /// 防御上限
        /// </summary>
        public int Ac2 { get; set; }
        /// <summary>
        /// 魔法防御下限
        /// </summary>
        public int Mac { get; set; }
        /// <summary>
        /// 魔法防御上限
        /// </summary>
        public int Mac2 { get; set; }
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int Dc { get; set; }
        /// <summary>
        /// 最大物理攻击力
        /// </summary>
        public int DcMax { get; set; }
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Mc { get; set; }
        /// <summary>
        /// 最大魔法攻击力
        /// </summary>
        public int McMax { get; set; }
        /// <summary>
        /// 道术攻击力
        /// </summary>
        public int Sc { get; set; }
        /// <summary>
        /// 最大道术攻击力
        /// </summary>
        public int ScMax { get; set; }
        /// <summary>
        /// 背包重量
        /// </summary>
        public int Bgw { get; set; }
        /// <summary>
        /// 穿戴重量
        /// </summary>
        public int Wear { get; set; }
        /// <summary>
        /// 腕力
        /// </summary>
        public int Wrist { get; set; }
    }
}
