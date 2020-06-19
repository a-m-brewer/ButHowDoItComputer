using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Parts
{
    public class Ram<TBusDataType> : IRam<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeRegisterFactory<TBusDataType> _busDataTypeRegisterFactory;
        private readonly IDecoder _decoder;
        private readonly int _decoderSize;

        public Ram(
            int bitLength,
            IBus<TBusDataType> outputBus, 
            IBusDataTypeRegisterFactory<TBusDataType> busDataTypeRegisterFactory,
            IDecoder decoder)
        {
            Io = outputBus;
            _busDataTypeRegisterFactory = busDataTypeRegisterFactory;
            _decoder = decoder;
            
            Set = false;
            Enable = false;

            _decoderSize = bitLength / 2;
            var internalRegisterBitLength = (int) Math.Pow(2, _decoderSize);

            SetupInputRegister();
            SetupInternalRegisters(internalRegisterBitLength);
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

        public void Apply()
        {
            var inputData = MemoryAddressRegister.Data;

            var yInput = inputData.SkipList(_decoderSize).ReverseList();

            var yDecoder = _decoder.Apply(yInput);

            var xInput = inputData.TakeList(_decoderSize).ReverseList();
            
            var xDecoder = _decoder.Apply(xInput);

            Apply(xDecoder, yDecoder);
        }

        private void Apply(IList<bool> xDecoder, IList<bool> yDecoder)
        {
            for (var y = 0; y < yDecoder.Count; y++)
            for (var x = 0; x < xDecoder.Count; x++)
            {
                var xAndY = xDecoder[x] && yDecoder[y];
                var s = xAndY && Set;
                var e = xAndY && Enable;
                
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
            MemoryAddressRegister = _busDataTypeRegisterFactory.Create(update => {}, nameof(MemoryAddressRegister));
            // never need to hide input registers value
            MemoryAddressRegister.Enable = true;
        }

        private void SetupInternalRegisters(int bitLength)
        {
            InternalRegisters = new IRegister<TBusDataType>[bitLength][];

            for (var y = 0; y < InternalRegisters.Length; y++)
            {
                InternalRegisters[y] = new IRegister<TBusDataType>[bitLength];

                for (var x = 0; x < InternalRegisters[y].Length; x++)
                {
                    var x1 = x;
                    var y1 = y;
                    InternalRegisters[y][x] =  _busDataTypeRegisterFactory.Create(updateWire =>
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