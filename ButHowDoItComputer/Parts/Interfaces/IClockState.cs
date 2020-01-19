using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IClockState
    {
        IBit Bit { get; }
        IBit Cycle();
    }
}