using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class AluWire : IAluWire
    {
        private readonly IByteFactory _byteFactory;

        public AluWire(IByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
        }

        public IByte Apply(params IByte[] input)
        {
            return input.LastOrDefault(w => w.Any(a => a)) ?? _byteFactory.Create(0);
        }
    }
}