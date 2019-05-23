using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RaspIOTService.Domain;

namespace RaspIOTService.Infrastructure
{
    public class FakeGpioService : IGpioService
    {
        private readonly IOptions<GpioOptions> _gpioOptions;
        private readonly ILogger<GpioService> _logger;

        public FakeGpioService(
            IOptions<GpioOptions> gpioOptions,
            ILogger<GpioService> logger)
        {
            _gpioOptions = gpioOptions;
            _logger = logger;
        }

        public void SendServoHigh() => _logger.LogTrace($"Writing HIGH to pin {_gpioOptions.Value.ServoPin}");

        public void SendServoLow() => _logger.LogTrace($"Writing LOW to pin {_gpioOptions.Value.ServoPin}");
    }
}
