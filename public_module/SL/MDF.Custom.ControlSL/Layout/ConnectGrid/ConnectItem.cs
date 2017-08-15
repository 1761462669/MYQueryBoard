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

namespace MDF.Custom.ControlSL.Layout.ConnectGrid
{
    [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MouseLeave", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
    [TemplateVisualState(Name = "Checked", GroupName = "CheckedStates")]
    [TemplateVisualState(Name = "UnChecked", GroupName = "CheckedStates")]
    public class ConnectItem : ContentControl
    {
        public ConnectItem()
        {
            this.DefaultStyleKey = typeof(ConnectItem);

        }

        public event RoutedEventHandler ItemChecked, ItemUnChecked;
        public event RoutedEventHandler ItemRightChecked, ItemRightUnChecked;
        public event RoutedEventHandler ItemClick;

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Checked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked", typeof(bool), typeof(ConnectItem), new PropertyMetadata(false, new PropertyChangedCallback((obj, arg) =>
            {
                if ((bool)arg.NewValue)
                {
                    VisualStateManager.GoToState(obj as Control, "Checked", false);
                    (obj as ConnectItem).checkEvent();
                }
                else
                {
                    VisualStateManager.GoToState(obj as Control, "UnChecked", false);
                }
            })));


        private void checkEvent()
        {

            if (ItemChecked != null)
            {
                if (Checked)
                    ItemChecked(this, new RoutedEventArgs());
                else
                    ItemUnChecked(this, new RoutedEventArgs());
            }
        }
        #region 事件到状态切换
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            VisualStateManager.GoToState(this, "Focused", false);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            VisualStateManager.GoToState(this, "Unfocused", false);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            VisualStateManager.GoToState(this, "MouseLeave", true);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.Checked = this.Checked ? false : true;

            if (ItemClick != null)
            {
                ItemClick(this, new RoutedEventArgs());
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (ItemRightChecked != null)
            {
                ItemRightChecked(this, new RoutedEventArgs());
            }
        }

        #endregion

        #region 背景图片 added by zongyh 2014/11/17

        public ImageSource BgImageSource
        {
            get { return (ImageSource)GetValue(BgImageSourceProperty); }
            set { SetValue(BgImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BgImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BgImageSourceProperty =
            DependencyProperty.Register("BgImageSource", typeof(ImageSource), typeof(ConnectItem), new PropertyMetadata(null, new PropertyChangedCallback((obj, arg) =>
            {
                ImageBrush imagebrush = new ImageBrush();
                imagebrush.ImageSource = arg.NewValue as ImageSource;

                (obj as ConnectItem).Background = imagebrush;
            })));
        

        #endregion
    }
}
