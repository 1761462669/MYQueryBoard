using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Controls
{


    public class SmesDataGrid : DataGrid
    {
        /// <summary>
        /// 编辑行模板
        /// </summary>
        public DataTemplate EditRowTemplate
        {
            get { return (DataTemplate)GetValue(EditRowTemplateProperty); }
            set { SetValue(EditRowTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditRowTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditRowTemplateProperty =
            DependencyProperty.Register("EditRowTemplate", typeof(DataTemplate), typeof(SmesDataGrid), new PropertyMetadata(null));

        public List<DataGridRow> Rows
        {
            get { return rowsPresenter.Children.Cast<DataGridRow>().ToList(); }
        }



        /// <summary>
        /// 添加行模板
        /// </summary>
        public DataTemplate AddRowTemplate
        {
            get { return (DataTemplate)GetValue(AddRowTemplateProperty); }
            set { SetValue(AddRowTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddRowTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddRowTemplateProperty =
            DependencyProperty.Register("AddRowTemplate", typeof(DataTemplate), typeof(SmesDataGrid), new PropertyMetadata(null));

        private DataGridRowsPresenter rowsPresenter = null;



        public SmesDataGrid()
        {
            this.DefaultStyleKey = typeof(SmesDataGrid);

        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            rowsPresenter = this.GetTemplateChild("RowsPresenter") as DataGridRowsPresenter;
        }



        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {

            base.OnLoadingRow(e);
            e.Row.Loaded -= Row_Loaded;
            e.Row.Loaded += Row_Loaded;


        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var item = e.AddedItems[0];
                this.ScrollIntoView(item, null);
            }
            if (e.RemovedItems != null)
                foreach (var item in e.RemovedItems)
                {
                    var model = item as IRootModel;
                    if (model != null && model.ViewState != ModelViewState.Add)
                        model.ViewState = ModelViewState.View;
                }
        }



        void Row_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.EditRowTemplate != null)
            {
                var editCtl = this.GetChild<ContentControl>(sender as DataGridRow, "editPresenter");
                if (editCtl != null)
                    editCtl.ContentTemplate = this.EditRowTemplate;
            }
            if (this.AddRowTemplate != null)
            {
                var addCtl = this.GetChild<ContentControl>(sender as DataGridRow, "addPresenter");
                if (addCtl != null)
                    addCtl.ContentTemplate = this.AddRowTemplate;
            }



        }


        public T GetChild<T>(DependencyObject reference, string name) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(reference);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(reference, i);
                if (child is T)
                {
                    var fe = child as FrameworkElement;
                    if (fe.Name == name)
                        return fe as T;
                }
                var result = GetChild<T>(child, name);
                if (result != null)
                    return result;

            }
            return null;

        }
    }


}
