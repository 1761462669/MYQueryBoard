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
    public class MDF_ConnectLineModel : System.ComponentModel.INotifyPropertyChanged
    {
        public string Id { get; set; }

        private string _from;
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("From"));
                }
            }
        }

        private string _to;
        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("To"));
                }
            }
        }

        private object _content;
        public object Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Content"));
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
