using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class XOr : IXOr
    {
        private readonly INAnd _nAnd;
        private readonly INot _not;

        public XOr(INot not, INAnd nAnd)
        {
            _not = not;
            _nAnd = nAnd;
        }

        public bool ApplyParams(params bool[] bits)
        {
            return Apply(bits);
        }

        public bool Apply(IList<bool> bits)
        {
            if (bits.Count < 2) return false;

            var lastResult = ApplyXor(bits[0], bits[1]);

            for (var i = 2; i < bits.Count; i++) lastResult = ApplyXor(lastResult, bits[i]);

            return lastResult;
        }

        private bool ApplyXor(bool a, bool b)
        {
            var notA = _not.Apply(a);
            var notB = _not.Apply(b);

            var nAndA = _nAnd.ApplyParams(a, notB);
            var nAndB = _nAnd.ApplyParams(notA, b);

            return _nAnd.ApplyParams(nAndA, nAndB);
        }
    }
}