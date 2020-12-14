using Loan.WebApi.ApplicationServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<AuthorizationViewModel> Login(UserViewModel model);
    }
}
