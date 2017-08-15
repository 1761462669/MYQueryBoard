using SMES.AspNetDTM.ICore.ProduceAncestry;
using SMES.Framework;
using SMES.Framework.EplDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.Core.ProduceAncestry
{
    public class CutProduceAncestry : ServiceBase, ICutProduceAncestry
    {
        /// <summary>
        /// 获取批次信息列表
        /// </summary>
        /// <param name="plandate">计划日期</param>
        /// <param name="productkey">关键字</param>
        /// <returns></returns>
        public DataTable QueryCutBatchInfo(DateTime plandate, string productkey)
        {
            string sql = "SELECT * FROM  MUSER.DPS_CUTBATCH T WHERE T.STATEID=73130 AND convert(varchar(10),T.REALSTARTTIME,120)='" + plandate.ToString("yyyy-MM-dd")
                + "' and (t.productname like '%" + productkey + "%' or t.plancode like '%" + productkey + "%')";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取批次信息列表（逆向追踪）
        /// </summary>
        /// <param name="plandate">计划日期</param>
        /// <param name="team">班组</param>
        /// <param name="product">牌号</param>
        /// <param name="stamp">钢印号</param>
        /// <param name="codeone">一号工程码</param>
        /// <param name="type">查询类型（0：按时间，1：按钢印号，2：按一号工程码）</param>
        /// <returns></returns>
        public DataTable QueryPackBatchInfo(DateTime plandate, string team, string product, string stamp, string codeone, int type)
        {
            string sql = string.Empty;
            if (type == 0)
            {//按日期
                sql = string.Format(@"select * from muser.v_pack_track  where plandate='{0}' 
                        and teamname like '%{1}%' and productname like '%{2}%' 
                        order by plandate,productname,plancode,teamid asc", plandate.Date.ToString("yyyy-MM-dd HH:mm:ss"), team, product);
            }
            else if (type == 1)
            {//按钢印号
                sql = string.Format(@"select * from muser.v_pack_track  where stamp='{0}' 
                        order by plandate,productname,plancode,teamid asc", stamp);
            }
            else
            { //按一号工程码
                sql = string.Format(@"select * from muser.v_pack_track  where codeone='{0}' 
                        order by plandate,productname,plancode,teamid asc", codeone);
            }

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取牌号信息列表（逆向追踪）
        /// </summary>
        /// <param name="plandate">计划日期</param>
        /// <returns></returns>
        public DataTable QueryPackMatInfo(DateTime plandate)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct PRODUCTNAME from muser.v_pack_track  where plandate='{0}'
                        order by productname asc", plandate.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取统计信息 （逆向追踪）
        /// </summary>
        /// <param name="plancode">作业号</param>
        /// <param name="teamid">班组id</param>
        /// <param name="plandate">生产日期</param>
        /// <returns></returns>
        public DataTable QueryPackSumInfo(string plancode, string teamid, string plandate)
        {
            string sql = string.Empty;
            sql = string.Format(@"select 
                                (select cast(round(sum(pw.QUANTITY)/5,3) as numeric(20,3)) QTY from prm.PACKWORKORDEROUTPUT pw
                                left join dps.PACKWORKORDER pr on pr.PLANCODE=pw.WORKORDERCODE
                                where pw.workordercode like '{0}%' and pw.OPERFLAGID in (613677,639431)
                                and pr.ProcessSegId in (select processsegid from prm.BATCHOUTPROCESSSEG)
                                and pw.TEAMID={1} and pw.BUSINESSDATE='{2}') PRODUCTQTY,
                                (select cast(round(sum(pe.QUANTITY),3) as numeric(20,3)) QTY from prm.PACKWORKORDERCONSUME pe
                                where pe.typeid=220
                                and pe.WORKORDERCODE like '{0}%' and pe.OPERFLAGID in (613677,639431)
                                and pe.TEAMID={1} and pe.BUSINESSDATE='{2}') COSTQTY,
                                (select cast(round(sum(pm.QUANTITY),3) as numeric(20,3)) QTY from prm.PACKWOREMOVINGMATERIAL pm
                                where pm.WORKORDERCODE like '{0}%' and pm.OPERFLAGID in (613677,639431)
                                and pm.TEAMID={1} and pm.BUSINESSDATE='{2}') FAULTQTY,
                                (SELECT avg(convert(decimal(18,5),jg.SCSCORE)) SCSCORE FROM MESETL.qua.JBCHECKSC_GYC AS jg
                                LEFT JOIN SMESDB.dps.TEAM AS t ON jg.TEAMCD=t.CD
                                WHERE jg.ORDERCD='{0}'
                                AND jg.CREATETIME='{2}'
                                AND t.id={1}) QASCORE", plancode, teamid, plandate);

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);
            
            return ds.Tables[0];

        }

        /// <summary>
        /// 获取卷包设备详细信息
        /// </summary>
        /// <param name="plancode">作业号</param>
        /// <param name="teamid">班组id</param>
        /// <param name="plandate">生产日期</param>
        /// <param name="typeid">消耗类型</param>
        /// <param name="pronm">机台名称</param>
        /// <returns></returns>
        public DataTable QueryEQUInfo(string plancode, string teamid, string plandate,int typeid,string pronm)
        {
            string sql = string.Empty;
            sql = string.Format(@"WITH QT AS
                                    (
                                    SELECT 
                                    (select sum(quantity) QXQTY from prm.PACKWOREMOVINGMATERIAL
                                    where processunitname='{4}'
                                    AND BUSINESSDATE='{2}'
                                    and workordercode like '{0}%'
                                    and TEAMID={1} and OPERFLAGID in (613677,639431)) AS QXQTY,
                                    (select sum(quantity) CCQTY from prm.PACKWORKORDEROUTPUT
                                    where processunitname='{4}'
                                    AND BUSINESSDATE='{2}'
                                    and workordercode like '{0}%'
                                    and TEAMID={1}  and OPERFLAGID in (613677,639431)) AS CCQTY,
                                    (select sum(quantity) XHQTY from prm.PACKWORKORDERCONSUME
                                    where typeid={3}
                                    and processunitname='{4}'
                                    AND BUSINESSDATE='{2}'
                                    and workordercode like '{0}%'
                                    and TEAMID={1}  and OPERFLAGID in (613677,639431)) AS XHQTY
                                    )

                                    SELECT cast(round(CCQTY,3) as numeric(20,3)) AS CCQTY,cast(round(QXQTY,3) as numeric(20,3)) AS QXQTY,
                                    cast(round(XHQTY*10000/CCQTY,3) as numeric(20,3)) AS CHQTY
                                     FROM QT", plancode, teamid, plandate, typeid, pronm);

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
        /// <summary>
        /// 获取工单信息 （逆向追踪）
        /// </summary>
        /// <param name="plancode">作业号</param>
        /// <param name="teamid">班组id</param>
        /// <param name="plandate">生产日期</param>
        /// <returns></returns>
        public DataTable QueryPackWoInfo(string plancode, string teamid, string plandate)
        {
            string sql = string.Empty;
            sql = string.Format(@"with wor as
(
	select a.ID as wsjwo,a.TEAMNAME,pr1.NEXTPLAN as jjjwo,pr2.NEXTPLAN as bzjwo,pr3.NEXTPLAN as fzxjwo from 
	(
	select distinct po.ID,fo.TEAMNAME from prm.FEEDWORKORDEROUTPUT fo  
	left join dps.PACKWORKORDER po on fo.WORKORDERCODE=po.PLANCODE
	where fo.WORKORDERCODE like '{0}%'
	and fo.TEAMID={1}
	and fo.BUSINESSDATE='{2}' 
	) a
	left join DPS.PACKWORKORDERRELATION pr1 on a.ID=pr1.PREPLAN
	left join dps.PACKWORKORDERRELATION pr2 on pr1.NEXTPLAN=pr2.PREPLAN
	left join dps.PACKWORKORDERRELATION pr3 on pr2.NEXTPLAN=pr3.PREPLAN
)

select wor.TEAMNAME,
PR1.PLANCODE AS WSJCODE,PR1.RESOURCEID AS WSJRSID,PR1.RESOURCENAME AS WSJRSNM, 
PR2.PLANCODE AS JJJCODE,PR2.RESOURCEID AS JJJRSID,PR2.RESOURCENAME AS JJJRSNM, 
PR3.PLANCODE AS BZJCODE,PR3.RESOURCEID AS BZJRSID,PR3.RESOURCENAME AS BZJRSNM,
PR4.PLANCODE AS FZXJCODE,PR4.RESOURCEID AS FZXJRSID,PR4.RESOURCENAME AS FZXJRSNM
from wor
left join dps.PACKWORKORDER pr1 on wor.wsjwo=pr1.ID
left join dps.PACKWORKORDER pr2 on wor.jjjwo=pr2.ID
left join dps.PACKWORKORDER pr3 on wor.bzjwo=pr3.ID
left join dps.PACKWORKORDER pr4 on wor.fzxjwo=pr4.ID
order by WSJRSNM,JJJRSNM,FZXJRSNM", plancode, teamid, plandate);

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];

        }

        /// <summary>
        /// 查询喂丝机详细信息
        /// </summary>
        /// <param name="plancode">计划号</param>
        /// <param name="teamid">班组ID</param>
        /// <param name="plandate">生产日期</param>
        /// <param name="feedwo">喂丝机工单</param>
        /// <returns></returns>
        public DataTable QueryFeedWoInfo(string plancode, string teamid, string plandate,string feedwo)
        {

            string sql = string.Empty;
            sql = string.Format(@"with wor as
                                (
	                                select a.ID as wsjwo,a.TEAMNAME,pr1.NEXTPLAN as jjjwo,pr2.NEXTPLAN as bzjwo from 
	                                (
	                                select distinct po.ID,fo.TEAMNAME from prm.FEEDWORKORDEROUTPUT fo  
	                                left join dps.PACKWORKORDER po on fo.WORKORDERCODE=po.PLANCODE
	                                where fo.WORKORDERCODE = '{3}'
	                                and fo.TEAMID={1}
	                                and fo.BUSINESSDATE='{2}' 
	                                ) a
	                                left join DPS.PACKWORKORDERRELATION pr1 on a.ID=pr1.PREPLAN
	                                left join dps.PACKWORKORDERRELATION pr2 on pr1.NEXTPLAN=pr2.PREPLAN
                                ),
                                qty as
                                (
                                select 
                                (
                                select sum(pw.QUANTITY) as PROQTY from prm.PACKWORKORDEROUTPUT pw
                                where pw.WORKORDERCODE in
                                (
                                select PR3.PLANCODE
                                from wor
                                left join dps.PACKWORKORDER pr3 on wor.bzjwo=pr3.ID
                                )
                                and pw.TEAMID={1}
                                and pw.BUSINESSDATE='{2}' and pw.OPERFLAGID in (613677,639431)
                                ) as PROQTY,
                                (
                                select sum(QUANTITY) as XHQTY from prm.PACKWORKORDERCONSUME
                                where typeid=220
                                and TEAMID={1} and BUSINESSDATE='{2}'  and WORKORDERCODE like '{0}%'
                                ) as XHQTY
                                )
                                select MATLOT,qs.XHQTY,qs.CHQTY from prm.FEEDWORKORDEROUTPUT 
                                left join (select XHQTY,(XHQTY*5/PROQTY) CHQTY from qty) as qs on 1=1
                                where WORKORDERCODE='{3}'
                                and TEAMID={1}
                                and BUSINESSDATE='{2}' 
                                order by MATLOT", plancode, teamid, plandate, feedwo);

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 根据批次id获取工单列表
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public DataTable QueryWorkOrderOutPut(string batchid)
        {
            string sql = @"select * from muser.mv_cutworkorderoutput t where t.batchid='" + batchid + "' order by productsequen";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 根据工单id获取工单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetCutWorkOrder(string id)
        {
            string sql = "select * from muser.mv_cutworkorderoutput where WORKORDERID='" + id + "'";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        //根据工单id，出入柜类型，获取出入柜信息
        public DataTable GetMaterialTransferinoutsilo(string woid, int typeid)
        {
            string sql = "SELECT * FROM PRM.MATERIALTRANSFERINOUTSILO T WHERE T.WORKORDERID='" + woid + "' AND T.TYPEID=" + typeid;

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];

        }

        /// <summary>
        /// 根据工单id得到工单消耗信息
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        public DataTable GetCutWorkorderConsume(string woid)
        {
            string sql = "SELECT * FROM MUSER.MV_PRM_CUTWORKORDERCONSUME T WHERE T.WORKORDERID='" + woid + "'";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 根据工单ID获得工单关键参数信息
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        public DataTable GetCutAncestryKeyParamData(string woid)
        {
            //return new CommSQLStrBusiness().GetCommSQLStrValueByType(1, woid);
            return new DataTable();
        }

        /// <summary>
        /// 根据工单号得分情况
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        public DataTable GetCutBatchSorce(string woid)
        {
            string sql = @"SELECT * FROM MUSER.MV_QUA_TASKINSPECTORDERDM T WHERE T.INSPECTSTATEID=107264 AND t.INSPECTIONTYPEID=6
AND T.WORKORDERID='" + woid + "'";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 得到本月同牌号得分
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataTable GetProductMonthCutBatchSorce(string woid)
        {
            string sql = @"SELECT ROUND(ISNULL(AVG(T.WORKORDERSCORE),0),0) AS SCORE FROM  MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN DPS.CUTBATCH A ON T.MONTH=A.MONTH AND T.PRODUCTID=A.PRODUCTID
JOIN DPS.CUTPROCESSWORKORDER B ON B.BATCHID=A.ID AND T.RESOURCEID=B.RESOURCEID
WHERE B.ID='" + woid + "' and T.INSPECTSTATEID=107264 and t.INSPECTIONTYPEID=6";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 得到所有牌号得分
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataTable GetAllProductMonthCutBatchSorce(string woid)
        {
            string sql = @"SELECT ROUND(ISNULL(AVG(T.WORKORDERSCORE),0),0) AS SCORE FROM  MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN DPS.CUTBATCH A ON T.MONTH=A.MONTH
JOIN DPS.CUTPROCESSWORKORDER B ON B.BATCHID=A.ID AND T.RESOURCEID=B.RESOURCEID
WHERE B.ID='" + woid + "' and T.INSPECTSTATEID=107264 and t.INSPECTIONTYPEID=6";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 工单参数检验情况
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        public DataTable GetCutWorkOrderPramQL(string woid)
        {
            string sql = @"SELECT Z.PARAMETERID,Z.PARAMETERNAME,ROUND(AVG(T.WORKORDERSCORE),2) AS SCORE,ROUND(AVG(T.WORKORDERDEDUCTSCORE),2) AS DEDUCTSCORE  FROM MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN QUA.PARAINSPECTORDERDM Z ON T.ID=Z.TASKINSPECTORDERID
WHERE T.INSPECTSTATEID=107264 AND T.INSPECTIONTYPEID=6
AND T.WORKORDERID=''
GROUP BY Z.PARAMETERID,Z.PARAMETERNAME";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        //本月同牌号参数检验
        public DataTable GetCutWorkProductOrderPramQL(string woid)
        {
            string sql = @"SELECT Z.PARAMETERID,Z.PARAMETERNAME,ROUND(AVG(T.WORKORDERSCORE),2) AS SCORE,ROUND(AVG(T.WORKORDERDEDUCTSCORE),2) AS DEDUCTSCORE  FROM MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN QUA.PARAINSPECTORDERDM Z ON T.ID=Z.TASKINSPECTORDERID
JOIN DPS.CUTBATCH A ON T.MONTH=A.MONTH AND T.PRODUCTID=A.PRODUCTID
JOIN DPS.CUTPROCESSWORKORDER B ON B.BATCHID=A.ID AND T.RESOURCEID=B.RESOURCEID
WHERE T.INSPECTSTATEID=107264 AND T.INSPECTIONTYPEID=6
AND B.ID='" + woid + @"'
GROUP BY Z.PARAMETERID,Z.PARAMETERNAME";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        //本月所有牌号参数检验
        public DataTable GetCutWorkOrderAllProductPramQL(string woid)
        {
            string sql = @"SELECT Z.PARAMETERID,Z.PARAMETERNAME,ROUND(AVG(T.WORKORDERSCORE),2) AS SCORE,ROUND(AVG(T.WORKORDERDEDUCTSCORE),2) AS DEDUCTSCORE  FROM MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN QUA.PARAINSPECTORDERDM Z ON T.ID=Z.TASKINSPECTORDERID
JOIN DPS.CUTBATCH A ON T.MONTH=A.MONTH
JOIN DPS.CUTPROCESSWORKORDER B ON B.BATCHID=A.ID AND T.RESOURCEID=B.RESOURCEID
WHERE T.INSPECTSTATEID=107264 AND T.INSPECTIONTYPEID=6
AND B.ID='" + woid + @"'
GROUP BY Z.PARAMETERID,Z.PARAMETERNAME";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        public DataTable GetAllWorkorderPramQL(string woid)
        {
            string sql = @"SELECT 'WORKORDER' AS NAME, Z.PARAMETERID,Z.PARAMETERNAME,ROUND(AVG(T.WORKORDERSCORE),2) AS SCORE,ROUND(AVG(T.WORKORDERDEDUCTSCORE),2) AS DEDUCTSCORE  FROM MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN QUA.PARAINSPECTORDERDM Z ON T.ID=Z.TASKINSPECTORDERID
WHERE T.INSPECTSTATEID=107264 AND T.INSPECTIONTYPEID=6
AND T.WORKORDERID='" + woid + @"'
GROUP BY Z.PARAMETERID,Z.PARAMETERNAME
UNION
--本月同牌号
SELECT 'MONTHPRODUCT' AS NAME, Z.PARAMETERID,Z.PARAMETERNAME,ROUND(AVG(T.WORKORDERSCORE),2) AS SCORE,ROUND(AVG(T.WORKORDERDEDUCTSCORE),2) AS DEDUCTSCORE  FROM MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN QUA.PARAINSPECTORDERDM Z ON T.ID=Z.TASKINSPECTORDERID
JOIN DPS.CUTBATCH A ON T.MONTH=A.MONTH AND T.PRODUCTID=A.PRODUCTID
JOIN DPS.CUTPROCESSWORKORDER B ON B.BATCHID=A.ID AND T.RESOURCEID=B.RESOURCEID
WHERE T.INSPECTSTATEID=107264 AND T.INSPECTIONTYPEID=6
AND B.ID='" + woid + @"'
GROUP BY Z.PARAMETERID,Z.PARAMETERNAME
UNION
--本月所有牌号
SELECT 'MONTHALLPRODUCT' AS NAME, Z.PARAMETERID,Z.PARAMETERNAME,ROUND(AVG(T.WORKORDERSCORE),2) AS SCORE,ROUND(AVG(T.WORKORDERDEDUCTSCORE),2) AS DEDUCTSCORE  FROM MUSER.MV_QUA_TASKINSPECTORDERDM T
JOIN QUA.PARAINSPECTORDERDM Z ON T.ID=Z.TASKINSPECTORDERID
JOIN DPS.CUTBATCH A ON T.MONTH=A.MONTH
JOIN DPS.CUTPROCESSWORKORDER B ON B.BATCHID=A.ID AND T.RESOURCEID=B.RESOURCEID
WHERE T.INSPECTSTATEID=107264 AND T.INSPECTIONTYPEID=6
AND B.ID='" + woid + @"'
GROUP BY Z.PARAMETERID,Z.PARAMETERNAME";

            DataSet ds = DataAccess.CreateDb().ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
