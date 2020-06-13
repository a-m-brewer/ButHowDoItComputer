using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IGate
    {
        bool ApplyParams(params bool[] bits);
        bool Apply(IList<bool> bits);
    }
}