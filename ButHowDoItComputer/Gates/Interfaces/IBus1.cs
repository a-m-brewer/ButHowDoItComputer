using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBus1 : IByteGate, IBusInputSubscriber<IByte>, IBusInputNotifier<IByte>, ICpuSettableSubscriber
    {
    }
}