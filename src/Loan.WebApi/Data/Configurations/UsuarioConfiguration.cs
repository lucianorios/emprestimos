using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Extensions.Data;
using Loan.WebApi.Extensions.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.WebApi.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ConfigurarPropriedadesBasicas("USUARIO");
            builder.Property(x => x.Login).HasColumnName("LOGIN");
            builder.Property(x => x.Password).HasColumnName("PASSWORD");

            builder.HasData(
                new Usuario(1, "johndoe@loan.com", "Senha@123456".Encrypt())
            );
        }
    }
}
