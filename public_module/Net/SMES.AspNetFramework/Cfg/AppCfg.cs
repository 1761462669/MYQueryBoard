using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Cfg
{
    public class AppCfg : Config
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName
        {
            get
            {                
                //if (KeyValues["Path"] == null) return "Upload";
                return KeyValues["AppName"].Value;
            }
            set
            {
                //if (KeyValues["Path"] == null) KeyValues["Path"] = new KeyValueElement() { Key = "Path", Value = value };
                //else KeyValues["Path"].Value = value;
                KeyValues["AppName"].Value = value;
            }

        }

        /// <summary>
        /// 数据服务地址
        /// </summary>
        public string SrvAddress
        {
            get {
                if (KeyValues["SrvAddress"] == null)
                    return "";
                else
                    return KeyValues["SrvAddress"].Value;
            }

            set {
                KeyValues["SrvAddress"].Value = value;
            }
        }
    }
}
