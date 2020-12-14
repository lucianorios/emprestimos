using Loan.WebApi.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IReadRepository<TEntity, TPrimaryKey> : IDisposable
        where TEntity : IEntity<TPrimaryKey>
    {
        #region SYNC

        TEntity GetById(TPrimaryKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryResult<TEntity> GetAll<T>(IRequestSearch<T> request) where T : class;
        IQueryResult<TEntity> GetAll(int page = 0, int? pageSize = null, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        #endregion SYNC


        #region ASSYNC

        Task<TEntity> GetByIdAsync(TPrimaryKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<List<TEntity>> GetByIdAsync(IEnumerable<TPrimaryKey> ids, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<IQueryResult<TEntity>> GetAllAsync<T>(IRequestSearch<T> request) where T : class;
        Task<IQueryResult<TEntity>> GetAllAsync(int page = 0, int? pageSize = null, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        #endregion ASSYNC
    }
}
