using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Things;

namespace Domain.Services
{
    public class PublicIlluminationControllerService : ServiceBase<PublicIlluminationController>, IPublicIlluminationControllerService
    {
        /// <summary>The service used at this app class.</summary>
        private readonly IThingRepository<PublicIlluminationController> _repository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service"></param>
        public PublicIlluminationControllerService(IThingRepository<PublicIlluminationController> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
