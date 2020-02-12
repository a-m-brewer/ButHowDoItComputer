namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface ICpuInput<out T>
    {
        T Output { get; }
    }
}