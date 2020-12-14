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
    public class PessoaService : BaseService, IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(INotificationHandler<DomainNotification> domainNotifications, IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper,
            IPessoaRepository pessoaRepository)
            : base(domainNotifications, unitOfWork, mediator, mapper)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<ResponseList<PessoaViewModel>> GetAllAsync(RequestSearch<PessoaSearch> request)
        {
            var result = await this._pessoaRepository.GetAllAsync(request);
            return base.CreateResultListModel<Pessoa, PessoaViewModel>(result);
        }

        public async Task Save(PessoaViewModel viewModel)
        {
            var pessoa = new Pessoa();

            if (viewModel.Id == 0)
            {
                pessoa = new Pessoa(0, viewModel.Nome, viewModel.Email, viewModel.Telefone);
            }
            else
            {
                pessoa = this._pessoaRepository.GetById(viewModel.Id);

                pessoa.SetEmail(viewModel.Email);
                pessoa.SetNome(viewModel.Nome);
                pessoa.SetTelefone(viewModel.Telefone);
            }

            pessoa.Validar();

            var duplicado = this._pessoaRepository.ValidarDuplicado(pessoa);


            if (pessoa.Valido && !duplicado)
            {

                await this._pessoaRepository.UpdateAsync(pessoa);

                await this.CommitAsync();
            }
            else
            {
                this.AddNotification(pessoa);

                if (duplicado)
                    this.AddNotification("A Pessoa informada já se encontra cadastrada");
            }
        }

        public async Task Remover(long id)
        {
            this._pessoaRepository.Delete(id);

            this.Commit();
        }
    }
}
