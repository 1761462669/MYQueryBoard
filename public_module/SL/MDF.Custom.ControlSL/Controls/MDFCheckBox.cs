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

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFCheckBox:CheckBox
    {
        public MDFCheckBox()
        {
            this.DefaultStyleKey = typeof(MDFCheckBox);
        }


        public object CheckedContent
        {
            get { return (object)GetValue(CheckedContentProperty); }
            set { SetValue(CheckedContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckedContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.Register("CheckedContent", typeof(object), typeof(MDFCheckBox), new PropertyMetadata(default(object)));


    }
}
