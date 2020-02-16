namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface ICpuPinStates
    {
        PinStates Step();
        void UpdateGeneralPurposeRegisters(bool regAEnable, bool regBEnable, bool regBSet);
    }
}