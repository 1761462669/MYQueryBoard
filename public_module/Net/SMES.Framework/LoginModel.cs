using MDF.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public class LoginModel :DataModel
    {
        public LoginModel() { }

        private string _UserName;
        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (this._UserName != value)
                {
                    this._UserName = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }

        private string _PersonName;
        public string PersonName
        {
            get { return this._PersonName; }
            set
            {
                if (this._PersonName != value)
                {
                    this._PersonName = value;
                    this.RaisePropertyChanged("PersonName", value);
                }
            }
        }

        private string _PassWord;
        public string PassWord
        {
            get { return this._PassWord; }
            set
            {
                if (this._PassWord != value)
                {
                    this._PassWord = value;
                    this.RaisePropertyChanged("PassWord");
                }
            }
        }

        private int _RoleID;
        public int RoleID
        {
            get { return this._RoleID; }
            set
            {
                if (this._RoleID != value)
                {
                    this._RoleID = value;
                    this.RaisePropertyChanged("RoleID");
                }
            }
        }

        private string _RoleName;
        public string RoleName
        {
            get { return this._RoleName; }
            set
            {
                if (this._RoleName != value)
                {
                    this._RoleName = value;
                    this.RaisePropertyChanged("RoleName", value);
                }
            }
        }

        private int _ShiftID;
        public int ShiftID
        {
            get { return this._ShiftID; }
            set
            {
                if (this._ShiftID != value)
                {
                    this._ShiftID = value;
                    this.RaisePropertyChanged("ShiftID");
                }
            }
        }

        private int  _AreaId;
        public int  AreaId
        {
            get { return this._AreaId; }
            set
            {
                if (this._AreaId != value)
                {
                    this._AreaId = value;
                    this.RaisePropertyChanged("AreaId");
                }
            }
        }

        private string _AreaName;
        public string AreaName
        {
            get { return this._AreaName; }
            set
            {
                if (this._AreaName != value)
                {
                    this._AreaName = value;
                    this.RaisePropertyChanged("AreaName", value);
                }
            }
        }

        private string _ShiftName;
        public string ShiftName
        {
            get { return this._ShiftName; }
            set
            {
                if (this._ShiftName != value)
                {
                    this._ShiftName = value;
                    this.RaisePropertyChanged("ShiftName", value);
                }
            }
        }

        private int _TeamId;
        public int TeamId
        {
            get { return this._TeamId; }
            set
            {
                if (this._TeamId != value)
                {
                    this._TeamId = value;
                    this.RaisePropertyChanged("TeamId");
                }
            }
        }

        private string _TeamName;
        public string TeamName
        {
            get { return this._TeamName; }
            set
            {
                if (this._TeamName != value)
                {
                    this._TeamName = value;
                    this.RaisePropertyChanged("TeamName", value);
                }
            }
        }

        private string _Processsegments;
        public string Processsegments
        {
            get { return this._Processsegments; }
            set
            {
                if (this._Processsegments != value)
                {
                    this._Processsegments = value;
                    this.RaisePropertyChanged("Processsegments");
                }
            }
        }

        private string _JobNumber;
        public string JobNumber
        {
            get { return this._JobNumber; }
            set
            {
                if (this._JobNumber != value)
                {
                    this._JobNumber = value;
                    this.RaisePropertyChanged("JobNumber", value);
                }
            }
        }

        private bool _IsAdmin;
        public bool IsAdmin
        {
            get { return this._IsAdmin; }
            set
            {
                if (this._IsAdmin != value)
                {
                    this._IsAdmin = value;
                    this.RaisePropertyChanged("IsAdmin");
                }
            }
        }

        private string _CurrentExcute;
        public string CurrentExcute
        {
            get { return this._CurrentExcute; }
            set
            {
                if (this._CurrentExcute != value)
                {
                    this._CurrentExcute = value;
                    this.RaisePropertyChanged("CurrentExcute", value);
                }
            }
        }

        private string _Material;
        public string Material
        {
            get { return this._Material; }
            set
            {
                if (this._Material != value)
                {
                    this._Material = value;
                    this.RaisePropertyChanged("Material", value);
                }
            }
        }

    }
}
