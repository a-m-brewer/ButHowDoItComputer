namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface ICpuSubscriberNotifier<in TData>
    {
        public void Update(TData newState);
        public void Apply();
    }
}