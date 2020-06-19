using System.Collections.Generic;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IRegisterTransfer<TBusDataType> where TBusDataType : IList<bool>
    {
        void Apply(IRegister<TBusDataType> inputRegister, IRegister<TBusDataType> outputRegister);
    }
}