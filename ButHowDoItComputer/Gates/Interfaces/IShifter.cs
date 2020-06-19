using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IShifter<TBusDataType> : IRegisterTransfer<TBusDataType> where TBusDataType : IList<bool>
    {
        bool ShiftIn { get; set; }
        bool ShiftOut { get; set; }
    }

    public interface IRightShifter<TBusDataType> : IShifter<TBusDataType> where TBusDataType : IList<bool>
    {
    }

    public interface ILeftShifter<TBusDataType> : IShifter<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}