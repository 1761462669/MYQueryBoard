
using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDF.Service.Host.Rmt
{
     [Serializable]
    public class Service : MarshalByRefObject,IServices
    {
         public T CreateServices<T>()
         {
             T obj = MDF.Framework.Bus.ObjectFactory.GetObject<T>(typeof(T).FullName);
             return obj;
         }
    }
}
