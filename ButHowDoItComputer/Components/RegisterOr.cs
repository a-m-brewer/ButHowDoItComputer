using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterOr : IRegisterOr
    {
        private readonly IByteOr _byteOr;

        public RegisterOr(IByteOr byteOr)
        {
            _byteOr = byteOr;
        }
        
        public void Apply(IList<IRegister> inputRegisters, IRegister outputRegister)
        {
            foreach (var inputRegister in inputRegisters)
            {
                inputRegister.Apply();
            }

            var outputRegisterInput = _byteOr.Apply(inputRegisters.Select(s => s.Output).ToArray());
            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();
        }
    }
}