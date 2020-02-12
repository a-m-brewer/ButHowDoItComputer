using System.Collections.Generic;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBusInputNotifier<T>
    {
        List<IBusInputSubscriber<T>> BusSubscribers { get; }
    }
}