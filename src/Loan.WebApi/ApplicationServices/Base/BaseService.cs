using AutoMapper;
using Loan.WebApi.ApplicationServices.Handlers;
using Loan.WebApi.CrossCutting.Http;
using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Base
{
    public abstract class BaseService
    {
        protected readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        private readonly DomainNotificationHandler domainNotifications;
        private bool isValid = true;
        public BaseService(INotificationHandler<DomainNotification> domainNotifications, IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            this.domainNotifications = (DomainNotificationHandler)domainNotifications;
            this.mapper = mapper;
        }

        public bool Valido()
        {
            return this.isValid;
        }
        protected void AddNotification(string message)
        {
            this.SendNotification(new DomainNotification(base.GetType().Name, message));
            this.isValid = false;
        }

        protected void AddNotification(Entity entity)
        {
            if (entity.Invalido)
            {
                foreach (var notificacao in entity.Notifications)
                {
                    this.AddNotification(notificacao.Value);
                }
            }
        }

        protected void SendNotification(Event notification)
        {
            mediator.Publish(notification).GetAwaiter().GetResult();
        }

        protected Task<bool> CommitAsync()
        {
            return Task.Factory.StartNew(this.Commit);
        }

        protected bool Commit()
        {
            if (domainNotifications.HasNotification()) return false;
            if (unitOfWork.Commit()) return true;

            this.AddNotification("Houve um problema ao salvar os dados!");
            return false;
        }

        protected ResponseList<E> CreateResultListModel<T, E>(IQueryResult<T> result) where T : Entity
        {
            var models = mapper.Map<IEnumerable<E>>(result.Itens);
            return new ResponseList<E>(models, result.Total, result.TotalItens);
        }

        protected ResponseList<E> CreateResultListModel<T, E>(IEnumerable<T> result) where T : Entity
        {
            var models = mapper.Map<IEnumerable<E>>(result);
            return new ResponseList<E>(models, result.Count(), result.Count());
        }
    }
}
