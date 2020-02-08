using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.CpuSubscribers
{
    public class SetSubscriberNotifier : ICpuSubscriberNotifier<bool>
    {
        private readonly ICpuSettableSubscriber[] _cpuSettableSubscriber;

        public SetSubscriberNotifier(params ICpuSettableSubscriber[] cpuSettableSubscriber)
        {
            _cpuSettableSubscriber = cpuSettableSubscriber;
        }
        
        public void Update(bool newState)
        {
            foreach (var cpuSettableSubscriber in _cpuSettableSubscriber)
            {
                cpuSettableSubscriber.Set = newState;
            }
        }

        public void Apply()
        {
            foreach (var cpuSettableSubscriber in _cpuSettableSubscriber)
            {
                cpuSettableSubscriber.Apply();
            }
        }
    }
}