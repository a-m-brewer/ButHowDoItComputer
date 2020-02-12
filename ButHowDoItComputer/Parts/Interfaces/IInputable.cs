namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IInputable<in T>
    {
        void UpdateInput(T input);
    }
}