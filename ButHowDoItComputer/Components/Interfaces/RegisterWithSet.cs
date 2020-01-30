using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterWithSet
    {
        void Apply(IRegister<IByte> inputRegister, IBit set, IRegister<IByte> outputRegister);
    }
}
