using IoTHubServo.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IoTHubServo.Infrastructure
{
    public class FakeGPIOService : IGPIOService
    {
        private readonly ILogger<GPIOService> _logger;
        private readonly IOptions<GpioOptions> _gpioOptions;

        public FakeGPIOService(ILogger<GPIOService> logger, IOptions<GpioOptions> gpioOptions)
        {
            _logger = logger;
            _gpioOptions = gpioOptions;
        }

        public void SendServoHigh() => _logger.LogTrace($"HIGH to {_gpioOptions.Value.ServoPin}");

        public void SendServoLow() => _logger.LogTrace($"LOW to {_gpioOptions.Value.ServoPin}");
    }
}
