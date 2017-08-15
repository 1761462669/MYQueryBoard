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
    public class MDFLayoutGroupPanel : ContentControl
    {
        public MDFLayoutGroupPanel()
        {
            this.DefaultStyleKey = typeof(MDFLayoutGroupPanel);
        }

        public object HeaderRightContent
        {
            get { return (object)GetValue(HeaderRightContentProperty); }
            set { SetValue(HeaderRightContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderRightContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderRightContentProperty =
            DependencyProperty.Register("HeaderRightContent", typeof(object), typeof(MDFLayoutGroupPanel), new PropertyMetadata(null));




        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MDFLayoutGroupPanel), new PropertyMetadata(""));



        public double HeaderWidth
        {
            get { return (double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth", typeof(double), typeof(MDFLayoutGroupPanel), new PropertyMetadata(0d));



        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(MDFLayoutGroupPanel), new PropertyMetadata(0d));

    }
}
