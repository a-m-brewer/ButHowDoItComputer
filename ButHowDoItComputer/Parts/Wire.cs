using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using System.Linq;

namespace ButHowDoItComputer.Parts
{

    public class Wire : IWire
    {
        private readonly IByteFactory _byteFactory;

        public Wire(IByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
        }

        public IByte Apply(params IByte[] input)
        {
            return input.LastOrDefault(w => w.Any(a => a)) ?? _byteFactory.Create(0);
        }
    }
}
