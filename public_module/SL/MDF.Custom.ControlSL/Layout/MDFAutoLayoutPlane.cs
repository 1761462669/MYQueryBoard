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
using System.Collections.Generic;
using System.Collections;

namespace MDF.Custom.ControlSL.Layout
{
    public class MDFAutoLayoutPlane:ContentControl
    {
        Grid grid;

        public MDFAutoLayoutPlane()
        {
            this.DefaultStyleKey = typeof(MDFAutoLayoutPlane);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            grid = this.GetTemplateChild("grid") as Grid;
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(MDFAutoLayoutPlane), new PropertyMetadata(Orientation.Vertical));

        public IEnumerable<MDFAutoLayoutPlaneItem> Items { get; set; }
    }

    public class MDFAutoLayoutPlaneItem:ContentControl
    {
        public MDFAutoLayoutPlaneItem()
        {
            this.DefaultStyleKey = typeof(MDFAutoLayoutPlaneItem);
        }
    }
}
