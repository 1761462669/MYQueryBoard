using MDF.Framework.Db;
using SMES.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace SMES.FrameworkAdpt.MaterialChooseControl.Service
{
    public class ComMaterialTypeService : IComMaterialTypeService
    {
        [Import]
        public IDataBase Db { get; set; }

        public IList<MaterialTypeModelAdpt> GetMaterialTypes(List<string> typeIds)
        {
            if (typeIds == null || typeIds.Count == 0)
                return null;
            using (IDbSession session = this.Db.OpenSession())
            {
                var list = session.Query<MaterialTypeModelAdpt>().Where(c => c.IsUsed == true && typeIds.Contains(c.Id.ToString())).ToList();

                foreach (var item in list)
                {
                    SetChilds(session, item);
                }

                return list;
            }
        }

        private void SetChilds(IDbSession session, MaterialTypeModelAdpt item)
        {
            var childs = session.Query<MaterialTypeModelAdpt>().Where(c => c.IsUsed == true && c.Parent != null && c.Parent.Id == item.Id).Select(c=>c as IHierarchyModel).ToList();

            foreach (var node in childs)
            {
                SetChilds(session, node as MaterialTypeModelAdpt);
            }
            
            item.Childs = childs;
        }





        public IList GetMaterials(List<string> typeIds)
        {
            if (typeIds == null || typeIds.Count == 0)
                return null;
            List<MaterialModelAdpt> results = new List<MaterialModelAdpt>();

            using (IDbSession session = this.Db.OpenSession())
            {
                var list = GetMaterialTypes(typeIds);
                var listTypeIds = list.Select(c => c.Id).ToList();

                var materials = session.Query<MaterialModelAdpt>().Where(c => c.IsUsed == true && listTypeIds.Contains(c.MaterialType.Id)).ToList();
                results = results.Union(materials).ToList();

                foreach (var item in list)
                {
                    SetMaterialChilds(session, item, results);
                }

                return results;
            }
        }

        private void SetMaterialChilds(IDbSession session, MaterialTypeModelAdpt item,List<MaterialModelAdpt> results)
        {
            var childs = session.Query<MaterialTypeModelAdpt>().Where(c => c.IsUsed == true && c.Parent != null && c.Parent.Id == item.Id).ToList();

            var childTypeIds = childs.Select(c => c.Id).ToList();

            var materials = session.Query<MaterialModelAdpt>().Where(c => c.IsUsed == true && childTypeIds.Contains(c.MaterialType.Id)).ToList();
            foreach (var mat in materials)
            {
                results.Add(mat);
            }

            foreach (var type in childs)
            {
                SetMaterialChilds(session, type, results);
            }
        }
    }
}
