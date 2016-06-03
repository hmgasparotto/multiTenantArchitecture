using Domain.Models.Tenants;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Tenants.Context
{
    public class TenantDbContext : IdentityDbContext<Tenant>
    {
        public TenantDbContext() : base ("TenantDatabase")
        {

        }

        public static TenantDbContext Create()
        {
            return new TenantDbContext();
        }
    }
}
