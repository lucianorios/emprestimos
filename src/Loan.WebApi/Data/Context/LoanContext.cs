using Loan.WebApi.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Data.Context
{
    public class LoanContext : DbContext
    {
        public LoanContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("LOAN");

            modelBuilder.ApplyConfiguration(new EmprestimoConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

            base.OnModelCreating(modelBuilder);
        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            base.OnConfiguring(optionsBuilder);
        }
#endif
    }
}
