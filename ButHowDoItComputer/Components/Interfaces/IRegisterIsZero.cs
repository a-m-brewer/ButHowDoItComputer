using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterIsZero
    {
        IBit IsZero(IRegister<IByte> inputRegister);
    }
}
