using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Domain.Repositories
{
    public interface IPessoaRepository : IRepository<Pessoa, long>
    {
        bool ValidarDuplicado(Pessoa pessoa);
    }
}
