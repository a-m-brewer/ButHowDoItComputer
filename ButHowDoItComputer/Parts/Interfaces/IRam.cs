using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRam : ICpuSettableSubscriber, ICpuEnableSubscriber
    {
        IBus MemoryAddressBus { get; }
        IRegister<IByte> MemoryAddressRegister { get; }
        IBit Set { get; set; }
        IBit Enable { get; set; }
        IBus Io { get; }
        void SetMemoryAddress(IByte address);
        void Apply();
        void ApplyState();
        void ApplyEnable();
    }
}