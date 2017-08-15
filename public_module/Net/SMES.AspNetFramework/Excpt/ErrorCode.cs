using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Excpt
{
    public class ErrorCode
    {
        public static int Undefined = 0;

        public static int NoLogin = 1;

        public static int NoFnParameter = 2;

        public static int FnParameterError = 3;

        public static int NoPrivile = 4;

    }
}
