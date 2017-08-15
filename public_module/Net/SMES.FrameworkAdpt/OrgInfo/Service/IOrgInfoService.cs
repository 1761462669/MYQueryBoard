using SMES.Framework;
using SMES.FrameworkAdpt.OrgInfo.IModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.OrgInfo.Service
{
    [InheritedExport]
    public interface IOrgInfoService
    {
        IList<IHierarchyModel> GetAreas(string parentId);

        System.Collections.IList GetPersons(string parentId);
    }
}
