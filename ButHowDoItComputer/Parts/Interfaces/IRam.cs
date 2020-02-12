using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRam : ICpuSettableSubscriber, ICpuEnableSubscriber
    {
        IRegister<IByte> MemoryAddressRegister { get; }
        List<List<IRegister<IByte>>> InternalRegisters { get; }
        bool Set { get; set; }
        bool Enable { get; set; }
        IBus Io { get; }
        void SetMemoryAddress(IByte address);
        void Apply();
        void ApplyState();
        void ApplyEnable();
    }
}