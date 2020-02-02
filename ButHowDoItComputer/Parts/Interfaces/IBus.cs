using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IBus : IList<IRegister<IByte>>, IBusInputSubscriber<IByte>
    {
        void Apply();
        IByte State { get; }
        List<IBusInputSubscriber<IByte>> BusSubscribers { get; set; } 
    }
}