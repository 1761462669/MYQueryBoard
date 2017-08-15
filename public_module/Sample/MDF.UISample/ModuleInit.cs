using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MDF.UISample
{
    public class ModuleInit : IModule
    {
        private readonly IUnityContainer container;
        //private readonly IRegionManager regionManager;

        public ModuleInit(IUnityContainer container)
        {
            this.container = container;
        }


        public void Initialize()
        {
            MDF.Framework.Bus.InfoExchange.KnownTypesBinder.RegistAssembly(this.GetType().Assembly);
            //var rel = new ResourceDictionary() { Source = new Uri("/SMES.Com.ScheduleCoreSL;component/Theme/System.Windows.Controls.Theming.BureauBlack.xaml",UriKind.RelativeOrAbsolute) };
            //Application.Current.Resources.MergedDictionaries.Add(rel);
            var types = this.GetType().Assembly.GetTypes().Where(c => c.IsSubclassOf(typeof(Control))).ToList();
            foreach (var item in types)
            {
                this.container.RegisterType(typeof(Control), item, item.FullName);
            }
        }
    }
}
