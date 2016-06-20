using System;
using System.Collections.Generic;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    /// <summary>
    /// Base service class to access core data repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        /// <summary>Repository use in this base class.</summary>
        private readonly IRepositoryBase<TEntity> _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all entites from repository.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Find entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(int id)
        {
            return _repository.Find(id);
        }

        /// <summary>
        /// Add new entity.
        /// </summary>
        /// <param name="obj"></param>
        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        /// <summary>
        /// Remove entity.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        /// <summary>
        /// Dispose this service.
        /// </summary>
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
