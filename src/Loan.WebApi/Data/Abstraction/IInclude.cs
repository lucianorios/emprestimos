using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IInclude<T>
    {
        Func<IQueryable<T>, IQueryable<T>> GetIncludes();
    }
}
