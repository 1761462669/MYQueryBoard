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
    public class JBJBProduceEfficiency : ServiceBase, IJBProduceEfficiency
    {
        /// <summary>
        /// 卷包机台运行效率
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public DataTable QueryEfficiencyRun(string strStartTime, string strEndTime, string strCon)
        {
            string sql = string.Format(@"--日班机台产量
                                        with qty as (
	                                        SELECT P.BUSINESSDATE,P.TEAMID,P.TEAMNAME,P.PROCESSUNITID,p.PROCESSUNITNAME,SUM(P.QUANTITY) QUANTITY
	                                        FROM prm.PACKWORKORDEROUTPUT AS p 
	                                        where p.BUSINESSDATE >=CONVERT(varchar(100), '{0}', 111) and p.BUSINESSDATE <CONVERT(varchar(100), dateadd(day,1,'{1}'), 111)
                                            and p.OPERFLAGID in (613677)
	                                        AND p.PROCESSUNITNAME like '%包装机%'   --包装机
	                                        GROUP BY P.BUSINESSDATE,P.TEAMID,P.TEAMNAME,P.PROCESSUNITID,p.PROCESSUNITNAME	
                                        ),
                                        --日班生产时间(min) 排产时间-固定班中餐时间[30min]-固定保养时间[30min]-固定计划停机时间[30min]
                                        TTH AS(
	                                        select C.TEAM_ID,C.TEAM_NAME,C.WORKDATE,(datediff(ss, C.STARTTIME, C.ENDTIME)/60.0-30-30-30) TEAMTIME_M 
	                                        from dps.CALENDAR C
	                                        WHERE C.WORKDATE >=CONVERT(varchar(100), '{0}', 111) and C.WORKDATE <CONVERT(varchar(100), dateadd(day,1,'{1}'), 111)
	                                        AND C.RESOURCEID = 186952 -- 卷包车间
                                        ),
                                        --日班机台运行效率
                                        DTP AS(
	                                        SELECT Q.BUSINESSDATE,Q.TEAMID,Q.TEAMNAME,Q.PROCESSUNITID,Q.PROCESSUNITNAME,
	                                        (case when E.THEORYCAPACITY=0 or T.TEAMTIME_M=0 then 0 else (Q.QUANTITY * 100 * 60/(E.THEORYCAPACITY * T.TEAMTIME_M)) end) PCT 
	                                        FROM qty Q
	                                        inner JOIN pub.EQUIPMENT E   --设备能力
	                                        ON E.ID=Q.PROCESSUNITID
	                                        inner JOIN TTH T 
	                                        ON Q.BUSINESSDATE=T.WORKDATE AND Q.TEAMID=T.TEAM_ID
                                        )

                                        SELECT PROCESSUNITID,PROCESSUNITNAME,Convert(decimal(18,2),AVG(PCT)) PCT FROM DTP 
                                        where  {2}
                                        group by PROCESSUNITID,PROCESSUNITNAME
                                        order by PROCESSUNITNAME", strStartTime, strEndTime, strCon);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 卷包机台台时产能
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public DataTable QueryCapacity(string strStartTime, string strEndTime, string strCon)
        {
            string sql = string.Format(@"--日班机台产量
                                        with qty as (
	                                        SELECT P.BUSINESSDATE,P.TEAMID,P.TEAMNAME,P.PROCESSUNITID,p.PROCESSUNITNAME,SUM(P.QUANTITY) QUANTITY
	                                        FROM prm.PACKWORKORDEROUTPUT AS p 
	                                        where p.BUSINESSDATE >=CONVERT(varchar(100), '{0}', 111) and p.BUSINESSDATE <CONVERT(varchar(100), dateadd(day,1,'{1}'), 111)
                                            and p.OPERFLAGID = 613677
	                                        AND p.PROCESSUNITNAME like '%包装机%'   --包装机
	                                        GROUP BY P.BUSINESSDATE,P.TEAMID,P.TEAMNAME,P.PROCESSUNITID,p.PROCESSUNITNAME	
                                        ),
                                        --日班生产时间(min) 排产时间
                                        TTH AS(
	                                        select C.TEAM_ID,C.TEAM_NAME,C.WORKDATE,datediff(ss, C.STARTTIME, C.ENDTIME)/60.0 TEAMTIME_M 
	                                        from dps.CALENDAR C
	                                        WHERE C.WORKDATE >=CONVERT(varchar(100), '{0}', 111) and C.WORKDATE <CONVERT(varchar(100), dateadd(day,1,'{1}'), 111)
	                                        AND C.RESOURCEID = 186952 
                                        ),
                                        --日班机台运行效率
                                        DTP AS(
	                                        SELECT Q.BUSINESSDATE,Q.TEAMID,Q.TEAMNAME,Q.PROCESSUNITID,Q.PROCESSUNITNAME,
	                                        (case when T.TEAMTIME_M=0 then 0 else (Q.QUANTITY*60/T.TEAMTIME_M) end) PCT 
	                                        FROM qty Q
	                                        inner JOIN TTH T 
	                                        ON Q.BUSINESSDATE=T.WORKDATE AND Q.TEAMID=T.TEAM_ID
                                        )

                                        SELECT PROCESSUNITID,PROCESSUNITNAME,Convert(decimal(18,2),AVG(PCT)) PCT FROM DTP 
                                        where {2}
                                        group by PROCESSUNITID,PROCESSUNITNAME
                                        order by PROCESSUNITNAME", strStartTime, strEndTime, strCon);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
