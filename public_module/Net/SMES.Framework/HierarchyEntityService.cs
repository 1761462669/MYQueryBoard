using System.Collections.Generic;
using System.Linq;
using MDF.Framework.Db;
using MDF.Framework.Bus;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Composition;

namespace SMES.Framework
{
    /// <summary>
    /// 层级实体服务
    /// added by changhl,2014-7-21
    /// </summary>
    public class HierarchyEntityService : IHierarchyEntityService
    {
        [Import]
        public IDataBase Db { get; set; }

        #region add by yinhz.20140725.递归所有子 放到一个集合中。（可以设置是否包含本身）

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransLeafToList<T>() where T : IHierarchyModel
        {
            using (var session = this.Db.OpenSession())
            {
                var entitys = session.Query<T>().Where(e=>e.Parent == null).ToList();

                if (entitys == null
                    || entitys.Count == 0)
                    return null;

                return this.TransLeafToList<T>(entitys);
            }
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransLeafToList<T>(T entity) where T : IHierarchyModel
        {
            return this.TransLeafToList<T>(new List<T>() { entity });
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransLeafToList<T>(IList<T> entitys) where T : IHierarchyModel
        {
            using (var session = this.Db.OpenSession())
            {
                return this.TransLeafToList<T>(entitys, session);
            }
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransToList<T>() where T : IHierarchyModel
        {
            using (var session = this.Db.OpenSession())
            {
                var list = session.Query<T>().Where(h => h.Parent == null).ToList();

                if (list == null
                    || list.Count == 0)
                    return null;

                return this.TransToList<T>(list, true, session);
            }
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransToList<T>(List<T> entityList) where T : IHierarchyModel
        {
            return this.TransToList<T>(entityList, true);
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransToList<T>(List<T> entityList, bool ContainSelf) where T : IHierarchyModel
        {
            using (var session = this.Db.OpenSession())
            {
                List<T> rslList = new List<T>();

                foreach (var entity in entityList)
                {
                    if (ContainSelf)
                        rslList.Add(entity);

                    AddToList<T>(session, entity, rslList);
                }

                return rslList;
            }
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransToList<T>(T entity) where T : IHierarchyModel
        {
            return this.TransToList<T>(new List<T>() { entity }, true);
        }

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> TransToList<T>(T entity, bool ContainSelf) where T : IHierarchyModel
        {
            return this.TransToList<T>(new List<T>() { entity }, ContainSelf);
        }

        #region private method

        private IList<T> TransLeafToList<T>(IList<T> entitys, IDbSession session) where T : IHierarchyModel
        {
            List<T> rslList = new List<T>();

            foreach (var entity in entitys)
            {
                this.AddLeafToList<T>(session, entity, rslList);
            }

            return rslList;
        }

        private IList<T> TransToList<T>(List<T> entityList, bool ContainSelf, IDbSession session) where T : IHierarchyModel
        {
            List<T> rslList = new List<T>();

            foreach (var entity in entityList)
            {
                if (ContainSelf)
                    rslList.Add(entity);

                AddToList<T>(session, entity, rslList);
            }

            return rslList;
        }

        private void AddToList<T>(IDbSession session, IHierarchyModel entity, List<T> rslList) where T : IHierarchyModel
        {
            var list = session.Query<T>().Where(h => h.Parent.Id == entity.Id).ToList();

            if (list == null
                || list.Count == 0)
                return;

            foreach (var centity in list)
            {
                rslList.Add(centity);

                AddToList(session, centity, rslList);
            }
        }

        private void AddLeafToList<T>(IDbSession session, IHierarchyModel entity, List<T> rslList) where T : IHierarchyModel
        {
            var list = session.Query<T>().Where(h => h.Parent.Id == entity.Id).ToList();

            if (list == null
                || list.Count == 0)
                rslList.Add((T)entity);

            foreach (var centity in list)
            {
                AddLeafToList(session, centity, rslList);
            }
        }

        #endregion

        #endregion

        [InfoExchangeAllParas(typeof(JsonKnownTypeInfoConverter))]
        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public T TransToTreeNode<T>(T entity) where T : IHierarchyModel
        {
            using (var session = this.Db.OpenSession())
            {
                this.SetChilds<T>(session, entity);
                return entity;
            }
        }

        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public IList<T> GetRootTrees<T>() where T : IHierarchyModel
        {
            using (var session = Db.OpenSession())
            {
                var list = session.Query<T>().Where(c => c.Parent == null).ToList();
                if (list != null && list.Count > 0)
                    foreach (var item in list)
                       this.SetChilds<T>(session,item);
                return list;
            }
        }

        [InfoExchangeReturn(typeof(JsonKnownTypeInfoConverter))]
        public T GetTreeNode<T>(int id) where T : IHierarchyModel
        {
            using (var session = this.Db.OpenSession())
            {
                var entity = session.GetObject<T>(id);
                return this.TransToTreeNode<T>(entity);

            }
        }

        private void SetChilds<T>(IDbSession session, T entity) where T : IHierarchyModel
        {
            if (entity != null)
            {
                entity.Childs = session.Query<T>()
                    .Where(c => c.Parent.Id == entity.Id).Cast<IHierarchyModel>().ToList();

                if (entity.Childs != null && entity.Childs.Count > 0)
                    foreach (var item in entity.Childs)
                        this.SetChilds<T>(session, (T)item);
            }
        }


        public IList<T> GetRootTreesNoTrans<T>() where T : IHierarchyModel
        {
            using (var session = Db.OpenSession())
            {
                var list = session.Query<T>().Where(c => c.Parent == null).ToList();
                return list;
            }
        }
    }
}
