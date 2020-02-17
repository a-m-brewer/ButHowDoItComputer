using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class ClockState : IClockState
    {
        public ClockState()
        {
            Bit = false;
        }

        public bool Bit { get; private set; }

        public bool Cycle()
        {
            Bit = !Bit;
            return Bit;
        }
    }
}