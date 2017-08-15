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
    public interface IZSProduceEfficiency
    {
        /// <summary>
        /// 获取制丝成产效率主数据
        /// </summary>
        /// <param name="strToday">开始日期</param>
        /// <returns></returns>
        DataTable QueryData(string strToday);
    }
}
