using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<IQueryResult<TEntity>> GetAllAsync<TEntity>(this IQueryable<TEntity> query, int page = 0, int? pageSize = null)
        {
            var pageReturn = new QueryResult<TEntity>(page, pageSize);

            var taskCount = query.CountAsync();
            query = query.ApplayPagination(page, pageSize);

            pageReturn.Itens = query.AsEnumerable();
            pageReturn.Total = await taskCount;
            pageReturn.TotalItens = pageReturn.Itens.Count();

            return pageReturn;
        }

        public static Task<IQueryResult<TEntity>> GetAllAsync<TEntity>(this IQueryable<TEntity> query, int page = 0, int? pageSize = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.GetAllAsync(page, pageSize);
        }

        public static Task<IQueryResult<TEntity>> GetAllAsync<TEntity>(this IQueryable<TEntity> query)
        {
            return query.GetAllAsync(0, null);
        }

        public static IQueryResult<TEntity> GetAll<TEntity>(this IQueryable<TEntity> query, int page = 0, int? pageSize = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return query.GetAllAsync(page, pageSize, orderBy, includes).GetAwaiter().GetResult();
        }

        internal static IQueryable<TEntity> ApplayPagination<TEntity>(this IQueryable<TEntity> query, int page = 0, int? pageSize = null)
        {
            var skip = page == 0 ? 0 : page * (pageSize ?? 0);

            query = query.Skip(skip);

            if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return query;
        }
    }
}
