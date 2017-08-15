using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel.Material
{
   /// <summary>
   /// 物料属性可选项
   /// added by changhl,2015-7-13
   /// </summary>
    public interface IMaterialAttributeValue:IBaseModel
    {
        /// <summary>
        /// 物料属性
        /// </summary>
        IMaterialAttribute MaterialAttribute
        {
            get;
            set;
        }

        
    }
}
