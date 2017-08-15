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
    public interface IZSQuaQD
    {
        /// <summary>
        /// 获取制丝牌号质量得分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        DataTable QueryData(string strToday);

        /// <summary>
        /// 获取制丝牌号批次得分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        DataTable QueryDatas(string strToday, string strMatName);

        /// <summary>
        /// 获取制丝牌号批次扣分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <param name="strMatName">牌号名称</param>
        /// <returns></returns>
        DataTable QueryChecks(string strToday, string strMatName);
    }
}
