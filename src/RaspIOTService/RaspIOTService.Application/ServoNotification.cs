using MediatR;

namespace RaspIOTService.Application
{
    public class ServoNotification : INotification
    {
        public readonly bool High;

        public ServoNotification(bool high) => High = high;
    }
}
