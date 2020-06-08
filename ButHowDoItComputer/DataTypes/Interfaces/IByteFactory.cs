namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IByteFactory
    {
        IByte Create(params bool[] bits);
        IByte Create(uint input);
    }
}