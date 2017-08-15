using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Browser;

namespace SMES.Framework.Utility
{
    /// <summary>
    /// added by changhl,2015.6.10
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// 获取屏幕分辨率
        /// </summary>
        public static Size ScreenSize
        {
            get
            {
                var width = (double)HtmlPage.Window.Eval("screen.width");
                var height = (double)HtmlPage.Window.Eval("screen.Height");
                return new Size(width, height);

            }
        }

        public static Size PageSize
        {
            get
            {
                return new Size(Application.Current.Host.Content.ActualWidth, Application.Current.Host.Content.ActualHeight);
            }
        }


        public static double ZoomFactor
        {
            get { return Application.Current.Host.Content.ZoomFactor; }
        }
    }
}
