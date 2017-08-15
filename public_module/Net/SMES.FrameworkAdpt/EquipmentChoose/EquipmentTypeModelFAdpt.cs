using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.EquipmentChoose
{   
    /// <summary>
    /// 设备类型数据模型
    /// added by yangyang 2015-4-2
    /// </summary>
    public class EquipmentTypeModelFAdpt : DataModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
