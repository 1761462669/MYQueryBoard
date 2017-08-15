using SMES.Framework;
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
    [TemplateVisualState(Name = "Closed", GroupName = "WindowStates")]
    public class MDFPopupWindow:ChildWindow
    {


        public object Para
        {
            get { return (object)GetValue(ParaProperty); }
            set { SetValue(ParaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Para.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParaProperty =
            DependencyProperty.Register("Para", typeof(object), typeof(MDFPopupWindow), new PropertyMetadata(null));

        
        Image imageClose;
        Grid overlay;

        public MDFPopupWindow()
        {
            this.DefaultStyleKey = typeof(MDFPopupWindow);
            this.Closing += MDFPopupWindow_Closing;
        }

        void MDFPopupWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //关闭前 自动清除错误信息
            object modelObj = null;

            var control = SMES.Framework.Utility.VisualTreeHelper.GetChildObject<TextBox>(this);
            if (control != null)
            {
                var binding = control.GetBindingExpression(TextBox.TextProperty);
                if(binding != null)
                {
                    modelObj = binding.DataItem;
                }
            }
            else
            {
                ComboBox cbx = SMES.Framework.Utility.VisualTreeHelper.GetChildObject<ComboBox>(this);
                if(cbx != null)
                {
                    var binding = cbx.GetBindingExpression(ComboBox.SelectedItemProperty);
                    if (binding != null)
                    {
                        modelObj = binding.DataItem;
                    }
                }
            }

            if(modelObj != null)
            {
                var props = modelObj.GetType().GetProperties();

                foreach (var prop in props)
                {
                    if (modelObj is RootModel)
                    {
                        (modelObj as RootModel).ClearErrors(prop.Name);
                        (modelObj as RootModel).RaiseErrorChanged(prop.Name);
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            imageClose = this.GetTemplateChild("imageClose") as Image;
            if (imageClose != null)
            {
                imageClose.MouseLeftButtonDown += imageClose_MouseLeftButtonDown;
            }


            overlay = this.GetTemplateChild("Overlay") as Grid;

            //if(overlay != null)
            //{
            //    VisualState state = this.GetTemplateChild("Open") as VisualState;
            //    state.Storyboard.Completed += Storyboard_Completed;
            //}
        }

        void Storyboard_Completed(object sender, EventArgs e)
        {
            overlay.MouseLeftButtonDown += overlay_MouseLeftButtonDown;
        }

        void overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            overlay.MouseLeftButtonDown -= overlay_MouseLeftButtonDown;
            this.Close();
        }

        void imageClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imageClose.MouseLeftButtonDown -= imageClose_MouseLeftButtonDown;
            this.Close();
        }
    }
}
