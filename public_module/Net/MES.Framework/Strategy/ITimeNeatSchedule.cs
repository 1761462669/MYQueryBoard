using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Strategy
{
    public interface ITimeNeatSchedule : ISchedule
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime NeatEndTime { get; }
    }
}
