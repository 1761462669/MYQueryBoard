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
    public class JBCJ_JBQuaFXKB : ServiceBase, IJBCJ_JBQuaFXKB
    {

        public DataTable Month_Qua_Rate()
        {
            string sql = @"select A.DJ, convert(float,A.RESULT) RESULT
                            from (
                            select '合格率' DJ,RESULT from FLQUA.F_MONTH_RATE(1,'1,2,3,4,5')
                            union
                            select '优等率' DJ,RESULT from FLQUA.F_MONTH_RATE(1,'1,2,3')
                            union
                            select '精品率' DJ,RESULT from FLQUA.F_MONTH_RATE(1,'1,2')
                            ) A";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable getData_PH(string DJ)
        {
            string sql = @"declare @DJtype varchar(200);
                        set @DJtype='{0}';

                        if(@DJtype='精品率')   
                        begin                        
                        select MATNAME, RESULT from FLQUA.F_Month_Mat_Rate(1,'1,2') 
                        order by MATNAME                      
                        end
                        else if(@DJtype='优等率')
                        begin                        
                        select MATNAME, RESULT from FLQUA.F_Month_Mat_Rate(1,'1,2,3') 
                        order by MATNAME                        
                        end
                        else if(@DJtype='合格率')
                        begin                        
                        select MATNAME, RESULT from FLQUA.F_Month_Mat_Rate(1,'1,2,3,4,5') 
                        order by MATNAME                       
                        end";
            sql = string.Format(sql, DJ);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable getData_Equ(string DJ)
        {
            string sql = @"declare @DJtype varchar(200);
                        set @DJtype='{0}';

                        if(@DJtype='精品率')   
                        begin                        
                        select a.ID EQUID,right(a.[NAME],3) EQUNAME ,b.RESULT BYRESULT
                        from PUB.EQUIPMENT a 
                        left join 
                        FLQUA.F_PROC_EQU_MONTH_RATE(left(convert(varchar,getdate(),23),7),1,'1,2') b on b.EQUID=a.ID
                        where a.ID in (select ID from PUB.EQUIPMENT where EQUIPMENTMODELID in (select ID from PUB.EQUIPMENTMODEL where EQUIPMENTTYPEID=684076 and ID!=1038584))
                        order by a.[NAME]                        
                        end
                        else if(@DJtype='优等率')
                        begin                        
                        select a.ID EQUID,right(a.[NAME],3) EQUNAME ,b.RESULT BYRESULT
                        from PUB.EQUIPMENT a 
                        left join 
                        FLQUA.F_PROC_EQU_MONTH_RATE(left(convert(varchar,getdate(),23),7),1,'1,2,3') b on b.EQUID=a.ID
                        where a.ID in (select ID from PUB.EQUIPMENT where EQUIPMENTMODELID in (select ID from PUB.EQUIPMENTMODEL where EQUIPMENTTYPEID=684076 and ID!=1038584))
                        order by a.[NAME]                        
                        end
                        else if(@DJtype='合格率')
                        begin                        
                        select a.ID EQUID,right(a.[NAME],3) EQUNAME ,b.RESULT BYRESULT
                        from PUB.EQUIPMENT a 
                        left join 
                        FLQUA.F_PROC_EQU_MONTH_RATE(left(convert(varchar,getdate(),23),7),1,'1,2,3,4,5') b on b.EQUID=a.ID
                        where a.ID in (select ID from PUB.EQUIPMENT where EQUIPMENTMODELID in (select ID from PUB.EQUIPMENTMODEL where EQUIPMENTTYPEID=684076 and ID!=1038584))
                        order by a.[NAME]                        
                        end";
            sql = string.Format(sql, DJ);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable getData_Team(string DJ)
        {
            string sql = @"declare @DJtype varchar(200);
                        set @DJtype='{0}';

                        if(@DJtype='精品率')   
                        begin                        
                        select plandate, JB, YB, BB from FLQUA.F_Month_Team_Rate(1,'1,2')                      
                        end
                        else if(@DJtype='优等率')
                        begin                        
                        select plandate, JB, YB, BB from FLQUA.F_Month_Team_Rate(1,'1,2,3')                        
                        end
                        else if(@DJtype='合格率')
                        begin                        
                        select plandate, JB, YB, BB from FLQUA.F_Month_Team_Rate(1,'1,2,3,4,5')                     
                        end";
            sql = string.Format(sql, DJ);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
