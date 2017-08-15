using System.Web.Mvc;

namespace SMES.AspNet.DTMUI.Areas.DT
{
    public class DTAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DT_default",
                "DT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SMES.AspNetDTM.UI.Areas.DT.Controllers" }
            );
        }
    }
}