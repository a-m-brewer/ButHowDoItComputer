using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Ram64 : RamBase<ISixteenBit>, IRam<ISixteenBit>
    {
        private readonly IDecoder _decoder;

        public Ram64(IBus<ISixteenBit> outputBus, IBusDataTypeRegisterFactory<ISixteenBit> busDataTypeFactory, IDecoder decoder, IAnd and) : base(outputBus, busDataTypeFactory, decoder, and, 256)
        {
            _decoder = decoder;
        }

        public override void Apply()
        {
            var inputData = MemoryAddressRegister.Data;

            var yInput = new[] {inputData[15], inputData[14], inputData[13], inputData[12], inputData[11], inputData[10], inputData[9], inputData[8]};
            var yDecoder = _decoder.Apply(yInput).ToList();

            var xInput = new[] {inputData[7], inputData[6], inputData[5], inputData[4], inputData[3], inputData[2], inputData[1], inputData[0]};
            var xDecoder = _decoder.Apply(xInput).ToList();

            Apply(xDecoder, yDecoder);
        }
    }
}