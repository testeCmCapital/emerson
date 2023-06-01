using Domain.Domain;
using Domain.DomainInteface;
using Domain.DomainService;
using Domain.DomainServiceInterface;
using Domain.UoWInterface;
using Infrastructure.CrossCutting.Data.Context;
using Infrastructure.CrossCutting.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting.IoC
{
    public static class Injector
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<DapperContext, DapperContext>();
  
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();           
            services.AddTransient<IAuthDomainServices, AuthDomainServices>();
            services.AddTransient<IJwtDomain, JwtDomain>();

            return services;
        }
    }
}
