using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asp.net_MVC_Debuger.Startup))]
namespace Asp.net_MVC_Debuger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
