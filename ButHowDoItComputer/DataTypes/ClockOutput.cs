using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class ClockOutput
    {
        public IBit Clk { get; set; }
        public IBit ClkD { get; set; }
        public IBit ClkS { get; set; }
        public IBit ClkE { get; set; }
    }
}