using Loan.WebApi.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IWriteRepository<TEntity, TPrimaryKey> : IDisposable
        where TEntity : IEntity<TPrimaryKey>
    {
        #region Sync

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);

        void Delete(TPrimaryKey id);

        #endregion

        #region Async

        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TPrimaryKey id);

        #endregion
    }
}
