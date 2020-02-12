using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteDecoder : IByteDecoder
    {
        private readonly IByteFactory _byteFactory;
        private readonly IDecoder _decoder;

        public ByteDecoder(IDecoder decoder, IByteFactory byteFactory)
        {
            _decoder = decoder;
            _byteFactory = byteFactory;
        }

        public IByte Decode(bool a, bool b, bool c)
        {
            return _byteFactory.Create(_decoder.Apply(a, b, c).ToArray());
        }
    }
}