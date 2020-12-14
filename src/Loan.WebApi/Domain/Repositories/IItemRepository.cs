using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Domain.Entities;

namespace Loan.WebApi.Domain.Repositories
{
    public interface IItemRepository : IRepository<Item, long>
    {
        bool ValidarDuplicado(Item item);
    }
}
