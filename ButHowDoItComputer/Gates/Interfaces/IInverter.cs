using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInverter
    {
        IByte Invert(IByte input);
    }
}