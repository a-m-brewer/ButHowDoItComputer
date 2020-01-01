namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IObjectCreationFactory<out T>
    {
        T Create();
    }
}