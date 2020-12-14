using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Context;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using System.Linq;

namespace Loan.WebApi.Data.Repositories
{
    public class ItemRepository : Repository<Item, long>, IItemRepository
    {
        public ItemRepository(LoanContext context) : base(context)
        {
        }

        public bool ValidarDuplicado(Item item)
        {
            return this.Set
                .Any(e => e.Nome.ToLower() == item.Nome.ToLower() && e.Id != item.Id);
        }

    }
}
