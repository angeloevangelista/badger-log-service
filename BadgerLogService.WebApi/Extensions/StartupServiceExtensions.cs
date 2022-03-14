using BadgerLogService.Domain.Contracts.Services;
using BadgerLogService.Shared.Contracts.Services;
using BadgerLogService.Implementation.Services;
using BadgerLogService.Shared.Services;
using BadgerLogService.Shared.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using BadgerLogService.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BadgerLogService.Domain.Contracts.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using BadgerLogService.Implementation.Repositories;
using System.Text.Json;

namespace BadgerLogService.WebApi.Extensions
{
  public static class StartupServiceExtensions
  {
    public static IServiceCollection ConfigureServicesExtension(
      this IServiceCollection serviceCollection,
      IConfiguration configuration
    )
    {
      /*Injection*/
      serviceCollection.AddTransient<GlobalExceptionHandlerMiddleware>();

      serviceCollection.AddScoped<IJwtService, JwtService>();
      serviceCollection.AddScoped<ILogService, LogService>();
      serviceCollection.AddScoped<IApplicationService, ApplicationService>();

      serviceCollection.AddScoped<ILogRepository, LogRepository>();
      serviceCollection.AddScoped<IApplicationRepository, ApplicationRepository>();
      /*Injection*/

      /*EF Core*/
      serviceCollection.AddDbContext<BadgerLogServiceDataContext>(options =>
        options.UseNpgsql(
          configuration.GetConnectionString("PostgreSQL"),
          action => action.MigrationsAssembly("BadgerLogService.Data")
        )
      );

      serviceCollection.AddScoped<BadgerLogServiceDataContext>();
      /*EF Core*/

      return serviceCollection;
    }

    public static IApplicationBuilder ConfigureExtension(
      this IApplicationBuilder applicationBuilder,
      IWebHostEnvironment env
    )
    {
      if (env.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BadgerLogService.WebApi v1"));
      }

      applicationBuilder.UseMiddleware<GlobalExceptionHandlerMiddleware>();

      applicationBuilder.UseCors(p => p
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
      );

      return applicationBuilder;
    }
  }
}
