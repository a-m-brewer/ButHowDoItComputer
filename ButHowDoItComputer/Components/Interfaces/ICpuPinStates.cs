using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface ICpuPinStates<TBusDataType> where TBusDataType : IBusDataType
    {
        PinStates Step();
        PinStates Step(TBusDataType instruction, Caez flags);
        void UpdateGeneralPurposeRegisters(bool regAEnable, bool regBEnable, bool regBSet);
    }
}