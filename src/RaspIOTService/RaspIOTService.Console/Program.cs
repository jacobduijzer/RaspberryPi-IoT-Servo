using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RaspIOTService.Application;
using RaspIOTService.Domain;
using RaspIOTService.Infrastructure;

namespace RaspIOTService.Console
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceCollection = GetServiceColletion();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var mediator = serviceProvider.GetService<IMediator>();

            for (int i = 0; i < 10; i++)
            {
                await mediator.Publish(new ServoNotification(true));
                await Task.Delay(TimeSpan.FromSeconds(2));

                await mediator.Publish(new ServoNotification(false));
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private static IServiceCollection GetServiceColletion()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddLogging(config => config.AddConsole().AddDebug())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);

            var configuration = GetConfiguration();
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            serviceCollection.AddOptions();
            serviceCollection.Configure<GpioOptions>(configuration.GetSection("Gpio"));

            serviceCollection.AddTransient<IGpioService, FakeGpioService>();
            //serviceCollection.AddTransient<IGpioService, GpioService>();

            serviceCollection.AddMediatR(cfg => cfg.AsScoped(), typeof(ServoNotificationHandler).GetTypeInfo().Assembly);

            return serviceCollection;
        }

        private static IConfigurationRoot GetConfiguration() => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false)
                .Build();
    }
}
