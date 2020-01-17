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
                bits[i] = _andGate.Apply(input[i], set);
            }

            return _byteFactory.Create(bits);
        }
    }

    public class ByteEnablerListGateAdapter : IByteEnablerListGateAdapter
    {
        private readonly IByteEnabler _byteEnabler;
        private readonly IBit _set;

        public ByteEnablerListGateAdapter(IByteEnabler byteEnabler, IBit set)
        {
            _byteEnabler = byteEnabler;
            _set = set;
        }

        public IByte Apply(IList<IByte> input)
        {
            return _byteEnabler.Apply(input[0], _set);
        }
    }

    public interface IByteEnablerListGateAdapter : IByteListGate { }

    public interface IByteEnablerListGateFactory
    {
        IByteEnablerListGateAdapter Create(IBit set);
    }

    public class ByteEnablerListGateFactory : IByteEnablerListGateFactory
    {
        private readonly IByteEnabler _byteEnabler;

        public ByteEnablerListGateFactory(IByteEnabler byteEnabler)
        {
            _byteEnabler = byteEnabler;
        }

        public IByteEnablerListGateAdapter Create(IBit set)
        {
            return new ByteEnablerListGateAdapter(_byteEnabler, set);
        }
    }
}