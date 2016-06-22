using System.Collections.Generic;

namespace Domain.Interfaces.Services
{
    /// <summary>
    /// Interface to access the services.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IServiceBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(int id);
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Dispose();
    }
}

