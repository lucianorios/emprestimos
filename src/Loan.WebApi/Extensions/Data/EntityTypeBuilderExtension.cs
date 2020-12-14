using Loan.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.WebApi.Extensions.Data
{
    internal static class EntityTypeBuilderExtension
    {
        public static void ConfigurarPropriedadesBasicas<T>(this EntityTypeBuilder<T> builder, string tableName) where T : Entity
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id).HasName($"PK_{tableName}_ID");
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.DataCriacao).HasColumnName("DATACRIACAO");
        }
    }
}
