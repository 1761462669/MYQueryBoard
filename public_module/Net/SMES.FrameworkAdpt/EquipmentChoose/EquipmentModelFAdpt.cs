using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.EquipmentChoose
{
    /// <summary>
    /// 设备数据模型
    /// added by yangyang 2015-4-3
    /// </summary>
    public class EquipmentModelFAdpt :DataModel
    {
        /// <summary>
        /// 理论生产能力
        /// </summary>
       public decimal TheoryCapacity { get; set; }

        /// <summary>
        /// 实际生产能力
        /// </summary>
       public decimal RealCapacity { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
       public int SequenceNumber { get; set; }

        /// <summary>
        /// 设备简称
        /// </summary>
       public string ShortName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
       public string Remark { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
       public string Place { get; set; }

       public DepartmentModelFAdpt DepartmentModel { get; set; }

        /// <summary>
        /// 工艺段
        /// </summary>
       //changed by yangyang 2015-4-4
       //public int ParentResourceId { get; set; }
       public ProcessSegmentModelFAdpt ProcessSegmentModel { get; set; }

        /// <summary>
        /// 机型
        /// </summary>
       public EquipmentModelModelFAdpt EquipmentModelModel { get; set; }


       private string _NamePY;
       public string NamePY
       {
           get
           {
               try
               {
                   string py = this.Name.Replace(" ", "").Trim().GetPYString().Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "");
                   _NamePY = py;
                   return this._NamePY;
               }
               catch (Exception)
               {
                   return "";
               }
           }
       }
    }
}
