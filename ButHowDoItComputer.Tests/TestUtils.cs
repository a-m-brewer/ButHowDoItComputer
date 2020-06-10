using ButHowDoItComputer.Components;
using ButHowDoItComputer.Components.Factories;
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
        public static BusDataTypeRegister<IByte> CreateRegister(bool set = true, bool enable = true)
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(), new And()));
            var and = new And();
            return new BusDataTypeRegister<IByte>(new BusDataTypeMemoryGate<IByte>(memoryGateFactory, byteFactory),
                new BusDataTypeEnabler<IByte>(and, byteFactory), byteFactory, wire => {})
            {
                Set = set, Enable = enable
            };
        }

        public static BusDataTypeRegister<IByte> CreateRegister(this uint input, bool set = true, bool enable = true)
        {
            var b10ToByte = CreateByteToBase10Converter();
            var register = CreateRegister(set, enable);
            register.Input = b10ToByte.ToByte(input);
            return register;
        }

        public static BusDataTypeRegister<IByte> CreateRegister(this IByte input, bool set = true, bool enable = true)
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

        public static BusDataTypeComparator<IByte> CreateByteComparator()
        {
            return new BusDataTypeComparator<IByte>(CreateBitComparator(), CreateByteFactory());
        }

        public static ArithmeticLogicUnit<IByte> CreateArithmeticLogicUnit()
        {
            var byteFactory = CreateByteFactory();
            return new ArithmeticLogicUnit<IByte>(
                new BusDataTypeXOr<IByte>(CreateXOr(), byteFactory),
                new BusDataTypeOr<IByte>(CreateOr(), byteFactory),
                new BusDataTypeAnd<IByte>(CreateAnd(), byteFactory),
                new Inverter<IByte>(CreateNot(), byteFactory),
                new BusDataTypeAdder<IByte>(new BitAdder(CreateXOr(), CreateOr(), CreateAnd()), CreateByteFactory()),
                new BusDataTypeEnabler<IByte>(CreateAnd(), CreateByteFactory()),
                CreateAnd(),
                new IsZeroGate<IByte>(CreateOr(), CreateNot()),
                new BusDataTypeDecoder<IByte>(new Decoder(CreateNot(), CreateAnd()), CreateByteFactory()),
                new BusDataTypeRightShifter<IByte>(CreateByteFactory()),
                new BusDataTypeLeftShifter<IByte>(CreateByteFactory()),
                CreateOr(),
                new AluWire<IByte>(CreateByteFactory()),
                new BusDataTypeComparator<IByte>(new BitComparator(CreateXOr(), CreateAnd(), CreateOr(), CreateNot()),
                    CreateByteFactory()), caez => {}, input => {},
                byteFactory);
        }

        public static Bus1<IByte> CreateBus1()
        {
            return new Bus1<IByte>(CreateAnd(), CreateNot(), CreateOr(), CreateByteFactory(), wire => {});
        }

        public static Bus1Factory<IByte> CreateBus1Factory()
        {
            return new Bus1Factory<IByte>(CreateAnd(), CreateNot(), CreateOr(), CreateByteFactory());
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

        public static Decoder CreateDecoder()
        {
            return new Decoder(CreateNot(), CreateAnd());
        }

        public static BusDataTypeMemoryGateFactory<IByte> CreateByteMemoryGateFactory()
        {
            return new BusDataTypeMemoryGateFactory<IByte>(CreateMemoryGateFactory(), CreateByteFactory());
        }

        public static BusDataTypeEnabler<IByte> CreateByteEnabler()
        {
            return new BusDataTypeEnabler<IByte>(CreateAnd(), CreateByteFactory());
        }

        public static BusDataTypeRegisterFactory<IByte> CreateBusTypeRegisterFactory()
        {
            return new BusDataTypeRegisterFactory<IByte>(CreateByteMemoryGateFactory(), CreateByteEnabler(), CreateByteFactory());
        }

        public static Ram CreateRam()
        {
            return CreateRam(new Bus<IByte>(new BusMessage<IByte> {Data = new Byte(), Name = "Ram"}));
        }

        public static Ram CreateRam(IBus<IByte> bus)
        {
            return new Ram(bus, CreateBusTypeRegisterFactory(), CreateDecoder(), CreateAnd());
        }
        
        public static CaezRegister CreateCaezRegister()
        {
            return new CaezRegister(new CaezMemoryGate(CreateMemoryGateFactory()), new CaezEnabler(CreateAnd()), wire => {});
        }

        public static CaezRegisterFactory CreateCaezRegisterFactory()
        {
            return new CaezRegisterFactory(CreateMemoryGateFactory(), CreateAnd());
        }
    }
}