using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterWithSet
    {
        void Apply(IRegister inputRegister, IBit set, IRegister outputRegister);
    }
}
