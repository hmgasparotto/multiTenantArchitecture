using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Models.Tenants
{
    public class Tenant : IdentityUser
    {
        public string HostName { get; set; }
        public string Type { get; set; }
    }
}
