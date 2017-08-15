using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SMES.AspNetDTM.Model.Pub;
using SMES.Framework.EplDb;

namespace SMES.AspNetDTM.ICore.Pub
{
    [InheritedExport]
    public interface ISqlCommandConfigService
    {
        int Query(string sqlcode,List<SQLParameter> paralist, out DataSet ds);

        int QueryById(string Id,List<SQLParameter> paralist,out DataSet ds);

        int QuerySqlCommand(string SqlCmd,out DataSet ds);
    }
}
