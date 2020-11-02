using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Core.Interfaces.Database;

namespace BlogApi.Core.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : IEntity
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