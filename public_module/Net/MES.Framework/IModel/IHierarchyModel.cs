using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel
{
    /// <summary>
    /// 具有层级关系实体的基类
    /// </summary>
    public interface IHierarchyModel : IBaseModel
    {
        /// <summary>
        /// 父节点
        /// </summary>
        IHierarchyModel Parent { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        IList<IHierarchyModel> Childs { get; set; }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        IHierarchyModel GetRoot();

        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        bool IsLeaf { get;  }

        /// <summary>
        /// 获取所有叶子节点
        /// </summary>
        /// <returns></returns>
        IList<IHierarchyModel> GetLeafs();
    }
}
