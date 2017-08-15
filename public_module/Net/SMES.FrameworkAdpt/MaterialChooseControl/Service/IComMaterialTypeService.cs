using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace SMES.FrameworkAdpt.MaterialChooseControl.Service
{
    [InheritedExport]
    public interface IComMaterialTypeService
    {
        IList<MaterialTypeModelAdpt> GetMaterialTypes(List<string> typeIds);

        IList GetMaterials(List<string> typeIds);
    }
}
