namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IGate
    {
        bool Apply(params bool[] bits);
    }
}