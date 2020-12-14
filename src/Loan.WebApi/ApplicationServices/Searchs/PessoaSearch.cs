using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Data;
using Loan.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.Searchs
{
    public class PessoaSearch : ISearch<Pessoa>, IOrderBy<Pessoa>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Expression<Func<Pessoa, bool>> GetFilter()
        {
            var expression = PredicateBuilder.True<Pessoa>();

            if (!string.IsNullOrEmpty(this.Nome))
                expression = expression.And(x => x.Nome.ToLower().Contains(this.Nome.ToLower()));

            if (!string.IsNullOrEmpty(this.Telefone))
                expression = expression.And(x => x.Telefone == this.Telefone);

            if (!string.IsNullOrEmpty(this.Email))
                expression = expression.And(x => x.Email.ToLower().Contains(this.Email.ToLower()));

            return expression;
        }

        public Func<IQueryable<Pessoa>, IOrderedQueryable<Pessoa>> GetOrderBy()
        {
            return query => query.OrderByDescending(x => x.Nome);
        }
    }
}
