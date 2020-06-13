using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Shifter<TBusDataType> : IShifter<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        protected Shifter(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
            ShiftIn = false;
            ShiftOut = false;
        }

        public bool ShiftIn { get; set; }
        public bool ShiftOut { get; set; }

        public void Apply(IRegister<TBusDataType> inputRegister, IRegister<TBusDataType> outputRegister)
        {
            inputRegister.Apply();
            var secondRegisterInput = GetShifter(inputRegister);
            outputRegister.Input = _busDataTypeFactory.Create(secondRegisterInput);
            outputRegister.Apply();
        }

        protected virtual bool[] GetShifter(IRegister<TBusDataType> inputRegister)
        {
            ShiftOut = inputRegister.Output[0];

            var output = inputRegister.Output.Skip(1).ToList();
            output.Add(ShiftIn);
            
            return output.ToArray();
        }
    }
}