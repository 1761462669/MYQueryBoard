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
using System.Reflection;
using System.Collections;
using MDF.Framework.Bus;

namespace MDF.Custom.ControlSL.Controls.Calendar
{
    public class ICSSCalendar : Control, System.ComponentModel.INotifyPropertyChanged
    {

        Dictionary<string, ICSSDayControl> dayList;
        Grid monthGrid;
        public delegate void DayClickHandle(object sender, ICSSDayControl day);
        public event DayClickHandle DayClick;
        public event DragObjectHandler OnItemDragObjectCompleteEvent;
        public ICSSCalendar()
        {
            this.DefaultStyleKey = typeof(ICSSCalendar);
            dayList = new Dictionary<string, ICSSDayControl>();

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            monthGrid = this.GetTemplateChild("monthGrid") as Grid;
            CreateDayControl();
        }

        #region 日期
        public static readonly DependencyProperty DayProperty = DependencyProperty.Register("Day", typeof(DateTime), typeof(ICSSCalendar),
            new PropertyMetadata(DateTime.Now, new PropertyChangedCallback((obj, arg) =>
            {
                (obj as ICSSCalendar).CreateDayControl();
            })));

        public DateTime Day
        {
            get
            {
                return (DateTime)GetValue(DayProperty);
            }
            set
            {
                SetValue(DayProperty, value);
            }
        }
        #endregion

        #region 每天控件的样式
        public static readonly DependencyProperty DayControlStyleProperty = DependencyProperty.Register("DayControlStyle",
            typeof(Style), typeof(ICSSCalendar),
            new PropertyMetadata(default(Style)));
        public Style DayControlStyle
        {
            get
            { return GetValue(DayControlStyleProperty) as Style; }
            set
            {
                SetValue(DayControlStyleProperty, value);
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("DayControlStyle"));
            }
        }
        #endregion

        #region DateFiled
        public static readonly DependencyProperty DateFileddProperty = DependencyProperty.Register("DateFiled", typeof(string), typeof(ICSSCalendar),
            new PropertyMetadata(default(string), new PropertyChangedCallback((obj, arg) =>
            {
                (obj as ICSSCalendar).SetDayControlDataContext();
            }
            )));
        public string DateFiled
        {
            get
            {
                return GetValue(DateFileddProperty) as string;
            }
            set
            {
                SetValue(DateFileddProperty, value);
            }
        }
        #endregion

        #region DataSource
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(IEnumerable), typeof(ICSSCalendar),
            new PropertyMetadata(default(IEnumerable), new PropertyChangedCallback((obj, arg) =>
            {
                (obj as ICSSCalendar).SetDayControlDataContext();
            })));
        public IEnumerable ItemSource
        {
            get
            {
                return GetValue(ItemSourceProperty) as IEnumerable;
            }
            set
            {
                SetValue(ItemSourceProperty, value);
            }
        }
        #endregion



        public DataTemplate ItemDaytTemplate
        {
            get { return (DataTemplate)GetValue(ItemDaytTemplateProperty); }
            set { SetValue(ItemDaytTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemDaytTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemDaytTemplateProperty =
            DependencyProperty.Register("ItemDaytTemplate", typeof(DataTemplate), typeof(ICSSCalendar), new PropertyMetadata(default(DataTemplate)));


        

        #region AllDrag

        public bool AllDrag
        {
            get { return (bool)GetValue(AllDragProperty); }
            set { SetValue(AllDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllDrag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllDragProperty =
            DependencyProperty.Register("AllDrag", typeof(bool), typeof(ICSSCalendar), new PropertyMetadata(false));


        #endregion

        #region DragCompleteCommand

        public ICommand DragCompleteCommand
        {
            get { return (ICommand)GetValue(DragCompleteCommandProperty); }
            set { SetValue(DragCompleteCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DragCompleteCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragCompleteCommandProperty =
            DependencyProperty.Register("DragCompleteCommand", typeof(ICommand), typeof(ICSSCalendar), new PropertyMetadata(default(ICommand)));

        
        #endregion

        private void CreateDayControl()
        {
            if (monthGrid == null)
                return;
            ClearGrid();
            dayList = new Dictionary<string, ICSSDayControl>();
            //monthGrid.Children.Clear();
            DateTime today = Day;

            DateTime Monthfirstday = DateTime.Parse(today.ToString("yyyy-MM") + "-01");

            int days = DateTime.DaysInMonth(today.Year, today.Month);
            int maxcol = 6;
            int startrow = 1;
            int startCol = WeekIndex(Monthfirstday.DayOfWeek); //开始的列

            ICSSDayControl daycontrol;
            for (int i = 0; i < days; i++)
            {
                daycontrol = new ICSSDayControl();
                daycontrol.SetBinding(ICSSDayControl.AllowDragProperty, new System.Windows.Data.Binding() { Source = this, Path = new PropertyPath("AllDrag") });
                daycontrol.Day = DateTime.Parse(today.ToString("yyyy-MM") + "-" + (i + 1));
                daycontrol.SetBinding(ICSSDayControl.StyleProperty, new System.Windows.Data.Binding() { Source = this, Path = new PropertyPath("DayControlStyle") });
                daycontrol.SetBinding(ICSSDayControl.ItemDaytTemplateProperty, new System.Windows.Data.Binding("ItemDaytTemplate") { Source=this });
                dayList.Add(daycontrol.Day.ToString("yyyy-MM-dd"), daycontrol);
                Grid.SetColumn(daycontrol, startCol);
                Grid.SetRow(daycontrol, startrow);
                monthGrid.Children.Add(daycontrol);
                startCol++;
                if (startCol > maxcol)
                {
                    startCol = 0;
                    startrow++;
                }

                daycontrol.DayControlClick += daycontrol_DayControlClick;//控件点击事件
                daycontrol.OnDragObjectEvent += daycontrol_OnDragObjectEvent;//
                daycontrol.OnDragObjectCompleteEvent += daycontrol_OnDragObjectCompleteEvent;
            }

            //绑定日控件DataContext
            SetDayControlDataContext();
        }

        #region 控件拖拽事件
        void daycontrol_OnDragObjectCompleteEvent(DragObject obj)
        {
            if (OnItemDragObjectCompleteEvent != null)
                OnItemDragObjectCompleteEvent(obj);

            if (DragCompleteCommand != null)
            {
                DragCompletePar par = new DragCompletePar();
                if (obj.Control != null)
                    par.From = (obj.Control as FrameworkElement).DataContext;
                    par.FromElement = obj.Control as FrameworkElement;

                if (obj.ToControl != null)
                    par.To = (obj.ToControl as FrameworkElement).DataContext;
                    par.ToElement = obj.ToControl as FrameworkElement;

                if (DragCompleteCommand.CanExecute(par))
                {
                    DragCompleteCommand.Execute(par);
                }
                else if (DragCompleteCommand.CanExecute(null))
                {
                    DragCompleteCommand.Execute(null);
                }
            }
        }

        /// <summary>
        /// 拖拽事件，将目标Control保存到DragObject中
        /// </summary>
        /// <param name="obj"></param>
        void daycontrol_OnDragObjectEvent(DragObject obj)
        {            
            if (obj.MouseLeftDown == false || obj.MouseImage == null)
                return;

            //得到拖拽控件中心点
            Point? p = obj.GetCenter();
            if (p == null)
                return;

            ICSSDayControl day=GetDragPlace(p.Value, obj);
            if (day == null || day==obj.Control)
            {
                obj.State = DragState.Normal;
                obj.ToControl = null;
            }
            else
            {
                obj.State = DragState.Allow;
                obj.ToControl = day;
            }
        }
        #endregion

        /// <summary>
        /// 获取拖拽目标位置的DayControl控件
        /// </summary>
        /// <param name="p"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private ICSSDayControl GetDragPlace(Point p,DragObject obj)
        {
            if (dayList == null)
                return null;

            Point? point;
            foreach (ICSSDayControl day in dayList.Values)
            {
                point = obj.GetCenter(day);
                if (point == null)
                    continue;

                if (p.X >= point.Value.X - 20 && p.X <= point.Value.X + 20 && p.Y >= point.Value.Y - 20 && p.Y <= point.Value.Y + 20)
                {
                    return day;
                }
            }

            return null;
        }

        protected void daycontrol_DayControlClick(object sender, RoutedEventArgs e)
        {
            if (DayClick != null)
            {
                DayClick(this, sender as ICSSDayControl);
            }
        }

        private void ClearGrid()
        {
            foreach (KeyValuePair<string, ICSSDayControl> pair in dayList)
            {
                pair.Value.DayControlClick -= daycontrol_DayControlClick;
                monthGrid.Children.Remove(pair.Value);
            }

            dayList.Clear();
            dayList = null;
        }

        private void SetDayControlDataContext()
        {
            foreach (ICSSDayControl day in dayList.Values)
            {
                day.DataContext = null;
            }

            if (ItemSource == null || dayList == null || dayList.Count == 0)
            {
                return;
            }
            string tmp;
            ICSSDayControl daycontrol;
            foreach (object obj in ItemSource)
            {
                tmp = GetObjDate(obj);
                if (tmp == null)
                    continue;

                if (dayList.ContainsKey(tmp))
                {
                    daycontrol = dayList[tmp];
                    daycontrol.DataContext = obj;
                }
            }
        }

        private string GetObjDate(object obj)
        {
            if (string.IsNullOrEmpty(DateFiled))
                return null;
            try
            {
                JsonToDictionary jsObj = obj as JsonToDictionary;
                string datestr = jsObj[DateFiled].ToString();

                DateTime date;

                if (DateTime.TryParse(datestr, out date))
                    return date.ToString("yyyy-MM-dd");
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        private int WeekIndex(DayOfWeek week)
        {
            switch (week)
            {
                case DayOfWeek.Sunday:
                    return 0;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return 0;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        
    }

    public class DragCompletePar
    {
        public object From
        { get; set; }

        public object To
        { get; set; }

        public FrameworkElement FromElement { get; set; }

        public FrameworkElement ToElement { get; set; }
    }
}

