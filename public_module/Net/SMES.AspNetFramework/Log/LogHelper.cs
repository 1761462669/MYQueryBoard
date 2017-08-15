using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Log
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Write(string msg)
        {
            new Log().Write(msg);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="ee">异常信息</param>
        public static void Write(Exception ee)
        {
            new Log().Write(ee);
        }
    }
}
