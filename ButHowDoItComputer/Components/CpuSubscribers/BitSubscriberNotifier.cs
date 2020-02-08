using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.CpuSubscribers
{
    public class BitSubscriberNotifier : ICpuSubscriberNotifier<bool>
    {

        public bool Bit { get; set; }
        
        public void Update(bool newState)
        {
            Bit = newState;
        }

        public void Apply()
        {
        }
    }
}