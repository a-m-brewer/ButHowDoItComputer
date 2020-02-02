using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.DataTypes
{
    public class Op
    {
        public IBit One { get; set; } = false.ToBit();
        public IBit Two { get; set; } = false.ToBit();
        public IBit Three { get; set; } = false.ToBit();
    }
}
