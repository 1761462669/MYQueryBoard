using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.IPCM
{
    [InheritedExport]
    public interface IStockSrv
    {
        /// <summary>
        /// 查询储柜信息，根据储柜类型
        /// </summary>
        /// <param name="typeid">类型</param>
        /// <returns></returns>
        DataTable QueryStoreInfoByType(int typeid);

        /// <summary>
        /// 查询储柜类型
        /// </summary>
        /// <returns></returns>
        DataTable QueryStoreType();
    }
}
