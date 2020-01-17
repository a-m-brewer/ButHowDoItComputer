using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IShifter : IRegisterTransfer
    {
        IBit ShiftIn { get; set; }
        IBit ShiftOut { get; set; }
    }

    public interface IRightShifter : IShifter { }
    public interface ILeftShifter : IShifter { }
}