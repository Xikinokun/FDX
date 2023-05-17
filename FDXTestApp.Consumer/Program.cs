using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using FDXTestApp.Application.Configuration;
using FDXTestApp.Infrastructure.Configuration;
using FDXTestApp.Application.ConfigurationSettings;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using FDXTestApp.Application.Consumers;

namespace FDXTestApp.Consumer;

public static class Program
{
    private static readonly IHost AppHost;

    static bool? _isRunningInContainer;

    static bool IsRunningInContainer =>
        _isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;
    static Program()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostContext, configuration) =>
            {
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddJsonFile("appsettings.json");
                configuration.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                    optional: true);
                configuration.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) => 
            {
                RabbitMqSettings rabbitMqSettings =
                    hostContext.Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>()!;

                services.AddMediatR(config =>
                    config.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).Assembly));

                services.AddMassTransit(busConfigurator =>
                {
                    busConfigurator.AddConsumer<SmsConsumer>();
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
                        busFactoryConfigurator.ReceiveEndpoint(rabbitMqSettings.QueueName, c => {
                            c.ConfigureConsumer<SmsConsumer>(context);
                        });
                    });
                });
            })
            .AddInfrastructure()
            .UseSerilog((HostBuilderContext hostContext, IServiceProvider services, LoggerConfiguration configuration) =>
            {
#if DEBUG
                Serilog.Debugging.SelfLog.Enable(Console.Error);
#endif
                configuration.ReadFrom.Configuration(hostContext.Configuration);

                configuration.Enrich.FromLogContext();
                configuration.Enrich.WithMachineName();
                configuration.Enrich.WithThreadId();
            })
            .Build();

        AppHost.Run();
    }

    public static void Main(string[] _)
    {
        
    }
}