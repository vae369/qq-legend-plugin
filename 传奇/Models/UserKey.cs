using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 传奇
{
    public class UserKey
    {
        public string NikcName { get; set; }
        public string cdKey { get; set; }
        public int keyCount { get; set; }
        public DateTime ExpireTime { get; set; }


        public string GenerateRandomNumber(int Length)
        {
            //设置生成随机数的随机值字符
            char[] constant ={
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            //实例化可变字符串类
            StringBuilder newRandom = new StringBuilder(constant.Length);
            //实例化随机数生成器
            Random rd = new Random();
            //进行For循环创建随机数Length是生成的长度
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
            }
            return newRandom.ToString();
        }


        public static List<T> GetPage<T>(List<T> list, int pageIndex, int pageSize)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (pageIndex < 1)
                throw new ArgumentException("页码必须大于0", nameof(pageIndex));

            if (pageSize < 1)
                throw new ArgumentException("每页大小必须大于0", nameof(pageSize));

            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
