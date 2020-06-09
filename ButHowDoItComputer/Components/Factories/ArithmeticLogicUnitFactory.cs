using System;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Components.Factories
{
    public class ArithmeticLogicUnitFactory<TBusDataType> : IArithmeticLogicUnitFactory<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public ArithmeticLogicUnitFactory(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
        }
        
        public IArithmeticLogicUnit<TBusDataType> Create()
        {
            return Create<TBusDataType>(b => {}, caez => {});
        }

        public IArithmeticLogicUnit<TBusDataType> Create(Action<TBusDataType> updateAcc, Action<Caez> updateFlags)
        {
            var not = new Not();
            var and = new And();
            var nAnd = new NAnd(not, and);
            var or = new Or(not, nAnd);
            var xOr = new XOr(not, nAnd);
            return new ArithmeticLogicUnit<TBusDataType>(
                new BusDataTypeXOr<TBusDataType>(xOr, byteFactory),
                new BusDataTypeOr<TBusDataType>(or, byteFactory),
                new BusDataTypeAnd<TBusDataType>(and, byteFactory),
                new Inverter(not, byteFactory),
                new ByteAdder(new BitAdder(xOr, or, and), byteFactory),
                new ByteEnabler(and, byteFactory),
                and,
                new IsZeroGate(or, not),
                new ByteDecoder(new Decoder(not, and), byteFactory),
                new ByteRightShifter(byteFactory),
                new ByteLeftShifter(byteFactory),
                or,
                new AluWire(byteFactory),
                new ByteComparator<TBusDataType>(new BitComparator(xOr, and, or, not), _busDataTypeFactory), updateFlags,
                updateAcc, _busDataTypeFactory);
        }
    }

    public interface IArithmeticLogicUnitFactory<TBusDataType> : IObjectCreationFactory<TBusDataType> where TBusDataType : IBusDataType
    {
        IArithmeticLogicUnit<TBusDataType> Create(Action<TBusDataType> updateAcc, Action<Caez> updateFlags);
    }
}