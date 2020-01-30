using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class CpuInput
    {
        public ICpuInput<IByte> Ir { get; set; }
        public Caez Caez { get; set; }
    }
}