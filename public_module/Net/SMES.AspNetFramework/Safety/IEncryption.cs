using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Safety
{
    public interface IEncryption
    {
        string Encryption(string text);

        string UnEncryption(string text);
    }
}
