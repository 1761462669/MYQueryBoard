using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SMES.Framework.Converter
{
    public class ViewStateToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ModelViewState && parameter != null)
            {
                var state = (int)value;
                var paraValue = parameter.ToString().Split('/');
                if (paraValue.Contains(((int)value).ToString()))
                    return Visibility.Visible;


            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
