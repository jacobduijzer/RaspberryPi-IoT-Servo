using System.IO;
using System.Threading.Tasks;
using IoTHubServo.Domain;
using IoTHubServo.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace IoTHubServo
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceCollection = GetServiceColletion();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var gpio = serviceProvider.GetService<IGPIOService>();

            for (int i = 0; i < 10; i++)
            {
                gpio.SendServoHigh();
                await Task.Delay(TimeSpan.FromSeconds(2));
                gpio.SendServoLow();
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

            serviceCollection.AddTransient<IGPIOService, GPIOService>();
            //serviceCollection.AddTransient<IGPIOService, FakeGPIOService>();

            return serviceCollection;
        }

        private static IConfigurationRoot GetConfiguration() => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false)
                .Build();
    }
}
