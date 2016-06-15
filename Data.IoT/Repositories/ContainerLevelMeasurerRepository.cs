using System;
using System.Collections.Generic;
using Domain.Interfaces.Repositories;
using Domain.Models.Things;
using Data.IoT.Context;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Concurrent;
using Data.IoT.Mapping;

namespace Data.IoT.Repositories
{
    public class ContainerLevelMeasurerRepository : IThingRepository<ContainerLevelMeasurer>
    {
        private IoTDataContext dbContext;

        public ContainerLevelMeasurerRepository(string tenantSchema, DbConnection connection)
        {
            IoTDataContext.ProvisionTenant(tenantSchema, connection, "ContainerLevel");
            dbContext = IoTDataContext.Create(tenantSchema, connection, "ContainerLevel");
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
