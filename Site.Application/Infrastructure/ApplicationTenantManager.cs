using Common.Infrastructure.Cryptography;
using Data.Tenants.Repositories;
using Domain.Models.Tenants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Site.Application.Infrastructure
{
    public class ApplicationTenantManager : UserManager<Tenant>
    {
        public ApplicationTenantManager(IUserStore<Tenant> store) : base(store) { }

        public static ApplicationTenantManager Create(IdentityFactoryOptions<ApplicationTenantManager> options, IOwinContext context)
        {
            var userManager = new ApplicationTenantManager(new UserStore<Tenant>(TenantRepository.Create()));
            userManager.PasswordHasher = new SimplePasswordHasher();
            return userManager;
        }
    }
}
