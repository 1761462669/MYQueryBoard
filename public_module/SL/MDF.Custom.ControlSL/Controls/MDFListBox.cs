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
using System.Linq;
using System.Collections;
using System.Windows.Data;

namespace MDF.Custom.ControlSL.Controls
{
    [TemplateVisualState(GroupName = "ValidationStates", Name = "Empty")]
    public class MDFListBox : ListBox, System.ComponentModel.INotifyPropertyChanged
    {
        public event RoutedEventHandler OnListItemsChanged;

        public Brush AlterBackGroundColor
        {
            get { return (Brush)GetValue(AlterBackGroundColorProperty); }
            set { SetValue(AlterBackGroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlterBackGroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlterBackGroundColorProperty =
            DependencyProperty.Register("AlterBackGroundColor", typeof(Brush), typeof(MDFListBox), new PropertyMetadata(null));


        public MDFListBox()
        {
            this.DefaultStyleKey = typeof(MDFListBox);
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (this.AlterBackGroundColor == null)
                return;
            var index = this.Items.IndexOf(item);
            if (index % 2 == 1)
            {
                var lb = element as ListBoxItem;
                lb.Background = this.AlterBackGroundColor;
            }

        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            PagedCollectionView collection = null;
            var listTemp = this.ItemsSource as IEnumerable;

            if (listTemp != null)
            {
                collection = new PagedCollectionView(listTemp);
            }

            if (collection == null) return;

            int count = collection.Count;

            if (count == 0)
            {
                if (IsEnabledEmptyState)
                {
                    VisualStateManager.GoToState(this, "Empty", false);
                }
            }
            else
            {
                VisualStateManager.GoToState(this, "NotEmpty", false);
            }

            if(ScrollToObj != null)
            {
                this.LayoutUpdated += MDFListBox_LayoutUpdated;
            }

            //for (int i = 0; i < this.Items.Count; i++)
            //{
            //    if (i % 2 == 1)
            //    {
            //        var lb = this.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
            //        lb.Background = new SolidColorBrush(Colors.Red);
            //    }
            //}

            
            if(OnListItemsChanged != null)
            {
                OnListItemsChanged(this, new RoutedEventArgs());
            }
        }

        void MDFListBox_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= MDFListBox_LayoutUpdated;
            this.ScrollIntoView(ScrollToObj);
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        private bool _IsEnabledEmptyState = true;
        /// <summary>
        /// 是否启用空数据状态
        /// </summary>
        public bool IsEnabledEmptyState
        {
            get { return this._IsEnabledEmptyState; }
            set
            {
                if (this._IsEnabledEmptyState != value)
                {
                    this._IsEnabledEmptyState = value; ;
                }
            }
        }

        #region 刷新后数据定位

        public object ScrollToObj
        {
            get { return (object)GetValue(ScrollToObjProperty); }
            set { SetValue(ScrollToObjProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollToObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollToObjProperty =
            DependencyProperty.Register("ScrollToObj", typeof(object), typeof(MDFListBox), new PropertyMetadata(null,new PropertyChangedCallback ((sender,e)=>
        {
            var sources = (sender as MDFListBox).ItemsSource;
            if (e.NewValue != null && sources != null)
            {
                (sender as MDFListBox).ScrollIntoView(e.NewValue);
            }
        })));

        #endregion

    }
}
