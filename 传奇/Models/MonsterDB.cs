using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 传奇怪物属性
    /// </summary>
    public class MonsterDB
    {
        /// <summary>
        /// 怪物序号
        /// </summary>
        public int MonID { get; set; }
        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonName { get; set; }
        /// <summary>
        /// 怪物等级
        /// </summary>
        public int Lvl { get; set; }
        /// <summary>
        /// 怪物外观(通过外观定位怪物是哪个素材)
        /// </summary>
        public int Appr { get; set; }
        /// <summary>
        /// 怪物经验
        /// </summary>
        public int Exp { get; set; }
        /// <summary>
        /// 怪物血量
        /// </summary>
        public int HP { get; set; }
        /// <summary>
        /// 怪物魔法
        /// </summary>
        public int MP { get; set; }
        /// <summary>
        /// 怪物防御力下限
        /// </summary>
        public int AC { get; set; }
        /// <summary>
        /// 魔法防御力
        /// </summary>
        public int MAC { get; set; }
        /// <summary>
        /// 物理攻击力下限
        /// </summary>
        public int Dc { get; set; }
        /// <summary>
        /// 物理攻击力上限
        /// </summary>
        public int Dc2 { get; set; }
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int MC { get; set; }
        /// <summary>
        /// 道士精神力
        /// </summary>
        public int SC { get; set; }
        /// <summary>
        /// 攻击命中率
        /// </summary>
        public int Hit { get; set; }
        /// <summary>
        /// 简单备注
        /// </summary>
        public string MonDescr { get; set; }

        /// <summary>
        /// 掉落的装备
        /// </summary>
        public List<StditemDB> MonItemList { get; set; }
    }
}
