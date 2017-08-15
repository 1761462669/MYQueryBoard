using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMES.Framework.Controls
{
    /// <summary>
    /// 模板选择器
    /// added by changhl,2015.4.8
    /// </summary>
    [TemplateVisualState(GroupName = "DataState",Name="Edit")]
    [TemplateVisualState(GroupName = "DataState", Name = "View")]
    public class DataTemplateSelector : ContentControl
    {
        #region DependencyProperty
        // Using a DependencyProperty as the backing store for ModelState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelStateProperty =
            DependencyProperty.Register("ModelState", typeof(ModelState), typeof(DataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for ViewTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewTemplateProperty =
            DependencyProperty.Register("ViewTemplate", typeof(DataTemplate), typeof(DataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for EditTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTemplateProperty =
            DependencyProperty.Register("EditTemplate", typeof(DataTemplate), typeof(DataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for DelteTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelteTemplateProperty =
            DependencyProperty.Register("DelteTemplate", typeof(DataTemplate), typeof(DataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for DelteTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddTemplateProperty =
            DependencyProperty.Register("AddTemplate", typeof(DataTemplate), typeof(DataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        #endregion

        #region Static Methods
        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as DataTemplateSelector;
            ctl.InitialState();
        }
        #endregion

        #region feilds && Properties
        /// <summary>
        /// Model状态
        /// </summary>
        public ModelState ModelState
        {
            get { return (ModelState)GetValue(ModelStateProperty); }
            set { SetValue(ModelStateProperty, value); }
        }

        /// <summary>
        /// 查看状态
        /// </summary>
        public DataTemplate ViewTemplate
        {
            get { return (DataTemplate)GetValue(ViewTemplateProperty); }
            set { SetValue(ViewTemplateProperty, value); }
        }



        /// <summary>
        /// 编辑状态
        /// </summary>
        public DataTemplate EditTemplate
        {
            get { return (DataTemplate)GetValue(EditTemplateProperty); }
            set { SetValue(EditTemplateProperty, value); }
        }


        /// <summary>
        /// 删除状态
        /// </summary>
        public DataTemplate DelteTemplate
        {
            get { return (DataTemplate)GetValue(DelteTemplateProperty); }
            set { SetValue(DelteTemplateProperty, value); }
        }




        public bool ModelIsWrap
        {
            get { return (bool)GetValue(IsWrapProperty); }
            set { SetValue(IsWrapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsWrap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsWrapProperty =
            DependencyProperty.Register("IsWrap", typeof(bool), typeof(DataTemplateSelector), new PropertyMetadata(false));



        /// <summary>
        /// 添加状态
        /// </summary>
        public DataTemplate AddTemplate
        {
            get { return (DataTemplate)GetValue(AddTemplateProperty); }
            set { SetValue(AddTemplateProperty, value); }
        }


        #endregion

        #region Constructs
        public DataTemplateSelector()
        {
            this.DataContextChanged += DataTemplateSelector_DataContextChanged;

        }

        #endregion


        #region Methods
        /// <summary>
        /// 当上下文改变时，改变Content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataTemplateSelector_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            //var dc = e.NewValue;
           
            //if (dc is IModelWrapper)
            //{
            //    if (!ModelIsWrap)
            //    this.Content = ((IModelWrapper)dc).Model;
            //else
            //        this.Content = dc;
            //}
            //else
            //{
                
            //    var model = e.NewValue as RootModel;
            //    if (model == null)
            //        return;

            //    var wraper = new ModelWrapper() { Model = dc, State = ModelState.View };

            //    if (model.Wrapper != null)
            //    {
            //        wraper.State = (model.Wrapper as ModelWrapper).State;
            //    }

            //    if (model != null)
            //        model.Wrapper = wraper;

            //    this.DataContext = wraper;

            //}
            //this.InitialState();
        }

        internal void InitialState()
        {
            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
            switch (this.ModelState)
            {
                case 0:
                case ModelState.View:
                    {
                        if (this.ViewTemplate != null)
                            this.ContentTemplate = this.ViewTemplate;
                        VisualStateManager.GoToState(this, "View", true);
                        break;
                    }

                case ModelState.Edit:
                    {
                        if (this.EditTemplate != null)
                            this.ContentTemplate = this.EditTemplate;
                        VisualStateManager.GoToState(this, "Edit", true);
                        break;
                    }
                case ModelState.Delete:
                    {
                        if (this.DelteTemplate != null)
                            this.ContentTemplate = this.DelteTemplate;
                        VisualStateManager.GoToState(this, "Edit", true);
                        break;
                    }
                case ModelState.Add:
                    {
                        if (this.AddTemplate != null)
                            this.ContentTemplate = this.AddTemplate;
                        VisualStateManager.GoToState(this, "Edit", true);
                        break;
                    }
                default:
                    break;
            }
            //var a = this.Content as MDF.Framework.Model.IRaisePropertyChanged;
            //if (a != null)
            //    a.RaisedPropertiesChanged();

        }

        #endregion

    }
}
