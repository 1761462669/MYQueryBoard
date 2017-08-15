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
    public class JBQuaQD : ServiceBase, IJBQuaQD
    {
        /// <summary>
        /// 获取卷包牌号质量得分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        public DataTable QueryData(string strToday)
        {
            string sql = string.Format(@"SELECT v.MATID PRODUCTID,v.MATNM PRODUCTNAME,Convert(numeric(18,2),AVG(Convert(numeric(18,2),v.SCSCORE))) CCORE 
                                         FROM MESETL.qua.JBCHECKSC_ZGC v
                                            where v.SCTIME>='{0}' and v.SCSCORE is not null and v.SCSCORE<>''
                                            group by v.MATID,v.MATNM
                                            order by v.MATID desc", strToday);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取卷包牌号得分详细数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        public DataTable QueryDatas(string strToday, string strMatName)
        {
            string sql = string.Format(@"select  v.SCTIME,avg(convert(float,v.SCSCORE)) SCSCORE from 
                                        (                                      
                                        SELECT convert(varchar(10),jz.SCTIME,120) SCTIME,jz.SCSCORE
                                        FROM MESETL.qua.JBCHECKSC_ZGC AS jz WHERE jz.MATNM='{1}' and jz.SCTIME>='{0}' AND 
                                        jz.CHECKTYPE=1 and jz.SCSCORE is not null and jz.SCSCORE<>''
                                        ) v
                                        group by v.SCTIME
                                        order by SCTIME asc", strToday, strMatName);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
