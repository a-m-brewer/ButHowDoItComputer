using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeShifter<TBusDataType> where TBusDataType : IList<bool>
    {
        (TBusDataType Ouput, bool ShiftOut) Shift(TBusDataType input, bool shiftIn);
    }

    public interface IRightBusDataTypeShifter<TBusDataType> : IBusDataTypeShifter<TBusDataType> where TBusDataType : IList<bool>
    {
    }

    public interface ILeftBusDataTypeShifter<TBusDataType> : IBusDataTypeShifter<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}