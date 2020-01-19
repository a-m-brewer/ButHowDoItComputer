using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class ClockState : IClockState
    {
        private readonly IBitFactory _bitFactory;

        public ClockState(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
            Bit = _bitFactory.Create(false);
        }

        public IBit Bit { get; private set; }

        public IBit Cycle()
        {
            Bit = _bitFactory.Create(!Bit.State);
            return Bit;
        }
    }
}