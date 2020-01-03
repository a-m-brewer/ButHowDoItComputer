using System.Collections.Generic;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IBitFactory
    {
        IBit Create(bool initialState);
        IList<IBit> Create(int size);
    }
}