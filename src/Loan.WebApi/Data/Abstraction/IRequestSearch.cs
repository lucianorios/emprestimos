using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IRequestSearch<T>
    {
        public T Search { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
    }
}
