using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 传奇
{
    public class LegendFilePath
    {
        public static string ConfigString = $"{Application.StartupPath}\\Config\\传奇\\config.ini";
        public static string BaseConfigPath = $"{Application.StartupPath}\\Config\\传奇\\";

        public static string LegendIMGDataPath = $"{BaseConfigPath}图片样式\\";
        public static string UserPath = $"{BaseConfigPath}用户数据\\";
        public static string UserRechargePath = $"{BaseConfigPath}用户充值\\";
        public static string UserAttrPath = $"{BaseConfigPath}人物等级属性\\";
        public static string Mud2Path = $"{BaseConfigPath}Mud2\\";
        public static string MapPath = $"{BaseConfigPath}地图数据\\";
        public static string MonQuestPath = $"{BaseConfigPath}物品爆率\\";
        public static string SignInPath = $"{BaseConfigPath}签到数据\\";
        public static string imgPath = $"{BaseConfigPath}图片数据\\";
        public static string UserSkillPath = $"{BaseConfigPath}技能配置\\";
        public static string PremiumAttributePath = $"{BaseConfigPath}极品属性\\";
        public static string ShopPath = $"{BaseConfigPath}商铺\\";
    }
}
