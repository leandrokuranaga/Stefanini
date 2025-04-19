using Microsoft.OpenApi.Models;
using Stefanini.API.SwaggerExamples.City;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Stefanini.Api.Extensions
{
    public static class SwaggerSetupExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Stefanini",
                    Version = "v1",
                    Description = "Stefanini Technical Test"
                });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var groupName = apiDesc.GroupName;
                    return groupName == docName;
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.ExampleFilters();

                c.EnableAnnotations();
            });

            services.AddSwaggerExamplesFromAssemblyOf<CityRequestExample>();
        }
        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stefanini API v1");
                c.RoutePrefix = "swagger";
            });
        }
    }
}