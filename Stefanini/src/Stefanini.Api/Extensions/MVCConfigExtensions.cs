using Microsoft.AspNetCore.Mvc;

namespace Stefanini.API.Extensions
{
    public static class MvcConfigExtensions
    {
        public static void AddCustomMvc(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap["lowercase"] = typeof(string);
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
        }
    }
}
