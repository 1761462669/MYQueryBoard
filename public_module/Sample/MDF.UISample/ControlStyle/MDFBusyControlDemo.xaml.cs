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
    public partial class MDFBusyControlDemo : UserControl
    {
        public MDFBusyControlDemo()
        {
            InitializeComponent();


            //List<string> list = new List<string>();
            //Random random = new Random();

            //while(true)
            //{
            //    string value = random.Next(0, 301).ToString();

            //    list.Add(value);

            //    if (list.Count == 20) break;
            //}

            //items.ItemsSource = list;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            busy.IsBusy = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            busy.IsBusy = false;
        }
    }
}
