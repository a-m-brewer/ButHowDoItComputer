using System.Collections.Generic;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBusInputNotifier<T>
    {
        List<IInputable<T>> BusSubscribers { get; }
    }
}