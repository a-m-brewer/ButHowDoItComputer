using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BaseBusDataTypeGate<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IGate _gate;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BaseBusDataTypeGate(IGate gate, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _gate = gate;
            _busDataTypeFactory = busDataTypeFactory;
        }
        
        public TBusDataType Apply(params TBusDataType[] input)
        {
            var result = new bool[input[0].Count];

            for (var i = 0; i < result.Length; i++)
            {
                var toOr = new bool[input.Length];

                for (var j = 0; j < toOr.Length; j++)
                {
                    toOr[j] = input[j][i];
                }
                
                result[i] = _gate.Apply(toOr);
            }

            return _busDataTypeFactory.CreateParams(result);   
        }
    }
}