using MDF.Framework.Bus;
using MDF.Framework.Commands;
using MDF.Framework.ViewModel;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SMES.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MDF.UISample.Tools
{
    public class PortalNavagationCommand : BaseViewModel, ICommand, IRaiseCanExecute
    {
        public static object GloablePara { get; private set; }

        public bool IsSetParaDataContext
        {
            get { return (bool)GetValue(IsSetParaDataContextProperty); }
            set { SetValue(IsSetParaDataContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSetParaDataContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSetParaDataContextProperty =
            DependencyProperty.Register("IsSetParaDataContext", typeof(bool), typeof(PortalNavagationCommand), new PropertyMetadata(false));


        public object Para
        {
            get { return (object)GetValue(ParaProperty); }
            set { SetValue(ParaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Para.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParaProperty =
            DependencyProperty.Register("Para", typeof(object), typeof(PortalNavagationCommand), new PropertyMetadata(null));




        public string RegionName
        {
            get { return (string)GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.Register("RegionName", typeof(string), typeof(PortalNavagationCommand), new PropertyMetadata(null));

        public string MenuName
        {
            get { return (string)GetValue(MenuNameProperty); }
            set { SetValue(MenuNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuNameProperty =
            DependencyProperty.Register("MenuName", typeof(string), typeof(PortalNavagationCommand), new PropertyMetadata(""));


        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Uri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register("Uri", typeof(string), typeof(PortalNavagationCommand), new PropertyMetadata(null));



        public bool IsGloablePara
        {
            get { return (bool)GetValue(IsGloableParaProperty); }
            set { SetValue(IsGloableParaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsGloablePara.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsGloableParaProperty =
            DependencyProperty.Register("IsGloablePara", typeof(bool), typeof(PortalNavagationCommand), new PropertyMetadata(false));


        public Dictionary<string, bool> CommandPrivales { get; set; }


        public bool CanExecute(object parameter)
        {
            return this.RegionName != null && this.Uri != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            GloablePara = null;
            var ctl = ServiceLocator.Current.GetInstance<Control>(this.Uri);

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            if (!regionManager.Regions.ContainsRegionWithName(this.RegionName))
            {
                MessageBox.Show(string.Format("未找到区域【{0}】！", this.RegionName));

            }
            var views = regionManager.Regions[this.RegionName].Views;
            object view = null;

            foreach (var v in views)
            {
                Type t = v.GetType();
                if (t.FullName == this.Uri)
                {
                    view = v;
                    break;
                }
            }

            if (view == null)
            {
                try
                {
                    regionManager.RegisterViewWithRegion(this.RegionName, () => ctl);
                }
                catch (ViewRegistrationException)
                {

                }
            }

            //命令测试数据Begin
            if(CommandPrivales == null || CommandPrivales.Count == 0)
            {
                CommandPrivales = new Dictionary<string, bool>();
                CommandPrivales.Add("btnEdit", true);
                CommandPrivales.Add("btnDelete", true);
            }

            ButtonPrivileAction buttonAction = new ButtonPrivileAction(ctl, CommandPrivales);

            UriQuery query = new UriQuery();
            var adress = new Uri(this.Uri, UriKind.RelativeOrAbsolute);
            if (this.Para != null)
            {
                if (!this.IsGloablePara)
                {
                    query.Add("para", InfoExchange.ConvertToJson(this.Para, InfoExchange.SetingsKonwnTypesBinder));
                    adress = new Uri(this.Uri + query.ToString(), UriKind.Relative);
                }
                else
                    GloablePara = Para;
                if (this.IsSetParaDataContext)
                {
                    ctl.DataContext = Para;
                }
            }

            if (!string.IsNullOrEmpty(this.MenuName))
            {
                query.Add("MenuName", InfoExchange.ConvertToJson(this.MenuName, InfoExchange.SetingsKonwnTypesBinder));
            }

            regionManager.RequestNavigate(this.RegionName, new Uri(this.Uri + query.ToString(), UriKind.Relative));
        }

        public void RaisedCanExecute()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, new EventArgs());
        }
    }
}
