using MDF.Framework;
using MDF.Framework.Bus;
using SMES.Bussiness.ControlSL.MaterialChoose;
using SMES.FrameworkAdpt.MaterialChooseControl;
using SMES.FrameworkAdpt.MaterialChooseControl.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.ObjectModel;

namespace SMES.Bussiness.ControlSL.MaterialChoose
{
    public class MDFMaterialChooseControlViewModel : DependencyObject, INotifyPropertyChanged
    {
        public MDFMaterialChooseControlViewModel()
        {
            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(typeof(MaterialModelAdpt).Assembly);
        }

        #region 查询属性


        private List<QueryMaterialType> _QueryMaterialTypes = new List<QueryMaterialType>();
        public List<QueryMaterialType> QueryMaterialTypes
        {
            get { return this._QueryMaterialTypes; }
            set
            {
                if (this._QueryMaterialTypes != value)
                {
                    this._QueryMaterialTypes = value;
                    this.RaisedPropertyChanged("QueryMaterialTypes");
                }
            }
        }

        //private string _QueryMaterialTypeIdString;
        //public string QueryMaterialTypeIdString
        //{
        //    get { return this._QueryMaterialTypeIdString; }
        //    set
        //    {
        //        if (this._QueryMaterialTypeIdString != value)
        //        {
        //            this._QueryMaterialTypeIdString = value;
        //            this.RaisedPropertyChanged("QueryMaterialTypeIdString");
        //        }
        //    }
        //}



        public string QueryMaterialTypeIdString
        {
            get { return (string)GetValue(QueryMaterialTypeIdStringProperty); }
            set { SetValue(QueryMaterialTypeIdStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QueryMaterialTypeIdString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QueryMaterialTypeIdStringProperty =
            DependencyProperty.Register("QueryMaterialTypeIdString", typeof(string), typeof(MDFMaterialChooseControlViewModel), new PropertyMetadata(""));



        public List<string> QueryMaterialTypeIds { get; set; }

        private IList<MaterialTypeModelAdpt> _MaterialTypes;
        public IList<MaterialTypeModelAdpt> MaterialTypes
        {
            get { return this._MaterialTypes; }
            set
            {
                if (this._MaterialTypes != value)
                {
                    this._MaterialTypes = value;
                    this.RaisedPropertyChanged("MaterialTypes");
                }
            }
        }


        private List<MaterialModelAdpt> _ItemSource;
        public List<MaterialModelAdpt> ItemSource
        {
            get { return this._ItemSource; }
            set
            {
                if (this._ItemSource != value)
                {
                    this._ItemSource = value;
                    this.RaisedPropertyChanged("ItemSource");
                }
            }
        }

        #endregion

        #region 调用服务

        public async void LoadDatas()
        {
            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var result = await wcf.Invoke<IComMaterialTypeService>(c => c.GetMaterialTypes(GetMaterialTypeIds()));

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialTypeModelAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                MaterialTypes = list;
            }
        }

        private List<string> GetMaterialTypeIds()
        {
            if (!string.IsNullOrEmpty(this.QueryMaterialTypeIdString))
            {
                return this.QueryMaterialTypeIdString.Split(',').ToList();
            }

            if (this.QueryMaterialTypes != null && this.QueryMaterialTypes.Count != 0)
            {
                return this.QueryMaterialTypes.Select(c => c.Value).ToList();
            }

            return null;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedPropertyChanged(string propteryName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propteryName));
            }
        }
    }
}
