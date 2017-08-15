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
    public interface IJBQuaQD
    {
        /// <summary>
        /// 获取卷包牌号质量得分数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        DataTable QueryData(string strToday);

        /// <summary>
        /// 获取卷包牌号得分详细数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        DataTable QueryDatas(string strToday, string strMatName);
    }
}
