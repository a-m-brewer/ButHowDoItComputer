using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterXOr : IRegisterXOr
    {
        private readonly RegisterListGate _registerListGate;

        public RegisterXOr(IByteXOr byteXOr)
        {
            _registerListGate = new RegisterListGate(byteXOr);
        }
        
        public void Apply(IList<IRegister<IByte>> inputRegisters, IRegister<IByte> outputRegister)
        {
            _registerListGate.Apply(inputRegisters, outputRegister);
        }
    }
}