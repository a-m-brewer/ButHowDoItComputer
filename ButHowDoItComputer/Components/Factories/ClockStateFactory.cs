using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;

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