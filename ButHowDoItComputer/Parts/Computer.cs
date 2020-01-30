using System.Collections.Generic;
using ButHowDoItComputer.Adapters.Factories;
using ButHowDoItComputer.Components;
using ButHowDoItComputer.Components.CpuSubscribers;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Factories;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Parts
{
    public class Computer
    {
        private CentralProcessingUnit _cpu;
        private BitSubscriberNotifier _dataAddress;
        private BitSubscriberNotifier _inputOutput;
        private Bus _bus;

        public Computer(IObjectCreationFactory<IArithmeticLogicUnit> aluFactory)
        {
            var bitFactory = new BitFactory();
            var byteFactory = new ByteFactory(bitFactory, new Base10Converter(bitFactory));
            var and = new And(bitFactory);
            var not = new Not(bitFactory);
            var nAnd = new NAnd(not, and);
            var or = new Or(not, nAnd);
            var clockStateFactory = new ClockStateFactory();
            var clock = new Clock(clockStateFactory, and, or, bitFactory);
            var memoryGateFactory = new MemoryGateFactory(nAnd, bitFactory);
            var stepper = new Stepper(memoryGateFactory, and, not, or, bitFactory);
            
            var registerListGateFactory = new RegisterListGateFactory();
            var byteGateListFactory = new ByteGateToListFactory();
            var byteMemoryGateFactory = new ByteMemoryGateFactory(memoryGateFactory, byteFactory);
            var byteEnabler = new ByteEnabler(and, byteFactory);
            var registerFactory = new ByteRegisterFactory(byteMemoryGateFactory, byteEnabler, byteFactory, bitFactory);
            var decoder = new Decoder(not, and, bitFactory);
            
            var acc = registerFactory.Create();
            var iar = registerFactory.Create();
            var r0 = registerFactory.Create();
            var r1 = registerFactory.Create();
            var r2 = registerFactory.Create();
            var r3 = registerFactory.Create();
            var ir = registerFactory.Create();
            var tmp = registerFactory.Create();
            
            tmp.Enable = bitFactory.Create(true);
            ir.Enable = bitFactory.Create(true);
            
            _bus = new Bus(new List<IRegister<IByte>> {r0, r1, r2, r3, ir, iar, acc, tmp}, byteFactory);
            
            var ram = new Ram(_bus, _bus, registerFactory, bitFactory, decoder, and);

            _dataAddress = new BitSubscriberNotifier();
            _inputOutput = new BitSubscriberNotifier();
            
            var ioClk = new Clock(clockStateFactory, and, or, bitFactory);

            var cpuEnables = new CpuEnables
            {
                Acc = new EnableSubscriberNotifier(acc),
                DataAddress = _dataAddress,
                Iar = new EnableSubscriberNotifier(iar),
                InputOutput = _inputOutput,
                IoClk = new EnableSubscriberNotifier(ioClk),
                R0 = new EnableSubscriberNotifier(r0),
                R1 = new EnableSubscriberNotifier(r1),
                R2 = new EnableSubscriberNotifier(r2),
                R3 = new EnableSubscriberNotifier(r3),
                Ram = new EnableSubscriberNotifier(ram)
            };
            
            var cpuSets = new CpuSets
            {
                Acc = new SetSubscriberNotifier(acc),
                Flags = null,
                Iar = new SetSubscriberNotifier(iar),
                IoClk = new SetSubscriberNotifier(ioClk),
                Ir = new SetSubscriberNotifier(ir),
                Mar = new SetSubscriberNotifier(ram.MemoryAddressRegister),
                R0 = new SetSubscriberNotifier(r0),
                R1 = new SetSubscriberNotifier(r0),
                R2 = new SetSubscriberNotifier(r0),
                R3 = new SetSubscriberNotifier(r0),
                Ram = new SetSubscriberNotifier(ram)
            };
            
            var cpuInput = new CpuInput
            {
                Caez = new Caez
                {
                    C = null,
                    A = null,
                    E = null,
                    Z = null
                }
            };
            
            var bus1 = new Bus1(and, not, or, byteFactory);

            var registerBus1 = new RegisterBus1(registerListGateFactory, bus1, byteGateListFactory, registerFactory);
            
            var bus1Set = new SetSubscriberNotifier(registerBus1);
            
            var alu = aluFactory.Create();
            var registerAlu = new RegisterArithmeticLogicUnit(alu, registerFactory);

            _cpu = new CentralProcessingUnit(clock, stepper, cpuEnables, cpuSets, cpuInput, bus1Set, registerAlu, and, or, not, decoder);  
        }   
        
        public void Step()
        {
            _bus.Apply();
            _cpu.Step();
        }
    }
}