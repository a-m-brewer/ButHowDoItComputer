using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class ComputerState : IComputerState
    {
        public ComputerState(
            IByteRegisterFactory byteRegisterFactory, 
            IRam ram, 
            IBus1Factory bus1, 
            IArithmeticLogicUnitFactory aluFactory, 
            ICaezRegisterFactory caezRegisterFactory,
            IRegisterFactory<bool> bitRegisterFactory,
            IBus<IByte> bus)
        {
            Bus = bus;
            GeneralPurposeRegisters = Enumerable.Range(0, 4).Select(index =>
            {
                var name = $"GeneralPurposeRegister_{index}";
                var register = byteRegisterFactory.Create(data =>
                {
                    bus.UpdateData(new BusMessage<IByte> {Data = data, Name = name});
                    bus.UpdateSubs();
                }, name);
                bus.AddRegister(register);
                return register;
            }).ToList();
            Ir = byteRegisterFactory.Create(data => {}, nameof(Ir));
            bus.AddRegister(Ir);
            Iar = byteRegisterFactory.Create(data =>
            {
                bus.UpdateData(new BusMessage<IByte> {Data = data, Name = nameof(Iar)});
                bus.UpdateSubs();
            }, nameof(Iar));
            bus.AddRegister(Iar);
            
            Acc = byteRegisterFactory.Create(b =>
            {
                // for some reason updating subs here blows up, this is being updated in Computer
                bus.UpdateData(new BusMessage<IByte> {Data = b, Name = nameof(Acc)});
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
            
            Tmp = byteRegisterFactory.Create(wire =>
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

        public List<IRegister<IByte>> GeneralPurposeRegisters { get; }
        public IRegister<IByte> Ir { get; }
        public IRegister<IByte> Iar { get; }
        public IRegister<IByte> Acc { get; }
        public IRam Ram { get; }
        public IRegister<IByte> Tmp { get; }
        public IBus1 Bus1 { get; }
        public IArithmeticLogicUnit Alu { get; }
        public IBus<IByte> Bus { get; }
        public IRegister<bool> CarryInTmp { get; set; }

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
        }
        private void UpdatePins(IReadOnlyList<IRegister<IByte>> parts, IReadOnlyList<SetEnable> states)
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
    }
}