using Application.Services.Interfaces;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    /// <summary>
    /// App base class implementation to access servers logic.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        /// <summary>The service base used at this app class.</summary>
        protected readonly IServiceBase<TEntity> _serviceBase;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceBase"></param>
        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        /// <summary>
        /// Get all entities from the service.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        /// <summary>
        /// Find entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(int id)
        {
            return _serviceBase.Find(id);
        }

        /// <summary>
        /// Add new entity.
        /// </summary>
        /// <param name="obj"></param>
        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }

        /// <summary>
        /// Remove entity.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        /// <summary>
        /// Dispose this server.
        /// </summary>
        public void Dispose()
        {
            _serviceBase.Dispose();
        }
    }
}
