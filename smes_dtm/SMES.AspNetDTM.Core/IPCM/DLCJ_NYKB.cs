using SMES.AspNetDTM.ICore;
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
    public class DLCJ_NYKB : ServiceBase, IDLCJ_NYKB
    {

        public DataTable DLCJ_NYJS_1()
        {
            string sql = @"select id,name,remark from STD.WORKENERGYAREA where isused=1 order by [NAME] ";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0]; 
        }


        public DataTable DLCJ_WS(string ID)
        {
            string sql = @"select areaid,round(wd_avg,1) wd_avg,round(wd_pass,1) wd_pass,round(wd_max,1) wd_max,round(wd_min,1) wd_min,round(SD_AVG,1) SD_AVG
                            ,round(SD_PASS,1) SD_PASS,round(sd_max,1) sd_max,round(sd_min,1) sd_min,datename(hour,convert(char(20),CleanTime,120)) as time
                            from [RTC].[WSD_Clean_Result]
                            where CleanTime between convert(varchar,getdate(),23) and  convert(varchar,dateadd(day,1,getdate()),23) 
                            and areaid in ('{0}')
                            order by CleanTime ";
            sql = string.Format(sql,ID);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0]; 
        }


        public DataTable DLCJ_NYJS_3()
        {
            string sql = @"exec FLPUB.PRO_NYJS_3 ";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0]; 
        }
    }
}
