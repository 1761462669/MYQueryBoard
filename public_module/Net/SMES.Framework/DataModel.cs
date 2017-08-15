using MDF.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public interface IRootModel
    {

        object Wrapper { get; set; }

        string PY { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        bool IsExpand { get; set; }

        bool IsEnabled { get; set; }


        bool IsChecked { get; set; }

    }

    /// <summary>
    /// DataModel基类，包含如是否被选中、被展开等属性
    /// addedy by changhl,2015-4.2
    /// </summary>
    /// 
    [Serializable]
    public class RootModel : NotifyObject, IRootModel
    {


        #region feilds && Properties

        private object _Wrapper;
        public object Wrapper
        {
            get { return this._Wrapper; }
            set
            {
                if (this._Wrapper != value)
                {
                    this._Wrapper = value;
                    this.RaisePropertyChanged("Wrapper");
                }
            }
        }
        private string _PY;
        public string PY
        {
            get { return this._PY; }
            set
            {
                if (this._PY != value)
                {
                    this._PY = value;
                    this.RaisePropertyChanged("PY");

                }
            }
        }



        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand { get; set; }

        public bool IsEnabled { get; set; }

        //public bool IsExpand { get; set; }


        private bool _IsChecked;
        public bool IsChecked
        {
            get { return this._IsChecked; }
            set
            {
                if (this._IsChecked != value)
                {
                    this._IsChecked = value;
                    this.RaisePropertyChanged("IsChecked");
                }
            }
        }
        #endregion

        #region events
        #endregion

        #region Constructs
        #endregion

        #region Methods
        #endregion


    }


    /// <summary>
    /// 实体基类
    /// </summary>
    /// 
    [Serializable]
    public class DataModel : RootModel, IDataModel
    {
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

        private int _Id;
        public int Id
        {
            get { return this._Id; }
            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }

        private bool _IsUsed = true;
        public bool IsUsed
        {
            get { return this._IsUsed; }
            set
            {
                if (this._IsUsed != value)
                {
                    this._IsUsed = value;
                    this.RaisePropertyChanged("IsUsed");
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

        public override bool Equals(object obj)
        {

            var castObj = obj as DataModel;
            if (castObj != null && castObj.Id == this.Id)
            {
                if (this.GetType() == castObj.GetType())
                {
                    return true;

                }
            }


            return false;
        }
        public override int GetHashCode()
        {
            return 7 ^ this.Id.GetHashCode();
        }


    }

    public class BusinessModel : RootModel, IBusinessModel
    {

        private string _Id;
        public string Id
        {
            get { return this._Id; }
            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }

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

        private bool _IsUsed = true;
        public bool IsUsed
        {
            get { return this._IsUsed; }
            set
            {
                if (this._IsUsed != value)
                {
                    this._IsUsed = value;
                    this.RaisePropertyChanged("IsUsed");
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

        public override bool Equals(object obj)
        {

            var castObj = obj as BusinessModel;
            if (castObj != null)
            {
                if (this.GetType() == castObj.GetType())
                {
                    if (this.Id == castObj.Id)
                        return true;
                }

            }

            return false;
        }
        public override int GetHashCode()
        {
            return 7 ^ this.Id.GetHashCode();
        }
    }
}
