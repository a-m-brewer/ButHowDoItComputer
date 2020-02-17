using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterComparator
    {
        (bool equal, bool aLarger) AreEqual(IRegister<IByte> registerA, IRegister<IByte> registerB, bool equal,
            bool aLarger,
            IRegister<IByte> outputRegister);
    }
}