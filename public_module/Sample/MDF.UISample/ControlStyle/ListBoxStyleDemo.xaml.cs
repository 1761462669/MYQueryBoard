using MDF.Custom.ControlSL;
using MDF.Framework;
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

namespace MDF.UISample.ControlStyle
{
    public partial class ListBoxStyleDemo : PageBase, System.ComponentModel.INotifyPropertyChanged
    {
        ViewModelPerson vm;

        public ListBoxStyleDemo()
        {
            InitializeComponent();

            this.Loaded += ListBoxStyleDemo_Loaded;
        }

        void ListBoxStyleDemo_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new ViewModelPerson();

            for (int i = 0; i < 3; i++)
            {
                Person p = new Person();
                p.Name = "Testfdsfdsfsd" + i.ToString();
                vm.Datas.Add(p);
            }

            this.DataContext = vm;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }

        }

    }

    public class ViewModelPerson : BaseModel
    {

        private ObservableCollection<Person> _datas = new ObservableCollection<Person>();
        public ObservableCollection<Person> Datas
        {
            get { return this._datas; }
            set
            {
                this._datas = value;
                this.RaisedPropertyChanged("Datas");
            }
        }
    }
}
