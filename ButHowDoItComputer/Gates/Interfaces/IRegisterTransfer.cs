using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IRegisterTransfer
    {
        void Apply(IRegister<IByte> inputRegister, IRegister<IByte> outputRegister);
    }
}