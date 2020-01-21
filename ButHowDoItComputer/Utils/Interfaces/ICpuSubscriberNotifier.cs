namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface ICpuSubscriberNotifier<in TData>
    {
        void Update(TData newState);
        void Apply();
    }
}