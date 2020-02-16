using System;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class ArithmeticLogicUnit : IArithmeticLogicUnit
    {
        private readonly IAluWire _aluWire;
        private readonly IAnd _and;
        private readonly IByteAdder _byteAdder;
        private readonly IByteAnd _byteAnd;
        private readonly IByteComparator _byteComparator;
        private readonly Action<Caez> _updateFlags;
        private readonly Action<IByte> _updateAcc;
        private readonly IByteDecoder _byteDecoder;
        private readonly IByteEnabler _byteEnabler;
        private readonly IByteOr _byteOr;
        private readonly IByteXOr _byteXOr;
        private readonly IInverter _inverter;
        private readonly IIsZeroGate _isZeroGate;
        private readonly ILeftByteShifter _leftByteShifter;
        private readonly IOr _or;
        private readonly IRightByteShifter _rightByteShifter;

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
            IAluWire aluWire,
            IByteComparator byteComparator,
            Action<Caez> updateFlags,
            Action<IByte> updateAcc,
            IByteFactory byteFactory)
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
            _aluWire = aluWire;
            _byteComparator = byteComparator;
            _updateFlags = updateFlags;
            _updateAcc = updateAcc;

            InputA = byteFactory.Create();
            InputB = byteFactory.Create();
            Op = new Op();
        }

        public AluOutput Apply(IByte a, IByte b, bool carryIn, Op op)
        {
            var opDecoder = _byteDecoder.Decode(op.One, op.Two, op.Three);

            var xOr = _byteXOr.Apply(a, b);
            var or = _byteOr.Apply(a, b);
            var and = _byteAnd.Apply(a, b);
            var not = _inverter.Invert(a);
            var shiftLeft = _leftByteShifter.Shift(a, carryIn);
            var shiftRight = _rightByteShifter.Shift(a, carryIn);
            var adder = _byteAdder.Add(a, b, carryIn);
            var comparatorResult = _byteComparator.AreEqual(a, b, true, false);

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

            var output = _aluWire.Apply(enabledAdd, enabledShiftLeft, enabledShiftRight, enabledNot, enabledAnd,
                enabledOr, enabledXOr, enabledComparator);
            var zero = _isZeroGate.IsZero(output);

            var aluOutput = new AluOutput
            {
                ALarger = comparatorResult.ALarger,
                CarryOut = carryOut,
                Equal = comparatorResult.equal,
                Output = output,
                Zero = zero
            };
            
            _updateFlags(new Caez {C = aluOutput.CarryOut, A = aluOutput.ALarger, E = aluOutput.Equal, Z = aluOutput.Zero});
            _updateAcc(aluOutput.Output);
            return aluOutput;
        }

        public Op Op { get; set; }
        public IByte InputA { get; set; }
        public IByte InputB { get; set; }
        public bool CarryIn { get; set; }

        public void Apply()
        {
            Apply(InputA, InputB, CarryIn, Op); ;
        }
    }
}