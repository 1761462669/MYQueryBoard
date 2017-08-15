using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Strategy
{
    /// <summary>
    /// 资源设备能力
    /// </summary>
    public class ResourceCapability : IResourceCapability
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        public string ResourceId { get; set; }

        public string ResourceName { get; set; }

        public string EquShortName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan Span
        {
            get { return this.EndTime - StartTime; }
        }

        public string ShiftId { get; set; }

        public string ShiftName { get; set; }

        public string TeamId { get; set; }

        public string TeamName { get; set; }

        public decimal Rate { get; set; }


        public decimal GetCapability(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                return 0M;
            if (startTime >= this.EndTime)
                return 0M;
            if (endTime <= this.StartTime)
                return 0M;
            var rs = this.StartTime <= startTime ? startTime : this.StartTime;
            var re = this.EndTime <= endTime ? this.EndTime : endTime;
            var hours = (re - rs).TotalHours;
            return decimal.Round(this.Rate * (decimal)hours, 1);

        }

        public decimal GetCapability()
        {
            if (this.EndTime == DateTime.MaxValue)
                return decimal.MaxValue;
            return this.GetCapability(this.StartTime, this.EndTime);
        }

        public IResourceCapability Copy()
        {
            return this.MemberwiseClone() as IResourceCapability;
        }
    }
}
