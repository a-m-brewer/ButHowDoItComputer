using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IByteToBase10Converter
    {
        uint ToInt(IByte input);
        IByte ToByte(uint input);
    }
}