using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IRegister
    {
        IBit Enable { get; set; }
        IBit Set { get; set; }
        IByte Byte { get; }
        
        IByte ApplyOnce(IByte input, bool enable = false);
        IByte Apply(IByte input);
    }
}