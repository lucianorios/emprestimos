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
    [Route("api/emprestimo")]
    public class EmprestimoController : ApiControllerBase
    {
        private readonly IEmprestimoService emprestimoService;

        public EmprestimoController(INotificationHandler<DomainNotification> domainNotification, IEmprestimoService emprestimoService)
            : base(domainNotification)
        {
            this.emprestimoService = emprestimoService;
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> Obter([FromBody] RequestSearch<EmprestimoSearch> request)
        {
            var result = await this.emprestimoService.GetAllAsync(request);
            return base.Result(result);
        }


        [HttpPost("salvar")]
        public async Task<IActionResult> Salvar([FromBody] EmprestimoViewModel model)
        {
            await this.emprestimoService.Save(model);
            return base.Result();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(long id)
        {
            await this.emprestimoService.Remover(id);
            return base.Result();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Finalizar(long id)
        {
            await this.emprestimoService.Finalizar(id);
            return base.Result();
        }
    }
}
