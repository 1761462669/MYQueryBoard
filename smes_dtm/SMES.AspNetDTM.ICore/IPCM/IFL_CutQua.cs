using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.IPCM
{
    public interface IFL_CutQua
    {
        DataTable QueryQua(string strCon);
    }
}
