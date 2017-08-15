using MES.Framework.IModel.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Model.Material
{
    /// <summary>
    /// 物料类型定义
    /// </summary>
    public class MaterialTypeModel : HierarchyModel, IMaterialType
    {
        /// <summary>
        /// 物料类型
        /// </summary>
        public IMaterialType ParentType
        {
            get { return this.Parent as IMaterialType; }
            set { this.Parent = value; }
        }
    }
}
