using System;
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
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace MDF.Custom.ControlSL.Controls
{
    public interface ITabItemModel
    {
        string Header { get; set; }
    }

    public class TabItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable source = value as IEnumerable;

            if (source != null)
            {
                List<TabItem> result = new List<TabItem>();

                var ien = source.GetEnumerator();

                while (ien.MoveNext())
                {
                    if (ien.Current is ITabItemModel)
                    {
                        var obj = ien.Current as ITabItemModel;

                        result.Add(new TabItem()
                        {
                            Header = obj.Header,
                            DataContext = obj,
                            Content = new TextBlock() { Text = obj.Header }
                        });
                    }
                }

                return result;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class MDFTabItem : TabItem
    {
        public MDFTabItem()
        {
            this.DefaultStyleKey = typeof(TabItem);
        }
    }

    public class MDFTabControl : TabControl
    {
        public MDFTabControl()
        {
            this.DefaultStyleKey = typeof(TabControl);
        }
    }
}
