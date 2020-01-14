using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterXOr : IRegisterXOr
    {
        private readonly IByteXOr _byteXOr;

        public RegisterXOr(IByteXOr byteXOr)
        {
            _byteXOr = byteXOr;
        }
        
        public void Apply(IList<IRegister> inputRegisters, IRegister outputRegister)
        {
            foreach (var inputRegister in inputRegisters)
            {
                inputRegister.Apply();
            }

            var outputRegisterInput = _byteXOr.Apply(inputRegisters.Select(s => s.Output).ToArray());
            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();
        }
    }
}