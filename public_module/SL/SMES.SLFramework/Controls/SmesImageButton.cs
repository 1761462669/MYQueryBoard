using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Controls
{
    [TemplateVisualState(Name = "IsBusy", GroupName = "BusyStates")]
    [TemplateVisualState(Name = "IsFree", GroupName = "BusyStates")]
    public class SmesImageButton : Button
    {
        public SmesImageButton()
        {
            this.DefaultStyleKey = typeof(SmesImageButton);

        }



        public ImageSource NormalImageSource
        {
            get { return (ImageSource)GetValue(NormalImageSourceProperty); }
            set { SetValue(NormalImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalImageSourceProperty =
            DependencyProperty.Register("NormalImageSource", typeof(ImageSource), typeof(SmesImageButton), new PropertyMetadata(null));




        public ImageSource PressedImageSource
        {
            get { return (ImageSource)GetValue(PressedImageSourceProperty); }
            set { SetValue(PressedImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PressedImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedImageSourceProperty =
            DependencyProperty.Register("PressedImageSource", typeof(ImageSource), typeof(SmesImageButton), new PropertyMetadata(null));



        public ImageSource MouseOverImageSource
        {
            get { return (ImageSource)GetValue(MouseOverImageSourceProperty); }
            set { SetValue(MouseOverImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverImageSourceProperty =
            DependencyProperty.Register("MouseOverImageSource", typeof(ImageSource), typeof(SmesImageButton), new PropertyMetadata(null));




        public ImageSource IsBusyImageSource
        {
            get { return (ImageSource)GetValue(IsBusyImageSourceProperty); }
            set { SetValue(IsBusyImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusyImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyImageSourceProperty =
            DependencyProperty.Register("IsBusyImageSource", typeof(ImageSource), typeof(SmesImageButton), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //VisualStateManager.GoToState(this,)
        }
    }
}
