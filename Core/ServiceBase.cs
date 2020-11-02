using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Core.Interfaces;
using BlogApi.Core.Interfaces.Database;

namespace BlogApi.Core
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IEntity
    {
        protected readonly IRepositoryAsync<TEntity> repository;

        public ServiceBase(IRepositoryAsync<TEntity> repository)
        {
            this.repository = repository;
        }
        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            return await repository.AddAsync(obj);
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return await repository.AddRangeAsync(entities);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await repository.GetByIdAsync(id);
        }

        public virtual async Task<bool> RemoveAsync(object id)
        {
            return await repository.RemoveAsync(id);
        }

        public virtual async Task<bool> RemoveAsync(TEntity obj)
        {
            return await repository.RemoveAsync(obj);
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            return await repository.RemoveRangeAsync(entities);
        }

        public virtual async Task<TEntity> UpdateAsync(object id, TEntity obj)
        {
            return await repository.UpdateAsync(id, obj);
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return await repository.UpdateRangeAsync(entities);
        }
    }
}