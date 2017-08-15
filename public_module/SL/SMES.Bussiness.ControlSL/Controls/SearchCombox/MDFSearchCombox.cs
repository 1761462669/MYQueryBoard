using SMES.Framework;
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
using System.Collections.ObjectModel;
using MDF.Framework.Bus;
using System.Collections.Generic;
using SMES.FrameworkAdpt.MaterialChooseControl;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using SMES.FrameworkAdpt.MaterialChooseControl.Service;
using System.ComponentModel;
using SMES.FrameworkAdpt.OrgInfo;
using SMES.FrameworkAdpt.OrgInfo.Service;
using SMES.FrameworkAdpt.OrgInfo.IModel;
using MDF.Framework.Db;

namespace SMES.Bussiness.ControlSL.SearchCombox
{
    public class MDFSearchCombox : ContentControl
    {
        TextBox searchTextBox;
        ListBox listbox;
        Popup pupup;
        TextBlock lblText;
        ToggleButton DropDownToggle;
        Border border;
        FrameworkElement root;

        public MDFSearchCombox()
        {
            this.DefaultStyleKey = typeof(MDFSearchCombox);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DesignerProperties.IsInDesignTool) return;

            searchTextBox = this.GetTemplateChild("serachTextBox") as TextBox;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            listbox = this.GetTemplateChild("listbox") as ListBox;
            pupup = this.GetTemplateChild("Popup") as Popup;
            lblText = this.GetTemplateChild("lblText") as TextBlock;
            DropDownToggle = this.GetTemplateChild("DropDownToggle") as ToggleButton;
            DropDownToggle.Click += (sender, e) =>
            {
                this.pupup.IsOpen = true;
                ArrangePopup();
            };

            Border ContentPresenterBorder = this.GetTemplateChild("ContentPresenterBorder") as Border;
            ContentPresenterBorder.MouseLeftButtonDown += (sender, e) =>
                {
                    this.pupup.IsOpen = true;
                    ArrangePopup();
                };

            lblText.Text = "请选择";

            listbox.SelectionChanged += listbox_SelectionChanged;
        }

        private void ArrangePopup()
        {
            var point = this.TransformToVisual(null).Transform(new Point(0, 0));
            var fr = VisualTreeHelper.GetRoot(this) as FrameworkElement;

            if (fr != null)
            {
                fr.MouseLeftButtonDown -= fr_MouseLeftButtonDown;
                fr.MouseLeftButtonDown += fr_MouseLeftButtonDown;
                var hegith = fr.ActualHeight;

                CompositeTransform transform = new CompositeTransform();

                if (hegith - point.Y < 300)
                {
                    var num = 300;
                    transform.TranslateY = -num;
                }
                else
                {
                    var num = this.ActualHeight;
                    transform.TranslateY = num;
                }

                if (fr.ActualWidth - point.X < 300)
                {
                    var num = this.Width - 300;
                    transform.TranslateX = num;
                }

                pupup.RenderTransform = transform;
            }
            else
            {
                double maxWidth = Application.Current.Host.Content.ActualWidth;
                double maxHeight = Application.Current.Host.Content.ActualHeight;

                if (maxWidth - point.X < 300)
                {
                    pupup.HorizontalOffset = this.Width - 300;
                }
                if (maxHeight - point.Y < 300)
                {
                    pupup.VerticalOffset = -300;
                }
                else
                {
                    pupup.VerticalOffset = this.Height;
                }
            }
        }

        void fr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.pupup.IsOpen = false;
        }

        void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedItem == null) return;

            this.SelectItem = listbox.SelectedItem as DataModel;
            this.SelectedId = this.SelectItem.Id.ToString();
            this.SelectedName = this.SelectItem.Name;

        }

        void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            QueryBySerachText();
        }

        private void QueryBySerachText()
        {
            if (IsChinese(searchTextBox.Text.Trim()))
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.ItemSource.Where(c => c.Name.Contains(value)).ToList();
                this.listbox.ItemsSource = list;
            }
            else
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.ItemSource.Where(c => c.NameShortPY.Contains(value)).ToList();
                this.listbox.ItemsSource = list;
            }

            this.listbox.GetScrollHost().ScrollToTop();
        }

        public bool IsChinese(string CString)
        {
            return Regex.IsMatch(CString, @"^[\u4e00-\u9fa5]+$");
        }

        #region 属性

        public List<DataModel> ItemSource
        {
            get { return (List<DataModel>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<DataModel>), typeof(MDFSearchCombox), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if ((sender as MDFSearchCombox).ItemSource != null)
                {
                    if (!string.IsNullOrEmpty((sender as MDFSearchCombox).SelectedId))
                    {
                        var selectObj = (sender as MDFSearchCombox).ItemSource.FirstOrDefault(c => c.Id.ToString() == (sender as MDFSearchCombox).SelectedId);

                        (sender as MDFSearchCombox).SelectItem = selectObj;
                    }

                    if (!string.IsNullOrEmpty((sender as MDFSearchCombox).SelectedName))
                    {
                        var selectObj = (sender as MDFSearchCombox).ItemSource.FirstOrDefault(c => c.Name.ToString() == (sender as MDFSearchCombox).SelectedName);

                        (sender as MDFSearchCombox).SelectItem = selectObj;
                    }
                }
            })));

        public DataModel SelectItem
        {
            get { return (DataModel)GetValue(SelectItemProperty); }
            set { SetValue(SelectItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectItemProperty =
            DependencyProperty.Register("SelectItem", typeof(DataModel), typeof(MDFSearchCombox), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null)
                {
                    (sender as MDFSearchCombox).pupup.IsOpen = false;
                    (sender as MDFSearchCombox).SelectedId = (e.NewValue as DataModel).Id.ToString();
                    (sender as MDFSearchCombox).SelectedName = (e.NewValue as DataModel).Name;
                    (sender as MDFSearchCombox).lblText.Text = (e.NewValue as DataModel).Name;
                }
            })));

        public string SelectedId
        {
            get { return (string)GetValue(SelectedIdProperty); }
            set { SetValue(SelectedIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIdProperty =
            DependencyProperty.Register("SelectedId", typeof(string), typeof(MDFSearchCombox), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if ((sender as MDFSearchCombox).ItemSource != null)
                    {
                        var selectObj = (sender as MDFSearchCombox).ItemSource.FirstOrDefault(c => c.Id.ToString() == e.NewValue.ToString());

                        (sender as MDFSearchCombox).SelectItem = selectObj;
                    }
                })));


        public string SelectedName
        {
            get { return (string)GetValue(SelectedNameProperty); }
            set { SetValue(SelectedNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNameProperty =
            DependencyProperty.Register("SelectedName", typeof(string), typeof(MDFSearchCombox), new PropertyMetadata(""));


        #endregion
    }
}
