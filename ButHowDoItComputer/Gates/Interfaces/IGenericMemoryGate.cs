using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IGenericMemoryGate<TData>
    {
        TData Apply(TData input, IBit set);
    }
}