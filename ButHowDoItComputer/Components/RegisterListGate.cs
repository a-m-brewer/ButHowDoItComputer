using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterListGate : IRegisterListGate
    {
        private readonly IByteListGate _byteListGate;

        public RegisterListGate(IByteListGate byteListGate)
        {
            _byteListGate = byteListGate;
        }
        
        public void Apply(IList<IRegister> inputRegisters, IRegister outputRegister)
        {
            foreach (var inputRegister in inputRegisters)
            {
                inputRegister.Apply();
            }

            var outputRegisterInput = _byteListGate.Apply(inputRegisters.Select(s => s.Output).ToArray());
            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();
        }
    }

    public class RegisterListGateFactory : IRegisterListGateFactory
    {
        public IRegisterListGate Create(IByteListGate byteListGate)
        {
            return new RegisterListGate(byteListGate);
        }
    }

    public interface IRegisterListGateFactory
    {
        IRegisterListGate Create(IByteListGate byteListGate);
    }
}