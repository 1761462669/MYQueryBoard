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

namespace MDF.Custom.ControlSL.Controls
{
    public class LableField : ContentControl
    {
        public LableField()
        {
            this.DefaultStyleKey = typeof(LableField);
        }

        #region 属性定义


        public object Heard
        {
            get { return (object)GetValue(HeardProperty); }
            set { SetValue(HeardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Heard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeardProperty =
            DependencyProperty.Register("Heard", typeof(object), typeof(LableField), new PropertyMetadata(null));




        public object Description
        {
            get { return (object)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(object), typeof(LableField), new PropertyMetadata(null));

        




        public double HeardWidth
        {
            get { return (double)GetValue(HeardWidthProperty); }
            set { SetValue(HeardWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeardWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeardWidthProperty =
            DependencyProperty.Register("HeardWidth", typeof(double), typeof(LableField), new PropertyMetadata(double.NaN));


        public HorizontalAlignment HeardHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeardHorizontalAlignmentProperty); }
            set { SetValue(HeardHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeardHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeardHorizontalAlignmentProperty =
            DependencyProperty.Register("HeardHorizontalAlignment", typeof(HorizontalAlignment), typeof(LableField), new PropertyMetadata(HorizontalAlignment.Left));


        #endregion
    }
}
