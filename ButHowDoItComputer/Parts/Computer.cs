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
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Computer
    {
        private BitSubscriberNotifier _dataAddress;
        private BitSubscriberNotifier _inputOutput;

        public Computer(IArithmeticLogicUnit alu)
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var and = new And();
            var not = new Not();
            var nAnd = new NAnd(not, and);
            var or = new Or(not, nAnd);
            var clockStateFactory = new ClockStateFactory();
            var clock = new Clock(clockStateFactory, and, or);
            var memoryGateFactory = new MemoryGateFactory(nAnd);
            var stepper = new Stepper(memoryGateFactory, and, not, or);
            
            var registerListGateFactory = new RegisterListGateFactory();
            var byteGateListFactory = new ByteGateToListFactory();
            var byteMemoryGateFactory = new ByteMemoryGateFactory(memoryGateFactory, byteFactory);
            var byteEnabler = new ByteEnabler(and, byteFactory);
            var registerFactory = new ByteRegisterFactory(byteMemoryGateFactory, byteEnabler, byteFactory);
            var decoder = new Decoder(not, and);
            
            Acc = registerFactory.Create();
            Acc.Name = nameof(Acc);
            InstructionAddressRegister = registerFactory.Create();
            InstructionAddressRegister.Name = nameof(InstructionAddressRegister);
            R0 = registerFactory.Create();
            R0.Name = nameof(R0);
            R1 = registerFactory.Create();
            R1.Name = nameof(R1);
            R2 = registerFactory.Create();
            R2.Name = nameof(R2);
            R3 = registerFactory.Create();
            R3.Name = nameof(R3);
            InstructionRegister = registerFactory.Create();
            InstructionRegister.Name = nameof(InstructionRegister);
            Temp = registerFactory.Create();
            Temp.Name = nameof(Temp);

            Temp.Enable = true;
            InstructionRegister.Enable = true;

            _dataAddress = new BitSubscriberNotifier();
            _inputOutput = new BitSubscriberNotifier();
            
            IoClock = new Clock(clockStateFactory, and, or);
            
            CaezRegister = new CaezRegisterFactory(memoryGateFactory, and).Create();
            CaezRegister.Enable = true;
            CaezRegister.Name = nameof(CaezRegister);

            Bus1 = new Bus1(and, not, or, byteFactory);;
            Temp.Subscribers.Add(Bus1);

            var bus1Set = new SetSubscriberNotifier(Bus1);

            Bus = new Bus(new List<IRegister<IByte>> {R0, R1, R2, R3, InstructionAddressRegister}, byteFactory);
            Bus.BusSubscribers.Add(Temp);
            Bus.BusSubscribers.Add(InstructionRegister);
            Acc.Subscribers.Add(Bus);

            Ram = new Ram(Bus, registerFactory, decoder, and);
            
            ArithmeticLogicUnit = new RegisterArithmeticLogicUnit(alu, registerFactory);
            ArithmeticLogicUnit.Subscribers.Add(Acc);
            
            Bus.BusSubscribers.Add(ArithmeticLogicUnit.InputA);
            Bus1.BusSubscribers.Add(ArithmeticLogicUnit.InputB);
            
            var cpuEnables = new CpuEnables
            {
                Acc = new EnableSubscriberNotifier(Acc),
                DataAddress = _dataAddress,
                Iar = new EnableSubscriberNotifier(InstructionAddressRegister),
                InputOutput = _inputOutput,
                IoClk = new EnableSubscriberNotifier(IoClock),
                R0 = new EnableSubscriberNotifier(R0),
                R1 = new EnableSubscriberNotifier(R1),
                R2 = new EnableSubscriberNotifier(R2),
                R3 = new EnableSubscriberNotifier(R3),
                Ram = new EnableSubscriberNotifier(Ram)
            };
            
            var cpuSets = new CpuSets
            {
                Acc = new SetSubscriberNotifier(Acc),
                Flags = new SetSubscriberNotifier(CaezRegister),
                Iar = new SetSubscriberNotifier(InstructionAddressRegister),
                IoClk = new SetSubscriberNotifier(IoClock),
                Ir = new SetSubscriberNotifier(InstructionRegister),
                Mar = new SetSubscriberNotifier(Ram.MemoryAddressRegister),
                R0 = new SetSubscriberNotifier(R0),
                R1 = new SetSubscriberNotifier(R1),
                R2 = new SetSubscriberNotifier(R2),
                R3 = new SetSubscriberNotifier(R3),
                Ram = new SetSubscriberNotifier(Ram),
                Tmp = new SetSubscriberNotifier(Temp)
            };
            
            var cpuInput = new CpuInput
            {
                Caez = () => CaezRegister.Output,
                Ir = () => InstructionRegister.Output
            };

            CentralProcessingUnit = new CentralProcessingUnit(clock, stepper, cpuEnables, cpuSets, cpuInput, bus1Set, ArithmeticLogicUnit, and, or, not, decoder);  
        }   
        
        public void Step()
        {
            CentralProcessingUnit.Step();
            Bus.Apply();
        }

        public ICentralProcessingUnit CentralProcessingUnit { get; }
        
        // Main Bus Loop for the Computer
        public IBus Bus { get; }

        // Four main registers for the CPU
        public IRegister<IByte> R0 { get; }
        public IRegister<IByte> R1 { get; }
        public IRegister<IByte> R2 { get; }
        public IRegister<IByte> R3 { get; }

        public IRegister<IByte> InstructionRegister { get; }
        public IRegister<IByte> InstructionAddressRegister { get; }

        public Ram Ram { get; }
        
        // ALU related registers
        public IRegister<IByte> Temp { get; }
        public IBus1 Bus1 { get; }
        public IRegister<IByte> Acc { get; }
        public IRegisterArithmeticLogicUnit ArithmeticLogicUnit { get; }
        public IRegister<Caez> CaezRegister { get; }
        
        // IO
        public IClock IoClock { get; }
    }
}