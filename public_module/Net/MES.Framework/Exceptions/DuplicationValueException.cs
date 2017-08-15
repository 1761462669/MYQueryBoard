using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Exceptions
{
    public class DuplicationValueException:Exception
    {
        public DuplicationValueException(string message)
            :base(message)
        {
           
        }
    }
}
