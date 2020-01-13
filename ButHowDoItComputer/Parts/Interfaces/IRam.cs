using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRam
    {
        IBus MemoryAddressBus { get; }
        IRegister MemoryAddressRegister { get; }
        IBit Set { get; set; }
        IBit Enable { get; set; }
        IBus Io { get; }
        void SetMemoryAddress(IByte address);
        void Apply();
        void ApplyState();
        void ApplyEnable();
    }
}