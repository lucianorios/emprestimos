using Loan.WebApi.ApplicationServices.Handlers;
using Loan.WebApi.ApplicationServices.Services.Impl;
using Loan.WebApi.ApplicationServices.Services.Interfaces;
using Loan.WebApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.WebApi.Extensions.ServiceConfiguration
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IEmprestimoService, EmprestimoService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}
