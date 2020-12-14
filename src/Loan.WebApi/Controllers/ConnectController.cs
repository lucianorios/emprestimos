using Loan.WebApi.ApplicationServices.Services.Interfaces;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Loan.WebApi.Controllers
{
    [ApiController]
    [Route("api/connect")]
    public class ConnectController : ApiControllerBase
    {
        public readonly IUsuarioService usuarioService;

        public ConnectController(INotificationHandler<DomainNotification> domainNotification, IUsuarioService usuarioService) 
            : base(domainNotification) 
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel model)
        {
            var result = await this.usuarioService.Login(model);
            
            return base.Result(result);
        }
    }
}
