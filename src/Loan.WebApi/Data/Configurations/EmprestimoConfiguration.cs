using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Extensions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.WebApi.Data.Configurations
{
    public class EmprestimoConfiguration : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder.ConfigurarPropriedadesBasicas("EMPRESTIMO");
            builder.Property(x => x.Data).HasColumnName("DATA");
            builder.Property(x => x.Devolucao).HasColumnName("DEVOLUCAO");

            builder.HasOne(e => e.Pessoa).WithMany().HasForeignKey(e => e.PessoaId);
            builder.HasOne(e => e.Item).WithMany().HasForeignKey(e => e.ItemId);
        }
    }
}
