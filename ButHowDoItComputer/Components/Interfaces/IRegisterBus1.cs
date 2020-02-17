using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterBus1 : IRegisterWithSet, ICpuSettableSubscriber, IInputable<IByte>
    {
        List<IInputable<IByte>> Subscribers { get; set; }
    }
}