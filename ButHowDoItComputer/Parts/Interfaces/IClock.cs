using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IClock
    {
        IBit ClkS { get; }
        IBit ClkE { get; }
        IBit ClkD { get; }
        IBit Clk { get; }
        ClockOutput Cycle();
    }
}