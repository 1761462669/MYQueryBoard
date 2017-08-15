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
    public interface IJBCJ_JBQuaKB
    {
        DataTable QueryMachineScore();

        DataTable QueryTeamScore();
    }
}
