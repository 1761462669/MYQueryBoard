using MDF.Framework;
using MDF.Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        bool IsHaveError { get; set; }

        /// <summary>
        /// 实体展示状态
        /// </summary>
        ModelViewState ViewState { get; set; }

    }

    /// <summary>
    /// DataModel基类，包含如是否被选中、被展开等属性
    /// addedy by changhl,2015-4.2
    /// </summary>
    public class RootModel : NotifyObject, IRootModel
    {
        #region feilds && Properties



        private ModelViewState _ViewState;
        public ModelViewState ViewState
        {
            get { return this._ViewState; }
            set
            {
                if (this._ViewState != value)
                {
                    this._ViewState = value;
                    this.RaisePropertyChanged("ViewState");
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





        private bool _IsExpand = false;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand
        {
            get { return this._IsExpand; }
            set
            {
                if (this._IsExpand != value)
                {
                    this._IsExpand = value;
                    this.RaisePropertyChanged("IsExpand");
                }
            }
        }

        
        private bool _IsEnabled = false;
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set
            {
                if (this._IsEnabled != value)
                {
                    this._IsEnabled = value;
                    this.RaisePropertyChanged("IsEnabled");
                }
            }
        }

        
        private bool _IsChecked = false;
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

        private bool _IsHaveError;
        public bool IsHaveError
        {
            get { return this._IsHaveError; }
            set
            {
                if (this._IsHaveError != value)
                {
                    this._IsHaveError = value;
                    this.RaisePropertyChanged("IsHaveError");
                }
            }
        }

        #endregion

        #region events
        #endregion

        #region Constructs

        public RootModel()
        {
            this.ErrorsChanged += Model_ErrorsChanged;
            this.ViewState = ModelViewState.View;
        }

        private void Model_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            this.IsHaveError = !this.HasErrors;
        }
        #endregion

        #region Methods
        #endregion
    }
    /// <summary>
    /// 实体基类
    /// </summary>
    public class DataModel : RootModel, IDataModel
    {
        
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
        //[Display(Name="名称")]
        //[Required(ErrorMessage = "必填项")]
        public string Name
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.RaisePropertyChanged("Name");

                   // base.RaiseError(value, "Name");
                }
            }
        }

        private string _NameShortPY;
        public string NameShortPY
        {
            get
            {
                try
                {
                    string py = this.Name.Replace(" ", "").Trim().GetPYString().Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "");
                    _NameShortPY = py;
                    return this._NameShortPY;
                }
                catch (Exception)
                {
                    return "";
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
    /// <summary>
    /// 实体基类
    /// </summary>
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
            if (castObj != null && castObj.Id == this.Id)
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
