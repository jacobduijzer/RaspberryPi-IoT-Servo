namespace RaspIOTService.Domain
{
    public interface IGpioService
    {
        void SendServoHigh();

        void SendServoLow();
    }
}
