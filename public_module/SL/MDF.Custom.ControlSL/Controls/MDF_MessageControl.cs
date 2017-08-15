using MDF.Custom.ControlSL.Tools;
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
    [TemplateVisualState(GroupName = "DataState", Name = "Show")]
    [TemplateVisualState(GroupName = "DataState", Name = "Hide")]
    public class MDF_MessageControl : ContentControl
    {
        Storyboard storyShow;
        TextBlock lblMsg;
        Image imageClose;
        Image imgInfo;

        public MDF_MessageControl()
        {
            this.DefaultStyleKey = typeof(MDF_MessageControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            storyShow = this.GetTemplateChild("storyShow") as Storyboard;
            lblMsg = this.GetTemplateChild("lblMsg") as TextBlock;

            imageClose = this.GetTemplateChild("imageClose") as Image;
            imageClose.MouseLeftButtonDown += imageClose_MouseLeftButtonDown;

            imgInfo = this.GetTemplateChild("imgInfo") as Image;
            imgInfo.MouseLeftButtonDown += imgInfo_MouseLeftButtonDown;
        }

        void imgInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(lblMsg.Text);
        }

        void imageClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.storyShow.SkipToFill();
        }

        public int State
        {
            get { return (int)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(int), typeof(MDF_MessageControl), new PropertyMetadata(3, OnPropertyChangedCallback));

        public string MsgText
        {
            get { return (string)GetValue(MsgTextProperty); }
            set { SetValue(MsgTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MsgText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MsgTextProperty =
            DependencyProperty.Register("MsgText", typeof(string), typeof(MDF_MessageControl), new PropertyMetadata("", OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MDF_MessageControl obj = sender as MDF_MessageControl;

            if (obj.storyShow == null) return;
            if (string.IsNullOrEmpty(obj.MsgText)) return;

            if(obj.MsgText.Contains("@"))
            {
                obj.lblMsg.Text = obj.MsgText.Substring(0, obj.MsgText.LastIndexOf("@"));
            }

            obj.storyShow.Begin();

            (sender as MDF_MessageControl).Background = new SolidColorBrush(ColorUnity.HtmlToColor("#FFFFFBA3"));
            (sender as MDF_MessageControl).Foreground = new SolidColorBrush(Colors.Black);

            if (obj.State == 1)
            {

                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5,0);
                brush.EndPoint = new Point(0.5, 1);

                GradientStop g1 = new GradientStop() { Color = ColorUnity.HtmlToColor("#FF1FD616"), Offset=1 };
                GradientStop g2 = new GradientStop() { Color = ColorUnity.HtmlToColor("#FF1BB914"), Offset = 1 };
                GradientStop g3 = new GradientStop() { Color = ColorUnity.HtmlToColor("#FF179111"), Offset = 0.006 };

                brush.GradientStops.Add(g1);
                brush.GradientStops.Add(g2);
                brush.GradientStops.Add(g3);

                (sender as MDF_MessageControl).Background = brush;
                (sender as MDF_MessageControl).Foreground = new SolidColorBrush(Colors.White);
            }

            if (obj.State == 2)
            {
                (sender as MDF_MessageControl).Background = new SolidColorBrush(Colors.Red);
                (sender as MDF_MessageControl).Foreground = new SolidColorBrush(Colors.White);
            }

            if (obj.State == 3)
            {
                (sender as MDF_MessageControl).Background = new SolidColorBrush(ColorUnity.HtmlToColor("#FFFFFBA3"));
                (sender as MDF_MessageControl).Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
