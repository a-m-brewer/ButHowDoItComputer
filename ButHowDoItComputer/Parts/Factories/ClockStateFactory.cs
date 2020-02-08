using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts.Factories
{
    public class ClockStateFactory : IClockStateFactory
    {
        public IClockState Create()
        {
            return new ClockState();
        }
    }
}