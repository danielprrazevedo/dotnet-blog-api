using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Core.Interfaces.Database
{
    public interface IRepositoryAsync<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> UpdateAsync(object id, TEntity obj);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> RemoveAsync(object id);
        Task<bool> RemoveAsync(TEntity obj);
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}