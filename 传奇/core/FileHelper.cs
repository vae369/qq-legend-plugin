using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Net;
using System.Collections;
using System.Data.OleDb;

namespace 传奇
{
    public class FileHelper
    {


        #region 日志记录
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="errMsg">信息</param>
        public static void Log(string errMsg)
        {
            try
            {
                string pathDir = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yy-MM-dd"));
                if (!Directory.Exists(pathDir))
                {
                    Directory.CreateDirectory(pathDir);
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("\r\n时间：{0}    信息：{1}\r\n", DateTime.Now.ToString(), errMsg);
                string path = string.Format("{0}/log.txt", pathDir);
                File.AppendAllText(path, sb.ToString(), Encoding.UTF8);
            }
            catch (IOException ex)
            {
                string path = string.Format("{0}syslog.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("时间：{0}    信息：[{1}----{2}----{3}]\r\n", DateTime.Now.ToString(), ex.Message, ex.Source, ex.StackTrace);
                File.AppendAllText(path, sb.ToString(), Encoding.UTF8);
            }
        }
        
        /// <summary>
        /// 获取文件信息
        /// </summary>
        public static List<string> GetFileInfo(string path)
        {
            List<string> list = new List<string>();
            if (!File.Exists(path))
                return null;
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string s;
            list.Clear();
            while ((s = sr.ReadLine()) != null)
            {
                list.Add(s);
            }
            sr.Close();
            sr.Dispose();
            return list;
        }
        
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="errMsg">内容</param>
        public static void WriteLine(string path, string strCnt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\r\n", strCnt);
            File.AppendAllText(path, sb.ToString(), Encoding.UTF8);
        }


        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="errMsg">内容</param>
        public static void WriteLine(string path, List<string> list)
        {
            File.WriteAllText(path, string.Join("\r\n", list), Encoding.UTF8);
        }
        #endregion

        #region 遍历文件夹中的子文件

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否删除</returns>
        public static bool DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (IOException e)
                {
                    Log(e.Message);
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 读文件
       
        /// <summary>
        /// 读取txt,返回List每行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> ReadTxtReturnList(string path)
        {
            if (!File.Exists(path))
                return null;
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (!s.StartsWith(";"))
                    list.Add(s);
            }
            sr.Close();
            sr.Dispose();
            return list;
        }


        /// <summary>
        /// 读取txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTxt(string path)
        {
            if (!File.Exists(path))
                return string.Empty;
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string str = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return str;
        }
         
        #endregion

    }
}
