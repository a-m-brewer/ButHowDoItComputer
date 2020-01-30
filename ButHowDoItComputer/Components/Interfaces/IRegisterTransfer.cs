using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterTransfer
    {
        void Apply(IRegister<IByte> inputRegister, IRegister<IByte> outputRegister);
    }
}