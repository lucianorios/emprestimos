using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.ViewModels
{
    public class AuthorizationViewModel
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
