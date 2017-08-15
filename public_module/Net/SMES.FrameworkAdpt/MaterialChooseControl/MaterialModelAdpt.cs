using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.MaterialChooseControl
{
    public class MaterialModelAdpt : DataModel
    {
        #region IMaterial 成员

        /// <summary>
        /// 物料类型
        /// </summary> 
        private MaterialTypeModelAdpt _MaterialType;
        public MaterialTypeModelAdpt MaterialType
        {
            get { return this._MaterialType; }
            set
            {
                if (this._MaterialType != value)
                {
                    this._MaterialType = value;
                    this.RaisePropertyChanged("MaterialType");
                }
            }
        }
        /// <summary>
        /// 简称
        /// </summary> 
        private string _ShortName;
        public string ShortName
        {
            get { return this._ShortName; }
            set
            {
                if (this._ShortName != value)
                {
                    this._ShortName = value;
                    this.RaisePropertyChanged("ShortName");
                }
            }
        }
        /// <summary>
        /// 备注
        /// </summary> 
        private string _Remark;
        public string Remark
        {
            get { return this._Remark; }
            set
            {
                base.ClearErrors("Remark");
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
