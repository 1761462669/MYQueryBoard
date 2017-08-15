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
    public class PortalRequest
    {
        public Action<OpenReadCompletedEventArgs> CompletedAction
        {
            get;
            set;
        }

        public void Request(string filepath)
        {
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += wc_OpenReadCompleted;
            wc.OpenReadAsync(new Uri(filepath, UriKind.Relative));
        }

        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {            
            if (CompletedAction != null)
                CompletedAction.Invoke(e);
        }
    }
}
