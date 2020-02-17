namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBitAdder
    {
        (bool Sum, bool CarryOut) Add(bool a, bool b, bool carryIn);
    }
}