using AutoMapper;
using Loan.WebApi.ApplicationServices.Base;
using Loan.WebApi.ApplicationServices.Searchs;
using Loan.WebApi.ApplicationServices.Services.Interfaces;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.CrossCutting.Http;
using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Impl
{
    public class ItemService : BaseService, IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(INotificationHandler<DomainNotification> domainNotifications, IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper,
            IItemRepository itemRepository)
            : base(domainNotifications, unitOfWork, mediator, mapper)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ResponseList<ItemViewModel>> GetAllAsync(RequestSearch<ItemSearch> request)
        {
            var result = await this._itemRepository.GetAllAsync(request);
            return base.CreateResultListModel<Item, ItemViewModel>(result);
        }

        public async Task Save(ItemViewModel viewModel)
        {
            var item = new Item();

            if (viewModel.Id == 0)
            {
                item = new Item(0, viewModel.Nome);
            }
            else
            {
                item = this._itemRepository.GetById(viewModel.Id);

                item.SetNome(viewModel.Nome);
            }

            item.Validar();

            var duplicado = this._itemRepository.ValidarDuplicado(item);


            if (item.Valido && !duplicado)
            {

                await this._itemRepository.UpdateAsync(item);

                await this.CommitAsync();
            }
            else
            {
                this.AddNotification(item);

                if (duplicado)
                    this.AddNotification("Item informado já se encontra cadastrado");
            }
        }

        public async Task Remover(long id)
        {
            this._itemRepository.Delete(id);

            this.Commit();
        }
    }
}
