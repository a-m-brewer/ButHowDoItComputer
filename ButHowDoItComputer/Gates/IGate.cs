using System.Collections.Generic;

namespace ButHowDoItComputer.Gates
{
    public interface IGate
    {
        IBit Apply(IEnumerable<IBit> bits);
    }
}