using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.MeasureControl.Service
{
    [InheritedExport]
    public interface IMeasureService
    {
        IList GetTypes(List<string> typeIds);

        IList GetMeasures(List<string> typeIds);
    }
}
