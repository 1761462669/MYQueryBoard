using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls.Calendar
{
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates"),
    TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates"),
    TemplateVisualState(Name = "Disabled", GroupName = "CommonStates"),
    TemplateVisualState(Name = "Click", GroupName = "CommonStates")]
    public class ICSSDayControl : Control
    {
        #region 属性

        public event RoutedEventHandler DayControlClick;

        public Button btnEditTime;
        public Popup popup;

        #endregion

        #region 事件
        public event DragObjectHandler OnDragObjectEvent;
        public event DragObjectHandler OnDragObjectCompleteEvent;
        #endregion

        public ICSSDayControl()
        {
            this.DefaultStyleKey = typeof(ICSSDayControl);
        }
        DragObject drg;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            lbDay = GetTemplateChild("lbDay") as TextBlock;
            SetDate();
            drg = new DragObject(this);
            BindingOperations.SetBinding(drg, DragObject.AllowDragProperty, new Binding() { Source = this, Path = new PropertyPath("AllowDrag") });
            drg.OnDragObjectEvent += drg_OnDragObjectEvent;
            drg.OnDragObjectCompleteEvent += drg_OnDragObjectCompleteEvent;
        }


        public DataTemplate ItemDaytTemplate
        {
            get { return (DataTemplate)GetValue(ItemDaytTemplateProperty); }
            set { SetValue(ItemDaytTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemDaytTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemDaytTemplateProperty =
            DependencyProperty.Register("ItemDaytTemplate", typeof(DataTemplate), typeof(ICSSDayControl), new PropertyMetadata(default(DataTemplate)));




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

        #region 鼠标状态事件
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (drg != null)
                drg.MouseMove(e);
        }        
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            //VisualStateManager.GoToState(this, "MouseOver", true);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            //VisualStateManager.GoToState(this, "Normal", true);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            //VisualStateManager.GoToState(this, "Click", true);
            if (drg != null)
                drg.MouseDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            //VisualStateManager.GoToState(this, "Normal", true);

            if (drg != null)
                drg.MouseUp();

            if (DayControlClick != null)
            {
                DayControlClick(this, new RoutedEventArgs());
            }
        }
        #endregion

        #region 依赖属性

        TextBlock lbDay;
        public static readonly DependencyProperty DayPropery = DependencyProperty.Register("Day", typeof(DateTime), typeof(ICSSDayControl),
            new PropertyMetadata(DateTime.Now, new PropertyChangedCallback((obj, arg) =>
            {
                (obj as ICSSDayControl).SetDate();
            })));

        

        public DateTime Day
        {
            get
            {
                return (DateTime)GetValue(DayPropery);
            }
            set
            {
                SetValue(DayPropery, value);
            }
        }

        public static readonly DependencyProperty DayFormatPropery = DependencyProperty.Register("DayFormat", typeof(string), typeof(ICSSDayControl),
            new PropertyMetadata(default(string), new PropertyChangedCallback((obj, arg) =>
            {
                (obj as ICSSDayControl).SetDate();
            })));

        public string DayFormat
        {
            get
            {
                return GetValue(DayFormatPropery) as string;
            }
            set
            {
                SetValue(DayFormatPropery, value);
            }
        }

        private void SetDate()
        {
            if (lbDay == null)
                return;

            if (string.IsNullOrEmpty(DayFormat))
                lbDay.Text = Day.ToString("dd");
            else
                lbDay.Text = Day.ToString(DayFormat);
        }



        public bool AllowDrag
        {
            get { return (bool)GetValue(AllowDragProperty); }
            set { SetValue(AllowDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowDrag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register("AllowDrag", typeof(bool), typeof(ICSSDayControl), new PropertyMetadata(false));

        
        #endregion
    }
}

