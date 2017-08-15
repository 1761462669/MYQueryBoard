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

namespace SMES.Framework.Controls
{
    /// <summary>
    /// Added by changhl,2015.4.8
    /// </summary>
    public class SmesSelctor : ComboBox
    {
        #region feilds && Properties


        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(SmesSelctor), new PropertyMetadata(null));

        
        #endregion
        public SmesSelctor()
        {
            this.DefaultStyleKey = typeof(ComboBox);
        }
    }
}
