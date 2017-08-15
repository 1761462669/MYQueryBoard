using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Tools
{
    public static class ColorUnity
    {
        public static Color HtmlToColor(string htmlColr)
        {
            int baseIndex = 1;
            byte a, r, g, b;
            a = r = g = b = 255;
            if (htmlColr.Length == 9)
            {
                a = System.Convert.ToByte(htmlColr.Substring(baseIndex, 2), 16);
                baseIndex += 2;
            }
            r = System.Convert.ToByte(htmlColr.Substring(baseIndex, 2), 16);
            g = System.Convert.ToByte(htmlColr.Substring(baseIndex += 2, 2), 16);
            b = System.Convert.ToByte(htmlColr.Substring(baseIndex += 2, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
    }
}
