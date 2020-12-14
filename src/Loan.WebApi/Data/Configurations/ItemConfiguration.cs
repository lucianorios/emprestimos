using Loan.WebApi.Domain.Entities;
using Loan.WebApi.Extensions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.WebApi.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ConfigurarPropriedadesBasicas("ITEM");
            builder.Property(x => x.Tipo).HasColumnName("TIPO");
            builder.Property(x => x.Nome).HasColumnName("NOME");
            builder.Property(x => x.Cedido).HasColumnName("CEDIDO");
        }
    }
}
