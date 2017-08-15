using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Strategy
{
    /// <summary>
    /// 排产
    /// </summary>
    public interface ISchedule
    {
        /// <summary>
        /// 排产目标
        /// </summary>
        IPlan Target { get; set; }
        
        /// <summary>
        /// 能力
        /// </summary>
        ICompositeCapability Capabilities { get; set; }

        /// <summary>
        /// 排产
        /// </summary>
        void Schedule();


        
    }
}
