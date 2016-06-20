using Application.Services.Interfaces;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ThingAppService<TEntity> : AppServiceBase<TEntity>, IThingAppService<TEntity> where TEntity : class
    {
        /// <summary>The service used at this app class.</summary>
        private readonly IThingService<TEntity> _service;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service"></param>
        public ThingAppService(IThingService<TEntity> service)
            : base(service)
        {
            _service = service;
        }
    }
}
