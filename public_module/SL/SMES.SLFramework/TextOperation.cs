using SMES.SLFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public static class TextOperator
    {
        static Encoding gb2312;
        public static string GetPYChar(this string c)
        {
            if (gb2312 == null)
                gb2312 = DBCSEncoding.GetDBCSEncoding("GB2312");
            byte[] bytes = new byte[2];
            bytes = gb2312.GetBytes(c);
            int num = (int)((bytes[0] * 0x100) + bytes[1]);
            if (num >= 0xb0a1)
            {
                if (num < 0xb0c5)
                {
                    return "a";
                }
                if (num < 0xb2c1)
                {
                    return "b";
                }
                if (num < 0xb4ee)
                {
                    return "c";
                }
                if (num < 0xb6ea)
                {
                    return "d";
                }
                if (num < 0xb7a2)
                {
                    return "e";
                }
                if (num < 0xb8c1)
                {
                    return "f";
                }
                if (num < 0xb9fe)
                {
                    return "g";
                }
                if (num < 0xbbf7)
                {
                    return "h";
                }
                if (num < 0xbfa6)
                {
                    return "j";
                }
                if (num < 0xc0ac)
                {
                    return "k";
                }
                if (num < 0xc2e8)
                {
                    return "l";
                }
                if (num < 0xc4c3)
                {
                    return "m";
                }
                if (num < 0xc5b6)
                {
                    return "n";
                }
                if (num < 0xc5be)
                {
                    return "o";
                }
                if (num < 0xc6da)
                {
                    return "p";
                }
                if (num < 0xc8bb)
                {
                    return "q";
                }
                if (num < 0xc8f6)
                {
                    return "r";
                }
                if (num < 0xcbfa)
                {
                    return "s";
                }
                if (num < 0xcdda)
                {
                    return "t";
                }
                if (num < 0xcef4)
                {
                    return "w";
                }
                if (num < 0xd1b9)
                {
                    return "x";
                }
                if (num < 0xd4d1)
                {
                    return "y";
                }
                if (num < 0xd7fa)
                {
                    return "z";
                }
            }
            return "*";
        }

        public static string GetPYString(this string str)
        {
            string str2 = "";
            string str4 = str;
            for (int i = 0; i < str4.Length; i = (int)(i + 1))
            {
                char ch = str4[i];
                if (!string.IsNullOrEmpty(ch.ToString().Trim()))
                {
                    if ((ch >= '!') && (ch <= '~'))
                    {
                        str2 = str2 + ((char)ch).ToString();
                    }
                    else
                    {
                        str2 = str2 + GetPYChar(((char)ch).ToString());
                    }

                }
            }
            return str2;
        }
    }
}
