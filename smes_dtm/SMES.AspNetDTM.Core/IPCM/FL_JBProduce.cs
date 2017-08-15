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
    /// <summary>
    /// 涪陵卷包产量  
    /// 创建  李金花
    /// 2016-12-10
    /// </summary>
    public class FL_JBProduce : ServiceBase, IFL_JBProduce
    {
        /// <summary>
        /// 卷包按价类产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable QueryJL(string strStartTime, string strEndTime, string strCon)
        {
            string sql = @"select convert(float,isnull(sum(a.relqua),0)) relqua,convert(float,isnull(sum(a.daqua),0)) daqua,a.pricetypeName,a.pricetype 
                            from (
                            select convert(varchar,output.BUSINESSDATE,23) plandate
                            ,sum(case output.OPERFLAGID when 613677 then output.QUANTITY end) relqua
                            ,sum(case output.OPERFLAGID when 639432 then output.QUANTITY end) daqua
                            ,eve.matid, eve.matname,eve.pricetypeName,eve.pricetype from  PRM.PACKWORKORDEROUTPUT  output
                            join 
                            (
                            select mat.ID matid,mat.[NAME] matname, baseCig.id,baseCig.name,baseCig.pricetype,matbutdt.[NAME] pricetypeName
                            from smesfl.itg.BaseCode_Cig baseCig
                            join PUB.MATERIAL mat on mat.CTRL=baseCig.id
                            left join PUB.MATERIALATTRDETAIL matbutdt on baseCig.pricetype=matbutdt.CTRL
                            join PUB.MATERIALATTRIBUTE matbut on matbut.ID = matbutdt.MATERIALATTRIBUTEID
                            where matbut.REMARK ='transCigClassBaseCode' and baseCig.sysFlag=1
                            and mat.MATERIALTYPEID in (select id from PUB.MATERIALTYPE where PARENTID=171397)
                            ) eve on eve.matid=output.MATERIALID
                            join PUB.EQUIPMENT equ on equ.ID=output.PROCESSUNITID
                            join PUB.EQUIPMENTMODEL model on model.id=equ.EQUIPMENTMODELID
                            join PUB.EQUIPMENTTYPE t on t.id=model.EQUIPMENTTYPEID
                            where t.id=684076  and output.ISDELETED=0 and output.OPERFLAGID in (613677,639432,613676)
                            group by convert(varchar,output.BUSINESSDATE,23),eve.matid, eve.matname,eve.pricetypeName,eve.pricetype
                            ) a
                            where a.plandate>='{0}' and a.plandate<='{1}'
                            group by a.pricetypeName,a.pricetype
                            order by a.pricetype ";
            sql = string.Format(sql, strStartTime, strEndTime);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 卷包按系列产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable QueryXL(string strStartTime, string strEndTime, string strCon)
        {
            string sql = @"select convert(float,isnull(sum(a.relqua),0)) relqua,convert(float,isnull(sum(a.daqua),0)) daqua,a.pricetypeName,a.pricetype 
                            from (
                            select convert(varchar,output.BUSINESSDATE,23) plandate
                            ,sum(case output.OPERFLAGID when 613677 then output.QUANTITY end) relqua
                            ,sum(case output.OPERFLAGID when 639432 then output.QUANTITY end) daqua
                            ,eve.matid, eve.matname,eve.pricetypeName,eve.pricetype from  PRM.PACKWORKORDEROUTPUT  output
                            join 
                            (
                            select mat.ID matid,mat.[NAME] matname, baseCig.id,baseCig.name,baseCig.pricetype,matbutdt.[NAME] pricetypeName
                            from smesfl.itg.BaseCode_Cig baseCig
                            join PUB.MATERIAL mat on mat.CTRL=baseCig.id
                            left join PUB.MATERIALATTRDETAIL matbutdt on baseCig.pricetype=matbutdt.CTRL
                            join PUB.MATERIALATTRIBUTE matbut on matbut.ID = matbutdt.MATERIALATTRIBUTEID
                            where matbut.REMARK ='transCigBrandBaseCode' and baseCig.sysFlag=1
                            and mat.MATERIALTYPEID in (select id from PUB.MATERIALTYPE where PARENTID=171397)
                            ) eve on eve.matid=output.MATERIALID
                            join PUB.EQUIPMENT equ on equ.ID=output.PROCESSUNITID
                            join PUB.EQUIPMENTMODEL model on model.id=equ.EQUIPMENTMODELID
                            join PUB.EQUIPMENTTYPE t on t.id=model.EQUIPMENTTYPEID
                            where t.id=684076  and output.ISDELETED=0 and output.OPERFLAGID in (613677,639432,613676)
                            group by convert(varchar,output.BUSINESSDATE,23),eve.matid, eve.matname,eve.pricetypeName,eve.pricetype
                            ) a
                            where a.plandate>='{0}' and a.plandate<='{1}'
                            group by a.pricetypeName,a.pricetype
                            order by a.pricetype";
            sql = string.Format(sql, strStartTime, strEndTime);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
        /// <summary>
        /// 卷包按牌号产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable QueryPH(string strStartTime, string strEndTime, string strCon)
        {
            string year = strStartTime.Substring(0,4);
            string month = strStartTime.Substring(5,2);

            string sql = @"select S.MATID matid,mat.name matname, ISNULL(S.planqua,0) planqua,isnull(S1.relqua,0) relqua,isnull(S1.daqua ,0) daqua
                            from (
                            select detail.MATID,sum(detail.DATEQUA)*5 planqua from  FLAPS.PackMonthPlan_Main main
                            join FLAPS.PackMonthPlan detail on detail.MAINID=main.ID
                            where [YEAR]='{0}' and [MONTH]='{1}' and ISUSE=1 and ISOVER=1
                            group by detail.MATID
                            ) S
                            left join (
                            select convert(float,isnull(sum(a.relqua),0)) relqua,convert(float,isnull(sum(a.daqua),0)) daqua,a.matid
                            from (
                            select convert(varchar,output.BUSINESSDATE,23) plandate
                            ,sum(case output.OPERFLAGID when 613677 then output.QUANTITY end) relqua
                            ,sum(case output.OPERFLAGID when 639432 then output.QUANTITY end) daqua
                            ,output.MATERIALID matid
                            from  PRM.PACKWORKORDEROUTPUT  output
                            join PUB.EQUIPMENT equ on equ.ID=output.PROCESSUNITID
                            join PUB.EQUIPMENTMODEL model on model.id=equ.EQUIPMENTMODELID
                            join PUB.EQUIPMENTTYPE t on t.id=model.EQUIPMENTTYPEID
                            where t.id=684076  and output.ISDELETED=0 and output.OPERFLAGID in (613677,639432,613676)
                            group by convert(varchar,output.BUSINESSDATE,23),output.MATERIALID) a
                            where a.plandate>='{2}' and a.plandate<='{3}'
                            group by a.matid
                            ) S1 on S1.matid=S.matid
                            left join PUB.MATERIAL mat on mat.ID=S.matid
                            order by mat.[NAME]";
            sql = string.Format(sql,year,month, strStartTime, strEndTime);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 当前班次班组日产量
        /// </summary>
        /// <returns></returns>
        public DataTable Shift_Team_Prdouct()
        {
            string sql = @"SELECT SHIFT_ID,SHIFTNAME,TEAM_ID,TEAMNAME,CONVERT(VARCHAR,PRODUCT)+'箱' PRODUCT FROM (
                            select top 1  c.SHIFT_ID,s.[NAME] SHIFTNAME,c.TEAM_ID,t.[NAME] TEAMNAME
                            from DPS.CALENDAR c
                            join dps.shift s on s.id=c.SHIFT_ID
                            join DPS.TEAM t on t.id=c.TEAM_ID
                            where RESOURCEID=186952  and TEAM_ID<>72926
                            and convert(varchar, WORKDATE,23)=convert(varchar,getdate(),23)
                            and convert(varchar, STARTTIME,20)<=convert(varchar,getdate(),20)
                            and convert(varchar, ENDTIME ,20)>=convert(varchar,getdate(),20)
                            ) B
                            LEFT JOIN (
                            SELECT convert(varchar,ROUND(isnull(sum(value),0)*200/50000,2)) PRODUCT
                            FROM insqldb.Runtime.DBO.LIVE WHERE TAGNAME IN (
                            select VAL1 from FLPUB.T_PUB_CODE where CD_type='011001001')
                            ) A ON  1=1 ";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text,sql);
            return ds.Tables[0];
        }
    }
}
