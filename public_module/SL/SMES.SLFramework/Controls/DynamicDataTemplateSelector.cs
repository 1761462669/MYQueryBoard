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
using System.Linq;

namespace SMES.Framework.Controls
{
    [TemplateVisualState(GroupName = "DataState", Name = "Edit")]
    [TemplateVisualState(GroupName = "DataState", Name = "View")]
    public class DynamicDataTemplateSelector : ContentControl
    {
        public int Key
        {
            get { return (int)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(int), typeof(DynamicDataTemplateSelector), new PropertyMetadata(0, OnPropertyChangedCallback));

        public KeyDataTemplateCollection Templates
        {
            get { return (KeyDataTemplateCollection)GetValue(TemplatesProperty); }
            set { SetValue(TemplatesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Templates.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplatesProperty = DependencyProperty.Register("Templates", typeof(KeyDataTemplateCollection), typeof(DynamicDataTemplateSelector), new PropertyMetadata(null, new PropertyChangedCallback(OnPropertyChangedCallback)));


        #region Static Methods
        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as DynamicDataTemplateSelector;
            ctl.IntialData();
        }
        #endregion


        public DynamicDataTemplateSelector()
        {
            Templates = new KeyDataTemplateCollection();

            //this.DataContextChanged += (send, e) =>
            //    {
            //        var dc = e.NewValue;

            //        if (dc is IModelWrapper)
            //        {
            //            this.Content = dc;
            //        }
            //        else
            //        {
            //            var model = e.NewValue as RootModel;

            //            var wraper = new ModelWrapper() { Model = dc, Key = 1 };

            //            if (model == null) return;

            //            if (model.Wrapper != null)
            //            {
            //                wraper.Key = (model.Wrapper as ModelWrapper).Key;
            //            }

            //            if (model != null)
            //                model.Wrapper = wraper;

            //            this.DataContext = wraper;
            //            this.Content = model;
            //        }

            //        this.IntialData();

            //    };
        }

        public void IntialData()
        {
            var dt = this.Templates.Where(c => c.Key == this.Key).FirstOrDefault();
            if (dt != null && dt.DataTemplate != null)
            {
                this.ContentTemplate = dt.DataTemplate;

                var dc = this.DataContext;
                if (dc is IModelWrapper)
                {
                    this.Content = (dc as IModelWrapper).Model;
                }
                else
                {
                    var model = dc as RootModel;

                    var wraper = new ModelWrapper() { Model = dc, Key = 1 };

                    if (model == null) return;

                    if (model.Wrapper != null)
                    {
                        wraper.Key = (model.Wrapper as ModelWrapper).Key;
                    }

                    if (model != null)
                        model.Wrapper = wraper;

                    this.DataContext = wraper;
                    this.Content = model;
                }

                if (Key == 1)
                {
                    VisualStateManager.GoToState(this, "View", true);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Edit", true);
                }
            }
        }
    }

    public class KeyDataTemplate:DependencyObject
    {
        public int Key
        {
            get { return (int)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(int), typeof(KeyDataTemplate), new PropertyMetadata(0));


        public DataTemplate DataTemplate { get; set; }
    }

    public class KeyDataTemplateCollection : System.Collections.ObjectModel.ObservableCollection<KeyDataTemplate>
    {

    }
}
