using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IRegisterTransfer<TBusDataType> where TBusDataType : IBusDataType
    {
        void Apply(IRegister<TBusDataType> inputRegister, IRegister<TBusDataType> outputRegister);
    }
}