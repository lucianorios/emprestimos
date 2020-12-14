using AutoMapper;
using Loan.WebApi.ApplicationServices.Base;
using Loan.WebApi.ApplicationServices.Searchs;
using Loan.WebApi.ApplicationServices.Services.Interfaces;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.CrossCutting.Http;
using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Data;
using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Services.Impl
{
    public class EmprestimoService : BaseService, IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPessoaRepository _pessoaRepository;
        public EmprestimoService(INotificationHandler<DomainNotification> domainNotifications, IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper,
            IEmprestimoRepository emprestimoRepository, IItemRepository itemRepository, IPessoaRepository pessoaRepository)
            : base(domainNotifications, unitOfWork, mediator, mapper)
        {
            _emprestimoRepository = emprestimoRepository;
            _itemRepository = itemRepository;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<ResponseList<EmprestimoViewModel>> GetAllAsync(RequestSearch<EmprestimoSearch> request)
        {
            var include = new Include<Emprestimo>(query => query
                .Include(x => x.Pessoa)
                .Include(x => x.Item));

            var result = await this._emprestimoRepository.GetAllAsync(request.Page, request.PageSize, request.Search.GetFilter(), request.Search.GetOrderBy(), include.Includes);
            return base.CreateResultListModel<Emprestimo, EmprestimoViewModel>(result);
        }

        public async Task Save(EmprestimoViewModel viewModel)
        {
            var emprestimo = new Emprestimo();

            var pessoa = _pessoaRepository.GetById(viewModel.Pessoa.Id);

            var item = _itemRepository.GetById(viewModel.Item.Id);

            item.SetCedido(true);

            emprestimo.SetData(viewModel.Data);
            emprestimo.SetPessoa(pessoa);
            emprestimo.SetItem(item);

            emprestimo.Validar();


            if (emprestimo.Valido)
            {
                await this._emprestimoRepository.InsertAsync(emprestimo);
                await this._itemRepository.UpdateAsync(item);

                await this.CommitAsync();
            }
            else
            {
                this.AddNotification(emprestimo);
            }
        }

        public async Task Remover(long id)
        {
            this._emprestimoRepository.Delete(id);

            this.Commit();
        }
        public async Task Finalizar(long id)
        {
            var emprestimo = await _emprestimoRepository.GetById(id);

            emprestimo.SetDevolucao(DateTime.Now);

            emprestimo.Validar();

            if (emprestimo.Valido)
            {
                emprestimo.Item.SetCedido(false);

                await this._emprestimoRepository.UpdateAsync(emprestimo);

                await this.CommitAsync();
            }
        }
    }
}
