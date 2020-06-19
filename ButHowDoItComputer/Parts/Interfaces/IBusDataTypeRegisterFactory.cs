using System.Collections.Generic;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IBusDataTypeRegisterFactory<TBusDataType> : IRegisterFactory<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}