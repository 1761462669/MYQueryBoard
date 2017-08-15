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
    public class JBCJ_JBProductKB : ServiceBase, IJBCJ_JBProductKB
    {
        /// <summary>
        /// 当前班次班组日产量   MY  
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable Shift_Team_Prdouct()
        {
            string sql = @"SELECT * FROM
(SELECT CR.SHIFT_ID,ST.NAME SHIFTNAME,CR.TEAM_ID,TM.NAME TEAMNAME FROM DPS.CALENDAR CR
LEFT JOIN DPS.SHIFT ST ON CR.SHIFT_ID=ST.ID
LEFT JOIN DPS.TEAM TM ON CR.TEAM_ID=TM.ID
WHERE GETDATE() BETWEEN STARTTIME AND ENDTIME AND RESOURCEID=107550)A,
(SELECT CONVERT(DECIMAL(10,2), SUM(CONVERT(DECIMAL, TOTALPRODUCTION))*20/50000) PRODUCT FROM RUNTIME.LIVE_BZJ)B,
(SELECT CONVERT(DECIMAL(10,2), SUM(PWO.QUANTITY)/5) PlanProduct  FROM DPS.CALENDAR CR
LEFT JOIN DPS.PACKWORKORDER PWO ON CR.WORKDATE=PWO.PLANDATE AND CR.TEAM_ID=TEAMID
WHERE GETDATE() BETWEEN CR.STARTTIME AND CR.ENDTIME AND CR.RESOURCEID=107550 AND PWO.PRODUCTSEGID=119383)C";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 卷包按牌号产量 MY
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable QueryPH(string strStartTime, string strEndTime, string strCon)
        {


            string sql = @"SELECT ML.ID matid,ML.NAME matname,A.planqua,B.daqua FROM 
                            (SELECT PWO.PRODUCTID,CONVERT(DECIMAL(10,2), SUM(PWO.QUANTITY)/5) planqua FROM DPS.PACKWORKORDER PWO
                            WHERE PWO.MONTH=SUBSTRING( CONVERT(VARCHAR, GETDATE(),112),1,6) AND PWO.PRODUCTSEGID=119383
                            GROUP BY PWO.PRODUCTID)A
                            LEFT JOIN PUB.MATERIAL ML ON A.PRODUCTID=ML.ID
                            LEFT JOIN 
                            (SELECT CIG_BRAND,CONVERT(DECIMAL(10,2),SUM(CONVERT(DECIMAL,PRINT_NUM))/5) daqua 
                            FROM ITG.getYHGCPrintNum_back YH
                               WHERE SUBSTRING( RECDATE,1,7)=SUBSTRING( CONVERT(VARCHAR, GETDATE(),23),1,7) AND FACT_CODE='12510701'
                               GROUP BY CIG_BRAND) B ON ML.ID_BACKUP=B.CIG_BRAND";
            //sql = string.Format(sql, strStartTime, strEndTime);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 当班机台产量 MY
        /// </summary>
        /// <returns></returns>
        public DataTable Team_Equ_Product()
        {
            string sql = @"SELECT  SUBSTRING(NAME,1,CHARINDEX('#',NAME)) EQU,CONVERT(DECIMAL(10,2),TOTALPRODUCTION*20/50000) PRODUCT
 FROM RUNTIME.LIVE_BZJ ";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 当月产量、计划量    MY
        /// </summary>
        /// <returns></returns>
        public DataTable Month_Prdouct()
        {
            string sql = @"  SELECT A.planproduct PlanProduct,B.product PRODUCT
                                FROM (
                                select SUM(OM.Quantity) planproduct  
                                from [APS].[DeliveryPlot] DP
                                LEFT JOIN APS.OrderForm OM ON DP.Id=OM.DeliveryPlotId
                                LEFT JOIN PUB.MATERIAL ML ON OM.MaterialId=ML.ID
                                where DP.month =convert(date,dateadd(day,-day(getdate())+1,getdate()))  AND PlanState=1) A,
                                (SELECT CONVERT(DECIMAL(10,2), SUM(PWO.QUANTITY)/5) product 
                                FROM PRM.PACKWORKORDEROUTPUT PWO 
                                LEFT JOIN PUB.EQUIPMENT EP ON PWO.PROCESSUNITID=EP.ID
                                LEFT JOIN PUB.EQUIPMENTMODEL EM ON EP.EQUIPMENTMODELID=EM.ID
                                LEFT JOIN PUB.EQUIPMENTTYPE ET ON EM.EQUIPMENTTYPEID=ET.ID
                                WHERE ISREPORTED=1  AND OPERATEUSERID<>68392
                                AND BUSINESSDATE>=convert(date,dateadd(day,-day(getdate())+1,getdate())) AND BUSINESSDATE <=convert(date,dateadd(day,-day(getdate()),DATEADD(M,1,GETDATE()))) 
                                AND OPERFLAGID=613677 AND ET.ID=73227) B";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
