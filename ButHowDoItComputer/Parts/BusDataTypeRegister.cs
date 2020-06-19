using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class BusDataTypeRegister<TBusDataType> : IRegister<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeEnabler<TBusDataType> _busDataTypeEnabler;
        private readonly Action<TBusDataType> _updateWire;
        private readonly IBusDataTypeMemoryGate<TBusDataType> _busDataTypeMemoryGate;

        public BusDataTypeRegister(IBusDataTypeMemoryGate<TBusDataType> busDataTypeMemoryGate, IBusDataTypeEnabler<TBusDataType> busDataTypeEnabler, IBusDataTypeFactory<TBusDataType> busDataTypeFactory, Action<TBusDataType> updateWire)
        {
            _busDataTypeMemoryGate = busDataTypeMemoryGate;
            _busDataTypeEnabler = busDataTypeEnabler;
            _updateWire = updateWire;
            Input = busDataTypeFactory.Create();
            Output = busDataTypeFactory.Create();
            Data = busDataTypeFactory.Create();
            Set = false;
            Enable = false;
        }

        public bool Enable { get; set; }
        public bool Set { get; set; }

        public TBusDataType Input { get; set; }

        public TBusDataType Data { get; set; }

        public TBusDataType Output { get; private set; }
        public string Name { get; set; }

        public TBusDataType ApplyOnce(TBusDataType input, bool enable = false)
        {
            Enable = enable;
            Set = true;
            var applied = Apply(input);
            Set = false;
            return applied;
        }

        public TBusDataType Apply(TBusDataType input)
        {
            Input = input;
            Apply();
            return Output;
        }

        public void Apply()
        {
            ApplyPrivate(Input);
            ApplyOutput();

            if (Enable)
            {
                _updateWire(Output);
            }
        }

        private void ApplyPrivate(TBusDataType input)
        {
            Data = _busDataTypeMemoryGate.Apply(input, Set);
        }

        private void ApplyOutput()
        {
            Output = _busDataTypeEnabler.Apply(Data, Enable);
        }
    }
}