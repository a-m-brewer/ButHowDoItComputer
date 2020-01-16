using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBitComparator
    {
        (IBit equal, IBit ALarger, IBit output) AreEqual(IBit a, IBit b, IBit aboveBitIsEqual, IBit aboveBitALarger);
    }
}