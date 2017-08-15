using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MDF.UISample
{
    public class Boot : UnityBootstrapper
    {


        protected override DependencyObject CreateShell()
        {
            var moduleInit = this.Container.TryResolve<ModuleInit>();
            moduleInit.Initialize();

            var ctl = this.Container.TryResolve<MainPage>();
            App.Current.RootVisual = ctl;
            return ctl;
        }

    }
}
