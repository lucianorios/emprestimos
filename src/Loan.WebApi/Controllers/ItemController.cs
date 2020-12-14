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
    [Route("api/item")]
    public class ItemController : ApiControllerBase
    {
        private readonly IItemService itemService;

        public ItemController(INotificationHandler<DomainNotification> domainNotification, IItemService itemService)
            : base(domainNotification)
        {
            this.itemService = itemService;
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> Obter([FromBody] RequestSearch<ItemSearch> request)
        {
            var result = await this.itemService.GetAllAsync(request);
            return base.Result(result);
        }


        [HttpPost("salvar")]
        public async Task<IActionResult> Salvar([FromBody] ItemViewModel model)
        {
            await this.itemService.Save(model);
            return base.Result();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(long id)
        {
            await this.itemService.Remover(id);
            return base.Result();
        }
    }
}
