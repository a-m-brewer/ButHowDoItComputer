using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IArithmeticLogicUnit<TBusDataType> : IApplicable where TBusDataType : IList<bool>
    {
        AluOutput<TBusDataType> Apply(TBusDataType a, TBusDataType b, bool carryIn, Op op);
        public Op Op { get; set; }
        public TBusDataType InputA { get; set; }
        public TBusDataType InputB { get; set; }
        public bool CarryIn { get; set; }
    }
}