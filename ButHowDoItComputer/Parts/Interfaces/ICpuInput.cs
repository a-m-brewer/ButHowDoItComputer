using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface ICpuInput<out T>
    {
        T Output { get; }
    }
}