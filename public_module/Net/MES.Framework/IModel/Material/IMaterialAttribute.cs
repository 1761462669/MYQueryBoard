using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.Framework.IModel.Material
{
    /// <summary>
    /// 物料属性
    /// </summary>
    public interface IMaterialAttribute : IBaseModel
    {
        /// <summary>
        /// 是否有可选项
        /// </summary>
        bool IsOptionalValue { get; set; }
    }
}
