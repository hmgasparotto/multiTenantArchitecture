using Data.Tenants.Context;
using Domain.Interfaces.Repositories;
using Domain.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Tenants.Repositories
{
    public class TenantRepository : IDisposable, ITenantRepository
    {
        private TenantDbContext context = Create();

        public static TenantDbContext Create()
        {
            return TenantDbContext.Create();
        }

        public void Add(Tenant obj)
        {
            context.Set<Tenant>().Add(obj);
            context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Tenant Find(int id)
        {
            return context.Set<Tenant>().Find(id);
        }

        public Tenant FindByCredentials(string username, string password)
        {
            return context.Set<Tenant>().Where(t => t.UserName == username && t.PasswordHash == password).FirstOrDefault();
        }

        public IEnumerable<Tenant> GetAll()
        {
            return context.Set<Tenant>().ToList();
        }

        public void Remove(Tenant obj)
        {
            context.Set<Tenant>().Remove(obj);
            context.SaveChanges();
        }

        public void Update(Tenant obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
