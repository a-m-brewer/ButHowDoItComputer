using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class And : IAnd
    {
        private readonly IBitFactory _bitFactory;

        public And(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
        }
        
        public IBit Apply(IEnumerable<IBit> bits)
        {
            return _bitFactory.Create(bits.All(a => a.State));
        }
    }
}