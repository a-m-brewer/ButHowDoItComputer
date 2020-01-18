using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterEnabler : IRegisterEnabler
    {
        private readonly IRegisterListGateFactory _registerListGateFactory;
        private readonly IByteEnablerListGateFactory _byteEnablerListGateFactory;

        public RegisterEnabler(IRegisterListGateFactory registerListGateFactory, IByteEnablerListGateFactory byteEnablerListGateFactory)
        {
            _registerListGateFactory = registerListGateFactory;
            _byteEnablerListGateFactory = byteEnablerListGateFactory;
        }

        public void Apply(IRegister inputRegister, IBit set, IRegister outputRegister)
        {
            var byteListGate = _byteEnablerListGateFactory.Create(set);
            var registerListGate = _registerListGateFactory.Create(byteListGate);
            registerListGate.Apply(new[] { inputRegister }, outputRegister);
        }
    }
}
