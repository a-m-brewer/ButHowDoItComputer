using System.Collections.Generic;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Computer<TDataType> where TDataType : IList<bool>
    {
        private readonly IBusDataTypeFactory<TDataType> _busDataTypeFactory;
        public ICpuPinStates<TDataType> CpuPinStates { get; }
        public IComputerState<TDataType> ComputerState { get; }

        public Computer(ICpuPinStates<TDataType> cpuPinStates, IComputerState<TDataType> computerState, IBusDataTypeFactory<TDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
            CpuPinStates = cpuPinStates;
            ComputerState = computerState;
            // set counter to 0 as default starting point
            ComputerState.Iar.ApplyOnce(busDataTypeFactory.Create());
        }

        public void Step()
        {
            var pinStates = CpuPinStates.Step(ComputerState.Ir.Output, ComputerState.Flags.Output);
            ComputerState.UpdatePins(pinStates);
            Apply();
            
            if (pinStates.ClockOutput.AllOff)
            {
                ComputerState.Bus.UpdateData(new BusMessage<TDataType> {Data = _busDataTypeFactory.Create(), Name = "Bus"});
            }
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