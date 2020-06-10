using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class RamBase<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IAnd _and;
        private readonly IBusDataTypeRegisterFactory<TBusDataType> _busDataTypeFactory;
        private readonly IDecoder _decoder;

        public RamBase(
            IBus<TBusDataType> outputBus, 
            IBusDataTypeRegisterFactory<TBusDataType> busDataTypeFactory,
            IDecoder decoder, IAnd and, int registerDepth)
        {
            Io = outputBus;
            _busDataTypeFactory = busDataTypeFactory;
            _decoder = decoder;
            _and = and;

            Set = false;
            Enable = false;

            SetupInputRegister();
            SetupInternalRegisters(registerDepth);
        }

        public IRegister<TBusDataType> MemoryAddressRegister { get; private set; }

        public bool Set { get; set; }
        public bool Enable { get; set; }


        public IBus<TBusDataType> Io { get; }

        public List<List<IRegister<TBusDataType>>> InternalRegisters { get; private set; } =
            new List<List<IRegister<TBusDataType>>>();

        public void SetMemoryAddress(TBusDataType address)
        {
            MemoryAddressRegister.Set = true;
            MemoryAddressRegister.Apply(address);
            MemoryAddressRegister.Set = false;
            Apply();
        }

        public virtual void Apply()
        {
            throw new NotImplementedException("needs to be overriden");
        }

        protected void Apply(List<bool> xDecoder, List<bool> yDecoder)
        {
            for (var y = 0; y < yDecoder.Count; y++)
            for (var x = 0; x < xDecoder.Count; x++)
            {
                var xAndY = _and.Apply(xDecoder[x], yDecoder[y]);

                var s = _and.Apply(xAndY, Set);
                var e = _and.Apply(xAndY, Enable);

                InternalRegisters[y][x].Set = s;
                InternalRegisters[y][x].Enable = e;

                InternalRegisters[y][x].Apply();
            }
        }

        public void ApplyState()
        {
            Set = true;
            Apply();
            Set = false;
        }

        public void ApplyEnable()
        {
            Enable = true;
            Apply();
            Enable = false;
        }

        private void SetupInputRegister()
        {
            MemoryAddressRegister = _busDataTypeFactory.Create(update => {}, nameof(MemoryAddressRegister));
            // never need to hide input registers value
            MemoryAddressRegister.Enable = true;
        }

        private void SetupInternalRegisters(int registerDepth)
        {
            InternalRegisters = Enumerable.Range(0, registerDepth)
                .Select(x => Enumerable.Range(0, registerDepth).Select(y =>
                {
                    var reg = _busDataTypeFactory.Create(updateWire =>
                    {
                        Io.UpdateData(new BusMessage<TBusDataType> {Name = $@"RamInternalRegister{x}{y}", Data = updateWire});
                        Io.UpdateSubs();
                    }, $@"RamInternalRegister{x}{y}");
                    return reg;
                }).ToList()).ToList();

            foreach (var register in InternalRegisters.SelectMany(internalRegisterRow => internalRegisterRow))
            {
                Io.AddRegister(register);
            }
        }
    }
}