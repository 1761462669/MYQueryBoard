using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.EplDb
{
    public class SQLParaCollection:List<SQLParameter>
    {
        public SQLParameter this[string name]
        {
            get
            {
                return this.FirstOrDefault(p=>p.Name==name);
            }
        }
    }
}
