using System;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Bus1<TBusDataType> : IBus1<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IAnd _and;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly Action<TBusDataType> _updateWire;
        private readonly INot _not;
        private readonly IOr _or;

        public Bus1(IAnd and, INot not, IOr or, IBusDataTypeFactory<TBusDataType> busDataTypeFactory, Action<TBusDataType> updateWire)
        {
            _and = and;
            _not = not;
            _or = or;
            _busDataTypeFactory = busDataTypeFactory;
            _updateWire = updateWire;
            Input = _busDataTypeFactory.Create();
            Output = _busDataTypeFactory.Create();
        }

        public TBusDataType Apply(TBusDataType input, bool bus1)
        {
            Input = input;
            Set = bus1;
            var one = input[0];
            var rest = input.Skip(1).ToArray();
            var notBus1 = _not.Apply(bus1);
            var notAndRest = rest.Select(s => _and.Apply(s, notBus1)).ToList();
            var orOneAndBus1 = _or.Apply(one, bus1);

            var output = notAndRest.Prepend(orOneAndBus1).ToArray();

            Output = _busDataTypeFactory.Create(output);
            _updateWire(Output);
            return Output;
        }

        public void Apply()
        {
            Apply(Input, Set);
        }

        public TBusDataType Input { get; set; }
        public bool Set { get; set; }
        public TBusDataType Output { get; set; }
    }
}