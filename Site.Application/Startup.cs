using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Site.Application.Startup))]
namespace Site.Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
