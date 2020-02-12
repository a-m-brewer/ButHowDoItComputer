using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteComparator : IByteComparator
    {
        private readonly IBitComparator _bitComparator;
        private readonly IByteFactory _byteFactory;

        public ByteComparator(IBitComparator bitComparator, IByteFactory byteFactory)
        {
            _bitComparator = bitComparator;
            _byteFactory = byteFactory;
        }

        public (bool equal, bool ALarger, IByte output) AreEqual(IByte a, IByte b, bool aboveBitIsEqual,
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

            return (aboveBitIsEqual, aboveBitALarger, _byteFactory.Create(output));
        }
    }
}