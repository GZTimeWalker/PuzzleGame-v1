using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public static class Coding
    {
        public static class BASE64
        {
            public static string Decode(string str, string type = "utf-8") 
                => Encoding.GetEncoding(type).GetString(Convert.FromBase64String(str));

            public static string Encode(string str, string type = "utf-8")
                => Convert.ToBase64String(Encoding.GetEncoding(type).GetBytes(str));
        }

        /// <summary>
        /// 获取字符串ASCII数组
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns></returns>
        public static List<int> ASCII(string str)
        {
            byte[] buff = Encoding.ASCII.GetBytes(str);
            List<int> res = new List<int>();
            foreach (var item in buff)
                res.Add(item);
            return res;
        }

        /// <summary>
        /// 转换为对应进制
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="tobase">进制支持2,8,10,16</param>
        /// <returns></returns>
        public static List<string> ToBase(List<int> source,int tobase)
            => new List<string>(source.ConvertAll((int a) => Convert.ToString(a,tobase)));

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="s">原字符串</param>
        /// <returns></returns>
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
    }
}
