using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xaml;
using System.Linq;
using SMES.FrameworkAdpt.CommonPara.Model;
using System.ComponentModel;

namespace SMES.Framework.Markup
{
    public class CommonPara : IMarkupExtension<string>
    {
        public static Dictionary<string, string> dic = new Dictionary<string, string>();

        public string KeyCode { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.IsInDesignTool) return "";

            if (!dic.Keys.Contains(KeyCode))
            {
                throw new Exception(KeyCode + "不存在");
            }

            return dic.FirstOrDefault(c => c.Key == KeyCode).Value;
        }
  
        public static string GetPara(string keyCode)
        {
            if (DesignerProperties.IsInDesignTool) return "";

            if (!dic.Keys.Contains(keyCode))
            {
                throw new Exception(keyCode + "不存在");
            }

            return dic.FirstOrDefault(c => c.Key == keyCode).Value;
        }
    }
}
