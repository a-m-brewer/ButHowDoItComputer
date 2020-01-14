using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterOr : IRegisterOr
    {
        private RegisterListGate _registerListGate;

        public RegisterOr(IByteOr byteOr)
        {
            _registerListGate = new RegisterListGate(byteOr);
        }
        
        public void Apply(IList<IRegister> inputRegisters, IRegister outputRegister)
        {
            _registerListGate.Apply(inputRegisters, outputRegister);
        }
    }
}