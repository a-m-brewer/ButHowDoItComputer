using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBusInputNotifier<T>
    {
        List<IBusInputSubscriber<T>> BusSubscribers { get; }
    }
}