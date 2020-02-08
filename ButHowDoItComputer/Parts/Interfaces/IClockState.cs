using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IClockState
    {
        bool Bit { get; }
        bool Cycle();
    }
}