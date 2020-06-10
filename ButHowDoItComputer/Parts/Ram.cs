using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Ram : RamBase<IByte>, IRam<IByte>
    {
        private readonly IDecoder _decoder;

        public Ram(IBus<IByte> outputBus, IBusDataTypeRegisterFactory<IByte> busDataTypeFactory,
            IDecoder decoder, IAnd and) : base(outputBus, busDataTypeFactory, decoder, and, 16)
        {
            _decoder = decoder;
        }

        public override void Apply()
        {
            var inputData = MemoryAddressRegister.Data;

            var yInput = new[] {inputData[7], inputData[6], inputData[5], inputData[4]};
            var yDecoder = _decoder.Apply(yInput).ToList();

            var xInput = new[] {inputData[3], inputData[2], inputData[1], inputData[0]};
            var xDecoder = _decoder.Apply(xInput).ToList();

            Apply(xDecoder, yDecoder);
        }
    }
}