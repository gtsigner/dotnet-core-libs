using System;
using System.Security.Cryptography;
using System.Text;

namespace rv_core.Utils
{
    public class Encoder
    {
        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(string text)
        {
            //声明字符集   
            Encoding utf8, gb2312;
            //gb2312   
            gb2312 = Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gb2312(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }

        /// <summary>
        /// UnicodeToUtf8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnicodeToUtf8(string text)
        {
            byte[] utf = null;
            utf = Encoding.Unicode.GetBytes(text);
            utf = System.Text.Encoding.Convert(Encoding.Unicode, Encoding.UTF8, utf);
            //返回转换后的字符   
            return Encoding.UTF8.GetString(utf);
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="text"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string ConvertString(string text, Encoding from, Encoding to)
        {
            byte[] utf;
            utf = from.GetBytes(text);
            utf = Encoding.Convert(from, to, utf);
            return to.GetString(utf);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            byte[] sor = Encoding.UTF8.GetBytes(password);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2")); //加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }

            return strbul.ToString();
        }

        ///编码
        public static string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.UTF8.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }

            return encode;
        }

        ///解码
        public static string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = code;
            }

            return decode;
        }
    }
}