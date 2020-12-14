using Loan.WebApi.ApplicationServices.Searchs;
using Loan.WebApi.ApplicationServices.Services.Interfaces;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.CrossCutting.Http;
using Loan.WebApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/pessoa")]
    public class PessoaController : ApiControllerBase
    {
        private readonly IPessoaService pessoaService;

        public PessoaController(INotificationHandler<DomainNotification> domainNotification, IPessoaService pessoaService)
            : base(domainNotification)
        {
            this.pessoaService = pessoaService;
        }


        [HttpPost("getAll")]
        public async Task<IActionResult> Obter([FromBody] RequestSearch<PessoaSearch> request)
        {
            var result = await this.pessoaService.GetAllAsync(request);
            return base.Result(result);
        }

        [HttpPost("salvar")]
        public async Task<IActionResult> Salvar([FromBody] PessoaViewModel model)
        {
            await this.pessoaService.Save(model);
            return base.Result();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(long id)
        {
            await this.pessoaService.Remover(id);
            return base.Result();
        }
    }
}
