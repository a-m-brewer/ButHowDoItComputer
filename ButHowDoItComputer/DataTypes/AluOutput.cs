using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class AluOutput
    {
        public IByte Output { get; set; }
        public bool ALarger { get; set; }
        public bool Equal { get; set; }
        public bool CarryOut { get; set; }
        public bool Zero { get; set; }
    }
}
