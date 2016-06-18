using Common.Infrastructure.Cryptography;
using Data.Tenants.Repositories;
using Domain.Models.Tenants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Site.Subscription.Infrastructure
{
    public class SubscriptionTenantManager : UserManager<Tenant>
    {
        public SubscriptionTenantManager(IUserStore<Tenant> store) : base(store) { }

        public static SubscriptionTenantManager Create(IdentityFactoryOptions<SubscriptionTenantManager> options, IOwinContext context)
        {
            var userManager = new SubscriptionTenantManager(new UserStore<Tenant>(TenantRepository.Create()));

            userManager.PasswordHasher = new SimplePasswordHasher();

            return userManager;
        }
    }
}
