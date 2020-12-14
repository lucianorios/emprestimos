using Loan.WebApi.Data.Extensions;
using Loan.WebApi.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public class Repository<TEntity, TPrimaryKey> :
       IRepository<TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>

    {
        public DbContext Context { get; protected set; }
        public DbSet<TEntity> Set { get; protected set; }

        public Repository(DbContext context)
        {
            this.Context = context;
            this.Set = context.Set<TEntity>();
        }

        #region SYNC

        #region Write

        public void Insert(TEntity entity)
        {
            this.InsertAsync(entity).GetAwaiter().GetResult();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            this.InsertAsync(entities).GetAwaiter().GetResult();
        }

        public void Update(TEntity entity)
        {
            this.UpdateAsync(entity).GetAwaiter().GetResult();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            this.UpdateAsync(entities).GetAwaiter().GetResult();
        }

        public void Delete(TEntity entity)
        {
            this.DeleteAsync(entity).GetAwaiter().GetResult();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            this.DeleteAsync(entities).GetAwaiter().GetResult();
        }

        public void Delete(TPrimaryKey id)
        {
            this.DeleteAsync(id).GetAwaiter().GetResult();
        }

        #endregion Write

        #region Read

        public TEntity GetById(TPrimaryKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return this.GetByIdAsync(id, includes).GetAwaiter().GetResult();
        }

        public IQueryResult<TEntity> GetAll<T>(IRequestSearch<T> request) where T : class
        {
            return this.GetAllAsync(request).GetAwaiter().GetResult();
        }


        public IQueryResult<TEntity> GetAll(int page = 0, int? pageSize = null, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return this.GetAllAsync(page, pageSize, filter, orderBy, includes).GetAwaiter().GetResult();
        }

        #endregion Read

        #endregion SYNC


        #region ASSYNC

        #region Write

        public Task InsertAsync(TEntity entity)
        {
            return this.Set.AddAsync(entity).AsTask();
        }

        public Task InsertAsync(IEnumerable<TEntity> entities)
        {
            return this.Set.AddRangeAsync(entities);
        }

        public Task UpdateAsync(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                this.Set.Update(entity);
            });
        }

        public Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            return Task.Factory.StartNew(() =>
            {
                this.Set.UpdateRange(entities);
            });
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.Set.Remove(entity);
            });
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            return Task.Factory.StartNew(() =>
            {
                this.Set.RemoveRange(entities);
            });
        }

        public Task DeleteAsync(TPrimaryKey id)
        {
            var entity = this.Set.Find(id);
            return this.DeleteAsync(entity);
        }

        #endregion Write

        #region Read

        public Task<TEntity> GetByIdAsync(TPrimaryKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            if (includes != null)
            {
                var query = includes(this.Set);
                return query.FirstOrDefaultAsync(x => x.Id.Equals(id));
            }

            return this.Set.FindAsync(id).AsTask();
        }

        public Task<List<TEntity>> GetByIdAsync(IEnumerable<TPrimaryKey> ids, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var query = this.Set.Where(x => ids.Contains(x.Id));

            if (includes != null)
            {
                query = includes(query);
            }

            return query.ToListAsync();
        }

        public Task<IQueryResult<TEntity>> GetAllAsync<T>(IRequestSearch<T> request) where T : class
        {
            var filter = request.Search as ISearch<TEntity>;
            var orderBy = request.Search as IOrderBy<TEntity>;
            var includes = request.Search as IInclude<TEntity>;

            return this.GetAllAsync(request.Page, request.PageSize, filter?.GetFilter(), orderBy?.GetOrderBy(), includes?.GetIncludes());
        }

        public Task<IQueryResult<TEntity>> GetAllAsync(int page = 0, int? pageSize = null, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = this.Set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.GetAllAsync(page, pageSize, orderBy, includes);
        }

        #endregion Read

        #endregion ASSYNC

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
