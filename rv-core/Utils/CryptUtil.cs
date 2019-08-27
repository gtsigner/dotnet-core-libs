using System.Security.Cryptography;
using System.Text;

namespace rv_core.Utils
{
    public class CryptUtil
    {
        public static string Md5Encode(string str)
        {
            var sor = Encoding.UTF8.GetBytes(str);
            var md5 = MD5.Create();
            var result = md5.ComputeHash(sor);
            var strbul = new StringBuilder(40);
            foreach (var t in result)
            {
                strbul.Append(t.ToString("x2")); //加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }

            return strbul.ToString();
        }
    }
}