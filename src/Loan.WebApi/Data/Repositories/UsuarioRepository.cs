using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Context;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario, long>, IUsuarioRepository
    {
        public UsuarioRepository(LoanContext context) : base(context)
        {
        }

        public Task<Usuario> GetByLogin(string login, string password)
        {
            return this.Set
                .FirstOrDefaultAsync(e => e.Login == login && e.Password == password);
        }
    }
}
