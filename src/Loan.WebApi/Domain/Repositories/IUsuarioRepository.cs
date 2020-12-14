using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Domain.Entities;
using System.Threading.Tasks;

namespace Loan.WebApi.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        Task<Usuario> GetByLogin(string login, string password);
    }
}
