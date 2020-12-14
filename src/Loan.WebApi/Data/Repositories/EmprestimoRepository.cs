using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Context;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Repositories
{
    public class EmprestimoRepository : Repository<Emprestimo, long>, IEmprestimoRepository
    {
        public EmprestimoRepository(LoanContext context) : base(context)
        {
        }

        public Task<Emprestimo> GetById(long id)
        {
            return this.Set
                .Include(x => x.Item)
                .Include(x => x.Pessoa)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
