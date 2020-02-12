using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Bus1 : IBus1
    {
        private readonly IAnd _and;
        private readonly IByteFactory _byteFactory;
        private readonly INot _not;
        private readonly IOr _or;

        public Bus1(IAnd and, INot not, IOr or, IByteFactory byteFactory)
        {
            _and = and;
            _not = not;
            _or = or;
            _byteFactory = byteFactory;
            Input = _byteFactory.Create(0);
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

            return _byteFactory.Create(output);
        }

        public void Apply()
        {
            var result = Apply(Input, Set);
            foreach (var subscriber in BusSubscribers) subscriber.Input = result;
        }

        public IByte Input { get; set; }
        public List<IBusInputSubscriber<IByte>> BusSubscribers { get; } = new List<IBusInputSubscriber<IByte>>();
        public bool Set { get; set; }
    }
}