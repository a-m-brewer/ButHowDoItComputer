using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IAluWire
    {
        IByte Apply(params IByte[] input);
    }
}