using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteEnabler : IByteEnabler
    {
        private readonly IAnd _andGate;
        private readonly IByteFactory _byteFactory;

        public ByteEnabler(IAnd andGate, IByteFactory byteFactory)
        {
            _andGate = andGate;
            _byteFactory = byteFactory;
        }

        public IByte Apply(IByte input, bool set)
        {
            var bits = new bool[8];
            for (var i = 0; i < input.Count; i++) bits[i] = _andGate.Apply(input[i], set);

            return _byteFactory.Create(bits);
        }
    }
}