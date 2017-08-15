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
    [TemplateVisualState(GroupName = "CommonStates", Name = "Checked")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "UnChecked")]
    public class MDFButton : Button
    {
        public MDFButton()
        {
            this.DefaultStyleKey = typeof(MDFButton);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Checked)
            {
                VisualStateManager.GoToState(this, "Checked", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "UnChecked", false);
            }
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(MDFButton), new PropertyMetadata(null));


        public Visibility ShowImage
        {
            get { return (Visibility)GetValue(ShowImageProperty); }
            set { SetValue(ShowImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowImageProperty =
            DependencyProperty.Register("ShowImage", typeof(Visibility), typeof(MDFButton), new PropertyMetadata(Visibility.Visible));


        //add by xys 2015-04-04 按钮图片的宽度
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(MDFButton), new PropertyMetadata(null));


        //add by xys 2015-04-04 按钮图片的高度
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(MDFButton), new PropertyMetadata(null));

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Checked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked", typeof(bool), typeof(MDFButton), new PropertyMetadata(false, new PropertyChangedCallback((obj, arg) =>
            {
                if ((bool)arg.NewValue)
                {
                    VisualStateManager.GoToState(obj as Control, "Checked", false);
                }
                else
                {
                    VisualStateManager.GoToState(obj as Control, "UnChecked", false);
                }
            })));

        


    }
}
