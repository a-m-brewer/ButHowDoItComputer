using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    interface IRegisterEnabler
    {
        public void Apply(IRegister inputRegister, IBit set, IRegister outputRegister);
    }
}
