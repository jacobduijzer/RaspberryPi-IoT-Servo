using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RaspIOTService.Domain;

namespace RaspIOTService.Application
{
    public class ServoNotificationHandler : INotificationHandler<ServoNotification>
    {
        private readonly ILogger<ServoNotificationHandler> _logger;

        private readonly IGpioService _gpioService;

        public ServoNotificationHandler(ILogger<ServoNotificationHandler> logger, IGpioService gpioService)
        {
            _logger = logger;
            _gpioService = gpioService;
        }

        public Task Handle(ServoNotification notification, CancellationToken cancellationToken)
        {
            if (notification.High)
                _gpioService.SendServoHigh();
            else
                _gpioService.SendServoLow();

            return Task.CompletedTask;
        }
    }
}
