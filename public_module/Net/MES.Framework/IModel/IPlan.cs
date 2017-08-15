using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel
{
    /// <summary>
    /// 计划
    /// </summary>
    public interface IPlan : ITimeSegment, ISequence
    {
        /// <summary>
        /// 主键
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 资源Id
        /// </summary>
        string ResourceId { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        string ResourceName { get; set; }

        /// <summary>
        /// 计划编码
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        string ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// 计划量
        /// </summary>
        decimal Qua { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        string UnitId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        string UnitName { get; set; }

        /// <summary>
        /// 状态Id
        /// </summary>
        string StateId { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        string StateName { get; set; }



    }
}
