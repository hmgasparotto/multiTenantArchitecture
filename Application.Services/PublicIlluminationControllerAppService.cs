using Application.Services.Interfaces;
using Data.IoT.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Things;
using Domain.Services;
using System.Data.Common;

namespace Application.Services
{
    public class PublicIlluminationControllerAppService : ThingAppService<PublicIlluminationController>, IPublicIlluminationControllerAppService
    {
        /// <summary>The service used at this app class.</summary>
        private readonly IPublicIlluminationControllerService _service;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service"></param>
        public PublicIlluminationControllerAppService(IPublicIlluminationControllerService service)
            : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Factory to create a new application.
        /// </summary>
        /// <returns></returns>
        public static IPublicIlluminationControllerAppService Factory(string tenantSchema, DbConnection connection, bool isCreation)
        {
            PublicIlluminationControllerRepository repository;
            if (isCreation)
                repository = PublicIlluminationControllerRepository.Create(tenantSchema, connection);
            else
                repository = PublicIlluminationControllerRepository.Get(tenantSchema, connection);
            var service = new PublicIlluminationControllerService(repository);
            return new PublicIlluminationControllerAppService(service);
        }
    }
}
