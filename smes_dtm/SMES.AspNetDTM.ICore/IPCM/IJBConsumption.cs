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
    public interface IJBConsumption
    {
        DataTable QueryJBConsumptionByMat(string strdate, string endDate);
        DataTable QueryJBConsumptionByMatAndShift(string strdate,string ShiftID);
        DataTable QueryJBConsumptionByModel(string StrDate, string endDate, string strCostType, string UnitConversion);
        DataTable QueryJBConsumptionByModelAndShift(string strdate, string strCostType, string ShiftID, string UnitConversion);
    }
}
