using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SMES.AspNetDTM.ICore.IPCM
{
     [InheritedExport]
    public interface IJBCJ_EquStop
    {
         DataTable JBCJ_Year();
         DataTable JBCJ_JT_Month(string CODE,string DATE);
         DataTable JBCJ_Month(string DATE);
         DataTable JBCJ_JT_Day(string CODE,string DATE);
    }

}
