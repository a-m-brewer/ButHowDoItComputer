using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IClock : ISettable, IEnablable, IApplicable
    {
        bool ClkS { get; }
        bool ClkE { get; }
        bool ClkD { get; }
        bool Clk { get; }
        ClockOutput Cycle();
    }
}