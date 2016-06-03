using Data.Tenants.Context;
using Domain.Models.Tenants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Subscription.Infrastructure
{
    public class SubscriptionTenantManager : UserManager<Tenant>
    {
        public SubscriptionTenantManager(IUserStore<Tenant> store) : base(store) { }

        public static SubscriptionTenantManager Create(IdentityFactoryOptions<SubscriptionTenantManager> options, IOwinContext context)
        {
            var appcontext = context.Get<TenantDbContext>();
            var userManager = new SubscriptionTenantManager(new UserStore<Tenant>(appcontext));
            return userManager;
        }
    }
}
