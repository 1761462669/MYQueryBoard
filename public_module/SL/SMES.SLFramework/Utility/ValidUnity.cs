using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Util
{
    public static class ValidUnity
    {
        public static bool IsDecimal(string s)
        {
            try
            {
                var v = decimal.Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
