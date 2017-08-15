using SMES.AspNetDTM.ICore.IPCM;
using SMES.Com.ExternalData.RealData;
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
    public class StockSrv : ServiceBase, IStockSrv
    {
        public System.Data.DataTable QueryStoreInfoByType(int typeid)
        {
            string sql = @"SELECT * FROM MUSER.STOREEQU WHERE EQUTYPEID=" + typeid;

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);
            //DataTable dt = ds.Tables[0];

            DataTable dtreal;
            using (IFIXHistorian ifix = new IFIXHistorian())
            {
                
                dtreal = ifix.ReplaceTableCellValue(ds.Tables[0], "QTY", "INQTY","INSTARTTIME", "INENDTIME", "OUTSTARTTIME", 
                    "OUTENDTIME", "STORETIME", "PRODUCTCODE", 
                    "LOT", "MODULE", "STATE", "FLAG","INSTARTTIMEY","INSTARTTIMEM","INSTARTTIMED","INSTARTTIMEH",
                    "INSTARTTIMEI","INSTARTTIMES","INENDTIMEY","INENDTIMEM","INENDTIMED","INENDTIMEH","INENDTIMEI",
                    "INENDTIMES","OUTSTARTTIMEY","OUTSTARTTIMEM","OUTSTARTTIMED","OUTSTARTTIMEH","OUTSTARTTIMEI","OUTSTARTTIMES",
                    "OUTENDTIMEY","OUTENDTIMEM","OUTENDTIMED","OUTENDTIMEH","OUTENDTIMEI","OUTENDTIMES",
                    "QTYPERCENT");
            }

            return dtreal;
        }

        public System.Data.DataTable QueryStoreType()
        {
            string sql = @"SELECT * FROM MUSER.STORETYPE ORDER BY ID";

            DataSet ds =DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
