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

namespace MDF.Custom.ControlSL.Controls.Calendar
{
    public class DragMouseControl : Control
    {
        public DragMouseControl()
        {
            this.DefaultStyleKey = typeof(DragMouseControl);
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }

        public ImageSource MouseImage
        {
            get { return (ImageSource)GetValue(MouseImageProperty); }
            set { SetValue(MouseImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseImageProperty =
            DependencyProperty.Register("MouseImage", typeof(ImageSource), typeof(DragMouseControl), new PropertyMetadata(default(ImageSource)));

    }
}
