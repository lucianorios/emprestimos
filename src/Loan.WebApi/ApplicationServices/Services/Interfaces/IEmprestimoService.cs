using Loan.WebApi.ApplicationServices.Searchs;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.CrossCutting.Http;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Interfaces
{
    public interface IEmprestimoService
    {
        Task<ResponseList<EmprestimoViewModel>> GetAllAsync(RequestSearch<EmprestimoSearch> request);
        Task Save(EmprestimoViewModel viewModel);
        Task Remover(long id);
        Task Finalizar(long id);
    }
}
