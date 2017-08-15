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
    public interface IJBCJ_JBQuaFXKB
    {
        /// <summary>
        /// 当月车间各等级比例
        /// </summary>
        /// <returns></returns>
        DataTable Month_Qua_Rate();
        DataTable getData_PH(string DJ);
        DataTable getData_Equ(string DJ);
        DataTable getData_Team(string DJ);
    }
}
