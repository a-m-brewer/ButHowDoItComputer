using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteShifter
    {
        (IByte Ouput, bool ShiftOut) Shift(IByte input, bool shiftIn);
    }

    public interface IRightByteShifter : IByteShifter { }
    public interface ILeftByteShifter : IByteShifter { }
}
