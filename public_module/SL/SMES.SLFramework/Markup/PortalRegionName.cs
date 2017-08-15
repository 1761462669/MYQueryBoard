using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xaml;

namespace SMES.Framework.Markup
{
    public class PortalRegionName : IMarkupExtension<string>
    {
        public string ProvideValue(IServiceProvider serviceProvider)
        {
            return "Portal_Main_Pages";
        }
    }
}
