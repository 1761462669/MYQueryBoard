using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Converter
{
    public class TwoColConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value is CustomDataType)
                return (value as CustomDataType).Name + "(" + (value as CustomDataType).ItemCount + ")";
             else 
                return null;
 
            }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CustomDataType : RootModel
    {

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

        private int _ItemCount;
        public int ItemCount
        {
            get { return this._ItemCount; }
            set
            {
                if (this._ItemCount != value)
                {
                    this._ItemCount = value;
                    this.RaisePropertyChanged("ItemCount");
                }
            }
        }

        private ReadOnlyObservableCollection<object> _Items;
        public ReadOnlyObservableCollection<object> Items
        {
            get { return this._Items; }
            set
            {
                if (this._Items != value)
                {
                    this._Items = value;
                    this.RaisePropertyChanged("Items");
                }
            }
        }

        private bool _IsExpander;
        public bool IsExpander
        {
            get { return this._IsExpander; }
            set
            {
                if (this._IsExpander != value)
                {
                    this._IsExpander = value;
                    this.RaisePropertyChanged("IsExpander");
                }
            }
        }

    }
}
