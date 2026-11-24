using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    /// <summary>
    /// Boss类
    /// </summary>
    public class MonBoss
    {
        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonsterName { get; set; }
        /// <summary>
        /// 怪物死亡时间
        /// </summary>
        public string MonsterDeadTime { get; set; }
        /// <summary>
        /// 怪物刷新时间
        /// </summary>
        public string MonsterLiveTime { get; set; }
        /// <summary>
        /// 怪物状态(已刷新,未刷新)
        /// </summary>
        public string MonsterStatus { get; set; }
        /// <summary>
        /// 所在地图
        /// </summary>
        public string MonsterMap { get; set; }
        /// <summary>
        /// 默认打怪活跃值
        /// </summary>
        public int MonsterDefaultQty { get; set; }
        /// <summary>
        /// 指定打怪活跃值
        /// </summary>
        public int MonsterAssignQty { get; set; }
        /// <summary>
        /// 怪物刷新时长(分钟)
        /// </summary>
        public int MonsterRefreshTime { get; set; }

        /// <summary>
        /// 设置Boss死亡数据
        /// </summary>
        /// <param name="mBoss"></param>
        public static void SetMonBossDead(MonBoss mBoss)
        {
            DateTime dt = DateTime.Now;
            mBoss.MonsterDeadTime = dt.ToString();
            mBoss.MonsterLiveTime = dt.AddMinutes(mBoss.MonsterRefreshTime).ToString();
            mBoss.MonsterStatus = "已死亡";
            string path = LegendFilePath.Mud2Path + "boss信息.txt";
            List<string> list = FileHelper.ReadTxtReturnList(LegendFilePath.Mud2Path + "boss信息.txt");
            int i = 0, p = 0;
            string map = "", str = "", monName = "";
            while (list.Count != i)
            {
                str = list[i];
                if (string.IsNullOrEmpty(str))
                {
                    i++;
                    p++;
                    continue;
                }
                if (str.StartsWith("[") && str.EndsWith("]"))
                {
                    map = str.Substring(1, str.Length - 2);
                    if (map == mBoss.MonsterMap)
                    {
                        p = 1;
                    }
                    else
                    {
                        map = "";
                    }
                    i++;
                    continue;
                }
                if (!string.IsNullOrEmpty(map))
                {
                    if (str.StartsWith(p + "怪物名称"))
                    {
                        monName = str.Split('=')[1];
                        if (map == mBoss.MonsterMap && monName == mBoss.MonsterName)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            monName = "";
                        }
                    }
                    if (str.StartsWith(p + "怪物死亡时间=") && !string.IsNullOrEmpty(monName))
                    {
                        list[i] = p + "怪物死亡时间=" + mBoss.MonsterDeadTime;
                    }
                    if (str.StartsWith(p + "怪物刷新时间=") && !string.IsNullOrEmpty(monName))
                    {
                        list[i] = p + "怪物刷新时间=" + mBoss.MonsterLiveTime;
                    }
                    if (str.StartsWith(p + "怪物状态=") && !string.IsNullOrEmpty(monName))
                    {
                        list[i] = p + "怪物状态=" + mBoss.MonsterStatus;
                    }
                }
                i++;
            }

            FileHelper.WriteLine(path, list);
        }
        /// <summary>
        /// 读取Boss txt文件内容转换为怪物列表
        /// </summary>
        public static List<MonBoss> GetMonBossByTXT()
        {
            List<string> list = FileHelper.ReadTxtReturnList(LegendFilePath.Mud2Path + "boss信息.txt");
            List<MonBoss> monBosses = new List<MonBoss>();
            MonBoss monBoss = null;
            string map = "", str = "";
            int i = 1;

            while (list.Count > 0)
            {
                try
                {
                    str = list[0];
                    if (string.IsNullOrEmpty(str))
                    {
                        list.RemoveAt(0);
                        continue;
                    }

                    if (str.StartsWith("[") && str.EndsWith("]"))
                    {
                        map = str.Substring(1, str.Length - 2);
                        list.RemoveAt(0);
                        i = 1;
                        continue;
                    }
                    if (str.Split('=').Length == 2)
                    {
                        if (monBoss == null)
                        {
                            monBoss = new MonBoss
                            {
                                MonsterMap = map,
                                MonsterName = str.Split('=')[1],
                            };
                            list.RemoveAt(0);
                            continue;
                        }
                        else
                        {
                            if (str.StartsWith(i + "怪物死亡时间="))
                            {
                                monBoss.MonsterDeadTime = str.Split('=')[1];
                                list.RemoveAt(0);
                                continue;
                            }
                            if (str.StartsWith(i + "怪物刷新时间="))
                            {
                                monBoss.MonsterLiveTime = str.Split('=')[1];
                                list.RemoveAt(0);
                                continue;
                            }
                            if (str.StartsWith(i + "怪物刷新时长="))
                            {
                                monBoss.MonsterRefreshTime = int.Parse(str.Split('=')[1]);
                                list.RemoveAt(0);
                                continue;
                            }
                            if (str.StartsWith(i + "怪物默认打怪活跃值="))
                            {
                                monBoss.MonsterDefaultQty = int.Parse(str.Split('=')[1]);
                                list.RemoveAt(0);
                                continue;
                            }
                            if (str.StartsWith(i + "怪物指定打怪活跃值="))
                            {
                                monBoss.MonsterAssignQty = int.Parse(str.Split('=')[1]);
                                list.RemoveAt(0);
                                continue;
                            }
                            if (str.StartsWith(i + "怪物状态="))
                            {
                                monBoss.MonsterStatus = str.Split('=')[1];
                                DateTime t1 = DateTime.Parse(monBoss.MonsterLiveTime);
                                if (DateTime.Compare(DateTime.Now, t1) > 0)
                                {
                                    monBoss.MonsterStatus = "已刷新";
                                }

                                list.RemoveAt(0);
                                i++;
                                monBosses.Add(monBoss);
                                monBoss = null;
                                continue;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }

            return monBosses;
        }
    }
}
