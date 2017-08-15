using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel
{
    /// <summary>
    /// 时间段
    /// </summary>
    public interface ITimeSegment
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; set; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        TimeSpan Span { get; }



    }
}
