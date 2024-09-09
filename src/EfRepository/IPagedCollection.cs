using System.Collections.Generic;

namespace EfRepository
{
    public interface IPagedCollection<TEntity>: IEnumerable<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> Entities { get; }
        int Page { get; }
        int PageSize { get; }
        int PageCount { get; }
    }
}