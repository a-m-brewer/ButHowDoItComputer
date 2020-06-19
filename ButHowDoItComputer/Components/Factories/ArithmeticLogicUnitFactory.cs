using System;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
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
            return Create(b => {}, caez => {});
        }

        public IArithmeticLogicUnit<TBusDataType> Create(Action<TBusDataType> updateAcc, Action<Caez> updateFlags)
        {
            var not = new Not();
            var nAnd = new NAnd(not);
            var or = new Or(not, nAnd);
            var xOr = new XOr(not, nAnd);
            return new ArithmeticLogicUnit<TBusDataType>(
                new BusDataTypeXOr<TBusDataType>(xOr, _busDataTypeFactory),
                new BusDataTypeOr<TBusDataType>(or, _busDataTypeFactory),
                new BusDataTypeAnd<TBusDataType>(_busDataTypeFactory),
                new Inverter<TBusDataType>(not, _busDataTypeFactory),
                new BusDataTypeAdder<TBusDataType>(new BitAdder(xOr, or), _busDataTypeFactory),
                new BusDataTypeEnabler<TBusDataType>(_busDataTypeFactory),
                new IsZeroGate<TBusDataType>(or, not),
                new BusDataTypeDecoder<TBusDataType>(new Decoder(not, new Base10Converter()), _busDataTypeFactory),
                new BusDataTypeRightShifter<TBusDataType>(_busDataTypeFactory),
                new BusDataTypeLeftShifter<TBusDataType>(_busDataTypeFactory),
                or,
                new AluWire<TBusDataType>(_busDataTypeFactory),
                new BusDataTypeComparator<TBusDataType>(new BitComparator(xOr, or, not), _busDataTypeFactory), updateFlags,
                updateAcc, _busDataTypeFactory);
        }
    }

    public interface IArithmeticLogicUnitFactory<TBusDataType> where TBusDataType : IBusDataType
    {
        IArithmeticLogicUnit<TBusDataType> Create(Action<TBusDataType> updateAcc, Action<Caez> updateFlags);
        IArithmeticLogicUnit<TBusDataType> Create();
    }
}