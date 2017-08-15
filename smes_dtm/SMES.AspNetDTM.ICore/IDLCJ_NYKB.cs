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
    public interface IDLCJ_NYKB
    {
        DataTable DLCJ_NYJS_1();
        DataTable DLCJ_WS(string ID);
        DataTable DLCJ_NYJS_3();
    }
}
