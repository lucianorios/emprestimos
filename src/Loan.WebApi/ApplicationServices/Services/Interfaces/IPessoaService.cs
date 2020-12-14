using Loan.WebApi.ApplicationServices.Searchs;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.CrossCutting.Http;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<ResponseList<PessoaViewModel>> GetAllAsync(RequestSearch<PessoaSearch> request);
        Task Save(PessoaViewModel viewModel);
        Task Remover(long id);
    }
}
