using ButHowDoItComputer.DataTypes.Interfaces;
using System.Linq;

namespace ButHowDoItComputer.Parts
{
    public interface IWire
    {
        IByte Apply(params IByte[] input);
    }

    public class Wire : IWire
    {
        private readonly IByteFactory _byteFactory;

        public Wire(IByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
        }

        public IByte Apply(params IByte[] input)
        {
            return input.Where(w => w.Any(a => a.State)).LastOrDefault() ?? _byteFactory.Create(0);
        }
    }
}
