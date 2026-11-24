using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 怪物地图类
    /// </summary>
    public class LegMapMonInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int LMMI_ID { get; set; }
        /// <summary>
        /// 地图序号
        /// </summary>
        public int LMI_ID { get; set; }
        /// <summary>
        /// 怪物序号
        /// </summary>
        public int MonID { get; set; }
        /// <summary>
        /// 怪物数量
        /// </summary>
        public int MonQty { get; set; }
        /// <summary>
        /// 怪物排序
        /// </summary>
        public int MonSort { get; set; }
        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonName { get; set; }
        /// <summary>
        /// 显示怪物名称
        /// </summary>
        public string DisMonName { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheckedMon { get; set; }
        /// <summary>
        /// 怪物等级
        /// </summary>
        public int Lvl { get; set; }
        /// <summary>
        /// 怪物血量
        /// </summary>
        public int HP { get; set; }
    }
}
