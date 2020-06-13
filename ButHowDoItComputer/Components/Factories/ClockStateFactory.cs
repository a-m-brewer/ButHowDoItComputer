using ButHowDoItComputer.Components.Interfaces;

namespace ButHowDoItComputer.Components.Factories
{
    public class ClockStateFactory : IClockStateFactory
    {
        public IClockState Create()
        {
            return new ClockState();
        }
    }
}