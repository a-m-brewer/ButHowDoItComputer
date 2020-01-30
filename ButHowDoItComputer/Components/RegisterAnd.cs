using System.Collections.Generic;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterAnd : IRegisterAnd
    {
        private readonly RegisterListGate _registerListGate;

        public RegisterAnd(IByteAnd byteAnd)
        {
            _registerListGate = new RegisterListGate(byteAnd);
        }
        
        public void Apply(IList<IRegister<IByte>> inputRegisters, IRegister<IByte> outputRegister)
        {
            _registerListGate.Apply(inputRegisters, outputRegister);
        }
    }
}