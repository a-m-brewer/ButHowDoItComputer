using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Clock : IClock
    {
        private readonly IOr _or;
        private readonly IClockState _clk;

        private bool _clkCycledLast;
        private readonly IClockState _clkD;

        public Clock(IClockStateFactory clockStateFactory, IOr or)
        {
            _or = or;
            _clk = clockStateFactory.Create();
            _clkD = clockStateFactory.Create();
            ClkE = false;
            ClkS = false;
        }

        public bool ClkS { get; private set; }

        public bool ClkE { get; private set; }

        public bool ClkD => _clkD.Bit;

        public bool Clk => _clk.Bit;

        public ClockOutput Cycle()
        {
            ApplyClkAndClkD();
            ApplyCycleE();
            ApplyCycleS();

            return new ClockOutput
            {
                Clk = Clk,
                ClkD = ClkD,
                ClkE = ClkE,
                ClkS = ClkS
            };
        }

        public void Apply()
        {
            Cycle();
        }

        public bool Set
        {
            get => ClkS;
            set => ClkS = value;
        }

        public bool Enable
        {
            get => ClkE;
            set => ClkE = value;
        }

        private void ApplyClkAndClkD()
        {
            if (_clkCycledLast)
                _clkD.Cycle();
            else
                _clk.Cycle();

            _clkCycledLast = !_clkCycledLast;
        }

        private void ApplyCycleE()
        {
            ClkE = _or.ApplyParams(Clk, ClkD);
        }

        private void ApplyCycleS()
        {
            ClkS = Clk && ClkD;
        }
    }
}