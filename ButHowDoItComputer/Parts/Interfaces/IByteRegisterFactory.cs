using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IByteRegisterFactory : IObjectCreationFactory<IRegister<IByte>>
    {
    }
}