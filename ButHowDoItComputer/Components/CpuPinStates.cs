using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class CpuPinStates<TBusDataType> : ICpuPinStates<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IAnd _and;
        private readonly IByteFactory _byteFactory;
        private Caez _caez;
        private readonly IClock _clock;
        private readonly IDecoder _decoder;
        private TBusDataType _instruction;
        private readonly INot _not;
        private readonly IOr _or;
        private readonly IStepper _stepper;

        public CpuPinStates(
            IClock clock,
            IStepper stepper,
            TBusDataType instruction,
            Caez caez,
            IAnd and,
            IOr or,
            INot not,
            IDecoder decoder,
            IByteFactory byteFactory)
        {
            _clock = clock;
            _stepper = stepper;
            _instruction = instruction;
            _caez = caez;
            _and = and;
            _or = or;
            _not = not;
            _decoder = decoder;
            _byteFactory = byteFactory;
        }

        private PinStates PinStates { get; set; } = new PinStates();

        // steps
        private bool Step1 => PinStates.StepperOutput[0];
        private bool Step1E => _and.ApplyParams(Step1, PinStates.ClockOutput.ClkE);
        private bool Step1S => _and.ApplyParams(Step1, PinStates.ClockOutput.ClkS);

        private bool Step2 => PinStates.StepperOutput[1];
        private bool Step2E => _and.ApplyParams(Step2, PinStates.ClockOutput.ClkE);
        private bool Step2S => _and.ApplyParams(Step2, PinStates.ClockOutput.ClkS);

        private bool Step3 => PinStates.StepperOutput[2];
        private bool Step3E => _and.ApplyParams(Step3, PinStates.ClockOutput.ClkE);
        private bool Step3S => _and.ApplyParams(Step3, PinStates.ClockOutput.ClkS);

        private bool Step4 => PinStates.StepperOutput[3];
        private bool Step4E => _and.ApplyParams(Step4, PinStates.ClockOutput.ClkE);
        private bool Step4S => _and.ApplyParams(Step4, PinStates.ClockOutput.ClkS);

        private bool Step5 => PinStates.StepperOutput[4];
        private bool Step5E => _and.ApplyParams(Step5, PinStates.ClockOutput.ClkE);
        private bool Step5S => _and.ApplyParams(Step5, PinStates.ClockOutput.ClkS);

        private bool Step6 => PinStates.StepperOutput[5];
        private bool Step6E => _and.ApplyParams(Step6, PinStates.ClockOutput.ClkE);
        private bool Step6S => _and.ApplyParams(Step6, PinStates.ClockOutput.ClkS);

        private bool Step7 => PinStates.StepperOutput[6];

        public PinStates Step(TBusDataType instruction, Caez flags)
        {
            _instruction = instruction;
            _caez = flags;
            return Step();
        }
        
        public PinStates Step()
        {
            // reset the pin states so nothing is from previous state
            PinStates = new PinStates();

            PinStates.ClockOutput = _clock.Cycle();
            PinStates.StepperOutput = _stepper.Step(PinStates.ClockOutput.Clk);

            Update3X8();

            UpdateStep1Pins();
            UpdateStep2Pins();
            UpdateStep3Pins();
            UpdateStep4Pins();
            UpdateStep5Pins();
            UpdateStep6Pins();

            UpdateGeneralPurposeRegisters(PinStates.RegA.Enable, PinStates.RegB.Enable, PinStates.RegB.Set);
            UpdateIoAllSteps();
            
            return PinStates;
        }

        private void UpdateStep1Pins()
        {
            // enable
            PinStates.Bus1 = NewPinState(PinStates.Bus1, Step1);
            PinStates.Iar.Enable = NewPinState(PinStates.Iar.Enable, Step1E);

            // set
            PinStates.Mar = NewPinState(PinStates.Mar, Step1S);
            PinStates.Acc.Set = NewPinState(PinStates.Acc.Set, Step1S);
        }

        private void UpdateStep2Pins()
        {
            // enable
            PinStates.Ram.Enable = NewPinState(PinStates.Ram.Enable, Step2E);

            // set
            PinStates.Ir.Set = NewPinState(PinStates.Ir.Set, Step2S);
        }

        private void UpdateStep3Pins()
        {
            // enable
            PinStates.Acc.Enable = NewPinState(PinStates.Acc.Enable, Step3E);

            // sets
            PinStates.Iar.Set = NewPinState(PinStates.Iar.Set, Step3S);
        }

        private void UpdateStep4Pins()
        {
            UpdateAluStep4();
            UpdateLoadStep4();
            UpdateDataStep4();
            UpdateJumpRegisterStep4();
            UpdateJumpStep4();
            UpdateJumpIfStep4();
            UpdateClearStep4();
            UpdateIoStep4();
        }

        private void UpdateAluStep4()
        {
            // setup
            var step4AndIr0 = _and.ApplyParams(Step4, _instruction[0]);

            // enables
            PinStates.RegB.Enable = NewPinState(PinStates.RegB.Enable, step4AndIr0);

            // sets
            PinStates.Tmp.Set = NewPinState(PinStates.Tmp.Set, ClkSAnd(step4AndIr0));
            PinStates.CarryInTmp = NewPinState(PinStates.CarryInTmp, ClkSAnd(step4AndIr0));
        }

        private void UpdateLoadStep4()
        {
            // setup
            var step43X80 = _and.ApplyParams(Step4, PinStates.ThreeXEight[0]);
            var step43X81 = _and.ApplyParams(Step4, PinStates.ThreeXEight[1]);

            var newState = _or.ApplyParams(step43X80, step43X81);

            // enables
            PinStates.RegA.Enable = NewPinState(PinStates.RegA.Enable, newState);

            // sets
            PinStates.Mar = NewPinState(PinStates.Mar, ClkSAnd(newState));
        }

        private void UpdateDataStep4()
        {
            // setup
            var step43X82 = _and.ApplyParams(Step4, PinStates.ThreeXEight[2]);

            // enables
            PinStates.Bus1 = NewPinState(PinStates.Bus1, step43X82);
            PinStates.Iar.Enable = NewPinState(PinStates.Iar.Enable, ClkEAnd(step43X82));

            // sets
            PinStates.Mar = NewPinState(PinStates.Mar, ClkSAnd(step43X82));
            PinStates.Acc.Set = NewPinState(PinStates.Acc.Set, ClkSAnd(step43X82));
        }

        private void UpdateJumpRegisterStep4()
        {
            // setup
            var step43X83 = _and.ApplyParams(Step4, PinStates.ThreeXEight[3]);

            // enables
            PinStates.RegB.Enable = NewPinState(PinStates.RegB.Enable, step43X83);

            // sets
            PinStates.Iar.Set = NewPinState(PinStates.Iar.Set, ClkSAnd(step43X83));
        }

        private void UpdateJumpStep4()
        {
            // setup
            var step43X84 = _and.ApplyParams(Step4, PinStates.ThreeXEight[4]);

            // enables
            PinStates.Iar.Enable = NewPinState(PinStates.Iar.Enable, ClkEAnd(step43X84));

            // sets
            PinStates.Mar = NewPinState(PinStates.Mar, ClkSAnd(step43X84));
        }

        private void UpdateJumpIfStep4()
        {
            // setup
            var step43X85 = _and.ApplyParams(Step4, PinStates.ThreeXEight[5]);

            // enables
            PinStates.Bus1 = NewPinState(PinStates.Bus1, step43X85);
            PinStates.Iar.Enable = NewPinState(PinStates.Iar.Enable, ClkEAnd(step43X85));

            // sets
            PinStates.Mar = NewPinState(PinStates.Mar, ClkSAnd(step43X85));
            PinStates.Acc.Set = NewPinState(PinStates.Acc.Set, ClkSAnd(step43X85));
        }

        private void UpdateClearStep4()
        {
            // setup 
            var step43X86 = _and.ApplyParams(Step4, PinStates.ThreeXEight[6]);
            
            // enable
            PinStates.Bus1 = NewPinState(PinStates.Bus1, step43X86);
            
            // set
            PinStates.Flags = NewPinState(PinStates.Flags, ClkSAnd(step43X86));
        }

        private void UpdateIoStep4()
        {
            // setup
            var step43X87Ir4 = _and.ApplyParams(Step4, PinStates.ThreeXEight[7], _instruction[4]);

            PinStates.RegB.Enable = NewPinState(PinStates.RegB.Enable, step43X87Ir4);
            
            // sets
            PinStates.IoClk.Set = NewPinState(PinStates.IoClk.Set, ClkSAnd(step43X87Ir4));
        }

        private void UpdateStep5Pins()
        {
            UpdateAluStep5();
            UpdateLoadStep5();
            UpdateDataStep5();
            UpdateJumpStep5();
            UpdateJumpIfStep5();
            UpdateIoStep5();
        }

        private void UpdateAluStep5()
        {
            // setup
            var step5AndIr0 = _and.ApplyParams(Step5, _instruction[0]);

            // enables
            PinStates.RegA.Enable = NewPinState(PinStates.RegA.Enable, step5AndIr0);

            // sets
            PinStates.Acc.Set = NewPinState(PinStates.Acc.Set, ClkSAnd(step5AndIr0));
            PinStates.Flags = NewPinState(PinStates.Flags, ClkSAnd(step5AndIr0));

            UpdateOpCode();
        }

        private void UpdateLoadStep5()
        {
            // setup
            var storeFlag = _and.ApplyParams(Step5, PinStates.ThreeXEight[0]);
            var loadFlag = _and.ApplyParams(Step5, PinStates.ThreeXEight[1]);

            // enables
            PinStates.RegB.Enable = NewPinState(PinStates.RegB.Enable, loadFlag);
            PinStates.Ram.Enable = NewPinState(PinStates.Ram.Enable, ClkEAnd(storeFlag));

            // sets
            PinStates.RegB.Set = NewPinState(PinStates.RegB.Set, storeFlag);
            PinStates.Ram.Set = NewPinState(PinStates.Ram.Set, ClkSAnd(loadFlag));
        }

        private void UpdateDataStep5()
        {
            // setup
            var step53X82 = _and.ApplyParams(Step5, PinStates.ThreeXEight[2]);

            // enables
            PinStates.Ram.Enable = NewPinState(PinStates.Ram.Enable, ClkEAnd(step53X82));

            // sets
            PinStates.RegB.Set = NewPinState(PinStates.RegB.Set, step53X82);
        }

        private void UpdateJumpStep5()
        {
            // setup
            var step53X84 = _and.ApplyParams(Step5, PinStates.ThreeXEight[4]);

            // enables
            PinStates.Ram.Enable = NewPinState(PinStates.Ram.Enable, ClkEAnd(step53X84));

            // sets
            PinStates.Iar.Set = NewPinState(PinStates.Iar.Set, ClkSAnd(step53X84));
        }

        private void UpdateJumpIfStep5()
        {
            // setup
            var step53X85 = _and.ApplyParams(Step5, PinStates.ThreeXEight[5]);

            // enables
            PinStates.Acc.Enable = NewPinState(PinStates.Acc.Enable, ClkEAnd(step53X85));

            // sets
            PinStates.Iar.Set = NewPinState(PinStates.Iar.Set, ClkSAnd(step53X85));
        }

        private void UpdateIoStep5()
        {
            var step53X87NotIr4 = _and.ApplyParams(Step5, PinStates.ThreeXEight[7], _not.Apply(_instruction[4]));

            // sets
            PinStates.RegB.Set = NewPinState(PinStates.RegB.Set, step53X87NotIr4);
            PinStates.IoClk.Enable = NewPinState(PinStates.IoClk.Enable, ClkEAnd(step53X87NotIr4));
        }

        private void UpdateOpCode()
        {
            PinStates.Op.One = _and.ApplyParams(Step5, _instruction[0], _instruction[1]);
            PinStates.Op.Two = _and.ApplyParams(Step5, _instruction[0], _instruction[2]);
            PinStates.Op.Three = _and.ApplyParams(Step5, _instruction[0], _instruction[3]);
        }

        private void UpdateStep6Pins()
        {
            UpdateAluStep6();
            UpdateDataStep6();
            UpdateJumpIfStep6();
        }

        private void UpdateAluStep6()
        {
            // setup
            var notAndIr123 = _not.Apply(_and.ApplyParams(_instruction[1], _instruction[2], _instruction[3]));
            var step6AndIr0AndNotAndIr123 = _and.ApplyParams(Step6, _instruction[0], notAndIr123);

            // enable
            PinStates.Acc.Enable = NewPinState(PinStates.Acc.Enable, ClkEAnd(step6AndIr0AndNotAndIr123));

            // set
            PinStates.RegB.Set = NewPinState(PinStates.RegB.Set, step6AndIr0AndNotAndIr123);
        }

        private void UpdateDataStep6()
        {
            // setup
            var step63X82 = _and.ApplyParams(Step6, PinStates.ThreeXEight[2]);

            // enable
            PinStates.Acc.Enable = NewPinState(PinStates.Acc.Enable, ClkEAnd(step63X82));

            // set
            PinStates.Iar.Set = NewPinState(PinStates.Iar.Set, ClkSAnd(step63X82));
        }

        private void UpdateJumpIfStep6()
        {
            var cAndIr4 = _and.ApplyParams(_caez.C, _instruction[4]);
            var aAndIr5 = _and.ApplyParams(_caez.A, _instruction[5]);
            var eAndIr6 = _and.ApplyParams(_caez.E, _instruction[6]);
            var zAndIr7 = _and.ApplyParams(_caez.Z, _instruction[7]);

            // setup
            var orCaez = _or.ApplyParams(cAndIr4, aAndIr5, eAndIr6, zAndIr7);
            var step63X85AndCaez = _and.ApplyParams(
                Step6,
                PinStates.ThreeXEight[5],
                orCaez);

            // enables
            PinStates.Ram.Enable = NewPinState(PinStates.Ram.Enable, ClkEAnd(step63X85AndCaez));

            // sets
            PinStates.Iar.Set = NewPinState(PinStates.Iar.Set, ClkSAnd(step63X85AndCaez));
        }

        public void UpdateGeneralPurposeRegisters(bool regAEnable, bool regBEnable, bool regBSet)
        {
            var decoderEnableRegA = _decoder.Apply(_instruction[4], _instruction[5]);
            var decoderEnableRegB = _decoder.Apply(_instruction[6], _instruction[7]);
            var decoderSetRegB = _decoder.Apply(_instruction[6], _instruction[7]);

            var clkE = PinStates.ClockOutput.ClkE;
            var clkS = PinStates.ClockOutput.ClkS;

            var r0Ea = _and.ApplyParams(decoderEnableRegA[0], regAEnable, clkE);
            var r1Ea = _and.ApplyParams(decoderEnableRegA[1], regAEnable, clkE);
            var r2Ea = _and.ApplyParams(decoderEnableRegA[2], regAEnable, clkE);
            var r3Ea = _and.ApplyParams(decoderEnableRegA[3], regAEnable, clkE);

            var r0Eb = _and.ApplyParams(decoderEnableRegB[0], regBEnable, clkE);
            var r1Eb = _and.ApplyParams(decoderEnableRegB[1], regBEnable, clkE);
            var r2Eb = _and.ApplyParams(decoderEnableRegB[2], regBEnable, clkE);
            var r3Eb = _and.ApplyParams(decoderEnableRegB[3], regBEnable, clkE);

            var r0S = _and.ApplyParams(decoderSetRegB[0], regBSet, clkS);
            var r1S = _and.ApplyParams(decoderSetRegB[1], regBSet, clkS);
            var r2S = _and.ApplyParams(decoderSetRegB[2], regBSet, clkS);
            var r3S = _and.ApplyParams(decoderSetRegB[3], regBSet, clkS);

            PinStates.GeneralPurposeRegisters[0].Enable = _or.ApplyParams(r0Ea, r0Eb);
            PinStates.GeneralPurposeRegisters[1].Enable = _or.ApplyParams(r1Ea, r1Eb);
            PinStates.GeneralPurposeRegisters[2].Enable = _or.ApplyParams(r2Ea, r2Eb);
            PinStates.GeneralPurposeRegisters[3].Enable = _or.ApplyParams(r3Ea, r3Eb);

            PinStates.GeneralPurposeRegisters[0].Set = r0S;
            PinStates.GeneralPurposeRegisters[1].Set = r1S;
            PinStates.GeneralPurposeRegisters[2].Set = r2S;
            PinStates.GeneralPurposeRegisters[3].Set = r3S;
        }

        private void Update3X8()
        {
            var threeXEightDecoded = _decoder.Apply(_instruction[1], _instruction[2], _instruction[3]);
            var notAluFlag = _not.Apply(_instruction[0]);
            PinStates.ThreeXEight =
                _byteFactory.Create(threeXEightDecoded.Select(bit => _and.ApplyParams(notAluFlag, bit)).ToArray());
        }

        private void UpdateIoAllSteps()
        {
            PinStates.IoInputOutput = NewPinState(PinStates.IoInputOutput, _instruction[4]);
            PinStates.IoDataAddress = NewPinState(PinStates.IoDataAddress, _instruction[5]);
        }

        private bool NewPinState(bool current, bool newData)
        {
            return _or.ApplyParams(current, newData);
        }

        private bool ClkEAnd(bool update)
        {
            return _and.ApplyParams(PinStates.ClockOutput.ClkE, update);
        }

        private bool ClkSAnd(bool update)
        {
            return _and.ApplyParams(PinStates.ClockOutput.ClkS, update);
        }
    }

    public class PinStates
    {
        public List<SetEnable> GeneralPurposeRegisters = new List<SetEnable>
        {
            new SetEnable(),
            new SetEnable(),
            new SetEnable(),
            new SetEnable()
        };

        public bool Bus1 { get; set; }
        public StepperOutput StepperOutput { get; set; } = new StepperOutput();
        public ClockOutput ClockOutput { get; set; } = new ClockOutput();
        public SetEnable Iar { get; set; } = new SetEnable();
        public bool Mar { get; set; }
        public SetEnable Acc { get; set; } = new SetEnable();
        public SetEnable Ram { get; set; } = new SetEnable();
        public SetEnable Ir { get; set; } = new SetEnable {Enable = true};

        public SetEnable RegA { get; set; } = new SetEnable();
        public SetEnable RegB { get; set; } = new SetEnable();

        public SetEnable Tmp { get; set; } = new SetEnable {Enable = true};

        public Op Op { get; set; } = new Op();
        public bool Flags { get; set; }

        public IByte ThreeXEight { get; set; } = new Byte();
        
        public bool CarryInTmp { get; set; }

        public SetEnable IoClk { get; set; } = new SetEnable();

        public bool IoInputOutput { get; set; }
        public bool IoDataAddress { get; set; }
    }

    public class SetEnable
    {
        public bool Set { get; set; }
        public bool Enable { get; set; }
    }
}