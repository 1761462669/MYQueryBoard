using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMES.Framework.Controls
{
    public class NoWrapperDataTemplateSelector : ContentControl
    {
        #region DependencyProperty
        // Using a DependencyProperty as the backing store for ModelState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelStateProperty =
            DependencyProperty.Register("ModelState", typeof(ModelViewState), typeof(NoWrapperDataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for ViewTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewTemplateProperty =
            DependencyProperty.Register("ViewTemplate", typeof(DataTemplate), typeof(NoWrapperDataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for EditTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTemplateProperty =
            DependencyProperty.Register("EditTemplate", typeof(DataTemplate), typeof(NoWrapperDataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for DelteTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelteTemplateProperty =
            DependencyProperty.Register("DelteTemplate", typeof(DataTemplate), typeof(NoWrapperDataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        // Using a DependencyProperty as the backing store for DelteTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddTemplateProperty =
            DependencyProperty.Register("AddTemplate", typeof(DataTemplate), typeof(NoWrapperDataTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));

        #endregion

        #region feilds && Properties
        /// <summary>
        /// Model状态
        /// </summary>
        public ModelViewState ModelState
        {
            get { return (ModelViewState)GetValue(ModelStateProperty); }
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


        /// <summary>
        /// 添加状态
        /// </summary>
        public DataTemplate AddTemplate
        {
            get { return (DataTemplate)GetValue(AddTemplateProperty); }
            set { SetValue(AddTemplateProperty, value); }
        }


        #endregion

        #region Static Methods
        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as NoWrapperDataTemplateSelector;
            if (ctl.DataContext != null)
                ctl.InitialState();
        }
        #endregion


        internal void InitialState()
        {
            switch (this.ModelState)
            {
                case 0:
                case ModelViewState.View:
                    {
                        if (this.ViewTemplate != null)
                            this.ContentTemplate = this.ViewTemplate;
                        VisualStateManager.GoToState(this, "View", true);
                        break;
                    }

                case ModelViewState.Edit:
                    {
                        if (this.EditTemplate != null)
                            this.ContentTemplate = this.EditTemplate;
                        VisualStateManager.GoToState(this, "Edit", true);
                        break;
                    }
                case ModelViewState.Delete:
                    {
                        if (this.DelteTemplate != null)
                            this.ContentTemplate = this.DelteTemplate;
                        VisualStateManager.GoToState(this, "Edit", true);
                        break;
                    }
                case ModelViewState.Add:
                    {
                        if (this.AddTemplate != null)
                            this.ContentTemplate = this.AddTemplate;
                        VisualStateManager.GoToState(this, "Edit", true);
                        break;
                    }
                default:
                    break;
            }

        }
    }
}
