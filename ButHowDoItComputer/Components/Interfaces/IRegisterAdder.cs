using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterAdder
    {
        IBit Apply(IRegister inputRegisterA, IRegister inputRegisterB, IBit carryIn, IRegister outputRegister);
    }
}