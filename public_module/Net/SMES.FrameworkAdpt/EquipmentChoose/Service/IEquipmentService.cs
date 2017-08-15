using SMES.Framework;
using SMES.FrameworkAdpt.OrgInfo.IModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.EquipmentChoose.Service
{
    [InheritedExport]
    public interface IEquipmentService
    {
        IList<EquipmentTypeModelFAdpt> GetTypes(List<string> typeIds);

        System.Collections.IList GetEquipments(List<string> typeIds);
    }
}
