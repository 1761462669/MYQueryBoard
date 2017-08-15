using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using SMES.AspNetDTM.ICore.IPCM;
using SMES.AspNetDTM.Model.Pub;
using MDF.Framework.Db;
using SMES.Framework;

namespace SMES.AspNetDTM.Core.IPCM
{
    public class TsStockService : ServiceBase, ITsStockService
    {
        public class EquType
        {
            public string EquID { get; set; }
            public string Typeid { get; set; }

        }

        public IDataBase InsqlDb { get; set; }

        public DataTable GetSiloInfos()
        {
            using (IDbSession session = this.InsqlDb.OpenSession())
            {
                //SQL
                string sql = @" SELECT *  FROM [dbo].[MV_SQL_SiloInformation] ";
                //执行查询

                DataSet ds = session.CreateSQLQuery(sql).DataSet();

                //查询结果为空，则直接返回空值
                if (ds == null || ds.Tables.Count == 0) return null;

                return ds.Tables[0];
            }
        }

        public DataTable GetCutSiloInfo()
        {
            using (IDbSession session = this.InsqlDb.OpenSession())
            {
                //SQL
                string sql = @" SELECT *  FROM [dbo].[MV_SQL_CutSiloInformation] ";
                //执行查询

                DataSet ds = session.CreateSQLQuery(sql).DataSet();

                //查询结果为空，则直接返回空值
                if (ds == null || ds.Tables.Count == 0) return null;

                return ds.Tables[0];
            }
        }

        public DataTable GetSoliListByTypeName(string Name)
        {
            using (IDbSession session = this.InsqlDb.OpenSession())
            {
                //SQL
                string sql = @" SELECT *  FROM [dbo].[MV_SQL_SiloInformation] where  EQU_SHORTNAME like '%" + Name + "%' ";
                //执行查询

                DataSet ds = session.CreateSQLQuery(sql).DataSet();

                //查询结果为空，则直接返回空值
                if (ds == null || ds.Tables.Count == 0) return null;

                return ds.Tables[0];
            }
        }


        public DataTable QueryStoreInfoByType(int typeid)
        {
            using (IDbSession session = this.InsqlDb.OpenSession())
            {
                //SQL
                string sql = @"SELECT EQU_ID,CHLL,CLBFB,ZGZT,ZGZTMC,CLL,JLL,WLMC,WLBM,PCH,EQU_SHORTNAME,JGSJ,CGSJ,EQUTYEPID,
                               EQUTYEPNAME,MAXSTORE,NEXTEQUID FROM [dbo].[MV_SQL_SiloInformation] where  EQUTYEPID=" + typeid.ToString();
                //执行查询

                DataSet ds = session.CreateSQLQuery(sql).DataSet();

                //查询结果为空，则直接返回空值
                if (ds == null || ds.Tables.Count == 0) return null;

                return ds.Tables[0];
            }
        }

        public DataTable QueryStoreType()
        {
            //InsqlDb = MDF.Framework.Bus.ObjectFactory.GetObject<IDataBase>("RTSDB");
            using (IDbSession session=this.InsqlDb.OpenSession())
            {
                string sql = @"SELECT id ID,name NAME,remark REMARK,isused ISUSED,area AREA FROM dbo.PUB_EQUTYPE order by id";

                DataSet ds = session.CreateSQLQuery(sql).DataSet();

                return ds.Tables[0];
            }
        }
    }
}
