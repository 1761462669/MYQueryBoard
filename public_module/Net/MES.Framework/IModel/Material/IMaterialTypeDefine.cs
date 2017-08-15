using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Material
{
    /// <summary>
    /// 物料类型定义
    /// </summary>
    public interface IMaterialTypeDefine
    {
        /// <summary>
        /// 主键
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        IMaterialType MaterialType
        {
            get;
            set;
        }
        /// <summary>
        /// 物料属性
        /// </summary>
        IMaterialAttribute MaterialAttribute
        {
            get;
            set;
        }
        /// <summary>
        /// 默认值
        /// </summary>
        string DefaultValue { get; set; }
    }
}
