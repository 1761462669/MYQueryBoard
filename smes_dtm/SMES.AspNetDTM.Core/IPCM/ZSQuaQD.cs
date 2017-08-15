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
    public class ZSQuaQD : ServiceBase, IZSQuaQD
    {
        /// <summary>
        /// 获取制丝牌号质量得分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        public DataTable QueryData(string strToday)
        {
            string sql = string.Format(@"SELECT v.PRODUCTID,v.PRODUCTNAME,Convert(decimal(18,2),AVG(v.SCORE)) CCORE FROM MESETL.[QUA].[V_CUT_BATCHINSPECT] v
                                        where v.SAMPLETIME>='{0}'
                                        group by v.productid,v.PRODUCTNAME
                                        order by v.productid desc", strToday);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取制丝牌号批次得分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <param name="strMatName">牌号名称</param>
        /// <returns></returns>
        public DataTable QueryDatas(string strToday,string strMatName)
        {
            string sql = string.Format(@"SELECT v.BATCHCODE,v.SCORE FROM MESETL.[QUA].[V_CUT_BATCHINSPECT] v
                                        where  v.SAMPLETIME>='{0}' and  v.PRODUCTNAME='{1}'
                                        order by v.BATCHCODE asc", strToday, strMatName);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取制丝牌号批次扣分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <param name="strMatName">牌号名称</param>
        /// <returns></returns>
        public DataTable QueryChecks(string strToday, string strMatName)
        {
            string sql = string.Format(@"SELECT z.PGNM, z.PANM, z.CHECKVALUE,z2.PASSFLAG,z2.DEDUCTSCORE,z2.MAXIMUM, z2.MINIMUM,
                                               z2.SETVALUE,z.ORDERCD
                                        FROM MESETL.qua.ZSCHECKPAORI AS z
                                        INNER JOIN MESETL.qua.ZSCHECKPA AS z2 ON z.SC=z2.SC AND z.PGNM=z2.PGNM AND z.PACD=z2.PACD
                                        INNER JOIN MESETL.qua.ZSCHECKSC AS z3 ON z2.SC=z3.SC
                                        WHERE z3.CHECKTYPE=1 AND z2.DEDUCTSCORE <>'0.000' 
                                        AND EXISTS (SELECT 1 FROM MESETL.[QUA].[V_CUT_BATCHINSPECT] v
				                                        where  v.SAMPLETIME>='{0}' 
				                                        and  v.PRODUCTNAME='{1}' 
				                                        and z2.ORDERCD=v.BATCHCODE)", strToday, strMatName);
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);

            return ds.Tables[0];
        }
    }
}
