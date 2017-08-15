using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SMES.AspNetDTM.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new SMES.AspNetFramework.MVC.ValueProviderFactory());

            ModelBinders.Binders.DefaultBinder = new SMES.AspNetFramework.MVC.ModelBinder();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Request.FilePath == "/") Context.RewritePath("index.html");
        }
    }
}
