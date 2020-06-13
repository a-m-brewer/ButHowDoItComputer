using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Stepper : IStepper
    {
        private readonly IAnd _and;
        private readonly INot _not;
        private readonly IOr _or;
        private readonly List<IMemoryGate> _memoryGates;
        private bool _step7;

        public Stepper(IMemoryGateFactory memoryGateFactory, IAnd and, INot not, IOr or)
        {
            _and = and;
            _not = not;
            _or = or;
            _memoryGates = Enumerable.Range(0, 12).Select(_ => memoryGateFactory.Create()).ToList();
        }

        public StepperOutput Step(bool clk, bool reset)
        {
            var notReset = _not.Apply(reset);
            var notClk = _not.Apply(clk);

            var resetOrNotClk = _or.ApplyParams(notClk, reset);
            var clkOrReset = _or.ApplyParams(clk, reset);

            var m0 = _memoryGates[0].Apply(notReset, resetOrNotClk);
            var m1 = _memoryGates[1].Apply(m0, clkOrReset);
            var m2 = _memoryGates[2].Apply(m1, resetOrNotClk);
            var m3 = _memoryGates[3].Apply(m2, clkOrReset);
            var m4 = _memoryGates[4].Apply(m3, resetOrNotClk);
            var m5 = _memoryGates[5].Apply(m4, clkOrReset);
            var m6 = _memoryGates[6].Apply(m5, resetOrNotClk);
            var m7 = _memoryGates[7].Apply(m6, clkOrReset);
            var m8 = _memoryGates[8].Apply(m7, resetOrNotClk);
            var m9 = _memoryGates[9].Apply(m8, clkOrReset);
            var m10 = _memoryGates[10].Apply(m9, resetOrNotClk);
            var m11 = _memoryGates[11].Apply(m10, clkOrReset);

            var notM1 = _not.Apply(m1);
            var notM3 = _not.Apply(m3);
            var notM5 = _not.Apply(m5);
            var notM7 = _not.Apply(m7);
            var notM9 = _not.Apply(m9);
            var notM11 = _not.Apply(m11);

            var step1 = _or.ApplyParams(reset, notM1);
            var step2 = _and.ApplyParams(m1, notM3);
            var step3 = _and.ApplyParams(m3, notM5);
            var step4 = _and.ApplyParams(m5, notM7);
            var step5 = _and.ApplyParams(m7, notM9);
            var step6 = _and.ApplyParams(m9, notM11);
            _step7 = m11;

            var stepperArray = new[] {step1, step2, step3, step4, step5, step6, _step7};

            return new StepperOutput(stepperArray);
        }

        public StepperOutput Step(bool clk)
        {
            return Step(clk, _step7);
        }
    }
}