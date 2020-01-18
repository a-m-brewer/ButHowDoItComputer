using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterBus1 : IRegisterBus1
    {
        private readonly IRegisterListGateFactory _registerListGateFactory;
        private readonly IBus1 _bus1;
        private readonly IByteGateToListFactory _byteGateToListFactory;

        public RegisterBus1(IRegisterListGateFactory registerListGateFactory, IBus1 bus1, IByteGateToListFactory byteGateToListFactory)
        {
            _registerListGateFactory = registerListGateFactory;
            _bus1 = bus1;
            _byteGateToListFactory = byteGateToListFactory;
        }

        public void Apply(IRegister inputRegister, IBit bus1, IRegister outputRegister)
        {
            var byteGate = _byteGateToListFactory.Convert(_bus1, bus1);
            var registerGate = _registerListGateFactory.Create(byteGate);
            registerGate.Apply(new[] { inputRegister }, outputRegister);
        }
    }
}
