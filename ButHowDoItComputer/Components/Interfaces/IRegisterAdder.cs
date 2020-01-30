using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterAdder
    {
        IBit Apply(IRegister<IByte> inputRegisterA, IRegister<IByte> inputRegisterB, IBit carryIn, IRegister<IByte> outputRegister);
    }
}