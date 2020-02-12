namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IByteFactory
    {
        IByte Create(params bool[] bits);
        IByte Create();
        IByte Create(uint input);
    }
}