using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Inverter : IInverter
    {
        private readonly INot _not;
        private readonly IByteFactory _byteFactory;

        public Inverter(INot not, IByteFactory byteFactory)
        {
            _not = not;
            _byteFactory = byteFactory;
        }
        
        public IByte Invert(IByte input)
        {
            return _byteFactory.Create(input.Select(s => _not.Apply(s)).ToArray());
        }
    }
}