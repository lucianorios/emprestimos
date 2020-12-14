using MediatR;
using System;

namespace Loan.WebApi.Domain.Entities
{
    public class Event : INotification
    {
        public Guid OperationId { get; private set; }

        public Event()
        {
            this.OperationId = Guid.NewGuid();
        }
    }
}
