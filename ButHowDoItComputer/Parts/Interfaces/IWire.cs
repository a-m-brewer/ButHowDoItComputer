namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IWire<in T>
    {
        void Update(T input);
    }
}