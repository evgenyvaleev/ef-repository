using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EfRepository
{
    public static class PaginationExtensions
    {
        public async static Task<int> GetPageCount<TEntity>(this IQueryable<TEntity> entities, int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("Page size is invalid.", nameof(pageSize));
            }

            int count = await entities.CountAsync();
            return (int)Math.Ceiling((float)count / pageSize);
        }

        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> entities, int page, int pageSize)
        {
            if (page < 1)
            {
                throw new ArgumentException("Page number is invalid.", nameof(page));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size is invalid.", nameof(pageSize));
            }

            int skip = (page - 1) * pageSize;
            return entities.Skip(skip).Take(pageSize);
        }
    }
}