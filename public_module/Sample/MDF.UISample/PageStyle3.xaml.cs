
using MDF.Custom.ControlSL;
using MDF.Custom.ControlSL.Controls;
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

namespace MDF.UISample
{
    public partial class PageStyle3 : PageBase
    {
        public PageStyle3()
        {
            InitializeComponent();

            this.Loaded += PageStyle3_Loaded;
        }

        void PageStyle3_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = new MDF.MES.BusinessControlSL.EntityBusinessVM<Material>(
            //    this,
            //    this.Resources["queryItemList"] as QueryItemList);
        }

        private void MDFButton_Click(object sender, RoutedEventArgs e)
        {
            DataTemplate test = new DataTemplate();
            Action showMethod = DisplayToWindow;
            //MDFChildWindowHelper.ShowCWindow(this.msgwin, test, showMethod);
        }

        public void DisplayToWindow()
        {
            MessageBox.Show("xxx");
            //MDFChildWindowHelper.CloseCWindow(this.msgwin);
        }
    }
}
