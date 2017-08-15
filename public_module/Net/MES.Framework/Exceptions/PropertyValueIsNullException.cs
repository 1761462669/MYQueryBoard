using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Exceptions
{
    /// <summary>
    /// 属性为空的异常
    /// </summary>
    public class PropertyValueIsNullException : Exception
    {
        public PropertyValueIsNullException()
            : base()
        {

        }
        public PropertyValueIsNullException(string message)
            : base(message)
        {

        }
    }
}
