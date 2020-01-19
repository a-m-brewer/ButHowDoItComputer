﻿using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using System.Linq;

namespace ButHowDoItComputer.Gates
{
    public class Bus1 : IBus1
    {
        private readonly IAnd _and;
        private readonly INot _not;
        private readonly IOr _or;
        private readonly IByteFactory _byteFactory;

        public Bus1(IAnd and, INot not, IOr or, IByteFactory byteFactory)
        {
            _and = and;
            _not = not;
            _or = or;
            _byteFactory = byteFactory;
        }

        public IByte Apply(IByte input, IBit bus1)
        {
            var one = input[0];
            var rest = input.Skip(1).ToArray();
            var notBus1 = _not.Apply(bus1);
            var notAndRest = rest.Select(s => _and.Apply(s, notBus1)).ToList();
            var orOneAndBus1 = _or.Apply(one, bus1);

            var output = notAndRest.Prepend(orOneAndBus1).ToArray();

            return _byteFactory.Create(output);
        }
    }
}