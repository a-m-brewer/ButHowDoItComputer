using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BitAdder : IBitAdder
    {
        private readonly IAnd _and;
        private readonly IOr _or;
        private readonly IXOr _xOr;

        public BitAdder(IXOr xOr, IOr or, IAnd and)
        {
            _xOr = xOr;
            _or = or;
            _and = and;
        }

        public (bool Sum, bool CarryOut) Add(bool a, bool b, bool carryIn)
        {
            var aXorB = _xOr.ApplyParams(a, b);
            var sum = _xOr.ApplyParams(aXorB, carryIn);

            var aAndB = _and.ApplyParams(a, b);
            var aXorBAndCarryIn = _and.ApplyParams(aXorB, carryIn);
            var carryOut = _or.ApplyParams(aAndB, aXorBAndCarryIn);

            return (sum, carryOut);
        }
    }
}