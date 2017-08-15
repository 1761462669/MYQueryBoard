using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls.ScheduleGant
{
    [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MouseLeave", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
    [TemplateVisualState(Name = "Checked", GroupName = "CheckedStates")]
    [TemplateVisualState(Name = "UnChecked", GroupName = "CheckedStates")]
    public class SchedulePanel : ContentControl
    {
        public SchedulePanel()
        {
            this.DefaultStyleKey = typeof(SchedulePanel);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public DataTemplate DataTemplate
        {
            get { return (DataTemplate)GetValue(DataTemplateProperty); }
            set { SetValue(DataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTemplateProperty =
            DependencyProperty.Register("DataTemplate", typeof(DataTemplate), typeof(SchedulePanel), new PropertyMetadata(null));

        
    }
}
