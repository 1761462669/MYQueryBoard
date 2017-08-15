using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    /// <summary>
    /// 服务对象接口
    /// </summary>
    public interface IServices
    {
        T CreateServices<T>();
    }
}
