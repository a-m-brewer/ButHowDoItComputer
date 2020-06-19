using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeLeftShifter<TBusDataType> : ILeftBusDataTypeShifter<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeLeftShifter(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
        }

        public (TBusDataType Ouput, bool ShiftOut) Shift(TBusDataType input, bool shiftIn)
        {
            var output = new bool[input.Count];
            output[0] = shiftIn;

            for (var i = 1; i < output.Length; i++)
            {
                output[i] = input[i - 1];
            }

            return (_busDataTypeFactory.Create(output), input.Last());
        }
    }
}