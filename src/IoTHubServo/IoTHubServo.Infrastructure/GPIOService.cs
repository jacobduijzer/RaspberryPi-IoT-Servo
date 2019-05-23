using System.Device.Gpio;
using System.Threading.Tasks;
using IoTHubServo.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IoTHubServo.Infrastructure
{
    public class GPIOService : IGPIOService
    {
        private readonly ILogger<GPIOService> _logger;
        private readonly IOptions<GpioOptions> _gpioOptions;

        private readonly GpioController _gpioController;

        public GPIOService(ILogger<GPIOService> logger, IOptions<GpioOptions> gpioOptions)
        {
            _logger = logger;
            _gpioOptions = gpioOptions;

            _gpioController = new GpioController();
            _gpioController.OpenPin(_gpioOptions.Value.ServoPin, PinMode.Output);
        }

        public void SendServoHigh() => WriteToServo(_gpioOptions.Value.ServoPin, PinValue.High);

        public void SendServoLow() => WriteToServo(_gpioOptions.Value.ServoPin, PinValue.Low);

        private void WriteToServo(int servoPin, PinValue pinValue)
        {
            _logger.LogDebug($"Start writing {pinValue} to pin {servoPin}");
            _gpioController.Write(servoPin, PinValue.Low);
            _logger.LogDebug($"Finished writing {pinValue} to pin {servoPin}");
        }
    }
}
