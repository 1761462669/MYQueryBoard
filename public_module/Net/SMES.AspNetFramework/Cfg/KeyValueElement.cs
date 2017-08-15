using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Cfg
{
    public class KeyValueElement : ConfigurationElement
    {
        /// <summary>
        /// 键
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return this["key"].ToString(); }
            set { this["key"] = value; }
        }
        /// <summary>
        /// 值
        /// </summary>
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return this["value"].ToString(); }
            set { this["value"] = value; }
        }

    }
}
