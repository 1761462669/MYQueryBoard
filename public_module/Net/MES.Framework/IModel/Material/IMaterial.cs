using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Material
{
    /// <summary>
    /// 物料
    /// </summary>
    public interface IMaterial:IBaseModel
    {
        IMaterialType MaterialType
        {
            get;
            set;
        }
    }
}
