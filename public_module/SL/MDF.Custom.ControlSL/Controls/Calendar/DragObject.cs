using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MDF.Custom.ControlSL.Tools;

namespace MDF.Custom.ControlSL.Controls.Calendar
{
    public delegate void DragObjectHandler(DragObject obj);
    public class DragObject : DependencyObject
    {
        public event DragObjectHandler OnDragObjectEvent;
        public event DragObjectHandler OnDragObjectCompleteEvent;

        public bool MouseLeftDown
        { get; set; }

        public Point DownPoint
        { get; set; }

        public DragMouseControl MouseImage
        { get; set; }

        public UIElement Control
        { get; set; }

        public UIElement ToControl
        { get; set; }

        public Panel Parent
        { get; set; }



        public bool AllowDrag
        {
            get { return (bool)GetValue(AllowDragProperty); }
            set { SetValue(AllowDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowDrag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register("AllowDrag", typeof(bool), typeof(DragObject), new PropertyMetadata(false));

        private DragState _state;
        public DragState State
        {
            get {
                return _state;
            }
            set {
                _state = value;
                if (MouseImage == null)
                    return;

                switch (value)
                { 
                    case DragState.Normal:
                        MouseImage.BorderBrush = null;
                        MouseImage.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        break;
                    case DragState.Allow:
                        MouseImage.BorderBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FF0F74D8"));
                        break;
                    case DragState.NoAllow:
                        MouseImage.BorderBrush = new SolidColorBrush(Colors.Red);
                        break;
                    case DragState.AllowSwitch:
                        MouseImage.BorderBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FFF0FB07"));
                        break;
                }
            }
        }

        public DragObject(UIElement control)
        {
            Control = control;
            
        }

        public void MouseMove(MouseEventArgs arg)
        {
            if (MouseLeftDown == false)
                return;

            if (Control == null && Parent == null)
                return;

            if (!AllowDrag)
            {
                MouseUp();
                return;
            }
            //创建虚拟图像
            CreateImage();

            Point toPoint;
            toPoint = arg.GetPosition(Parent);

            CompositeTransform tt = MouseImage.RenderTransform as CompositeTransform;
            if (tt == null)
            {
                tt = new CompositeTransform();
                Control.RenderTransform = tt;
            }

            tt.TranslateX = tt.TranslateX + (toPoint.X - DownPoint.X);
            tt.TranslateY = tt.TranslateY + (toPoint.Y - DownPoint.Y);

            MouseImage.RenderTransform = tt;

            DownPoint = toPoint;

            //调用委托保存目标控件的对象
            if (OnDragObjectEvent != null)
                OnDragObjectEvent(this);
        }

        public void MouseDown(MouseButtonEventArgs arg)
        {
            if (Parent == null)
            {
                Parent = (Control as FrameworkElement).Parent as Panel;
            }

            if (Parent == null)
                return;

            if (!AllowDrag)
            {
                MouseUp();
                return;
            }

            MouseLeftDown = true;
            Control.CaptureMouse();
            DownPoint = arg.GetPosition(Parent);
            
        }

        public void MouseUp()
        {
            if (MouseLeftDown)
            {
                MouseLeftDown = false;
                Parent.Children.Remove(MouseImage);
                MouseImage = null;
                Control.ReleaseMouseCapture();

                if (OnDragObjectCompleteEvent != null )
                {
                    if (State == DragState.Allow || State == DragState.AllowSwitch)
                    {
                        OnDragObjectCompleteEvent(this);
                    }
                }
            }
        }

        private void CreateImage()
        {
            if (MouseImage != null)
                return;

            if (Parent == null)
                return;
            
            //构建虚拟图像控件
            MouseImage = CreateControlImg();
            if (MouseImage == null)
                return;

            Parent.Children.Add(MouseImage);
            int row = Grid.GetRow(Control as FrameworkElement);
            int col = Grid.GetColumn(Control as FrameworkElement);

            Grid.SetRow(MouseImage,row);
            Grid.SetColumn(MouseImage,col);
            MouseImage.UpdateLayout();
            Point p = Control.TransformToVisual(Parent).Transform(new Point(0, 0));
            Point p2 = MouseImage.TransformToVisual(Parent).Transform(new Point(0, 0));

            CompositeTransform ctf = new CompositeTransform();
            ctf.TranslateX = p.X - p2.X;
            ctf.TranslateY = p.Y - p2.Y;

            MouseImage.RenderTransform = ctf;
            
        }

        private DragMouseControl CreateControlImg()
        {
            if (Control == null)
                return null;
            WriteableBitmap bitmap = new WriteableBitmap(Control, null);
            
            DragMouseControl dmc = new DragMouseControl();
            dmc.Height = (Control as FrameworkElement).ActualHeight;
            dmc.Width = (Control as FrameworkElement).ActualWidth;
            dmc.MouseImage = bitmap;
            return dmc;
        }

        public Point? GetCenter()
        {
            if (Parent == null || MouseImage == null)
                return null;

            Point tmp=MouseImage.TransformToVisual(Parent).Transform(new Point(0,0));
            return new Point(tmp.X+MouseImage.Width/2,tmp.Y+MouseImage.Height/2);

        }

        public Point? GetCenter(UIElement ui)
        {
            if (Parent == null || MouseImage == null)
                return null;

            Point tmp = ui.TransformToVisual(Parent).Transform(new Point(0, 0));
            return new Point(tmp.X + (ui as FrameworkElement).ActualWidth / 2, tmp.Y + (ui as FrameworkElement).ActualHeight / 2);

        }
    }

    public enum DragState
    { 
        Normal,
        Allow,
        NoAllow,
        AllowSwitch
    }
}
