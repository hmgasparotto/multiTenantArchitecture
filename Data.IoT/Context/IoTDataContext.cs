using Data.IoT.Mapping;
using Domain.Models.Things;
using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Data.IoT.Context
{
    public class IoTDataContext : DbContext
    {
        private static ConcurrentDictionary<Tuple<string, string>, DbCompiledModel> modelCache = new ConcurrentDictionary<Tuple<string, string>, DbCompiledModel>();

        private IoTDataContext(DbConnection connection, DbCompiledModel model) : base(connection, model, contextOwnsConnection: false)
        {

        }

        public DbSet<Thing> Things { get; set; }

        public static IoTDataContext Create(string tenantSchema, DbConnection connection, string thingType)
        {
            Database.SetInitializer<IoTDataContext>(null);
            DbCompiledModel compiledModel = modelCache.GetOrAdd(
                Tuple.Create(connection.ConnectionString, tenantSchema), t => {
                    DbModelBuilder builder = new DbModelBuilder();
                    builder.Conventions.Remove();

                    if (thingType.Contains("ContainerLevel"))
                        builder.Configurations.Add<ContainerLevelMeasurer>(new ContainerLevelMeasurerConfiguration(tenantSchema));
                    else if (thingType.Contains("PublicIllumination"))
                        builder.Configurations.Add<PublicIlluminationController>(new PublicIlluminationControllerConfiguration(tenantSchema));

                    var model = builder.Build(connection);
                    return model.Compile();
                }
            );

            return new IoTDataContext(connection, compiledModel);
        }

        public static void ProvisionTenant(string tenantSchema, DbConnection connection, string thingType)
        {
            using (var ctx = Create(tenantSchema, connection, thingType))
            {
                if (!ctx.Database.Exists())
                {
                    ctx.Database.Create();
                }
                else
                {
                    var createScript = ((IObjectContextAdapter)ctx).ObjectContext.CreateDatabaseScript();
                    ctx.Database.ExecuteSqlCommand(createScript);
                }
            }
        }
    }
}
