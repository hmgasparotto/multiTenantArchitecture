using Application.Services.Interfaces;
using Data.IoT.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Things;
using Domain.Services;
using System.Data.Common;

namespace Application.Services
{
    public class ContainerLevelMeasurerAppService : ThingAppService<ContainerLevelMeasurer>, IContainerLevelMeasurerAppService
    {
        /// <summary>The service used at this app class.</summary>
        private readonly IContainerLevelMeasurerService _service;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service"></param>
        public ContainerLevelMeasurerAppService(IContainerLevelMeasurerService service)
            : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Factory to create a new application.
        /// </summary>
        /// <returns></returns>
        public static IContainerLevelMeasurerAppService Factory(string tenantSchema, DbConnection connection, bool isCreation)
        {
            ContainerLevelMeasurerRepository repository;
            if (isCreation)
                repository = ContainerLevelMeasurerRepository.Create(tenantSchema, connection);
            else
                repository = ContainerLevelMeasurerRepository.Get(tenantSchema, connection);
            var service = new ContainerLevelMeasurerService(repository);
            return new ContainerLevelMeasurerAppService(service);
        }
    }
}
