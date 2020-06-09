using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteComparator<TBusDataType> : IByteComparator<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBitComparator _bitComparator;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public ByteComparator(IBitComparator bitComparator, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _bitComparator = bitComparator;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public (bool equal, bool ALarger, TBusDataType output) AreEqual(TBusDataType a, TBusDataType b, bool aboveBitIsEqual,
            bool aboveBitALarger)
        {
            var output = new bool[8];
            for (var i = a.Count - 1; i >= 0; i--)
            {
                var (eq, lg, op) = _bitComparator.AreEqual(a[i], b[i], aboveBitIsEqual, aboveBitALarger);
                aboveBitIsEqual = eq;
                aboveBitALarger = lg;
                output[i] = op;
            }

            return (aboveBitIsEqual, aboveBitALarger, _busDataTypeFactory.Create(output));
        }
    }
}