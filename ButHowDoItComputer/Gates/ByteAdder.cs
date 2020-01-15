using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteAdder : IByteAdder
    {
        private readonly IBitAdder _bitAdder;
        private readonly IByteFactory _byteFactory;

        public ByteAdder(IBitAdder bitAdder, IByteFactory byteFactory)
        {
            _bitAdder = bitAdder;
            _byteFactory = byteFactory;
        }

        public (IByte Sum, IBit CarryOut) Add(IByte a, IByte b, IBit carryIn)
        {
            var carryOut = carryIn;
            var output = new IBit[8];

            for (var i = 0; i < a.Count; i++)
            {
                var (sum, carry) = _bitAdder.Add(a[i], b[i], carryOut);
                output[i] = sum;
                carryOut = carry;
            }

            var outputByte = _byteFactory.Create(output);

            return (outputByte, carryOut);
        }
    }
}