using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IRegister
    {
        IBit Enable { get; set; }
        IBit Set { get; set; }
        IByte Byte { get; }
        
        IByte Apply(IByte input, IBit set, IBit enable);
        IByte Apply(IByte input);
    }
}