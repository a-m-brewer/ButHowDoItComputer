using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IShifter : IRegisterTransfer
    {
        bool ShiftIn { get; set; }
        bool ShiftOut { get; set; }
    }

    public interface IRightShifter : IShifter { }
    public interface ILeftShifter : IShifter { }
}