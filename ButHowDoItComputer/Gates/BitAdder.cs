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
            var aXorB = _xOr.Apply(a, b);
            var sum = _xOr.Apply(aXorB, carryIn);

            var aAndB = _and.Apply(a, b);
            var aXorBAndCarryIn = _and.Apply(aXorB, carryIn);
            var carryOut = _or.Apply(aAndB, aXorBAndCarryIn);

            return (sum, carryOut);
        }
    }
}