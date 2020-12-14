using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IOrderBy<T>
    {
        Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy();
    }
}
