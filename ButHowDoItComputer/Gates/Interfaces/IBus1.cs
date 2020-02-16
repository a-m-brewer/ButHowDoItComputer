using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBus1 : IByteGate, ISettable, IApplicable, IOutputable<IByte>, IInputable<IByte>
    {
    }
}