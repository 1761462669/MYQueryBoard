using MDF.Framework.Db;
using SMES.FrameworkAdpt.MeasureControl.Model.MeasureModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.MeasureControl.Service
{
    public class MeasureService : IMeasureService
    {
        [Import]
        public IDataBase Db { get; set; }

        public System.Collections.IList GetTypes(List<string> typeIds)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                if (typeIds == null || typeIds.Count == 0)
                {
                    return session.Query<MeasureTypeModelFAdpt>().Where(c => c.IsUsed == true).ToList();
                }

                return session.Query<MeasureTypeModelFAdpt>().Where(c => c.IsUsed == true && typeIds.Contains(c.Id.ToString())).ToList();
            }
        }

        public System.Collections.IList GetMeasures(List<string> typeIds)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                var list = session.Query<MeasureModelFAdpt>().Where(c =>c.IsUsed == true && typeIds.Contains(c.MeasureType.Id.ToString())).ToList();

                return list;
            }
        }
    }
}
