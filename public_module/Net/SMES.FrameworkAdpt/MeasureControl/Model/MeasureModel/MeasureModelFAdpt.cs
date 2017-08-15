using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.MeasureControl.Model.MeasureModel
{
    /// <summary>
    /// 计量单位管理
    /// added by baijl,2015-04-02
    /// </summary>
    public class MeasureModelFAdpt : DataModel
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public int? SequenceNumber { get; set; }
        /// <summary>
        /// 计量单位类型
        /// </summary>
        public MeasureTypeModelFAdpt MeasureType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
