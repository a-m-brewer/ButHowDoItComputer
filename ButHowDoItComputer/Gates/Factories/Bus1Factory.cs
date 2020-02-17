using System;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;

namespace ButHowDoItComputer.Gates.Factories
{
    public interface IBus1Factory
    {
        IBus1 Create(Action<IByte> updateWireAction);
    }

    public class Bus1Factory : IBus1Factory
    {
        private readonly IAnd _and;
        private readonly INot _not;
        private readonly IOr _or;
        private readonly IByteFactory _byteFactory;

        public Bus1Factory(IAnd and, INot not, IOr or, IByteFactory byteFactory)
        {
            _and = and;
            _not = not;
            _or = or;
            _byteFactory = byteFactory;
        }

        public IBus1 Create(Action<IByte> updateWireAction)
        {
            return new Bus1(_and, _not, _or, _byteFactory, updateWireAction);
        }
    }
}