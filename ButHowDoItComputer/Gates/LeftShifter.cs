using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Gates
{
    public class LeftShifter<TBusDataType> : Shifter<TBusDataType>, ILeftShifter<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly ILeftBusDataTypeShifter<TBusDataType> _leftBusDataTypeShifter;

        public LeftShifter(IBusDataTypeFactory<TBusDataType> busDataTypeFactory, ILeftBusDataTypeShifter<TBusDataType> leftBusDataTypeShifter) : base(busDataTypeFactory)
        {
            _leftBusDataTypeShifter = leftBusDataTypeShifter;
        }

        protected override IList<bool> GetShifter(IRegister<TBusDataType> inputRegister)
        {
            var (output, shiftOut) = _leftBusDataTypeShifter.Shift(inputRegister.Output, ShiftIn);
            ShiftOut = shiftOut;
            return output.ToBits();
        }
    }
}