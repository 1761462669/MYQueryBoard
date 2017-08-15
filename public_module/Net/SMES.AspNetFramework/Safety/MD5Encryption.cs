using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Safety
{
    public class MD5Encryption : IEncryption
    {
        public string Encryption(string text)
        {
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(text));
            //return System.Text.Encoding.UTF8.GetString(result);

            byte[] result = Encoding.Default.GetBytes(text);    //tbPass为输入密码的文本框  
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的

        }

        public string UnEncryption(string text)
        {
            return text;
        }
    }
}
