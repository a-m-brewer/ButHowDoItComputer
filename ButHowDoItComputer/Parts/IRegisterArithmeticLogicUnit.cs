using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public interface IRegisterArithmeticLogicUnit : ICpuSubscriberNotifier<Op>
    {
        bool CarryIn { get; set; }
        Op OpInstruction { get; set; }
        IRegister<IByte> OutputRegister { get; set; }
        IRegister<IByte> InputB { get; set; }
        IRegister<IByte> InputA { get; set; }
        AluOutput Output { get; set; }
        AluOutput Apply(IRegister<IByte> a, IRegister<IByte> b, bool carryIn, Op op, IRegister<IByte> outputRegister);
        List<IBusInputSubscriber<IByte>> Subscribers { get; } 
    }
}