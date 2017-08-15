using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

using MDF.Custom.ControlSL.Controls.Calendar;
using MDF.Custom.ControlSL.Tools;

namespace MDF.Custom.ControlSL.Controls.ScheduleGant
{
    [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MouseLeave", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
    [TemplateVisualState(Name = "Checked", GroupName = "CheckedStates")]
    [TemplateVisualState(Name = "UnChecked", GroupName = "CheckedStates")]
    public class ScheduleGantItem : ContentControl
    {
        #region 拖动
        public event DragObjectHandler OnDragObjectEvent;
        public event DragObjectHandler OnDragObjectCompleteEvent;

        DragObject drg;
        #endregion

        Rectangle rectMain;
        Rectangle rectProcessBar;
        StackPanel stackQty;

        public ScheduleGantItem()
        {
            this.DefaultStyleKey = typeof(ScheduleGantItem);

            drg = new DragObject(this);
            BindingOperations.SetBinding(drg, DragObject.AllowDragProperty, new Binding() { Source = this, Path = new PropertyPath("AllowDrag") });
            drg.OnDragObjectEvent += drg_OnDragObjectEvent;
            drg.OnDragObjectCompleteEvent += drg_OnDragObjectCompleteEvent;
        }

        void drg_OnDragObjectCompleteEvent(DragObject obj)
        {
            if (OnDragObjectCompleteEvent != null)
                OnDragObjectCompleteEvent(obj);
        }

        void drg_OnDragObjectEvent(DragObject obj)
        {
            if (OnDragObjectEvent != null)
                OnDragObjectEvent(obj);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            rectMain = this.GetTemplateChild("rectMain") as Rectangle;
            rectProcessBar = this.GetTemplateChild("rectProcessBar") as Rectangle;
            stackQty = this.GetTemplateChild("stackQty") as StackPanel;

            SetRectMainBgByType(this.BgType.ToString());

            SetRectProcessBarBgByType(this.ProcessBgBarType.ToString());
        }

        public event RoutedEventHandler ItemChecked, ItemUnChecked;
        public event RoutedEventHandler ItemRightChecked, ItemRightUnChecked;
        public event RoutedEventHandler ItemMouseOver, ItemMouseLeave;

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }


        #region 依赖属性

        // Using a DependencyProperty as the backing store for Checked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked", typeof(bool), typeof(ScheduleGantItem), new PropertyMetadata(false, new PropertyChangedCallback((obj, arg) =>
            {
                if ((bool)arg.NewValue)
                {
                    VisualStateManager.GoToState(obj as Control, "Checked", false);
                    (obj as ScheduleGantItem).checkEvent();
                }
                else
                {
                    VisualStateManager.GoToState(obj as Control, "UnChecked", false);
                }
            })));

        public bool AllowDrag
        {
            get { return (bool)GetValue(AllowDragProperty); }
            set { SetValue(AllowDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowDrag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register("AllowDrag", typeof(bool), typeof(ICSSDayControl), new PropertyMetadata(true));

        public double ProcessBarWidth
        {
            get { return (double)GetValue(ProcessBarWidthProperty); }
            set { SetValue(ProcessBarWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProcessBarWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessBarWidthProperty =
            DependencyProperty.Register("ProcessBarWidth", typeof(double), typeof(ScheduleGantItem), new PropertyMetadata(0.0));


        #region 计划类型

        /// <summary>
        /// 0:灰色 1：绿色 2：黄色
        /// </summary>
        public int BgType
        {
            get { return (int)GetValue(BgTypeProperty); }
            set { SetValue(BgTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BgType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BgTypeProperty =
            DependencyProperty.Register("BgType", typeof(int), typeof(ScheduleGantItem), new PropertyMetadata(0, new PropertyChangedCallback((sender, e) =>
                {
                    Rectangle rectMainTemp = (sender as ScheduleGantItem).rectMain;

                    if (rectMainTemp == null) return;

                    (sender as ScheduleGantItem).SetRectMainBgByType(e.NewValue.ToString());
                })));

        public void SetRectMainBg(LinearGradientBrush brush,string effectColor)
        {
            DropShadowEffect effect = new DropShadowEffect();
            effect.Color = ColorUnity.HtmlToColor(effectColor);

            this.rectMain.Fill = brush;
            this.rectMain.Effect = effect;
        }

        public void SetRectMainBgByType(string type)
        {
            if (type == "0")//需求：灰色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFF7F7F2"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFDED7D7"), Offset = 1 });

                SetRectMainBg(brush, "#FFDCDECB");
            }
            if (type == "1")//正常计划：绿色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFA3F0A6"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FF0AC11B"), Offset = 1 });

                SetRectMainBg(brush, "#FF5E6459");
            }
            if (type == "2")//已完成：黄色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFF3E8A0"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFF7F704"), Offset = 1 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFDED92D"), Offset = 0.507 });

                SetRectMainBg(brush, "#FFDEE0CF");
            }

            if (type == "3")//已完成：蓝色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FF88E0F9"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FF647EF0"), Offset = 1 });

                SetRectMainBg(brush, "#FFDEE0CF");
            }

            if (type == "4")//试制：棕色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFFFC767"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFE65B0C"), Offset = 1 });

                SetRectMainBg(brush, "#FFDEE0CF");
            }
        }

        #endregion

        #region 进度条

        /// <summary>
        /// 1：蓝色 2：红色
        /// </summary>
        public int ProcessBgBarType
        {
            get { return (int)GetValue(ProcessBgBarTypeProperty); }
            set { SetValue(ProcessBgBarTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProcessBgBarType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessBgBarTypeProperty =
            DependencyProperty.Register("ProcessBgBarType", typeof(int), typeof(ScheduleGantItem), new PropertyMetadata(0, new PropertyChangedCallback((sender, e) =>
            {
                Rectangle rectProcessBarTemp = (sender as ScheduleGantItem).rectProcessBar;

                if (rectProcessBarTemp == null) return;

                (sender as ScheduleGantItem).SetRectProcessBarBgByType(e.NewValue.ToString());
            })));

        public void SetRectProcessBarBgByType(string type)
        {
            if (type == "1")//蓝色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FF7C96F0"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FF0362C1"), Offset = 1 });

                this.rectProcessBar.Fill = brush;
            }

            if (type == "2")//红色
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0.5, 0);
                brush.EndPoint = new Point(0.5, 1);

                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFF07C7C"), Offset = 0 });
                brush.GradientStops.Add(new GradientStop() { Color = ColorUnity.HtmlToColor("#FFC10303"), Offset = 1 });

                this.rectProcessBar.Fill = brush;
            }
            
        }

        #endregion

        #endregion

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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (drg != null)
                drg.MouseMove(e);
        }  

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.Checked = this.Checked ? false : true;

            if (drg != null)
            {
                var border = ((drg.Control as FrameworkElement).Parent as FrameworkElement).Parent as FrameworkElement;
                Panel grid = border.Parent as Panel;
                drg.Parent = grid;

                drg.MouseDown(e);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (drg != null)
                drg.MouseUp();
        }

        #endregion

    }
}
