namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface ICpuSubscriberNotifier<in TData> : IApplicable
    {
        public void Update(TData newState);
    }
}