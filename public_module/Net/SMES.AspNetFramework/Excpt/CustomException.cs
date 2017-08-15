using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Excpt
{
    public class CustomException:Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code
        { get; set; }

        public CustomException(int code, string message)
            : base(message)
        {
            Code = code;            
        }

        public CustomException(string message)
            : this(0,message)
        { }
    }
}
