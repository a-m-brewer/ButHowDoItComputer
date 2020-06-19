using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BitAdder : IBitAdder
    {
        private readonly IOr _or;
        private readonly IXOr _xOr;

        public BitAdder(IXOr xOr, IOr or)
        {
            _xOr = xOr;
            _or = or;
        }

        public (bool Sum, bool CarryOut) Add(bool a, bool b, bool carryIn)
        {
            var aXorB = _xOr.ApplyParams(a, b);
            var sum = _xOr.ApplyParams(aXorB, carryIn);

            var aAndB = a && b;
            var aXorBAndCarryIn = aXorB && carryIn;
            var carryOut = _or.ApplyParams(aAndB, aXorBAndCarryIn);

            return (sum, carryOut);
        }
    }
}