using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.EquipmentChoose
{
    /// <summary>
    /// 设备机型数据模型
    /// </summary>
    public class EquipmentModelModelFAdpt : DataModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public EquipmentTypeModelFAdpt EquipmentTypeModel { get; set; }

    }
}
