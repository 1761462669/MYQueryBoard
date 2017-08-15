using SMES.AspNetDTM.Model.Pub;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.Pub
{
    [InheritedExport]
    public interface IMaterialService
    {
        /// <summary>
        /// 查询所有物料信息
        /// </summary>
        /// <returns>物料列表</returns>
        List<Material> Query();

        DataSet QueryDataset();
       
    }
}
