using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Components
{
    public class ArithmeticLogicUnit : IArithmeticLogicUnit
    {
        private readonly IByteXOr _byteXOr;
        private readonly IByteOr _byteOr;
        private readonly IByteAnd _byteAnd;
        private readonly IInverter _inverter;
        private readonly IByteAdder _byteAdder;
        private readonly IByteEnabler _byteEnabler;
        private readonly IAnd _and;
        private readonly IIsZeroGate _isZeroGate;
        private readonly IByteDecoder _byteDecoder;
        private readonly IRightByteShifter _rightByteShifter;
        private readonly ILeftByteShifter _leftByteShifter;
        private readonly IOr _or;
        private readonly IWire _wire;
        private readonly IByteComparator _byteComparator;

        public ArithmeticLogicUnit(
            IByteXOr byteXOr,
            IByteOr byteOr,
            IByteAnd byteAnd,
            IInverter inverter,
            IByteAdder byteAdder,
            IByteEnabler byteEnabler,
            IAnd and,
            IIsZeroGate isZeroGate,
            IByteDecoder byteDecoder,
            IRightByteShifter rightByteShifter,
            ILeftByteShifter leftByteShifter,
            IOr or,
            IWire wire,
            IByteComparator byteComparator)
        {
            _byteXOr = byteXOr;
            _byteOr = byteOr;
            _byteAnd = byteAnd;
            _inverter = inverter;
            _byteAdder = byteAdder;
            _byteEnabler = byteEnabler;
            _and = and;
            _isZeroGate = isZeroGate;
            _byteDecoder = byteDecoder;
            _rightByteShifter = rightByteShifter;
            _leftByteShifter = leftByteShifter;
            _or = or;
            _wire = wire;
            _byteComparator = byteComparator;
        }

        public AluOutput Apply(IByte a, IByte b, IBit carryIn, Op op)
        {
            var opDecoder = _byteDecoder.Decode(op.One, op.Two, op.Three);

            var xOr = _byteXOr.Apply(a, b);
            var or = _byteOr.Apply(a, b);
            var and = _byteAnd.Apply(a, b);
            var not = _inverter.Invert(a);
            var shiftLeft = _leftByteShifter.Shift(a, carryIn);
            var shiftRight = _rightByteShifter.Shift(a, carryIn);
            var adder = _byteAdder.Add(a, b, carryIn);
            var comparatorResult = _byteComparator.AreEqual(a, b, true.ToBit(), false.ToBit());

            var enabledAdd = _byteEnabler.Apply(adder.Sum, opDecoder[0]);
            var enabledShiftRight = _byteEnabler.Apply(shiftRight.Ouput, opDecoder[1]);
            var enabledShiftLeft = _byteEnabler.Apply(shiftLeft.Ouput, opDecoder[2]);
            var enabledNot = _byteEnabler.Apply(not, opDecoder[3]);
            var enabledAnd = _byteEnabler.Apply(and, opDecoder[4]);
            var enabledOr = _byteEnabler.Apply(or, opDecoder[5]);
            var enabledXOr = _byteEnabler.Apply(xOr, opDecoder[6]);
            var enabledComparator = _byteEnabler.Apply(comparatorResult.output, opDecoder[7]);

            var carryOutAdd = _and.Apply(adder.CarryOut, opDecoder[0]);
            var carryOutShiftRight = _and.Apply(shiftRight.ShiftOut, opDecoder[1]);
            var carryOutShiftLeft = _and.Apply(shiftLeft.ShiftOut, opDecoder[2]);
            var carryOut = _or.Apply(carryOutAdd, carryOutShiftRight, carryOutShiftLeft);

            var output = _wire.Apply(enabledAdd, enabledShiftLeft, enabledShiftRight, enabledNot, enabledAnd, enabledOr, enabledXOr, enabledComparator);
            var zero = _isZeroGate.IsZero(output);

            return new AluOutput
            {
                ALarger = comparatorResult.ALarger,
                CarryOut = carryOut,
                Equal = comparatorResult.equal,
                Output = output,
                Zero = zero
            };
        }
    }
}
