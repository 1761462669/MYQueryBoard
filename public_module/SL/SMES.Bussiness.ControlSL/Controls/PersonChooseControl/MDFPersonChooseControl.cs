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

namespace SMES.Bussiness.ControlSL.PersonChooseControl
{
    public class MDFPersonChooseControl : ContentControl
    {
        TreeView treeType;
        TextBox searchTextBox;
        ListBox listbox;
        Popup pupup;
        TextBlock lblText;
        ToggleButton DropDownToggle;
        Border border;
        FrameworkElement root;

        public MDFPersonChooseControl()
        {
            this.DefaultStyleKey = typeof(MDFPersonChooseControl);
            PersonsTemp = new List<PersonModel>();

            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(typeof(SiteModel).Assembly);
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

            if (QueryAreaIds == null || QueryAreaIds.Count == 0)
            {
                QueryAreaIds = this._QueryAreas.Select(c => c.Value).ToList();
            }

            GetAreas();
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

            this.SelectedPerson = listbox.SelectedItem as PersonModel;
            this.SelectedPersonId = this.SelectedPerson.Id.ToString();
            this.SelectedPersonName = this.SelectedPerson.Name;

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
                var list = this.PersonsTemp.Where(c => c.Name.Contains(value)).ToList();
                this.listbox.ItemsSource = list;
            }
            else
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.PersonsTemp.Where(c => c.NamePY.Contains(value)).ToList();
                this.listbox.ItemsSource = list;
            }

            this.listbox.GetScrollHost().ScrollToTop();
        }

        public bool IsChinese(string CString)
        {
            return Regex.IsMatch(CString, @"^[\u4e00-\u9fa5]+$");
        }

        void treeType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedType = treeType.SelectedItem as AreaModel;
        }

        #region 属性

        public List<PersonModel> ItemSource
        {
            get { return (List<PersonModel>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<PersonModel>), typeof(MDFPersonChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if ((sender as MDFPersonChooseControl).ItemSource != null)
                {
                    if (!string.IsNullOrEmpty((sender as MDFPersonChooseControl).SelectedPersonId))
                    {
                        var selectObj = (sender as MDFPersonChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == (sender as MDFPersonChooseControl).SelectedPersonId);

                        (sender as MDFPersonChooseControl).SelectedPerson = selectObj;
                    }

                    if (!string.IsNullOrEmpty((sender as MDFPersonChooseControl).SelectedPersonName))
                    {
                        var selectObj = (sender as MDFPersonChooseControl).ItemSource.FirstOrDefault(c => c.Name.ToString() == (sender as MDFPersonChooseControl).SelectedPersonName);

                        (sender as MDFPersonChooseControl).SelectedPerson = selectObj;
                    }
                }
            })));

        public List<PersonModel> PersonsTemp { get; set; }

        public AreaModel SelectedType
        {
            get { return (AreaModel)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTypeProperty =
            DependencyProperty.Register("SelectedType", typeof(AreaModel), typeof(MDFPersonChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                var obj = e.NewValue;

                (sender as MDFPersonChooseControl).GetPersonsBySelectArea();
            })));


        public IList<IHierarchyModel> Areas
        {
            get { return (IList<IHierarchyModel>)GetValue(AreasProperty); }
            set { SetValue(AreasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaterialTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreasProperty =
            DependencyProperty.Register("Areas", typeof(IList<IHierarchyModel>), typeof(MDFPersonChooseControl), new PropertyMetadata(null));


        private List<QueryAreaType> _QueryAreas = new List<QueryAreaType>();
        public List<QueryAreaType> QueryAreas
        {
            get { return this._QueryAreas; }
            set
            {
                if (this._QueryAreas != value)
                {
                    this._QueryAreas = value;
                }
            }
        }

        public List<string> QueryAreaIds { get; set; }


        public string QueryAreaIdString
        {
            get { return (string)GetValue(QueryAreaIdStringProperty); }
            set { SetValue(QueryAreaIdStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QueryMaterialTypeIdString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QueryAreaIdStringProperty =
            DependencyProperty.Register("QueryAreaIdString", typeof(string), typeof(MDFPersonChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null && e.NewValue.ToString() != "")
                {
                    (sender as MDFPersonChooseControl).QueryAreaIds = e.NewValue.ToString().Split(',').ToList();
                }

            })));



        public PersonModel SelectedPerson
        {
            get { return (PersonModel)GetValue(SelectedPersonProperty); }
            set { SetValue(SelectedPersonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPersonProperty =
            DependencyProperty.Register("SelectedPerson", typeof(PersonModel), typeof(MDFPersonChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null)
                {
                    (sender as MDFPersonChooseControl).pupup.IsOpen = false;
                    (sender as MDFPersonChooseControl).SelectedPersonId = (e.NewValue as PersonModel).Id.ToString();
                    (sender as MDFPersonChooseControl).SelectedPersonName = (e.NewValue as PersonModel).Name;
                    (sender as MDFPersonChooseControl).lblText.Text = (e.NewValue as PersonModel).Name;
                }
            })));

        public string SelectedPersonId
        {
            get { return (string)GetValue(SelectedPersonIdProperty); }
            set { SetValue(SelectedPersonIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPersonIdProperty =
            DependencyProperty.Register("SelectedPersonId", typeof(string), typeof(MDFPersonChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if ((sender as MDFPersonChooseControl).ItemSource != null)
                    {
                        var selectObj = (sender as MDFPersonChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == e.NewValue.ToString());

                        (sender as MDFPersonChooseControl).SelectedPerson = selectObj;
                    }
                })));


        public string SelectedPersonName
        {
            get { return (string)GetValue(SelectedPersonNameProperty); }
            set { SetValue(SelectedPersonNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPersonNameProperty =
            DependencyProperty.Register("SelectedPersonName", typeof(string), typeof(MDFPersonChooseControl), new PropertyMetadata(""));


        #endregion

        #region 调用服务

        public async void GetAreas()
        {
            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var result = await wcf.Invoke<IOrgInfoService>(c => c.GetAreas(QueryAreaIds.First()));

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<IHierarchyModel>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                Areas = list;
            }
        }

        private void SetChilds(AreaModel node, List<AreaModel> list)
        {
            var childs = list.Where(c => c.Parent != null && c.Parent.Id == node.Id).Select(c => c as IHierarchyModel).ToList();

            if (childs != null && childs.Count != 0)
            {
                foreach (var child in childs)
                {
                    SetChilds(child as AreaModel, list);
                }

                node.Childs = childs;
            }
        }

        public async void GetPersonsBySelectArea()
        {
            string typeId = SelectedType.Id.ToString();

            HqlQuerySetting qsMaterial = new HqlQuerySetting();
            qsMaterial.QueryString = string.Format("select r from SMES.FrameworkAdpt.OrgInfo.PersonModel r where r.Area.Id = {0} and r.IsUsed = true", typeId);

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<MDF.Framework.Db.IDatabaseService>(c => c.Query(qsMaterial));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<PersonModel>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                PersonsTemp = list;

                QueryBySerachText();
            }
        }

        public async void GetInitPersons()
        {
            var typeIds = QueryAreaIds;

            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<IOrgInfoService>(c => c.GetPersons(typeIds.First()));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<PersonModel>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                ItemSource = list;
                PersonsTemp = list;
            }
        }

        #endregion
    }

    public class QueryAreaType:DependencyObject
    {
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(QueryAreaType), new PropertyMetadata(""));

    }
}
