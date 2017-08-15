using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using SMES.FrameworkAdpt.CommonPara.Model;

namespace SMES.FrameworkAdpt.CommonPara.Service
{
    [InheritedExport]
    public interface ICommonParaService
    {
        IList GetAllParas();

        void SaveOrUpdate(CommonParaModel obj);

        void Delete(CommonParaModel obj);

        IList GetParaTypes();

        IList QueryParasByTypeId(CommonParaTypeModel type,string name);
    }
}
