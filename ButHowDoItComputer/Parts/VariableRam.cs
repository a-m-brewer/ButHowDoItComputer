using System;
using System.Linq;
using ButHowDoItComputer.DataTypes.BusDataTypes;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class VariableRam : RamBase<VariableBitLength>, IRam<VariableBitLength>
    {
        public VariableRam(IBus<VariableBitLength> outputBus, IBusDataTypeRegisterFactory<VariableBitLength> busDataTypeFactory, IDecoder decoder, IAnd and) : base(outputBus, busDataTypeFactory, decoder, and)
        {
            // TODO: make this work on variable size
            var arraySize = (int) Math.Pow(2, 8);
            X = arraySize;
            Y = arraySize;
            SetupInternalRegisters(X, Y);
        }
        
        // TODO: make this work on variable size
        public override void Apply()
        {
            var inputData = MemoryAddressRegister.Data;

            var yInput = new[] {inputData[7], inputData[6], inputData[5], inputData[4]};
            var yDecoder = Decoder.Apply(yInput).ToList();

            var xInput = new[] {inputData[3], inputData[2], inputData[1], inputData[0]};
            var xDecoder = Decoder.Apply(xInput).ToList();

            Apply(xDecoder, yDecoder);
        }
    }
}