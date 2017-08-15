
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
    public partial class PageStyle2 : PageBase
    {
        public PageStyle2()
        {
            InitializeComponent();

            this.Loaded += PageStyle2_Loaded;
        }

        void PageStyle2_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = new MDF.MES.BusinessControlSL.EntityBusinessVM<Material>(
            //    this,
            //    this.Resources["queryItemList"] as QueryItemList);
        }

        private void MDFButton_Click(object sender, RoutedEventArgs e)
        {
            DataTemplate test = new DataTemplate();
            Action showMethod = DisplayToWindow;
            MDFChildWindowHelper.ShowCWindow(this.msgwin, test, showMethod);

            //MDFChildWindowHelper.ShowMessage(msgwin, "阿克苏飞拉伸法啊；浪费就阿里斯蒂芬骄傲了是否的骄傲是浪费骄傲是浪费就爱上；两地分居爱上；罗的付款就爱上；率大幅度叫撒；辅料卷爱上；法律所");

            //msgwin.Title = "系统提示";
            //msgwin.Content = "";
            //msgwin.Shown = true;
        }

        public void DisplayToWindow()
        {
            MessageBox.Show("xxx");
            MDFChildWindowHelper.CloseCWindow(this.msgwin);
        }
    }
}
