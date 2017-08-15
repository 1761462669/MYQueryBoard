using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDF.Framework.Db;
using System.ComponentModel.Composition;

namespace SMES.Framework
{
    [InheritedExport]
    public interface IHierarchyEntityService
    {
        #region add by yinhz.20140725

        IList<T> TransLeafToList<T>() where T : IHierarchyModel;

        IList<T> TransLeafToList<T>(T entity) where T : IHierarchyModel;

        IList<T> TransLeafToList<T>(IList<T> entitys) where T : IHierarchyModel;

        IList<T> TransToList<T>() where T : IHierarchyModel;

        IList<T> TransToList<T>(List<T> entityList) where T : IHierarchyModel;

        IList<T> TransToList<T>(List<T> entityList, bool ContainSelf) where T : IHierarchyModel;

        IList<T> TransToList<T>(T entity) where T : IHierarchyModel;

        IList<T> TransToList<T>(T entity, bool ContainSelf) where T : IHierarchyModel;

        #endregion

        T TransToTreeNode<T>(T entity) where T : IHierarchyModel;
        IList<T> GetRootTrees<T>() where T : IHierarchyModel;
        T GetTreeNode<T>(int id) where T : IHierarchyModel;
        IList<T> GetRootTreesNoTrans<T>() where T : IHierarchyModel;
    }
}
