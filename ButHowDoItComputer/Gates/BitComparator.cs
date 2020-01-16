using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BitComparator : IBitComparator
    {
        private readonly IXOr _xOr;
        private readonly IAnd _and;
        private readonly IOr _or;
        private readonly INot _not;

        public BitComparator(IXOr xOr, IAnd and, IOr or, INot not)
        {
            _xOr = xOr;
            _and = and;
            _or = or;
            _not = not;
        }
        
        public (IBit equal, IBit ALarger, IBit output) AreEqual(IBit a, IBit b, IBit aboveBitIsEqual, IBit aboveBitALarger)
        {
            var one = _xOr.Apply(a, b);
            var two = _not.Apply(one);
            var three = _and.Apply(two, aboveBitIsEqual);
            var four = _and.Apply(aboveBitIsEqual, one, a);
            var five = _or.Apply(four, aboveBitALarger);

            return (three, five, one);
        }
    }
}