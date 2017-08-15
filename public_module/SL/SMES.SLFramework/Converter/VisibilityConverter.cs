using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SMES.Framework.Converter
{
    /// <summary>
    /// Visibility正向转换器
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool visible = false;
            if (value != null)
            {
                if (value is IList)
                {
                    var list = value as IList;
                    if (list.Count > 0)
                        visible = true;
                }
                else if ((bool)value)
                    visible = true;

            }
            if (parameter != null && (bool)parameter)
                visible = !visible;


            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible ? true : false;
            }
            return (bool)value;
        }
    }
}
