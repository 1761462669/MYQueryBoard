using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Schedule
{
    /// <summary>
    /// 工单
    /// </summary>
    public interface IWorkOrder : IPlan
    {
        /// <summary>
        /// 对应的作业
        /// </summary>
        IBatch Batch { get;set; }
    }
}
