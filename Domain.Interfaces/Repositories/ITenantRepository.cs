using Domain.Models.Tenants;

namespace Domain.Interfaces.Repositories
{
    public interface ITenantRepository : IRepositoryBase<Tenant>
    {
        Tenant FindByCredentials(string username, string password);
    }
}
