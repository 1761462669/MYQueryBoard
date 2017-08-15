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
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using MDF.Framework.Db;
using SMES.FrameworkAdpt.EquipmentChoose;
using SMES.FrameworkAdpt.EquipmentChoose.Service;
using System.Text;

namespace SMES.Bussiness.ControlSL.EquipmentChoose
{
    public class MDFEquipmentChooseControl : ContentControl
    {
        public event RoutedEventHandler EquipmentSelectedChanged;

        TreeView treeType;
        TextBox searchTextBox;
        ListBox listbox;
        Popup pupup;
        TextBlock lblText;
        ToggleButton DropDownToggle;

        public MDFEquipmentChooseControl()
        {
            this.DefaultStyleKey = typeof(MDFEquipmentChooseControl);
            EquipmentsTemp = new List<EquipmentModelFAdpt>();
            SelectedManyIds = new List<string>();
            SelectedManyEquipments = new List<EquipmentModelFAdpt>();

            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(typeof(EquipmentModelFAdpt).Assembly);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DesignerProperties.IsInDesignTool) return;

            searchTextBox = this.GetTemplateChild("serachTextBox") as TextBox;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            treeType = this.GetTemplateChild("treeType") as TreeView;
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

            treeType.SelectedItemChanged += treeType_SelectedItemChanged;

            if (QueryEquipmentTypeIds == null || QueryEquipmentTypeIds.Count == 0)
            {
                QueryEquipmentTypeIds = this._QueryEquipmentTypes.Select(c => c.Value).ToList();
            }

            GetInitEquipments();

            GetEquipmentTypes();
            
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

            if (this.SelectMode == ChooseMode.Many)
            {

            }

            if (this.SelectMode == ChooseMode.Single)
            {
                this.SelectedEquipment = listbox.SelectedItem as EquipmentModelFAdpt;

                if (EquipmentSelectedChanged != null)
                {
                    EquipmentSelectedChanged(this, new RoutedEventArgs());
                }
            }
        }

        void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            QueryBySerachText();
        }

        private void QueryBySerachText()
        {
            List<EquipmentModelFAdpt> sources = new List<EquipmentModelFAdpt>();
            if (IsChinese(searchTextBox.Text.Trim()))
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.EquipmentsTemp.Where(c => c.Name.Contains(value)).ToList();
                sources = list;
            }
            else
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.EquipmentsTemp.Where(c => c.NamePY.Contains(value)).ToList();
                sources = list;
            }

            this.listbox.ItemsSource = sources;

            foreach (var item in sources)
            {
                item.PropertyChanged -= item_PropertyChanged;
                item.PropertyChanged += item_PropertyChanged;
            }

            this.listbox.GetScrollHost().ScrollToTop();
        }

        public bool IsChinese(string CString)
        {
            return Regex.IsMatch(CString, @"^[\u4e00-\u9fa5]+$");
        }

        void treeType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedType = treeType.SelectedItem as EquipmentTypeModelFAdpt;
        }

        #region 属性

        public List<EquipmentModelFAdpt> ItemSource
        {
            get { return (List<EquipmentModelFAdpt>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<EquipmentModelFAdpt>), typeof(MDFEquipmentChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if ((sender as MDFEquipmentChooseControl).ItemSource != null)
                {
                    if (!string.IsNullOrEmpty((sender as MDFEquipmentChooseControl).SelectedEquipmentId))
                    {
                        var selectObj = (sender as MDFEquipmentChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == (sender as MDFEquipmentChooseControl).SelectedEquipmentId);

                        (sender as MDFEquipmentChooseControl).SelectedEquipment = selectObj;
                    }

                    if (!string.IsNullOrEmpty((sender as MDFEquipmentChooseControl).SelectedEquipmentName))
                    {
                        var selectObj = (sender as MDFEquipmentChooseControl).ItemSource.FirstOrDefault(c => c.Name.ToString() == (sender as MDFEquipmentChooseControl).SelectedEquipmentName);

                        (sender as MDFEquipmentChooseControl).SelectedEquipment = selectObj;
                    }
                }
            })));

        public List<EquipmentModelFAdpt> EquipmentsTemp { get; set; }

        public EquipmentTypeModelFAdpt SelectedType
        {
            get { return (EquipmentTypeModelFAdpt)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTypeProperty =
            DependencyProperty.Register("SelectedType", typeof(EquipmentTypeModelFAdpt), typeof(MDFEquipmentChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
                {
                    var obj = e.NewValue;

                    (sender as MDFEquipmentChooseControl).GetEquipmentsBySelectType();
                })));


        public IList<EquipmentTypeModelFAdpt> EquipmentTypes
        {
            get { return (IList<EquipmentTypeModelFAdpt>)GetValue(EquipmentTypesProperty); }
            set { SetValue(EquipmentTypesProperty, value); }
        }

        public static readonly DependencyProperty EquipmentTypesProperty =
            DependencyProperty.Register("EquipmentTypes", typeof(IList<EquipmentTypeModelFAdpt>), typeof(MDFEquipmentChooseControl), new PropertyMetadata(null));


        private List<QueryEquipmentType> _QueryEquipmentTypes = new List<QueryEquipmentType> ();
        public List<QueryEquipmentType> QueryEquipmentTypes
        {
            get { return this._QueryEquipmentTypes; }
            set
            {
                if (this._QueryEquipmentTypes != value)
                {
                    this._QueryEquipmentTypes = value;
                }
            }
        }

        public List<string> QueryEquipmentTypeIds { get; set; }


        public string QueryEquipmentTypeIdString
        {
            get { return (string)GetValue(QueryEquipmentTypeIdStringProperty); }
            set { SetValue(QueryEquipmentTypeIdStringProperty, value); }
        }

        public static readonly DependencyProperty QueryEquipmentTypeIdStringProperty =
            DependencyProperty.Register("QueryEquipmentTypeIdString", typeof(string), typeof(MDFEquipmentChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null && e.NewValue.ToString() != "")
                {
                    (sender as MDFEquipmentChooseControl).QueryEquipmentTypeIds = e.NewValue.ToString().Split(',').ToList();
                    (sender as MDFEquipmentChooseControl).GetEquipmentTypes();
                }

            })));

        #region 单项选择

        public EquipmentModelFAdpt SelectedEquipment
        {
            get { return (EquipmentModelFAdpt)GetValue(SelectedEquipmentProperty); }
            set { SetValue(SelectedEquipmentProperty, value); }
        }

        public static readonly DependencyProperty SelectedEquipmentProperty =
            DependencyProperty.Register("SelectedEquipment", typeof(EquipmentModelFAdpt), typeof(MDFEquipmentChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
                {
                    if (e.NewValue != null)
                    {
                        (sender as MDFEquipmentChooseControl).pupup.IsOpen = false;

                        (sender as MDFEquipmentChooseControl).SelectedEquipmentId = (e.NewValue as EquipmentModelFAdpt).Id.ToString();
                        (sender as MDFEquipmentChooseControl).SelectedEquipmentName = (e.NewValue as EquipmentModelFAdpt).Name;
                        (sender as MDFEquipmentChooseControl).lblText.Text = (e.NewValue as EquipmentModelFAdpt).Name;
                    }
                })));

        public string SelectedEquipmentId
        {
            get { return (string)GetValue(SelectedEquipmentIdProperty); }
            set { SetValue(SelectedEquipmentIdProperty, value); }
        }

        public static readonly DependencyProperty SelectedEquipmentIdProperty =
            DependencyProperty.Register("SelectedEquipmentId", typeof(string), typeof(MDFEquipmentChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if ((sender as MDFEquipmentChooseControl).ItemSource != null && (sender as MDFEquipmentChooseControl).ItemSource.Count >0 )
                    {
                        var selectObj = (sender as MDFEquipmentChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == e.NewValue.ToString());

                        (sender as MDFEquipmentChooseControl).SelectedEquipment = selectObj;
                    }
                })));


        public string SelectedEquipmentName
        {
            get { return (string)GetValue(SelectedEquipmentNameProperty); }
            set { SetValue(SelectedEquipmentNameProperty, value); }
        }

        public static readonly DependencyProperty SelectedEquipmentNameProperty =
            DependencyProperty.Register("SelectedEquipmentName", typeof(string), typeof(MDFEquipmentChooseControl), new PropertyMetadata(""));

        public ChooseMode SelectMode
        {
            get { return (ChooseMode)GetValue(SelectModeProperty); }
            set { SetValue(SelectModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectModeProperty =
            DependencyProperty.Register("SelectMode", typeof(ChooseMode), typeof(MDFEquipmentChooseControl), new PropertyMetadata(ChooseMode.Single));

        #endregion

        #region 多项选择

        public List<string> SelectedManyIds
        {
            get { return (List<string>)GetValue(SelectedManyIdsProperty); }
            set { SetValue(SelectedManyIdsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedManyIds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedManyIdsProperty =
            DependencyProperty.Register("SelectedManyIds", typeof(List<string>), typeof(MDFEquipmentChooseControl), new PropertyMetadata(new List<string>()));

        public List<EquipmentModelFAdpt> SelectedManyEquipments
        {
            get { return (List<EquipmentModelFAdpt>)GetValue(SelectedManyEquipmentsProperty); }
            set { SetValue(SelectedManyEquipmentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedManyMaterials.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedManyEquipmentsProperty =
            DependencyProperty.Register("SelectedManyEquipments", typeof(List<EquipmentModelFAdpt>), typeof(MDFEquipmentChooseControl), new PropertyMetadata(new List<EquipmentModelFAdpt>()));


        #endregion

        #endregion

        #region 调用服务

        public async void GetEquipmentTypes()
        {
            var typeIds = QueryEquipmentTypeIds;

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var result = await wcf.Invoke<IEquipmentService>(c => c.GetTypes(typeIds));

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<EquipmentTypeModelFAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                EquipmentTypes = list;
            }
        }

        public async void GetEquipmentsBySelectType()
        {
            string typeId = SelectedType.Id.ToString();
            List<string> listTypeIdTemps = new List<string>();
            listTypeIdTemps.Add(typeId);

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var results = await wcf.Invoke<IEquipmentService>(c => c.GetEquipments(listTypeIdTemps));

            if (results.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<EquipmentModelFAdpt>>(results.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                SetVisbleDatas(list);

                EquipmentsTemp = list;

                QueryBySerachText();
            }
        }

        public async void GetInitEquipments()
        {
            var typeIds = QueryEquipmentTypeIds;

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var results = await wcf.Invoke<IEquipmentService>(c => c.GetEquipments(typeIds));

            if (results.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<EquipmentModelFAdpt>>(results.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                SetVisbleDatas(list);

                ItemSource = list;
                EquipmentsTemp = list;
            }
        }

        private void SetVisbleDatas(List<EquipmentModelFAdpt> models)
        {
            if (this.SelectMode == ChooseMode.Many)
            {
                foreach (var item in models)
                {
                    item.PropertyChanged -= item_PropertyChanged;
                    item.PropertyChanged += item_PropertyChanged;

                    item.IsExpand = true;

                    if(this.SelectedManyIds != null)
                    {
                        if (this.SelectedManyIds.Contains(item.Id.ToString()))
                        {
                            item.IsChecked = true;
                        }
                    }
                }
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                EquipmentModelFAdpt obj = (sender as EquipmentModelFAdpt);
                if (obj.IsChecked)
                {
                    if (!this.SelectedManyIds.Contains(obj.Id.ToString()))
                    {
                        if (this.SelectedManyEquipments == null)
                        {
                            this.SelectedManyEquipments = new List<EquipmentModelFAdpt>();
                        }
                        if(this.SelectedManyIds == null)
                        {
                            this.SelectedManyIds = new List<string>();
                        }

                        this.SelectedManyEquipments.Add(obj);
                        this.SelectedManyIds.Add(obj.Id.ToString());
                    }
                }
                else
                {
                    this.SelectedManyEquipments.Remove(obj);
                    this.SelectedManyIds.Remove(obj.Id.ToString());
                }

                if(SelectedManyEquipments.Count != 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in this.SelectedManyEquipments)
                    {
                        sb.Append(item.Name);
                        sb.Append(",");
                    }

                    if (sb.ToString().EndsWith(","))
                    {
                        string text = sb.ToString().Substring(0, sb.Length - 1);
                        this.lblText.Text = text;

                        ToolTipService.SetToolTip(lblText, text);
                    }
                }
                else
                {
                    this.lblText.Text = "";
                }
                
            }
        }

        #endregion
    }

    public class QueryEquipmentType:DependencyObject
    {
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(QueryEquipmentType), new PropertyMetadata(""));

    }

    public enum ChooseMode
    {
        Single = 1,
        Many = 2
    }
}
