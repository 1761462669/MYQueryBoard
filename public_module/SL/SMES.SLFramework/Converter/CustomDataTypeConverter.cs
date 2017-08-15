using System;
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
    public class CustomDataTypeConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IList<CustomDataType> resultList = new List<CustomDataType>();
            if (value == null)
                return null;
            if (value is CollectionViewGroup)
            {
                for (int i = 0; i < (value as CollectionViewGroup).Items.Count; i++)
                {
                    CustomDataType cdt = new CustomDataType();
                    cdt.Name = ((value as CollectionViewGroup).Items[i] as CollectionViewGroup).Name.ToString();
                    cdt.Items = ((value as CollectionViewGroup).Items[i] as CollectionViewGroup).Items;
                    cdt.ItemCount = ((value as CollectionViewGroup).Items[i] as CollectionViewGroup).ItemCount;
                    cdt.IsExpander = false;
                    resultList.Add(cdt);
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
