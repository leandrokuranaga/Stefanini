using Stefanini.Application.City.Services;
using Stefanini.Application.Person.Services;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.PersonAggregate;
using Stefanini.Infra.Data;
using Stefanini.Infra.Repository;

namespace Stefanini.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();

            #region Repository
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            #endregion

            #region Services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICityService, CityService>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }
    }
}