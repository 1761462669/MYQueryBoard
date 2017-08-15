using MES.Framework.IService.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Framework.Utility;
using MES.Framework.IModel.Material;
using MDF.Framework.Db;
using System.ComponentModel.Composition;
using MES.Framework.IModel;

namespace MES.Framework.Service.Material
{
    /// <summary>
    /// 物料类型服务
    /// added by changhl,2015-7-17
    /// </summary>
    public class MaterialTypeService : HierarchyModelService<IMaterialType>, IMaterialTypeService
    {

        protected override IMaterialType Update(IMaterialType obj, IDbSession session)
        {
            var oldParent = session.GetObject<IMaterialType>(obj.Id).Parent;
            if (oldParent != obj.Parent)
                obj.Seq = this.GetMaxSeq(obj, session) + 1;
            return base.Update(obj, session);
        }
    }
}
