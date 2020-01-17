using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using System.Linq;

namespace ButHowDoItComputer.Gates
{
    public class ByteDecoder : IByteDecoder
    {
        private readonly IDecoder _decoder;
        private readonly IByteFactory _byteFactory;

        public ByteDecoder(IDecoder decoder, IByteFactory byteFactory)
        {
            _decoder = decoder;
            _byteFactory = byteFactory;
        }

        public IByte Decode(IBit a, IBit b, IBit c)
        {
            return _byteFactory.Create(_decoder.Apply(a, b, c).ToArray());
        }
    }
}
