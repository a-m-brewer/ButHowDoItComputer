namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IObjectCreationFactory<T>
    {
        T Create();
    }
}