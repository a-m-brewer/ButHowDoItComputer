using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteComparator
    {
        (bool equal, bool ALarger, IByte output) AreEqual(IByte a, IByte b, bool aboveBitIsEqual, bool aboveBitALarger);
    }
}