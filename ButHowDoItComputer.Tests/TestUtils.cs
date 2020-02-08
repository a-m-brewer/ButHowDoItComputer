using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Factories;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Tests
{
    public static class TestUtils
    {
        public static ByteRegister CreateRegister(bool set = true, bool enable = true)
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(), new And()));
            var and = new And();
            return new ByteRegister(new ByteMemoryGate(memoryGateFactory, byteFactory),
                new ByteEnabler(and, byteFactory), byteFactory)
            {
                Set = set, Enable = enable
            };
        }

        public static ByteRegister CreateRegister(this uint input, bool set = true, bool enable = true)
        {
            var b10ToByte = CreateByteToBase10Converter();
            var register = CreateRegister(set, enable);
            register.Input = b10ToByte.ToByte(input);
            return register;
        }
        
        public static ByteRegister CreateRegister(this IByte input, bool set = true, bool enable = true)
        {
            var register = CreateRegister(set, enable);
            register.Input = input;
            return register;
        }

        public static Not CreateNot()
        {
            return new Not();
        }

        public static And CreateAnd()
        {
            return new And();
        }

        public static Or CreateOr()
        {
            return new Or(CreateNot(), CreateNAnd());
        }

        public static XOr CreateXOr()
        {
            return new XOr(CreateNot(), CreateNAnd());
        }

        public static NAnd CreateNAnd()
        {
            return new NAnd(CreateNot(), CreateAnd());
        }

        public static ByteFactory CreateByteFactory()
        {
            return new ByteFactory(new Base10Converter());
        }

        public static ByteToBase10Converter CreateByteToBase10Converter()
        {
            return new ByteToBase10Converter(CreateByteFactory(), new Base10Converter());
        }

        public static BitComparator CreateBitComparator()
        {
            return new BitComparator(CreateXOr(), CreateAnd(), CreateOr(), CreateNot());
        }

        public static ByteComparator CreateByteComparator()
        {
            return new ByteComparator(CreateBitComparator(), CreateByteFactory());
        }

        public static ArithmeticLogicUnit CreateArithmeticLogicUnit()
        {
            var byteFactory = CreateByteFactory();
            return new ArithmeticLogicUnit(
                new ByteXOr(CreateXOr(), byteFactory),
                new ByteOr(CreateOr(), byteFactory),
                new ByteAnd(CreateAnd(), byteFactory),
                new Inverter(CreateNot(), byteFactory),
                new ByteAdder(new BitAdder(CreateXOr(), CreateOr(), CreateAnd()), CreateByteFactory()),
                new ByteEnabler(CreateAnd(), CreateByteFactory()),
                CreateAnd(),
                new IsZeroGate(CreateOr(), CreateNot()),
                new ByteDecoder(new Decoder(CreateNot(), CreateAnd()), CreateByteFactory()),
                new ByteRightShifter(CreateByteFactory()),
                new ByteLeftShifter(CreateByteFactory()),
                CreateOr(),
                new Wire(CreateByteFactory()),
                new ByteComparator(new BitComparator(CreateXOr(), CreateAnd(), CreateOr(), CreateNot()), CreateByteFactory()));
        }

        public static Bus1 CreateBus1()
        {
            return new Bus1(CreateAnd(), CreateNot(), CreateOr(), CreateByteFactory());
        }

        public static Clock CreateClock()
        {
            return new Clock(CreateClockStateFactory(), CreateAnd(), CreateOr());
        }

        public static ClockStateFactory CreateClockStateFactory()
        {
            return new ClockStateFactory();
        }

        public static MemoryGateFactory CreateMemoryGateFactory()
        {
            return new MemoryGateFactory(CreateNAnd());
        }
        
        public static Stepper CreateStepper()
        {
            return new Stepper(CreateMemoryGateFactory(), CreateAnd(), CreateNot(), CreateOr());
        }
    }
}