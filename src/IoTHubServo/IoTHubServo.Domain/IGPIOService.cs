using System;

namespace IoTHubServo.Domain
{
    public interface IGPIOService
    {
        void SendServoHigh();

        void SendServoLow();
    }
}
