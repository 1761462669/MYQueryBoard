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
    public interface IJBCJ_JBTotalKB
    {
        
        /// <summary>
        /// 卷包当月产量、计划量
        /// </summary>
        /// <returns></returns>
        DataTable Month_Prdouct();
        /// <summary>
        /// 卷包当月质量缺陷
        /// </summary>
        /// <returns></returns>
        DataTable Month_Qua_QX();

        /// <summary>
        /// 当年车间精品率
        /// </summary>
        /// <returns></returns>
        DataTable Year_Rate();
        /// <summary>
        /// 卷包当月车间精品率
        /// </summary>
        /// <returns></returns>
        DataTable Month_Rate();
        /// <summary>
        /// 卷包当日车间精品率
        /// </summary>
        /// <returns></returns>
        DataTable Day_Rate();
        /// <summary>
        /// 当前设备有效作业率
        /// </summary>
        /// <returns></returns>
        DataTable Equ_Efficiency();
        /// <summary>
        /// 年度产量对比
        /// </summary>
        /// <returns></returns>
        DataTable Year_Product();
    }
}
