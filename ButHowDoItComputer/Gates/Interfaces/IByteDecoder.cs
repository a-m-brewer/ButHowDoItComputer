using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteDecoder
    {
        IByte Decode(IBit a, IBit b, IBit c);
    }
}