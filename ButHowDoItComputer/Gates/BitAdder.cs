using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BitAdder : IBitAdder
    {
        private readonly IXOr _xOr;
        private readonly IOr _or;
        private readonly IAnd _and;

        public BitAdder(IXOr xOr, IOr or, IAnd and)
        {
            _xOr = xOr;
            _or = or;
            _and = and;
        }
        
        public (bool Sum, bool CarryOut) Add(bool a, bool b, bool carryIn)
        {
            var aXorB = _xOr.Apply(new[] {a, b});
            var sum = _xOr.Apply(new[] {aXorB, carryIn});

            var aAndB = _and.Apply(new[] {a, b});
            var aXorBAndCarryIn = _and.Apply(new[] {aXorB, carryIn});
            var carryOut = _or.Apply(new[] {aAndB, aXorBAndCarryIn});

            return (sum, carryOut);
        }
    }
}