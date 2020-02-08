using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Ram : IRam, IApplicable, IEnablable, ISettable
    {
        private readonly IByteRegisterFactory _byteRegisterFactory;
        private readonly IDecoder _decoder;
        private readonly IAnd _and;

        public IRegister<IByte> MemoryAddressRegister { get; private set; }

        public bool Set { get; set; }
        public bool Enable { get; set; }


        public IBus Io { get; }

        public List<List<IRegister<IByte>>> InternalRegisters { get; private set; } = new List<List<IRegister<IByte>>>();

        public Ram(IBus outputBus, IByteRegisterFactory byteRegisterFactory,
            IDecoder decoder, IAnd and)
        {
            Io = outputBus;
            _byteRegisterFactory = byteRegisterFactory;
            _decoder = decoder;
            _and = and;

            Set = false;
            Enable = false;

            SetupInputRegister();
            SetupInternalRegisters();
        }

        private void SetupInputRegister()
        {
            MemoryAddressRegister = _byteRegisterFactory.Create();
            // never need to hide input registers value
            MemoryAddressRegister.Enable = true;
            MemoryAddressRegister.Name = "MAR";
            Io.BusSubscribers.Add(MemoryAddressRegister);
        }

        private void SetupInternalRegisters()
        {
            InternalRegisters = Enumerable.Range(0, 16)
                .Select(x => Enumerable.Range(0, 16).Select(y =>
                {
                    var reg = _byteRegisterFactory.Create();
                    reg.Name = $@"RamInternalRegister{x}{y}";
                    return reg;
                }).ToList()).ToList();

            foreach (var register in InternalRegisters.SelectMany(row => row))
            {
                Io.Add(register);
            }
        }

        public void SetMemoryAddress(IByte address)
        {
            MemoryAddressRegister.Set = true;
            MemoryAddressRegister.Apply(address);
            MemoryAddressRegister.Set = false;
            Apply();
        }

        public void Apply()
        {
            var inputData = MemoryAddressRegister.Data;
            
            var yInput = new [] {inputData.Eight, inputData.Seven, inputData.Six, inputData.Five};
            var yDecoder = _decoder.Apply(yInput).ToList();

            var xInput = new [] {inputData.Four, inputData.Three, inputData.Two, inputData.One};
            var xDecoder = _decoder.Apply(xInput).ToList();

            for (var y = 0; y < yDecoder.Count; y++)
            {
                for (var x = 0; x < xDecoder.Count; x++)
                {
                    var xAndY = _and.Apply(xDecoder[x], yDecoder[y]);

                    var s = _and.Apply(xAndY, Set);
                    var e = _and.Apply(xAndY, Enable);

                    InternalRegisters[y][x].Set = s;
                    InternalRegisters[y][x].Enable = e;
                }
            }
        }

        public void ApplyState()
        {
            Set = true;
            Apply();
            Io.Apply();
            Set = false;
        }

        public void ApplyEnable()
        {
            Enable = true;
            Apply();
            Io.Apply();
            Enable = false;
        }
    }
}