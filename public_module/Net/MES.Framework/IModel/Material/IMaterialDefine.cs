using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Material
{
    /// <summary>
    /// 物料定义
    /// </summary>
    public interface IMaterialDefine
    {
        /// <summary>
        /// 物料
        /// </summary>
        IMaterial Material
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
        /// 值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        string Display { get; set; }
    }
}
