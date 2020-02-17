using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRam : IApplicable, IPinPart
    {
        IRegister<IByte> MemoryAddressRegister { get; }
        List<List<IRegister<IByte>>> InternalRegisters { get; }
        bool Set { get; set; }
        bool Enable { get; set; }
        IBus<IByte> Io { get; }
        void SetMemoryAddress(IByte address);
        void Apply();
        void ApplyState();
        void ApplyEnable();
    }
}