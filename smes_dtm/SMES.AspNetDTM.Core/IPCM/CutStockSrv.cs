using SMES.AspNetDTM.ICore.IPCM;
using SMES.Framework;
using SMES.Framework.EplDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.Core.IPCM
{
    public class CutStockSrv :ServiceBase, ICutStockSrv
    {
        public System.Data.DataTable QueryCutStockMat(string key)
        {

            string sql = @"SELECT T.*,
(
SELECT COUNT(*) FROM (
SELECT BATCH_NO,MATERIAL_CD FROM V_NBCF_HOST_INV_LIST A GROUP BY A.BATCH_NO,A.MATERIAL_CD
) X WHERE X.MATERIAL_CD=T.MATERIAL_CD) AS LOTCOUNT
 FROM V_NBCF_HOST_INV_STAT T WHERE T.MATERIAL_NM LIKE '%" + key + "%' OR T.WAREHOUSE_CD LIKE '" + key + "'";

            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        public System.Data.DataTable QueryCutStockBatchList(string materialcd)
        {
            string sql = @"SELECT T.BATCH_NO,T.MATERIAL_CD,T.MATERIAL_NM,SUM(T.NET_WEIGHT) AS NET_WEIGHT,COUNT(*) AS TOTAL_BOXES,
(
   SELECT NVL(SUM(NET_WEIGHT),0) FROM V_NBCF_HOST_INV_LIST A WHERE A.CONTAINER_STATUS=1 AND A.BATCH_NO=T.BATCH_NO
) AS TOTAL_AVAIL_QTY,
(
   SELECT NVL(SUM(NET_WEIGHT),0) FROM V_NBCF_HOST_INV_LIST A WHERE A.LOCK_STATUS='L' AND A.BATCH_NO=T.BATCH_NO
) AS LOCK_QTY_KG,
(
   SELECT NVL(COUNT(*),0) FROM V_NBCF_HOST_INV_LIST A WHERE A.CONTAINER_STATUS=1 AND A.BATCH_NO=T.BATCH_NO
) AS AVAIL_BOXS,
(
   SELECT NVL(SUM(NET_WEIGHT),0) FROM V_NBCF_HOST_INV_LIST A WHERE A.LOCK_STATUS='L' AND A.BATCH_NO=T.BATCH_NO
) AS LOCK_BOXS

 FROM V_NBCF_HOST_INV_LIST T WHERE T.MATERIAL_CD='" + materialcd + @"'
GROUP BY T.BATCH_NO,T.MATERIAL_CD,T.MATERIAL_NM";

            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        public System.Data.DataTable QueryBatchLockDetail(string lot)
        {
            string sql = "SELECT  * FROM V_NBCF_HOST_INV_LIST T WHERE T.BATCH_NO='" + lot + "'";

            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
