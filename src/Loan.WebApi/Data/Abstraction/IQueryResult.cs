using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IQueryResult<T>
    {
        int Page { get; set; }
        int? PageSize { get; set; }
        long Total { get; set; }
        long TotalItens { get; set; }
        IEnumerable<T> Itens { get; set; }
    }
}
