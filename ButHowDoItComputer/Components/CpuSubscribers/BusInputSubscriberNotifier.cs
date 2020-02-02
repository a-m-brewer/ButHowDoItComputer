using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components.CpuSubscribers
{
    public class BusInputSubscriberNotifier<T> : ICpuSubscriberNotifier<T>
    {
        private readonly IBusInputSubscriber<T>[] _subs;

        private BusInputSubscriberNotifier(IBusInputSubscriber<T>[] subs)
        {
            _subs = subs;
        }

        public static BusInputSubscriberNotifier<T> Create(params IBusInputSubscriber<T>[] subs)
        {
            return new BusInputSubscriberNotifier<T>(subs);
        }
        
        public void Update(T newState)
        {
            foreach (var sub in _subs)
            {
                sub.Input = newState;
            }
        }

        public void Apply()
        {
            foreach (var sub in _subs)
            {
                sub.Apply();
            }
        }
    }
}