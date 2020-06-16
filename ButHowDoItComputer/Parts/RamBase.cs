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
        protected readonly IDecoder Decoder;

        protected int X;
        protected int Y;

        public RamBase(
            IBus<TBusDataType> outputBus, 
            IBusDataTypeRegisterFactory<TBusDataType> busDataTypeFactory,
            IDecoder decoder, IAnd and)
        {
            Io = outputBus;
            _busDataTypeFactory = busDataTypeFactory;
            Decoder = decoder;
            _and = and;

            Set = false;
            Enable = false;

            SetupInputRegister();
        }

        public IRegister<TBusDataType> MemoryAddressRegister { get; private set; }

        public bool Set { get; set; }
        public bool Enable { get; set; }


        public IBus<TBusDataType> Io { get; }

        public IRegister<TBusDataType>[][] InternalRegisters { get; private set; }

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
                var xAndY = _and.ApplyParams(xDecoder[x], yDecoder[y]);

                var s = _and.ApplyParams(xAndY, Set);
                var e = _and.ApplyParams(xAndY, Enable);

                InternalRegisters[y][x].Set = s;
                InternalRegisters[y][x].Enable = e;

                if (!s && !e)
                {
                    continue;
                }

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

        protected void SetupInternalRegisters(int lenX, int lenY)
        {
            InternalRegisters = new IRegister<TBusDataType>[lenY][];

            for (var y = 0; y < InternalRegisters.Length; y++)
            {
                InternalRegisters[y] = new IRegister<TBusDataType>[lenX];

                for (var x = 0; x < InternalRegisters[y].Length; x++)
                {
                    var x1 = x;
                    var y1 = y;
                    InternalRegisters[y][x] =  _busDataTypeFactory.Create(updateWire =>
                    {
                        Io.UpdateData(new BusMessage<TBusDataType> {Name = $@"RamInternalRegister{x1}{y1}", Data = updateWire});
                        Io.UpdateSubs();
                    }, $@"RamInternalRegister{x}{y}");
                }
            }

            foreach (var register in InternalRegisters.SelectMany(internalRegisterRow => internalRegisterRow))
            {
                Io.AddRegister(register);
            }
        }
    }
}