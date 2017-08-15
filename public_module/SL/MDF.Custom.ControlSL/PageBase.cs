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

using MDF.Custom.ControlSL.Controls;
using Microsoft.Practices.Prism.Regions;
using System.Collections.Generic;

namespace MDF.Custom.ControlSL
{
    public class PageBase : ContentControl, INavigationAware
    {
        #region 构造函数

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PageBase), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    var length = e.NewValue.ToString().Length;

                    if(length >= 8 )
                    {
                        (sender as PageBase).HeaderWidthAuto = 200;
                    }
                    else
                    {
                        (sender as PageBase).HeaderWidthAuto = 70 + length * 10;
                    }

                })));



        //public string MyProperty
        //{
        //    get { return (string)GetValue(MyPropertyProperty); }
        //    set { SetValue(MyPropertyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyPropertyProperty =
        //    DependencyProperty.Register("MyProperty", typeof(string), typeof(ownerclass), new PropertyMetadata(0));

        


        public double HeaderWidth
        {
            get { return (double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth", typeof(double), typeof(PageBase), new PropertyMetadata(0d));


        public double HeaderWidthAuto
        {
            get { return (double)GetValue(HeaderWidthAutoProperty); }
            set { SetValue(HeaderWidthAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderWidthAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderWidthAutoProperty =
            DependencyProperty.Register("HeaderWidthAuto", typeof(double), typeof(PageBase), new PropertyMetadata(0d));


        
        



        public object HeaderRightContent
        {
            get { return (object)GetValue(HeaderRightContentProperty); }
            set { SetValue(HeaderRightContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderRightContentProperty =
            DependencyProperty.Register("HeaderRightContent", typeof(object), typeof(PageBase), new PropertyMetadata(null));

        

        public PageBase()
        {
            this.DefaultStyleKey = typeof(PageBase);
        }

        #endregion

        public event RoutedEventHandler NavigatedComplatedEvent;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public string Para
        {
            get { return (string)GetValue(ParaProperty); }
            set { SetValue(ParaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Para.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParaProperty =
            DependencyProperty.Register("Para", typeof(string), typeof(PageBase), new PropertyMetadata(""));


        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            //获取参数的Json串
            string jsonPara = navigationContext.Parameters["para"];
            string menuName = navigationContext.Parameters["MenuName"];
            string s_commandPrivales = navigationContext.Parameters["Privales"];
            
            if(jsonPara != null)
            {
                //得到参数
                string para = MDF.Framework.Bus.InfoExchange.DeConvert<string>(jsonPara);
                Para = para;

                if (NavigatedComplatedEvent != null)
                {
                    NavigatedComplatedEvent(this, new RoutedEventArgs());
                }
            }

            if(!string.IsNullOrEmpty(menuName))
            {
                this.Title = MDF.Framework.Bus.InfoExchange.DeConvert<string>(menuName);
            }

            //解析命令权限
            if (!string.IsNullOrEmpty(s_commandPrivales))
            {
                CommandPrivales = MDF.Framework.Bus.InfoExchange.DeConvert<Dictionary<string, bool>>(s_commandPrivales);
            }
        }

        //public Dictionary<string, bool> CommandPrivales { get; set; }



        public Dictionary<string, bool> CommandPrivales
        {
            get { return (Dictionary<string, bool>)GetValue(CommandPrivalesProperty); }
            set { SetValue(CommandPrivalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandPrivales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandPrivalesProperty =
            DependencyProperty.Register("CommandPrivales", typeof(Dictionary<string, bool>), typeof(PageBase), new PropertyMetadata(null));


    }
}
