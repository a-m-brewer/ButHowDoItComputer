using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class BitFactory : IBitFactory
    {
        public IBit Create(bool initialState)
        {
            return new Bit(initialState);
        }
    }
}