using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFComboGrid : ComboBox
    {
        #region private field && property
        private DataGrid dg;
        private Popup popup;
        private bool HasSetDefault = false;
        private bool IsFirst = true;
        #endregion

        public MDFComboGrid()
        {
            this.DefaultStyleKey = typeof(MDFComboGrid);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.dg = this.GetTemplateChild("data_grid") as DataGrid;
            this.popup = this.GetTemplateChild("Popup") as Popup;

            this.CreateGridColumns();

            this.DisplayMemberPath = "Name";
            this.ItemsSource = this.GridItemSource;

            //this.SetBinding(
            //    ComboBox.SelectedItemProperty, new Binding()
            //    {
            //        Source = this.dg,
            //        Path = new PropertyPath("SelectedItem"),
            //        Mode = BindingMode.TwoWay
            //    });
            this.dg.SetBinding(
               DataGrid.SelectedItemProperty, new Binding()
               {
                   Source = this,
                   Path = new PropertyPath("SelectedItem"),
                   Mode = BindingMode.TwoWay
               });

            this.dg.ItemsSource = this.GridItemSource;

            this.dg.SelectionChanged += dg_SelectionChanged;

            if (this.IsSelectFirst)
                this.dg.SelectedIndex = 0;
        }

        void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsFirst)
            {
                this.IsFirst = false;
                return;
            }

            this.IsDropDownOpen = false;
        }
        
        private void CreateGridColumns()
        {
            if (this.GridColumns == null
                || this.GridColumns.Count == 0)
                return;

            this.dg.Columns.Clear();

            foreach (var column in this.GridColumns)
            {
                this.dg.Columns.Add(column);
            }
        }



        public bool IsSelectFirst
        {
            get { return (bool)GetValue(IsSelectFirstProperty); }
            set { SetValue(IsSelectFirstProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelectFirst.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectFirstProperty =
            DependencyProperty.Register("IsSelectFirst", typeof(bool), typeof(MDFComboGrid), new PropertyMetadata(true));

        

        public PagedCollectionView GridItemSource
        {
            get { return (PagedCollectionView)GetValue(GridItemSourceProperty); }
            set { SetValue(GridItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridItemSourceProperty =
            DependencyProperty.Register("GridItemSource", typeof(PagedCollectionView), typeof(MDFComboGrid),
            new PropertyMetadata(null,
                (sender, e) => {
                    if ((sender as MDFComboGrid).dg == null)
                        return;

                    (sender as MDFComboGrid).dg.ItemsSource = e.NewValue as IEnumerable;
                    (sender as MDFComboGrid).ItemsSource = e.NewValue as IEnumerable;
                }));

        

        public  List<DataGridColumn> GridColumns
        {
            get { return ( List<DataGridColumn>)GetValue(GridColumnsProperty); }
            set { SetValue(GridColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridColumns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridColumnsProperty =
            DependencyProperty.Register("GridColumns", typeof(List<DataGridColumn>), typeof(MDFComboGrid),
            new PropertyMetadata(new List<DataGridColumn>(),
                (sender, e) => {
                    (sender as MDFComboGrid).CreateGridColumns();
                }));


        public bool IsAutoExpanded
        {
            get { return (bool)GetValue(IsAutoExpandedProperty); }
            set { SetValue(IsAutoExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoExpandedProperty =
            DependencyProperty.Register("IsAutoExpanded", typeof(bool), typeof(MDFComboGrid), 
            new PropertyMetadata(true));
    }
}
