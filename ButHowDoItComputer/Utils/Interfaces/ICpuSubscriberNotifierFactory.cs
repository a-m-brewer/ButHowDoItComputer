namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface ICpuSubscriberNotifierFactory<in TSub, in TData> where TSub : class
    {
        ICpuSubscriberNotifier<TData> Create(TSub sub);
    }
}