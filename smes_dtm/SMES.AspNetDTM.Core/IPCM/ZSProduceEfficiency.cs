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
    public class JBZSProduceEfficiency : ServiceBase, IZSProduceEfficiency
    {
        /// <summary>
        /// 获取制丝成产效率主数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        public DataTable QueryData(string strToday)
        {
            string sql = string.Format(@"select t.BATCHID,t.PRODUCTID,t.producelineid,t.RESOURCEID,min(t.REALSTARTTIME) REALSTARTTIME,max(t.REALENDTIME) REALENDTIME,sum(t.QUANTITY) QUANTITY,sum(t.LLSJ) LLSJ
	                                    from 
                                        (
	                                        select e.*,r.producelineid,c.RESOURCEID,c.BATCHID,c.PRODUCTID
                                            ,(case when p.QUANTITY is null then 0 else p.QUANTITY end) QUANTITY 
                                            ,(DATEDIFF(ss, e.PLANSTARTTIME, e.PLANENDTIME)/60.0) LLSJ
                                            from dps.cutworkorderexcuterecord e
	                                        left join prm.CUTWORKORDEROUTPUT p on p.WORKORDERCODE=e.CUTWONO
	                                        left join dps.CUTWORKORDER c on e.CUTWONO=c.PLANCODE
	                                        left join PUB.RELPRODUCELINEPROCESSSEG r on r.producelineid is not null and c.RESOURCEID=r.processsegmentid 
	                                        where e.REALSTARTTIME is not null  
	                                        --and (e.REALENDTIME> CONVERT(varchar(100), '{0}', 111) or e.REALSTARTTIME>CONVERT(varchar(100), '{0}', 111))
                                            and (e.REALENDTIME>'{0}'+' 6:20:00' or e.REALSTARTTIME>'{0}'+' 6:20:00')
                                        ) t
                                        group by t.BATCHID,t.PRODUCTID,t.producelineid,t.RESOURCEID 
                                        order by RESOURCEID,REALSTARTTIME ", strToday);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
