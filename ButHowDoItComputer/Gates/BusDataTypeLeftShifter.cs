using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeLeftShifter<TBusDataType> : ILeftBusDataTypeShifter<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeLeftShifter(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
        }

        public (TBusDataType Ouput, bool ShiftOut) Shift(TBusDataType input, bool shiftIn)
        {
            var output = new List<bool> {shiftIn};
            
            output.AddRange(input.SkipLast(1));

            return (_busDataTypeFactory.Create(output.ToArray()), input.Last());
        }
    }
}