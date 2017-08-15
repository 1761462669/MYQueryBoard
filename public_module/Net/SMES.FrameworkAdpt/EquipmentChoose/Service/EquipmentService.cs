using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDF.Framework.Db;
using System.ComponentModel.Composition;
using SMES.Framework;
using SMES.FrameworkAdpt.EquipmentChoose.Service;
using SMES.FrameworkAdpt.EquipmentChoose;

namespace SMES.FrameworkAdpt.OrgInfo.Service
{
    public class EquipmentService : IEquipmentService
    {
        [Import]
        public IDataBase Db { get; set; }

        public IList<EquipmentTypeModelFAdpt> GetTypes(List<string> typeIds)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                return session.Query<EquipmentTypeModelFAdpt>().Where(c => typeIds.Contains(c.Id.ToString())).ToList();
            }
        }

        public System.Collections.IList GetEquipments(List<string> typeIds)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                var equModelModelIds = session.Query<EquipmentModelModelFAdpt>().Where(c => typeIds.Contains(c.EquipmentTypeModel.Id.ToString())).Select(c=>c.Id).ToList();

                var list = session.Query<EquipmentModelFAdpt>().Where(c => equModelModelIds.Contains(c.EquipmentModelModel.Id)).ToList();

                return list;
            }
        }
    }
}
