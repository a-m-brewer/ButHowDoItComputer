using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteComparator
    {
        (IBit equal, IBit ALarger, IByte output) AreEqual(IByte a, IByte b, IBit aboveBitIsEqual, IBit aboveBitALarger);
    }
}