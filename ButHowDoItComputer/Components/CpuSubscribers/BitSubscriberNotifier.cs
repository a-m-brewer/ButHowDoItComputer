using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.CpuSubscribers
{
    public class BitSubscriberNotifier : ICpuSubscriberNotifier<IBit>
    {

        public IBit Bit { get; set; }
        
        public void Update(IBit newState)
        {
            Bit = newState;
        }

        public void Apply()
        {
        }
    }
}