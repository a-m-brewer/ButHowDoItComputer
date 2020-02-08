using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterAdder
    {
        bool Apply(IRegister<IByte> inputRegisterA, IRegister<IByte> inputRegisterB, bool carryIn, IRegister<IByte> outputRegister);
    }
}