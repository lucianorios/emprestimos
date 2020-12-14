using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Data;
using Loan.WebApi.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Loan.WebApi.ApplicationServices.Searchs
{
    public class ItemSearch : ISearch<Item>, IOrderBy<Item>
    {
        public string Nome { get; set; }
        public bool Disponivel { get; set; }

        public Expression<Func<Item, bool>> GetFilter()
        {
            var expression = PredicateBuilder.True<Item>();

            if (!string.IsNullOrEmpty(this.Nome))
                expression = expression.And(x => x.Nome.ToLower().Contains(this.Nome.ToLower()));

            if (this.Disponivel)
                expression = expression.And(x => x.Cedido == false);

            return expression;
        }

        public Func<IQueryable<Item>, IOrderedQueryable<Item>> GetOrderBy()
        {
            return query => query.OrderByDescending(x => x.Nome);
        }
    }
}
