using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls.ScheduleGant
{
    public class RectAreaFillControl : ContentControl
    {
        public RectAreaFillControl()
        {
            this.DefaultStyleKey = typeof(RectAreaFillControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #region 依赖属性

        public double FillHeight
        {
            get { return (int)GetValue(FillHeightProperty); }
            set { SetValue(FillHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FillHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillHeightProperty =
            DependencyProperty.Register("FillHeight", typeof(double), typeof(RectAreaFillControl), new PropertyMetadata(20d));

        #endregion
    }
}

