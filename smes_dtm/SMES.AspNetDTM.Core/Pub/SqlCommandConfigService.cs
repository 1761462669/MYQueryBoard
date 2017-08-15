using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MDF.Framework.Db;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetDTM.Model.Pub;
using SMES.Framework;
using SMES.Framework.EplDb;


namespace SMES.Com.EnterpriseBoard.Core.Pub
{
    public class SqlCommandConfigService:EntityServiceBase<SqlCommandConfigModel>,ISqlCommandConfigService
    {
        private SQLParaCollection SqlPC = new SQLParaCollection();
        private static readonly string placeholder = ":";

        public int Query(string sqlcode,List<SQLParameter> paralist, out DataSet ds)
        {
            IList<SqlCommandConfigModel> query = null;
            ds =null;
            using (IDbSession session = Db.OpenSession())
            {
                if (!string.IsNullOrEmpty(sqlcode))
                    query = session.Query<SqlCommandConfigModel>().Where(p => p.Code == sqlcode).ToList();
            }
            if (query.Count == 0)
                return -1;

            ds = QueryByConfig(query[0],paralist);

            if (ds == null || ds.Tables.Count == 0)
                return 0;
            else
                return ds.Tables[0].Rows.Count;

        }

        public int QueryById(string Id,List<SQLParameter> paralist, out DataSet ds)
        {           
            SqlCommandConfigModel query;
            using (IDbSession session = Db.OpenSession())
            {
                query = session.GetObject<SqlCommandConfigModel>(Id);
            }

            ds = QueryByConfig(query,paralist);

            if (ds == null || ds.Tables.Count == 0)
                return 0;
            else
                return ds.Tables[0].Rows.Count;
        }

        private DataSet QueryByConfig(SqlCommandConfigModel query,List<SQLParameter> pars)
        {
            if (query == null)
                throw new Exception("SQL查询配置不能为空");

            if (string.IsNullOrEmpty(query.Script))
                throw new Exception("查询配置中SQL脚本不能为空");

            Database db;

            if (string.IsNullOrEmpty(query.StrCon))
                db = DataAccess.CreateDb();
            else
                db = DataAccess.CreateDb(query.StrCon);

            SQLParaCollection sqlcollection = new SQLParaCollection();
            sqlcollection.AddRange(pars);

            DataSet ds = db.Query(query.Script, sqlcollection);

            return ds;

        }

        public int QuerySqlCommand(string SqlCmd, out DataSet ds)
        {
            Database db;

            db = DataAccess.CreateDb();

            ds = db.Query(SqlCmd);

            if (ds == null || ds.Tables.Count == 0)
                return 0;
            else
                return ds.Tables[0].Rows.Count;
        }
    }
}
