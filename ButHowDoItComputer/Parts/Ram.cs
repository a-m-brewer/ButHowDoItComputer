using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Ram : IRam<IByte>
    {
        private readonly IAnd _and;
        private readonly IBusDataTypeRegisterFactory<IByte> _busDataTypeFactory;
        private readonly IDecoder _decoder;

        public Ram(IBus<IByte> outputBus, IBusDataTypeRegisterFactory<IByte> busDataTypeFactory,
            IDecoder decoder, IAnd and)
        {
            Io = outputBus;
            _busDataTypeFactory = busDataTypeFactory;
            _decoder = decoder;
            _and = and;

            Set = false;
            Enable = false;

            SetupInputRegister();
            SetupInternalRegisters();
        }

        public IRegister<IByte> MemoryAddressRegister { get; private set; }

        public bool Set { get; set; }
        public bool Enable { get; set; }


        public IBus<IByte> Io { get; }

        public List<List<IRegister<IByte>>> InternalRegisters { get; private set; } =
            new List<List<IRegister<IByte>>>();

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

            var yInput = new[] {inputData[7], inputData[6], inputData[5], inputData[4]};
            var yDecoder = _decoder.Apply(yInput).ToList();

            var xInput = new[] {inputData[3], inputData[2], inputData[1], inputData[0]};
            var xDecoder = _decoder.Apply(xInput).ToList();

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

        private void SetupInternalRegisters()
        {
            InternalRegisters = Enumerable.Range(0, 16)
                .Select(x => Enumerable.Range(0, 16).Select(y =>
                {
                    var reg = _busDataTypeFactory.Create(updateWire =>
                    {
                        Io.UpdateData(new BusMessage<IByte> {Name = $@"RamInternalRegister{x}{y}", Data = updateWire});
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