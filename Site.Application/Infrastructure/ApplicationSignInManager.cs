using Domain.Models.Tenants;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Site.Application.Infrastructure
{
    public class ApplicationSignInManager : SignInManager<Tenant, string>
    {
        public ApplicationSignInManager(ApplicationTenantManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) { }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> option, IOwinContext context)
        {
            var manager = context.GetUserManager<ApplicationTenantManager>();
            var sign = new ApplicationSignInManager(manager, context.Authentication);
            return sign;
        }
    }
}
