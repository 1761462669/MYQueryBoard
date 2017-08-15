using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDF.Framework.Db;
using System.ComponentModel.Composition;
using SMES.Framework;
using SMES.FrameworkAdpt.OrgInfo.IModel;

namespace SMES.FrameworkAdpt.OrgInfo.Service
{
    public class OrgInfoService : IOrgInfoService
    {
        [Import]
        public IDataBase Db { get; set; }

        public IList<IHierarchyModel> GetAreas(string parentId)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                var obj = session.Query<IResource>().FirstOrDefault(c => c.IsUsed == true && parentId == c.Id.ToString());

                SetChilds(session, obj);

                return obj.Childs;
            }
        }

        private void SetChilds(IDbSession session, IResource item)
        {
            var childs = session.Query<IResource>().Where(c => c.IsUsed == true && c.Parent != null && c.Parent.Id == item.Id).Select(c => c as IHierarchyModel).ToList();

            foreach (var node in childs)
            {
                SetChilds(session, node as IResource);
            }

            item.Childs = childs;
        }

        public System.Collections.IList GetPersons(string parentId)
        {
            List<PersonModel> results = new List<PersonModel>();

            using (IDbSession session = this.Db.OpenSession())
            {
                var list = GetAreas(parentId);
                var listTypeIds = list.Select(c => c.Id).ToList();

                var materials = session.Query<PersonModel>().Where(c => c.IsUsed == true && listTypeIds.Contains(c.Area.Id)).ToList();
                results = results.Union(materials).ToList();

                foreach (var item in list)
                {
                    SetChilds(session, item, results);
                }

                return results;
            }
        }

        private void SetChilds(IDbSession session, IHierarchyModel item, List<PersonModel> results)
        {
            var childs = session.Query<IResource>().Where(c => c.IsUsed == true && c.Parent != null && c.Parent.Id == item.Id).ToList();

            var childTypeIds = childs.Select(c => c.Id).ToList();

            var materials = session.Query<PersonModel>().Where(c => c.IsUsed == true && childTypeIds.Contains(c.Area.Id)).ToList();
            foreach (var mat in materials)
            {
                results.Add(mat);
            }

            foreach (var type in childs)
            {
                SetChilds(session, type, results);
            }
        }
    }
}
