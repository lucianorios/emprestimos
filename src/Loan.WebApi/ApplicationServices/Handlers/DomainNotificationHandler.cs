using Loan.WebApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Loan.WebApi.ApplicationServices.Handlers
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>, IDisposable
    {
        private List<DomainNotification> domainNotifications;

        public DomainNotificationHandler()
        {
            domainNotifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            domainNotifications.Add(notification);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications() => domainNotifications;

        public virtual bool HasNotification() => GetNotifications().Any();

        public void Dispose()
        {
            domainNotifications = new List<DomainNotification>();
        }
    }
}
