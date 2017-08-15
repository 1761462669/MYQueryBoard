using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SMES.Framework.Behaviors
{
    public class DoubleClickBehavior : Behavior<FrameworkElement>
    {

        public event EventHandler<MouseButtonEventArgs> DoubleClickEvent = null;
        private DateTime? clickTime;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject != null)
            {
               
                this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            }
            var btn = this.AssociatedObject as Button;
            if(btn!=null)
            {
                //btn.ClickMode = ClickMode.Release;
               //btn.AddHandler(t)


            }
            
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (clickTime == null)
            {
                clickTime = DateTime.Now;
                return;
            }
            var span = DateTime.Now - clickTime.Value;
            if (span.TotalMilliseconds <= 200)
            {
                if (DoubleClickEvent != null)
                {
                    DoubleClickEvent(sender, e);
                }
            }
            clickTime = DateTime.Now;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        }
    }
}
