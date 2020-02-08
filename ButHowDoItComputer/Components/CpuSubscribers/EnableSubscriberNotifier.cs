using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.CpuSubscribers
{
    public class EnableSubscriberNotifier : ICpuSubscriberNotifier<bool>
    {
        private readonly ICpuEnableSubscriber[] _cpuEnableSubscriber;

        public EnableSubscriberNotifier(params ICpuEnableSubscriber[] cpuEnableSubscriber)
        {
            _cpuEnableSubscriber = cpuEnableSubscriber;
        }
        
        public void Update(bool newState)
        {
            foreach (var cpuEnableSubscriber in _cpuEnableSubscriber)
            {
                cpuEnableSubscriber.Enable = newState;
            }
        }

        public void Apply()
        {
            foreach (var cpuEnableSubscriber in _cpuEnableSubscriber)
            {
                cpuEnableSubscriber.Apply();
            }
        }
    }
    
}