using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IClock : ICpuSettableSubscriber, ICpuEnableSubscriber
    {
        IBit ClkS { get; }
        IBit ClkE { get; }
        IBit ClkD { get; }
        IBit Clk { get; }
        ClockOutput Cycle();
    }
}