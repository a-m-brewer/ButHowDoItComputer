using System;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Bus1 : IBus1
    {
        private readonly IAnd _and;
        private readonly IByteFactory _byteFactory;
        private readonly Action<IByte> _updateWire;
        private readonly INot _not;
        private readonly IOr _or;

        public Bus1(IAnd and, INot not, IOr or, IByteFactory byteFactory, Action<IByte> updateWire)
        {
            _and = and;
            _not = not;
            _or = or;
            _byteFactory = byteFactory;
            _updateWire = updateWire;
            Input = _byteFactory.Create(0);
            Output = _byteFactory.Create(0);
        }

        public IByte Apply(IByte input, bool bus1)
        {
            Input = input;
            Set = bus1;
            var one = input[0];
            var rest = input.Skip(1).ToArray();
            var notBus1 = _not.Apply(bus1);
            var notAndRest = rest.Select(s => _and.Apply(s, notBus1)).ToList();
            var orOneAndBus1 = _or.Apply(one, bus1);

            var output = notAndRest.Prepend(orOneAndBus1).ToArray();

            Output = _byteFactory.Create(output);
            _updateWire(Output);
            return Output;
        }

        public void Apply()
        {
            Apply(Input, Set);
        }

        public IByte Input { get; set; }
        public bool Set { get; set; }
        public IByte Output { get; set; }
    }
}