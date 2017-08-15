using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMES.AspNetDTM.UI.Startup))]
namespace SMES.AspNetDTM.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
