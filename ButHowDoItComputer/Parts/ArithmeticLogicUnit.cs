using System;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class ArithmeticLogicUnit<TBusDataType> : IArithmeticLogicUnit<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IAluWire<TBusDataType> _aluWire;
        private readonly IBusDataTypeAdder<TBusDataType> _busDataTypeAdder;
        private readonly IBusDataTypeAnd<TBusDataType> _busDataTypeAnd;
        private readonly IBusDataTypeComparator<TBusDataType> _busDataTypeComparator;
        private readonly Action<Caez> _updateFlags;
        private readonly Action<TBusDataType> _updateAcc;
        private readonly IBusDataTypeDecoder<TBusDataType> _busDataTypeDecoder;
        private readonly IBusDataTypeEnabler<TBusDataType> _busDataTypeEnabler;
        private readonly IBusDataTypeOr<TBusDataType> _busDataTypeOr;
        private readonly IBusDataTypeXOr<TBusDataType> _busDataTypeXOr;
        private readonly IInverter<TBusDataType> _inverter;
        private readonly IIsZeroGate<TBusDataType> _isZeroGate;
        private readonly ILeftBusDataTypeShifter<TBusDataType> _leftBusDataTypeShifter;
        private readonly IOr _or;
        private readonly IRightBusDataTypeShifter<TBusDataType> _rightBusDataTypeShifter;

        public ArithmeticLogicUnit(
            IBusDataTypeXOr<TBusDataType> busDataTypeXOr,
            IBusDataTypeOr<TBusDataType> busDataTypeOr,
            IBusDataTypeAnd<TBusDataType> busDataTypeAnd,
            IInverter<TBusDataType> inverter,
            IBusDataTypeAdder<TBusDataType> busDataTypeAdder,
            IBusDataTypeEnabler<TBusDataType> busDataTypeEnabler,
            IIsZeroGate<TBusDataType> isZeroGate,
            IBusDataTypeDecoder<TBusDataType> busDataTypeDecoder,
            IRightBusDataTypeShifter<TBusDataType> rightBusDataTypeShifter,
            ILeftBusDataTypeShifter<TBusDataType> leftBusDataTypeShifter,
            IOr or,
            IAluWire<TBusDataType> aluWire,
            IBusDataTypeComparator<TBusDataType> busDataTypeComparator,
            Action<Caez> updateFlags,
            Action<TBusDataType> updateAcc,
            IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeXOr = busDataTypeXOr;
            _busDataTypeOr = busDataTypeOr;
            _busDataTypeAnd = busDataTypeAnd;
            _inverter = inverter;
            _busDataTypeAdder = busDataTypeAdder;
            _busDataTypeEnabler = busDataTypeEnabler;
            _isZeroGate = isZeroGate;
            _busDataTypeDecoder = busDataTypeDecoder;
            _rightBusDataTypeShifter = rightBusDataTypeShifter;
            _leftBusDataTypeShifter = leftBusDataTypeShifter;
            _or = or;
            _aluWire = aluWire;
            _busDataTypeComparator = busDataTypeComparator;
            _updateFlags = updateFlags;
            _updateAcc = updateAcc;

            InputA = busDataTypeFactory.Create();
            InputB = busDataTypeFactory.Create();
            Op = new Op();
        }

        public AluOutput<TBusDataType> Apply(TBusDataType a, TBusDataType b, bool carryIn, Op op)
        {
            var opDecoder = _busDataTypeDecoder.Decode(op.One, op.Two, op.Three);

            var xOr = _busDataTypeXOr.Apply(a, b);
            var or = _busDataTypeOr.Apply(a, b);
            var and = _busDataTypeAnd.Apply(a, b);
            var not = _inverter.Invert(a);
            var shiftLeft = _leftBusDataTypeShifter.Shift(a, carryIn);
            var shiftRight = _rightBusDataTypeShifter.Shift(a, carryIn);
            var adder = _busDataTypeAdder.Add(a, b, carryIn);
            var comparatorResult = _busDataTypeComparator.AreEqual(a, b, true, false);

            var enabledAdd = _busDataTypeEnabler.Apply(adder.Sum, opDecoder[0]);
            var enabledShiftRight = _busDataTypeEnabler.Apply(shiftRight.Ouput, opDecoder[1]);
            var enabledShiftLeft = _busDataTypeEnabler.Apply(shiftLeft.Ouput, opDecoder[2]);
            var enabledNot = _busDataTypeEnabler.Apply(not, opDecoder[3]);
            var enabledAnd = _busDataTypeEnabler.Apply(and, opDecoder[4]);
            var enabledOr = _busDataTypeEnabler.Apply(or, opDecoder[5]);
            var enabledXOr = _busDataTypeEnabler.Apply(xOr, opDecoder[6]);
            var enabledComparator = _busDataTypeEnabler.Apply(comparatorResult.output, opDecoder[7]);

            var carryOutAdd = adder.CarryOut && opDecoder[0];
            var carryOutShiftRight = shiftRight.ShiftOut && opDecoder[1];
            var carryOutShiftLeft = shiftLeft.ShiftOut && opDecoder[2];
            var carryOut = _or.ApplyParams(carryOutAdd, carryOutShiftRight, carryOutShiftLeft);

            var output = _aluWire.Apply(enabledAdd, enabledShiftLeft, enabledShiftRight, enabledNot, enabledAnd,
                enabledOr, enabledXOr, enabledComparator);
            var zero = _isZeroGate.IsZero(output);

            var aluOutput = new AluOutput<TBusDataType>
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
        public TBusDataType InputA { get; set; }
        public TBusDataType InputB { get; set; }
        public bool CarryIn { get; set; }

        public void Apply()
        {
            Apply(InputA, InputB, CarryIn, Op); ;
        }
    }
}