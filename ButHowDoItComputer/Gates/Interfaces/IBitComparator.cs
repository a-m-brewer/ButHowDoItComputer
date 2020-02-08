using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBitComparator
    {
        (bool equal, bool ALarger, bool output) AreEqual(bool a, bool b, bool aboveBitIsEqual, bool aboveBitALarger);
    }
}