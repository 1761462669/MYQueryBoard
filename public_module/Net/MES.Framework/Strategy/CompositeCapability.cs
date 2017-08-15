using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Strategy
{
    /// <summary>
    /// 组合设备能力
    /// </summary>
    public class CompositeCapability : List<IResourceCapability>, ICompositeCapability
    {
        /// <summary>
        /// 速率
        /// </summary>
        public decimal Rate
        {
            get
            {
                return this.Sum(c => c.Rate);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal GetCapability(DateTime startTime, DateTime endTime)
        {
            var sum = 0M;
            foreach (var item in this)
            {
                sum += item.GetCapability(startTime, endTime);
            }
            return sum;
        }

        public decimal GetCapability()
        {
            return this.Sum(c => c.GetCapability());
        }

        public IResourceCapability Copy()
        {
            throw new NotImplementedException();
        }
    }
}
