using System;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IByteFactory
    {
        IByte Create(IBit[] bits);
        IByte Create();
    }
}