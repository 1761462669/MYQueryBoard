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
    public class MDF_RecyleControl : ContentControl
    {
        Image image = null;
        public MDF_RecyleControl()
        {
            this.DefaultStyleKey = typeof(MDF_RecyleControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            image = this.GetTemplateChild("image") as Image;

            if (IsEmpty)
            {
                Image = Application.Current.Resources["img_control_bg_recycle_empty"] as ImageSource;
            }
            else
            {
                Image = Application.Current.Resources["img_control_bg_recycle_full"] as ImageSource;
            }
            if (IsChilds)
            {
                Image = Application.Current.Resources["Folder_style_72"] as ImageSource;
            }
            else
            {
                Image = Application.Current.Resources["img_control_SMESfile"] as ImageSource;
            }
        }

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            set { SetValue(IsEmptyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEmpty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEmptyProperty =
            DependencyProperty.Register("IsEmpty", typeof(bool), typeof(MDF_RecyleControl), new PropertyMetadata
                (false, new PropertyChangedCallback((sender, arg) =>
                {
                    if ((bool)arg.NewValue)
                    {
                        (sender as MDF_RecyleControl).Image = Application.Current.Resources["img_control_bg_recycle_empty"] as ImageSource;
                    }
                    else
                    {
                        (sender as MDF_RecyleControl).Image = Application.Current.Resources["img_control_bg_recycle_full"] as ImageSource;
                    }
                })));



        public bool IsChilds
        {
            get { return (bool)GetValue(IsChildsProperty); }
            set { SetValue(IsChildsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChilds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildsProperty =
            DependencyProperty.Register
            ("IsChilds", typeof(bool), typeof(MDF_RecyleControl), new PropertyMetadata
                (false, new PropertyChangedCallback((sender, arg) =>
                {
                 if ((bool)arg.NewValue)
                    {
                        (sender as MDF_RecyleControl).Image = Application.Current.Resources["Folder_style_72"] as ImageSource;
                    }
                    else
                    {
                        (sender as MDF_RecyleControl).Image = Application.Current.Resources["img_control_SMESfile"] as ImageSource;
                    }
                }
                )
                ));



        
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(MDF_RecyleControl), new PropertyMetadata(null));


    }
}
