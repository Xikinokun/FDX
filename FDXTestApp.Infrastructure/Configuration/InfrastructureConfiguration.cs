using FDXTestApp.Application.Contracts;
using FDXTestApp.Infrastructure.Contexts;
using FDXTestApp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FDXTestApp.Infrastructure.Configuration
{
    public static class InfrastructureConfiguration
    {
        static bool? _isRunningInContainer;

        static bool IsRunningInContainer =>
            _isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;

        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                if (IsRunningInContainer)
                {
                    services.AddDbContext<Context>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("SmsConnectionStringContainer")));
                }
                else
                {
                    services.AddDbContext<Context>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("SmsConnectionString")));
                }

                services.AddScoped<ISmsRepository, SmsRepository>();
            });

            return builder;
        }
    }
}
