using System;
using System.Collections;
using System.Collections.Generic;

namespace EfRepository
{
    public class PagedCollection<TEntity> : IPagedCollection<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Entities { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        
        public PagedCollection(IEnumerable<TEntity> entities, int page, int pageSize, int pageCount)
        {
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
            Page = page;
            PageSize = pageSize;
            PageCount = pageCount;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Entities.GetEnumerator();
        }
    }
}