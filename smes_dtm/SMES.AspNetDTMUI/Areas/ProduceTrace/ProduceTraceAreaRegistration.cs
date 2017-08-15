using System.Web.Mvc;

namespace SMES.AspNet.DTMUI.Areas.ProduceTrace
{
    public class ProduceTraceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProduceTrace";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProduceTrace_default",
                "ProduceTrace/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SMES.AspNetDTM.UI.Areas.ProduceTrace.Controllers" }
            );
        }
    }
}