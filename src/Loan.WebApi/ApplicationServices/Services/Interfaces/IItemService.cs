using Loan.WebApi.ApplicationServices.Searchs;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.CrossCutting.Http;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Interfaces
{
    public interface IItemService
    {
        Task<ResponseList<ItemViewModel>> GetAllAsync(RequestSearch<ItemSearch> request);
        Task Save(ItemViewModel viewModel);
        Task Remover(long id);
    }
}
