using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IWire
    {
        IByte Apply(params IByte[] input);
    }
}
