using System;
using System.Collections.Generic;
using Domain.Interfaces.Repositories;
using Domain.Models.Things;
using Data.IoT.Context;
using System.Data.Common;

namespace Data.IoT.Repositories
{
    public class PublicIlluminationControllerRepository : IThingRepository<PublicIlluminationController>
    {
        private IoTDataContext dbContext;

        public PublicIlluminationControllerRepository(string tenantSchema, DbConnection connection)
        {
            dbContext = IoTDataContext.Create(tenantSchema, connection, "PublicIllumination");
        }

        public static PublicIlluminationControllerRepository Create(string tenantSchema, DbConnection connection)
        {
            IoTDataContext.ProvisionTenant(tenantSchema, connection, "PublicIllumination");
            return new PublicIlluminationControllerRepository(tenantSchema, connection);
        }

        public static PublicIlluminationControllerRepository Get(string tenantSchema, DbConnection connection)
        {
            return new PublicIlluminationControllerRepository(tenantSchema, connection);
        }

        public void Add(PublicIlluminationController obj)
        {
            dbContext.Set<PublicIlluminationController>().Add(obj);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public PublicIlluminationController Find(int id)
        {
            return dbContext.Set<PublicIlluminationController>().Find(id);
        }

        public IEnumerable<PublicIlluminationController> GetAll()
        {
            return dbContext.Set<PublicIlluminationController>();
        }

        public void Remove(PublicIlluminationController obj)
        {
            dbContext.Set<PublicIlluminationController>().Remove(obj);
            dbContext.SaveChanges();
        }

        public void Update(PublicIlluminationController obj)
        {
            dbContext.Entry<PublicIlluminationController>(obj).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
