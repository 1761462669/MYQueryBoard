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

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFMaterialChooseControl : ComboBox
    {
        public event RoutedEventHandler MaterialSelectedChanged;

        TreeView treeType;
        TextBox searchTextBox;
        ListBox listbox;
        Popup pupup;
        TextBlock lblText;

        public MDFMaterialChooseControl()
        {
            this.DefaultStyleKey = typeof(MDFMaterialChooseControl);
            MaterialsTemp = new List<MaterialModelAdpt>();

            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(typeof(MaterialModelAdpt).Assembly);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            searchTextBox = this.GetTemplateChild("serachTextBox") as TextBox;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            treeType = this.GetTemplateChild("treeType") as TreeView;
            listbox = this.GetTemplateChild("listbox") as ListBox;
            pupup = this.GetTemplateChild("Popup") as Popup;
            lblText = this.GetTemplateChild("lblText") as TextBlock;            
            lblText.Text = "请选择";

            listbox.SelectionChanged += listbox_SelectionChanged;

            treeType.SelectedItemChanged += treeType_SelectedItemChanged;

            if (!DesignerProperties.IsInDesignTool)
            {
                if (QueryMaterialTypeIds == null || QueryMaterialTypeIds.Count == 0)
                {
                    QueryMaterialTypeIds = this._QueryMaterialTypes.Select(c => c.Value).ToList();
                }

                GetInitMaterials();

                GetMaterialTypes();
            }
        }

        void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedItem == null) return;

            this.SelectedMaterial = listbox.SelectedItem as MaterialModelAdpt;

            if(MaterialSelectedChanged != null)
            {
                MaterialSelectedChanged(this, new RoutedEventArgs());
            }
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
                var list = this.MaterialsTemp.Where(c => c.Name.Contains(value)).ToList();
                this.listbox.ItemsSource = list;
            }
            else
            {
                var value = searchTextBox.Text.Trim().ToLower();
                var list = this.MaterialsTemp.Where(c => c.NamePY.Contains(value)).ToList();
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
            SelectedType = treeType.SelectedItem as MaterialTypeModelAdpt;
        }

        #region 属性

        public List<MaterialModelAdpt> ItemSource
        {
            get { return (List<MaterialModelAdpt>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<MaterialModelAdpt>), typeof(MDFMaterialChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {

            })));

        public List<MaterialModelAdpt> MaterialsTemp { get; set; }

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

                    (sender as MDFMaterialChooseControl).GetMaterialsBySelectType();
                })));


        public IList<MaterialTypeModelAdpt> MaterialTypes
        {
            get { return (IList<MaterialTypeModelAdpt>)GetValue(MaterialTypesProperty); }
            set { SetValue(MaterialTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaterialTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaterialTypesProperty =
            DependencyProperty.Register("MaterialTypes", typeof(IList<MaterialTypeModelAdpt>), typeof(MDFMaterialChooseControl), new PropertyMetadata(null));


        private List<QueryMaterialType> _QueryMaterialTypes = new List<QueryMaterialType> ();
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
            DependencyProperty.Register("SelectedMaterialId", typeof(string), typeof(MDFMaterialChooseControl), new PropertyMetadata(""));


        public string SelectedMaterialName
        {
            get { return (string)GetValue(SelectedMaterialNameProperty); }
            set { SetValue(SelectedMaterialNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMaterialNameProperty =
            DependencyProperty.Register("SelectedMaterialName", typeof(string), typeof(MDFMaterialChooseControl), new PropertyMetadata(""));




        

        #endregion

        #region 调用服务

        //public async void GetMaterialTypes()
        //{
        //    MDF.Framework.Db.HqlQuerySetting qs = new Framework.Db.HqlQuerySetting();
        //    string hql = "";

        //    if (MaterialTypeIdString == "All")
        //    {
        //        hql = string.Format("select r from SMES.FrameworkAdpt.MaterialChooseControl.MaterialTypeModelAdpt r where r.IsUsed = true");
        //    }
        //    else
        //    {
        //        hql = string.Format("select r from SMES.FrameworkAdpt.MaterialChooseControl.MaterialTypeModelAdpt r where r.IsUsed = true and id in ({0})", MaterialTypeIdString);
        //    }

        //    qs.QueryString = hql;

        //    MDF.Framework.Bus.SynInvokeWcfService wcf = new Framework.Bus.SynInvokeWcfService();
        //    var result = await wcf.Invoke<MDF.Framework.Db.IDatabaseService>(c => c.Query(qs));

        //    if (result.IsSuccess)
        //    {
        //        if (MaterialTypeIdString == "All")
        //        {
        //            var list = InfoExchange.DeConvert<List<MaterialTypeModelAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

        //            var firstLeaveNodes = list.Where(c => c.Parent == null).ToList();

        //            foreach (var item in firstLeaveNodes)
        //            {
        //                SetChilds(item, list);
        //            }

        //            MaterialTypes = firstLeaveNodes;
        //        }
        //        else
        //        {
        //            var list = InfoExchange.DeConvert<List<MaterialTypeModelAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
        //            MaterialTypes = list;
        //        }


        //    }
        //}

        public async void GetMaterialTypes()
        {
            var typeIds = QueryMaterialTypeIds;

            MDF.Framework.Bus.SynInvokeWcfService wcf = new Framework.Bus.SynInvokeWcfService();
            var result = await wcf.Invoke<IComMaterialTypeService>(c => c.GetMaterialTypes(typeIds));

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialTypeModelAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                MaterialTypes = list;
            }
        }

        private void SetChilds(MaterialTypeModelAdpt node, List<MaterialTypeModelAdpt> list)
        {
            var childs = list.Where(c => c.Parent != null && c.Parent.Id == node.Id).Select(c => c as IHierarchyModel).ToList();

            if (childs != null && childs.Count != 0)
            {
                foreach (var child in childs)
                {
                    SetChilds(child as MaterialTypeModelAdpt, list);
                }

                node.Childs = childs;
            }
        }

        public async void GetMaterialsBySelectType()
        {
            string typeId = SelectedType.Id.ToString();

            MDF.Framework.Db.HqlQuerySetting qsMaterial = new Framework.Db.HqlQuerySetting();
            qsMaterial.QueryString = string.Format("select r from SMES.FrameworkAdpt.MaterialChooseControl.MaterialModelAdpt r where r.MaterialType.Id = {0} and r.IsUsed = true", typeId);

            MDF.Framework.Bus.SynInvokeWcfService wcf = new Framework.Bus.SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<MDF.Framework.Db.IDatabaseService>(c => c.Query(qsMaterial));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialModelAdpt>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                MaterialsTemp = list;

                QueryBySerachText();
            }
        }

        //public async void GetInitMaterials()
        //{
        //    string hql = "";

        //    if (MaterialTypeIdString == "All")
        //    {
        //        hql = string.Format("select r from SMES.FrameworkAdpt.MaterialChooseControl.MaterialModelAdpt r where r.IsUsed = true");
        //    }
        //    else
        //    {
        //        hql = string.Format("select r from SMES.FrameworkAdpt.MaterialChooseControl.MaterialModelAdpt r where r.MaterialType.Id in ({0}) and r.IsUsed = true", MaterialTypeIdString);
        //    }

        //    MDF.Framework.Db.HqlQuerySetting qsMaterial = new Framework.Db.HqlQuerySetting();
        //    qsMaterial.QueryString = hql;

        //    MDF.Framework.Bus.SynInvokeWcfService wcf = new Framework.Bus.SynInvokeWcfService();
        //    var resultMaterial = await wcf.Invoke<MDF.Framework.Db.IDatabaseService>(c => c.Query(qsMaterial));

        //    if (resultMaterial.IsSuccess)
        //    {
        //        var list = InfoExchange.DeConvert<List<MaterialModelAdpt>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

        //        ItemSource = list;
        //        MaterialsTemp = list;
        //    }
        //}

        public async void GetInitMaterials()
        {
            var typeIds = QueryMaterialTypeIds;

            MDF.Framework.Bus.SynInvokeWcfService wcf = new Framework.Bus.SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<IComMaterialTypeService>(c => c.GetMaterials(typeIds));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialModelAdpt>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                ItemSource = list;
                MaterialsTemp = list;
            }
        }

        #endregion
    }

    public class QueryMaterialType
    {
        public string Value { get; set; }
    }
}
