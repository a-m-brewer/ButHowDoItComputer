using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class IsZeroGate<TBusDataType> : IIsZeroGate<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly INot _not;
        private readonly IOr _or;

        public IsZeroGate(IOr or, INot not)
        {
            _or = or;
            _not = not;
        }

        public bool IsZero(TBusDataType input)
        {
            var orRes = _or.ApplyParams(input.ToArray());

            return _not.Apply(orRes);
        }
    }
}