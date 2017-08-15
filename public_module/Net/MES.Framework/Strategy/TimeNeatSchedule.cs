using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Strategy
{
    public class TimeNeatSchedule : ITimeNeatSchedule
    {

        /// <summary>
        /// 整齐的结束时间
        /// </summary>
        public DateTime NeatEndTime { get; private set; }


        public IPlan Target { get; set; }


        public ICompositeCapability Capabilities { get; set; }


        public void Schedule()
        {
            if (this.Target == null || this.Capabilities == null || this.Capabilities.Count == 0)
                return;
            var start = this.Capabilities.Select(c => c.StartTime).Min();
            var end = this.Capabilities.Select(c => c.EndTime).Max();
            this.NeatEndTime = this.Schedule(start, end, Target.Qua, new TimeSpan(0, 10, 0));

        }


        protected virtual DateTime Schedule(DateTime start, DateTime end, decimal qua, TimeSpan span)
        {
            var temp = start;
            var tempQua = qua;
            while (tempQua > 0M && temp < end)
            {
                temp = temp.Add(span);
                var cap = this.Capabilities.GetCapability(start, temp);
                tempQua = qua - cap;

            }
            return temp;


        }





    }
}
