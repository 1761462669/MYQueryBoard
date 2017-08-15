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

namespace MDF.Custom.ControlSL.Controls
{
    [
        TemplateVisualState(GroupName = "Expander", Name = "Expanded"),
        TemplateVisualState(GroupName = "Expander", Name = "Closed"),
    ]
    public class MDFExpander:ContentControl
    {
        public MDFExpander()
        {
            this.DefaultStyleKey=typeof(MDFExpander);
        }


        ContentPresenter content;
        public event RoutedEventHandler Expaned;
        public event RoutedEventHandler Closed;
        
        public override void OnApplyTemplate()
        {            
            base.OnApplyTemplate();
            content = this.GetTemplateChild("content") as  ContentPresenter;
            GotoExpanderState();
        }       

        #region 属性定义

        /// <summary>
        /// 内容高度
        /// </summary>
        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register("ContentHeight", typeof(double), typeof(MDFExpander), new PropertyMetadata(20d, (sender, arg) => {
                
            }));
        

        /// <summary>
        /// Heard
        /// </summary>
        public object Heard
        {
            get { return (object)GetValue(HeardProperty); }
            set { SetValue(HeardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Heard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeardProperty =
            DependencyProperty.Register("Heard", typeof(object), typeof(MDFExpander), new PropertyMetadata(default(object)));


        public bool IsExpande
        {
            get { return (bool)GetValue(IsExpandeProperty); }
            set { SetValue(IsExpandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandeProperty =
            DependencyProperty.Register("IsExpande", typeof(bool), typeof(MDFExpander), new PropertyMetadata(true, (sender, arg) => {
                (sender as MDFExpander).GotoExpanderState();
            }));

        private void GotoExpanderState()
        {
            if (IsExpande)
                Expande();
            else
                Close();
        }

        private void Expande()
        {
            
            VisualStateManager.GoToState(this, "Expanded", true);
            
            if(Expaned != null)
                Expaned(this, new RoutedEventArgs());
           
        }

        private void Close()
        {
            VisualStateManager.GoToState(this, "Closed", true);
            if (Closed != null)
                Closed(this,new RoutedEventArgs());
        }

        #endregion
    }
}
