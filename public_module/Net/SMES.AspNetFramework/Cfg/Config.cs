using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Cfg
{
    public class Config : ConfigurationSection
    {
        //private static object _cfg;
        public static T GetConfig<T>() where T : class
        {
            //if (_cfg == null)
            //    _cfg = System.Configuration.ConfigurationManager.GetSection(typeof(T).Name);

            //return _cfg as T;
            return System.Configuration.ConfigurationManager.GetSection(typeof(T).Name) as T;
        }

        protected static ConfigurationProperty _property = new ConfigurationProperty(string.Empty, typeof(KeyValueElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
        /// <summary>
        /// 配置列表
        /// </summary>
        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        protected KeyValueElementCollection KeyValues
        {
            get { return (KeyValueElementCollection)base[_property]; }
            set { base[_property] = value; }
        }
        
    }
}
