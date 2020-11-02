
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogApi.Core.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Core.Database
{
    public abstract class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity>
        where TEntity : class, IEntity
    {

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> dbSet;

        protected RepositoryAsync(DbContext dbContext)
        {
            this._context = dbContext;
            this.dbSet = this._context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            var r = await dbSet.AddAsync(obj);
            await CommitAsync();
            return r.Entity;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return await CommitAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(dbSet);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> RemoveAsync(object id)
        {
            TEntity entity = await GetByIdAsync(id);

            if (entity == null) return false;

            return await RemoveAsync(entity);
        }

        public virtual async Task<bool> RemoveAsync(TEntity obj)
        {
            dbSet.Remove(obj);
            return await CommitAsync() > 0 ? true : false;
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return await CommitAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(object id, TEntity obj)
        {
            var avoidingAttachedEntity = await GetByIdAsync(id);
            _context.Entry(avoidingAttachedEntity).State = EntityState.Detached;

            var entry = _context.Entry(obj);
            if (entry.State == EntityState.Detached) _context.Attach(obj);

            _context.Entry(obj).State = EntityState.Modified;
            await CommitAsync();
            return entry.Entity;
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return await CommitAsync();
        }

        private async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            query = GenerateQueryableWhereExpression(query, filter);
            query = GenerateIncludeProperties(query, includeProperties);

            if (orderBy != null)
                return orderBy(query);

            return query;
        }
        private IQueryable<TEntity> GenerateQueryableWhereExpression(IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
                return query.Where(filter);

            return query;
        }

        private IQueryable<TEntity> GenerateIncludeProperties(IQueryable<TEntity> query, params string[] includeProperties)
        {
            foreach (string includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        protected IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities, Func<TEntity, TEntity> DoAction)
        {
            foreach (var e in entities)
            {
                yield return DoAction(e);
            }
            yield break;
        }
    }
}