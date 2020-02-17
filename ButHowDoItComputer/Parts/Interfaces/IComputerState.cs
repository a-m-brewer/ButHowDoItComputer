using System.Collections.Generic;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IComputerState
    {
        IRegister<Caez> Flags { get; }
        List<IRegister<IByte>> GeneralPurposeRegisters { get; }
        IRegister<IByte> Ir { get; }
        IRegister<IByte> Iar { get; }
        IRegister<IByte> Acc { get; }
        IRam Ram { get; }
        IRegister<IByte> Tmp { get; }
        IBus1 Bus1 { get; }
        IArithmeticLogicUnit Alu { get; }
        IBus<IByte> Bus { get; }
        void UpdatePins(PinStates pinStates);
    }
}