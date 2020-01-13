using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterTransfer
    {
        void Apply(IRegister inputRegister, IRegister outputRegister);
    }
}