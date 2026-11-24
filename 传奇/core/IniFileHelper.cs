using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace 传奇.core
{
    public class IniFileHelper
    {
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>获取字符串类型对应的字节</summary>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileSection(string section, IntPtr lpReturnedString, int nSize, string fileName);

        private string iniFilePath;

        public IniFileHelper(string iniFilePath)
        {
            this.iniFilePath = iniFilePath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.iniFilePath);
        }

        public string Read(string section, string key)
        {
            StringBuilder SB = new StringBuilder(1024);
            int i = GetPrivateProfileString(section, key, "", SB, 1024, this.iniFilePath);
            if (SB == null) throw new Exception("异常：当前ini信息读到");
            return SB.ToString();
        }

        public string ReadValue(string section, string key)
        {
            bool foundSection = false;
            string value = string.Empty;
            if (File.Exists(this.iniFilePath))
            {
                var lines = File.ReadAllLines(this.iniFilePath, Encoding.Default);

                foreach (var line in lines)
                {
                    var trimmedLine = line.Trim();
                    if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                    {
                        var currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                        if (currentSection == section)
                        {
                            foundSection = true;
                        }
                        else
                        {
                            foundSection = false;
                        }
                    }
                    else if (foundSection && line.Contains("="))
                    {
                        var keyValue = line.Split('=');
                        if (keyValue.Length == 2 && keyValue[0].Trim() == key)
                        {
                            value = keyValue[1].Trim();
                            break;
                        }
                    }
                }
            }
            return value;
        }

        public bool Delete(string section, string key)
        {
            int r = WritePrivateProfileString(section, key, null, this.iniFilePath);
            if (r != 0) return true;
            else return false;
        }

        /// <summary>
        /// 获取段落下的所有键,写死读4096个大小字节,如果段落加起来字符过长会读不完整
        /// </summary>
        /// <param name="section">段落名</param>
        /// <returns>返回字符串数组,所有Key</returns>
        public List<string> GetAllKeys(string section)
        {
            List<string> keys = new List<string>();
            byte[] buf = new byte[10000000];
            int charLengh = GetPrivateProfileString(section, null, "", buf, buf.Length, this.iniFilePath);
            int j = 0;
            for (int i = 0; i < charLengh; i++)
            {
                if (buf[i] == 0)
                {
                    keys.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            }

            return keys;
        }

        public string GetRndId()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(100, 300));
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}
