using SMES.AspNetFramework.Cfg;
using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Srv
{
    public class SrvProxy
    {
        private static string _address;

        /// <summary>
        /// 服务地址
        /// </summary>
        public static string Address
        {
            get
            {
                if (_address == null)
                    _address=Config.GetConfig<AppCfg>().SrvAddress;
                return _address;
            }
        }

        /// <summary>
        /// 创建远程服务对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        public static T CreateServices<T>()
        {
            IServices server = (IServices)Activator.GetObject(typeof(IServices), Address);
            if (server == null)
                return default(T);
            else
                return server.CreateServices<T>();
        }
    }
}
