using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
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