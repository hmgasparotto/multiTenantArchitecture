using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Things;

namespace Domain.Services
{
    public class ContainerLevelMeasurerService : ServiceBase<ContainerLevelMeasurer>, IContainerLevelMeasurerService
    {
        /// <summary>The service used at this app class.</summary>
        private readonly IThingRepository<ContainerLevelMeasurer> _repository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service"></param>
        public ContainerLevelMeasurerService(IThingRepository<ContainerLevelMeasurer> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
