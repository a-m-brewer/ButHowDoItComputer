using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeAdder<TBusDataType> : IBusDataTypeAdder<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBitAdder _bitAdder;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeAdder(IBitAdder bitAdder, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _bitAdder = bitAdder;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public (TBusDataType Sum, bool CarryOut) Add(TBusDataType a, TBusDataType b, bool carryIn)
        {
            var carryOut = carryIn;
            var output = new bool[a.Count];

            for (var i = 0; i < a.Count; i++)
            {
                var (sum, carry) = _bitAdder.Add(a[i], b[i], carryOut);
                output[i] = sum;
                carryOut = carry;
            }

            var outputByte = _busDataTypeFactory.CreateParams(output);

            return (outputByte, carryOut);
        }
    }
}