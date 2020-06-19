using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IComputerState<TBusDataType> where TBusDataType : IList<bool>
    {
        IRegister<Caez> Flags { get; }
        List<IRegister<TBusDataType>> GeneralPurposeRegisters { get; }
        IRegister<TBusDataType> Ir { get; }
        IRegister<TBusDataType> Iar { get; }
        IRegister<TBusDataType> Acc { get; }
        IRam<TBusDataType> Ram { get; }
        IRegister<TBusDataType> Tmp { get; }
        IBus1<TBusDataType> Bus1 { get; }
        IArithmeticLogicUnit<TBusDataType> Alu { get; }
        IBus<TBusDataType> Bus { get; }
        IoPinStates<TBusDataType> Io { get; set; }
        void UpdatePins(PinStates pinStates);
    }
}