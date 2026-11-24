using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// 地图信息
    /// </summary>
    public class LengendMapInfo
    {
        /// <summary>
        /// 地图ID
        /// </summary>
        public int LMI_ID { get; set; }
        /// <summary>
        /// 地图父ID
        /// </summary>
        public int LMI_PID { get; set; }

        /// <summary>
        /// 地图名称
        /// </summary>
        public string LMI_MapName { get; set; }
        /// <summary>
        /// 显示地图名称
        /// </summary>
        public string LMI_DisplayMember { get; set; }        
        /// <summary>
        /// 地图编号
        /// </summary>
        public string LMI_MapCode { get; set; }
        /// <summary>
        /// 地图地址
        /// </summary>
        public string LMI_MapFile { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        public int? LMI_Xpoint { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public int? LMI_Ypoint { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int? LMI_FontSize { get; set; }
        /// <summary>
        /// 字体宽度
        /// </summary>
        public int? LMI_FrontWidth { get; set; }
        /// <summary>
        /// 字体高度
        /// </summary>
        public int? LMI_FrontHeight { get; set; }

    }
}
