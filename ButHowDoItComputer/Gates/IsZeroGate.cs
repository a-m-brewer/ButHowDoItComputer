using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class IsZeroGate : IIsZeroGate
    {
        private readonly IOr _or;
        private readonly INot _not;

        public IsZeroGate(IOr or, INot not)
        {
            _or = or;
            _not = not;
        }
        
        public IBit IsZero(IByte input)
        {
            var orRes = _or.Apply(input.ToArray());

            return _not.Apply(orRes);
        }
    }

    public interface IIsZeroGate
    {
        IBit IsZero(IByte input);
    }
}