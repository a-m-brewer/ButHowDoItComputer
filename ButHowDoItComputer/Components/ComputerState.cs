using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class ComputerState<TBusDataType> : IComputerState<TBusDataType> where TBusDataType : IBusDataType
    {
        public ComputerState(
            IRegisterFactory<TBusDataType> registerFactory, 
            IRam ram, 
            IBus1Factory bus1, 
            IArithmeticLogicUnitFactory<TBusDataType> aluFactory, 
            ICaezRegisterFactory caezRegisterFactory,
            IRegisterFactory<bool> bitRegisterFactory,
            IBus<TBusDataType> bus,
            IBus<TBusDataType> ioBus,
            IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            Io = new IoPinStates<TBusDataType>(busDataTypeFactory);
            Bus = bus;
            Bus.AddByte(input =>
            {
                Io.Bus.UpdateData(new BusMessage<TBusDataType> {Name = nameof(Bus), Data = input});
                Io.Bus.UpdateSubs();
            });
            
            Io.Bus.AddByte(input =>
            {
                Bus.UpdateData(new BusMessage<TBusDataType> {Name = nameof(Io), Data = input});
            });
            
            GeneralPurposeRegisters = Enumerable.Range(0, 4).Select(index =>
            {
                var name = $"GeneralPurposeRegister_{index}";
                var register = registerFactory.Create(data =>
                {
                    bus.UpdateData(new BusMessage<TBusDataType> {Data = data, Name = name});
                    bus.UpdateSubs();
                }, name);
                bus.AddRegister(register);
                return register;
            }).ToList();
            Ir = registerFactory.Create(data => {}, nameof(Ir));
            bus.AddRegister(Ir);
            Iar = registerFactory.Create(data =>
            {
                bus.UpdateData(new BusMessage<TBusDataType> {Data = data, Name = nameof(Iar)});
                bus.UpdateSubs();
            }, nameof(Iar));
            bus.AddRegister(Iar);
            
            Acc = registerFactory.Create(b =>
            {
                // for some reason updating subs here blows up, this is being updated in Computer
                bus.UpdateData(new BusMessage<TBusDataType> {Data = b, Name = nameof(Acc)});
            }, nameof(Acc));
            
            Alu = aluFactory.Create(input =>
            {
                Acc.Input = input;
                Acc.Apply();
            }, caez =>
            {
                Flags.Input = caez;
                Flags.Apply();
            });

            CarryInTmp = bitRegisterFactory.Create(output => { Alu.CarryIn = output; }, nameof(CarryInTmp));
            CarryInTmp.Enable = true;

            Bus1 = bus1.Create(updateAlu =>
            {
                Alu.InputB = updateAlu;
                Alu.Apply();
            });
            
            Tmp = registerFactory.Create(wire =>
            {
                Bus1.Input = wire;
                Bus1.Apply();
            }, nameof(Tmp));
            bus.AddRegister(Tmp);
            
            Flags = caezRegisterFactory.Create(data =>
            {
                CarryInTmp.Input = data.C;
                CarryInTmp.Apply();
            }, nameof(Flags));
            Flags.Enable = true;
            
            Ram = ram;
            Bus.AddRegister(ram.MemoryAddressRegister);

            bus.AddByte(inputByte =>
            {
                Alu.InputA = inputByte;
                Alu.Apply();
            });
        }

        public IRegister<Caez> Flags { get; }

        public List<IRegister<TBusDataType>> GeneralPurposeRegisters { get; }
        public IRegister<TBusDataType> Ir { get; }
        public IRegister<TBusDataType> Iar { get; }
        public IRegister<TBusDataType> Acc { get; }
        public IRam Ram { get; }
        public IRegister<TBusDataType> Tmp { get; }
        public IBus1 Bus1 { get; }
        public IArithmeticLogicUnit<TBusDataType> Alu { get; }
        public IBus<TBusDataType> Bus { get; }
        // This register does not exist in the book but is required to make this work
        // Going to steal the comment from simple-computer github
        // -----------
        // We will add a new memory bit called
        // "Carry Temp" that goes between the Carry Flag and the enabler
        // we just added above. It will be set in step 4, the same time that the TMP register gets
        // set. Thus, the ALU instruction will have a carry input that cannot change during step 5.
        // -----------
        public IRegister<bool> CarryInTmp { get; set; }
        
        // IO
        public IoPinStates<TBusDataType> Io { get; set; }

        public void UpdatePins(PinStates pinStates)
        {
            UpdatePins(GeneralPurposeRegisters, pinStates.GeneralPurposeRegisters); 
            UpdatePins(Ir, pinStates.Ir);
            UpdatePins(Iar, pinStates.Iar);
            UpdatePins(Acc, pinStates.Acc);
            UpdatePins(Ram, pinStates.Ram);
            UpdatePins(Tmp, pinStates.Tmp);
            Ram.MemoryAddressRegister.Set = pinStates.Mar;
            Bus1.Set = pinStates.Bus1;
            Alu.Op = pinStates.Op;
            Flags.Set = pinStates.Flags;
            CarryInTmp.Set = pinStates.CarryInTmp;
            UpdateIo(pinStates);
        }
        private void UpdatePins(IReadOnlyList<IRegister<TBusDataType>> parts, IReadOnlyList<SetEnable> states)
        {
            for (var i = 0; i < states.Count; i++)
            {
                UpdatePins(parts[i], states[i]);
            }
        }

        private void UpdatePins(IPinPart pinPart, SetEnable setEnable)
        {
            pinPart.Set = setEnable.Set;
            pinPart.Enable = setEnable.Enable;
        }

        private void UpdateIo(PinStates pinStates)
        {
            Io.Clk.Enable = pinStates.IoClk.Enable;
            Io.Clk.Set = pinStates.IoClk.Set;
            Io.DataAddress = pinStates.IoDataAddress;
            Io.InputOutput = pinStates.IoInputOutput;
        }
    }
}