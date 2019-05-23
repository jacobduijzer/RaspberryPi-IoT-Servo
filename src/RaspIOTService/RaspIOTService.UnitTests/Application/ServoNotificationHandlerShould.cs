using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RaspIOTService.Application;
using RaspIOTService.Domain;
using Xunit;

namespace RaspIOTService.UnitTests.Application
{
    public class ServoNotificationHandlerShould
    {
        private readonly Mock<ILogger<ServoNotificationHandler>> _mockLogger;
        private readonly Mock<IGpioService> _mockGpioService;

        public ServoNotificationHandlerShould()
        {
            _mockLogger = new Mock<ILogger<ServoNotificationHandler>>();
            _mockGpioService = new Mock<IGpioService>(MockBehavior.Strict);
            _mockGpioService.Setup(x => x.SendServoHigh()).Verifiable();
            _mockGpioService.Setup(x => x.SendServoLow()).Verifiable();
        }

        [Fact]
        public void Construct() =>
            new ServoNotificationHandler(_mockLogger.Object, _mockGpioService.Object)
                .Should().BeOfType<ServoNotificationHandler>();

        [Fact]
        public async Task SendHigh()
        {
            var handler = new ServoNotificationHandler(_mockLogger.Object, _mockGpioService.Object);
            await handler.Handle(new ServoNotification(true), default);

            _mockGpioService.Verify(x => x.SendServoHigh(), Times.Once);
        }

        [Fact]
        public async Task SendLow()
        {
            var handler = new ServoNotificationHandler(_mockLogger.Object, _mockGpioService.Object);
            await handler.Handle(new ServoNotification(false), default);

            _mockGpioService.Verify(x => x.SendServoLow(), Times.Once);
        }
    }
}
