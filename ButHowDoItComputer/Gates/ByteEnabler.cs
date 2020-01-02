using System.Collections.Generic;
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
        
        public IByte Apply(IByte input, IBit set)
        {
            var bits = new IBit[8];
            for (var i = 0; i < input.Count; i++)
            {
                bits[i] = _andGate.Apply(new List<IBit> {input[i], set});
            }

            return _byteFactory.Create(bits);
        }
    }
}