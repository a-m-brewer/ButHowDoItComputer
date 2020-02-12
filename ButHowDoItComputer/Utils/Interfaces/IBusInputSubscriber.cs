namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBusInputSubscriber<T> : IApplicable
    {
        T Input { get; set; }
    }
}