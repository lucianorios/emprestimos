using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Extensions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.WebApi.Data.Configurations
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ConfigurarPropriedadesBasicas("PESSOA");
            builder.Property(x => x.Email).HasColumnName("EMAIL");
            builder.Property(x => x.Nome).HasColumnName("NOME");
            builder.Property(x => x.Telefone).HasColumnName("TELEFONE");

            builder.HasMany(e => e.Emprestimos).WithOne(e => e.Pessoa).HasForeignKey(e => e.PessoaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
