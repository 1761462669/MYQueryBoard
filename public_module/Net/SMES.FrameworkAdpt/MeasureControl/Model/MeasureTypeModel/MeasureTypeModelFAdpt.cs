using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.MeasureControl.Model.MeasureModel
{
    /// <summary>
    /// 计量单位类型
    /// added by baijl,2015-4-2;
    /// </summary>
    public class MeasureTypeModelFAdpt : DataModel
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? SequenceNumber { get; set; }
        /// <summary>
        /// 父类型
        /// </summary>
        public virtual int? ParentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
