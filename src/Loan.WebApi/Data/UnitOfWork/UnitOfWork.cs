using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LoanContext context;

        public UnitOfWork(LoanContext context)
        {
            this.context = context;
        }

        public bool Commit()
        {
            return context.SaveChangesAsync().GetAwaiter().GetResult() > 0;
        }

        public Task<bool> CommitAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return Commit();
            });
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
