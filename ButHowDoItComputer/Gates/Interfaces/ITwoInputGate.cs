using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface ITwoInputGate
    {
        IBit Apply(IBit input1, IBit input2);
    }
}