using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IComputerState<TBusDataType> where TBusDataType : IBusDataType
    {
        IRegister<Caez> Flags { get; }
        List<IRegister<TBusDataType>> GeneralPurposeRegisters { get; }
        IRegister<TBusDataType> Ir { get; }
        IRegister<TBusDataType> Iar { get; }
        IRegister<TBusDataType> Acc { get; }
        IRam Ram { get; }
        IRegister<TBusDataType> Tmp { get; }
        IBus1 Bus1 { get; }
        IArithmeticLogicUnit Alu { get; }
        IBus<TBusDataType> Bus { get; }
        IoPinStates Io { get; set; }
        void UpdatePins(PinStates pinStates);
    }
}