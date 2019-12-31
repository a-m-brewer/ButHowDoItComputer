using System.Collections.Generic;
using System.Linq;

namespace ButHowDoItComputer.Gates
{
    public class And : IGate
    {
        public IBit Apply(IEnumerable<IBit> bits)
        {
            return new Bit(bits.All(a => a.State));
        }
    }
}