using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 传奇
{
    public class MonQuest
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int MQ_ID { get; set; }
        /// <summary>
        /// 怪物序号
        /// </summary>
        public int MQ_MonID { get; set; }
        /// <summary>
        /// 物品序号
        /// </summary>
        public int MQ_StdID { get; set; }
        /// <summary>
        /// 物品图片
        /// </summary>
        public Image MQ_StdImg { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string MQ_StdName { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string StdImgPath { get; set; }
        /// <summary>
        /// 物品数量
        /// </summary>
        public int MQ_MonQty { get; set; }
        /// <summary>
        /// 物品爆率
        /// </summary>
        public int MQ_MonExpRate { get; set; }
        /// <summary>
        /// 是否必爆
        /// </summary>
        public bool MQ_IsBB { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool MQ_IsChecked { get; set; }
        /// <summary>
        /// 必爆几率
        /// </summary>
        public int MQ_ItemFalling { get; set; }
    }
}
