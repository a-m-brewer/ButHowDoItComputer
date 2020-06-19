using System.Collections.Generic;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRam<TBusDataType> : IApplicable, IPinPart where TBusDataType : IList<bool>
    {
        IRegister<TBusDataType> MemoryAddressRegister { get; }
        IRegister<TBusDataType>[][] InternalRegisters { get; }
        bool Set { get; set; }
        bool Enable { get; set; }
        IBus<TBusDataType> Io { get; }
        void SetMemoryAddress(TBusDataType address);
        void Apply();
        void ApplyState();
        void ApplyEnable();
    }
}