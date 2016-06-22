using Data.Tenants.Repositories;
using Microsoft.Owin;
using Owin;
using Site.Subscription.Infrastructure;

[assembly: OwinStartupAttribute(typeof(Site.Subscription.Startup))]
namespace Site.Subscription
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(TenantRepository.Create);
            app.CreatePerOwinContext<SubscriptionTenantManager>(SubscriptionTenantManager.Create);
        }
    }
}