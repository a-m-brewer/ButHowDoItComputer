using System;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IByteFactory
    {
        IByte Create(params IBit[] bits);
        IByte Create();
        IByte Create(uint input);
    }
}