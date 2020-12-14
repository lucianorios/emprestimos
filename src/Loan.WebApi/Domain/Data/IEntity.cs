using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.WebApi.Domain.Data
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; }
        DateTime DataCriacao { get; }
    }
}
