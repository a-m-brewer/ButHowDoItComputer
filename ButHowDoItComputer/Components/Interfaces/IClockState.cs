namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IClockState
    {
        bool Bit { get; }
        bool Cycle();
    }
}