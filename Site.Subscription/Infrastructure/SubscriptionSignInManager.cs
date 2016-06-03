using Domain.Models.Tenants;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Subscription.Infrastructure
{
    public class SubscriptionSignInManager : SignInManager<Tenant, string>
    {
        public SubscriptionSignInManager(SubscriptionTenantManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) { }

        public static SubscriptionSignInManager Create(IdentityFactoryOptions<SubscriptionSignInManager> option, IOwinContext context)
        {
            var manager = context.GetUserManager<SubscriptionTenantManager>();
            var sign = new SubscriptionSignInManager(manager, context.Authentication);
            return sign;
        }
    }
}
