using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class AluOutput
    {
        public IByte Output { get; set; }
        public IBit ALarger { get; set; }
        public IBit Equal { get; set; }
        public IBit CarryOut { get; set; }
        public IBit Zero { get; set; }
    }
}
