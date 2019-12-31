using System.Collections.Generic;

namespace ButHowDoItComputer.Gates
{
    public class NAnd : IGate
    {
        public IBit Apply(IEnumerable<IBit> bits)
        {
            return new Bit(new Not().Apply(new And().Apply(bits)).State);
        }
    }
}