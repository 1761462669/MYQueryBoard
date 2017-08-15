using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Interactivity
{
    /// <summary>
    /// 是否可视轮转
    /// 需指定targetObject
    /// </summary>
    public class VisiblityTrunTriggerAction : TargetedTriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            if (this.Target == null)
                return;

            if (this.Target.Visibility == Visibility.Visible)
                this.Target.Visibility = Visibility.Collapsed;

            else
                this.Target.Visibility = Visibility.Visible;
        }
    }
}
