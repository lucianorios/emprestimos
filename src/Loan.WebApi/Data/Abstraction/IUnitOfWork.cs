using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
