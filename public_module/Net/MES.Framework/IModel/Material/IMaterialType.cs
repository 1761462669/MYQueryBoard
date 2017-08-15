using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Material
{
    /// <summary>
    /// 物料类型
    /// </summary>
    public interface IMaterialType : IHierarchyModel
    {
        IMaterialType ParentType { get; }
    }


}
