using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Tests
{
    public static class TestUtils
    {
        public static Register CreateRegister(bool set = true, bool enable = true)
        {
            var bitFactory = new BitFactory();
            var byteFactory = new ByteFactory(bitFactory, new Base10Converter(bitFactory));
            var memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(bitFactory), new And(bitFactory)), bitFactory);
            var and = new And(bitFactory);
            return new Register(new ByteMemoryGate(memoryGateFactory, byteFactory),
                new ByteEnabler(and, byteFactory), byteFactory, bitFactory)
            {
                Set = new Bit(set), Enable = new Bit(enable)
            };
        }
    }
}