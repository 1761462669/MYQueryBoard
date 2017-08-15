using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDF.Service.Host
{
    //host111122221113
    public interface ITestService
    {
        IList<string> Test();
        IList<string> MyTest(string name);
    }
}
