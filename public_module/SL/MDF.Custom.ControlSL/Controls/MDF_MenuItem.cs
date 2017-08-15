using Microsoft.Expression.Media.Effects;
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
    [TemplateVisualState(GroupName = "CommonStates", Name = "MouseOver")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "Disabled")]
    public class MDF_MenuItem:ContentControl
    {
        Border border;
        MDFButton btnDownLoad;
        MDFButton btnBuy;
        Image img;

        public MDF_MenuItem()
        {
            this.DefaultStyleKey = typeof(MDF_MenuItem);

            ActiveImgBrush = Application.Current.Resources["img_ball_gray"] as ImageSource;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            border = this.GetTemplateChild("border") as Border;
            border.MouseEnter += border_MouseEnter;
            border.MouseLeave += border_MouseLeave;

            btnDownLoad = this.GetTemplateChild("btnDownLoad") as MDFButton;
            btnBuy = this.GetTemplateChild("btnBuy") as MDFButton;
            img = this.GetTemplateChild("img") as Image;

        }

        void border_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseLeave", false);
        }

        void border_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", false);
        }

        public string MenuName
        {
            get { return (string)GetValue(MenuNameProperty); }
            set { SetValue(MenuNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuNameProperty =
            DependencyProperty.Register("MenuName", typeof(string), typeof(MDF_MenuItem), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {


                    if(e.NewValue.ToString().Length > 9 )
                    {
                        string splitName = e.NewValue.ToString().Substring(0, 8) + "...";
                        (sender as MDF_MenuItem).MenuNameForSplit = splitName;
                    }
                    else
                    {
                        (sender as MDF_MenuItem).MenuNameForSplit = e.NewValue.ToString();
                    }
                })));



        public string MenuNameForSplit
        {
            get { return (string)GetValue(MenuNameForSplitProperty); }
            set { SetValue(MenuNameForSplitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuNameForSplit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuNameForSplitProperty =
            DependencyProperty.Register("MenuNameForSplit", typeof(string), typeof(MDF_MenuItem), new PropertyMetadata(""));


        



        public Visibility ShowBuyButton
        {
            get { return (Visibility)GetValue(ShowBuyButtonProperty); }
            set { SetValue(ShowBuyButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowBuyButtonProperty =
            DependencyProperty.Register("ShowBuyButton", typeof(Visibility), typeof(MDF_MenuItem), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback((sender, e) =>
                {
                    
                })));

        public Visibility ShowDownLoad
        {
            get { return (Visibility)GetValue(ShowDownLoadProperty); }
            set { SetValue(ShowDownLoadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDownLoad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDownLoadProperty =
            DependencyProperty.Register("ShowDownLoad", typeof(Visibility), typeof(MDF_MenuItem), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback((sender, e) =>
                {

                })));





        public string ActiveName
        {
            get { return (string)GetValue(ActiveNameProperty); }
            set { SetValue(ActiveNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveNameProperty =
            DependencyProperty.Register("ActiveName", typeof(string), typeof(MDF_MenuItem), new PropertyMetadata("未激活"));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }



        public ImageSource ActiveImgBrush
        {
            get { return (ImageSource)GetValue(ActiveImgBrushProperty); }
            set { SetValue(ActiveImgBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveImgBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveImgBrushProperty =
            DependencyProperty.Register("ActiveImgBrush", typeof(ImageSource), typeof(MDF_MenuItem), new PropertyMetadata(Application.Current.Resources["img_ball_gray"] as ImageSource));

        

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(MDF_MenuItem), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
                {
                    if((bool)e.NewValue)
                    {
                        (sender as MDF_MenuItem).ActiveName = "已激活";
                        (sender as MDF_MenuItem).ActiveImgBrush = Application.Current.Resources["img_ball_green"] as ImageSource;
                    }
                    else
                    {
                        (sender as MDF_MenuItem).ActiveName = "未激活";
                        (sender as MDF_MenuItem).ActiveImgBrush = Application.Current.Resources["img_ball_gray"] as ImageSource;
                    }

                })));


        public Visibility ShowDisabledImg
        {
            get { return (Visibility)GetValue(ShowDisabledImgProperty); }
            set { SetValue(ShowDisabledImgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowDisabledImg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDisabledImgProperty =
            DependencyProperty.Register("ShowDisabledImg", typeof(Visibility), typeof(MDF_MenuItem), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback((sender, e) =>
                {

                })));



        public Visibility ShowNormalImg
        {
            get { return (Visibility)GetValue(ShowNormalImgProperty); }
            set { SetValue(ShowNormalImgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowNormalImg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowNormalImgProperty =
            DependencyProperty.Register("ShowNormalImg", typeof(Visibility), typeof(MDF_MenuItem), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback((sender, e) =>
            {

            })));





        public double StarValue
        {
            get { return (double)GetValue(StarValueProperty); }
            set { SetValue(StarValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StarValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StarValueProperty =
            DependencyProperty.Register("StarValue", typeof(double), typeof(MDF_MenuItem), new PropertyMetadata(0.8d));

        

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MDF_MenuItem), new PropertyMetadata(null));

        
    }
}
