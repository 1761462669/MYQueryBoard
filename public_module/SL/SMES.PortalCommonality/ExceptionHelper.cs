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

namespace SMES.PortalCommonality
{
    public class ExceptionHelper
    {
        public static string GetExceptionString(Exception ex)
        {
            string msg = "";

            if (ex == null)
                return msg;

            msg = ex.Message;
            if (ex.InnerException != null)
                msg = msg + "\r\n " + GetExceptionString(ex.InnerException);

            return msg;
        }
    }
}
