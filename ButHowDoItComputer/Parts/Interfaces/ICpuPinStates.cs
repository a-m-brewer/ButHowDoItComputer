using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface ICpuPinStates
    {
        PinStates Step();
        PinStates Step(IByte instruction, Caez flags);
        void UpdateGeneralPurposeRegisters(bool regAEnable, bool regBEnable, bool regBSet);
    }
}