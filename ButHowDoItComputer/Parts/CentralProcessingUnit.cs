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
    public class CentralProcessingUnit
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
        private IBit _eRegA;
        private IBit _sRegB;
        private IBit _eRegB;

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

            _eRegA = false.ToBit();
            _sRegB = false.ToBit();
            _eRegB = false.ToBit();
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

            var registerAEnable = RegisterAEnable(
                _and.Apply(_stepperOutput[4], _cpuInput.Ir.Output[0]), 
                threeXEightOutputAndStep4Ir4[0], 
                threeXEightOutputAndStep4Ir4[1]);
            
            var registerBEnable = RegisterBEnable(
                _and.Apply(_cpuInput.Ir.Output[0], _stepperOutput[4]),
                threeXEightOutputAndStep5NotIr4[1], 
                threeXEightOutputAndStep4Ir4[4],
                _and.Apply(_stepperOutput[3], threeXEightOutput[7], _cpuInput.Ir.Output[4]));
            
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
            
            // TODO: UpdateAccSet
            // TODO: UpdateRamSet
            // TODO: UpdateTmpSet
            // TODO: UpdateFlagsSet
            // TODO: RegisterBSet
            // TODO: UpdateIoClkSet
            
            ApplyEnables();
            ApplySets();
        }
        
        

        /// <summary>
        /// pg. 120
        /// </summary>
        private void UpdateAlu()
        {
            var stp4AndIr0 = _and.Apply(_stepperOutput[3], _cpuInput.Ir.Output[0]);
            var stp5AndIr0 = _and.Apply(_stepperOutput[4], _cpuInput.Ir.Output[0]);

            var andIr123 = _and.Apply(_cpuInput.Ir.Output[1], _cpuInput.Ir.Output[2], _cpuInput.Ir.Output[3]);
            var notAndIr123 = _not.Apply(andIr123);

            var notAndIr123AndIr0AndStp6 = _and.Apply(notAndIr123, _cpuInput.Ir.Output[0], _stepperOutput[5]);

            _eRegB = stp4AndIr0;
            _cpuSets.Tmp.Update(stp4AndIr0);

            // TODO: eRegA etc will need to be OR'd with the other inputs coming in the following chapters
            _eRegA = stp5AndIr0;
            _cpuSets.Acc.Update(stp5AndIr0);

            _sRegB = notAndIr123AndIr0AndStp6;
            _cpuEnables.Acc.Update(notAndIr123AndIr0AndStp6);

            var aluInput0 = _and.Apply(_cpuInput.Ir.Output[0], _cpuInput.Ir.Output[1], _stepperOutput[4]);
            var aluInput1 = _and.Apply(_cpuInput.Ir.Output[0], _cpuInput.Ir.Output[2], _stepperOutput[4]);
            var aluInput2 = _and.Apply(_cpuInput.Ir.Output[0], _cpuInput.Ir.Output[3], _stepperOutput[4]);

            _aluOpSub.Update(new Op {One = aluInput0, Two = aluInput1, Three = aluInput2});
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

        private void UpdateMarSet(IBit one, IBit two, IBit three, IBit four, IBit five, IBit six)
        {
            _cpuSets.Mar.Update(CreateSetUpdate(one, two, three, four, five, six));
        }
        
        private void UpdateIarSet(IBit one, IBit two, IBit three, IBit four, IBit five, IBit six)
        {
            _cpuSets.Iar.Update(CreateSetUpdate(one, two, three, four, five, six));
        }

        /// <summary>
        /// TODO: if this remains a permanent wiring, create some unit tests. read pg. 105 for intended output
        ///
        /// pg. 119
        /// When the book refers to register A and Register B it means the registers selected from the
        ///
        /// 1 000 00 01
        ///
        /// Second from last group of bits means register A is R0
        /// Last 2 bits means that register B is R1
        /// </summary>
        private void WireAdd()
        {
            var setDecoder = _decoder.Apply(_cpuInput.Ir.Output[5], _cpuInput.Ir.Output[6]).ToArray();
            var enableDecoder1 = _decoder.Apply(_cpuInput.Ir.Output[5], _cpuInput.Ir.Output[6]).ToArray();
            var enableDecoder2 = _decoder.Apply(_cpuInput.Ir.Output[3], _cpuInput.Ir.Output[4]).ToArray();

            // TODO: set this to what it is meant to be once that has been found out

            _cpuEnables.Acc.Update(CreateEnableUpdate(_stepperOutput[5]));
            _cpuSets.Acc.Update(CreateSetUpdate(_stepperOutput[4]));
            _cpuSets.Tmp.Update(CreateSetUpdate(_stepperOutput[3]));

            var eAnd0 = CreateEnableUpdate(enableDecoder1[0], _sRegB);
            var eAnd1 = CreateEnableUpdate(enableDecoder1[1], _sRegB);
            var eAnd2 = CreateEnableUpdate(enableDecoder1[2], _sRegB);
            var eAnd3 = CreateEnableUpdate(enableDecoder1[3], _sRegB);
            
            var eAnd4 = CreateEnableUpdate(enableDecoder2[0], _eRegA);
            var eAnd5 = CreateEnableUpdate(enableDecoder2[1], _eRegA);
            var eAnd6 = CreateEnableUpdate(enableDecoder2[2], _eRegA);
            var eAnd7 = CreateEnableUpdate(enableDecoder2[3], _eRegA);

            var r0In = _or.Apply(eAnd0, eAnd4);
            var r1In = _or.Apply(eAnd1, eAnd5);
            var r2In = _or.Apply(eAnd2, eAnd6);
            var r3In = _or.Apply(eAnd3, eAnd7);
            
            _cpuEnables.R0.Update(r0In);
            _cpuEnables.R1.Update(r1In);
            _cpuEnables.R2.Update(r2In);
            _cpuEnables.R3.Update(r3In);

            var sAnd0 = CreateSetUpdate(setDecoder[0], _eRegB);
            var sAnd1 = CreateSetUpdate(setDecoder[1], _eRegB);
            var sAnd2 = CreateSetUpdate(setDecoder[2], _eRegB);
            var sAnd3 = CreateSetUpdate(setDecoder[3], _eRegB);
            
            _cpuSets.R0.Update(sAnd0);
            _cpuSets.R1.Update(sAnd1);
            _cpuSets.R2.Update(sAnd2);
            _cpuSets.R3.Update(sAnd3);
        }

        /// <summary>
        /// pg. 112
        /// </summary>
        private void WireInstructionRegister()
        {
            _bus1Sub.Update(_stepperOutput[0]);
            
            _cpuEnables.Iar.Update(CreateEnableUpdate(_stepperOutput[0]));
            _cpuEnables.Ram.Update(CreateEnableUpdate(_stepperOutput[1]));
            _cpuEnables.Acc.Update(CreateEnableUpdate(_stepperOutput[2]));
            
            _cpuSets.Ir.Update(CreateSetUpdate(_stepperOutput[1]));
            _cpuSets.Mar.Update(CreateSetUpdate(_stepperOutput[0]));
            _cpuSets.Iar.Update(CreateSetUpdate(_stepperOutput[2]));
            _cpuSets.Acc.Update(CreateSetUpdate(_stepperOutput[0]));
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