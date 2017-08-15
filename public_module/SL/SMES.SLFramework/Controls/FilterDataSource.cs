using MDF.Framework.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using MDF.Framework.Bus;
using System.Windows.Controls;
using MDF.Framework.Model;
using MDF.Framework;

namespace SMES.Framework.Controls
{
    public class DataItem : BaseModel
    {

        private string _Json;
        public string Json
        {
            get { return this._Json; }
            set
            {
                if (this._Json != value)
                {
                    this._Json = value;
                    this.RaisedPropertyChanged("Json");
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
                    this.RaisedPropertyChanged("PY");
                }
            }
        }


        private object _Data;
        public object Data
        {
            get { return this._Data; }
            set
            {
                if (this._Data != value)
                {
                    this._Data = value;
                    this.RaisedPropertyChanged("Data");
                }
            }
        }
    }
    public class FilterDataSource : BaseViewModel
    {

        //internal PagedCollectionView originalSource;

        private PagedCollectionView _DataSource ;
        /// <summary>
        /// 数据源
        /// </summary>
        public PagedCollectionView DataSource
        {
            get { return this._DataSource; }
            set
            {
                if (this._DataSource != value)
                {
                    this._DataSource = value;
                    this.RaisePropertyChanged("DataSource");
                }
            }
        }


        private string _FilterString;
        /// <summary>
        /// 帅选项
        /// </summary>
        public string FilterString
        {
            get { return this._FilterString; }
            set
            {
                if (this._FilterString != value)
                {
                    this._FilterString = value;
                    this.RaisePropertyChanged("FilterString");
                    this.FilterData();
                }
            }
        }


        private string _PYPropertyName;
        public string PYPropertyName
        {
            get { return this._PYPropertyName; }
            set
            {
                if (this._PYPropertyName != value)
                {
                    this._PYPropertyName = value;
                    this.RaisePropertyChanged("PYPropertyName");
                    this.Inital();
                }
            }
        }


        public IEnumerable FromSource
        {
            get { return (IEnumerable)GetValue(FromSourceProperty); }
            set { SetValue(FromSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FromSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromSourceProperty =
            DependencyProperty.Register("FromSource", typeof(IEnumerable), typeof(FilterDataSource), new PropertyMetadata(new PropertyChangedCallback(OnFromSourcePropertyChangedCallback)));

        private static void OnFromSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FilterDataSource;
            var ds = e.NewValue as IEnumerable;
            if (ds != null)
            {
                var list = new List<DataItem>();
                foreach (var item in ds)
                {
                    list.Add(new DataItem() { Data = item });
                }
                obj.DataSource = new PagedCollectionView(list);
                obj.Inital();
            }



        }

        internal void Inital()
        {
            if (this.DataSource != null)
            {
                foreach (DataItem item in this.DataSource)
                {
                    item.Json = InfoExchange.ConvertToJson(item.Data);
                    if (item.Data != null && this.PYPropertyName != null && this.PYPropertyName != "")
                    {
                        var prp = item.Data.GetType().GetProperty(this.PYPropertyName);
                        if (prp != null)
                        {
                            var value = prp.GetValue(item.Data, null);
                            if (value != null)
                            {
                                item.PY = value.ToString().GetPYString();
                            }
                        }
                    }
                }
            }


        }



        internal void FilterData()
        {
            if (this.DataSource != null)
            {
                //this.DataSource.DeferRefresh();
                
                this.DataSource.Filter = p =>
                {
                    var dt = p as DataItem;
                    if (dt == null)
                        return false;
                    if (dt.Json.Contains(this.FilterString))
                        return true;
                    if (dt.PY.Contains(this.FilterString))
                        return true;
                    return false;
                };
                this.DataSource.Refresh();
               
            }

        }

        public void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            this.FilterString = txt.Text;
            this.FilterData();
        }




    }
}
