using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterBus1 : IRegisterWithSet, ICpuSettableSubscriber, IBusInputSubscriber<IByte>
    {
        List<IBusInputSubscriber<IByte>> Subscribers { get; set; }
    }
}