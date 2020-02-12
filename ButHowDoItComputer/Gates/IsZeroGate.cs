using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class IsZeroGate : IIsZeroGate
    {
        private readonly INot _not;
        private readonly IOr _or;

        public IsZeroGate(IOr or, INot not)
        {
            _or = or;
            _not = not;
        }

        public bool IsZero(IByte input)
        {
            var orRes = _or.Apply(input.ToArray());

            return _not.Apply(orRes);
        }
    }
}