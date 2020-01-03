using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class BitFactory : IBitFactory
    {
        public IBit Create(bool initialState)
        {
            return new Bit(initialState);
        }

        public IList<IBit> Create(int size)
        {
            return Enumerable.Range(0, size).Select(s => Create(false)).ToList();
        }
    }
}