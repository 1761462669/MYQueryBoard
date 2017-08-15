using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDF.Service.Host
{
    [Export(typeof(ITestService))]
    public class TestService : ITestService
    {

        public IList<string> Test()
        {
            var list = new List<string>();
            list.Add("chang");
            list.Add("hai");
            list.Add("long");
            //ConfigurationSettings.GetConfig("runtime")
            return list;
            //ConfigurationSettings.AppSettings.GetKey()

        }


        public IList<string> MyTest(string name)
        {
            var list = new List<string>();
            list.Add("chang");
            list.Add("hai");
            list.Add("long");
            //ConfigurationSettings.GetConfig("runtime")
            return list;
        }
    }
}
