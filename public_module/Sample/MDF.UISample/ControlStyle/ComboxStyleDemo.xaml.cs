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
    public partial class ComboxStyleDemo : UserControl
    {
        public ComboxStyleDemo()
        {
            InitializeComponent();

            List<Person> ps = new List<Person>() 
            { 
                new Person(){ Id="1",Name="adminaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"},
                new Person(){Id="2",Name="xxaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"}
            };

            this.DataContext = ps;
        }

        ComboBox a;


    }
}
