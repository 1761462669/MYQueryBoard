using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFDeleteConfirmButton : ContentControl
    {
        public event RoutedEventHandler Click;

        Image img;
        Popup popup;
        HyperlinkButton btnYes;
        MDFButton btn;

        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(MDFDeleteConfirmButton), new PropertyMetadata(null));




        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(double), typeof(MDFDeleteConfirmButton), new PropertyMetadata(80d));



        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register("ButtonHeight", typeof(double), typeof(MDFDeleteConfirmButton), new PropertyMetadata(24d));



        public Thickness ButtonMargin
        {
            get { return (Thickness)GetValue(ButtonMarginProperty); }
            set { SetValue(ButtonMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonMarginProperty =
            DependencyProperty.Register("ButtonMargin", typeof(Thickness), typeof(MDFDeleteConfirmButton), new PropertyMetadata(null));








        public MDFDeleteConfirmButton()
        {
            this.DefaultStyleKey = typeof(MDFDeleteConfirmButton);
        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DesignerProperties.IsInDesignTool) return;

            img = this.GetTemplateChild("img") as Image;
            popup = this.GetTemplateChild("popup") as Popup;
            btnYes = this.GetTemplateChild("btnYes") as HyperlinkButton;
            if (btnYes != null)
                btnYes.Click += btnYes_Click;

            btn = this.GetTemplateChild("btn") as MDFButton;
            if (ButtonStyle != null)
            {
                btn.Style = ButtonStyle;
            }
            else
            {
                this.btn.Width = ButtonWidth;
                this.btn.Height = ButtonHeight;
            }

            if (this.Content != null)
            { 
                btn.Content = Content;
            }
            if (this.ButtonMargin != null)
            {
                btn.Margin = ButtonMargin;
            }



        }

        void btnYes_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                popup.IsOpen = false;
                Click(this, new RoutedEventArgs());
            }
        }
    }
}
