using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Context;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Repositories
{
    public class PessoaRepository : Repository<Pessoa, long>, IPessoaRepository
    {
        public PessoaRepository(LoanContext context) : base(context)
        {
        }

        public bool ValidarDuplicado(Pessoa pessoa)
        {
            return this.Set
                .Any(e => e.Nome.ToLower() == pessoa.Nome.ToLower() && e.Telefone == pessoa.Telefone && e.Email == pessoa.Email && e.Id != pessoa.Id);
        }
    }
}
