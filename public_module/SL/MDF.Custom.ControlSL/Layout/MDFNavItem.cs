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

namespace MDF.Custom.ControlSL.Layout
{
    public class MDFNavItem:ContentControl
    {
        public MDFNavItem()
        {
            this.DefaultStyleKey=typeof(MDFNavItem);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }





        public object Heard
        {
            get { return (object)GetValue(HeardProperty); }
            set { SetValue(HeardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Heard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeardProperty =
            DependencyProperty.Register("Heard", typeof(object), typeof(MDFNavItem), new PropertyMetadata(default(object)));


        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Checked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked", typeof(bool), typeof(MDFNavItem), new PropertyMetadata(false));

        
        
    }
}
