using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.IPCM
{
    /// <summary>
    /// 涪陵卷包产量  
    /// 创建  李金花
    /// 2016-12-10
    /// </summary>
    [InheritedExport]
    public interface IFL_JBProduce
    {
        /// <summary>
        /// 卷包按价类产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        DataTable QueryJL(string strStartTime, string strEndTime, string strCon);
        /// <summary>
        /// 卷包按系列产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        DataTable QueryXL(string strStartTime, string strEndTime, string strCon);
        /// <summary>
        /// 卷包按牌号产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        DataTable QueryPH(string strStartTime, string strEndTime, string strCon);

        /// <summary>
        /// 卷包当前班次班组及日产量
        /// </summary>
        /// <returns></returns>
        DataTable Shift_Team_Prdouct();
    }
}
