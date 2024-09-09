using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EfRepository
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task<bool> ExistsAsync(object key, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(object key);
        Task<TEntity> GetAsync(object key);
        Task<TEntity> GetAsync(object key, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<PagedCollection<TEntity>> GetAllAsync(int page, int pageSize);
        Task<PagedCollection<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}