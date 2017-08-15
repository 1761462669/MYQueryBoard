using MES.Framework.Exceptions;
using MES.Framework.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Utility
{
    public static class ValidationHelper
    {
        public static void ValidateNullPropertyValue(this object obj, params string[] propNames)
        {
            if (obj == null)
                return;
            var prpDics = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .ToDictionary(c => c.Name);
            StringBuilder sb = new StringBuilder();
            bool isHaveNull = false;
            if (propNames != null || propNames.Length > 0)
            {
                foreach (var prpName in propNames)
                {
                    if (!prpDics.ContainsKey(prpName))
                        continue;
                    var prpValue = prpDics[prpName].GetValue(obj);
                    if (prpValue != null)
                        continue;
                    AddExceptionString(sb, prpName);
                    isHaveNull = true;
                }
            }
            else
            {
                foreach (var item in prpDics.Values)
                {
                    var prpValue= item.GetValue(obj);
                    if (prpValue != null)
                        continue;
                    AddExceptionString(sb, item.Name);
                    isHaveNull = true;
                }
            }

            if (isHaveNull)
                throw new PropertyValueIsNullException(sb.ToString());




        }

        private static void AddExceptionString(StringBuilder sb,  string prpName)
        {
            var exceptionStr = string.Format(Resources.exception_propValuenull, prpName);
            sb.AppendLine(exceptionStr);
            
        }
    }
}
