using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class MemoryGate : IMemoryGate
    {
        private readonly INAnd _nAnd;
        private IBit _o;
        private IBit _c;

        public MemoryGate(INAnd nAnd, IBitFactory bitFactory)
        {
            _nAnd = nAnd;
            var bitFactory1 = bitFactory;
            _o = bitFactory1.Create(false);
            _c = bitFactory1.Create(false);
        }
        
        public IBit Apply(IBit input, IBit set)
        {
            var a = _nAnd.Apply(input, set);
            var b = _nAnd.Apply(a, set);

            _c = _nAnd.Apply(b, _o);
            _o = _nAnd.Apply(a, _c);

            return _o;
        }
    }
}