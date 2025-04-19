using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stefanini.Application.City.Services;
using Stefanini.Application.Person.Services;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.SeedWork.Notification;
using Stefanini.Infra.Data;
using Stefanini.Infra.Data.Repository;
using Stefanini.Infra.Utils;

namespace Stefanini.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void AddLocalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotification, Notification>();

            #region Repository
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            #endregion

            #region Services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICityService, CityService>();
            #endregion
        }

        public static void AddLocalDbContext(this IServiceCollection services, AppSettings settings)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(settings.ConnectionStrings.DefaultConnection));

            services.AddMemoryCache();
        }

    }
}
