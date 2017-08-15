using MDF.Framework.Db;
using MES.Framework.IModel;
using MES.Framework.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Framework.Utility;

namespace MES.Framework.Service
{
    public class HierarchyModelService<T> : BaseModelService<T>, IHierarchyModelService<T> where T : IHierarchyModel
    {


        public IList<T> GetRoots()
        {
            using (var session = this.Db.OpenSession())
            {
                var roots = session.Query<T>().Where(c => c.Parent == null).ToList();
                return roots;
            }
        }

        public IList<T> GetTrees()
        {
            using (var session = this.Db.OpenSession())
            {
                var roots = session.Query<T>().Where(c => c.Parent == null).ToList();
                foreach (var root in roots)
                {
                    root.SetChilds(session);
                }
                return roots;
            }
        }

        protected override int GetMaxSeq(T obj, IDbSession session)
        {
            var query = session.Query<T>().Where(c => c.Parent == obj.Parent);
            return query.MaxSeq(session);
        }


        protected override IQueryable<T> GetQuerySequenceCondition(T modle, IDbSession session)
        {
            var query = session.Query<T>().Where(c => c.Parent == modle.Parent);
            return query;
        }




    }
}
