using SMES.AspNetDTM.Model.Pub;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.IPCM
{
    [InheritedExport]
    public interface ITsStockService
    {
        DataTable GetSiloInfos();

        DataTable GetCutSiloInfo();


        DataTable GetSoliListByTypeName(string Name);

        DataTable QueryStoreInfoByType(int typeid);

        DataTable QueryStoreType();

    }
}
