﻿using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeRightShifter<TBusDataType> : IRightBusDataTypeShifter<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeRightShifter(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
        }

        public (TBusDataType Ouput, bool ShiftOut) Shift(TBusDataType input, bool shiftIn)
        {
            var output = input.Skip(1).ToList();
            
            output.Add(shiftIn);
            
            return (_busDataTypeFactory.Create(output.ToArray()), input[0]);
        }
    }
}