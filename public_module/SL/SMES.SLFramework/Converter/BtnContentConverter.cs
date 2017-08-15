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

namespace SMES.Framework.Converter
{
    /// <summary>
    /// 基础数据 编辑开关 转换器
    /// added by yangyang 2015-4-6
    /// </summary>
    public class BtnContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var flag = (bool)value;
            return flag ? "结束编辑" : "开始编辑";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 基础数据 回收站 转换器
    /// added by yangyang 2015-4-8
    /// </summary>
    public class RbContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var flag = (bool)value;
            return flag ? "正常数据" : "回收站数据";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BtnHideConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
     {
 	        var flag = (bool)value;
            return flag ? "隐藏未启用的标准" : "查询未启用的标准";
      }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
     {
 	        throw new NotImplementedException();
      }
   }

}
