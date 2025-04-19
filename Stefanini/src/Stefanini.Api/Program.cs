using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stefanini.Infra.Utils;
using Stefanini.Infra.CrossCutting.IoC;
using Stefanini.API.Extensions;
using Stefanini.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<AppSettings>>().Value);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString);

var appSettings = builder.Configuration.Get<AppSettings>();

builder.Services.AddLocalServices(builder.Configuration);
builder.Services.AddLocalDbContext(appSettings);

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddControllers();

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.UseAuthorization();

app.MapControllers();

app.Run();
