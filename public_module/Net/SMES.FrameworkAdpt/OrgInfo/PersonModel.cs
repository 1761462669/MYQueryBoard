using SMES.Framework;
using System;

namespace SMES.FrameworkAdpt.OrgInfo
{
    /// <summary>
    /// 人员信息在基本的DataModel基础上还有人员的其他基本信息属性
    /// 手机，地址等
    /// add by  wuyun  2015-04-03
    /// </summary>
    public class PersonModel : DataModel
    {
        private DateTime _Birthday = DateTime.MinValue;
        public DateTime Birthday
        {
            get { return this._Birthday; }
            set
            {
                if (this._Birthday != value)
                {
                    this._Birthday = value;
                    this.RaisePropertyChanged("Birthday");
                }
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public AreaModel Area { get; set; }

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
