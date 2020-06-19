using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BitComparator : IBitComparator
    {
        private readonly INot _not;
        private readonly IOr _or;
        private readonly IXOr _xOr;

        public BitComparator(IXOr xOr, IOr or, INot not)
        {
            _xOr = xOr;
            _or = or;
            _not = not;
        }

        public (bool equal, bool ALarger, bool output) AreEqual(bool a, bool b, bool aboveBitIsEqual,
            bool aboveBitALarger)
        {
            var unequal = _xOr.ApplyParams(a, b);
            var equal = _not.Apply(unequal);
            var equalAndAboveBitEqual = equal && aboveBitIsEqual;
            var aLarger = aboveBitIsEqual && unequal && a;
            var aLargerOrAboveALarger = _or.ApplyParams(aLarger, aboveBitALarger);

            return (equalAndAboveBitEqual, aLargerOrAboveALarger, unequal);
        }
    }
}