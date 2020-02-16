using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Computer
    {
        public ICpuPinStates CpuPinStates { get; }
        public IComputerState ComputerState { get; }

        public Computer(ICpuPinStates cpuPinStates, IComputerState computerState)
        {
            CpuPinStates = cpuPinStates;
            ComputerState = computerState;
            // set counter to 0 as default starting point
            ComputerState.Iar.ApplyOnce(new Byte());
        }

        public void Step()
        {
            var pinStates = CpuPinStates.Step(ComputerState.Ir.Output, ComputerState.Flags.Output);
            ComputerState.UpdatePins(pinStates);
            Apply();
        }

        private void Apply()
        {
            ComputerState.Iar.Apply();
            ComputerState.Ram.Apply();
            
            // this is to make up for the fact that I can't seem to update bus during acc
            ComputerState.Bus.UpdateSubs();
        }
    }
}