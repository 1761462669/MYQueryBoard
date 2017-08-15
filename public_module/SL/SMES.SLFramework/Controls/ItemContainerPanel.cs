using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SMES.Framework.Controls
{
    /// <summary>
    /// 实体容器
    /// 1.对应一个实体，要展示相关属性的值
    /// 2.具有查看时状态、编辑时状态等多个状态
    /// 3.如果未定义属性模板，可自动生产属性列表
    /// 4.自动对齐布局
    /// added by changhl,2015-5-18
    /// </summary>
    public class ItemContainerPanel : Control
    {
      
        #region ----------------------------------------------------------------DependencyProperty----------------------------------------------------------------
        // 状态
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(string), typeof(ItemContainerPanel), new PropertyMetadata(""));

        // 行数
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(ItemContainerPanel), new PropertyMetadata(1));

        // 列数
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(ItemContainerPanel), new PropertyMetadata(1));
        #endregion

        #region ----------------------------------------------------------------feilds && Properties----------------------------------------------------------------
        /// <summary>
        /// grid布局
        /// </summary>
        Grid grid_layOut = null;
        internal ObservableCollection<ItemPanelColumn> columns = new ObservableCollection<ItemPanelColumn>();
        /// <summary>
        /// 属性列别
        /// </summary>
        public ObservableCollection<ItemPanelColumn> Columns
        {
            get
            {
                return this.columns;
            }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        #endregion

        #region ----------------------------------------------------------------Constructs----------------------------------------------------------------
        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemContainerPanel()
        {
            this.DefaultStyleKey = typeof(ItemContainerPanel);
        }
        #endregion

        #region ----------------------------------------------------------------Methods----------------------------------------------------------------
        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.grid_layOut = this.GetTemplateChild("grid_layOut") as Grid;
        }



        #endregion

        


    }

    /// <summary>
    /// 数据项显示
    /// </summary>
    public class ItemPanelColumn : Control
    {


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(ItemPanelColumn), new PropertyMetadata(""));





        public Binding Binding
        {
            get { return (Binding)GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Binding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingProperty =
            DependencyProperty.Register("Binding", typeof(Binding), typeof(ItemPanelColumn), new PropertyMetadata(null));



        /// <summary>
        /// 显示模板
        /// </summary>
        public DataTemplate ViewTemplate
        {
            get { return (DataTemplate)GetValue(ViewTemplateProperty); }
            set { SetValue(ViewTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewTemplateProperty =
            DependencyProperty.Register("ViewTemplate", typeof(DataTemplate), typeof(ItemPanelColumn), new PropertyMetadata(null));





        /// <summary>
        /// 编辑模板
        /// </summary>
        public DataTemplate EditTemplate
        {
            get { return (DataTemplate)GetValue(EditTemplateProperty); }
            set { SetValue(EditTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTemplateProperty =
            DependencyProperty.Register("EditTemplate", typeof(DataTemplate), typeof(ItemPanelColumn), new PropertyMetadata(null));



     

        /// <summary>
        ///所对应的Panel
        /// </summary>
        internal ItemContainerPanel OwningPanel { get; set; }






    }


}
