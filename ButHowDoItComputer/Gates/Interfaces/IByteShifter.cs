using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteShifter
    {
        (IByte Ouput, IBit ShiftOut) Shift(IByte input, IBit shiftIn);
    }

    public interface IRightByteShifter : IByteShifter { }
    public interface ILeftByteShifter : IByteShifter { }
}
