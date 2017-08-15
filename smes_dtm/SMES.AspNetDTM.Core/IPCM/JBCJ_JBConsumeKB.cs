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
    //MY
    public class JBCJ_JBConsumeKB : ServiceBase, IJBCJ_JBConsumeKB
    {
        

        public System.Data.DataTable Query_JYZConsume()
        {
            string sql = @"EXEC [dbo].[PD_PACKCONSUME]  73560";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0]; 
        }
        /// <summary>
        /// 嘴棒
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable Query_ZBConsume()
        {
            string sql = @"EXEC [dbo].[PD_PACKCONSUME]  73568";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
        /// <summary>
        /// 小盒
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable Query_XiaoHConsume()
        {
            string sql = @"EXEC [dbo].[PD_PACKCONSUME]  73582";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public System.Data.DataTable Query_TiaoHConsume()
        {
            string sql = @"EXEC [dbo].[PD_PACKCONSUME]  73585";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
