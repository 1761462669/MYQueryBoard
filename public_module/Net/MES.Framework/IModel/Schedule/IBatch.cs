using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Schedule
{
    /// <summary>
    /// 生产作业
    /// </summary>
    public interface IBatch : IPlan
    {
        /// <summary>
        /// 生产订单
        /// </summary>
        IProductOrder ProductOrder { get; set; }
    }
}
