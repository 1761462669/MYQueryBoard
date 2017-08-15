using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public static class DeepCopy
    {
        public static T Copy<T>(this T obj) where T : class
        {
            if (obj == null)
                return null;
            var json = MDF.Framework.Bus.InfoExchange.ConvertToJson(obj,MDF.Framework.Bus.InfoExchange.SetingsKonwnTypesBinder);
            T result = MDF.Framework.Bus.InfoExchange.DeConvert<T>(json, MDF.Framework.Bus.InfoExchange.SetingsKonwnTypesBinder);
            return result;

        }
    }
}
