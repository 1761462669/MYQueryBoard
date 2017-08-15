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
    public class JBCJ_JBQuaKB : ServiceBase, IJBCJ_JBQuaKB
    {
        /// <summary>
        /// 卷包车间机台得分
        /// </summary>
        public DataTable QueryMachineScore()
        {
            string sql = @"SELECT A.EQUID,A.EQUNAME,A.BYRESULT,B.SYRESULT FROM 
            (SELECT WIO.EQUID,CONVERT(VARCHAR, WIO.EQUSEQ)+'#' EQUNAME,CONVERT(decimal(10,2), AVG(ISD.SCORE)) BYRESULT 
            FROM QUA.WORKORDERINSPECTORDERDM WIO
            LEFT JOIN QUA.TASKINSPECTORDERDM TIO ON WIO.ID = TIO.WORKORDERINSPECTORDERID
            LEFT JOIN QUA.INSPECTSAMPLEDM ISD ON TIO.ID=ISD.TASKINSPECTORDERID
            WHERE  TIO.personname IS NOT NULL AND TIO.INSPECTIONTYPEID=713970  AND ISD.SCORE IS NOT NULL
            AND WIO.WORKORDERSTARTTIME>=convert(date,dateadd(day,-day(getdate())+1,getdate()))
            GROUP BY WIO.EQUID,WIO.EQUSEQ)A
            RIGHT JOIN
            (SELECT WIO.EQUID,CONVERT(VARCHAR, WIO.EQUSEQ)+'#' EQUNAME,CONVERT(decimal(10,2), AVG(ISD.SCORE)) SYRESULT 
            FROM QUA.WORKORDERINSPECTORDERDM WIO
            LEFT JOIN QUA.TASKINSPECTORDERDM TIO ON WIO.ID = TIO.WORKORDERINSPECTORDERID
            LEFT JOIN QUA.INSPECTSAMPLEDM ISD ON TIO.ID=ISD.TASKINSPECTORDERID
            WHERE  TIO.personname IS NOT NULL AND TIO.INSPECTIONTYPEID=713970  AND ISD.SCORE IS NOT NULL
            AND WIO.WORKORDERSTARTTIME<convert(date,dateadd(day,-day(getdate())+1,getdate())) AND WIO.WORKORDERSTARTTIME>=DATEADD(M,-1, convert(date,dateadd(day,-day(getdate())+1,getdate())))
            GROUP BY WIO.EQUID,WIO.EQUSEQ)B on A.EQUID=B.EQUID";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
        /// <summary>
        /// 卷包车间班组得分
        /// </summary>
        public DataTable QueryTeamScore()
        {
            //string sql = @"exec FLQUA.PROC_EQU_MONTH_RATE 1,'1,2'";
            string sql = @"select a.PLANDATE, max(case a.TEAMID when 72923 then SCORE end) 'YB', max(case a.TEAMID when 72925 then SCORE end) 'BB',max(case a.TEAMID when 72922 then SCORE end) 'JB'  from (
            SELECT SUBSTRING( CONVERT(VARCHAR,WIO.WORKORDERSTARTTIME,23),3,5) PLANDATE,WIO.TEAMID, CONVERT(decimal(10,2), AVG(ISD.SCORE)) SCORE 
            FROM QUA.WORKORDERINSPECTORDERDM WIO
            LEFT JOIN QUA.TASKINSPECTORDERDM TIO ON WIO.ID 
            = TIO.WORKORDERINSPECTORDERID
            LEFT JOIN QUA.INSPECTSAMPLEDM ISD ON TIO.ID 
            =ISD.TASKINSPECTORDERID
            WHERE  TIO.personname IS NOT NULL AND TIO.INSPECTIONTYPEID=713970  AND ISD.SCORE IS NOT NULL
            AND WIO.WORKORDERSTARTTIME>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,10)
            GROUP BY SUBSTRING( CONVERT(VARCHAR,WIO.WORKORDERSTARTTIME,23),3,5),WIO.TEAMID
            ) a group by a.PLANDATE";
            sql = string.Format(sql);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

    }
}
