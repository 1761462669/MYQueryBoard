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

namespace MDF.Custom.ControlSL.Layout.ConnectGrid.Model
{
    public class MDF_ConnectItemModel : System.ComponentModel.INotifyPropertyChanged
    {
        private string _key;
        private int _columnIndex;
        private int _rowIndex;
        private object _content;
        private Color itemColor;
        private string stateTag;

        public string StateTag
        {
            get { return stateTag; }
            set
            {
                stateTag = value;
                this.PropertyChangMothed("StateTag");
            }
        }

        public Color ItemColor
        {
            get { return itemColor; }
            set
            {
                itemColor = value;
                PropertyChangMothed("ItemColor");
            }
        }

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                PropertyChangMothed("Key");
            }
        }

        public int ColumnIndex
        {
            get
            {
                return _columnIndex;
            }
            set
            {
                _columnIndex = value;
                PropertyChangMothed("ColumnIndex");
            }
        }

        public int RowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                _rowIndex = value;
                PropertyChangMothed("RowIndex");
            }
        }

        public object Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                PropertyChangMothed("Content");
            }
        }

        private int _realColumnIndex;
        public int RealColumnIndex
        {
            get
            {
                return _realColumnIndex;
            }
            set
            {
                _realColumnIndex = value;
                PropertyChangMothed("RealColumnIndex");
            }
        }

        private int _realRowIndex;
        public int RealRowIndex
        {
            get
            {
                return _realRowIndex;
            }
            set
            {
                _realRowIndex = value;
                PropertyChangMothed("RealRowIndex");
            }
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangMothed(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }
    }
}
