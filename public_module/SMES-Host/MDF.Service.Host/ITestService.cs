using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDF.Service.Host
{
    public interface ITestService
    {
        IList<string> Test();
        IList<string> MyTest(string name);
    }
}
