using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterAnd
    {
        private readonly IByteAnd _byteAnd;

        public RegisterAnd(IByteAnd byteAnd)
        {
            _byteAnd = byteAnd;
        }
        
        public void Apply(IList<IRegister> inputRegisters, IRegister outputRegister)
        {
            foreach (var inputRegister in inputRegisters)
            {
                inputRegister.Apply();
            }

            var outputRegisterInput = _byteAnd.Apply(inputRegisters.Select(s => s.Output).ToArray());
            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();
        }
    }
}