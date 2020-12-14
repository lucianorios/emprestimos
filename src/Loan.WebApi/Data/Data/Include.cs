using Loan.WebApi.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Data
{
    public class Include<TEntity> : IInclude<TEntity>
    {
        public Include(Func<IQueryable<TEntity>, IQueryable<TEntity>> expression)
        {
            Includes = expression;
        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Includes { get; }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> GetIncludes()
        {
            return this.Includes;
        }
    }
}
