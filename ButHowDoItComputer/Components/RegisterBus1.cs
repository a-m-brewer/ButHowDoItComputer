using System.Collections.Generic;
using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterBus1 : IRegisterBus1
    {
        private readonly IRegisterListGateFactory _registerListGateFactory;
        private readonly IBus1 _bus1;
        private readonly IByteGateToListFactory _byteGateToListFactory;

        public IRegister<IByte> InputRegister { get; private set; }
        public IRegister<IByte> OutputRegister { get; private set; }
        
        public bool Set { get; set; }

        public RegisterBus1(IRegisterListGateFactory registerListGateFactory, IBus1 bus1, IByteGateToListFactory byteGateToListFactory, IByteRegisterFactory byteRegisterFactory)
        {
            _registerListGateFactory = registerListGateFactory;
            _bus1 = bus1;
            _byteGateToListFactory = byteGateToListFactory;
            InputRegister = byteRegisterFactory.Create();
            OutputRegister = byteRegisterFactory.Create();
        }

        public void Apply(IRegister<IByte> inputRegister, bool set, IRegister<IByte> outputRegister)
        {
            InputRegister = inputRegister;
            Input = InputRegister.Input;
            OutputRegister = outputRegister;
            Set = set;
            Apply();
        }

        public void Apply()
        {
            InputRegister.Input = Input;
            var byteGate = _byteGateToListFactory.Convert(_bus1, Set);
            var registerGate = _registerListGateFactory.Create(byteGate);
            registerGate.Apply(new[] { InputRegister }, OutputRegister);

            foreach (var subscriber in Subscribers)
            {
                subscriber.Input = OutputRegister.Output;
            }
        }

        public List<IBusInputSubscriber<IByte>> Subscribers { get; set; } = new List<IBusInputSubscriber<IByte>>();
        public IByte Input { get; set; }
    }
}
