using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using FDXTestApp.Application.ConfigurationSettings;

namespace FDXTestApp.Application.Configuration
{
    public static class ApplicationConfiguration
    {
        public static RabbitMqSettings RabbitMqSettings { get; set; } = null!;

        static bool? _isRunningInContainer;

        static bool IsRunningInContainer =>
            _isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;

        public static IHostBuilder AddApplication(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                RabbitMqSettings rabbitMqSettings =
                    hostContext.Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>()!;

                services.AddMediatR(config =>
                    config.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).Assembly));

                services.AddMassTransit(busConfigurator =>
                {
                    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                    {
                        if (IsRunningInContainer)
                        {
                            busFactoryConfigurator.Host("rabbitmq");
                        }
                        else
                        {
                            busFactoryConfigurator.Host(rabbitMqSettings.Host);
                        }
                    });
                });
            });

            return builder;
        }
    }
}
