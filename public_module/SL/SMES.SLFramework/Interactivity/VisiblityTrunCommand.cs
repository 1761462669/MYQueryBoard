using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Interactivity
{
    [ContentProperty("Children")]
    public class VisiblityTrunCommand : DependencyObject, ICommand
    { 
        public List<UIElement> TargetObjects { get; set; }

        public UIElement TargetObject
        {
            get { return (UIElement)GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetObjectProperty =
            DependencyProperty.Register("TargetObject", typeof(UIElement), typeof(VisiblityTrunCommand), new PropertyMetadata(null));

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (this.TargetObject == null)
                return;

            if (this.TargetObject.Visibility == Visibility.Visible)
                this.TargetObject.Visibility = Visibility.Collapsed;

            else
                this.TargetObject.Visibility = Visibility.Visible;
        }
    }
}
