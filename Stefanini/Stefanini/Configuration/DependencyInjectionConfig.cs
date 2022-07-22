using Stefanini.Domain.Interfaces;
using Stefanini.Infra.Context;
using Stefanini.Infra.Repository;

namespace Stefanini.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }
    }
}