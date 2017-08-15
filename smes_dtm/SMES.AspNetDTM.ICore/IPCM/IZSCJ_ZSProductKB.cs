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
    public interface IZSCJ_ZSProductKB
    {
        DataTable GetYear_DLRate();
        DataTable GetMonth_DLRate();
        DataTable GetYear_HGRate();
        DataTable GetMonth_HGRate();
    }
}
