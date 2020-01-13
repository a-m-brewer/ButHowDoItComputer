using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRegister
    {
        IBit Enable { get; set; }
        IBit Set { get; set; }
        IByte Byte { get; }
        IByte Input { get; set; }
        IByte Output { get; }
        
        IByte ApplyOnce(IByte input, bool enable = false);
        IByte Apply(IByte input);
        IByte Apply();
    }
}