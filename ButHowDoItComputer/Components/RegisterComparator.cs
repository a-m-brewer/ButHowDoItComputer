using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterComparator : IRegisterComparator
    {
        private readonly IByteComparator _byteComparator;

        public RegisterComparator(IByteComparator byteComparator)
        {
            _byteComparator = byteComparator;
        }
        
        public (IBit equal, IBit aLarger) AreEqual(IRegister<IByte> registerA, IRegister<IByte> registerB, IBit equal, IBit aLarger,
            IRegister<IByte> outputRegister)
        {
            registerA.Apply();
            registerB.Apply();
            var (eq, al, o) = _byteComparator.AreEqual(registerA.Output, registerB.Output, equal, aLarger);
            outputRegister.Input = o;
            return (eq, al);
        }
    }
}