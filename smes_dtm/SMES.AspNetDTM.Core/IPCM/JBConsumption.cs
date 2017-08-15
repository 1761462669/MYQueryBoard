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
    public class JBConsumption : ServiceBase, IJBConsumption
    {
        public System.Data.DataTable QueryJBConsumptionByMat(string strDate, string endDate)
        {

            string sql = string.Format(@"
                            --牌号产量
                            with qty as (
	                            SELECT p.MATERIALID,sum(p.QUANTITY) QUANTITY
                                FROM prm.PACKWORKORDEROUTPUT p 
                                left join pub.EQUIPMENT e on p.PROCESSUNITID=e.id 
                                where convert(char(10), p.BUSINESSDATE, 120) >= '{0}' and convert(char(10), p.BUSINESSDATE, 120) <= '{1}' and p.OPERFLAGID in (613677,639431)
                                  and e.PARENTRESOURCEID in (681773, 683063)  --包装机和手工包装
                                group by MATERIALID
                            ),
                            --牌号消耗(卷烟纸)
                            costJYZ as (
                                select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                                (
                                  select * from prm.PACKWORKORDERCONSUME p where EXISTS(select 1 from pub.MATERIAL m1 where p.materialid=m1.id and  m1.materialtypeid in 
                                  (select id from pub.materialtype mt where mt.parentid = 171399 or mt.id = 171399) and m1.isused = 1)
                                ) p
                                inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                                where convert(char(10), p.BUSINESSDATE, 120) >= '{0}' and convert(char(10), p.BUSINESSDATE, 120) <= '{1}' and p.OPERFLAGID in (613677,639431)
                                group by po.PRODUCTID
                            ),
                            --牌号消耗(嘴棒)
                            costZB as (
	                            select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                                (
                                  select * from prm.PACKWORKORDERCONSUME p where EXISTS ( select 1 from pub.MATERIAL m1 where p.materialid=m1.id and  m1.materialtypeid in 
                                  (select id from pub.materialtype mt where mt.parentid = 171906 or mt.id = 171906) and m1.isused = 1)
                                ) p
                                inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                                where convert(char(10), p.BUSINESSDATE, 120) >= '{0}' and convert(char(10), p.BUSINESSDATE, 120) <= '{1}' and p.OPERFLAGID in (613677,639431)
                                group by po.PRODUCTID
                            ),
                            --牌号消耗(小盒)
                            costXH as (
	                            select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                              (
                                select * from prm.PACKWORKORDERCONSUME p where EXISTS (select 1 from pub.MATERIAL m1 where p.materialid=m1.id and  m1.materialtypeid in 
                                (select id from pub.materialtype mt where mt.parentid = 1026769 or mt.id = 1026769) and m1.isused = 1)
                              ) p
                              inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                              where convert(char(10), p.BUSINESSDATE, 120) >= '{0}' and convert(char(10), p.BUSINESSDATE, 120) <= '{1}' and p.OPERFLAGID in (613677,639431)
                              group by po.PRODUCTID
                            ),
                            --牌号消耗(条盒)
                            costTH as (
	                            select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                              (
                                select * from prm.PACKWORKORDERCONSUME p where EXISTS ( select 1 from pub.MATERIAL m1 where p.materialid=m1.id and  m1.materialtypeid in 
                                (select id from pub.materialtype mt where mt.parentid = 1026770 or mt.id = 1026770) and m1.isused = 1)
                              ) p
                              inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                              where convert(char(10), p.BUSINESSDATE, 120) >= '{0}' and convert(char(10), p.BUSINESSDATE, 120) <= '{1}' and p.OPERFLAGID in (613677,639431)
                              group by po.PRODUCTID
                            ),
                            --产耗(卷烟纸)
                            consumJYZ as (
	                            select c.PRODUCTID,round((c.QUANTITY/q.QUANTITY), 2) as Consum,m.NAME 
                              from qty q inner join costJYZ c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --产耗(嘴棒)
                            consumZB as (
	                            select c.PRODUCTID,round((c.QUANTITY/q.QUANTITY), 2) as Consum,m.NAME  
                              from qty q inner join costZB c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --产耗(小盒)
                            consumXH as (
	                            select c.PRODUCTID,round((c.QUANTITY/q.QUANTITY) * 10000, 2) as Consum,m.NAME  
                              from qty q inner join costXH c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --产耗(条盒)
                            consumTH as (
	                            select c.PRODUCTID,round((c.QUANTITY/q.QUANTITY) * 10000, 2) as Consum,m.NAME  
                              from qty q inner join costTH c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --卷烟纸-前5
                            JYZQ5 as
                            (
	                            select   ROW_NUMBER() OVER( order by jyzQ.Consum asc) as rownum,jyzQ.NAME as MatJYZQ,jyzQ.Consum as ConsumptionJYZQ from
	                            (
		                            select top 5 * from consumJYZ jyz order by jyz.Consum asc
	                            ) as jyzQ 
                            )
                            ,
                            --卷烟纸-后5
                            JYZH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by jyzH.Consum desc) as rownum,jyzH.NAME as MatJYZH,jyzH.Consum as ConsumptionJYZH from
	                            (
		                            select top 5 * from consumJYZ jyz order by jyz.Consum desc
	                            ) as jyzH
                            ),
                            --嘴棒-前5
                            ZBQ5 as
                            (
	                            select  ROW_NUMBER() OVER( order by zbQ.Consum asc) as rownum,zbQ.NAME as MatZBQ,zbQ.Consum as ConsumptionZBQ from
	                            (
		                            select top 5 * from consumZB zb order by zb.Consum asc
	                            ) as zbQ
                            ),
                            --嘴棒-后5
                            ZBH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by zbH.Consum desc) as rownum,zbH.NAME as MatZBH,zbH.Consum as ConsumptionZBH from
	                            (
		                            select top 5 * from consumZB zb order by zb.Consum desc
	                            ) as zbH
                            ),
                            --小盒-前5
                            XHQ5 as
                            (
	                            select  ROW_NUMBER() OVER( order by XHQ.Consum asc) as rownum,XHQ.NAME as MatXHQ,XHQ.Consum as ConsumptionXHQ from
	                            (
		                            select top 5 * from consumXH zb order by zb.Consum asc
	                            ) as XHQ
                            ),
                            --小盒-后5
                            XHH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by XHH.Consum desc) as rownum,XHH.NAME as MatXHH,XHH.Consum as ConsumptionXHH from
	                            (
		                            select top 5 * from consumXH zb order by zb.Consum desc
	                            ) as XHH
                            ),
                            --条盒-前5
                            THQ5 as
                            (
	                            select  ROW_NUMBER() OVER( order by THQ.Consum asc) as rownum,THQ.NAME as MatTHQ,THQ.Consum as ConsumptionTHQ from
	                            (
		                            select top 5 * from consumTH zb order by zb.Consum asc
	                            ) as THQ
                            ),
                            --条盒-后5
                            THH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by THH.Consum desc) as rownum,THH.NAME as MatTHH,THH.Consum as ConsumptionTHH from
	                            (
		                            select top 5 * from consumTH zb order by zb.Consum desc
	                            ) as THH
                            )

                            --当日卷包消耗（按牌号）统计结果集合
                            SELECT *
                            FROM
                            (
	                            SELECT 1 AS rownum UNION SELECT 2 AS rownum UNION SELECT 3 AS rownum UNION SELECT 4 AS rownum UNION SELECT 5 AS rownum
                            ) AS RN
                            LEFT JOIN JYZQ5 ON RN.rownum=JYZQ5.rownum
                            LEFT JOIN JYZH5 ON RN.rownum=JYZH5.rownum
                            LEFT JOIN ZBQ5 ON RN.rownum=ZBQ5.rownum
                            LEFT JOIN ZBH5 ON RN.rownum=ZBH5.rownum
                            LEFT JOIN XHQ5 ON RN.rownum=XHQ5.rownum
                            LEFT JOIN XHH5 ON RN.rownum=XHH5.rownum
                            LEFT JOIN THQ5 ON RN.rownum=THQ5.rownum
                            LEFT JOIN THH5 ON RN.rownum=THH5.rownum
                       ", strDate, endDate);

            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }


        public DataTable QueryJBConsumptionByMatAndShift(string strDate, string ShiftID)
        {

            string sql = string.Format(@"
                            --牌号产量
                            with qty as (
								SELECT p.MATERIALID,sum(p.QUANTITY) QUANTITY 
	                            FROM prm.PACKWORKORDEROUTPUT p 
	                            where convert(char(10), p.BUSINESSDATE,120) = '{0}' and p.OPERFLAGID in (613677,639431)
                                and p.SHIFTID={1}
	                            group by MATERIALID
                            ),
                            --牌号消耗(卷烟纸)
                            costJYZ as (
								select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
	                            (
		                            select * from prm.PACKWORKORDERCONSUME p where EXISTS(select 1 from pub.MATERIAL m1 where p.materialid=m1.id and m1.MATERIALTYPEID in
                                (select id from pub.materialtype mt where mt.parentid = 171399 or mt.id = 171399) and m1.isused = 1)
                                 and p.SHIFTID={1}
	                            ) p
	                            inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
	                            where convert(char(10), p.BUSINESSDATE,120) = '{0}' and p.OPERFLAGID in (613677,639431)
	                            group by po.PRODUCTID
                            ),
                            --牌号消耗(嘴棒)
                            costZB as (
	                            select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                              (
                                select * from prm.PACKWORKORDERCONSUME p where EXISTS (select 1 from pub.MATERIAL m1 where p.materialid=m1.id and m1.materialtypeid in 
                                  (select id from pub.materialtype mt where mt.parentid = 171906 or mt.id =171906) and m1.isused = 1)
                                    and p.SHIFTID={1}
                              ) p
                              inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                              where convert(char(10),p.BUSINESSDATE,120) = '{0}' and p.OPERFLAGID in (613677,639431)
                              group by po.PRODUCTID
                            ),
                            --牌号消耗(小盒)
                            costXH as (
	                            select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                              (
                                select * from prm.PACKWORKORDERCONSUME p where EXISTS (select 1 from pub.MATERIAL m1 where p.materialid=m1.id and m1.materialtypeid in 
                                (select id from pub.materialtype mt where mt.parentid = 1026769 or mt.id = 1026769) and m1.isused = 1)
                                    and p.SHIFTID={1}
                              ) p
                              inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                              where convert(char(10),p.BUSINESSDATE,120) = '{0}' and p.OPERFLAGID in (613677,639431)
                              group by po.PRODUCTID
                            ),
                            --牌号消耗(条盒)
                            costTH as (
	                            select po.PRODUCTID,sum(p.QUANTITY) QUANTITY FROM
                              (
                                select * from prm.PACKWORKORDERCONSUME p where EXISTS (select 1 from pub.MATERIAL m1 where p.materialid=m1.id and m1.materialtypeid in 
                                (select id from pub.materialtype mt where mt.parentid = 1026770 or mt.id =1026770) and m1.isused = 1)
                                    and p.SHIFTID={1}
                              ) p
                              inner join dps.PACKWORKORDER po on p.WORKORDERID=po.ID
                              where convert(char(10),p.BUSINESSDATE,120) = '{0}' and p.OPERFLAGID in (613677,639431)
                              group by po.PRODUCTID
                            ),
                            --产耗(卷烟纸)
                            consumJYZ as (
	                            select c.PRODUCTID, round((c.QUANTITY/q.QUANTITY), 2) as Consum, m.NAME 
                              from qty q inner join costJYZ c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --产耗(嘴棒)
                            consumZB as (
	                            select c.PRODUCTID, round((c.QUANTITY * 10000/q.QUANTITY), 2) as Consum, m.NAME  
                              from qty q inner join costZB c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --产耗(小盒)
                            consumXH as (
	                            select c.PRODUCTID, round((c.QUANTITY * 10000/q.QUANTITY), 2) as Consum, m.NAME  
                              from qty q inner join costXH c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --产耗(条盒)
                            consumTH as (
	                            select c.PRODUCTID, round((c.QUANTITY * 10000/q.QUANTITY), 2) as Consum, m.NAME  
                              from qty q inner join costTH c
                              on q.MATERIALID=c.PRODUCTID
                              left join pub.MATERIAL m on m.ID=c.PRODUCTID
                            ),
                            --卷烟纸-前5
                            JYZQ5 as
                            (
	                            select   ROW_NUMBER() OVER( order by jyzQ.Consum asc) as rownum,jyzQ.NAME as MatJYZQ,jyzQ.Consum as ConsumptionJYZQ from
	                            (
		                            select top 5 * from consumJYZ jyz order by jyz.Consum asc
	                            ) as jyzQ 
                            )
                            ,
                            --卷烟纸-后5
                            JYZH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by jyzH.Consum desc) as rownum,jyzH.NAME as MatJYZH,jyzH.Consum as ConsumptionJYZH from
	                            (
		                            select top 5 * from consumJYZ jyz order by jyz.Consum desc
	                            ) as jyzH
                            ),
                            --嘴棒-前5
                            ZBQ5 as
                            (
	                            select  ROW_NUMBER() OVER( order by zbQ.Consum asc) as rownum,zbQ.NAME as MatZBQ,zbQ.Consum as ConsumptionZBQ from
	                            (
		                            select top 5 * from consumZB zb order by zb.Consum asc
	                            ) as zbQ
                            ),
                            --嘴棒-后5
                            ZBH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by zbH.Consum DESC) as rownum,zbH.NAME as MatZBH,zbH.Consum as ConsumptionZBH from
	                            (
		                            select top 5 * from consumZB zb order by zb.Consum DESC
	                            ) as zbH
                            ),
                            --小盒-前5
                            XHQ5 as
                            (
	                            select  ROW_NUMBER() OVER( order by XHQ.Consum asc) as rownum,XHQ.NAME as MatXHQ,XHQ.Consum as ConsumptionXHQ from
	                            (
		                            select top 5 * from consumXH zb order by zb.Consum asc
	                            ) as XHQ
                            ),
                            --小盒-后5
                            XHH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by XHH.Consum DESC) as rownum,XHH.NAME as MatXHH,XHH.Consum as ConsumptionXHH from
	                            (
		                            select top 5 * from consumXH zb order by zb.Consum DESC
	                            ) as XHH
                            ),
                            --条盒-前5
                            THQ5 as
                            (
	                            select  ROW_NUMBER() OVER( order by THQ.Consum asc) as rownum,THQ.NAME as MatTHQ,THQ.Consum as ConsumptionTHQ from
	                            (
		                            select top 5 * from consumTH zb order by zb.Consum asc
	                            ) as THQ
                            ),
                            --条盒-后5
                            THH5 as
                            (
	                            select  ROW_NUMBER() OVER( order by THH.Consum DESC) as rownum,THH.NAME as MatTHH,THH.Consum as ConsumptionTHH from
	                            (
		                            select top 5 * from consumTH zb order by zb.Consum DESC
	                            ) as THH
                            )

                            --当日卷包消耗（按牌号）统计结果集合
                            SELECT *
                            FROM
                            (
	                            SELECT 1 AS rownum UNION SELECT 2 AS rownum UNION SELECT 3 AS rownum UNION SELECT 4 AS rownum UNION SELECT 5 AS rownum
                            ) AS RN
                            LEFT JOIN JYZQ5 ON RN.rownum=JYZQ5.rownum
                            LEFT JOIN JYZH5 ON RN.rownum=JYZH5.rownum
                            LEFT JOIN ZBQ5 ON RN.rownum=ZBQ5.rownum
                            LEFT JOIN ZBH5 ON RN.rownum=ZBH5.rownum
                            LEFT JOIN XHQ5 ON RN.rownum=XHQ5.rownum
                            LEFT JOIN XHH5 ON RN.rownum=XHH5.rownum
                            LEFT JOIN THQ5 ON RN.rownum=THQ5.rownum
                            LEFT JOIN THH5 ON RN.rownum=THH5.rownum
                       ", strDate,ShiftID);

            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        public DataTable QueryJBConsumptionByModel(string strdate, string enddate, string strCostType, string UnitConversion)
        {
            string sql = string.Empty;
            sql = string.Format(@"--机台产量
                                        with qty as (
                                          SELECT p.processunitid,sum(p.QUANTITY) QUANTITY 
                                          FROM prm.PACKWORKORDEROUTPUT p 
                                          where convert(char(10),p.BUSINESSDATE,120) >= '{0}' and convert(char(10),p.BUSINESSDATE,120) <= '{1}' and p.OPERFLAGID in (613677,639431)
                                          group by p.processunitid
                                        ),
                                        --机台消耗
                                        cost as (
                                          select p.PROCESSUNITID,sum(p.QUANTITY) QUANTITY 
                                          FROM prm.PACKWORKORDERCONSUME p 
                                           where EXISTS(select 1 from pub.MATERIAL m1 where p.materialid=m1.id and m1.MATERIALTYPEID in
                                           (select id from pub.materialtype mt where mt.parentid = {2} or mt.id = {2}) and m1.isused = 1) 
                                           and convert(char(10), p.BUSINESSDATE,120) >= '{0}' and convert(char(10), p.BUSINESSDATE,120) <= '{1}' and p.OPERFLAGID in (613677,639431) 
                                           group by p.PROCESSUNITID
                                        ),
                                        --产耗
                                        cost_qty as (
                                            select c.PROCESSUNITID,round((c.quantity * {3} / q.quantity), 2) as Consum from cost c
                                            inner join qty q on  c.PROCESSUNITID=q.PROCESSUNITID    
                                        )
                                        select e.EQUIPMENTMODELID,m.name as typeName,e.name as equName,cq.* from cost_qty cq
                                        left join PUB.EQUIPMENT e  on cq.PROCESSUNITID=e.ID
                                        left join pub.EQUIPMENTMODEL m on e.EQUIPMENTMODELID=m.ID
                                        order by e.EQUIPMENTMODELID,cq.Consum", strdate, enddate, strCostType, UnitConversion);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }


        public DataTable QueryJBConsumptionByModelAndShift(string strdate, string strCostType, string ShiftID, string UnitConversion)
        {
            string sql = string.Empty;
            sql = string.Format(@"--机台产量
                                        with qty as (
                                          SELECT p.processunitid,sum(p.QUANTITY) QUANTITY 
                                          FROM prm.PACKWORKORDEROUTPUT p 
                                          where convert(char(10), p.BUSINESSDATE,120) = '{0}' and p.OPERFLAGID in (613677,639431)
                                          and p.SHIFTID={2}
                                            group by p.processunitid
                                        ),
                                        --机台消耗
                                        cost as (
                                          select p.PROCESSUNITID,sum(p.QUANTITY) QUANTITY 
                                          FROM prm.PACKWORKORDERCONSUME p 
                                          where EXISTS(select 1 from pub.MATERIAL m1 where p.materialid=m1.id and m1.MATERIALTYPEID in
                                           (select id from pub.materialtype mt where mt.parentid = {1} or mt.id = {1}) and m1.isused = 1) 
                                           and convert(char(10), p.BUSINESSDATE, 120) = '{0}' and p.OPERFLAGID in (613677,639431) 
                                           and p.SHIFTID={2}
                                           group by p.PROCESSUNITID
                                        ),
                                        --产耗
                                        cost_qty as (
                                            select c.PROCESSUNITID,round((c.quantity * {3} / q.quantity), 2) as Consum from cost c
                                            inner join qty q on  c.PROCESSUNITID=q.PROCESSUNITID    
                                        )
                                        select e.EQUIPMENTMODELID,m.name as typeName,e.name as equName,cq.* from cost_qty cq
                                        left join PUB.EQUIPMENT e  on cq.PROCESSUNITID=e.ID
                                        left join pub.EQUIPMENTMODEL m on e.EQUIPMENTMODELID=m.ID
                                        order by e.EQUIPMENTMODELID,cq.Consum", strdate, strCostType, ShiftID, UnitConversion);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
