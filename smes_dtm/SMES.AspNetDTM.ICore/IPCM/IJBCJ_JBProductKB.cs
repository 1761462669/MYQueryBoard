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
    public interface IJBCJ_JBProductKB
    {
        /// <summary>
        /// 卷包当前班次班组及日产量
        /// </summary>
        /// <returns></returns>
        DataTable Shift_Team_Prdouct();
        /// <summary>
        /// 卷包当班机台产量
        /// </summary>
        /// <returns></returns>
        DataTable Team_Equ_Product();
        /// <summary>
        /// 卷包当月产量、计划量
        /// </summary>
        /// <returns></returns>
        DataTable Month_Prdouct();
        /// <summary>
        /// 卷包按牌号产量
        /// </summary>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        DataTable QueryPH(string strStartTime, string strEndTime, string strCon);
    }
}
