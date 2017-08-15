using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.IPCM
{
    [InheritedExport]
    public interface IJBProduceEfficiency
    {
        /// <summary>
        /// 卷包机台运行效率
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        DataTable QueryEfficiencyRun(string strStartTime, string strEndTime, string strCon);

        /// <summary>
        /// 卷包机台台时产能
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        DataTable QueryCapacity(string strStartTime, string strEndTime, string strCon);
    }
}
