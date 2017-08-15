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
    public class JBCJ_JBTotalKB : ServiceBase, IJBCJ_JBTotalKB
    {
        /// <summary>
        /// 当月产量、计划量  MY
        /// </summary>
        /// <returns></returns>
        public DataTable Month_Prdouct()
        {
            string sql = @"SELECT A.planproduct,B.monthproduct,A.planproduct-B.monthproduct noproduct,CONVERT(DECIMAL(10,2), B.monthproduct/A.planproduct*100) monthrate
                            FROM (
                            select SUM(OM.Quantity) planproduct  
                            from [APS].[DeliveryPlot] DP
                            LEFT JOIN APS.OrderForm OM ON DP.Id=OM.DeliveryPlotId
                            LEFT JOIN PUB.MATERIAL ML ON OM.MaterialId=ML.ID
                            where DP.month =convert(date,dateadd(day,-day(getdate())+1,getdate())) AND PlanState=1) A,

                            (SELECT CONVERT(DECIMAL(10,2), SUM(PWO.QUANTITY)/5) monthproduct 
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
        /// <summary>
        /// 当月质量缺陷   MY
        /// </summary>
        /// <returns></returns>
        public DataTable Month_Qua_QX()
        {
            string sql = @"with tmp as(
                                    SELECT  a.PARAMETERNAME PROJECTNAME, SAMPLEQUANTITY qx, convert(float,A.SAMPLEQUANTITY)/convert(float,B.SUMQUANTITY)*100 rate FROM (
                                    SELECT top 10 PG.NAME+'-'+PIO.REMARK+'-'+ PIO.PARAMETERNAME PARAMETERNAME,SUM(SAMPLEQUANTITY) SAMPLEQUANTITY FROM  QUA.TASKINSPECTORDERDM TIO
                                    LEFT JOIN QUA.PARAINSPECTORDERDM PIO ON TIO.ID=PIO.TASKINSPECTORDERID
                                    LEFT JOIN STD.RELPARAMETERGROUPPARAMETER RPG ON PIO.PARAMETERID=RPG.PARAMETERID 
                                    LEFT JOIN STD.PARAMETERGROUP  PG ON RPG.PARAMETERGROUPID=PG.ID 
                                    WHERE  TIO.INSPECTTIME>=convert(date,dateadd(day,-day(getdate()-1),DATEADD(M,-1,GETDATE()))) 
                                    AND TIO.INSPECTTIME<convert(date,dateadd(day,-day(getdate())+1,getdate())) AND  TIO.personname IS NOT NULL AND TIO.INSPECTIONTYPEID=713970 
                                    AND PIO.PARAMETERNAME IS NOT NULL
                                    GROUP BY PG.NAME+'-'+PIO.REMARK+'-'+ PIO.PARAMETERNAME
                                    order by SAMPLEQUANTITY desc
                                    ) A,
                                    (
                                    SELECT  SUM(qx) SUMQUANTITY FROM 
                                    (
                                    select top 10 PARAMETERNAME, sum(SAMPLEQUANTITY) qx from QUA.TASKINSPECTORDERDM TIO
                                    LEFT JOIN QUA.PARAINSPECTORDERDM PIO ON TIO.ID 

 
                                    =PIO.TASKINSPECTORDERID
                                    WHERE  TIO.INSPECTTIME>=convert(date,dateadd(day,-day(getdate()-1),DATEADD(M,-1,GETDATE()))) 
                                    AND TIO.INSPECTTIME<convert(date,dateadd(day,-day(getdate())+1,getdate())) AND  TIO.personname IS NOT NULL AND TIO.INSPECTIONTYPEID=713970 
                                    AND PIO.PARAMETERNAME IS NOT NULL
                                    group by PIO.PARAMETERNAME
                                    order by sum(PIO.SAMPLEQUANTITY) desc
                                    ) b1
                                    ) B)
                                    SELECT
                                    t2.PROJECTNAME
                                    ,t2.QX
                                    --,round(t2.RATE,2) N
                                    ,round(SUM(t1.RATE),2) rate
                                    FROM (SELECT PROJECTNAME,QX, RATE, ROW_NUMBER() OVER(order BY QX,RATE) RK from tmp) t1
                                    ,(SELECT PROJECTNAME,QX, RATE, ROW_NUMBER() OVER(order BY QX,RATE) RK from tmp )  t2
                                    WHERE t1.rk >= t2.rk
                                    group by t2.PROJECTNAME,t2.QX,t2.RATE
                                    ORDER BY SUM(t1.RATE) asc";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable Year_Rate()
        {
            string sql = @"exec FLQUA.PROC_YEAR_RATE 1, '1,2'";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public DataTable Month_Rate()
        {
            string sql = @"exec FLQUA.PROC_MONTH_RATE 1, '1,2'";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public DataTable Day_Rate()
        {
            string sql = @"exec FLQUA.PROC_DAY_RATE 1, '1,2'";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable Equ_Efficiency()
        {
//            string sql = @"SELECT '#'+substring(TagName,4,2) Equ,ROUND(isnull(sum(value),0),2) Efficiency
//                            FROM insqldb.Runtime.DBO.LIVE WHERE TAGNAME IN (
//                            select VAL1 from FLPUB.T_PUB_CODE where CD_type='011001002')
//                            group by TagName";
            string sql = @"SELECT '#'+substring(TagName,4,2) Equ,ROUND(isnull(sum(convert(float,value)),0),2) Efficiency
                            FROM etl.LIVE_EQU_EFF WHERE TAGNAME IN (
                            select VAL1 from FLPUB.T_PUB_CODE where CD_type='011001002')
                            group by TagName    ";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 年度产量对比    MY
        /// </summary>
        /// <returns></returns>
        public DataTable Year_Product()
        {
            string sql = @"SELECT plandate,planqua,CASE WHEN daqua IS NULL THEN 0.00 ELSE daqua END daqua 
                            FROM 
                            (select SUBSTRING(CONVERT(VARCHAR,DP.Month),3,5) plandate,SUM(OM.Quantity) planqua
                            from [APS].[DeliveryPlot] DP
                            LEFT JOIN APS.OrderForm OM ON DP.Id=OM.DeliveryPlotId
                            LEFT JOIN PUB.MATERIAL ML ON OM.MaterialId=ML.ID
                            where  PlanState=1 AND SUBSTRING(CONVERT(VARCHAR,DP.Month),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7)
                            GROUP BY DP.Month) A
                            LEFT JOIN 
                            (SELECT SUBSTRING( RECDATE,3,5) RECDATE,CONVERT(DECIMAL(10,2),SUM(CONVERT(DECIMAL,PRINT_NUM))/5) daqua FROM ITG.getYHGCPrintNum_back
                            WHERE FACT_CODE='12510701'
                            GROUP BY SUBSTRING( RECDATE,3,5)) B ON A.plandate=B.RECDATE";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
