using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ThingService<TEntity> : ServiceBase<TEntity>, IThingService<TEntity> where TEntity : class
    {
        /// <summary>Repository use in this base class.</summary>
        private readonly IThingRepository<TEntity> _repository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="clienteRepository"></param>
        public ThingService(IThingRepository<TEntity> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}
