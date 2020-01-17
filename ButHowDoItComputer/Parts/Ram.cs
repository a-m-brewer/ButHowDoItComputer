using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Ram : IRam
    {
        public IBus MemoryAddressBus { get; }
        private readonly IRegisterFactory _registerFactory;
        private readonly IBitFactory _bitFactory;
        private readonly IDecoder _decoder;
        private readonly IAnd _and;

        public IRegister MemoryAddressRegister { get; private set; }

        public IBit Set { get; set; }
        public IBit Enable { get; set; }


        public IBus Io { get; }

        private List<List<IRegister>> _internalRegisters = new List<List<IRegister>>();

        public Ram(IBus memoryAddressBus, IBus outputBus, IRegisterFactory registerFactory, IBitFactory bitFactory,
            IDecoder decoder, IAnd and)
        {
            MemoryAddressBus = memoryAddressBus;
            Io = outputBus;
            _registerFactory = registerFactory;
            _bitFactory = bitFactory;
            _decoder = decoder;
            _and = and;

            Set = bitFactory.Create(false);
            Enable = bitFactory.Create(false);

            SetupInputRegister();
            SetupInternalRegisters();
        }

        private void SetupInputRegister()
        {
            MemoryAddressRegister = _registerFactory.Create();
            // never need to hide input registers value
            MemoryAddressRegister.Enable.State = true;
            MemoryAddressBus.Add(MemoryAddressRegister);
        }

        private void SetupInternalRegisters()
        {
            _internalRegisters = Enumerable.Range(0, 16)
                .Select(s => Enumerable.Range(0, 16).Select(y => _registerFactory.Create()).ToList()).ToList();

            foreach (var register in _internalRegisters.SelectMany(row => row))
            {
                Io.Add(register);
            }
        }

        public void SetMemoryAddress(IByte address)
        {
            MemoryAddressRegister.Set.State = true;
            MemoryAddressRegister.Apply(address);
            MemoryAddressRegister.Set.State = false;
        }

        public void Apply()
        {
            var inputData = MemoryAddressRegister.Byte;
            var yInput = new [] {inputData.One, inputData.Two, inputData.Three, inputData.Four};
            var yDecoder = _decoder.Apply(yInput).ToList();

            var xInput = new [] {inputData.Five, inputData.Six, inputData.Seven, inputData.Eight};
            var xDecoder = _decoder.Apply(xInput).ToList();

            for (var y = 0; y < yDecoder.Count; y++)
            {
                for (var x = 0; x < xDecoder.Count; x++)
                {
                    var xAndY = _and.Apply(xDecoder[x], yDecoder[y]);

                    var s = _and.Apply(xAndY, Set);
                    var e = _and.Apply(xAndY, Enable);

                    _internalRegisters[y][x].Set = s;
                    _internalRegisters[y][x].Enable = e;
                }
            }
        }

        public void ApplyState()
        {
            Set.State = true;
            Apply();
            Io.Apply();
            Set.State = false;
        }

        public void ApplyEnable()
        {
            Enable.State = true;
            Apply();
            Io.Apply();
            Enable.State = false;
        }
    }
}