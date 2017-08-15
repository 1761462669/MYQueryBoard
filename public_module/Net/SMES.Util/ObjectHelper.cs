using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDF.Framework.Bus;
namespace SMES.Util
{
    public class ObjectHelper
    {
        public static T ObjectClone<T>(T src_obj)
        {
            return
                InfoExchange.DeConvert<T>(
                InfoExchange.ConvertToJson(src_obj, InfoExchange.SetingsKonwnTypesBinder),
                InfoExchange.SetingsKonwnTypesBinder
                );
        }
    }
}
