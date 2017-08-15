using SMES.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SMES.Framework.Converter
{
    public class ModelWrapperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var model = value as RootModel;

            if (model != null)
            {
                if (model.Wrapper != null)
                {
                    return model.Wrapper;
                }
                else
                {
                    var wraper = new ModelWrapper() { Model = model, State = ModelState.View };
                    model.Wrapper = wraper;
                    return wraper;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var wrapper = value as ModelWrapper;
            if (wrapper != null)
                return wrapper.Model;
            return value;

        }
    }
}
