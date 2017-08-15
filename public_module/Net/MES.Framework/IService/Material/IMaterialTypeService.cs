using MES.Framework.IModel.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IService.Material
{
    /// <summary>
    /// 物料类型服务
    /// </summary>
    [InheritedExport]
    public interface IMaterialTypeService:IHierarchyModelService<IMaterialType>
    {
       


    }
}
