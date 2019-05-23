using System.Device.Gpio;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RaspIOTService.Domain;

namespace RaspIOTService.Infrastructure
{
    public class GpioService : IGpioService
    {
        private readonly IGpioController _gpioController;
        private readonly IOptions<GpioOptions> _gpioOptions;
        private readonly ILogger<GpioService> _logger;

        public GpioService(
            IOptions<GpioOptions> gpioOptions,
            ILogger<GpioService> logger)
        {
            _gpioOptions = gpioOptions;
            _logger = logger;

            _gpioController = new GpioController();
            _gpioController.OpenPin(_gpioOptions.Value.ServoPin, PinMode.Output);
        }

        public void SendServoHigh() => WriteToServo(_gpioOptions.Value.ServoPin, PinValue.High);

        public void SendServoLow() => WriteToServo(_gpioOptions.Value.ServoPin, PinValue.Low);

        private void WriteToServo(int servoPin, PinValue pinValue)
        {
            _logger.LogTrace($"Writing {pinValue} to pin {servoPin}");
            _gpioController.Write(servoPin, PinValue.Low);
            _logger.LogTrace($"Wrote {pinValue} to pin {servoPin}");
        }
    }
}
