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
    public class EmprestimoSearch : ISearch<Emprestimo>, IOrderBy<Emprestimo>
    {
        public string Pessoa { get; set; }
        public string Item { get; set; }
        public DateTime? Data { get; set; }
        public bool SomenteCedidos { get; set; }

        public Expression<Func<Emprestimo, bool>> GetFilter()
        {
            var expression = PredicateBuilder.True<Emprestimo>();

            if (!string.IsNullOrEmpty(this.Pessoa))
                expression = expression.And(x => x.Pessoa.Nome.ToLower().Contains(this.Pessoa.ToLower()));

            if (!string.IsNullOrEmpty(this.Item))
                expression = expression.And(x => x.Item.Nome.ToLower().Contains(this.Item.ToLower()));

            if (this.Data.HasValue)
                expression = expression.And(x => x.Data >= this.Data.Value);

            if (this.SomenteCedidos)
                expression = expression.And(x => x.Devolucao == null);

            return expression;
        }

        public Func<IQueryable<Emprestimo>, IOrderedQueryable<Emprestimo>> GetOrderBy()
        {
            return query => query.OrderBy(x => x.Data);
        }
    }
}
