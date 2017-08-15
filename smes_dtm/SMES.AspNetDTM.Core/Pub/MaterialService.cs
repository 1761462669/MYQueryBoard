using MDF.Framework.Db;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetDTM.Model.Pub;
using SMES.Framework;
using SMES.Framework.EplDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.Core.Pub
{
    public class MaterialService :ServiceBase,IMaterialService
    {
        public List<Material> Query()
        {
            using (IDbSession session = Db.OpenSession())
            {
                List<Material> list= session.Query<Material>().ToList();

                
                return list;
            }
        }


        public System.Data.DataSet QueryDataset()
        {
            Database db = DataAccess.CreateDb("smes");

            string sql = "select * from pub.material";

            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);

            return ds;
        }
    }
}
