using MDF.Custom.ControlSL.Controls;
using MDF.Framework.Commands;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SMES.Framework.Utility;

namespace MDF.Custom.ControlSL.Command
{
    public class OpenChildWindowCommand : DependencyObject, ICommand, IRaiseCanExecute
    {

        public event EventHandler Closed;
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Uri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register("Uri", typeof(string), typeof(OpenChildWindowCommand), new PropertyMetadata(null));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(string), new PropertyMetadata(""));



        public FrameworkElement BaseElement
        {
            get { return (FrameworkElement)GetValue(BaseElementProperty); }
            set { SetValue(BaseElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BaseElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseElementProperty =
            DependencyProperty.Register("BaseElement", typeof(FrameworkElement), typeof(OpenChildWindowCommand), new PropertyMetadata(null));





        public object Para
        {
            get { return (object)GetValue(ParaProperty); }
            set { SetValue(ParaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Para.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParaProperty =
            DependencyProperty.Register("Para", typeof(object), typeof(object), new PropertyMetadata(null));


        public object ChildWindow
        {
            get { return (MDFPopupWindow)GetValue(ChildWindowProperty); }
            set { SetValue(ChildWindowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChildWindow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildWindowProperty =
            DependencyProperty.Register("ChildWindow", typeof(MDFPopupWindow), typeof(OpenChildWindowCommand), new PropertyMetadata(null, (sender, e) =>
            {
                var v = e.NewValue;
            }));


        public DataTemplate DataTemplate
        {
            get { return (DataTemplate)GetValue(DataTemplateProperty); }
            set { SetValue(DataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTemplateProperty =
            DependencyProperty.Register("DataTemplate", typeof(DataTemplate), typeof(OpenChildWindowCommand), new PropertyMetadata(null));




        public Thickness Margin
        {
            get { return (Thickness)GetValue(MarginProperty); }
            set { SetValue(MarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Margin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.Register("Margin", typeof(Thickness), typeof(OpenChildWindowCommand), new PropertyMetadata(new Thickness(0, 0, 0, 0)));


        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register("HorizontalAlignment", typeof(HorizontalAlignment), typeof(OpenChildWindowCommand), new PropertyMetadata(HorizontalAlignment.Center));

        //public VerticalAlignment VerticalAlignment


        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalAlignmentProperty); }
            set { SetValue(VerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register("VerticalAlignment", typeof(VerticalAlignment), typeof(OpenChildWindowCommand), new PropertyMetadata(VerticalAlignment.Center));




        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MDFPopupWindow cw = new MDFPopupWindow();
            var style = Application.Current.Resources["pupupStyle"] as Style;

            if (Para != null)
            {
                cw.DataContext = Para;
            }
            if(this.ChildWindow != null)
            {
                cw = ChildWindow as MDFPopupWindow;
                cw.Style = style;
            }
            else
            {
                if (this.DataTemplate != null)
                {
                    cw.Content = cw.DataContext;
                    cw.ContentTemplate = this.DataTemplate;
                }
                else
                {
                    if (this.Uri == null)
                    {
                        MessageBox.Show("未配置对话框对应的数据模板或对话框对应的Uri", "提示!", MessageBoxButton.OK);
                        return;
                    }
                    var ctl = ServiceLocator.Current.GetInstance<Control>(this.Uri);

                    if (ctl == null)
                    {
                        MessageBox.Show(string.Format("未找到Uri未【{0}】的控件，请先注册！", this.Uri));
                        return;
                    }
                    if (ctl is ChildWindow)
                    {
                        cw = ctl as MDFPopupWindow;
                        cw.Style = style;
                        cw.HorizontalAlignment = HorizontalAlignment;
                        cw.VerticalAlignment = VerticalAlignment;
                        cw.Margin = Margin;
                        cw.Para = this.Para;

                        var vm = cw.DataContext as Microsoft.Practices.Prism.Regions.INavigationAware;

                        cw.DataContext = Para;
                    }
                    else
                        cw.Content = ctl;

                }
            }
            

            if (this.BaseElement != null)
                cw.SetTransform(this.BaseElement);
            cw.Show();
            cw.Closed += cw_Closed;
        }

        private ChildWindow cw;
        public void Close()
        {
            if (cw != null)
                cw.Close();
        }
        void cw_Closed(object sender, EventArgs e)
        {
            if (this.Closed != null)
                this.Closed(this, e);
        }

        public void RaisedCanExecute()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, new EventArgs());
        }
    }
}
