using SMES.Framework;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.PortalCommonality.UserInfo
{
    public class PortalUser:RootModel
    {
        private string _UserId;
        public string UserId
        {
            get { return this._UserId; }
            set
            {
                if (this._UserId != value)
                {
                    this._UserId = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }

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

        public string PersonId { get; set; }

        private string _Cd;
        public string Cd
        {
            get { return this._Cd; }
            set
            {
                if (this._Cd != value)
                {
                    this._Cd = value;
                    this.RaisePropertyChanged("Cd");
                }
            }
        }

        private string _Ctrl;
        public string Ctrl
        {
            get { return this._Ctrl; }
            set
            {
                if (this._Ctrl != value)
                {
                    this._Ctrl = value;
                    this.RaisePropertyChanged("Ctrl");
                }
            }
        }

        private string _Name;
        public string Name
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        private string _RoleID;
        public string RoleID
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
    }
}