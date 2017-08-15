using MDF.Framework.Model;
using SMES.Framework;
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

namespace MDF.UISample.ControlStyle
{
    public partial class ValidationTest : UserControl
    {
        public ValidationTest()
        {
            InitializeComponent();

            this.DataContext = new Model();
        }

        private void txtAge_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtName.Text = this.txtName.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtName_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }

    public class Model : RootModel
    {
        public Model()
        {
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


        private string _Age;
        public string Age
        {
            get { return this._Age; }
            set
            {
                if (this._Age != value)
                {
                    this._Age = value;
                    this.RaisePropertyChanged("Age");
                }
            }
        }


        
    }
}
