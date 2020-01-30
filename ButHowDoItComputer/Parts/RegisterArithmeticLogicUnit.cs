using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    class RegisterArithmeticLogicUnit : ICpuSubscriberNotifier<Op>
    {
        private readonly IArithmeticLogicUnit _arithmeticLogicUnit;

        public RegisterArithmeticLogicUnit(IArithmeticLogicUnit arithmeticLogicUnit, IByteRegisterFactory byteRegisterFactory)
        {
            InputA = byteRegisterFactory.Create();
            InputB = byteRegisterFactory.Create();
            OutputRegister = byteRegisterFactory.Create();
            OpInstruction = new Op();
            CarryIn = new Bit(false);
            Output = new AluOutput();
            _arithmeticLogicUnit = arithmeticLogicUnit;
        }

        public IBit CarryIn { get; set; }

        public Op OpInstruction { get; set; }

        public IRegister<IByte> OutputRegister { get; set; }

        public IRegister<IByte> InputB { get; set; }

        public IRegister<IByte> InputA { get; set; }

        public AluOutput Apply(IRegister<IByte> a, IRegister<IByte> b, IBit carryIn, Op op, IRegister<IByte> outputRegister)
        {
            InputA = a;
            InputB = b;
            CarryIn = carryIn;
            OpInstruction = op;
            OutputRegister = outputRegister;

            Apply();
            
            return Output;
        }

        public void Update(Op newState)
        {
            OpInstruction = newState;
        }

        public void Apply()
        {
            InputA.Apply();
            InputB.Apply();
            Output = _arithmeticLogicUnit.Apply(InputA.Output, InputB.Output, CarryIn, OpInstruction);
        }

        public AluOutput Output { get; set; }
    }
}
