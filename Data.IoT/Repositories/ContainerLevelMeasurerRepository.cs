using System;
using System.Collections.Generic;
using Domain.Interfaces.Repositories;
using Domain.Models.Things;
using Data.IoT.Context;
using System.Data.Common;

namespace Data.IoT.Repositories
{
    public class ContainerLevelMeasurerRepository : IThingRepository<ContainerLevelMeasurer>
    {
        private IoTDataContext dbContext;

        public ContainerLevelMeasurerRepository(string tenantSchema, DbConnection connection)
        {
            dbContext = IoTDataContext.Create(tenantSchema, connection, "ContainerLevel");
        }

        public static ContainerLevelMeasurerRepository Create(string tenantSchema, DbConnection connection)
        {
            IoTDataContext.ProvisionTenant(tenantSchema, connection, "ContainerLevel");
            return new ContainerLevelMeasurerRepository(tenantSchema, connection);
        }

        public static ContainerLevelMeasurerRepository Get(string tenantSchema, DbConnection connection)
        {
            return new ContainerLevelMeasurerRepository(tenantSchema, connection);
        }

        public void Add(ContainerLevelMeasurer obj)
        {
            dbContext.Set<ContainerLevelMeasurer>().Add(obj);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ContainerLevelMeasurer Find(int id)
        {
            return dbContext.Set<ContainerLevelMeasurer>().Find(id);
        }

        public IEnumerable<ContainerLevelMeasurer> GetAll()
        {
            return dbContext.Set<ContainerLevelMeasurer>();
        }

        public void Remove(ContainerLevelMeasurer obj)
        {
            dbContext.Set<ContainerLevelMeasurer>().Remove(obj);
            dbContext.SaveChanges();
        }

        public void Update(ContainerLevelMeasurer obj)
        {
            dbContext.Entry<ContainerLevelMeasurer>(obj).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
