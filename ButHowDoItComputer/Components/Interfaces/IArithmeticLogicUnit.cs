using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IArithmeticLogicUnit : IApplicable
    {
        AluOutput Apply(IByte a, IByte b, bool carryIn, Op op);
        public Op Op { get; set; }
        public IByte InputA { get; set; }
        public IByte InputB { get; set; }
        public bool CarryIn { get; set; }
    }
}