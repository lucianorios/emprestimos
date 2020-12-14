using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface ISearch<T> where T : class
    {
        Expression<Func<T, bool>> GetFilter();
    }
}
