using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.MaterialChooseControl
{
    public class MaterialTypeModelAdpt : BaseHierarchyModel
    {
        #region IMaterialTypeModel 成员
        /// <summary>
        /// 备注
        /// </summary> 
        private string _Remark;
        public string Remark
        {
            get { return this._Remark; }
            set
            {
                if (this._Remark != value)
                {
                    this._Remark = value;
                    this.RaisePropertyChanged("Remark");
                }
            }
        }
        /// <summary>
        /// 顺序
        /// </summary>
        private int _SequenceNumber;
        public int SequenceNumber
        {
            get { return this._SequenceNumber; }
            set
            {
                if (this._SequenceNumber != value)
                {
                    this._SequenceNumber = value;
                    this.RaisePropertyChanged("SequenceNumber");
                }
            }
        } 

        #endregion 
    }
}
