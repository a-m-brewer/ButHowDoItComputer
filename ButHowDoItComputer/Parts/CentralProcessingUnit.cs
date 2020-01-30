using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class CentralProcessingUnit : ICentralProcessingUnit
    {
        private readonly IClock _clock;
        private readonly IStepper _stepper;
        private readonly CpuEnables _cpuEnables;
        private readonly CpuSets _cpuSets;
        private readonly CpuInput _cpuInput;
        private readonly ICpuSubscriberNotifier<IBit> _bus1Sub;
        private readonly ICpuSubscriberNotifier<Op> _aluOpSub;
        private readonly IAnd _and;
        private readonly IOr _or;
        private readonly INot _not;
        private readonly IDecoder _decoder;
        private StepperOutput _stepperOutput;

        public CentralProcessingUnit(
            IClock clock, 
            IStepper stepper, 
            CpuEnables cpuEnables, 
            CpuSets cpuSets,
            CpuInput cpuInput,
            ICpuSubscriberNotifier<IBit> bus1Sub,
            ICpuSubscriberNotifier<Op> aluOpSub,
            IAnd and,
            IOr or,
            INot not,
            IDecoder decoder)
        {
            _clock = clock;
            _stepper = stepper;
            _cpuEnables = cpuEnables;
            _cpuSets = cpuSets;
            _cpuInput = cpuInput;
            _bus1Sub = bus1Sub;
            _aluOpSub = aluOpSub;
            _and = and;
            _or = or;
            _not = not;
            _decoder = decoder;
        }

        public void Step()
        {
            var cycleResult = _clock.Cycle();
            _stepperOutput = _stepper.Step(cycleResult.Clk);

            var threeXEightOutput = ThreeXEightOutput();
            var threeXEightOutputAndStep4Ir4 = ThreeXEightOutputAndStep4Ir4(threeXEightOutput);
            var threeXEightOutputAndStep5NotIr4 = ThreeXEightOutputAndStep5NotIr4(threeXEightOutput);

            var caezOuput = CreateCaezOutput();
            var orCaez = _or.Apply(caezOuput.C, caezOuput.A, caezOuput.E, caezOuput.Z);
            var threeXEightStep6OrCaez = _and.Apply(threeXEightOutput[5], _stepperOutput[5], orCaez);
            
            var andIr123 = _and.Apply(_cpuInput.Ir.Output[1], _cpuInput.Ir.Output[2], _cpuInput.Ir.Output[3]);
            var notAndIr123 = _not.Apply(andIr123);

            var threeXEight2AndStep6 = _and.Apply(threeXEightOutput[2], _stepperOutput[5]);

            var naIr123AndStep6AndStep0 = _and.Apply(notAndIr123, _cpuInput.Ir.Output[0], _stepperOutput[5]);

            var step4AndIr0 = _and.Apply(_cpuInput.Ir.Output[0], _stepperOutput[3]);
            var step5AndIr0 = _and.Apply(_cpuInput.Ir.Output[0], _stepperOutput[4]);

            var step5AndThreeXEight7NotIr4 =
                _and.Apply(_stepperOutput[4], threeXEightOutput[7], _not.Apply(_cpuInput.Ir.Output[4]));

            var step4AndThreeXEight7AndIr4 =
                _and.Apply(_stepperOutput[3], threeXEightOutput[7], _cpuInput.Ir.Output[4]);

            UpdateAluOpCode();
            
            var registerAEnable = RegisterAEnable(
                step5AndIr0, 
                threeXEightOutputAndStep4Ir4[0], 
                threeXEightOutputAndStep4Ir4[1]);
            
            var registerBEnable = RegisterBEnable(
                step4AndIr0,
                threeXEightOutputAndStep5NotIr4[1], 
                threeXEightOutputAndStep4Ir4[4],
                step4AndThreeXEight7AndIr4);

            var registerBSet = RegisterBSet(naIr123AndStep6AndStep0, threeXEightOutputAndStep5NotIr4[0],
                threeXEightOutputAndStep5NotIr4[2], step5AndThreeXEight7NotIr4);
            
            UpdateRegisters(registerAEnable, registerBEnable, registerBSet);
            
            UpdateIoClkEnable(threeXEightOutputAndStep5NotIr4[7]);
            
            UpdateBus1(_stepperOutput[0], threeXEightOutputAndStep4Ir4[2], threeXEightOutputAndStep4Ir4[5], threeXEightOutputAndStep4Ir4[6]);
            UpdateIarEnable(_stepperOutput[0], threeXEightOutputAndStep4Ir4[2], threeXEightOutputAndStep4Ir4[4], threeXEightOutputAndStep4Ir4[5]);
            UpdateMarSet(
                _stepperOutput[0], 
                threeXEightOutputAndStep4Ir4[0],
                threeXEightOutputAndStep4Ir4[1], 
                threeXEightOutputAndStep4Ir4[2], 
                threeXEightOutputAndStep4Ir4[4], 
                threeXEightOutputAndStep4Ir4[5]);
            UpdateAccSet(
                _stepperOutput[0], 
                step5AndIr0, 
                threeXEightOutputAndStep4Ir4[2], 
                threeXEightOutputAndStep4Ir4[5]);
            
            UpdateRamEnable(_stepperOutput[1], threeXEightOutputAndStep5NotIr4[0], threeXEightOutputAndStep5NotIr4[2], threeXEightOutputAndStep5NotIr4[4], threeXEightStep6OrCaez);
            UpdateIrSet(_stepperOutput[1]);
            
            UpdateAccEnable(_stepperOutput[2], naIr123AndStep6AndStep0, threeXEight2AndStep6, threeXEightOutputAndStep5NotIr4[5]);
            UpdateIarSet(
                _stepperOutput[2], 
                threeXEight2AndStep6, 
                threeXEightOutputAndStep4Ir4[4], 
                threeXEightOutputAndStep5NotIr4[4], 
                threeXEightOutputAndStep5NotIr4[5], 
                threeXEightStep6OrCaez);
            
            UpdateRamSet(threeXEightOutputAndStep5NotIr4[1]);
            UpdateTmpSet(step4AndIr0);
            UpdateFlagsSet(threeXEightOutputAndStep4Ir4[6], _and.Apply(_cpuInput.Ir.Output[0], _stepperOutput[4]));
            UpdateIoClkSet(step4AndThreeXEight7AndIr4);
            
            _cpuEnables.InputOutput.Update(_cpuInput.Ir.Output[4]);
            _cpuEnables.DataAddress.Update(_cpuInput.Ir.Output[5]);

            ApplyEnables();
            ApplySets();
        }

        private IBit[] ThreeXEightOutput()
        {
            var decoderOut = _decoder.Apply(_cpuInput.Ir.Output[1], _cpuInput.Ir.Output[2], _cpuInput.Ir.Output[3]);
            var result = decoderOut.Select(decoderBit => _and.Apply(decoderBit, _not.Apply(_cpuInput.Ir.Output[0]))).ToArray();
            return result;
        }

        private IBit[] ThreeXEightOutputAndStep4Ir4(IReadOnlyList<IBit> threeXEightOutput)
        {
            var output = new IBit[8];
            for (var i = 0; i < threeXEightOutput.Count; i++)
            {
                if (i == 7)
                {
                    output[i] = _and.Apply(_stepperOutput[3], threeXEightOutput[i], _cpuInput.Ir.Output[4]);
                }
                else
                {
                    output[i] = _and.Apply(_stepperOutput[3], threeXEightOutput[i]);
                }
            }

            return output;
        }

        private IBit[] ThreeXEightOutputAndStep5NotIr4(IReadOnlyList<IBit> threeXEightOutput)
        {
            var output = new IBit[8];
            for (var i = 0; i < threeXEightOutput.Count; i++)
            {
                if (i == 7)
                {
                    output[i] = _and.Apply(_stepperOutput[4], threeXEightOutput[i], _not.Apply(_cpuInput.Ir.Output[4]));
                }
                else
                {
                    output[i] = _and.Apply(_stepperOutput[4], threeXEightOutput[i]);
                }
            }

            return output;
        }

        private Caez CreateCaezOutput()
        {
            return new Caez
            {
                C = _and.Apply(_cpuInput.Caez.C, _cpuInput.Ir.Output[4]),
                A = _and.Apply(_cpuInput.Caez.A, _cpuInput.Ir.Output[5]),
                E = _and.Apply(_cpuInput.Caez.E, _cpuInput.Ir.Output[6]),
                Z = _and.Apply(_cpuInput.Caez.Z, _cpuInput.Ir.Output[7]),
            };
        }
        
        private void UpdateBus1(IBit step1, IBit two, IBit three, IBit four)
        {
            _bus1Sub.Update(_or.Apply(step1, two, three, four));
        }

        private void UpdateIarEnable(IBit step1, IBit two, IBit three, IBit four)
        {
            _cpuEnables.Iar.Update(CreateEnableUpdate(_or.Apply(step1, two, three, four)));
        }

        private void UpdateRamEnable(IBit step2, IBit one, IBit two, IBit three, IBit four)
        {
            _cpuEnables.Ram.Update(CreateEnableUpdate(_or.Apply(step2, one, two, three, four)));
        }

        private void UpdateAccEnable(IBit step3, IBit two, IBit three, IBit four)
        {
            _cpuEnables.Acc.Update(CreateEnableUpdate(_or.Apply(step3, two, three, four)));
        }

        private void UpdateIoClkEnable(IBit bit)
        {
            _cpuEnables.IoClk.Update(CreateEnableUpdate(bit));
        }

        private IBit RegisterAEnable(IBit one, IBit two, IBit three)
        {
            return _or.Apply(one, two, three);
        }
        
        private IBit RegisterBEnable(IBit one, IBit two, IBit three, IBit four)
        {
            return _or.Apply(one, two, three, four);
        }

        private void UpdateIrSet(IBit step2)
        {
            _cpuSets.Ir.Update(CreateSetUpdate(step2));
        }

        private void UpdateIoClkSet(IBit one)
        {
            _cpuSets.IoClk.Update(CreateSetUpdate(one));
        }

        private void UpdateRegisters(IBit regAEnable, IBit regBEnable, IBit regBSet)
        {
            var setDecoder = _decoder.Apply(_cpuInput.Ir.Output[6], _cpuInput.Ir.Output[7]).ToArray();
            var enableDecoder1 = _decoder.Apply(_cpuInput.Ir.Output[6], _cpuInput.Ir.Output[7]).ToArray();
            var enableDecoder2 = _decoder.Apply(_cpuInput.Ir.Output[4], _cpuInput.Ir.Output[5]).ToArray();

            var e0 = CreateEnableUpdate(enableDecoder1[0], regBEnable);
            var e1 = CreateEnableUpdate(enableDecoder1[1], regBEnable);
            var e2 = CreateEnableUpdate(enableDecoder1[2], regBEnable);
            var e3 = CreateEnableUpdate(enableDecoder1[3], regBEnable);
            
            var e4 = CreateEnableUpdate(enableDecoder2[0], regAEnable);
            var e5 = CreateEnableUpdate(enableDecoder2[1], regAEnable);
            var e6 = CreateEnableUpdate(enableDecoder2[2], regAEnable);
            var e7 = CreateEnableUpdate(enableDecoder2[3], regAEnable);
            
            var enableRegister0Input = _or.Apply(e0, e4);
            var enableRegister1Input = _or.Apply(e1, e5);
            var enableRegister2Input = _or.Apply(e2, e6);
            var enableRegister3Input = _or.Apply(e3, e7);
            
            _cpuEnables.R0.Update(enableRegister0Input);
            _cpuEnables.R1.Update(enableRegister1Input);
            _cpuEnables.R2.Update(enableRegister2Input);
            _cpuEnables.R3.Update(enableRegister3Input);

            var s0 = CreateSetUpdate(setDecoder[0], regBSet);
            var s1 = CreateSetUpdate(setDecoder[1], regBSet);
            var s2 = CreateSetUpdate(setDecoder[2], regBSet);
            var s3 = CreateSetUpdate(setDecoder[3], regBSet);
            
            _cpuSets.R0.Update(s0);
            _cpuSets.R1.Update(s1);
            _cpuSets.R2.Update(s2);
            _cpuSets.R3.Update(s3);
        }

        private void UpdateAluOpCode()
        {
            var one = _and.Apply( _cpuInput.Ir.Output[0], _stepperOutput[4], _cpuInput.Ir.Output[1]);
            var two = _and.Apply( _cpuInput.Ir.Output[0], _stepperOutput[4], _cpuInput.Ir.Output[2]);
            var three = _and.Apply( _cpuInput.Ir.Output[0], _stepperOutput[4], _cpuInput.Ir.Output[3]);

            _aluOpSub.Update(new Op {One = one, Two = two, Three = three});
        }

        private void UpdateMarSet(IBit one, IBit two, IBit three, IBit four, IBit five, IBit six)
        {
            _cpuSets.Mar.Update(CreateSetUpdate(_or.Apply(one, two, three, four, five, six)));
        }
        
        private void UpdateIarSet(IBit one, IBit two, IBit three, IBit four, IBit five, IBit six)
        {
            _cpuSets.Iar.Update(CreateSetUpdate(_or.Apply(one, two, three, four, five, six)));
        }

        private void UpdateAccSet(IBit one, IBit two, IBit three, IBit four)
        {
            _cpuSets.Acc.Update(CreateSetUpdate(_or.Apply(one, two, three, four)));
        }

        private void UpdateRamSet(IBit one)
        {
            _cpuSets.Ram.Update(CreateSetUpdate(one));
        }

        private void UpdateTmpSet(IBit one)
        {
            _cpuSets.Tmp.Update(CreateSetUpdate(one));
        }

        private void UpdateFlagsSet(IBit one, IBit two)
        {
            _cpuSets.Flags.Update(CreateSetUpdate(_or.Apply(one, two)));
        }

        private IBit RegisterBSet(IBit one, IBit two, IBit three, IBit four)
        {
            return _or.Apply(one, two, three, four);
        }

        private void ApplyEnables()
        {
            foreach (var cpuEnable in _cpuEnables)
            {
                cpuEnable.Apply();
            }
        }

        private void ApplySets()
        {
            foreach (var cpuSet in _cpuSets)
            {
                cpuSet.Apply();
            }
        }

        private IBit CreateEnableUpdate(params IBit[] other)
        {
            var tmpList = new List<IBit> {_clock.ClkE};
            tmpList.AddRange(other);
            var tmpArray = tmpList.ToArray();
            
            return _and.Apply(tmpArray);
        }

        private IBit CreateSetUpdate(params IBit[] other)
        {
            var tmpList = new List<IBit> {_clock.ClkS};
            tmpList.AddRange(other);
            var tmpArray = tmpList.ToArray();
            
            return _and.Apply(tmpArray);
        }
    }
}