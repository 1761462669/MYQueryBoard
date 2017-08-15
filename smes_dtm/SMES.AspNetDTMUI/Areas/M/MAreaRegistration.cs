using System.Web.Mvc;

namespace SMES.AspNet.DTMUI.Areas.M
{
    public class MAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "M";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "M_default",
                "M/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SMES.AspNetDTM.UI.Areas.M.Controllers" }
            );
        }
    }
}