using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class CentralProcessingUnit
    {
        private readonly IClock _clock;
        private readonly IStepper _stepper;
        private readonly CpuEnables _cpuEnables;
        private readonly CpuSets _cpuSets;
        private readonly ICpuSubscriberNotifier<IBit> _bus1Sub;
        private readonly IAnd _and;
        private StepperOutput _stepperOutput;

        public CentralProcessingUnit(
            IClock clock, 
            IStepper stepper, 
            CpuEnables cpuEnables, 
            CpuSets cpuSets,
            ICpuSubscriberNotifier<IBit> bus1Sub,
            IAnd and)
        {
            _clock = clock;
            _stepper = stepper;
            _cpuEnables = cpuEnables;
            _cpuSets = cpuSets;
            _bus1Sub = bus1Sub;
            _and = and;
        }

        public void Step()
        {
            var cycleResult = _clock.Cycle();
            _stepperOutput = _stepper.Step(cycleResult.Clk);
            
            // TODO: route cpu updates here
            WireAdd();
            WireInstructionRegister();
            
            ApplyEnables();
            ApplySets();
        }

        /// <summary>
        /// TODO: if this remains a permanent wiring, create some unit tests. read pg. 105 for intended output
        /// </summary>
        private void WireAdd()
        {
            _cpuEnables.Acc.Update(CreateEnableUpdate(_stepperOutput[5]));
            _cpuEnables.R0.Update(CreateEnableUpdate(_stepperOutput[4]));
            _cpuEnables.R1.Update(CreateEnableUpdate(_stepperOutput[3]));
            
            _cpuSets.Acc.Update(CreateSetUpdate(_stepperOutput[4]));
            _cpuSets.Tmp.Update(CreateSetUpdate(_stepperOutput[3]));
            _cpuSets.R0.Update(CreateSetUpdate(_stepperOutput[5]));
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

        private IBit CreateEnableUpdate(IBit other)
        {
            return _and.Apply(_clock.ClkE, other);
        }

        private IBit CreateSetUpdate(IBit other)
        {
            return _and.Apply(_clock.ClkS, other);
        }
    }
}