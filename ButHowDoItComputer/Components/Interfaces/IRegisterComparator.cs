using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterComparator
    {
        (IBit equal, IBit aLarger) AreEqual(IRegister<IByte> registerA, IRegister<IByte> registerB, IBit equal, IBit aLarger,
            IRegister<IByte> outputRegister);
    }
}