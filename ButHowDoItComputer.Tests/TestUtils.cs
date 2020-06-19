using System;
using System.Collections.Generic;
using ButHowDoItComputer.Components;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Factories;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Tests
{
    public static class TestUtils
    {
        public static BusDataTypeRegister<IList<bool>> CreateRegister(bool set = true, bool enable = true)
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var memoryGateFactory = new MemoryGateFactory(new NAnd(new Not()));
            return new BusDataTypeRegister<IList<bool>>(new BusDataTypeMemoryGate<IList<bool>>(memoryGateFactory, byteFactory, 8),
                new BusDataTypeEnabler<IList<bool>>(byteFactory), byteFactory, wire => {})
            {
                Set = set, Enable = enable
            };
        }

        public static BusDataTypeRegister<IList<bool>> CreateRegister(this uint input, bool set = true, bool enable = true)
        {
            var b10ToByte = CreateByteToBase10Converter();
            var register = CreateRegister(set, enable);
            register.Input = b10ToByte.ToByte(input);
            return register;
        }

        public static BusDataTypeRegister<IList<bool>> CreateRegister(this IList<bool> input, bool set = true, bool enable = true)
        {
            var register = CreateRegister(set, enable);
            register.Input = input;
            return register;
        }

        public static Not CreateNot()
        {
            return new Not();
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
            return new NAnd(CreateNot());
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
            return new BitComparator(CreateXOr(), CreateOr(), CreateNot());
        }

        public static BusDataTypeComparator<IList<bool>> CreateByteComparator()
        {
            return new BusDataTypeComparator<IList<bool>>(CreateBitComparator(), CreateByteFactory());
        }

        public static ArithmeticLogicUnit<IList<bool>> CreateArithmeticLogicUnit()
        {
            var byteFactory = CreateByteFactory();
            return new ArithmeticLogicUnit<IList<bool>>(
                new BusDataTypeXOr<IList<bool>>(CreateXOr(), byteFactory),
                new BusDataTypeOr<IList<bool>>(CreateOr(), byteFactory),
                new BusDataTypeAnd<IList<bool>>(byteFactory),
                new Inverter<IList<bool>>(CreateNot(), byteFactory),
                new BusDataTypeAdder<IList<bool>>(new BitAdder(CreateXOr(), CreateOr()), CreateByteFactory()),
                new BusDataTypeEnabler<IList<bool>>(CreateByteFactory()),
                
                new IsZeroGate<IList<bool>>(CreateOr(), CreateNot()),
                new BusDataTypeDecoder<IList<bool>>(new Decoder(CreateNot(), new Base10Converter()), CreateByteFactory()),
                new BusDataTypeRightShifter<IList<bool>>(CreateByteFactory()),
                new BusDataTypeLeftShifter<IList<bool>>(CreateByteFactory()),
                CreateOr(),
                new AluWire<IList<bool>>(CreateByteFactory()),
                new BusDataTypeComparator<IList<bool>>(new BitComparator(CreateXOr(), CreateOr(), CreateNot()),
                    CreateByteFactory()), caez => {}, input => {},
                byteFactory);
        }

        public static Bus1<IList<bool>> CreateBus1()
        {
            return new Bus1<IList<bool>>(CreateNot(), CreateOr(), CreateByteFactory(), wire => {});
        }

        public static Bus1Factory<IList<bool>> CreateBus1Factory()
        {
            return new Bus1Factory<IList<bool>>(CreateNot(), CreateOr(), CreateByteFactory());
        }

        public static Clock CreateClock()
        {
            return new Clock(CreateClockStateFactory(), CreateOr());
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
            return new Stepper(CreateMemoryGateFactory(), CreateNot(), CreateOr());
        }

        public static Decoder CreateDecoder()
        {
            return new Decoder(CreateNot(), new Base10Converter());
        }

        public static BusDataTypeMemoryGateFactory<IList<bool>> CreateByteMemoryGateFactory()
        {
            return new BusDataTypeMemoryGateFactory<IList<bool>>(CreateMemoryGateFactory(), CreateByteFactory(), 8);
        }

        public static BusDataTypeEnabler<IList<bool>> CreateByteEnabler()
        {
            return new BusDataTypeEnabler<IList<bool>>(CreateByteFactory());
        }

        public static BusDataTypeRegisterFactory<IList<bool>> CreateBusTypeRegisterFactory()
        {
            return new BusDataTypeRegisterFactory<IList<bool>>(CreateByteMemoryGateFactory(), CreateByteEnabler(), CreateByteFactory());
        }

        public static Ram<IList<bool>> CreateRam()
        {
            return CreateRam(new Bus<IList<bool>>(new BusMessage<IList<bool>> {Data = new bool[8], Name = "Ram"}));
        }

        public static Ram<IList<bool>> CreateRam(IBus<IList<bool>> bus)
        {
            return new Ram<IList<bool>>(8, bus, CreateBusTypeRegisterFactory(), CreateDecoder());
        }
        
        public static CaezRegister CreateCaezRegister()
        {
            return new CaezRegister(new CaezMemoryGate(CreateMemoryGateFactory()), new CaezEnabler(), wire => {});
        }

        public static CaezRegisterFactory CreateCaezRegisterFactory()
        {
            return new CaezRegisterFactory(CreateMemoryGateFactory());
        }
    }
}