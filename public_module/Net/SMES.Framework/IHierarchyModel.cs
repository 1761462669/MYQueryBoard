using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public interface IHierarchyModel : IDataModel
    {
        /// <summary>
        /// 层级数据（父级）
        /// </summary>
        IHierarchyModel Parent { get; set; }
        /// <summary>
        /// 层级数据结构(子级)
        /// </summary>
        IList<IHierarchyModel> Childs { get; set; }
    }
}
