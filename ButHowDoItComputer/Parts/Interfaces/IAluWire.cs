using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IAluWire
    {
        IByte Apply(params IByte[] input);
    }
}