using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.CpuSubscribers
{
    public class SetSubscriberNotifier : ICpuSubscriberNotifier<IBit>
    {
        private readonly ICpuSettableSubscriber[] _cpuSettableSubscriber;

        public SetSubscriberNotifier(params ICpuSettableSubscriber[] cpuSettableSubscriber)
        {
            _cpuSettableSubscriber = cpuSettableSubscriber;
        }
        
        public void Update(IBit newState)
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