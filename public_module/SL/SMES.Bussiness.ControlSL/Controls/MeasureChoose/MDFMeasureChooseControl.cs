using SMES.FrameworkAdpt.MeasureControl.Model.MeasureModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using MDF.Framework.Bus;
using SMES.FrameworkAdpt.MeasureControl.Service;
using System.Text.RegularExpressions;

namespace SMES.Bussiness.ControlSL.Controls.MeasureChoose
{
    public class MDFMeasureChooseControl : ContentControl
    {
        public event RoutedEventHandler MeasureSelectedChanged;

        TreeView treeType;
        TextBox searchTextBox;
        ListBox listbox;
        Popup pupup;
        TextBlock lblText;
        ToggleButton DropDownToggle;
        Border border;
        FrameworkElement root;

        public MDFMeasureChooseControl()
        {
            this.DefaultStyleKey = typeof(MDFMeasureChooseControl);
            PersonsTemp = new List<MeasureModelFAdpt>();

            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(typeof(MeasureModelFAdpt).Assembly);
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

            if (QueryTypeIds == null || QueryTypeIds.Count == 0)
            {
                QueryTypeIds = this._QueryTypes.Select(c => c.Value).ToList();
            }

            if (this.SelectedMeasureName != null && this.SelectedMeasureName.ToString() != "")
            {
                this.lblText.Text = SelectedMeasureName;
            }

            GetTypes(QueryTypeIds);
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

            this.SelectedMeasure = listbox.SelectedItem as MeasureModelFAdpt;
            this.SelectedMeasureId = this.SelectedMeasure.Id.ToString();
            this.SelectedMeasureName = this.SelectedMeasure.Name;

            if(this.MeasureSelectedChanged != null)
            {
                MeasureSelectedChanged(this, new RoutedEventArgs());
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
            SelectedType = treeType.SelectedItem as MeasureTypeModelFAdpt;
        }

        #region 属性

        public List<MeasureModelFAdpt> ItemSource
        {
            get { return (List<MeasureModelFAdpt>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<MeasureModelFAdpt>), typeof(MDFMeasureChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if ((sender as MDFMeasureChooseControl).ItemSource != null)
                {
                    if (!string.IsNullOrEmpty((sender as MDFMeasureChooseControl).SelectedMeasureId))
                    {
                        var selectObj = (sender as MDFMeasureChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == (sender as MDFMeasureChooseControl).SelectedMeasureId);

                        (sender as MDFMeasureChooseControl).SelectedMeasure = selectObj;
                    }

                    if (!string.IsNullOrEmpty((sender as MDFMeasureChooseControl).SelectedMeasureName))
                    {
                        var selectObj = (sender as MDFMeasureChooseControl).ItemSource.FirstOrDefault(c => c.Name.ToString() == (sender as MDFMeasureChooseControl).SelectedMeasureName);

                        (sender as MDFMeasureChooseControl).SelectedMeasure = selectObj;
                    }
                }
            })));

        public List<MeasureModelFAdpt> PersonsTemp { get; set; }

        public MeasureTypeModelFAdpt SelectedType
        {
            get { return (MeasureTypeModelFAdpt)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTypeProperty =
            DependencyProperty.Register("SelectedType", typeof(MeasureTypeModelFAdpt), typeof(MDFMeasureChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                var obj = e.NewValue;

                if(obj != null)
                {
                    string selectTypeId = (e.NewValue as MeasureTypeModelFAdpt).Id.ToString();

                    List<string> typeIds = new List<string>();
                    typeIds.Add(selectTypeId);

                    (sender as MDFMeasureChooseControl).GetMeasures(typeIds);
                }

            })));


        public IList<MeasureTypeModelFAdpt> MeasureTypes
        {
            get { return (IList<MeasureTypeModelFAdpt>)GetValue(MeasureTypesProperty); }
            set { SetValue(MeasureTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaterialTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeasureTypesProperty =
            DependencyProperty.Register("MeasureTypes", typeof(IList<MeasureTypeModelFAdpt>), typeof(MDFMeasureChooseControl), new PropertyMetadata(null));


        private List<QueryMeasureType> _QueryTypes = new List<QueryMeasureType>();
        public List<QueryMeasureType> QueryTypes
        {
            get { return this._QueryTypes; }
            set
            {
                if (this._QueryTypes != value)
                {
                    this._QueryTypes = value;
                }
            }
        }

        public List<string> QueryTypeIds { get; set; }


        public string QueryTypeIdString
        {
            get { return (string)GetValue(QueryTypeIdStringProperty); }
            set { SetValue(QueryTypeIdStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QueryMaterialTypeIdString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QueryTypeIdStringProperty =
            DependencyProperty.Register("QueryTypeIdString", typeof(string), typeof(MDFMeasureChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null && e.NewValue.ToString() != "")
                {
                    (sender as MDFMeasureChooseControl).QueryTypeIds = e.NewValue.ToString().Split(',').ToList();
                }
            })));

        public MeasureModelFAdpt SelectedMeasure
        {
            get { return (MeasureModelFAdpt)GetValue(SelectedMeasureProperty); }
            set { SetValue(SelectedMeasureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMeasureProperty =
            DependencyProperty.Register("SelectedMeasure", typeof(MeasureModelFAdpt), typeof(MDFMeasureChooseControl), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                if (e.NewValue != null)
                {
                    var control = sender as MDFMeasureChooseControl;
                    if (control == null)
                        return;
                    if(control.pupup!=null)
                        control.pupup.IsOpen = false;
                    control.SelectedMeasureId = (e.NewValue as MeasureModelFAdpt).Id.ToString();
                    control.SelectedMeasureName = (e.NewValue as MeasureModelFAdpt).Name;
                    if(control.lblText!=null)
                        control.lblText.Text = (e.NewValue as MeasureModelFAdpt).Name;
                    //(sender as MDFMeasureChooseControl).pupup.IsOpen = false;
                    //(sender as MDFMeasureChooseControl).SelectedMeasureId = (e.NewValue as MeasureModelFAdpt).Id.ToString();
                    //(sender as MDFMeasureChooseControl).SelectedMeasureName = (e.NewValue as MeasureModelFAdpt).Name;
                    //(sender as MDFMeasureChooseControl).lblText.Text = (e.NewValue as MeasureModelFAdpt).Name;
                }
            })));

        public string SelectedMeasureId
        {
            get { return (string)GetValue(SelectedMeasureIdProperty); }
            set { SetValue(SelectedMeasureIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMeasureIdProperty =
            DependencyProperty.Register("SelectedMeasureId", typeof(string), typeof(MDFMeasureChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if ((sender as MDFMeasureChooseControl).ItemSource != null && e.NewValue!=null)
                    {
                       
                        var selectObj = (sender as MDFMeasureChooseControl).ItemSource.FirstOrDefault(c => c.Id.ToString() == e.NewValue.ToString());

                        (sender as MDFMeasureChooseControl).SelectedMeasure = selectObj;
                    }

                })));


        public string SelectedMeasureName
        {
            get { return (string)GetValue(SelectedMeasureNameProperty); }
            set { SetValue(SelectedMeasureNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaterialName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMeasureNameProperty =
            DependencyProperty.Register("SelectedMeasureName", typeof(string), typeof(MDFMeasureChooseControl), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if ((sender as MDFMeasureChooseControl).ItemSource != null && e.NewValue!=null)
                    {
                        var selectObj = (sender as MDFMeasureChooseControl).ItemSource.FirstOrDefault(c => c.Name == e.NewValue.ToString());
                        (sender as MDFMeasureChooseControl).SelectedMeasure = selectObj;
                    }
                    else
                    {
                        if ((sender as MDFMeasureChooseControl).lblText!=null)
                        {
                            if (e.NewValue != null )
                            {
                                (sender as MDFMeasureChooseControl).lblText.Text = e.NewValue.ToString();
                            }
                            else
                            {
                                (sender as MDFMeasureChooseControl).lblText.Text = "请选择";
                            }
                            
                        }
                    }
                })));


        #endregion

        #region 调用服务

        public async void GetTypes(List<string> QueryTypeIds)
        {
            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var result = await wcf.Invoke<IMeasureService>(c => c.GetTypes(QueryTypeIds));

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MeasureTypeModelFAdpt>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                MeasureTypes = list;
            }
        }

        public async void GetMeasures(List<string> QueryTypeIds)
        {
            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var resultMaterial = await wcf.Invoke<IMeasureService>(c => c.GetMeasures(QueryTypeIds));

            if (resultMaterial.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MeasureModelFAdpt>>(resultMaterial.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                ItemSource = list;
                PersonsTemp = list;
            }
        }

        #endregion
    }

    public class QueryMeasureType:DependencyObject
    {
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(QueryMeasureType), new PropertyMetadata(""));

    }
}
