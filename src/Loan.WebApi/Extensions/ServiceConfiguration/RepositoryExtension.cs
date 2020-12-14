using Loan.WebApi.Data.Abstraction;
using Loan.WebApi.Data.Context;
using Loan.WebApi.Data.Repositories;
using Loan.WebApi.Data.UnitOfWork;
using Loan.WebApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.WebApi.Extensions.ServiceConfiguration
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<LoanContext>(opt =>
                {
                    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), option => option.MigrationsAssembly("Loan.WebApi"));
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
