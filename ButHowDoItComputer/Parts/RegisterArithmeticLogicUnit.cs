using System.Collections.Generic;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    class RegisterArithmeticLogicUnit : IRegisterArithmeticLogicUnit
    {
        private readonly IArithmeticLogicUnit _arithmeticLogicUnit;

        public RegisterArithmeticLogicUnit(IArithmeticLogicUnit arithmeticLogicUnit, IByteRegisterFactory byteRegisterFactory)
        {
            InputA = byteRegisterFactory.Create();
            InputB = byteRegisterFactory.Create();
            InputA.Name = $"{nameof(RegisterArithmeticLogicUnit)}.{InputA}";
            InputB.Name = $"{nameof(RegisterArithmeticLogicUnit)}.{InputB}";
            InputA.Set = true;
            InputA.Enable = true;
            InputB.Set = true;
            InputB.Enable = true;
            
            OutputRegister = byteRegisterFactory.Create();
            OutputRegister.Name = $"{nameof(RegisterArithmeticLogicUnit)}.{OutputRegister}";
            OutputRegister.Set = true;
            OutputRegister.Enable = true;
            
            OpInstruction = new Op();
            CarryIn = false;
            Output = new AluOutput();
            _arithmeticLogicUnit = arithmeticLogicUnit;
        }

        public bool CarryIn { get; set; }

        public Op OpInstruction { get; set; }

        public IRegister<IByte> OutputRegister { get; set; }

        public IRegister<IByte> InputB { get; set; }

        public IRegister<IByte> InputA { get; set; }

        public AluOutput Apply(IRegister<IByte> a, IRegister<IByte> b, bool carryIn, Op op, IRegister<IByte> outputRegister)
        {
            InputA = a;
            InputB = b;
            CarryIn = carryIn;
            OpInstruction = op;
            OutputRegister = outputRegister;

            Apply();
            
            return Output;
        }

        public List<IBusInputSubscriber<IByte>> Subscribers { get; } = new List<IBusInputSubscriber<IByte>>();

        public void Update(Op newState)
        {
            OpInstruction = newState;
        }

        public void Apply()
        {
            InputA.Apply();
            InputB.Apply();
            Output = _arithmeticLogicUnit.Apply(InputA.Output, InputB.Output, CarryIn, OpInstruction);

            foreach (var subscriber in Subscribers)
            {
                subscriber.Input = Output.Output;
            }
        }

        public AluOutput Output { get; set; }
    }
}
