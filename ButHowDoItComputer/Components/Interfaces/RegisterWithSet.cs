using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterWithSet
    {
        void Apply(IRegister<IByte> inputRegister, bool set, IRegister<IByte> outputRegister);
    }
}