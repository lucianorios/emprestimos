using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.WebApi.Domain.Entities
{
    public class DomainNotification : Event
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value) : base()
        {
            Key = key;
            Value = value;
        }
    }
}
