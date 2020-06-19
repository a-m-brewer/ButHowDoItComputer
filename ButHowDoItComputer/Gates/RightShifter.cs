using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Gates
{
    public class RightShifter<TBusDataType> : Shifter<TBusDataType>, IRightShifter<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IRightBusDataTypeShifter<TBusDataType> _rightBusDataTypeShifter;

        public RightShifter(IBusDataTypeFactory<TBusDataType> busDataTypeFactory, IRightBusDataTypeShifter<TBusDataType> rightBusDataTypeShifter) : base(busDataTypeFactory)
        {
            _rightBusDataTypeShifter = rightBusDataTypeShifter;
        }

        protected override IList<bool> GetShifter(IRegister<TBusDataType> inputRegister)
        {
            var (secondRegisterInput, shiftOut) = _rightBusDataTypeShifter.Shift(inputRegister.Output, ShiftIn);
            ShiftOut = shiftOut;

            return secondRegisterInput.ToBits();
        }
    }
}