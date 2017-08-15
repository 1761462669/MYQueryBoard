using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Converter
{
    public class CollectionViewGroupConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IList<CollectionViewGroup> resultList = new List<CollectionViewGroup>();
            if (value == null)
                return null;
            if (value is CustomDataType)
            {
                for (int i = 0; i < (value as CustomDataType).Items.Count; i++)
			      {
                      resultList.Add((value as CustomDataType).Items[i] as CollectionViewGroup);
			      }
                 return resultList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
