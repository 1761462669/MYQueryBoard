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
   public class JBCJ_EquStop:ServiceBase,IJBCJ_EquStop
    {
       //年度停机次数趋势图   当月至前推11个月数据  共12个月数据
        public DataTable JBCJ_Year()
        {
            string sql = @"select    LEFT(C.PRODATE,7) YM,SUM(CONVERT(FLOAT,A.STOPTIMES)) STOPCS, round(sum(convert(float,a.STOPTIME))/60,2) STOPTIME
                            from [ITG].[jb_transEntryFeedBackInfoOrder_TJTJ_INFO]  a
                            left join itg.Log b on b.id=a.logid
                            left join [ITG].[jb_transEntryFeedBackInfoOrder_GDSCFK_INFO] c on c.logid=b.id
                            WHERE LEFT(CONVERT(VARCHAR,C.PRODATE,23),7)>=LEFT(CONVERT(VARCHAR,DATEADD(MONTH,-11,GETDATE()),23),7)
                            AND LEFT(CONVERT(VARCHAR,C.PRODATE,23),7)<=LEFT(CONVERT(VARCHAR,GETDATE(),23),7)
                            GROUP BY LEFT(C.PRODATE,7)
                            ORDER BY LEFT(c.PRODATE,7)";          
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
       /// <summary>
       /// 月度机台停机次数统计
        /// DATE   YYYY-MM
       /// </summary>
       /// <param name="CODE"></param>
       /// <returns></returns>

        public DataTable JBCJ_JT_Month(string CODE,string DATE)
        {
            string sql = @"DECLARE @JTTYPE VARCHAR(20),@JTH VARCHAR(20),@DATE VARCHAR(20);
                        SET @JTTYPE='{0}';
                        SET @JTH=@JTTYPE+'%';
                        SET @DATE='{1}'   --'2017-08'
                        SELECT RIGHT(c.MACH_ID,2)+'#' MACH_ID, LEFT(C.PRODATE,7) YM,SUM(CONVERT(FLOAT,A.STOPTIMES)) STOPCS,round(sum(convert(float,a.STOPTIME))/60,2) STOPTIME
                        from [ITG].[jb_transEntryFeedBackInfoOrder_TJTJ_INFO]  a
                        left join itg.Log b on b.id=a.logid
                        left join [ITG].[jb_transEntryFeedBackInfoOrder_GDSCFK_INFO] c on c.logid=b.id
                        WHERE LEFT(CONVERT(VARCHAR,C.PRODATE,23),7)>=LEFT(CONVERT(VARCHAR,DATEADD(MONTH,-11,GETDATE()),23),7)
                        AND LEFT(CONVERT(VARCHAR,C.PRODATE,23),7)<=LEFT(CONVERT(VARCHAR,GETDATE(),23),7)
                        and left(c.PRODATE,7)=@DATE and c.MACH_ID like @JTH
                        GROUP BY C.MACH_ID,LEFT(C.PRODATE,7) order by c.MACH_ID";
            sql = string.Format(sql,CODE, DATE);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
       /// <summary>
       /// 月度停机趋势图    默认显示当月的   当选择其他月份时，则显示对于月份数据
       /// 
       /// </summary>
       /// <returns></returns>

        public DataTable JBCJ_Month(string DATE)
        {
            string sql = @"select    C.PRODATE ,SUM(CONVERT(FLOAT,A.STOPTIMES)) STOPCS, round(sum(convert(float,a.STOPTIME))/60,2) STOPTIME
                        from [ITG].[jb_transEntryFeedBackInfoOrder_TJTJ_INFO]  a
                        left join itg.Log b on b.id=a.logid
                        left join [ITG].[jb_transEntryFeedBackInfoOrder_GDSCFK_INFO] c on c.logid=b.id
                        WHERE CONVERT(VARCHAR,C.PRODATE,23)>=convert(varchar,dateadd(dd,-day('{0}')+1,'{0}'),23)  
                        AND CONVERT(VARCHAR,C.PRODATE,23)<=convert(varchar,dateadd(dd,-day('{0}'),dateadd(m,1,'{0}')),23)
                        GROUP BY PRODATE
                        ORDER BY c.PRODATE";
            sql = string.Format(sql, DATE);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
       /// <summary>
       /// 机台日停机次数统计图
       /// DATE  YYYY-MM-DD
       /// </summary>
       /// <returns></returns>
        public DataTable JBCJ_JT_Day(string CODE, string DATE)
        {
            string sql = @" DECLARE @JTTYPE VARCHAR(20),@JTH VARCHAR(20),@DATE VARCHAR(20);
                             SET @JTTYPE='{0}';
                             SET @JTH=@JTTYPE+'%';
                             SET @DATE='{1}'
                            select  RIGHT(c.MACH_ID,2)+'#' MACH_ID, C.PRODATE ,SUM(CONVERT(FLOAT,A.STOPTIMES)) STOPCS, round(sum(convert(float,a.STOPTIME))/60,2) STOPTIME
                            from [ITG].[jb_transEntryFeedBackInfoOrder_TJTJ_INFO]  a
                            left join itg.Log b on b.id=a.logid
                            left join [ITG].[jb_transEntryFeedBackInfoOrder_GDSCFK_INFO] c on c.logid=b.id
                            WHERE  convert(varchar,c.PRODATE,23)=@DATE and c.MACH_ID LIKE @JTH
                            GROUP BY C.MACH_ID,PRODATE   ORDER BY C.MACH_ID  ";
            sql = string.Format(sql, CODE,DATE );
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
