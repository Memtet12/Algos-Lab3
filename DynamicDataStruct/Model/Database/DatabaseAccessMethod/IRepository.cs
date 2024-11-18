using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenRepApp
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void CreateAsync(TEntity item);
        Task<TEntity> FindByIdAsync(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> GetAsync(Func<TEntity, bool> predicate);
        void RemoveAsync(TEntity item);
        void UpdateAsync(TEntity item);
    }
}