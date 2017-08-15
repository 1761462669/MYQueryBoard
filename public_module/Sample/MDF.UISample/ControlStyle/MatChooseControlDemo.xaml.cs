using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MDF.Custom.ControlSL.Controls;
using MDF.Framework;

using SMES.FrameworkAdpt.MaterialChooseControl;

namespace MDF.UISample.ControlStyle
{
    public partial class MatChooseControlDemo : UserControl,System.ComponentModel.INotifyPropertyChanged
    {
        DemoViewModel d;

        public MatChooseControlDemo()
        {
            InitializeComponent();

            d = new DemoViewModel();
            this.DataContext = d;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }

        private void MDFButton_Click(object sender, RoutedEventArgs e)
        {
            //var obj = new CommonPara(){ KeyCode = "STATE_NEW"};
            //string value = obj.ProvideValue(null);

            //btnTest.Content = value;

            d.Selected = new MaterialModelAdpt() { Id = 74024, Name = "黄金叶(名仕之风)" };
        }

        private void MDFButton_Click_1(object sender, RoutedEventArgs e)
        {
            ChildWindowTest cw = new ChildWindowTest();
            cw.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            cw.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            cw.Style = Application.Current.Resources["pupupStyle"] as Style; ;
            cw.Show();
        }
    }

    public class DemoViewModel : BaseModel
    {
        
        private object _Selected;
        public object Selected
        {
            get { return this._Selected; }
            set
            {
                if (this._Selected != value)
                {
                    this._Selected = value;
                    this.RaisedPropertyChanged("Selected");
                }
            }
        }

        
        private string _Id;
        public string Id
        {
            get { return this._Id; }
            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.RaisedPropertyChanged("Id");
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
                    this.RaisedPropertyChanged("Name");
                }
            }
        }
    }

    public class Demo:BaseModel
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
                    this.RaisedPropertyChanged("Id");
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
                    this.RaisedPropertyChanged("Name");
                }
            }
        }
    }

    
}
