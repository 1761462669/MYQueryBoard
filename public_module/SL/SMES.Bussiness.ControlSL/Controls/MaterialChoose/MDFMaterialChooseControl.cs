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
using MDF.Framework.Db;
using SMES.Bussiness.ControlSL.Controls.MaterialChoose.Command;
using System.Text;
using System.Windows.Data;

namespace SMES.Bussiness.ControlSL.MaterialChoose
{
    public class MDFMaterialChooseControl : ContentControl
    {
        public event RoutedEventHandler MaterialSelectedChanged;

        TreeView treeType;
        TextBox searchTextBox;
        ListBox listbox;
        Popup pupup;
        TextBlock lblText;
        ToggleButton DropDownToggle;
        Border PopupBorder;
        FrameworkElement root;

        public MDFMaterialChooseControl()
        {
            this.DefaultStyleKey = typeof(MDFMaterialChooseControl);
            MaterialsTemp = new List<MaterialModelAdpt>();
            SelectedManyIds = new List<string>();
            SelectedManyMaterials = new List<MaterialModelAdpt>();

            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(typeof(MaterialModelAdpt).Assembly);
        }

        public MDFMaterialChooseControlViewModel ViewModel { get; set; }

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
            lblText.Text = "请选择";
            DropDownToggle = this.GetTemplateChild("DropDownToggle") as ToggleButton;
            PopupBorder = this.GetTemplateChild("PopupBorder") as Border;

            Border ContentPresenterBorder = this.GetTemplateChild("ContentPresenterBorder") as Border;
            ContentPresenterBorder.MouseLeftButtonDown += (sender, e) =>
            {
                this.pupup.IsOpen = true;
                ArrangePopup();
            };

            DropDownToggle.Click += DropDownToggle_Click;
            listbox.SelectionChanged += listbox_SelectionChanged;
            treeType.SelectedItemChanged += treeType_SelectedItemChanged;

            if (QueryMaterialTypeIds == null || QueryMaterialTypeIds.Count == 0)
            {
                QueryMaterialTypeIds = this._QueryMaterialTypes.Select(c => c.Value).ToList();
            }

            if (this.SelectedMaterialName != null && this.SelectedMaterialName.ToString() != "")
            {
                this.lblText.Text = SelectedMaterialName;
            }

            if (ViewModel != null)
            {
                Binding bingdingItemSource = new Binding("ItemSource");
                bingdingItemSource.Source = ViewModel;
                this.SetBinding(ItemSourceProperty, bingdingItemSource);

                Binding bingdingTypes = new Binding("MaterialTypes");
                bingdingTypes.Source = ViewModel;
                this.SetBinding(MaterialTypesProperty, bingdingTypes);

                return;
            }

            GetMaterialTypes();
        }

        void DropDownToggle_Click(object sender, RoutedEventArgs e)
        {
            this.pupup.IsOpen = true;
            ArrangePopup();

            if (   this.SelectedManyMaterials!=null)
            {
                this.SelectedManyMaterials.Clear();
            }

            if (  this.SelectedManyIds!=null)
            {
                this.SelectedManyIds.Clear();
            }

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
                this.SelectedMaterial = listbox.SelectedItem as MaterialModelAdpt;

                if (MaterialSelectedChanged != null)
                {
                    MaterialSelectedChanged(this, new RoutedEventArgs());
                }
            }
        }

        void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            QueryBySerachText();
        }

        private void QueryBySerachText()
        {
            List<MaterialModelAdpt> sources = new List<MaterialModelAdpt>();

            if (IsChinese(searchTextBox.Text.Trim()))
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.MaterialsTemp.Where(c => c.Name.Contains(value)).ToList();
                sources = list;
            }
            else
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.MaterialsTemp.Where(c => c.NamePY.Contains(value) || c.Cd.Trim().ToLower().Contains(value)).ToList();
               
                sources = list;
            }

            foreach (var item in sources)
            {
                item.PropertyChanged -= item_PropertyChanged;
                item.PropertyChanged += item_PropertyChanged;
            }

            this.listbox.ItemsSource = sources;
            this.listbox.GetScrollHost().ScrollToTop();
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                if (this.SelectedManyIds == null)
                {
                    this.SelectedManyIds = new List<string>();
                }
                MaterialModelAdpt obj = (sender as MaterialModelAdpt);
                if (obj.IsChecked)
                {
                    if (!this.SelectedManyIds.Contains(obj.Id.ToString()))
                    {
                        if (this.SelectedManyMaterials == null)
                        {
                            this.SelectedManyMaterials = new List<MaterialModelAdpt>();
                        }

               
                        this.SelectedManyMaterials.Add(obj);
                        this.SelectedManyIds.Add(obj.Id.ToString());
                    }
                }
                else
                {
                    this.SelectedManyMaterials.Remove(obj);
                    this.SelectedManyIds.Remove(obj.Id.ToString());
                }

                if (this.SelectedManyMaterials.Count != 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in this.SelectedManyMaterials)
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

        public bool IsChinese(string CString)
        {
            return Regex.IsMatch(CString, @"^[\u4e00-\u9fa5]+$");
        }

        void treeType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedType = treeType.SelectedItem as MaterialTypeModelAdpt;
        }

        #region 属性

        #region 条件、数据源属性
        public List<MaterialModelAdpt> ItemSource
        {
            get { return (List<MaterialModelAdpt>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<MaterialModelAdpt>), typeof(MDFMaterialChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                //郑州老冉提出的选中黄金叶下面的一个烟叶以后再选黄金叶下的烟叶无法选择 
                //注释 by yangyang 2015-8-24
                //if ((sender as MDFMaterialChooseControl).ItemSource != null)
                //{
                //    if (!string.IsNullOrEmpty((sender as MDFMaterialChooseControl).SelectedMaterialId))
                //    {
                //        var selectObj = (sender as MDFMaterialChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == (sender as MDFMaterialChooseControl).SelectedMaterialId);

                //        (sender as MDFMaterialChooseControl).SelectedMaterial = selectObj;
                //    }

                //    if (!string.IsNullOrEmpty((sender as MDFMaterialChooseControl).SelectedMaterialName))
                //    {
                //        var selectObj = (sender as MDFMaterialChooseControl).ItemSource.FirstOrDefault(c => c.Name.ToString() == (sender as MDFMaterialChooseControl).SelectedMaterialName);

                //        (sender as MDFMaterialChooseControl).SelectedMaterial = selectObj;
                //    }
                //}
            })));

        private List<MaterialModelAdpt> MaterialsTemp { get; set; }

        public MaterialTypeModelAdpt SelectedType
        {
            get { return (MaterialTypeModelAdpt)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTypeProperty =
            DependencyProperty.Register("SelectedType", typeof(MaterialTypeModelAdpt), typeof(MDFMaterialChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
                {
                    var obj = e.NewValue;

                    if (!DesignerProperties.IsInDesignTool)
                    {
                        (sender as MDFMaterialChooseControl).GetMaterialsBySelectType();
                    }

                })));


        public IList<MaterialTypeModelAdpt> MaterialTypes
        {
            get { return (IList<MaterialTypeModelAdpt>)GetValue(MaterialTypesProperty); }
            set { SetValue(MaterialTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaterialTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaterialTypesProperty =
            DependencyProperty.Register("MaterialTypes", typeof(IList<MaterialTypeModelAdpt>), typeof(MDFMaterialChooseControl), new PropertyMetadata(null));


        private List<QueryMaterialType> _QueryMaterialTypes = new List<QueryMaterialType>();
        public List<QueryMaterialType> QueryMaterialTypes
        {
            get { return this._QueryMaterialTypes; }
            set
            {
                if (this._QueryMaterialTypes != value)
                {
                    this._QueryMaterialTypes = value;
                }
            }
        }

        public List<string> QueryMaterialTypeIds { get; set; }


        public string QueryMaterialTypeIdString
        {
            get { return (string)GetValue(QueryMaterialTypeIdStringProperty); }
            set { SetValue(QueryMaterialTypeIdStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QueryMaterialTypeIdString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QueryMaterialTypeIdStringProperty =
            DependencyProperty.Register("QueryMaterialTypeIdString", typeof(string), typeof(MDFMaterialChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null && e.NewValue.ToString() != "")
                {
                    (sender as MDFMaterialChooseControl).QueryMaterialTypeIds = e.NewValue.ToString().Split(',').ToList();
                }

            })));

        #endregion

        #region 单项选择属性

        public MaterialModelAdpt SelectedMaterial
        {
            get { return (MaterialModelAdpt)GetValue(SelectedMaterialProperty); }
            set { SetValue(SelectedMaterialProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMaterialProperty =
            DependencyProperty.Register("SelectedMaterial", typeof(MaterialModelAdpt), typeof(MDFMaterialChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
                {
                    if (e.NewValue != null)
                    {
                        (sender as MDFMaterialChooseControl).pupup.IsOpen = false;

                        (sender as MDFMaterialChooseControl).SelectedMaterialId = (e.NewValue as MaterialModelAdpt).Id.ToString();
                        (sender as MDFMaterialChooseControl).SelectedMaterialName = (e.NewValue as MaterialModelAdpt).Name;
                        (sender as MDFMaterialChooseControl).lblText.Text = (e.NewValue as MaterialModelAdpt).Name;
                    }
                })));

        public string SelectedMaterialId
        {
            get { return (string)GetValue(SelectedMaterialIdProperty); }
            set { SetValue(SelectedMaterialIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMaterialIdProperty =
            DependencyProperty.Register("SelectedMaterialId", typeof(string), typeof(MDFMaterialChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if ((sender as MDFMaterialChooseControl).ItemSource != null && e.NewValue != null)
                    {
                        var selectObj = (sender as MDFMaterialChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == e.NewValue.ToString());

                        (sender as MDFMaterialChooseControl).SelectedMaterial = selectObj;
                    }
                    else
                    {
                        (sender as MDFMaterialChooseControl).SelectedMaterial = null;
                    }
                })));


        public string SelectedMaterialName
        {
            get { return (string)GetValue(SelectedMaterialNameProperty); }
            set { SetValue(SelectedMaterialNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMaterialNameProperty =
            DependencyProperty.Register("SelectedMaterialName", typeof(string), typeof(MDFMaterialChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue!=null)
                {
                    var selectObj = (sender as MDFMaterialChooseControl).MaterialsTemp.FirstOrDefault(c => c.Name.ToString() == e.NewValue.ToString());

                    (sender as MDFMaterialChooseControl).SelectedMaterial = selectObj;
                }
                else
                {
                    (sender as MDFMaterialChooseControl).SelectedMaterial = null;
                    var lblText = (sender as MDFMaterialChooseControl).lblText;
                    if (lblText!=null)
                    {
                        lblText.Text = "请选择";
                    }
                   
                }
                
            })));


        #endregion

        #region 多项选择

        public List<string> SelectedManyIds
        {
            get { return (List<string>)GetValue(SelectedManyIdsProperty); }
            set { SetValue(SelectedManyIdsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedManyIds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedManyIdsProperty =
            DependencyProperty.Register("SelectedManyIds", typeof(List<string>), typeof(MDFMaterialChooseControl), new PropertyMetadata(new List<string>()));

        public List<MaterialModelAdpt> SelectedManyMaterials
        {
            get { return (List<MaterialModelAdpt>)GetValue(SelectedManyMaterialsProperty); }
            set { SetValue(SelectedManyMaterialsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedManyMaterials.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedManyMaterialsProperty =
            DependencyProperty.Register("SelectedManyMaterials", typeof(List<MaterialModelAdpt>), typeof(MDFMaterialChooseControl), new PropertyMetadata(new List<MaterialModelAdpt>()));


        #endregion

        public ChooseMode SelectMode
        {
            get { return (ChooseMode)GetValue(SelectModeProperty); }
            set { SetValue(SelectModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectModeProperty =
            DependencyProperty.Register("SelectMode", typeof(ChooseMode), typeof(MDFMaterialChooseControl), new PropertyMetadata(ChooseMode.Single));



        public double PopupHeight
        {
            get { return (double)GetValue(PopupHeightProperty); }
            set { SetValue(PopupHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupHeightProperty =
            DependencyProperty.Register("PopupHeight", typeof(double), typeof(MDFMaterialChooseControl), new PropertyMetadata(300d));

        


        #endregion

        #region 调用服务

        public async void GetMaterialTypes()
        {
            var typeIds = QueryMaterialTypeIds;

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var result = await wcf.Invoke<IComMaterialTypeService>(c => c.GetMaterialTypes(typeIds));

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialTypeModelAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                MaterialTypes = list;
            }
        }

        public async void GetMaterialsBySelectType()
        {
            string typeId = SelectedType.Id.ToString();

            HqlQuerySetting qsMaterial = new HqlQuerySetting();
            qsMaterial.QueryString = string.Format("select r from SMES.FrameworkAdpt.MaterialChooseControl.MaterialModelAdpt r where r.MaterialType.Id = {0} and r.IsUsed = true", typeId);

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<MDF.Framework.Db.IDatabaseService>(c => c.Query(qsMaterial));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialModelAdpt>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                SetVisbleDatas(list);

                MaterialsTemp = list;

                list.Insert(0, new MaterialModelAdpt() { Id = -1, Name = "请选择", Cd="" });
                this.ItemSource = list;

                QueryBySerachText();
            }
        }

        public async void GetInitMaterials()
        {
            var typeIds = QueryMaterialTypeIds;

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<IComMaterialTypeService>(c => c.GetMaterials(typeIds));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialModelAdpt>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                SetVisbleDatas(list);

                ItemSource = list;
                MaterialsTemp = list;
            }
        }

        private void SetVisbleDatas(List<MaterialModelAdpt> modes)
        {
            if (this.SelectMode == ChooseMode.Many)
            {
                foreach (var item in modes)
                {
                    item.PropertyChanged -= item_PropertyChanged;
                    item.PropertyChanged += item_PropertyChanged;

                    item.IsExpand = true;

                    if (this.SelectedManyIds != null)
                    {
                        if (this.SelectedManyIds.Contains(item.Id.ToString()))
                        {
                            item.IsChecked = true;
                        }
                    }
                }
            }
        }

        #endregion
    }

    public class QueryMaterialType:DependencyObject
    {
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(QueryMaterialType), new PropertyMetadata(""));

    }

    public enum ChooseMode
    {
        Single = 1,
        Many = 2
    }
}
