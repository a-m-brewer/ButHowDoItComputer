using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeMemoryGate<TBusDataType> : IBusDataTypeMemoryGate<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly INAnd _nAnd;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly bool[] _o;
        private readonly bool[] _c;

        public BusDataTypeMemoryGate(INAnd nAnd,  IBusDataTypeFactory<TBusDataType> busDataTypeFactory, int bits)
        {
            _nAnd = nAnd;
            _busDataTypeFactory = busDataTypeFactory;
            _o = new bool[bits];
            _c = new bool[bits];
        }

        public TBusDataType Apply(TBusDataType input, bool set)
        {
            var bits = new bool[input.Count];

            for (var i = 0; i < bits.Length; i++)
            {
                bits[i] = Apply(input[i], set, i);
            }

            return _busDataTypeFactory.CreateParams(bits);
        }
        
        private bool Apply(bool input, bool set, int index)
        {
            var a = _nAnd.ApplyParams(input, set);
            var b = _nAnd.ApplyParams(a, set);

            _c[index] = _nAnd.ApplyParams(b, _o[index]);
            _o[index] = _nAnd.ApplyParams(a, _c[index]);

            return _o[index];
        }
    }
}