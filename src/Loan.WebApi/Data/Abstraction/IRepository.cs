using Loan.WebApi.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IRepository<TEntity, TPrimaryKey> :
        IReadRepository<TEntity, TPrimaryKey>,
        IWriteRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        DbContext Context { get; }
        DbSet<TEntity> Set { get; }
    }
}
