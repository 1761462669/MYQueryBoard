using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IService
{
    public interface IHierarchyModelService<T> : IBaseModelService<T> where T:IHierarchyModel
    {
        /// <summary>
        /// 获取所有根节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetRoots() ;
        /// <summary>
        /// 获取所有树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetTrees();
    }
}
