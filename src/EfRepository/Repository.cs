using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EfRepository
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _dbSet.AddRangeAsync(entities);
        }

        public virtual Task<bool> ExistsAsync(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return ExistsAsync(key, CancellationToken.None);
        }

        public async virtual Task<bool> ExistsAsync(object key, CancellationToken cancellationToken)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await GetAsync(key, cancellationToken)) != null;
        }

        public virtual Task<TEntity> GetAsync(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetAsync(key, CancellationToken.None);
        }

        public virtual Task<TEntity> GetAsync(object key, CancellationToken cancellationToken)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _dbSet.FindAsync(key, cancellationToken);
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return GetAllAsync(CancellationToken.None);
        }

        public virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _dbSet.ToListAsync(cancellationToken);
        }

        public virtual Task<PagedCollection<TEntity>> GetAllAsync(int page, int pageSize)
        {
            if (page < 1)
            {
                throw new ArgumentException("Page number is invalid.", nameof(page));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size is invalid.", nameof(pageSize));
            }

            return GetAllAsync(page, pageSize, CancellationToken.None);
        }

        public async virtual Task<PagedCollection<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            if (page < 1)
            {
                throw new ArgumentException("Page number is invalid.", nameof(page));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size is invalid.", nameof(pageSize));
            }

            List<TEntity> entities = await _dbSet.Paginate(page, pageSize).ToListAsync();
            int pageCount = await _dbSet.GetPageCount(pageSize);

            return new PagedCollection<TEntity>(entities, page, pageSize, pageCount);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
        }
    }
}