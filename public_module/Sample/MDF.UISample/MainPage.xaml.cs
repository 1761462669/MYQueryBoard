using MDF.Framework.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.UISample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }


    public class FunctionPoint : NotifyObject
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



        private string _Uri;
        public string Uri
        {
            get { return this._Uri; }
            set
            {
                if (this._Uri != value)
                {
                    this._Uri = value;
                    this.RaisePropertyChanged("Uri");
                }
            }
        }
    }

    public class FucntionPoints : ObservableCollection<FunctionPoint>
    {

        private FunctionPoint _Selected;
        public FunctionPoint Selected
        {
            get { return this._Selected; }
            set
            {
                if (this._Selected != value)
                {
                    this._Selected = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
                }
            }
        }
    }
}
