using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.EplDb
{
    [Serializable]
    public class SQLParameter
    {
        public string Name
        { get; set; }

        public DbType DbType
        { get; set; }

        public object Value
        { get; set; }
    }
}
